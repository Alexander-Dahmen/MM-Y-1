using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    #region Delegates and Events
    public delegate void CameraControllerActiveDelegate();

    public static event CameraControllerActiveDelegate OnActivate;
    public static event CameraControllerActiveDelegate OnDeactivate;
    #endregion

    #region Singleton Instance
    private static CameraController instance = null;
    public static CameraController Instance { get { return instance; } }
    #endregion

    #region Variables
    [SerializeField] private Transform target;
    [SerializeField] private TransformPair pivots;

    private Camera cam;
    private float zDist;
    private bool active;
    private Map<string, Vector2> offsets;
    private Vector3Pair anchors;
    #endregion

    #region Unity Methods
    void Awake() {
        if (instance == null)
            instance = this;
        if (instance != this)
            Debug.LogError("Duplicate CameraController on GameObject: " + gameObject);
        if (target == null)
            target = PlayerController.Instance.Entity.Center;
        offsets = new Map<string, Vector2>();
        zDist = transform.position.z;
        cam = GetComponent<Camera>();
        active = true;
    }
    
    void Start() {
        UpdateAnchors();
    }
    
    void Update() {
        if (target == null)
            return;
        if (!active)
            return;

        Vector2 position = pivots.Same() ?
            anchors.Left :
            CameraLocation();

        AddOffset(ref position);

        transform.position = new Vector3(position.x, position.y, zDist);
    }
    #endregion

    #region Properties
    public bool Active {
        get { return active; }
        set {
            if (active != value) {
                active = value;
                var call = active ?
                    OnActivate :
                    OnDeactivate;
                if (call != null)
                    call();
            }
        }
    }

    public Vector3 LowerLeftCorner { get { return cam.ViewportToWorldPoint(new Vector3(0, 0, zDist)); } }
    public Vector3 UpperLeftCorner { get { return cam.ViewportToWorldPoint(new Vector3(0, 1, zDist)); } }
    public Vector3 LowerRightCorner { get { return cam.ViewportToWorldPoint(new Vector3(1, 0, zDist)); } }
    public Vector3 UpperRightCorner { get { return cam.ViewportToWorldPoint(new Vector3(1, 1, zDist)); } }
    
    public Transform Target {
        get { return target; }
        set { target = value; }
    }

    public TransformPair Pivots {
        get { return pivots; }
        set {
            pivots = value;
            UpdateAnchors();
        }
    }

    public float ZDist { get { return zDist; } }
    #endregion

    #region Public Methods
    public void SetPivots(Transform left, Transform right) {
        Pivots = new TransformPair(left, right);
    }

    public void SetOffset(string key, Vector2 offset) {
        offsets.Put(key, offset);
    }

    public void ClearOffset(string key) {
        offsets.Remove(key);
    }

    public Vector3 CameraLocation() {
        if (target == null) {
            return transform.position;
        } else {
            Vector3 m = anchors.Right - anchors.Left;
            if (m == Vector3.zero)
                return anchors.Left;
            Vector3 t = target.position;
            float d = m.x * t.x + m.y * t.y;
            float r = (d - m.x * anchors.Left.x - m.y * anchors.Left.y) / (m.x * m.x + m.y * m.y);
            r = Mathf.Clamp01(r);
            return anchors.Left + r * m;
        }
    }
    #endregion

    #region Private Methods
    private void UpdateAnchors() {
        Vector3 upLeftDelta = transform.position - UpperLeftCorner;
        Vector3 upRightDelta = transform.position - UpperRightCorner;
        Vector3 left = pivots.Left.position + upLeftDelta;
        Vector3 right = pivots.Right.position + upRightDelta;
        anchors = new Vector3Pair(left, right);
    }

    private void AddOffset(ref Vector2 position) {
        foreach (Vector2 offset in offsets.Values)
            position += offset;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected () {
        if (cam == null)
            cam = Camera.main;
        if (anchors == null)
            UpdateAnchors();

		Gizmos.DrawSphere (anchors.Left, 0.5f);
		Gizmos.DrawSphere (anchors.Right, 0.5f);
		Gizmos.DrawLine (pivots.Left.position, pivots.Right.position);
		Gizmos.DrawLine (anchors.Left, anchors.Right);

		if (target)
			Gizmos.DrawLine (transform.position, target.position);
	}
#endif
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor {
    public override void OnInspectorGUI() {
        CameraController cc = target as CameraController;
        cc.Active = EditorGUILayout.Toggle("Active", cc.Active);
        base.OnInspectorGUI();
    }
}
#endif