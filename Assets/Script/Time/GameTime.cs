using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameTime : MonoBehaviour {

    [SerializeField]
    private string id;

    public virtual string Id { get { return id; } }

    public abstract float TimeScale { get; set; }
    public abstract float Time { get; }
    public abstract float DeltaTime { get; }
    public abstract float FixedTime { get; }
    public abstract float FixedDeltaTime { get; }
}