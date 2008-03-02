using System;
using System.IO;

namespace POP3Pipe
{
	public interface IEmailClient {
		bool GetFolderIndex ( CTNInbox inbox, int npage, int npagesize, bool askserver );
		bool PurgeInbox ( CTNInbox inbox, bool all );
		bool GetMessage ( MemoryStream Message, int mindex, String uidl );
		String UserName { get;}
		String Password { get;}
	}
}
