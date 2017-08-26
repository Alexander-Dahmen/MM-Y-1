using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForMoveTowards : CustomYieldInstruction {

    private readonly IMoveable moveable;
    private Vector3 target;
    private float speed;

    public WaitForMoveTowards(Rigidbody body, Vector3 target, float speed) {
        this.moveable = new MoveableRigidbody(body);
        this.target = target;
        this.speed = speed;
    }
    public WaitForMoveTowards(Rigidbody2D body, Vector2 target, float speed) {
        this.moveable = new MoveableRigidbody2D(body);
        this.target = target;
        this.speed = speed;
    }
    public WaitForMoveTowards(Transform transform, Vector3 target, float speed) {
        this.moveable = new MoveableTransform(transform);
        this.target = target;
        this.speed = speed;
    }
    
    public override bool keepWaiting {
        get {
            moveable.Position = Vector3.MoveTowards(moveable.Position, target, Time.deltaTime * speed);
            return (moveable.Position != target);
        }
    }

    private interface IMoveable {
        Vector3 Position { get; set; }
    }
    private class MoveableRigidbody2D : IMoveable {
        private readonly Rigidbody2D body;
        public MoveableRigidbody2D(Rigidbody2D body) {
            this.body = body;
        }
        Vector3 IMoveable.Position {
            get { return body.position; }
            set { body.position = value; }
        }
    }
    private class MoveableRigidbody : IMoveable {
        private readonly Rigidbody body;
        public MoveableRigidbody(Rigidbody body) { this.body = body; }
        Vector3 IMoveable.Position {
            get { return body.position; }
            set { body.position = value; }
        }
    }
    private class MoveableTransform : IMoveable {
        private readonly Transform transform;
        public MoveableTransform(Transform transform) { this.transform = transform; }
        Vector3 IMoveable.Position {
            get { return transform.position; }
            set { transform.position = value; }
        }
    }
}