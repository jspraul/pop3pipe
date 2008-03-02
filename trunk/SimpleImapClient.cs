using System;
using System.Data;
using System.IO;
using System.Text;

namespace POP3Pipe
{
	internal class SimpleIMAPClient : SimpleEmailClient {

		protected String folder;

		protected String server_delimiter;

		protected String tag;
		private bool selected = false;
		private Random taggen;

		public SimpleIMAPClient( String host, Int32 port, String user, String pass ) : base(host, port, user, pass) {
			this.folder = "INBOX";
			this.server_delimiter = "/";
			this.taggen = new Random();
			this.commandEnd = "\r\n";
			this.responseEndSL = "\r\n";
			this.responseEndOnEnd = false;
		}

		public SimpleIMAPClient( String host, Int32 port, String user, String pass, long timeout ) : base(host, port, user, pass, timeout) {
			this.folder = "INBOX";
			this.server_delimiter = "/";
			this.taggen = new Random();
			this.commandEnd = "\r\n";
			this.responseEndSL = "\r\n";
			this.responseEndOnEnd = false;
		}

		protected override String buildcommand ( anmar.SharpWebMail.EmailClientCommand cmd, params Object[] args ) {
			String command = String.Empty;
			this.randomTag();
			switch ( cmd ) {
				case EmailClientCommand.Delete:
					if ( args.Length==1 )
						command = String.Format("{0} STORE {1}:{1} +FLAGS.SILENT (\\Deleted)", this.tag, args[0]);
					break;
				case EmailClientCommand.Header:
					if ( args.Length==2 )
						command = String.Format("{0} FETCH {1}:{1} (RFC822.HEADER)", this.tag, args[0], args[1]);
					break;
				case EmailClientCommand.ListSize:
					command = System.String.Format("{0} FETCH 1:* (RFC822.SIZE)", this.tag);
					break;
				case EmailClientCommand.ListUID:
					if ( args.Length==1 ) {
						if ( args[0].ToString().Equals(String.Empty) )
							command = String.Format("{0} FETCH 1:* (UID)", this.tag);
						else
							command = String.Format("{0} FETCH {1}:{1} (UID)", this.tag, args[0]);
					}
					break;
				case EmailClientCommand.Login:
					if ( args.Length==2 )
						command = String.Format("{0} LOGIN {1} {2}", this.tag, args[0], args[1]);
					break;
				case EmailClientCommand.Logout:
					command = String.Concat(this.tag, " LOGOUT");
					break;
				case EmailClientCommand.Message:
					if ( args.Length==1 )
						command = String.Format("{0} FETCH {1}:{1} (BODY.PEEK[])", this.tag, args[0]);
					break;
				case EmailClientCommand.Status:
					command = String.Format("{0} EXAMINE {1}", this.tag, this.folder);
					break;
				
			}
			return command;
		}

		protected override bool commandResponseTypeIsSL (EmailClientCommand cmd, params Object[] args ) {
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
					responseSL = false;
					break;
				case EmailClientCommand.Login:
					break;
				case EmailClientCommand.Logout:
					responseSL = false;
					break;
				case EmailClientCommand.Message:
					responseSL = false;
					break;
				case EmailClientCommand.Status:
					responseSL = false;
					break;
			}
			return responseSL;
		}

		protected override bool delete ( int mindex ) {
			if ( !this.select() ) {
				return false;
			}
			return base.delete(mindex);
		}

		protected override bool deletemessages (DataView result ) {
			bool error = !base.deletemessages( result );
			if ( !error ) {
				this.randomTag();
				MemoryStream response = new MemoryStream();
				String cmd = String.Format("{0} EXPUNGE ", this.tag);
				error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand(EmailClientCommand.Other, cmd, response, false );
			}
			return !error;
		}

		protected override bool evaluateresponse (String response ) {
			bool error = false;
			if ( response.IndexOf(' ')>0 ) {
				error = !response.Substring(response.IndexOf(' ')+1).ToLower().Trim().StartsWith("ok");
			} else {
				error = true;
			}
			return !error;
		}

		protected override MemoryStream getStreamDataPortion (MemoryStream data ) {
			StreamReader reader = new StreamReader(data, ASCIIEncoding.ASCII);
			String line = reader.ReadLine();
			if ( line.StartsWith("*") ) {
				int size = this.parseInteger((line.Substring(line.LastIndexOf('{'))).Trim(new Char[]{'{','}'}));
				if ( size>0 ) {
					int offset = ASCIIEncoding.ASCII.GetByteCount(line + "\r\n");
					reader.DiscardBufferedData();
					reader=null;
					data.Seek(offset, SeekOrigin.Begin);
					data.SetLength(offset + size);
				}
			}
			return data;
		}

		protected override bool header (int mindex, ulong nlines, MemoryStream response ) {
			if ( !this.select() ) {
				return false;
			}
			return base.header(mindex, nlines, response);
		}

		protected override bool list (Int32[] list ) {
			if ( !this.select() ) {
				return false;
			}
			return base.list(list);
		}

		private int parseInteger (String value ) {
			try {
				return Int32.Parse(value);
			} catch (Exception ) {
				return -1;
			}
		}

		protected override void parseLastResponse (MemoryStream response, bool SLReponse ) {
			if ( !SLReponse ) {
				response.Seek (0, SeekOrigin.Begin);
				StreamReader resp = new StreamReader(response);
				String line = System.String.Empty;
				for (String tmp=resp.ReadLine(); tmp!=null ; line=tmp, tmp=resp.ReadLine() ) {}
				this.lastResponse=line;
				response.Seek (0, SeekOrigin.Begin);
			}
		}

		protected override bool parseListSize (Int32[] list, MemoryStream response ) {
			Object[] tmplist = new Object[list.Length];
			bool error = !this.parseUntaggedResponse(response, tmplist, "RFC822.SIZE",EmailClientCommand.ListSize);
			if ( !error )
				tmplist.CopyTo(list, 0);
			return !error;
		}

		protected override bool parseListUID (String[] list, MemoryStream response, int mindex ) {
			return this.parseUntaggedResponse(response, list, "UID", EmailClientCommand.ListUID);
		}

		protected override bool parseStatus ( ref int num, ref int numbytes, MemoryStream response ) {
			bool error = false;
			Object[] tmplist = new Object[1];
			error = !this.parseUntaggedResponse(response, tmplist, " EXISTS", EmailClientCommand.Status);
			if ( !error ) {
				numbytes = 0;
				num = (int)tmplist[0];
			}
			return !error;
		}

		private bool parseUntaggedResponse (MemoryStream response, Object[] list, String token, EmailClientCommand cmd ) {
			bool error = false;
			int msgnum=-1;
			Object value=-1;
			StreamReader resp = new StreamReader(response);
			for ( System.String line=resp.ReadLine(); line!=null; line=resp.ReadLine(), msgnum=-1, value=-1 ) {
				if ( line.StartsWith("*") && line.IndexOf(token)>0 ) {
					String[] values = line.Split( new char[]{' ', '(', ')'} );
					for ( int i=0; i<values.Length; i++ ) {
						if ( values[i].Equals("*") ) {
							msgnum = this.parseInteger(values[++i]);
						} else if ( values[i].Equals(token) ) {
							if  ( cmd.Equals(anmar.SharpWebMail.EmailClientCommand.ListSize) )
								value = this.parseInteger(values[++i]);
							else
								value = values[++i];
						}
					}
					if ( ( cmd.Equals(anmar.SharpWebMail.EmailClientCommand.ListUID) || cmd.Equals(anmar.SharpWebMail.EmailClientCommand.ListSize) )
						&& msgnum>0 && list.Length>=msgnum ) {
						list[msgnum-1]=value;
					} else if ( cmd.Equals(anmar.SharpWebMail.EmailClientCommand.Status) && msgnum>=0 ) {
						list[0] = msgnum;
					} else if ( log.IsErrorEnabled ) {
						log.Error ( "Error while parsing response line:" + line);
						error = true;
					}
				}
			}
			return !error;
		}

		private void randomTag () {
			this.tag = String.Format ("swm{0}", (int)(this.taggen.NextDouble()*10000));
			this.responseEnd = this.tag + " ";
		}

		protected override bool retrieve ( int mindex, MemoryStream response ) {
			if ( !this.select() ) {
				return false;
			}
			return base.retrieve(mindex, response);
		}

		private bool select () {
			bool error = false;
			if ( !this.selected ) {
				this.randomTag();
				MemoryStream response = new MemoryStream();
				String cmd = String.Format("{0} SELECT {1}", this.tag, this.folder);
				error = ( cmd.Equals(String.Empty) )?true:!this.sendCommand(EmailClientCommand.Other, cmd, response, false );
				if ( !error )
					this.selected = true;
			}
			return !error;
		}

		protected override bool uidl (String[] list, int mindex ) {
			if ( !this.select() ) {
				return false;
			}
			return base.uidl(list, mindex);
		}

		protected override bool quit () {
			this.selected = false;
			return base.quit();
		}
	}
}
