using System.Globalization;

namespace Json {
    public abstract class JsonNumber : JsonValue {

        public static JsonNumber GetJsonNumber(int value) {
            return new JsonIntNumber(value);
        }

        public static JsonNumber GetJsonNumber(long value) {
            return new JsonLongNumber(value);
        }

        public static JsonNumber GetJsonNumber(double value) {
            return new JsonDoubleNumber(value);
        }

        public static JsonNumber GetJsonNumber(float value) {
            return new JsonFloatNumber(value);
        }

        public abstract bool IsIntegral { get; }

        public abstract int IntValue { get; }

        public abstract long LongValue { get; }

        public abstract double DoubleValue { get; }

		public abstract float FloatValue { get; }

        public override JsonValueType ValueType {
            get { return JsonValueType.Number; }
        }

		public override bool Equals(object obj) {
			JsonNumber other = obj as JsonNumber;
			if (other == null) return false;
			return other.GetHashCode() == this.GetHashCode();
		}

		public abstract override int GetHashCode();

		public abstract override string ToString();



        private class JsonLongNumber : JsonNumber {
            private readonly long value;

            public JsonLongNumber(long value) {
                this.value = value;
            }

			public override string ToString() {
				return value.ToString();
			}

			public override int GetHashCode() {
				return value.GetHashCode();
			}

			public override bool IsIntegral { get { return true; } }
            public override int IntValue { get { return (int)value; } }
            public override long LongValue { get { return (long)value; } }
            public override double DoubleValue { get { return (double)value; } }
			public override float FloatValue { get { return (float)value; } }
        }

        private class JsonIntNumber : JsonNumber {
            private readonly int value;

            public JsonIntNumber(int value) {
                this.value = value;
            }

            public override string ToString() {
                return value.ToString();
            }

			public override int GetHashCode() {
				return value.GetHashCode();
			}

			public override bool IsIntegral { get { return true; } }
			public override int IntValue { get { return (int)value; } }
            public override long LongValue { get { return (long)value; } }
            public override double DoubleValue { get { return (double)value; } }
			public override float FloatValue { get { return (float)value; } }
		}

		private class JsonDoubleNumber : JsonNumber {
			private readonly double value;

			public JsonDoubleNumber(double value) {
				this.value = value;
			}

			public override string ToString() {
				return value.ToString();
			}

			public override int GetHashCode() {
				return value.GetHashCode();
			}

			public override bool IsIntegral { get { return false; } }
			public override int IntValue { get { return (int)value; } }
			public override long LongValue { get { return (long)value; } }
			public override double DoubleValue { get { return (double)value; } }
			public override float FloatValue { get { return (float)value; } }
		}

		private class JsonFloatNumber : JsonNumber {
			private readonly float value;

			public JsonFloatNumber(float value) {
				this.value = value;
			}

			public override string ToString() {
				return value.ToString();
			}

			public override int GetHashCode() {
				return value.GetHashCode();
			}

			public override bool IsIntegral { get { return false; } }
			public override int IntValue { get { return (int)value; } }
			public override long LongValue { get { return (long)value; } }
			public override double DoubleValue { get { return (double)value; } }
			public override float FloatValue { get { return (float)value; } }
		}
    }
}