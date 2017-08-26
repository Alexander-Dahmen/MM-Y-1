using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNpcWalk : CustomYieldInstruction {

    private readonly Rigidbody2D body;
    private readonly float target;
    private readonly float speed;

    public WaitNpcWalk(Rigidbody2D body, Vector2 target, float speed, bool delta = false) :
        this(body, target.x, speed, delta) { }

    public WaitNpcWalk(Rigidbody2D body, float target, float speed, bool delta = false) {
        this.body = body;
        this.speed = speed;
        this.target = (delta ? body.position.x : 0f) + target;
    }

    public override bool keepWaiting {
        get {
            // Move towards target position
            body.position = Vector2.MoveTowards(
                body.position,
                new Vector2(
                    target,
                    body.position.y),
                speed * Time.unscaledDeltaTime);

            // Keep walking if target is not reached
            return (body.position.x != target);
        }
    }
}
