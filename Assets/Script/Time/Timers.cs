using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour {

    private static Timers instance = null;

    private Map<string, GameTime> timers;

    void Awake() {
        if (instance == null) {
            instance = this;
        }

        if (instance != this) {
            Debug.LogWarning("Destroyed duplicate Timers instance on GameObject: " + gameObject);
            Destroy(this);
        } else {
            timers = new Map<string, GameTime>();
            InitializeGameTimers();
        }
    }
    
    public static GameTime Get(string id) {
        if (id == null)
            return null;
        if (id.Length == 0)
            id = RootGameTime.ROOT_ID;

        GameTime result = instance.timers.Get(id);
        Debug.Assert(
            (result != null),
            "Invalid GameTime ID requested: " + id);

        return result;
    }

    private void InitializeGameTimers() {
        GameTime[] search = FindObjectsOfType<GameTime>();
        foreach (GameTime timer in search) {
            string id = timer.Id;
            if (timers.ContainsKey(id)) {
                Debug.LogError("Duplicate GameTime ID: " + id);
            } else {
                timers.Put(id, timer);
            }
        }
    }

}