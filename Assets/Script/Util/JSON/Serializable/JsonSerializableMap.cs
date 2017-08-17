using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Json {
	public class JsonSerializableMap : Map<string, JsonValue>, IJsonSerializable {

		public JsonSerializableMap() : base() { }
		public JsonSerializableMap(int capacity) : base(capacity) { }
		public JsonSerializableMap(IDictionary<string, JsonValue> dictionary) : base(dictionary) { }

		public JsonObject toJsonObject() {
			JsonObject result = new JsonObject();
			foreach (string key in Keys) {
				JsonValue value = this[key];
				if (value == null) result.AddNull(key);
				else result.Add(key, value);
			}
			return result;
		}
	}
}
