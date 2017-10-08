﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public abstract class PlayerControlComponent : MonoBehaviour {
    
    private PlayerController controller;

    protected virtual void Awake() {
        controller = GetComponent<PlayerController>();
    }

    protected virtual void Start() { }

    protected Entity Entity { get { return controller.Entity; } }
    protected EntityState State { get { return controller.Entity.State; } }
    
    public abstract void ControllerUpdate();
    public abstract void UpdatePlayer(ref Vector2 velocity, ref Vector2 position);
}