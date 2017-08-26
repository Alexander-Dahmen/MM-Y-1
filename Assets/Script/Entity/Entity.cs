using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Entity : MonoBehaviour {

    private static readonly string DEFAULT_TIME_ID = "Time";

    public enum EntityDirection {
        LEFT,
        RIGHT
    }

    [SerializeField] private string timeId;
    [SerializeField] private Transform center;
    [SerializeField] private EntityState state;
    [SerializeField] private Collider2D mainCollider;

    private EntityDirection defaultDirection;
    private EntityDirection direction;

    private GameTime time;
    private Rigidbody2D body;


	void Awake () {
        if (center == null)
            center = transform;
        if (state == null)
            state = GetComponent<EntityState>();
        if (mainCollider == null)
            mainCollider = GetComponent<Collider2D>();
        body = GetComponent<Rigidbody2D>();

        InitializeTimer();
        SetDirectionTransformScale();
	}
	
    public Transform Center {
        get { return center; }
        set { center = value; }
    }

    public EntityState State {
        get {
#if UNITY_EDITOR
            Debug.AssertFormat(
                (state != null),
                "Requested EntityState on Entity without EntityState: {0}",
                name);
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

    public GameTime Time { get { return time; } }

    public Rigidbody2D Body { get { return body; } }
    

    private void InitializeTimer() {
        if (timeId.Length == 0)
            timeId = DEFAULT_TIME_ID;
        time = Timers.Get(timeId);
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
        base.OnInspectorGUI();

        Entity entity = (Entity)target;

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
