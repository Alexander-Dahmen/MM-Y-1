using System;
using System.Collections;
using System.Collections.Generic;

namespace Json {
	public class JsonReader {
		private readonly JsonDocument document;

		public JsonReader(string content) {
			document = new JsonDocument(content);
		}
	
		public JsonObject ReadObject() {
			return Read() as JsonObject;
		}

		public JsonArray ReadArray() {
			return Read() as JsonArray;
		}

		private JsonStructure Read() {
			JsonStructure result;

			if (document.Read('{')) {
				result = ParseObject();
				return result;
			}

			if (document.Read('[')) {
				result = ParseArray();
				return result;
			}

			throw new JsonReaderException("Expected JSON Object or JSON Array");
		}

		private JsonObject ParseObject() {
			JsonObject result = new JsonObject();
			if (document.Read('}')) return result;

			for (;;) {
				string key = ParseString();
				if (!document.Read(':')) throw new JsonReaderException("ParseObject: Expected ':'");
				JsonValue value = ParseValue();
				result.Add(key, value);

				if (document.Read('}')) return result;
				if (!document.Read(',')) throw new JsonReaderException("ParseObject: Expected ','");
			}
		}

		private JsonArray ParseArray() {
			JsonArray result = new JsonArray();
			if (document.Read(']')) return result;

			for (;;) {
				JsonValue value = ParseValue();
				result.Add(value);

				if (document.Read(']')) return result;
				if (!document.Read(',')) throw new JsonReaderException("ParseArray: Expected ','");
			}
		}

		private JsonValue ParseValue() {
			if (document.Read("true")) return JsonValue.True;
			if (document.Read("false")) return JsonValue.False;
			if (document.Read("null")) return JsonValue.Null;
			if (document.Read('[')) return ParseArray();
			if (document.Read('{')) return ParseObject();

			string str = ParseString();
			if (str != null) return new JsonString(str);

			JsonNumber number = ParseNumber();
			if (number != null) return number;

			throw new JsonReaderException("ParseValue: Invalid value");
		}

		private string ParseString() {
			if (!document.Read('"')) return null;
			System.Text.StringBuilder result = new System.Text.StringBuilder();

			while (document.Chr != '"') {
				char c = document.Chr;
				
				if (c == '\\') {
					document.Next();
					switch (document.Chr) {
						case '\\':
						case '"':
							c = document.Chr;
							break;
						case 'n':
							c = '\n';
							break;
						case 't':
							c = '\t';
							break;
						case 'r':
							c = '\r';
							break;
					}
				}
				
				result.Append(c);
				document.Next();
			}
			document.Next();

			return result.ToString();
		}

		private JsonNumber ParseNumber() {
			document.SkipWhitespace();
			if (!JsonDocument.IsNumber(document.Chr)) return null;
			System.Text.StringBuilder content = new System.Text.StringBuilder();

			while (JsonDocument.IsNumber(document.Chr)) {
				content.Append(document.Chr);
				document.Next();
			}

			string number = content.ToString();
			if (number.Contains(".")) return JsonNumber.GetJsonNumber(double.Parse(number));
			else return JsonNumber.GetJsonNumber(int.Parse(number));
		}
	}

	public class JsonReaderException : Exception {
		public JsonReaderException(string msg) : base(msg) { }
	}
}
