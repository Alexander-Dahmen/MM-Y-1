using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BarController : MonoBehaviour {

	#region Constants

	private static readonly string HUD_SORTING_LAYER = "HUD";
	private static readonly int HUD_SORTING_ORDER = 9001;

	#endregion
	#region Variables

	[SerializeField] private int value;
	[SerializeField] private Vector2 pixelSize;
	[SerializeField] private float fillSpeed;
	[SerializeField] private AudioClip fillSound;
	[SerializeField] private string fillHaltTimer;
	[SerializeField] private bool disableRenderersOnZero;

	private float height;
	private float width;
	private Mesh mesh;
	private MeshFilter filter;
	private MeshRenderer rend;
	private Coroutine fillRoutine;

	#endregion
	#region Unity Methods

	void Awake() {
		filter = GetComponent<MeshFilter>();
		rend = GetComponent<MeshRenderer>();
	}

	void Start() {
		PixelSize = pixelSize;
		InitRenderer();
		CreateMesh();
	}

	#endregion
	#region Properties

	public int Value {
		get { return value; }
		set {
			if (value > this.value) {
				// Value going UP -> Coroutine
				if (fillRoutine != null)
					StopCoroutine(fillRoutine);
				fillRoutine = StartCoroutine(SetValueRoutine(value));
			} else if (value < this.value) {
				// Value going DOWN -> Instant
				this.value = value;
				CreateMesh();
				UpdateRenderersActive();
			}
		}
	}

	public Vector2 PixelSize {
		get { return pixelSize; }
		set {
			if (pixelSize != value) {
				pixelSize = new Vector2((int)value.x, (int)value.y);
				height = value.y / 16f;
				width = value.x / 16f;
			}
		}
	}

	#endregion
	#region Coroutines

	private IEnumerator SetValueRoutine(int target) {
		// If ID is not empty, get timer to halt
		GameTime timer = (fillHaltTimer.Length == 0)
			? null
			: Timers.Get(fillHaltTimer);

		// Create wait yield
		float speed = (fillSpeed > 0f) ? fillSpeed : 1f;
		WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1f / speed);

		// Halt time
		float timerStore = 1f;
		if (timer != null) {
			timerStore = timer.TimeScale;
			timer.TimeScale = 0f;
		}

		// Repeat step by step until target value is reached
		while (value != target) {
			SfxManager.Play(fillSound);

			if (value < target) {
				value += 1;
			} else {
				value -= 1;
			}
			CreateMesh();
			UpdateRenderersActive();

			yield return wait;
		}

		// Resume time
		if (timer != null) {
			timer.TimeScale = timerStore;
		}
		yield return null;
	}

	#endregion
	#region Private Methods

	private void InitRenderer() {
		rend.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
		rend.sortingLayerName = HUD_SORTING_LAYER;
		rend.sortingOrder = HUD_SORTING_ORDER;
	}

	private void UpdateRenderersActive() {
		if (disableRenderersOnZero) {
			bool active = (value > 0);
			Renderer[] renderers = GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in renderers) {
				renderer.enabled = active;
			}
		}
	}

	private void CreateMesh() {
		int i, n;

		// Create mesh instance
		mesh = new Mesh();

		// Apply Vertices
		Vector3[] vertices = new Vector3[(value + 1) * 2];
		for (i = 0; i <= value; i++) {
			vertices[i * 2] = new Vector3(width * i, 0f, 0f);
			vertices[i * 2 + 1] = new Vector3(width * i, height, 0f);
		}
		mesh.vertices = vertices;

		// Apply Triangles
		int[] triangles = new int[value * 6];
		for (i = 0;  i < (2 * value); i++) {
			for (n = 0; i < 3; n++) {
				triangles[i * 3 + n] = i + n;
			}
		}
		mesh.triangles = triangles;

		// Apply UVs
		Vector2[] uv = new Vector2[vertices.Length];
		for (i = 0; i < vertices.Length; i++) {
			uv[i] = new Vector2((int)(i / 2), (i % 2));
		}
		mesh.uv = uv;

		// Register mesh
		mesh.name = "Bar Mesh";
		filter.mesh = mesh;
	}


	#endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(BarController))]
public class BarControllerEditor : Editor {
	public override void OnInspectorGUI() {
		BarController instance = (BarController)target;

		instance.Value = EditorGUILayout.IntField("Value", instance.Value);
		instance.PixelSize = EditorGUILayout.Vector2Field("Pixel Size", instance.PixelSize);
	}
}
#endif