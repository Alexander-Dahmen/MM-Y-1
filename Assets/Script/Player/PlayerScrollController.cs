using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerScrollController : MonoBehaviour {

    private PlayerController controller;
    private CameraScrollController cameraScroll;

    private Vector2 velocityStore;


    void Awake() {
        controller = gameObject.GetComponent<PlayerController>();
        cameraScroll = Camera.main.GetComponent<CameraScrollController>();
    }

    public void SetScroll(Vector2 movement) {
        controller.Active = false;
        velocityStore = controller.Body.velocity;
        controller.Body.velocity = Vector2.zero;
        StartCoroutine(ScrollRoutine(movement));
    }

    public void ClearScroll() {
        controller.Active = true;
        controller.Body.velocity = velocityStore;
    }

    private IEnumerator ScrollRoutine(Vector2 movement) {
        Vector2 origin = controller.Body.position;
        Vector2 target = controller.Body.position + movement;

        yield return null;

        while (cameraScroll.InScreenScroll) {
            transform.position = Vector2.Lerp(
                origin,
                target,
                cameraScroll.ScrollFraction);
            yield return null;
        }
    }
}