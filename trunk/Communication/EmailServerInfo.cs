using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net;

namespace POP3Pipe
{
	public class EmailServerInfo {
		private static log4net.ILog log  = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private Regex _condition = null;
		private ServerProtocol _protocol;
		private String _host;
		private String _name = null;
		private int _port;

		public EmailServerInfo ( ServerProtocol protocol, String host, int port ) {
			this._protocol = protocol;
			this._host = host;
			this._port = port;
		}

		public EmailServerInfo ( String protocol, String host, String port ) {
			this._protocol = EmailServerInfo.ParseProtocol(protocol);
			this._host = EmailServerInfo.ParseHost(host);
			this._port = EmailServerInfo.ParsePort(port, this._protocol);
		}

		public Regex Condition {
			get {
				return this._condition;
			}
		}

		public String Host {
			get {
				return this._host;
			}
		}

		public String Name {
			get {
				return this._name;
			}
			set {
				this._name = value;
			}
		}

		public int Port {
			get {
				return this._port;
			}
		}

		public ServerProtocol Protocol {
			get {
				return this._protocol;
			}
		}

		private static int GetDefaultPort ( ServerProtocol protocol ) {
			switch ( protocol ) {
				case ServerProtocol.Imap:
					return 143;
				case ServerProtocol.Pop3:
					return 110;
				case ServerProtocol.Smtp:
					return 25;
			}
			return 0;
		}

		public bool IsValid () {
			return ( !this._protocol.Equals(ServerProtocol.Unknown) && this._port>0 && this._host!=null && (this._condition!=null || this._name!=null) );
		}

		public static EmailServerInfo Parse ( String value ) {
			EmailServerInfo server = null;
			String[] values = value.ToString().Split(':');
			if ( values.Length==3 ) {
				ServerProtocol protocol = EmailServerInfo.ParseProtocol(values[0]);
				String host = EmailServerInfo.ParseHost(values[1]);
				int port = EmailServerInfo.ParsePort(values[2], protocol);
				if ( !protocol.Equals(ServerProtocol.Unknown) && port>0 && host!=null ) {
					server = new EmailServerInfo(protocol, host, port);
				}
			}
			return server;
		}

		private static String ParseHost ( String value ) {
			try {
				Dns.Resolve(value);
			} catch ( Exception e ) {
				if ( log.IsErrorEnabled )
					log.Error(String.Format("Error parsing host: {0}", value), e);
				return null;
			}
			return value;

		}

		private static int ParsePort ( String value, ServerProtocol protocol ) {
			int port;
			try {
				port = Int32.Parse(value);
			} catch ( System.Exception e ) {
				if ( log.IsErrorEnabled )
					log.Error(System.String.Format("Error parsing port: {0}", value), e);
				port = anmar.SharpWebMail.EmailServerInfo.GetDefaultPort(protocol);
			}
			return port;
		}

		private static ServerProtocol ParseProtocol ( String value ) {
			ServerProtocol protocol;
			try {
				protocol = (ServerProtocol)System.Enum.Parse(typeof(ServerProtocol), value, true);
			} catch ( Exception e ) {
				if ( log.IsErrorEnabled )
					log.Error(String.Format("Error parsing protocol: {0}", value), e);
				protocol = ServerProtocol.Unknown;
			}
			return protocol;
		}

		public void SetCondition ( String pattern ) {
			try {
				if ( pattern.Equals("*") )
					pattern = ".*";
				this._condition = new Regex(pattern, RegexOptions.IgnoreCase|RegexOptions.ECMAScript);
			} catch ( Exception e ) {
				if ( log.IsErrorEnabled )
					log.Error(String.Format("Error parsing pattern: {0}", pattern), e);
			}
		}

		public override String ToString () {
			if ( this._name!=null )
				return this._name;
			else
				return this._host;
		}
	}

	public enum ServerProtocol {
		/// <summary>
		/// IMAP. Read RFC 3501
		/// </summary>
		Imap,
		/// <summary>
		/// POP3. Read RFC 3461
		/// </summary>
		Pop3,
		/// <summary>
		/// SMTP. Read RFC 2821
		/// </summary>
		Smtp,
		/// <summary>
		/// SMTP combined with SMTP AUTH
		/// </summary>
		SmtpAuth,
		/// <summary>
		/// Unknown protocol
		/// </summary>
		Unknown
	}
}
