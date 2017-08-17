using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json {
    public class JsonString : JsonValue {

        private readonly string value;

        public JsonString(string value) {
            this.value = value;
        }

		public string GetString() {
			return value;
		}

        public override JsonValueType ValueType {
            get { return JsonValueType.String; }
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append('"');

            foreach (char c in value) {
                if (c >= 0x20 && c <= 0x10ff && c != 0x22 && c != 0x5c) {
					result.Append(c);
                } else {
                    switch (c) {
                        case '"':
                        case '\\':
                            result.Append('\\').Append(c);
                            break;
                        case '\b':
                            result.Append('\\').Append('b');
                            break;
                        case '\f':
                            result.Append('\\').Append('f');
                            break;
                        case '\n':
                            result.Append('\\').Append('n');
                            break;
                        case '\r':
                            result.Append('\\').Append('r');
                            break;
                        case '\t':
                            result.Append('\\').Append('t');
                            break;
                        default:
                            string hex = "000" + ((int) c).ToString("x4");
                            result.Append("\\u").Append(hex.Substring(hex.Length - 4));
                            break;
                    }
                }
            }

            result.Append('"');
            return result.ToString();
        }

		public override bool Equals(object obj) {
			JsonString other = obj as JsonString;
			if (other == null) return false;
			return (this.GetHashCode() == other.GetHashCode());
		}

		public override int GetHashCode() {
			return value.GetHashCode();
		}
    }
}
