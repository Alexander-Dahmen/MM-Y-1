using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScroller : MonoBehaviour {

    #region Delegates and Events
    public delegate void GlobalScreenScrollDelegate(ScreenScroller scroller, TransformPair pivots);
    public delegate void LocalScreenScrollDelegate(TransformPair pivots);

    public static event GlobalScreenScrollDelegate GlobalScrollStart;
    public static event GlobalScreenScrollDelegate GlobalScrollFinish;
    public event LocalScreenScrollDelegate OnScrollStart;
    public event LocalScreenScrollDelegate OnScrollFinish;
    #endregion

    #region Constants
    private static readonly Vector2 NORMALIZED_MOVE_DISTANCE = new Vector2(3f, 3f);
    #endregion

    #region Static
    private static readonly List<Func<bool>> scrollConditions = new List<Func<bool>>();
    public static void AddScrollCondition(Func<bool> condition) { scrollConditions.Add(condition); }
    public static void RemoveScrollCondition(Func<bool> condition) { scrollConditions.Remove(condition); }

    private static ScreenScroller currentScroller = null;
    private static float Sign(float f) { return (f > 0f) ? +1f : ((f < 0f) ? -1f : 0f); }
    #endregion

    #region Variables
    [SerializeField] private string timeId;
    [SerializeField] private bool onTrigger;
    [SerializeField] private TransformPair pivots;
    [SerializeField] private Vector2 move;
    [SerializeField] private bool normalize;
    [SerializeField] private float customScrollSpeed;

    private GameTime time;
    private CameraController cameraController;
    private CameraScrollController scrollController;

    private GameObject oldRoomPivot;
    private GameObject newRoomPivot;

    #endregion

    #region Unity Methods
    void Awake() {
        cameraController = Camera.main.GetComponent<CameraController>();
        scrollController = Camera.main.GetComponent<CameraScrollController>();
    }
    
    void Start() {
        if (normalize) {
            move = new Vector2(
                Sign(move.x) * NORMALIZED_MOVE_DISTANCE.x,
                Sign(move.y) * NORMALIZED_MOVE_DISTANCE.y);
        }
        time = Timers.Get(timeId);
        
        Debug.AssertFormat((pivots.Left != null), "[{0}] Left pivot is null", this);
        Debug.AssertFormat((pivots.Right != null), "[{0}] Right pivot is null", this);
        Debug.AssertFormat((time != null), "[{0}] GameTime with ID '{1}' is null", this, timeId);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (!onTrigger)
            return;
        if (scrollController.InScreenScroll)
            return;
        PlayerScrollController player =
            collision.GetComponent<PlayerScrollController>();
        if (player == null)
            return;
        Scroll(player);
    }
    #endregion

    #region Public Methods
    public Coroutine Scroll() {
        PlayerScrollController player = PlayerController.Instance.GetComponent<PlayerScrollController>();
        if (player == null) {
            Debug.LogError("ScreenScroller could not find PlayerScrollController: " + this);
            return null;
        } else {
            return Scroll(player);
        }
    }

    public Coroutine Scroll(PlayerScrollController player) {
        if (!CheckScrollConditions())
            return null;

        if (currentScroller == this)
            return null;

        oldRoomPivot = cameraController.Pivots.Left.gameObject;
        newRoomPivot = pivots.Left.gameObject;

        return StartCoroutine(ScrollRoutine(player));
    }
    #endregion

    #region Private Methods
    private bool CheckScrollConditions() {
        foreach (Func<bool> condition in scrollConditions) {
            if (condition == null)
                throw new NullReferenceException("Null scroll condition Func<bool>");
            if (!condition())
                return false;
        }
        return true;
    }
    #endregion

    #region Scrolling Routine
    private IEnumerator ScrollRoutine(PlayerScrollController player) {
        BeforeScroll(player);

        yield return new WaitWhileUpdate(
            () => scrollController.InScreenScroll,
            WhileScroll);
        
        AfterScroll(player);
    }

    private void BeforeScroll(PlayerScrollController player) {
        newRoomPivot.SetActive(true);
        scrollController.ScreenScroll(pivots);
        player.SetScroll(move);
        time.TimeScale = 0f;

        if (GlobalScrollStart != null)
            GlobalScrollStart(this, pivots);
        if (OnScrollStart != null)
            OnScrollStart(pivots);
    }

    private void WhileScroll() {
        //TODO Find some purpose or remove
    }

    private void AfterScroll(PlayerScrollController player) {
        oldRoomPivot.SetActive(false);
        time.TimeScale = 1f;
        player.ClearScroll();

        if (GlobalScrollFinish != null)
            GlobalScrollFinish(this, pivots);
        if (OnScrollFinish != null)
            OnScrollFinish(pivots);
    }
    
    #endregion
}
