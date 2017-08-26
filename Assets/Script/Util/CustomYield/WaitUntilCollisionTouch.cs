using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilCollisionTouch : CustomYieldInstruction {

    private bool touch;
    private Collider2D first;
    private Collider2D second;

    public WaitUntilCollisionTouch(Collider2D first, Collider2D second, bool touch) {
        this.touch = touch;
        this.first = first;
        this.second = second;
    }

    public override bool keepWaiting {
        get {
            // Keep waiting while touch state is not the desired touch state
            return (first.IsTouching(second) != touch);
        }
    }
}