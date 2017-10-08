using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class TEST_MOVE_BODY : PlayerControlComponent {

    public float speed;
    
    protected override void Start() { }

    public override void ControllerUpdate() { }

    public override void UpdatePlayer(ref Vector2 velocity, ref Vector2 position) {
        position +=
            new Vector2(Axis(KeyCode.A, KeyCode.D), Axis(KeyCode.S, KeyCode.W)) *
            speed *
            Entity.Time.DeltaTime;
    }

    private float Axis(KeyCode negative, KeyCode positive) {
        bool nDown = Input.GetKey(negative);
        bool pDown = Input.GetKey(positive);
        if (nDown == pDown)
            return 0f;
        if (nDown)
            return -1f;
        if (pDown)
            return +1f;
        return 0f;
    }

}