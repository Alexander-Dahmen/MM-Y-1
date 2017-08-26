using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing a value and invoking an event if that value is changed
/// </summary>
/// <typeparam name="T">Type of the contained value</typeparam>
public class ObservableValue<T> {

    public delegate void ObservableChangeDelegate(T before, T after, object sender);

    public event ObservableChangeDelegate OnValueChange;

    [SerializeField]
    private T value;

    public ObservableValue() {
        this.value = default(T);
    }

    public ObservableValue(T value) {
        this.value = value;
    }

    public T Value {
        get { return value; }
        set {
            T before = this.value;
            T after = value;
            if (OnValueChange != null)
                OnValueChange(before, after, this);
            this.value = after;
        }
    }

    public T Get() {
        return value;
    }

    public void Set(T value) {
        this.Value = value;
    }

    public override bool Equals(object obj) {
        object other = (obj is ObservableValue<T>) ?
            (obj as ObservableValue<T>).Value :
            obj;
        
        if (value == null)
            return (other == null);
        else
            return value.Equals(other);
    }

    public override int GetHashCode() {
        return (value == null) ? 0 : value.GetHashCode();
    }

    public override string ToString() {
        return (value == null) ? "null" : value.ToString();
    }
}

[System.Serializable]
public class ObservableInt : ObservableValue<int> {
    public ObservableInt() : base() { }
    public ObservableInt(int value) : base(value) { }
}

[System.Serializable]
public class ObservableFloat : ObservableValue<float> {
    public ObservableFloat() : base() { }
    public ObservableFloat(float value) : base(value) { }
}

[System.Serializable]
public class ObservableString : ObservableValue<string> {
    public ObservableString() : base() { }
    public ObservableString(string value) : base(value) { }
}

[System.Serializable]
public class ObservableVector2 : ObservableValue<Vector2> {
    public ObservableVector2() : base() { }
    public ObservableVector2(Vector2 value) : base(value) { }
}

[System.Serializable]
public class ObservableVector3 : ObservableValue<Vector3> {
    public ObservableVector3() : base() { }
    public ObservableVector3(Vector3 value) : base(value) { }
}

[System.Serializable]
public class ObservableTransform : ObservableValue<Transform> {
    public ObservableTransform() : base() { }
    public ObservableTransform(Transform value) : base(value) { }
}

[System.Serializable]
public class ObservableGameObject : ObservableValue<GameObject> {
    public ObservableGameObject() : base() { }
    public ObservableGameObject(GameObject value) : base(value) { }
}

[System.Serializable]
public class ObservableUnityObject : ObservableValue<Object> {
    public ObservableUnityObject() : base() { }
    public ObservableUnityObject(Object value) : base(value) { }
}
