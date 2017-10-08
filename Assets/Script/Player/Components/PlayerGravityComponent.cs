using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerGravityComponent : PlayerControlComponent {
    
    #region Variables

    [SerializeField] private float gravity;
    [SerializeField] private float terminalVelocity;
    [SerializeField] private float underwaterGravity;
    [SerializeField] private float underwaterTerminalVelocity;
    [SerializeField] private float ceilingHitDropoff;

    private float defaultGravity;
    private float defaultTerminalVelocity;
    private float centerOffset;
    //private PlayerDashComponent dash;

    private int frameCounter;

    #endregion

    #region Control Component Implementation

    protected override void Start() {
        defaultGravity = gravity;
        defaultTerminalVelocity = terminalVelocity;
        centerOffset = Entity.Center.localPosition.y;
    }

    public override void ControllerUpdate() { }

    public override void UpdatePlayer(ref Vector2 velocity, ref Vector2 position) {
        // Calculate movement acceleration
        float deltaVelocity = Entity.State.IsGrounded ? 0f : gravity;
        float diffToTerminal = terminalVelocity - (velocity.y * GravitySign);
        float acceleration = Mathf.Min(deltaVelocity, diffToTerminal);

        // Add acceleration to velocity
        velocity += Vector2.down * acceleration * Entity.Time.DeltaTime * 60f;
    }

    #endregion

    #region Public Methods

    public void ResetGravity() {
        Gravity = defaultGravity;
        TerminalVelocity = defaultTerminalVelocity;
    }

    #endregion

    #region Properties

    public float Gravity {
        get { return gravity; }
        set {
            if (Gravity != value) {
                gravity = value;
                SetPlayerScale();
            }
        }
    }

    public bool InvertedGravity {
        get { return (GravitySign > 0f); }
        set {
            if (value != InvertedGravity) {
                Gravity = Mathf.Abs(Gravity) * (value ? -1f : +1f);
            }
        }
    }

    public float TerminalVelocity {
        get { return terminalVelocity; }
        set { terminalVelocity = value; }
    }

    public int GravitySign { get { return ((gravity < 0f) ? +1 : -1);  } }

    #endregion

    #region Private Methods

    private void SetPlayerScale() {
        float before = transform.localScale.y;
        float after = (-1f) * GravitySign;
        if (before != after) {
            transform.localScale = new Vector3(
                transform.localScale.x,
                after,
                transform.localScale.z);
            transform.position +=
                Vector3.up
                * 2f
                * GravitySign
                * centerOffset;
        }
    }

    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerGravityComponent))]
public class PlayerGravityComponentEditor : Editor {
    public override void OnInspectorGUI() {
        PlayerGravityComponent component = target as PlayerGravityComponent;
        
        component.Gravity = EditorGUILayout.FloatField("Gravity", component.Gravity);
        component.InvertedGravity = EditorGUILayout.Toggle("Inverted", component.InvertedGravity);
    }
}
#endif
