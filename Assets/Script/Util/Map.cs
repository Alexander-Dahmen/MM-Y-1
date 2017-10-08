using System.Collections.Generic;
using System.Linq;

public class Map<K, V> : Dictionary<K, V> {

    public Map() : base() { }
    public Map(int capacity) : base(capacity) { }
    public Map(IEqualityComparer<K> comparer) : base(comparer) { }
    public Map(IDictionary<K, V> dictionary) : base(dictionary) { }
    public Map(int capacity, IEqualityComparer<K> comparer) : base(capacity, comparer) { }
    public Map(IDictionary<K, V> dictionary, IEqualityComparer<K> comparer) : base(dictionary, comparer) { }

    /// <summary>
    /// Get the value associated with the specified key.
    /// </summary>
    /// <param name="key">Key instance.</param>
    /// <returns>Value instance or default if no key is associated</returns>
    public V Get(K key) {
        V value = default(V);
        TryGetValue(key, out value);
        return value;
    }

    /// <summary>
    /// Put the specified value under the specified key.
    /// </summary>
    /// <param name="key">Key instance.</param>
    /// <param name="value">Value instance.</param>
    public void Put(K key, V value) {
        this[key] = value;
    }

	public override string ToString() {
		return "{" + string.Join(",", this.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
	}
}
