using System;
using System.Text.RegularExpressions;

namespace POP3Pipe
{
	public sealed class BasicSanitizer {
		private static Object[] comment = new Object[] {
			// Comment all <script></script> occurrences
			new Regex(@"(<script([^<]*(?:(?!</)<){0,1})*</script[^>]*>)+", RegexOptions.IgnoreCase|RegexOptions.Singleline),
			// Comment everything before <body>
			new Regex(@"(.*<body[^>]*>)", RegexOptions.IgnoreCase|RegexOptions.Singleline),
			// Comment everything after </body>
			new Regex(@"(</body[^>]*>.*)", RegexOptions.IgnoreCase|RegexOptions.Singleline),
			// Comment all <applet></applet> occurrences
			new Regex(@"(<applet([^<]*(?:(?!</)<){0,1})*</applet[^>]*>)+", RegexOptions.IgnoreCase|RegexOptions.Singleline),
			// Comment all <embed></embed> occurrences
			new Regex(@"(<embed([^<]*(?:(?!</)<){0,1})*</embed[^>]*>)+", RegexOptions.IgnoreCase|RegexOptions.Singleline),
			// Comment all <object></object> occurrences
			new Regex(@"(<object([^<]*(?:(?!</)<){0,1})*</object[^>]*>)+", RegexOptions.IgnoreCase|RegexOptions.Singleline),
			// Comment all <layer></layer> occurrences
			new Regex(@"(<layer([^<]*(?:(?!</)<){0,1})*</layer[^>]*>)+", RegexOptions.IgnoreCase|RegexOptions.Singleline),
			// Comment all <ilayer></ilayer> occurrences
			new Regex(@"(<ilayer([^<]*(?:(?!</)<){0,1})*</ilayer[^>]*>)+", RegexOptions.IgnoreCase|RegexOptions.Singleline),
			// Comment all <iframe></iframe> occurrences
			new Regex(@"(<iframe([^<]*(?:(?!</)<){0,1})*</iframe[^>]*>)+", RegexOptions.IgnoreCase|RegexOptions.Singleline)
		};
		private static Regex events = new Regex(@"(?:\b(on)(Abort|Blur|Change|Click|DblClick|DblClick|Error|Focus|KeyDown|KeyPress|KeyPress|Load|MouseDown|MouseMove|MouseMove|MouseOver|MouseUp|Move|Reset|Resize|Select|Submit|Unload)(\s*=))", RegexOptions.IgnoreCase|RegexOptions.Singleline);

		public static System.String SanitizeHTML ( String htmlstring, SanitizerMode mode ) {
			if ( (mode & SanitizerMode.CommentBlocks) == SanitizerMode.CommentBlocks ) {
				foreach (Regex item in BasicSanitizer.comment ) {
					htmlstring = item.Replace(htmlstring, "<!-- Commented by SharpWebMail \r\n$1\r\n -->");
				}
			}
			if ( (mode & SanitizerMode.RemoveEvents) == SanitizerMode.RemoveEvents ) {
				htmlstring = BasicSanitizer.events.Replace(htmlstring, "$1_$2$3");
			}
			return htmlstring;
		}
	}

	[Flags]
	public enum SanitizerMode {

		CommentBlocks,

		RemoveEvents
	}
}
