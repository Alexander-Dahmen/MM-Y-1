using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List implementation that is observable
/// </summary>
/// <typeparam name="T">Type of list items</typeparam>
public class ObservableList<T> : IList<T> {

    public delegate void ListUpdateDelegate(T item, object sender);
    public delegate void ListClearDelegate(object sender);

    public event ListUpdateDelegate OnItemAdded;
    public event ListUpdateDelegate OnItemRemoved;
    public event ListClearDelegate OnListCleared;

    private readonly List<T> list;

    public ObservableList() {
        list = new List<T>();
    }

    public ObservableList(int capacity) {
        list = new List<T>(capacity);
    }

    public ObservableList(IEnumerable<T> collection) {
        list = new List<T>(collection);
    }

    #region IList<T> implementation

    public int IndexOf(T value) {
        return list.IndexOf(value);
    }

    public void Insert(int index, T value) {
        list.Insert(index, value);
    }

    public void RemoveAt(int index) {
        list.RemoveAt(index);
    }

    public T this[int index] {
        get { return list[index]; }
        set { list[index] = value; }
    }

    #endregion
    #region IEnumerable<T> implementation

    public IEnumerator<T> GetEnumerator() {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    #endregion
    #region ICollection<T> implementation

    public void Add(T item) {
        if (OnItemAdded != null)
            OnItemAdded(item, this);
        list.Add(item);
    }

    public void Clear() {
        if (OnListCleared != null)
            OnListCleared(this);
    }

    public bool Contains(T item) {
        return list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex) {
        list.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item) {
        if (OnItemRemoved != null)
            OnItemRemoved(item, this);
        return list.Remove(item);
    }

    public int Count { get { return list.Count; } }

    public bool IsReadOnly { get { return false; } }

    #endregion
}