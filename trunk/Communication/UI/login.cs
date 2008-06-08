using System;
using System.Reflection;
using System.Resources;
using System.Collections.Specialized;
using System.Data;
using System.Collections;

namespace POP3Pipe
{
	public class Login : UI.Page {
		protected static log4net.ILog log  = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		// Labels
		protected anmar.SharpWebMail.UI.WebControls.Label errorMsgLogin;
		protected System.Web.UI.WebControls.Label loginWindowPassword;
		protected System.Web.UI.WebControls.Label loginWindowTitle;
		protected System.Web.UI.WebControls.Label loginWindowUsername;

		// Input boxes
		protected System.Web.UI.HtmlControls.HtmlInputText username;
		protected System.Web.UI.HtmlControls.HtmlInputText password;

		//Other form elements
		protected System.Web.UI.WebControls.Button loginButton;
		protected System.Web.UI.HtmlControls.HtmlForm LoginForm;
		protected System.Web.UI.WebControls.Literal loginWindowHeadTitle;
		protected System.Web.UI.HtmlControls.HtmlSelect selectculture;
		protected System.Web.UI.HtmlControls.HtmlSelect selectserver;
		protected System.Web.UI.WebControls.RegularExpressionValidator usernameValidator;

		protected String PrepareLogin ( String user ) {
			// Remove comments allowed by addr-spec
			user = SharpMimeTools.uncommentString (user);
			String[] tmp = user.Split ('@');
			if ( tmp.Length==2 )
				// Remove space surrounding local-part and domain allowed by addr-spec
				user = String.Format ("{0}@{1}", tmp[0].Trim(), tmp[1].Trim());
			// TODO: limit user length
			return user;
		}
		/*
		 * Events
		*/
		protected void CultureChange (Object sender, EventArgs args ) {
			if ( selectculture!=null ) {
				try {
					Session["resources"] = ((ResourceManager)Application["resources"]).GetResourceSet(new System.Globalization.CultureInfo(selectculture.Value), true,true);
					Session["effectiveculture"] = selectculture.Value;
				} catch (Exception e ) {
					if ( log.IsErrorEnabled )
						log.Error(String.Concat("Error changing culture to [", selectculture.Value, "]"), e);
				}
			}
		}
		protected void Login_Click (Object sender, EventArgs args ) {
			// authenticate user
			if (this.IsValid) {
				int login_mode = (int)Application["sharpwebmail/login/mode"];
				if ( login_mode==3 && Application["sharpwebmail/login/append"]!=null ) {
					if ( this.username.Value.IndexOf ("@") == -1 ) {
						this.username.Value = String.Format ( "{0}@{1}", this.username.Value, Application["sharpwebmail/login/append"]);
					}
					this.usernameValidator.ValidationExpression = "^" + SharpMimeTools.ABNF.addr_spec + "$";
					this.usernameValidator.Validate();
				}
				if ( this.IsValid ) {
					this.username.Value=this.PrepareLogin(this.username.Value);
					ServerSelector selector = (ServerSelector)Application["sharpwebmail/read/servers"];
					EmailServerInfo server = null;
					if ( selectserver!=null && selectserver.Visible )
						server = selector.Select(this.selectserver.Value, false);
					else
						server = selector.Select(this.username.Value, true);
					IEmailClient client = EmailClientFactory.CreateEmailClient(server, this.username.Value, password.Value );
					CTNInbox inbox = (CTNInbox)Session["inbox"];
					inbox.Client = client;
					System.String folder = Page.Request.QueryString["mode"];
					if ( folder==null )
						folder = "inbox";
					inbox.CurrentFolder = folder;
					if ( client!=null && client.GetFolderIndex ( inbox, 0, (int)Application["sharpwebmail/read/inbox/pagesize"], true ) ) {
						Session["client"] = client;
						Session["inbox"] = inbox;
						if ( Application["sharpwebmail/send/addressbook"]!=null ) {
							SortedList addressbooks = (SortedList)Application["sharpwebmail/send/addressbook"];
							foreach (ListDictionary addressbook in addressbooks.Values ) {
								if ( addressbook.Contains("searchstringrealname") ) {
									DataTable result = AddressBook.GetDataSource(addressbook, true, client);
									if ( result==null )
										continue;
									if ( result.Rows.Count==1 ) {
										Session["DisplayName"] = result.Rows[0][addressbook["namecolumn"].ToString()];
										break;
									}
								}
							}
						}
						if ( log.IsDebugEnabled )
							log.Debug (String.Concat("Successful authentication for user [", this.username.Value, "], found [", inbox.Count, "] messages. Setting cookie and redirecting."));
						System.Web.Security.FormsAuthentication.RedirectFromLoginPage(this.username.Value, false);
					} else {
						errorMsgLogin.Visible=true;
					}
					client = null;
					inbox = null;
					selector = null;
				}
			}
		}

		/*
		 * Page Events
		*/
		protected void Page_Init () {
			if ( !this.IsPostBack ) {
				if ( selectculture!=null ) {
					selectculture.DataSource = Application["AvailableCultures"];
					selectculture.DataTextField = "Key";
					selectculture.DataValueField = "Value";
					selectculture.DataBind();
					if ( selectculture.Items.Count==0 )
						selectculture.Visible = false;
					else
						selectculture.Value = Session["effectiveculture"].ToString();
				}
				if ( selectserver!=null && Application["sharpwebmail/login/serverselection"]!=null && Application["sharpwebmail/login/serverselection"].Equals("manual") ) {
					ServerSelector selector = (ServerSelector)Application["sharpwebmail/read/servers"];
					selectserver.DataSource = selector.Servers;
					selectserver.DataTextField = "Name";
					selectserver.DataBind();
					if ( selectserver.Items.Count!=0 )
						selectserver.Visible = true;
				}
			}
		}
		protected void Page_Load (Object sender, EventArgs args ) {
			// Prevent caching, so can't be viewed offline
			Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
			if ( !this.IsPostBack ) {
				//Localized resources for this session
				ResourceSet rs = (ResourceSet) Session["resources"];
				// Set labels localized texts
				loginWindowTitle.Text = rs.GetString("loginWindowTitle") + ": " + Application["sharpwebmail/login/title"].ToString();
				loginWindowHeadTitle.Text = Application["sharpwebmail/general/title"].ToString();
	            loginWindowUsername.Text = rs.GetString("loginWindowUsername");
				loginWindowPassword.Text = rs.GetString("loginWindowPassword");
				loginButton.Text = rs.GetString("loginButton");
				errorMsgLogin.Text = rs.GetString("errorMsgLogin");
				switch ( (int)Application["sharpwebmail/login/mode"] ) {
					case 2:
						loginWindowUsername.Text = rs.GetString("loginWindowUsername2");
						this.usernameValidator.ValidationExpression = ".+";
						break;
					case 3:
						this.usernameValidator.ValidationExpression = "^" + SharpMimeTools.ABNF.local_part + "(@" + anmar.SharpMimeTools.ABNF.domain + "$){0,1}";
						break;
					case 1:
					default:
						this.usernameValidator.ValidationExpression = "^" + SharpMimeTools.ABNF.addr_spec + "$";
						break;
				}
				rs = null;
				if ( (bool)Application["sharpwebmail/login/enablequerystringlogin"] ) {
					String username = Request.QueryString["username"];
					String password = Request.QueryString["password"];
					if ( username!=null ) {
						this.username.Value = username;
						if ( password!=null ) {
							this.password.Value = password;
							this.Validate();
							this.Login_Click(sender, args);
						}
					}
				}
			}
		}

	}
}
