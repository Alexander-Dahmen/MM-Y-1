using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


/// <summary>
/// Unity Component for Entity GameObjects.
/// Add this component to have some basic Entity utility functionalities.
/// </summary>
public class Entity : MonoBehaviour {

	#region Constants

    private static readonly string DEFAULT_TIME_ID = "Time";

	#endregion
	#region Static Classes and Methods

	/// <summary>
	/// Entity direction enum.
	/// An entity can either face to the left or face to the right.
	/// </summary>
    public enum EntityDirection {
        LEFT,
        RIGHT
    }

	/// <summary>
	/// Get the direction opposite to the specified direction.
	/// </summary>
	/// <returns>The direction to be inversed.</returns>
	/// <param name="direction">The inverse direction.</param>
	public static EntityDirection OtherDirection(EntityDirection direction) {
		switch (direction) {
			case EntityDirection.LEFT:
				return EntityDirection.RIGHT;
			case EntityDirection.RIGHT:
				return EntityDirection.LEFT;
			default:
				throw new System.Exception("Invalid direction: " + direction);
		}
	}

	#endregion
	#region Variables

    [SerializeField] private string timeId;
    [SerializeField] private Transform center;
    [SerializeField] private EntityState state;
    [SerializeField] private Collider2D mainCollider;

    private EntityDirection defaultDirection;
    private EntityDirection direction;

    private GameTime time;
    private Rigidbody2D body;

	#endregion
	#region Unity Methods

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

	#endregion
	#region Properties

	/// <summary>
	/// Gets or sets the Entity center Transform.
	/// By default, the parent Transform is the center,
	/// it can however be set to another Transform, for instance a child object.
	/// </summary>
	/// <value>The Transform representing the center point of the Entity.</value>
    public Transform Center {
        get { return center; }
        set { center = value; }
    }

	/// <summary>
	/// Gets the EntityState instance associated with this Entity.
	/// </summary>
	/// <value>The EntityState instance or null.</value>
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

	/// <summary>
	/// Gets or sets the default direction the image of this entity is facing.
	/// </summary>
	/// <value>The default direction.</value>
    public EntityDirection DefaultDirection {
        get { return defaultDirection; }
        set { defaultDirection = value; }
    }

	/// <summary>
	/// Gets or sets the direction this Entity is facing.
	/// Setting this value will also affect the Transform localScale x value.
	/// </summary>
	/// <value>The direction .</value>
    public EntityDirection Direction {
        get {
			return direction;
		}
        set {
			if (direction != value) {
				direction = value;
				SetDirectionTransformScale();
			}
        }
    }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is facing right.
	/// </summary>
	/// <value><c>true</c> if this instance is facing right; otherwise, <c>false</c>.</value>
    public bool IsFacingRight {
        get { return direction == EntityDirection.RIGHT; }
        set { Direction = (value ? EntityDirection.RIGHT : EntityDirection.LEFT); }
    }
    
	/// <summary>
	/// Gets or sets a value indicating whether this instance is facing left.
	/// </summary>
	/// <value><c>true</c> if this instance is facing left; otherwise, <c>false</c>.</value>
    public bool IsFacingLeft {
        get { return direction == EntityDirection.LEFT; }
        set { Direction = (value ? EntityDirection.LEFT : EntityDirection.RIGHT); }
    }

	/// <summary>
	/// Gets the GameTime instance used by this Entity.
	/// </summary>
	/// <value>A GameTime instance.</value>
    public GameTime Time {
		get { return time; }
	}

	/// <summary>
	/// Gets the Rigidbody2D attached to this Entity GameObject.
	/// </summary>
	/// <value>The Rigidbody2D.</value>
    public Rigidbody2D Body {
		get { return body; }
	}
    
	#endregion
	#region Private Methods

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

	#endregion
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
