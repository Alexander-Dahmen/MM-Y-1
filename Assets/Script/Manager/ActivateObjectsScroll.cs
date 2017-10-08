using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScreenScroller))]
public class ActivateObjectsScroll : MonoBehaviour {

    [SerializeField] private GameObject[] enable;
    [SerializeField] private GameObject[] disable;

    private ScreenScroller scroller;
    private ActiveObjectManager manager;

    void Awake() {
        scroller = GetComponent<ScreenScroller>();
    }

    void Start() {
        manager = ActiveObjectManager.Instance;
    }

    void OnEnable() {
        scroller.OnScrollStart += ScrollStart;
        scroller.OnScrollFinish += ScrollFinish;
    }

    void OnDisable() {
        scroller.OnScrollStart -= ScrollStart;
        scroller.OnScrollFinish -= ScrollFinish;
    }

    void ScrollStart(Pair<Transform> t) {
        manager.SetEnabled(enable);
    }

    void ScrollFinish(Pair<Transform> t) {
        manager.SetDisabled(disable);
    }
}