using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public abstract class PlayerControlComponent : MonoBehaviour {
    
    private PlayerController controller;

    protected virtual void Awake() {
        controller = GetComponent<PlayerController>();
    }
    
    protected Entity Entity { get { return controller.Entity; } }
    protected EntityState State { get { return controller.Entity.State; } }


    protected abstract void Start();

    public abstract void ControllerUpdate();
    public abstract void UpdateVelocity(ref Vector2 velocity);
    public abstract void UpdatePosition(ref Vector2 position);
}