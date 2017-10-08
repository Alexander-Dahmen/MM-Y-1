using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    #region Static Variables

    private static Checkpoint current = null;
    public static Checkpoint Current { get { return current; } }
    
    #endregion

    #region Variables

    private TransformPair pivots;
    private List<GameObject> activeObjects;
    private CameraController cameraController;
    private ActiveObjectManager activeObjectManager;
    
    #endregion

    #region Unity Methods

    void Start() {
        cameraController = CameraController.Instance;
        activeObjectManager = ActiveObjectManager.Instance;
    }
    
    public void Set() {
        current = this;
        pivots = cameraController.Pivots;
        activeObjects = activeObjectManager.Copy();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PlayerController>()) {
            Set();
        }
    }
    
    #endregion

    #region Public Properties

    public TransformPair Pivots { get { return pivots; } }
    public List<GameObject> ActiveObjects { get { return activeObjects; } }

    #endregion
}