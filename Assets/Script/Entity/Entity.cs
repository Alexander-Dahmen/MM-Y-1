using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Entity : MonoBehaviour {

    public enum EntityDirection {
        LEFT,
        RIGHT
    }


    [SerializeField] private Transform center;
    [SerializeField] private EntityState state;

    private EntityDirection defaultDirection;
    
    private EntityDirection direction;
    

	void Awake () {
        if (center == null)
            center = transform;
        if (state == null)
            state = GetComponent<EntityState>();

        SetDirectionTransformScale();
	}
	
    public Transform Center {
        get { return center; }
        set { center = value; }
    }

    public EntityState State {
        get {
#if UNITY_EDITOR
            Debug.AssertFormat(state != null, "Requested EntityState on Entity without EntityState: {0}", name);
#endif
            return state;
        }
    }

    public EntityDirection DefaultDirection {
        get { return defaultDirection; }
        set { defaultDirection = value; }
    }

    public EntityDirection Direction {
        get { return direction; }
        set {
            direction = value;
            SetDirectionTransformScale();
        }
    }

    public bool IsFacingRight {
        get { return direction == EntityDirection.RIGHT; }
        set { Direction = (value ? EntityDirection.RIGHT : EntityDirection.LEFT); }
    }
    
    public bool IsFacingLeft {
        get { return direction == EntityDirection.LEFT; }
        set { Direction = (value ? EntityDirection.LEFT : EntityDirection.RIGHT); }
    }

    private void SetDirectionTransformScale() {
        transform.localScale = new Vector3(
            (direction == defaultDirection) ? +1f : -1f,
            transform.localScale.y,
            transform.localScale.z);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(Entity))]
public class EntityEditor : Editor {
    public override void OnInspectorGUI() {
        Entity entity = (Entity)target;

        entity.Center = EditorGUILayout.ObjectField("Center", entity.Center, typeof(Transform), true) as Transform;

        Entity.EntityDirection previousDefaultDir = entity.DefaultDirection;
        Entity.EntityDirection previousDirection = entity.Direction;
        Entity.EntityDirection inputDefaultDir = (Entity.EntityDirection)EditorGUILayout.EnumPopup("Default Direction", entity.DefaultDirection);
        Entity.EntityDirection inputDirection = (Entity.EntityDirection)EditorGUILayout.EnumPopup("Direction", entity.Direction);
        
        if (previousDefaultDir != inputDefaultDir || previousDirection != inputDirection) {
            entity.DefaultDirection = inputDefaultDir;
            entity.Direction = inputDirection;
        }
    }
}
#endif
