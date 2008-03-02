using System;
using System.Net.Sockets;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Text;

namespace POP3Pipe
{

	internal class CTNSimpleTCPClient {
		// Create a logger for use in this class
		private static log4net.ILog log  = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		protected String lastErrorMessage;
		protected TcpClient client;
		protected long timeoutResponse = 20000;

		public CTNSimpleTCPClient() {
		}
		
		public CTNSimpleTCPClient( long timeout ) {
			this.timeoutResponse = timeout;
		}

		public bool connect( String host, Int32 port ) {
			bool error = false;
			this.init();
			try {
				if ( log.IsDebugEnabled )
					log.Debug(String.Concat("Connecting to host: ", host, " port: ", port));
				client.Connect( host, port );
			} catch ( ArgumentNullException e ) {
				error = true;
				lastErrorMessage = "Please provide a valid hostname";
				if ( log.IsErrorEnabled ) log.Error ( lastErrorMessage, e );
			} catch ( ArgumentOutOfRangeException e ) {
				error = true;
				lastErrorMessage = "Please provide a valid port number";
				if ( log.IsErrorEnabled ) log.Error ( lastErrorMessage, e );
			} catch ( SocketException e ) {
				error = true;
				lastErrorMessage = "Error while trying to open socket";
				if ( log.IsErrorEnabled ) log.Error ( lastErrorMessage, e );
			}
			return !error;
		}

		public bool closeConnection () {
			bool error = false;
			try {
				client.Close();
			} catch ( SocketException e ) {
				error = true;
				lastErrorMessage = "Error while trying to close socket";
				if ( log.IsErrorEnabled ) log.Error ( lastErrorMessage, e );
			}
			return !error;
		}

		// Get NetworkStream Object from TCPClient
		protected bool getStream ( ref NetworkStream ns ) {
			bool error = false;
			try {
				ns = client.GetStream();
			} catch ( System.Exception e ) {
				error = true;
				lastErrorMessage = "Connection is not propertly established";
				if ( log.IsErrorEnabled ) log.Error ( lastErrorMessage, e );
			}
			return !error;
		}

		protected void init () {
			client = new TcpClient();
		}

		public bool readResponse ( MemoryStream response, String waitFor, bool machresponseend ) {
			bool error = false;
			NetworkStream ns = null;

			//Get NetworkStream
			error = !this.getStream( ref ns );

			//Get response from NetworkStream
			error = ( error )?error:!readBytes( ns, response, waitFor, machresponseend );

			return !error;
		}

		public bool readResponse ( ref String response, String waitFor, bool machresponseend ) {
			bool error = false;
			NetworkStream ns = null;

			//Get NetworkStream
			error = !this.getStream( ref ns );

			//Get response from NetworkStream
			error = ( error )?error:!this.readString( ns, ref response, waitFor, machresponseend );

			return !error;
		}

		protected bool readBytes ( NetworkStream ns, MemoryStream response, String waitFor, bool machresponseend ) {
			bool error = false;
			byte[] readBytes = new byte[client.ReceiveBufferSize];
			int nbytes = 0;
			String lastBoundary = System.String.Empty;
			WaitState state = new WaitState(true);
			Timer aTimer = new System.Threading.Timer(new TimerCallback(this.StopWaiting), state, Timeout.Infinite,  Timeout.Infinite);

			if ( log.IsDebugEnabled ) log.Debug ( "Reading response" );
			// We wait until data is available but only if Stream is open
			// We setup a timer that stops the loop after x seconds
			for ( aTimer.Change(this.timeoutResponse,  Timeout.Infinite); !error && ns.CanRead && ns.CanWrite && !ns.DataAvailable && state.Status; ){Thread.Sleep(50);}
			state.Status = true;

			// If I can read from NetworkStream and there is
			// some data, I get it
			for ( aTimer.Change(this.timeoutResponse,  Timeout.Infinite); !error && ns.CanRead && state.Status && (ns.DataAvailable || !(lastBoundary.Equals(waitFor)) ) ; nbytes = 0) {
				try {
					if ( ns.DataAvailable ) {
#if MONO
						// Reinitialize buffer to make mono happy
						readBytes = new byte[client.ReceiveBufferSize];
#endif
						nbytes = ns.Read( readBytes, 0, client.ReceiveBufferSize );
					} else
						Thread.Sleep(50);
				} catch ( Exception e ) {
					error = true;
					nbytes = 0;
					lastErrorMessage = "Read error";
					if ( log.IsErrorEnabled ) log.Error ( lastErrorMessage, e );
				}
				if ( !error && nbytes>0 ) {
					if ( log.IsDebugEnabled ) log.Debug ( "Read " + nbytes + " bytes" );
					response.Write( readBytes, 0, nbytes );
					// Only test waitfor secuence if there is no data for reading
					// and there are enouth data available for comparing
					if ( !ns.DataAvailable && response.Length>waitFor.Length ) {
						// The waitfor text must be the last portion of the response
						if ( machresponseend ) {
							response.Seek(response.Length - waitFor.Length, SeekOrigin.Begin);
							response.Read(readBytes,  0, waitFor.Length);
							lastBoundary = Encoding.ASCII.GetString(readBytes, 0, waitFor.Length);
						// The waitfor text must be in the begining of the last line of the response
						} else {
							response.Seek(0, SeekOrigin.Begin);
							StreamReader reader = new StreamReader(response);
							String line = System.String.Empty;
							for ( System.String tmp=reader.ReadLine(); tmp!=null ; line=tmp, tmp=reader.ReadLine() ) {}
							if ( line!=null && line.Length>=waitFor.Length )
								lastBoundary = line.Substring(0, waitFor.Length);
							reader.DiscardBufferedData();
							reader=null;
							response.Seek (0, SeekOrigin.End);
						}
					}
					// Reset timer
					aTimer.Change(this.timeoutResponse,  Timeout.Infinite);
					state.Status = true;
				}
			}
			response.Flush();
			if ( log.IsDebugEnabled ) log.Debug ( String.Concat("Reading response finished. Error: ", error) );
			// Discard response if there has been a read error.
			if ( error )
				response.SetLength(0);
			else if ( response.Length==0 )
				error = true;
			return !error;
		}

		protected bool readString ( NetworkStream ns, ref String response, String waitFor, bool machresponseend) {
			bool error = false;
			if ( log.IsDebugEnabled ) log.Debug ( "Reading response string" );
			response = String.Empty;
			MemoryStream stream = new MemoryStream();
			error = !this.readBytes(ns, stream, waitFor, machresponseend);
			if ( !error ) {
				response = Encoding.ASCII.GetString(stream.GetBuffer(), 0, (int)stream.Length );
				response = response.Trim();
				if ( log.IsDebugEnabled ) log.Debug ( "Response string read: " + response );
			}
			error = (error||response.Length==0)?true:false;
			return !error;
		}

		public bool sendCommand ( String cmd, String end ) {
			bool error = false;
			NetworkStream ns = null;

			//Get NetWork Stream
			error = !this.getStream( ref ns );

			// Send the command
			error = ( !error && cmd.Length>0 )?!this.sendString ( ns, String.Concat( cmd, end ) ):true;

			return !error;
		}

		protected bool sendString ( NetworkStream ns, String cmd ) {
			bool error = false;
			byte[] sendBytes;

			// Check string length
			if ( !(cmd.Length>0) ) {
				error = true;
				lastErrorMessage = "There should be something to send";
			} else {
				if ( log.IsDebugEnabled ) log.Debug ( "Sending string " + cmd);
				sendBytes = Encoding.ASCII.GetBytes( cmd );
				// Check previous error and if network stream is writable
				if ( ns.CanWrite ){
					try {
						ns.Write( sendBytes, 0, sendBytes.Length );
					} catch ( IOException e ) {
						error = true;
						lastErrorMessage = "Write error";
						if ( log.IsErrorEnabled ) log.Error ( lastErrorMessage, e );
					}
				}
				if ( log.IsDebugEnabled ) log.Debug ( "String sent");
			}
			return !error;
		}

		protected void StopWaiting (Object state) {
			((WaitState)state).Status = false;
			return;
		}

		public string errormessage {
			get {
				return this.lastErrorMessage;
			}
		}

		private class WaitState {
			private bool wait;
			public WaitState (bool wait) {
				this.wait = wait;
			}
			public bool Status {
				get { return this.wait;}
				set { this.wait=value;}
			}
		}
	}
}
