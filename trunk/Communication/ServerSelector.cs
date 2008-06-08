using System;
using System.Collections;
using System.Reflection;

namespace POP3Pipe
{
	public class ServerSelector {
		private static log4net.ILog log  = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private ArrayList _servers;

		public ServerSelector () {
			this._servers = new ArrayList();
		}

		public ICollection Servers {
			get {
				return this._servers;
			}
		}

		public void Add ( Object key, Object value ) {
			if ( key==null || value ==null )
				throw new ArgumentNullException();

			EmailServerInfo server = EmailServerInfo.Parse(value.ToString());
			if ( server!=null ) {
				server.SetCondition(key.ToString());
				if ( server.IsValid() )
					this._servers.Add (server);
			}
		}

		public void Add ( EmailServerInfo server ) {
			if ( server==null || !server.IsValid() )
				throw new ArgumentNullException();
			this._servers.Add (server);
		}

		public EmailServerInfo Select ( String key, bool match ) {
			foreach(EmailServerInfo item in this._servers ) {
				if ( item.Condition!=null && match ) {
					if ( item.Condition.IsMatch(key) ) {
						if ( log.IsDebugEnabled )
							log.Debug (String.Concat("[", item.Name, "] selected for condicion [", item.Condition, "] and input [", key, "]" ));
						return item;
					}
				} else if ( !match && item.Name!=null && item.Name.Equals(key) ) {
					if ( log.IsDebugEnabled )
						log.Debug (String.Concat("[", item.Name, "] selected for input [", key, "]" ));
					return item;
				}
			}
			return null;
		}
	}
}
