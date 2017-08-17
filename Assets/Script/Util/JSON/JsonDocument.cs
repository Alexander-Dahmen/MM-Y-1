using System.Collections;
using System.Collections.Generic;

namespace Json {
	public class JsonDocument {
		private readonly string content;
		private int pos;
		private char chr;

		public JsonDocument(string content) {
			this.content = content;
			this.pos = -1;
			this.Next();
		}

		public void Next() {
			if (pos >= content.Length) throw new System.Exception("End of JSON document");

			pos++;
			chr = (pos >= content.Length) ? '\0' : content[pos];
		}

		public char Chr { get { return chr; } }

		public bool HasNext { get { return (pos < content.Length); } }

		public bool Read(char c) {
			SkipWhitespace();
			bool match = (chr == c);
			if (match) Next();
			return match;
		}

		public bool Read(string s) {
			int orig = pos - 1;
			char[] toRead = s.ToCharArray();

			SkipWhitespace();

			for (int i = 0; i < toRead.Length; i++) {
				if (chr == toRead[i]) {
					Next();
				} else {
					pos = orig;
					Next();
					return false;
				}
			}

			return true;
		}

		public void SkipWhitespace() {
			while (
				(chr == ' ') ||
				(chr == '\n') ||
				(chr == '\t') ||
				(chr == '\r')
			) Next();
		}

		public string Substring(int pos, int len) {
			return content.Substring(pos, len);
		}

		// ----- Static Util -----

		public static bool IsAlpha(char c) {
			return (
				((c >= 'a') && (c <= 'z')) ||
				((c >= 'A') && (c <= 'Z'))
			);
		}

		public static bool IsAlpha(string s) {
			foreach (char c in s) {
				if (!IsAlpha(c)) return false;
			}
			return true;
		}

		public static bool IsNumber(char c) {
			return ((c >= '0' && c <= '9') || (c == '.'));
		}

		public static bool IsNumber(string s) {
			foreach (char c in s) {
				if (!IsNumber(c)) return false;
			}
			return true;
		}
	}
}
