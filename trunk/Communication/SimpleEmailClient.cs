using System;
using System.Reflection;
using System.Data;
using System.Collections;
using System.IO;
using System.Text;

namespace POP3Pipe
{
	internal abstract class SimpleEmailClient : IEmailClient {
		protected static log4net.ILog log  = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private CTNSimpleTCPClient client;

		protected Boolean connected;

		protected Int32 portnumber;

		protected String hostname;

		protected String lastResponse;

		protected String password;

		protected String username;

		protected String commandEnd;

		protected bool responseEndOnEnd;

		protected String responseEnd;

		protected String responseEndSL;
		
		public SimpleEmailClient( String host, Int32 port, String user, String pass ) {
			client = new CTNSimpleTCPClient();
			this.hostname = host;
			this.password = pass;
			this.portnumber = port;
			this.username = user;
		}

		public SimpleEmailClient( String host, Int32 port, String user, String pass, long timeout ) {
			client = new CTNSimpleTCPClient(timeout);
			this.hostname = host;
			this.password = pass;
			this.portnumber = port;
			this.username = user;
		}

		protected abstract String buildcommand ( EmailClientCommand cmd, params Object[] args );

		protected abstract bool commandResponseTypeIsSL ( EmailClientCommand cmd, params Object[] args );

		protected bool connect () {
			bool error = false;
			String response = null;

			if (!this.connected) {
				// Connect to email server
				error = !client.connect( this.hostname, this.portnumber );
				
				if (!error) {
					this.connected = true;
					error = !client.readResponse( ref response, responseEndSL, true );
					error = (error)?true:!this.evaluateresponse( response );
					this.lastResponse = response;
				}
			}
			return !error;
		}

		protected virtual bool delete ( int mindex ) {
			bool error = false;

			// Send delete command for mindex message
			String cmd = this.buildcommand(EmailClientCommand.Delete, mindex);
			error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand( EmailClientCommand.Delete, cmd );

			return !error;
		}

		protected virtual bool deletemessages ( DataView result ) {
			bool error = false;
			foreach ( DataRowView msg in result )
				error = (error)?error:!this.delete ( (int)msg[1] );
			return !error;
		}

		protected bool disconnect () {
			bool error = false;

			if (this.connected) {
				// Disconnect from email server
				error = !client.closeConnection();
				this.connected = false;
			}

			return !error;
		}

		protected abstract bool evaluateresponse ( String response );

		public virtual bool GetFolderIndex ( CTNInbox inbox, int npage, int npagesize, bool askserver ) {
			bool error = false;
			int total = 0;
			int totalbytes = 0;
			Hashtable list = new Hashtable();

			if ( !askserver ) {
				error = !inbox.buildMessageList ( list, npage, npagesize );
				askserver = (!error&&list.Count>0)?!askserver:askserver;
			}
			if ( askserver ) {
				Hashtable messages = new Hashtable();
				error = !this.connect();
				error = (error)?error:!this.login ( this.username, this.password );
				error = (error)?error:!this.status ( ref total, ref totalbytes );

				error = (error)?error:!this.getListToIndex ( list, total, inbox, npage, npagesize );

				if ( !error && total>0 && list.Count>0 ) {
					MemoryStream header = null;
					foreach ( DictionaryEntry msg in list ) {
						error = (error)?error:!this.getMessageHeader ( out header, (int) msg.Key );
						if ( !error )
							messages.Add(msg.Value, header);
					}
				}
				this.quit();
				foreach ( DictionaryEntry item in messages ) {
					MemoryStream stream = this.getStreamDataPortion(item.Value as MemoryStream);
					SharpMimeHeader header = new SharpMimeHeader( stream, stream.Position );
					header.Close();
					inbox.newMessage ( item.Key.ToString(), header );
				}
			}
			return !error;
		}

		private bool getListToIndex ( Hashtable msgs, int total, CTNInbox inbox, int npage, int npagesize ) {
			bool error = false;

			Int32[] list = new Int32[total];
			String[] uidlist = new String[total];
			if ( total>0 ) {
				// Get uid list
				error = (error)?error:!this.uidl ( uidlist, 0);
				//Get messages list
				error = (error)?error:!this.list ( list );
			}
			// Prepare message table with new messages
			error = (error)?error:!inbox.buildMessageTable ( list, uidlist );

			list = null;
			uidlist =  null;

			//Determine what messages we have to index
			if ( msgs!=null )
				error = (error)?error:!inbox.buildMessageList ( msgs, npage, npagesize );

			return !error;
		}

		public bool GetMessage ( MemoryStream message, int mindex, String uid ) {
			bool error = false;

			error = !this.connect();
			error = (error)?true:!this.login ( this.username, this.password );
			if ( !error && uid!=null ) {
				String[] uidllist = new String[mindex];
				error = !this.uidl( uidllist, mindex );
				// Make sure mindex message is there and its UID is uid
				if ( error || uidllist[mindex-1]==null || !uidllist[mindex-1].Equals(uid) ) {
					error = true;
				}
				uidllist=null;
			}
			error = (error)?true:!this.retrieve ( mindex, message );
			if ( !error )
				message = this.getStreamDataPortion(message);
			this.quit();

			return !error;
		}

		protected bool getMessageHeader ( out MemoryStream stream, int mindex ) {
			bool error = false;
			stream = new MemoryStream ();
			error = (error)?error:!this.header ( mindex, 0, stream );
			return !error;
		}

		protected abstract MemoryStream getStreamDataPortion ( MemoryStream data );

		protected virtual bool header ( int mindex, ulong nlines, MemoryStream response ) {
			bool error = false;

			String cmd = this.buildcommand(EmailClientCommand.Header, mindex, nlines);
			bool SLResponse = this.commandResponseTypeIsSL(EmailClientCommand.Header, mindex, nlines);
			error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand( EmailClientCommand.Header, cmd, response, SLResponse );

			return !error;
		}

		protected virtual bool login ( String user, String pass ) {
			bool error = false;

			String cmd = this.buildcommand(EmailClientCommand.Login, user, pass);
			error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand( EmailClientCommand.Login, cmd );

			return !error;
		}

		protected virtual bool list ( Int32[] list ) {
			bool error = false;
			MemoryStream response = new MemoryStream();

			// Send LIST and parse response
			String cmd = this.buildcommand(EmailClientCommand.ListSize);
			bool SLResponse = this.commandResponseTypeIsSL(EmailClientCommand.ListSize);

			error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand( EmailClientCommand.ListSize, cmd, response, SLResponse );

			//Parse the result
			error = (error)?true:!this.parseListSize(list, response);

			return !error;
		}

		protected virtual void parseLastResponse ( MemoryStream response, bool SLReponse ) {
			Byte[] readBytes = new Byte[3];
			response.Seek(0,0);
			response.Read (readBytes,  0, 3);
			this.lastResponse = Encoding.ASCII.GetString(readBytes, 0, 3);
			response.Seek(0,0);
		}

		protected abstract bool parseListSize ( Int32[] list, MemoryStream response );

		protected abstract bool parseListUID ( String[] list, MemoryStream response, int mindex );

		protected abstract bool parseStatus ( ref int num, ref int numbytes, MemoryStream response );

		public bool PurgeInbox ( CTNInbox inbox, bool all ) {
			bool error = false;
			String filter;
			if ( all )
				filter = String.Empty;
			else
				filter = "delete=true";
			DataView result = inbox.Inbox;
			result.RowFilter = filter;
			if ( result.Count>0 ) {
				int total = 0, totalbytes = 0;
				error = !this.connect();
				error = (error)?error:!this.login ( this.username, this.password );
				error = (error)?error:!this.status ( ref total, ref totalbytes );
				error = (error)?error:!this.getListToIndex ( null, total, inbox, 0, 0 );
				result.RowFilter = filter;
				error = (error)?error:!this.deletemessages(result);
				error = (error)?error:!this.getListToIndex ( null, total, inbox, 0, 0 );
				this.quit();
			}
			result.RowFilter = String.Empty;
			return !error;
		}

		protected virtual bool retrieve ( int mindex, MemoryStream response ) {
			bool error = false;

			String cmd = this.buildcommand(EmailClientCommand.Message, mindex);
			bool SLResponse = this.commandResponseTypeIsSL(EmailClientCommand.Message, mindex);
			error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand( EmailClientCommand.Message, cmd, response, SLResponse );

			return !error;
		}

		protected virtual bool sendCommand ( EmailClientCommand command, String cmd ) {
			bool error = false;
			System.String response = null;

			//Send cmd and evaluate response
			if (connected) {
				// Send cmd
				error = !client.sendCommand( cmd, commandEnd );
				// Read Response
				error = (error)?true:!client.readResponse(ref response, responseEndSL, true);
				// Evaluate the result
				error = (error)?true:!this.evaluateresponse(response);
				if ( error || !command.Equals(EmailClientCommand.Logout) ) {
					this.lastResponse = response;
				}
			} else {
				error = true;
			}

			return !error;
		}

		protected virtual bool sendCommand ( EmailClientCommand command, String cmd, MemoryStream response, bool SLReponse ) {
			bool error = false;

			//Send cmd and evaluate response
			if (connected) {
				// Send cmd
				error = !client.sendCommand( cmd, commandEnd );
				// Read Response
				error = (error)?true:!client.readResponse( response, ((SLReponse)?responseEndSL:responseEnd), (SLReponse||this.responseEndOnEnd));
				// Get the last response string from a multiline response
				this.parseLastResponse( response, SLReponse);
				// Evaluate the result
				error = (error)?true:!this.evaluateresponse( this.lastResponse );
				response.Seek(0, SeekOrigin.Begin);
			} else {
				error = true;
			}

			return !error;
		}

		protected virtual bool status ( ref int num, ref int numbytes ) {
			bool error = false;
			MemoryStream response = new MemoryStream();
			// Get status command
			String cmd = this.buildcommand(EmailClientCommand.Status);
			bool SLResponse = this.commandResponseTypeIsSL(EmailClientCommand.Status);
			error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand( EmailClientCommand.Status, cmd, response, SLResponse );

			//Parse the result
			error = (error)?true:!this.parseStatus(ref num, ref numbytes, response);

			return !error;
		}

		protected virtual bool uidl ( String[] list, int mindex ) {
			bool error = false;
			MemoryStream response = new MemoryStream();

			// Send ListUID command
			String cmd = this.buildcommand(EmailClientCommand.ListUID, ((mindex>0)?mindex.ToString():String.Empty));
			bool SLResponse = this.commandResponseTypeIsSL(EmailClientCommand.ListUID, ((mindex>0)?mindex.ToString():String.Empty));
			error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand(EmailClientCommand.ListUID, cmd, response, SLResponse );

			//Parse the result
			error = (error)?true:!this.parseListUID(list, response, mindex);

			return !error;
		}

		protected virtual bool quit () {
			bool error = false;

			// Send Quit and disconnect
			String cmd = this.buildcommand(EmailClientCommand.Logout);
			error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand(EmailClientCommand.Logout, cmd );

			this.disconnect();

			return !error;
		}

		public String errormessage {
			get {
				return client.errormessage;
			}
		}

		public String lastMessage {
			get {
				return this.lastResponse;
			}
		}
		public String Password {
			get {
				return this.password;
			}
		}
		public String UserName {
			get {
				return this.username;
			}
		}
	}
	internal enum EmailClientCommand {
		Delete,
		Header,
		Login,
		Logout,
		ListSize,
		ListUID,
		Message,
		Other,
		Status
	}
}
