using System;
using anmar.SharpWebMail;

namespace POP3Pipe
{
	public sealed class EmailClientFactory {
		public static IEmailClient CreateEmailClient ( EmailServerInfo server, String username, String password ) {
			if ( server==null )
				return null;
			switch ( server.Protocol ) {
				case ServerProtocol.Pop3:
					return new CTNSimplePOP3Client ( server.Host, server.Port, username, password );
				case ServerProtocol.Imap:
					return new SimpleIMAPClient( server.Host, server.Port, username, password );
			}
			return null;
		}
	}
}
