using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace POP3Pipe
{

	public class SharpMimeTools {
#if LOG
		private static log4net.ILog log  = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
#endif
		private static String[] _date_formats = new String[] {
																@"dddd, d MMM yyyy H:m:s zzz", @"ddd, d MMM yyyy H:m:s zzz", @"d MMM yyyy H:m:s zzz",
																@"dddd, d MMM yy H:m:s zzz", @"ddd, d MMM yy H:m:s zzz", @"d MMM yy H:m:s zzz",
																@"dddd, d MMM yyyy H:m zzz", @"ddd, d MMM yyyy H:m zzz", @"d MMM yyyy H:m zzz",
																@"dddd, d MMM yy H:m zzz", @"ddd, d MMM yy H:m zzz", @"d MMM yy H:m zzz"
			};
		
		internal static String GetFileName ( String name ) {
			if ( name==null || name.Length==0 )
				return name;
			name = name.Replace("\t", "");
			try {
				name = Path.GetFileName(name);
			} catch ( ArgumentException ) {
				// Remove invalid chars
				foreach ( char ichar in Path.InvalidPathChars ) {
					name = name.Replace ( ichar.ToString(), String.Empty );
				}
				name = Path.GetFileName(name);
			}
			try {
				FileInfo fi = new FileInfo(name);
				if ( fi!=null )
					fi = null;
			} catch ( ArgumentException ) {
				name = null;
#if LOG
				if ( log.IsErrorEnabled ) {
					log.Error(System.String.Concat("Filename [", name, "] is not allowed by the filesystem"));
				}
#endif
			}
			return name;
		}

		/// <summary>
		///     Parses a <see cref="System.Text.Encoding" /> from a charset name
		/// </summary>
		/// <param name="charset">charset to parse</param>
		/// <returns>A <see cref="System.Text.Encoding" /> that represents the given <c>charset</c></returns>
		public static Encoding parseCharSet ( String charset ) {
			try {
				return Encoding.GetEncoding (charset);
			} catch ( System.Exception ) {
				return null;
			}
		}

		internal static Enum ParseEnum ( Type t, Object s, Enum defaultvalue ) {
			if ( s==null )
				return defaultvalue;
			Enum value = defaultvalue;
			if ( Enum.IsDefined(t, s) ) {
				value = (Enum)Enum.Parse(t, s.ToString());
			}
			return value;
		}

		/// <summary>
		/// Parse a rfc 2822 address specification. rfc 2822 section 3.4
		/// </summary>
		/// <param name="from">field body to parse</param>
		/// <returns>A <see cref="System.Collections.IEnumerable" /> collection of <see cref="anmar.SharpMimeTools.SharpMimeAddress" /></returns>
		public static IEnumerable parseFrom ( String from ) {
			return SharpMimeTools.SharpMimeAddressCollection.Parse (from);
		}

		/// <summary>
		///     Parse a rfc 2822 name-address specification. rfc 2822 section 3.4
		/// </summary>
		/// <param name="from">address</param>
		/// <param name="part">1 is display-name; 2 is addr-spec</param>
		/// <returns>the requested <see cref="System.String" /></returns>
		public static String parseFrom ( String from, int part ) {
			int pos;
			if ( from==null || from.Length<1) {
				return String.Empty;
			}
			switch (part) {
				case 1:
					pos = from.LastIndexOf('<');
					pos = (pos<0)?from.Length:pos;
					from = from.Substring (0, pos).Trim();
					from = anmar.SharpMimeTools.SharpMimeTools.parserfc2047Header ( from );
					return from;
				case 2:
					pos = from.LastIndexOf('<')+1;
					return from.Substring(pos, from.Length-pos).Trim(new char[]{'<','>',' '});
			}
			return from;
		}

		/// <summary>
		///     Parse a rfc 2822 date and time specification. rfc 2822 section 3.3
		/// </summary>
		/// <param name="date">rfc 2822 date-time</param>
		/// <returns>A <see cref="System.DateTime" /> from the parsed header body</returns>
		public static DateTime parseDate ( String date ) {
			if ( date==null || date.Equals(String.Empty) )
				return DateTime.MinValue;
			DateTime msgDateTime;
			date = SharpMimeTools.SharpMimeTools.uncommentString (date);
			msgDateTime = new DateTime (0);
			try {
				// TODO: Complete the list
				date = date.Replace("UT", "+0000");
				date = date.Replace("GMT", "+0000");
				date = date.Replace("EDT", "-0400");
				date = date.Replace("EST", "-0500");
				date = date.Replace("CDT", "-0500");
				date = date.Replace("MDT", "-0600");
				date = date.Replace("MST", "-0600");
				date = date.Replace("EST", "-0700");
				date = date.Replace("PDT", "-0700");
				date = date.Replace("PST", "-0800");

				date = date.Replace("AM", String.Empty);
				date = date.Replace("PM", String.Empty);
				int rpos = date.LastIndexOfAny(new Char[]{' ', '\t'});
				if (rpos>0 && rpos != date.Length - 6)
					date = date.Substring(0, rpos + 1) + "-0000";
				date = date.Insert(date.Length-2, ":");
				msgDateTime = DateTime.ParseExact(date, 
					_date_formats,
					CultureInfo.CreateSpecificCulture("en-us"),
					DateTimeStyles.AllowInnerWhite);
#if LOG
			} catch ( System.Exception e ) {
				if ( log.IsErrorEnabled )
					log.Error(System.String.Concat("Error parsing date: [", date, "]"), e);
#else
			} catch ( Exception ) {
#endif
				msgDateTime = new DateTime (0);
			}
			return msgDateTime;
		}

		/// <summary>
		///     Parse a rfc 2822 header field with parameters
		/// </summary>
		/// <param name="field">field name</param>
		/// <param name="fieldbody">field body to parse</param>
		/// <returns>A <see cref="System.Collections.Specialized.StringDictionary" /> from the parsed field body</returns>
		public static StringDictionary parseHeaderFieldBody ( String field, String fieldbody ) {
			if ( fieldbody==null )
				return null;
			// FIXME: rewrite parseHeaderFieldBody to being regexp based.
			fieldbody = SharpMimeTools.uncommentString (fieldbody);
			StringDictionary fieldbodycol = new StringDictionary ();
			String[] words = fieldbody.Split(new Char[]{';'});
			if ( words.Length>0 ) {
				fieldbodycol.Add (field.ToLower(), words[0].ToLower());
				for (int i=1; i<words.Length; i++ ) {
					String[] param = words[i].Trim(new Char[]{' ', '\t'}).Split(new Char[]{'='}, 2);
					if ( param.Length==2 ) {
						param[0] = param[0].Trim(new Char[]{' ', '\t'});
						param[1] = param[1].Trim(new Char[]{' ', '\t'});
						if ( param[1].StartsWith("\"") && !param[1].EndsWith("\"")) {
							do {
								param[1] += ";" + words[++i];
							} while  ( !words[i].EndsWith("\"") && i<words.Length);
						}
						fieldbodycol.Add ( param[0], SharpMimeTools.parserfc2047Header (param[1].TrimEnd(';').Trim('\"')) );
					}
				}
			}
			return fieldbodycol;
		}

		/// <summary>
		///     Parse and decode rfc 2047 header body
		/// </summary>
		/// <param name="header">header body to parse</param>
		/// <returns>parsed <see cref="System.String" /></returns>
		public static String parserfc2047Header ( String header ) {
			header = header.Replace ("\"", String.Empty);
			header = SharpMimeTools.rfc2047decode(header);
			return header;
		}

		/// <summary>
		///     Decode rfc 2047 definition of quoted-printable
		/// </summary>
		/// <param name="charset">charset to use when decoding</param>
		/// <param name="orig"><c>string</c> to decode</param>
		/// <returns>the decoded <see cref="System.String" /></returns>
		public static String QuotedPrintable2Unicode ( String charset, String orig ) {
			Encoding enc = SharpMimeTools.parseCharSet (charset);
			if ( enc==null || orig==null )
				return orig;
			SharpMimeTools.QuotedPrintable2Unicode ( enc, ref orig );
			return orig;
		}

		/// <summary>
		///     Decode rfc 2047 definition of quoted-printable
		/// </summary>
		/// <param name="enc"><see cref="System.Text.Encoding" /> to use</param>
		/// <param name="orig"><c>string</c> to decode</param>
		public static void QuotedPrintable2Unicode ( Encoding enc, ref String orig ) {
			if ( enc==null || orig==null )
				return;

			StringBuilder decoded = new StringBuilder(orig);
			int i = 0;
			String hexNumber;
			Byte[] ch = new Byte[1];
			while ( i < decoded.Length - 2 ) {
				System.String decodedItem = null;
				if ( decoded[i] == '=' ) {
					hexNumber = decoded.ToString(i+1, 2);
					if ( hexNumber.Equals(ABNF.CRLF) ) {
						decodedItem = String.Empty;
					// Do not replace 3D(=)
					} else if ( hexNumber.ToUpper().Equals("3D") ) {
						decodedItem = null;
					} else {
						try {
							//TODO: this ugly workaround should disapear
							ch[0] = Convert.ToByte(hexNumber, 16);
							decodedItem = enc.GetString ( ch );
						} catch ( Exception ) {}
					}
					if ( decodedItem!=null )
						decoded.Replace( "=" + hexNumber, decodedItem );
				}
				if ( decodedItem!=null )
					i+=decodedItem.Length;
				else
					i++;
			}
			decoded.Replace("=3D", "=");
			decoded.Replace("=3d", "=");
			orig = decoded.ToString();
			return;
		}

		/// <summary>
		///     rfc 2047 header body decoding
		/// </summary>
		/// <param name="word"><c>string</c> to decode</param>
		/// <returns>the decoded <see cref="System.String" /></returns>
		public static String rfc2047decode ( String word ) {
			String[] words;
			String[] wordetails;

			Regex rfc2047format = new Regex (@"(=\?[\-a-zA-Z0-9]+\?[qQbB]\?[a-zA-Z0-9=_\-\.$%&/\'\\!:;{}\+\*\|@#~`^]+\?=)\s*", RegexOptions.ECMAScript);
			// No rfc2047 format
			if ( !rfc2047format.IsMatch (word) ){
#if LOG
				if ( log.IsDebugEnabled )
					log.Debug ("Not a RFC 2047 string: " + word);
#endif
				return word;
			}
#if LOG
			if ( log.IsDebugEnabled )
				log.Debug ("Decoding 2047 string: " + word);
#endif
			words = rfc2047format.Split ( word );
			word = String.Empty;
			rfc2047format = new Regex (@"=\?([\-a-zA-Z0-9]+)\?([qQbB])\?([a-zA-Z0-9=_\-\.$%&/\'\\!:;{}\+\*\|@#~`^]+)\?=", RegexOptions.ECMAScript);
			for ( int i=0; i<words.GetLength (0); i++ ) {
				if ( !rfc2047format.IsMatch (words[i]) ){
					word += words[i];
					continue;
				}
				wordetails = rfc2047format.Split ( words[i] );

				switch (wordetails[2]) {
					case "q":
					case "Q":
						word += anmar.SharpMimeTools.SharpMimeTools.QuotedPrintable2Unicode ( wordetails[1], wordetails[3] ).Replace ('_', ' ');;
						break;
					case "b":
					case "B":
						try {
							Encoding enc = Encoding.GetEncoding (wordetails[1]);
							Byte[] ch = Convert.FromBase64String(wordetails[3]);
							word += enc.GetString (ch);
						} catch ( Exception ) {
						}
						break;
				}
			}
#if LOG
			if ( log.IsDebugEnabled )
				log.Debug ("Decoded 2047 string: " + word);
#endif
			return word;
		}

		/// <summary>
		///     Remove rfc 2822 comments
		/// </summary>
		/// <param name="fieldValue"><c>string</c> to uncomment</param>
		/// <returns></returns>
		// TODO: refactorize this
		public static String uncommentString ( String fieldValue ) {
			if ( fieldValue==null || fieldValue.Equals(String.Empty) )
				return fieldValue;
			if ( fieldValue.IndexOf('(')==-1 )
				return fieldValue.Trim();
			const int a = 0;
			const int b = 1;
			const int c = 2;

			StringBuilder buf = new StringBuilder();
			int leftSqureCount = 0;
			bool isQuotedPair = false;
			int state = a;

			for (int i = 0; i < fieldValue.Length; i ++) {
				switch (state) {
					case a:
						if (fieldValue[i] == '"') {
							state = c;
							Debug.Assert(!isQuotedPair, "quoted-pair");
						}
						else if (fieldValue[i] == '(') {
							state = b;
							leftSqureCount ++;
							Debug.Assert(!isQuotedPair, "quoted-pair");
						}
						break;
					case b:
						if (!isQuotedPair) {
							if (fieldValue[i] == '(')
								leftSqureCount ++;
							else if (fieldValue[i] == ')') {
								leftSqureCount --;
								if (leftSqureCount == 0) {
									buf.Append(' ');
									state = a;
									continue;
								}
							}
						}
						break;
					case c:
						if (!isQuotedPair) {
							if (fieldValue[i] == '"')
								state = a;
						}
						break;
					default:
						break;
				}

				if (state != a) { //quoted-pair
					if (isQuotedPair)
						isQuotedPair = false;
					else
						isQuotedPair = fieldValue[i] == '\\';
				}
				if (state != b)
					buf.Append(fieldValue[i]);
			}
      
			return buf.ToString().Trim();

		}

		/// <summary>
		///     Encodes a Message-ID or Content-ID following RFC 2392 rules. 
		/// </summary>
		/// <param name="input"><see cref="System.String" /> with the Message-ID or Content-ID.</param>
		/// <returns><see cref="System.String" /> with the value encoded as RFC 2392 dictates.</returns> 
		public static String Rfc2392Url ( String input) {
			if ( input==null || input.Length<4 )
				return input;
			if ( input.Length>2 && input[0]=='<' && input[input.Length-1]=='>' )
				input = input.Substring(1, input.Length-2);
			if ( input.IndexOf('/')!=-1 ) {
				input = input.Replace("/", "%2f");
			}
			return input;
		}

		/// <summary>
		///     Decodes the provided uuencoded string. 
		/// </summary>
		/// <param name="input"><see cref="System.String" /> with the uuendoced content.</param>
		/// <returns>A <see cref="System.Byte" /> <see cref="System.Array" /> with the uudecoded content. Or the <b>null</b> reference if content can't be decoded.</returns>
		/// <remarks>The input string must contain the <b>begin</b> and <b>end</b> delimiters.</remarks>
		public static Byte[] UuDecode ( String input ) {
			if ( input==null || input.Length==0 )
				return null;
			StringReader reader = new StringReader(input);
			MemoryStream stream = null;
			Byte[] output = null;
			for ( String line=reader.ReadLine(); line!=null; line=reader.ReadLine() ) {
				// Found the start point of uuencoded content
				if ( line.Length>10 && line[0]=='b' && line[1]=='e' && line[2]=='g' && line[3]=='i' && line[4]=='n' && line[5]==' ' && line[9]==' ' ) {
					stream = new MemoryStream();
					continue;
				}
				// Not within uuencoded content
				if ( stream==null )
					continue;
				// Content finished
				if ( line.Length==3 && line=="end" ) {
					stream.Flush();
					output = stream.ToArray();
					stream.Close();
					stream = null;
					break;
				}
				// Decode and write uuencoded line
				UuDecodeLine(line, stream);
			}
			reader.Close();
			reader = null;
			if ( stream!=null ) {
				stream.Close();
				stream = null;
			}
			return output;
		}

		/// <summary>
		///     Decodes the provided uuencoded line. 
		/// </summary>
		/// <param name="s"><see cref="System.String" /> with the uuendoced line.</param>
		/// <param name="stream"><see cref="System.IO.Stream" /> where decoded <see cref="System.Byte" /> should be written.</param>
		/// <returns><b>true</b> if content has been decoded and <b>false</b> otherwise.</returns>
		public static bool UuDecodeLine ( String s, Stream stream ) {
			if ( s==null || s.Length==0 || stream==null || !stream.CanWrite )
				return false;
			Byte[] input = Encoding.ASCII.GetBytes(s);
			int length = input.Length;
			int length_output = 0;
			// Full line, so it has length info in the first byte
			if ( (length%4)==1 ) {
				length_output = ((input[0]-0x20) & 0x3f);
			}
			// Wrong input
			if ( length==0 || length_output<=0 )
				return false;
			// Decode each four characters of input to three octets of output
			for ( int i=1, pos=0; i<length; i+=4 ) {
				Byte b = (byte)((input[i+1]-0x20) & 0x3f);
				Byte c = (byte)((input[i+2]-0x20) & 0x3f);
				stream.WriteByte((byte)(((input[i]-0x20) & 0x3f)<<2|b>>4));
				pos++;
				if ( pos<length_output ) {
					stream.WriteByte((byte)(b<<4|c>>2));
					pos++;
				}
				if ( pos<length_output ) {
					stream.WriteByte((byte)(c<<6|((input[i+3]-0x20) & 0x3f)));
					pos++;
				}
			}
			return true;
		}
	}
}
