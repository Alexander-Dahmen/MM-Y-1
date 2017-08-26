using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScroller : MonoBehaviour {

#region Delegates and Events
    public delegate void ScreenScrollDelegate(TransformPair pivots);

    public static event ScreenScrollDelegate OnScrollStart;
    public static event ScreenScrollDelegate OnScrollFinish;
#endregion

#region Constants
    private static readonly Vector2 NORMALIZED_MOVE_DISTANCE = new Vector2(3f, 3f);
    private static readonly List<GameObject> activeObjects = new List<GameObject>();
#endregion

#region Static
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

    [SerializeField] private List<GameObject> enableObjects;
    [SerializeField] private List<GameObject> disableObjects;

    private GameTime time;
    private CameraController cameraController;
    private CameraScrollController scrollController;
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
        //TODO Check if player is alive before scroll

        if (currentScroller == this)
            return null;
        
        return StartCoroutine(ScrollRoutine(player));
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
        scrollController.ScreenScroll(pivots);
        player.SetScroll(move);
        time.TimeScale = 0f;

        if (OnScrollStart != null)
            OnScrollStart(pivots);
    }

    private void WhileScroll() {
        //TODO Find some purpose or remove
    }

    private void AfterScroll(PlayerScrollController player) {
        time.TimeScale = 1f;
        player.ClearScroll();

        if (OnScrollFinish != null)
            OnScrollFinish(pivots);
    }

    private void SetPivotsActive() {
        GameObject oldPivot = cameraController.Pivots.Left.gameObject;
        GameObject newPivot = pivots.Left.gameObject;

        //TODO Check if pivots need to be added to activate/deactivate objects

        oldPivot.SetActive(false);
        newPivot.SetActive(true);
    }
#endregion
}
