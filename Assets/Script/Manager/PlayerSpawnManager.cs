using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour {

    private static PlayerSpawnManager instance = null;
    public static PlayerSpawnManager Instance { get { return instance; } }

    //public static event System.Action OnPlayerDeath;
    //public static event System.Action OnPlayerRespawn;

    [SerializeField] private float respawnDelay;
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private GameObject spawnEffectPrefab;




    void Awake() {
        if (instance == null)
            instance = this;
        if (instance != this)
            Debug.LogErrorFormat("[{0}] Duplicate PlayerSpawnManager instance", this);
    }
    
    void Start() {
        
    }
    
    void Update() {
        
    }
}