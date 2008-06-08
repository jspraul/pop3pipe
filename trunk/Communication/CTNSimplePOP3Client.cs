using System;
using System.IO;

namespace POP3Pipe
{
	internal class CTNSimplePOP3Client : SimpleEmailClient {

		public CTNSimplePOP3Client(String host, Int32 port, String user, String pass ) : base(host, port, user, pass) {
			this.commandEnd = "\r\n";
			this.responseEnd = "\r\n.\r\n";
			this.responseEndSL = "\r\n";
			this.responseEndOnEnd = true;
		}

		public CTNSimplePOP3Client( String host, Int32 port, String user, String pass, long timeout ) : base(host, port, user, pass, timeout) {
			this.commandEnd = "\r\n";
			this.responseEnd = "\r\n.\r\n";
			this.responseEndSL = "\r\n";
			this.responseEndOnEnd = true;
		}

		protected override System.String buildcommand ( EmailClientCommand cmd, params Object[] args ) {
			String command = String.Empty;
			switch ( cmd ) {
				case EmailClientCommand.Delete:
					if ( args.Length==1 )
						command = String.Format("DELE {0}", args[0]);
					break;
				case EmailClientCommand.Header:
					if ( args.Length==2 )
						command = String.Format("TOP {0} {1}", args[0], args[1]);
					break;
				case EmailClientCommand.ListSize:
					command = "LIST";
					break;
				case EmailClientCommand.ListUID:
					if ( args.Length==1 ) {
						if ( args[0]==null || args[0].Equals(String.Empty) )
							command = "UIDL";
						else
							command = String.Concat("UIDL ", args[0]);
					}
					break;
				case EmailClientCommand.Logout:
					command = "QUIT";
					break;
				case EmailClientCommand.Message:
					if ( args.Length==1 )
						command = String.Format("RETR {0}", args[0]);
					break;
				case EmailClientCommand.Status:
					command = "STAT";
					break;
				
			}
			return command;
		}

		protected override bool commandResponseTypeIsSL ( EmailClientCommand cmd, params Object[] args ) {
			bool responseSL = true;
			switch ( cmd ) {
				case EmailClientCommand.Delete:
					break;
				case EmailClientCommand.Header:
					responseSL = false;
					break;
				case EmailClientCommand.ListSize:
					responseSL = false;
					break;
				case EmailClientCommand.ListUID:
					if ( args.Length==1 )
						if ( args[0].ToString().Equals(String.Empty) )
							responseSL = false;
					break;
				case EmailClientCommand.Logout:
					break;
				case EmailClientCommand.Message:
					responseSL = false;
					break;
				case EmailClientCommand.Status:
					break;
				
			}
			return responseSL;
		}

		protected override bool evaluateresponse ( String response ) {
			return response.ToLower().Trim().StartsWith("+ok");
		}

		protected override MemoryStream getStreamDataPortion ( MemoryStream data ) {
			return data;
		}

		protected override bool login ( String user, String pass ) {
			bool error = false;

			// Send USER and PASS and see what happends
			// Send USER Command
			error = !this.sendCommand( EmailClientCommand.Login, String.Concat( "USER ", user ) );
			// If USER is accepted send PASS
			error = (error)?true:!this.sendCommand( EmailClientCommand.Login, String.Concat( "PASS ", pass ) );

			return !error;
		}

        protected override bool parseListSize ( Int32[] list, MemoryStream response ) {
			bool error = false;
			String tmp;
			//Parse the result
			if (!error) {
				StreamReader resp = new StreamReader(response);
				resp.ReadLine();

				for ( tmp=resp.ReadLine() ; tmp != null && tmp != "." ; tmp=resp.ReadLine() ) {
					try {
						String[] values = tmp.Split( null, 2 );
						list[Int32.Parse(values[0])-1] = Int32.Parse(values[1]);
					} catch ( Exception e ) {
						if ( log.IsErrorEnabled ) log.Error ( "Error while parsing LIST response", e );
					}
				}
			}
			return !error;
		}

		protected override bool parseListUID ( String[] list, MemoryStream response, int mindex ) {
			bool error = false;
			String tmp;
			//Parse the result
			if ( !error ) {
				StreamReader resp = new StreamReader(response);
				if ( mindex == 0 ) {
					resp.ReadLine();
				}
				for ( tmp=resp.ReadLine() ; tmp != null && tmp != "." ; tmp=resp.ReadLine() ) {
					try {
						if ( mindex>0 ) {
							tmp = tmp.Remove(0,4);
						}
						String[] values = tmp.Split( null, 2 );
						list[Int32.Parse(values[0])-1] = values[1];
					} catch ( Exception e ) {
						if ( log.IsErrorEnabled ) log.Error ( "Error while parsing UIDL response", e );
					}
				}
			}
			return !error;
		}

		protected override bool parseStatus ( ref int num, ref int numbytes, MemoryStream response ) {
			bool error = false;
			//Parse the result
			if (!error) {
				String resp = new StreamReader(response).ReadToEnd();
				String[] values = resp.Split( null, 3 );
				try {
					num = Int32.Parse(values[1]);
					numbytes = Int32.Parse(values[2]);
				} catch (Exception e) {
					num = 0;
					numbytes = 0;
					if ( log.IsErrorEnabled ) log.Error ( "Error while parsing STAT response", e );
				}
			}
			return !error;
		}
	}
}
