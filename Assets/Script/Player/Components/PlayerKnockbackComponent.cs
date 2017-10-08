using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackComponent : PlayerControlComponent {

    #region Variables

    [SerializeField] private float knockBackLength;
    [SerializeField] private Vector2 knockBackVelocity;

    private bool firstFrame;
    private float knockBackTimer;
    private Entity.EntityDirection knockDirection;

    #endregion
    #region PlayerControlComponent Implementation

    protected override void Start() {
        base.Start();
        knockBackTimer = 0f;
    }

    public override void ControllerUpdate() { }

    public override void UpdatePlayer(ref Vector2 velocity, ref Vector2 position) {
        if (InKnockBack) {
            velocity = new Vector2(
                KnockDirectionFloat * knockBackVelocity.x,
                firstFrame ? 0f : velocity.y);

            firstFrame = false;

            knockBackTimer -= Entity.Time.DeltaTime;
			if (knockBackTimer <= 0f) {
				//TODO ReadControls.Enabled = true;
				//TODO Clear player knockback animation
			}
        }
    }

    #endregion
    #region Public Methods

	public void KnockBack(Transform source) {
		knockBackTimer = knockBackLength;
		firstFrame = true;
		//TODO ReadControls.Enabled = false;
		//TODO Set player knockback animation

		if (source == null) {
			knockDirection = Entity.OtherDirection(Entity.Direction);
		} else {
			knockDirection = (transform.position.x < source.position.x)
				? Entity.EntityDirection.LEFT
				: Entity.EntityDirection.RIGHT;
		}
	}

    #endregion
    #region Properties

    public bool InKnockBack {
        get { return (knockBackTimer > 0f); }
    }

    private float KnockDirectionFloat {
        get {
            if (knockDirection == Entity.EntityDirection.LEFT)
                return (-1f);
            if (knockDirection == Entity.EntityDirection.RIGHT)
                return (+1f);
            return 0f;
        }
    }

    #endregion
}