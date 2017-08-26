using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CameraController))]
public class CameraScrollController : MonoBehaviour {
    
    [SerializeField]
    private float scrollSpeed;

    private Camera cam;
    private CameraController controller;

    private bool inScreenScroll;
    private Vector3 scrollTarget;
    private float initialScrollDistance;
    

    void Awake() {
        cam = GetComponent<Camera>();
        controller = GetComponent<CameraController>();
        inScreenScroll = false;
    }
    
    public bool InScreenScroll { get { return inScreenScroll; } }

    public void ScreenScroll(Transform leftPivot, Transform rightPivot) {
        ScreenScroll(new TransformPair(leftPivot, rightPivot));
    }

    public void ScreenScroll(TransformPair pivots) {
        // Ignore duplicate scrolls
        if (inScreenScroll) {
            Debug.LogWarning("Duplicate ScreenScroll request");
        } else {
            controller.Pivots = pivots;
            scrollTarget = pivots.Same() ?
                pivots.Left.position :
                controller.CameraLocation();
            initialScrollDistance = Vector2.Distance(
                cam.transform.position,
                scrollTarget);

            StartCoroutine(ScrollRoutine());
        }
    }

    public float ScrollFraction {
        get {
            if (inScreenScroll) {
                if (initialScrollDistance == 0f) {
                    Debug.LogError("Initial ScrollDistance is zero");
                    return 1f;
                } else {
                    return (1f - (Vector2.Distance(cam.transform.position, scrollTarget) / initialScrollDistance));
                }
            } else {
                return 1f;
            }
        }
    }

    private IEnumerator ScrollRoutine() {
        inScreenScroll = true;
        controller.Active = false;

        while (cam.transform.position.x != scrollTarget.x || cam.transform.position.y != scrollTarget.y) {
            Vector2 position = Vector2.MoveTowards(
                cam.transform.position,
                scrollTarget,
                scrollSpeed * Time.unscaledDeltaTime);

            cam.transform.position = new Vector3(
                position.x,
                position.y,
                controller.ZDist);

            yield return null;
        }
        yield return null;

        inScreenScroll = false;
        controller.Active = true;
    }
}
