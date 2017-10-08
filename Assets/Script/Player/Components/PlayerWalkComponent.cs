using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkComponent : PlayerControlComponent {

	#region Nested Classes

	public enum WalkState {
		STOP,
		ACCELERATING,
		WALKING,
		DECELERATING
	}

	#endregion
	#region Variables

	[SerializeField] private float walkSpeed;
	[SerializeField] private int accFrames;
	[SerializeField] private float accSpeed;
	[SerializeField] private int decFrames;
	[SerializeField] private float decSpeed;

	private bool isWalking;
	private float walkVelocity;
	private float speedChangeTimer;
	private WalkState walkState;

	//TODO Maybe implement ice-pysics modifier here

	#endregion
	#region Unity Methods

	protected override void Start() {
		base.Start();
	}

	#endregion
	#region Implementation

	public override void ControllerUpdate() {
		bool left = ReadControls.Left;
		bool right = ReadControls.Right;
		isWalking = (left != right);
		//TODO Start/Stop walking animation

		if (isWalking) {
			Entity.IsFacingRight = right;
			Accelerate();
		} else {
			Decelerate();
		}
	}

	public override void UpdatePlayer(ref Vector2 velocity, ref Vector2 position) {
		velocity = new Vector2(
			walkVelocity * (Entity.IsFacingLeft ? -1f : +1f),
			velocity.y);
	}

	#endregion
	#region Properties

	public bool IsWalking {
		get { return isWalking; }
	}

	private float AccTime {
		get { return (float)accFrames / 60f; }
	}

	private float DecTime {
		get { return (float)decFrames / 60f; }
	}

	#endregion
	#region Private Methods

	private void Accelerate() {
		switch (walkState) {
			case WalkState.STOP:
				speedChangeTimer = 0f;
				walkState = WalkState.ACCELERATING;
				break;	  
		}
	}

	private void Decelerate() {
		
	}

	#endregion
}
