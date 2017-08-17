namespace Json {
	public abstract class JsonValue {

		/// <summary>
		/// JSON Null value
		/// </summary>
		public static readonly JsonValue Null = new JsonValueNull();

		/// <summary>
		/// JSON True vale
		/// </summary>
		public static readonly JsonValue True = new JsonValueTrue();

		/// <summary>
		/// JSON False value
		/// </summary>
		public static readonly JsonValue False = new JsonValueFalse();

		/// <summary>
		/// Gets the type of this JSON value.
		/// </summary>
		/// <value>Json.ValueType</value>
		public abstract JsonValueType ValueType { get; }

		/// <summary>
		/// Returns JSON text for this JSON value.
		/// </summary>
		/// <returns>JSON text as String</returns>
		public abstract override string ToString();

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Json.JsonValue"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Json.JsonValue"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="Json.JsonValue"/>;
		/// otherwise, <c>false</c>.</returns>
		public abstract override bool Equals(object obj);

		/// <summary>
		/// Functions as Hash-function for a certain Type.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public abstract override int GetHashCode();


	    internal class JsonValueNull : JsonValue {
			public override JsonValueType ValueType { get { return JsonValueType.Null; } }
			public override string ToString() {
				return "null";
			}
			public override bool Equals(object obj) {
				return (obj is JsonValueNull);
			}
			public override int GetHashCode() {
				return 0;
			}
		}

		internal class JsonValueTrue : JsonValue {
			public override JsonValueType ValueType { get { return JsonValueType.True; } }
			public override string ToString() {
				return "true";
			}
			public override bool Equals(object obj) {
				return (obj is JsonValueTrue);
			}
			public override int GetHashCode() {
				return true.GetHashCode();
			}
		}

		internal class JsonValueFalse : JsonValue {
			public override JsonValueType ValueType { get { return JsonValueType.False; } }
			public override string ToString() {
				return "false";
			}
			public override bool Equals(object obj) {
				return (obj is JsonValueFalse);
			}
			public override int GetHashCode() {
				return false.GetHashCode();
			}
		}
	}
}
