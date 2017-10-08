using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectManager : MonoBehaviour {

    #region Singleton Instance

    private static ActiveObjectManager instance = null;
    public static ActiveObjectManager Instance { get { return instance; } }

    #endregion

    #region Variables

    [SerializeField]
    private List<GameObject> objects;

    #endregion

    #region Unity Methods

    void Awake() {
        if (instance == null)
            instance = this;
        if (instance != this)
            Debug.LogErrorFormat("[{0}] Duplicate ActiveObjectManager instance", this);
        if (objects == null)
            objects = new List<GameObject>();
    }

    void Start() {
        foreach (GameObject obj in objects)
            obj.SetActive(true);
    }

    #endregion

    #region Public Methods

    public void SetActive(IEnumerable<GameObject> enable, IEnumerable<GameObject> disable) {
        SetEnabled(enable);
        SetDisabled(disable);
    }

    public void SetEnabled(IEnumerable<GameObject> enable) {
        foreach (GameObject obj in enable) {
            obj.SetActive(true);
            if (!objects.Contains(obj))
                objects.Add(obj);
        }
    }

    public void SetDisabled(IEnumerable<GameObject> disable) {
        foreach (GameObject obj in disable) {
            obj.SetActive(false);
            if (objects.Contains(obj))
                objects.Remove(obj);
        }
    }

    public List<GameObject> Copy() {
        return new List<GameObject>(objects);
    }

    public void Restore(List<GameObject> activeObjects) {
        foreach (GameObject obj in activeObjects) {
            // New active object
            if (!this.objects.Contains(obj)) {
                obj.SetActive(true);
                this.objects.Add(obj);
            }
        }
        foreach (GameObject obj in this.objects) {
            // Remove inactive object
            if (!activeObjects.Contains(obj)) {
                obj.SetActive(false);
                this.objects.Remove(obj);
            }
        }
    }

    #endregion
}