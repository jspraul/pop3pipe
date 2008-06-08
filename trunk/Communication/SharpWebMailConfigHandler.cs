using System;
using System.Configuration;
using System.Reflection;
using System.Collections.Specialized;
using System.Collections;
using System.Xml;

namespace POP3Pipe
{
	public class SharpWebMailConfigHandler : IConfigurationSectionHandler {
		private static log4net.ILog log  = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public virtual Object Create( Object parent, Object context, XmlNode section ) {
			Hashtable config = CollectionsUtil.CreateCaseInsensitiveHashtable();
			InitConfigDefaults(config);
			ParseNode(parent, context, section, config, "sharpwebmail");
			return config;
		}
		private void ParseNode ( Object parent, Object context, XmlNode node, Hashtable config, String prefix ) {
			foreach ( XmlNode item in node.ChildNodes ) {
				if ( item.NodeType.Equals(XmlNodeType.Element) ) {
					String sectionname = String.Concat(prefix, "/", item.Name);;
					switch ( item.Name ) {
						case "general":
						case "login":
						case "message":
						case "inbox":
							SingleTagSectionHandler singlesection = new SingleTagSectionHandler();
							InitConfigSection(config, sectionname, singlesection.Create(parent, context, item) as System.Collections.Hashtable);
							break;
						case "read":
						case "send":
							if ( item.HasChildNodes )
								ParseNode( parent, context, item, config, sectionname );
							break;
						case "servers":
							if ( item.HasChildNodes )
								config.Add(sectionname, ParseConfigServers(item.ChildNodes));
							break;
						case "addressbook":
							if ( !config.Contains(sectionname) )
								config.Add(sectionname, new SortedList());
							SortedList addressbooks = (SortedList)config[sectionname];
							Hashtable tmpaddressbook = (Hashtable)(new System.Configuration.SingleTagSectionHandler()).Create(parent, context, item);
							ListDictionary addressbook = new ListDictionary(new System.Collections.CaseInsensitiveComparer());
							foreach ( String configitem in tmpaddressbook.Keys) {
								addressbook.Add(configitem, tmpaddressbook[configitem]);
							}
							tmpaddressbook = null;
							if ( addressbook.Contains("type") && !addressbook["type"].Equals("none") && addressbook.Contains("name") && !addressbooks.Contains(addressbook["name"]) ) {
								if ( addressbook.Contains("pagesize") )
									addressbook["pagesize"] = ParseConfigElement(addressbook["pagesize"].ToString(), 10);
								else
									addressbook["pagesize"] = 10;
								addressbooks.Add(addressbook["name"], addressbook);
								if ( addressbook.Contains("allowupdate") )
									addressbook["allowupdate"] = ParseConfigElement(addressbook["allowupdate"].ToString(), false);
								else
									addressbook["allowupdate"] = false;
							}
							break;
					}
				}
			}
		}

		private void InitConfigDefaults (Hashtable config) {
			config.Add ( "sharpwebmail/general/addressbooks", false );
			config.Add ( "sharpwebmail/general/default_lang", "en" );
			config.Add ( "sharpwebmail/general/title", String.Empty );
			config.Add ( "sharpwebmail/login/append", String.Empty );
			config.Add ( "sharpwebmail/login/enablequerystringlogin", false );
			config.Add ( "sharpwebmail/login/mode", 1 );
			config.Add ( "sharpwebmail/login/serverselection", String.Empty );
			config.Add ( "sharpwebmail/login/title", String.Empty );
			config.Add ( "sharpwebmail/read/inbox/commit_onexit", true );
			config.Add ( "sharpwebmail/read/inbox/commit_ondelete", false );
			config.Add ( "sharpwebmail/read/inbox/pagesize", 10 );
			config.Add ( "sharpwebmail/read/inbox/sort", "msgnum DESC" );
			config.Add ( "sharpwebmail/read/inbox/stat", 2 );
			config.Add ( "sharpwebmail/read/message/commit_ondelete", false );
			config.Add ( "sharpwebmail/read/message/sanitizer_mode", 0 );
			config.Add ( "sharpwebmail/read/message/temppath", String.Empty );
			config.Add ( "sharpwebmail/send/message/attach_ui", "normal" );
			config.Add ( "sharpwebmail/send/message/forwardattachments", true );
			config.Add ( "sharpwebmail/send/message/replyquotechar", "> " );
			config.Add ( "sharpwebmail/send/message/replyquotestyle", "padding-left: 5px; margin-left: 5px; border-left: #0000ff 2px solid; margin-left: 0px" );
			config.Add ( "sharpwebmail/send/message/sanitizer_mode", 0 );
			config.Add ( "sharpwebmail/send/message/smtp_engine", String.Empty );
			config.Add ( "sharpwebmail/send/message/temppath", String.Empty );
			config.Add ( "sharpwebmail/read/message/useserverencoding", false );
		}

		private void InitConfigSection (Hashtable config, String section, Hashtable configsection ) {
			foreach (DictionaryEntry item in configsection ) {
				String config_item = System.String.Concat(section, "/", item.Key);
				config[config_item] = ParseConfigElement(item.Value.ToString(), config[config_item]);
			}
		}

		private Object ParseConfigElement (String value, Object defaultvalue ) {
			if ( value==null )
				return defaultvalue;
			try {
				if ( defaultvalue.GetType().Equals(typeof(int)) )
					return Int32.Parse(value);
				else if ( defaultvalue.GetType().Equals(typeof(bool)) )
					return Boolean.Parse(value);
				else
					return value;
			} catch ( Exception e ) {
				if ( log.IsErrorEnabled )
					log.Error("Error parsing value", e);
				return defaultvalue;
			}
		}

		private ServerSelector ParseConfigServers (XmlNodeList list ) {
			ServerSelector selector = new ServerSelector();
			foreach (XmlNode item in list ) {
				if ( item.NodeType.Equals(XmlNodeType.Element) && (item.LocalName.Equals("server") || item.LocalName.Equals("add")) ) {
					XmlElement element = (XmlElement)item;
					if ( element.HasAttribute("key") && element.HasAttribute("value") ) // Old format
						selector.Add(element.GetAttribute("key"), element.GetAttribute("value"));
					else if ( element.HasAttribute("protocol") && element.HasAttribute("host") && element.HasAttribute("port") ) { // New format
						anmar.SharpWebMail.EmailServerInfo server = new anmar.SharpWebMail.EmailServerInfo(element.GetAttribute("protocol"), element.GetAttribute("host"), element.GetAttribute("port"));
						if ( element.HasAttribute("regexp") )
							server.SetCondition (element.GetAttribute("regexp"));
						if ( element.HasAttribute("name") )
							server.Name = element.GetAttribute("name");

						if ( server.IsValid() )
							selector.Add(server);
					}
				}
			}
			return selector;
		}
	}
}
