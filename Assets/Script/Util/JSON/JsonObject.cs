using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Json;

namespace Json {
    public class JsonObject : JsonStructure, IDictionary<string, JsonValue> {
        private readonly Dictionary<string, JsonValue> values;

        public JsonObject() {
            values = new Map<string, JsonValue>();
        }

        public override Json.JsonValueType ValueType {
            get { return JsonValueType.Object; }
        }

		public JsonValue Get(string key) {
			JsonValue result = null;
			values.TryGetValue(key, out result);
			if (result is JsonValueNull) return null;
			return result;
		}

		public JsonObject GetJsonObject(string key) {
			return Get(key) as JsonObject;
		}

		public JsonArray GetJsonArray(string key) {
			return Get(key) as JsonArray;
		}

		public JsonString GetJsonString(string key) {
			return Get(key) as JsonString;
		}

		public string GetString(string key, string defaultValue) {
			JsonString jsonString = GetJsonString(key);
			if (jsonString == null) return defaultValue;
			return jsonString.GetString();
		}

		public JsonNumber GetJsonNumber(string key) {
			return Get(key) as JsonNumber;
		}

		public double GetDouble(string key) {
			JsonNumber jsonNumber = GetJsonNumber(key);
			if (jsonNumber == null) throw new Exception("JsonNumber not found: " + key);
			return jsonNumber.DoubleValue;
		}

		public double GetDouble(string key, double defaultValue) {
			JsonNumber jsonNumber = GetJsonNumber(key);
			if (jsonNumber == null) return defaultValue;
			return jsonNumber.DoubleValue;
		}

		public int GetInt(string key) {
			JsonNumber jsonNumber = GetJsonNumber(key);
			if (jsonNumber == null) throw new Exception("JsonNumber not found: " + key);
			return jsonNumber.IntValue;
		}

		public int GetInt(string key, int defaultValue) {
			JsonNumber jsonNumber = GetJsonNumber(key);
			if (jsonNumber == null) return defaultValue;
			return jsonNumber.IntValue;
		}

		public bool GetBool(string key) {
			JsonValue value = Get(key);
			if (value == null) throw new Exception("Json bool not found: " + key);

			switch (value.ValueType) {
				case JsonValueType.True:
					return true;
				case JsonValueType.False:
					return false;
				default:
					throw new Exception("Expected bool JSON value, found " + value.ValueType);
			}
		}

		public bool GetBool(string key, bool defaultValue) {
			JsonValue value = Get(key);
			if (value == null) return defaultValue;

			switch (value.ValueType) {
				case JsonValueType.True:
					return true;
				case JsonValueType.False:
					return false;
				default:
					throw new Exception("Expected bool JSON value, found " + value.ValueType);
			}
		}

		public JsonObject Add(string key, int item) {
			Add(key, JsonNumber.GetJsonNumber(item));
			return this;
		}

		public JsonObject Add(string key, long item) {
			Add(key, JsonNumber.GetJsonNumber(item));
			return this;
		}

		public JsonObject Add(string key, double item) {
			Add(key, JsonNumber.GetJsonNumber(item));
			return this;
		}

		public JsonObject Add(string key, float item) {
			Add(key, JsonNumber.GetJsonNumber(item));
			return this;
		}

		public JsonObject Add(string key, bool item) {
			Add(key, item ? JsonValue.True : JsonValue.False);
			return this;
		}

		public JsonObject Add(string key, string item) {
			Add(key, new JsonString(item));
			return this;
		}

		public JsonObject Add(string key, JsonStructure item) {
			Add(key, (JsonValue)item);
			return this;
		}


		public JsonObject AddNull(string key) {
			Add(key, JsonValue.Null);
			return this;
		}


        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append('{');
            foreach (KeyValuePair<string, JsonValue> pair in values) {
                result
                    .Append('"')
                    .Append(pair.Key)
                    .Append("\":")
                    .Append(pair.Value)
                    .Append(',');
            }
			if (values.Count > 0) result.Remove(result.Length - 1, 1);
            result.Append('}');

            return result.ToString();
        }

		public override bool Equals(object obj) {
			JsonObject other = obj as JsonObject;
			if (other == null) return false;
			return (this.GetHashCode() == other.GetHashCode());
		}

		public override int GetHashCode() {
			return values.GetHashCode();
		}


        public IEnumerator<KeyValuePair<string, JsonValue>> GetEnumerator() {
            return values.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, JsonValue> item) {
            values[item.Key] = item.Value;
        }

        public void Clear() {
            values.Clear();
        }

        public bool Contains(KeyValuePair<string, JsonValue> item) {
			if (!values.ContainsKey(item.Key)) return false;
			JsonValue value = values[item.Key];
			return (value == item.Value);
        }
		
        public void CopyTo(KeyValuePair<string, JsonValue>[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, JsonValue> item) {
            return values.Remove(item.Key);
        }

        public int Count {
            get { return values.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public bool ContainsKey(string key) {
            return values.ContainsKey(key);
        }

        public void Add(string key, JsonValue value) {
			values[key] = value;
        }

        public bool Remove(string key) {
            return values.Remove(key);
        }

        public bool TryGetValue(string key, out JsonValue value) {
            return values.TryGetValue(key, out value);
        }

        public JsonValue this[string key] {
            get { return values[key]; }
            set { values[key] = value; }
        }

        public ICollection<string> Keys {
            get { return values.Keys; }
        }

        public ICollection<JsonValue> Values {
            get { return values.Values; }
        }
    }
}
