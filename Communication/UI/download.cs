using System;

namespace POP3Pipe
{
	public class download : anmar.SharpWebMail.UI.Page {

		protected void Page_Load( System.Object sender, System.EventArgs args ) {
			//Our Inbox
			CTNInbox inbox = (anmar.SharpWebMail.CTNInbox)Session["inbox"];

			String msgid = System.Web.HttpUtility.HtmlEncode (Page.Request.QueryString["msgid"]);
			String name = Page.Request.QueryString["name"];
			String inline = Page.Request.QueryString["i"];
			if ( msgid != null && name!=null && Session["sharpwebmail/read/message/temppath"]!=null ) {
				Object[] details = inbox[ msgid ];
				if ( details != null && details.Length>0 ) {
					String path = Session["sharpwebmail/read/message/temppath"].ToString();
					try {
						path = System.IO.Path.Combine (path, msgid);
					} catch ( System.ArgumentException ) {
						// Remove invalid chars
						foreach ( char ichar in System.IO.Path.InvalidPathChars ) {
							msgid = msgid.Replace ( ichar.ToString(), System.String.Empty );
						}
						path = System.IO.Path.Combine (path, msgid);
					}
					try {
						name = System.IO.Path.GetFileName(name);
					} catch ( System.ArgumentException ) {
						// Remove invalid chars
						foreach ( char ichar in System.IO.Path.InvalidPathChars ) {
							name = name.Replace ( ichar.ToString(), System.String.Empty );
						}
						name = System.IO.Path.GetFileName(name);
					}
					System.IO.FileInfo file = new System.IO.FileInfo ( System.IO.Path.Combine (path, name) );
					System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo ( path );
					if ( dir.Exists && file.Exists && dir.FullName.Equals (new System.IO.DirectoryInfo (file.Directory.FullName).FullName) ) {
						Page.Response.Clear();
						//FIXME: return correct Content-Type
						Response.AppendHeader("Content-Type", "application/octet-stream");
						System.String header;
						if ( inline!=null && inline.Equals("1") )
							header = "inline";
						else
							header = "attachment";
						header = System.String.Format ("{0}; filename=\"{1}\"; size=\"{2}\";", header, name, file.Length);
						Response.AppendHeader("Content-Disposition", header);
						Response.AppendHeader("Content-Length", file.Length.ToString());
						Response.WriteFile ( file.FullName );
					}
					file = null;
					dir = null;
				}
				details = null;
			}
			inbox = null;
		}
	}
}
