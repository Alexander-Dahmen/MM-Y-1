using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class containing a left-right pair of values
/// </summary>
/// <typeparam name="T">Type that is present in this pair</typeparam>
[System.Serializable]
public class Pair<T> {
    [SerializeField] private T left;
    [SerializeField] private T right;

    public Pair() {
        left = default(T);
        right = default(T);
    }

    public Pair(T value) {
        left = right = value;
    }

    public Pair(T left, T right) {
        this.left = left;
        this.right = right;
    }

    public T Left {
        get { return left; }
        set { left = value; }
    }

    public T Right {
        get { return right; }
        set { right = value; }
    }

    public void Swap() {
        T swap = left;
        right = left;
        left = swap;
    }

    public virtual bool Same() {
        return (left.Equals(right));
    }

    public override bool Equals(object obj) {
        Pair<T> other = obj as Pair<T>;
        if (other == null)
            return false;
        else
            return (other.GetHashCode() == this.GetHashCode());
    }

    public override int GetHashCode() {
        return 31 * left.GetHashCode() + right.GetHashCode();
    }

    public override string ToString() {
        return string.Format(
            "Pair<{0}> Left = {1} Right = {2}",
            typeof(T).Name,
            left.ToString(),
            right.ToString());
    }
}

[System.Serializable]
public class IntPair : Pair<int> {
    public IntPair() : base() { }
    public IntPair(int value) : base(value) { }
    public IntPair(int left, int right) : base(left, right) { }
    public override bool Same() { return (Left == Right); }
}

[System.Serializable]
public class FloatPair : Pair<float> {
    public FloatPair() : base() { }
    public FloatPair(float value) : base(value) { }
    public FloatPair(float left, float right) : base(left, right) { }
    public override bool Same() { return (Left == Right); }
}

[System.Serializable]
public class StringPair : Pair<string> {
    public StringPair() : base() { }
    public StringPair(string value) : base(value) { }
    public StringPair(string left, string right) : base(left, right) { }
    public override bool Same() { return (Left == Right); }
}

[System.Serializable]
public class Vector2Pair : Pair<Vector2> {
    public Vector2Pair() : base() { }
    public Vector2Pair(Vector2 value) : base(value) { }
    public Vector2Pair(Vector2 left, Vector2 right) : base(left, right) { }
    public override bool Same() { return (Left == Right); }
}

[System.Serializable]
public class Vector3Pair : Pair<Vector3> {
    public Vector3Pair() : base() { }
    public Vector3Pair(Vector3 value) : base(value) { }
    public Vector3Pair(Vector3 left, Vector3 right) : base(left, right) { }
    public override bool Same() { return (Left == Right); }
}

[System.Serializable]
public class TransformPair : Pair<Transform> {
    public TransformPair() : base() { }
    public TransformPair(Transform value) : base(value) { }
    public TransformPair(Transform left, Transform right) : base(left, right) { }
    public override bool Same() { return (Left == Right); }
}

[System.Serializable]
public class UnityObjectPair : Pair<Object> {
    public UnityObjectPair() : base() { }
    public UnityObjectPair(Object value) : base(value) { }
    public UnityObjectPair(Object left, Object right) : base(left, right) { }
    public override bool Same() { return (Left == Right); }
}

[System.Serializable]
public class GameObjectPair : Pair<GameObject> {
    public GameObjectPair() : base() { }
    public GameObjectPair(GameObject value) : base(value) { }
    public GameObjectPair(GameObject left, GameObject right) : base(left, right) { }
    public override bool Same() { return (Left == Right); }
}
