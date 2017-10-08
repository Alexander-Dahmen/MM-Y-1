using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectsTrigger : MonoBehaviour {

    public enum EnableDisableMode {
        AllAtOnce,
        EnterThenExit
    }

    private static readonly System.Type PLAYER_COMPONENT = typeof(PlayerController);

    [SerializeField] private EnableDisableMode mode;
    [SerializeField] private GameObject[] enable;
    [SerializeField] private GameObject[] disable;

    private ActiveObjectManager manager;
    
    void Start() {
        manager = ActiveObjectManager.Instance;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent(PLAYER_COMPONENT)) {
            if (mode == EnableDisableMode.AllAtOnce)
                manager.SetActive(enable, disable);
            else if (mode == EnableDisableMode.EnterThenExit)
                manager.SetEnabled(enable);
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent(PLAYER_COMPONENT)) {
            if (mode == EnableDisableMode.EnterThenExit)
                manager.SetDisabled(disable);
        }
    }
}