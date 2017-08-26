using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(EntityState))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private static PlayerController instance = null;
    public static PlayerController Instance { get { return instance; } }

    public string[] componentNames;

    private bool active;
    private Entity entity;
    private Rigidbody2D body;
    private Vector2 velocity;
    private Vector2 position;
    private PlayerControlComponent[] components;

    void Awake() {
        if (instance == null)
            instance = this;
        Debug.AssertFormat(
            instance,
            "Duplicated PlayerController instance: {0} and {1}",
            instance, this);

        entity = GetComponent<Entity>();
        body = GetComponent<Rigidbody2D>();
    }

    void Start() {
        InitializeControlComponents();
    }

    void OnEnable() {
        this.position = body.position;
        this.velocity = body.velocity;
        this.active = true;
    }

    void Update() {
        if (!active)
            return;

        this.position = body.position;
        this.velocity = body.velocity;

        foreach (PlayerControlComponent component in components) {
            if (component.enabled) {
                component.ControllerUpdate();
                component.UpdatePosition(ref position);
                component.UpdateVelocity(ref position);
            }
        }

        body.position = this.position;
        body.velocity = this.velocity;
    }

    public bool Active {
        get { return active; }
        set {
            active = value;
            //TODO Activate/Deactivate management
        }
    }

    public Entity Entity { get { return entity; } }

    public Rigidbody2D Body { get { return body; } }


    private void InitializeControlComponents() {
        components = new PlayerControlComponent[componentNames.Length];
        int i = 0;
        foreach (string componentName in componentNames) {
            PlayerControlComponent component = GetComponent(componentName) as PlayerControlComponent;
            if (component == null) {
                Debug.LogErrorFormat(
                    "Player control component type does not exist: {0}",
                    componentName);
            } else {
                components[i++] = component;
            }
        }
    }
}