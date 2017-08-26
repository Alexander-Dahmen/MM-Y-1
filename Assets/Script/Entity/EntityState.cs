using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EntityState : MonoBehaviour {

    public static readonly int OFF_GROUND_FRAMES = 2;

    [SerializeField] private Collider2D mainCollider;

    [SerializeField] private LayerMask groundLayer;
    
    [SerializeField] private Collider2D groundCheck;
    [SerializeField] private Collider2D ceilingCheck;
    
    private bool mainColliderGrounded;

    private int offGroundCounter;
    private int offCeilingCounter;


	void Awake() {
		if (mainCollider == null) {
            mainCollider = GetComponent<Collider2D>();
        }

        Debug.AssertFormat(
            (mainCollider != null),
            "Main Collider2D missing in Entity \"{0}\"", gameObject.name);
	}

    void Start() {
        offGroundCounter = offCeilingCounter = 0;
    }

    public bool IsGrounded {
        get { return (offGroundCounter <= OFF_GROUND_FRAMES); }
    }

    public bool IsTouchingCeiling {
        get { return (offCeilingCounter <= OFF_GROUND_FRAMES); }
    }

	void FixedUpdate() {
        mainColliderGrounded = mainCollider.IsTouchingLayers(groundLayer);
        offGroundCounter = Math.Max(
            UpdateCounter(offGroundCounter, groundCheck),
            OFF_GROUND_FRAMES);
        offCeilingCounter = Math.Max(
            UpdateCounter(offCeilingCounter, ceilingCheck),
            OFF_GROUND_FRAMES);
	}

    private int UpdateCounter(int offGroundCounter, Collider2D groundCheck) {
        if (GroundCollision(groundCheck)) {
            return 0;
        } else {
            return (offGroundCounter + 1);
        }
    }

    private bool GroundCollision(Collider2D collider) {
        return (
            collider.IsTouchingLayers(groundLayer)
            && mainColliderGrounded);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EntityState))]
public class EntityStateEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        EntityState state = (EntityState)target;
        EditorGUILayout.Toggle("Grounded", state.IsGrounded);
        EditorGUILayout.Toggle("Ceiling", state.IsTouchingCeiling);
    }
}
#endif

