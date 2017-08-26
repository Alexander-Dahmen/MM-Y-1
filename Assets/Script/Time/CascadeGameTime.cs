using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeGameTime : GameTime {

    [SerializeField]
    private string parentId;

    [SerializeField]
    private float timeScale;

    private GameTime parent;
    private float time;
    private float deltaTime;
    private float fixedTime;
    private float fixedDeltaTime;

    void Start() {
        parent = Timers.Get(parentId);
        Debug.AssertFormat(
            (parent != null),
            "Cascading GameTime '{0}' parent timer does not exist: {1}",
            Id, parentId);

        time = fixedTime = 0f;
    }

    void Update() {
        deltaTime = parent.DeltaTime * timeScale;
        fixedDeltaTime = parent.FixedDeltaTime * timeScale;
        time += deltaTime * timeScale;
        fixedTime += FixedDeltaTime * timeScale;
    }

    public override float TimeScale {
        get { return timeScale; }
        set { timeScale = value; }
    }

    public override float Time { get { return time; } }

    public override float DeltaTime { get { return deltaTime; } }

    public override float FixedTime { get { return fixedTime; } }

    public override float FixedDeltaTime { get { return fixedDeltaTime; } }
}