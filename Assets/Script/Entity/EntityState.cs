using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState : MonoBehaviour {

    public static readonly int OFF_GROUND_FRAMES = 2;

    [SerializeField]
    private LayerMask ground;

    [SerializeField]
    private Collider2D mainCollider;

    [SerializeField]
    private Transform[] groundChecks;

    [SerializeField]
    private Transform[] ceilingChecks;

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
		
	}
    
    private bool GetGroundState() {
        //TODO Check ground state
        return false;
    }
}
