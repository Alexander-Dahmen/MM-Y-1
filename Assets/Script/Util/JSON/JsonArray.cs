using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace Json {
	public class JsonArray : JsonStructure, IList<JsonValue> {
		private readonly List<JsonValue> values;

		public JsonArray() {
			values = new List<JsonValue>();
		}
        
		public JsonValue Get(int index) {
			JsonValue result = values[index];
			if (result is JsonValueNull) return null;
			return result;
		}

        public JsonObject GetJsonObject(int index) {
			return Get(index) as JsonObject;
	    }

	    public JsonArray GetJsonArray(int index) {
			return Get(index) as JsonArray;
	    }

	    public JsonNumber GetJsonNumber(int index) {
			return Get(index) as JsonNumber;
	    }

		public JsonString GetJsonString(int index) {
			return Get(index) as JsonString;
		}

		public string GetString(int index) {
			JsonString jsonString = GetJsonString(index);
			if (jsonString == null) return null;
			return jsonString.GetString();
		}

		public JsonArray Add(int item) {
			Add(JsonNumber.GetJsonNumber(item));
			return this;
		}

		public JsonArray Add(long item) {
			Add(JsonNumber.GetJsonNumber(item));
			return this;
		}

		public JsonArray Add(double item) {
			Add(JsonNumber.GetJsonNumber(item));
			return this;
		}

		public JsonArray Add(float item) {
			Add(JsonNumber.GetJsonNumber(item));
			return this;
		}

		public JsonArray Add(bool item) {
			Add(item ? JsonValue.True : JsonValue.False);
			return this;
		}

		public JsonArray Add(string item) {
			Add(new JsonString(item));
			return this;
		}

		public JsonArray Add(JsonStructure item) {
			Add((JsonValue)item);
			return this;
		}


		public JsonArray AddNull() {
			Add(JsonValue.Null);
			return this;
		}


		public override JsonValueType ValueType { get { return JsonValueType.Array; } }

		public override string ToString() {
			StringBuilder result = new StringBuilder();
			result.Append('[');
			foreach (JsonValue value in values) result.Append(value).Append(',');
			if (values.Count > 0) result.Remove(result.Length - 1, 1);
			result.Append(']');
			return result.ToString();
		}

		public override bool Equals(object obj) {
			JsonArray other = obj as JsonArray;
			if (other == null) return false;
			return this.GetHashCode() == other.GetHashCode();
		}

		public override int GetHashCode() {
			return values.GetHashCode();
		}


        public int IndexOf(JsonValue value) {
            return values.IndexOf(value);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return values.GetEnumerator();
        }

	    public IEnumerator<JsonValue> GetEnumerator() {
	        return values.GetEnumerator();
	    }

	    public void Add(JsonValue item) {
	        values.Add(item);
	    }

	    public void Clear() {
	        values.Clear();
	    }

	    public bool Contains(JsonValue item) {
	        return values.Contains(item);
	    }

	    public void CopyTo(JsonValue[] array, int arrayIndex) {
	        values.CopyTo(array, arrayIndex);
	    }

	    public bool Remove(JsonValue item) {
	        return values.Remove(item);
	    }

	    public int Count {
	        get { return values.Count; }
	    }

	    public bool IsReadOnly {
	        get { return false; }
	    }

	    public void Insert(int index, JsonValue item) {
	        values.Insert(index, item);
	    }

	    public void RemoveAt(int index) {
	        values.RemoveAt(index);
	    }

	    public JsonValue this[int index] {
	        get { return values[index]; }
	        set { values[index] = value; }
	    }
	}
}

