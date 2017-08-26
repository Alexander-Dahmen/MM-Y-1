using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class RootGameTime : GameTime {

    public static readonly string ROOT_ID = "Time";

    public override string Id { get { return ROOT_ID; } }

    public override float TimeScale {
        get { return UnityEngine.Time.timeScale; }
        set { UnityEngine.Time.timeScale = value; }
    }

    public override float Time { get { return UnityEngine.Time.time; } }

    public override float DeltaTime { get { return UnityEngine.Time.deltaTime; } }

    public override float FixedTime { get { return UnityEngine.Time.fixedTime; } }

    public override float FixedDeltaTime { get { return UnityEngine.Time.fixedDeltaTime; } }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RootGameTime))]
public class RootGameTimeEditor : Editor {
    public override void OnInspectorGUI() {
        RootGameTime time = target as RootGameTime;
        EditorGUILayout.LabelField("Id", time.Id);
        time.TimeScale = EditorGUILayout.FloatField("Time Scale", time.TimeScale);
        EditorGUILayout.FloatField("Time", time.Time);
        EditorGUILayout.FloatField("Delta Time", time.DeltaTime);
        EditorGUILayout.FloatField("Fixed Time", time.FixedTime);
        EditorGUILayout.FloatField("Fixed Delta Time", time.FixedDeltaTime);
    }
}
#endif