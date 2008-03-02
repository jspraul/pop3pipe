using System;
using System.Collections;
using System.Text;
using System.Collections.Specialized;
using System.IO;

namespace anmar.SharpMimeTools
{
	/// <summary>
	///     rfc 2822 header of a rfc 2045 entity
	/// </summary>
	public class SharpMimeHeader : IEnumerable {
#if LOG
		private static log4net.ILog log  = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
#endif
		private static Encoding default_encoding = Encoding.ASCII;
		private SharpMimeMessageStream message;
		private HybridDictionary headers;
		private String _cached_headers = null;
		private long startpoint;
		private long endpoint;
		private long startbody;

		private struct HeaderInfo {
			public StringDictionary contenttype;
			public StringDictionary contentdisposition;
			public StringDictionary contentlocation;
			public MimeTopLevelMediaType TopLevelMediaType;
			public Encoding enc;
			public String subtype;

			public HeaderInfo ( HybridDictionary headers ) {
				this.TopLevelMediaType = new MimeTopLevelMediaType();
				this.enc = null;
				try {
					this.contenttype = SharpMimeTools.parseHeaderFieldBody ( "Content-Type", headers["Content-Type"].ToString() );
					this.TopLevelMediaType = (MimeTopLevelMediaType)Enum.Parse(TopLevelMediaType.GetType(), this.contenttype["Content-Type"].Split('/')[0], true);
					this.subtype = this.contenttype["Content-Type"].Split('/')[1];
					this.enc = SharpMimeTools.parseCharSet ( this.contenttype["charset"] );
				} catch (Exception) {
					this.enc = SharpMimeHeader.default_encoding;
					this.contenttype = SharpMimeTools.parseHeaderFieldBody ( "Content-Type", System.String.Concat("text/plain; charset=", this.enc.BodyName) );
					this.TopLevelMediaType = MimeTopLevelMediaType.text;
					this.subtype = "plain";
				}
				if ( this.enc==null ) {
					this.enc = SharpMimeHeader.default_encoding;
				}
				// TODO: rework this
				try {
					this.contentdisposition = SharpMimeTools.parseHeaderFieldBody ( "Content-Disposition", headers["Content-Disposition"].ToString() );
				} catch ( Exception ) {
					this.contentdisposition = new StringDictionary();
				}
				try {
					this.contentlocation = SharpMimeTools.parseHeaderFieldBody ( "Content-Location", headers["Content-Location"].ToString() );
				} catch ( System.Exception ) {
					this.contentlocation = new StringDictionary();
				}
			}
		}

		private HeaderInfo mt;

		internal SharpMimeHeader(SharpMimeMessageStream message ) : this ( message, 0 ){}

		internal SharpMimeHeader(SharpMimeMessageStream message, long startpoint) {
			this.startpoint = startpoint;
			this.message = message;
			if ( this.startpoint==0 ) {
				System.String line = this.message.ReadLine();
				// Perhaps there is part of the POP3 response
				if ( line!=null && line.Length>3 && line[0]=='+' && line[1]=='O' && line[2]=='K' ) {
#if LOG
					if ( log.IsDebugEnabled ) log.Debug ("+OK present at top of the message");
#endif
					this.startpoint = this.message.Position;
				} else this.message.ReadLine_Undo(line);
			}
			this.headers = new HybridDictionary(2, true);
			this.parse();
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="anmar.SharpMimeTools.SharpMimeHeader"/> class from a <see cref="System.IO.Stream"/>
		/// </summary>
		/// <param name="message"><see cref="System.IO.Stream"/> to read headers from</param>
		public SharpMimeHeader(Stream message ) : this( new SharpMimeMessageStream (message), 0 ) {
		}

		public SharpMimeHeader(Byte[] message ) : this( new SharpMimeMessageStream (message), 0 ) {
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="anmar.SharpMimeTools.SharpMimeHeader"/> class from a <see cref="System.IO.Stream"/> starting from the specified point
		/// </summary>
		/// <param name="message">the <see cref="System.IO.Stream" /> to read headers from</param>
		/// <param name="startpoint">initial point of the <see cref="System.IO.Stream"/> where the headers start</param>
		public SharpMimeHeader(Stream message, long startpoint ) : this( new SharpMimeMessageStream (message), startpoint ) {
		}

		/// <summary>
		///     Gets header fields
		/// </summary>
		/// <param name="name">field name</param>
		/// <remarks>Field names is case insentitive</remarks>
		public String this[ Object name ] {
			get {
				return this.getProperty( name.ToString() );
			}
		}

		public void Close(){
			this._cached_headers = this.message.ReadLines( this.startpoint, this.endpoint );
			this.message.Close();
		}

		public bool Contains ( String name ) {
			if ( this.headers==null )
				this.parse();
			return this.headers.Contains(name);
		}

		/// <summary>
		///     Returns an enumerator that can iterate through the header fields
		/// </summary>
		/// <returns>A <see cref="System.Collections.IEnumerator" /> for the header fields</returns>
		public IEnumerator GetEnumerator() {
			return headers.GetEnumerator();
		}

		/// <summary>
		///     Returns the requested header field body.
		/// </summary>
		/// <param name="name">Header field name</param>
		/// <param name="defaultvalue">Value to return when the requested field is not present</param>
		/// <param name="uncomment"><b>true</b> to uncomment using <see cref="anmar.SharpMimeTools.SharpMimeTools.uncommentString" />; <b>false</b> to return the value unchanged.</param>
		/// <param name="rfc2047decode"><b>true</b> to decode <see cref="anmar.SharpMimeTools.SharpMimeTools.rfc2047decode" />; <b>false</b> to return the value unchanged.</param>
		/// <returns>Header field body</returns>
		public String GetHeaderField ( String name, String defaultvalue, bool uncomment, bool rfc2047decode ) {
			System.String tmp = this.getProperty(name);
			if ( tmp==null )
				tmp = defaultvalue;
			else {
				if ( uncomment )
					tmp = SharpMimeTools.uncommentString(tmp);
				if ( rfc2047decode )
					tmp = SharpMimeTools.rfc2047decode(tmp);
			}
			return tmp;
		}

		private String getProperty (  String name ) {
			String Value=null;
			name = name.ToLower();
			this.parse();
			if ( this.headers!=null && this.headers.Count > 0 && name!=null && name.Length>0 && this.headers.Contains(name) )
				Value = this.headers[name].ToString();
			return Value;
		}

		private bool parse () {
			bool error = false;
			if ( this.headers.Count>0 ) {
				return !error;
			}
			String line = String.Empty;
			this.message.SeekPoint ( this.startpoint );
			this.message.Encoding = SharpMimeHeader.default_encoding;
			for ( line=this.message.ReadUnfoldedLine(); line!=null ; line=this.message.ReadUnfoldedLine() ) {
				if ( line.Length == 0 ) {
					this.endpoint = this.message.Position_preRead;
					this.startbody = this.message.Position;
					this.message.ReadLine_Undo(line);
					break;
				} else {
					String [] headerline = line.Split ( new Char[] {':'}, 2);
					if ( headerline.Length == 2 ) {
						headerline[1] = headerline[1].TrimStart(new Char[] {' '});
						if ( this.headers.Contains ( headerline[0]) ) {
							this.headers[headerline[0]] = String.Concat(this.headers[headerline[0]], "\r\n", headerline[1]);
						} else {
							this.headers.Add (headerline[0].ToLower(), headerline[1]);
						}
					}
				}
			}
			this.mt = new HeaderInfo ( this.headers );
			return !error;
		}

		/// <summary>
		///     Gets the point where the headers end
		/// </summary>
		/// <value>Point where the headers end</value>
		public long BodyPosition {
			get {
				return this.startbody;
			}
		}

		/// <summary>
		///     Gets CC header field
		/// </summary>
		/// <value>CC header body</value>
		public String Cc {
			get { return this.GetHeaderField("Cc", String.Empty, true, false); }
		}

		/// <summary>
		///     Gets the number of header fields found
		/// </summary>
		public int Count {
			get {
				return this.headers.Count;
			}
		}

		/// <summary>
		///     Gets Content-Disposition header field
		/// </summary>
		/// <value>Content-Disposition header body</value>
		public String ContentDisposition {
			get { return this.GetHeaderField("Content-Disposition", System.String.Empty, true, false); }
		}

		/// <summary>
		///     Gets the elements found in the Content-Disposition header body
		/// </summary>
		/// <value><see cref="System.Collections.Specialized.StringDictionary"/> with the elements found in the header body</value>
		public StringDictionary ContentDispositionParameters {
			get {
				return this.mt.contentdisposition;
			}
		}

		/// <summary>
		///     Gets Content-ID header field
		/// </summary>
		/// <value>Content-ID header body</value>
		public String ContentID {
			get { return this.GetHeaderField("Content-ID", String.Empty, true, false); }
		}

		/// <summary>
		///     Gets Content-Location header field
		/// </summary>
		/// <value>Content-Location header body</value>
		public String ContentLocation {
			get { return this.GetHeaderField("Content-Location", String.Empty, true, false); }
		}

		/// <summary>
		///     Gets the elements found in the Content-Location header body
		/// </summary>
		/// <value><see cref="System.Collections.Specialized.StringDictionary"/> with the elements found in the header body</value>
		public StringDictionary ContentLocationParameters {
			get {
				return this.mt.contentlocation;
			}
		}

		/// <summary>
		///     Gets Content-Transfer-Encoding header field
		/// </summary>
		/// <value>Content-Transfer-Encoding header body</value>
		public String ContentTransferEncoding {
			get {
				String tmp = this.GetHeaderField("Content-Transfer-Encoding", null, false, false);
				if ( tmp!=null ) {
					tmp = tmp.ToLower();
				}
				return tmp;
			}
		}

		/// <summary>
		///     Gets Content-Type header field
		/// </summary>
		/// <value>Content-Type header body</value>
		public String ContentType {
			get { return this.GetHeaderField("Content-Type", String.Concat("text/plain; charset=", this.mt.enc.BodyName), false, false); }
		}

		/// <summary>
		///     Gets the elements found in the Content-Type header body
		/// </summary>
		/// <value><see cref="System.Collections.Specialized.StringDictionary"/> with the elements found in the header body</value>
		public StringDictionary ContentTypeParameters {
			get {
				return this.mt.contenttype;
			}
		}

		/// <summary>
		///     Gets Date header field
		/// </summary>
		/// <value>Date header body</value>
		public String Date {
			get { return this.GetHeaderField("Date", String.Empty, true, false); }
		}

		/// <summary>
		///     Gets <see cref="System.Text.Encoding"/> found on the headers and applies to the body
		/// </summary>
		/// <value><see cref="System.Text.Encoding"/> for the body</value>
		public Encoding Encoding {
			get {
				this.parse();
				return this.mt.enc;
			}
		}

		/// <summary>
		///     Gets or sets the default <see cref="System.Text.Encoding" /> used when it isn't defined otherwise.
		/// </summary>
		/// <value>The <see cref="System.Text.Encoding" /> used when it isn't defined otherwise</value>
		/// <remarks>The default value is <see cref="System.Text.ASCIIEncoding" /> as defined in RFC 2045 section 5.2.<br />
		/// If you change this value you'll be violating this rfc section.</remarks>
		public static Encoding EncodingDefault {
			get {return default_encoding; }
			set {
				if ( value!=null && !value.BodyName.Equals(String.Empty) )
					default_encoding=value;
				else
					default_encoding=Encoding.ASCII;
			}
		}

		/// <summary>
		///     Gets From header field
		/// </summary>
		/// <value>From header body</value>
		public String From {
			get { return this.GetHeaderField("From", String.Empty, true, false); }
		}

		/// <summary>
		///     Gets Raw headers
		/// </summary>
		/// <value>From header body</value>
		public String RawHeaders {
			get {
				if ( this._cached_headers!=null )
					return this._cached_headers;
				else
					return this.message.ReadLines( this.startpoint, this.endpoint );
			}
		}

		/// <summary>
		///     Gets Message-ID header field
		/// </summary>
		/// <value>Message-ID header body</value>
		public String MessageID {
			get { return this.GetHeaderField("Message-ID", String.Empty, true, false); }
		}

		/// <summary>
		///     Gets reply address as defined by <c>rfc 2822</c>
		/// </summary>
		/// <value>Reply address</value>
		public String Reply {
			get {
				if ( !this.ReplyTo.Equals(String.Empty) )
					return this.ReplyTo;
				else
					return this.From;
			}
		}

		/// <summary>
		///     Gets Reply-To header field
		/// </summary>
		/// <value>Reply-To header body</value>
		public String ReplyTo {
			get { return this.GetHeaderField("Reply-To", String.Empty, true, false); }
		}

		/// <summary>
		///     Gets Return-Path header field
		/// </summary>
		/// <value>Return-Path header body</value>
		public String ReturnPath {
			get { return this.GetHeaderField("Return-Path", String.Empty, true, false); }
		}

		/// <summary>
		///     Gets Sender header field
		/// </summary>
		/// <value>Sender header body</value>
		public String Sender {
			get { return this.GetHeaderField("Sender", String.Empty, true, false); }
		}
		/// <summary>
		///     Gets Subject header field
		/// </summary>
		/// <value>Subject header body</value>
		public String Subject {
			get { return this.GetHeaderField("Subject", String.Empty, false, false); }
		}

		/// <summary>
		///     Gets SubType from Content-Type header field
		/// </summary>
		/// <value>SubType from Content-Type header field</value>
		public String SubType {
			get {
				this.parse();
				return this.mt.subtype;
			}
		}

		/// <summary>
		///     Gets To header field
		/// </summary>
		/// <value>To header body</value>
		public String To {
			get { return this.GetHeaderField("To", String.Empty, true, false); }
		}

		/// <summary>
		///     Gets top-level media type from Content-Type header field
		/// </summary>
		/// <value>Top-level media type from Content-Type header field</value>
		public MimeTopLevelMediaType TopLevelMediaType {
			get {
				this.parse();
				return this.mt.TopLevelMediaType;
			}
		}

		/// <summary>
		///     Gets Version header field
		/// </summary>
		/// <value>Version header body</value>
		public String Version {
			get { return this.GetHeaderField("Version", "1.0", true, false); }
		}
	}

	/// <summary>
	///     RFC 2046 Initial top-level media types
	/// </summary>
	[Flags]
	public enum MimeTopLevelMediaType {
		/// <summary>
		/// RFC 2046 section 4.1
		/// </summary>
		text = 1,
		/// <summary>
		/// RFC 2046 section 4.2
		/// </summary>
		image = 2,
		/// <summary>
		/// RFC 2046 section 4.3
		/// </summary>
		audio = 4,
		/// <summary>
		/// RFC 2046 section 4.4
		/// </summary>
		video = 8,
		/// <summary>
		/// RFC 2046 section 4.5
		/// </summary>
		application = 16,
		/// <summary>
		/// RFC 2046 section 5.1
		/// </summary>
		multipart = 32,
		/// <summary>
		/// RFC 2046 section 5.2
		/// </summary>
		message = 64
	}
}
