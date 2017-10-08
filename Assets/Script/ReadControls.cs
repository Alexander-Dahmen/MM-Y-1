using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using UnityEngine;

[ExecuteInEditMode]
public class ReadControls : MonoBehaviour {

	#region Constants

    private const string CONTROL_FILE = "Controls.xml";
    private const float JOY_DEADZONE = 0.2f;
	private const string JOY_AXIS_X = "Horizontal";
	private const string JOY_AXIS_Y = "Vertical";

	#endregion
	#region Singleton

    private static ReadControls instance = null;

	#endregion
	#region Nested Classes

    [XmlRoot("controls")]
	[System.Serializable]
    public class ControlData {
        [XmlElement("up")] public KeyCode up;
        [XmlElement("down")] public KeyCode down;
        [XmlElement("left")] public KeyCode left;
        [XmlElement("right")] public KeyCode right;
        [XmlElement("jump")] public KeyCode jump;
        [XmlElement("fire1")] public KeyCode fire1;
        [XmlElement("fire2")] public KeyCode fire2;
        [XmlElement("fire3")] public KeyCode fire3;
        [XmlElement("pause")] public KeyCode pause;
    }

	#endregion
	#region Variables

	[SerializeField] private bool useJoystick;
	[SerializeField] private ControlData controlData;

	[SerializeField] private bool up;
	[SerializeField] private bool down;
	[SerializeField] private bool left;
	[SerializeField] private bool right;
	[SerializeField] private bool jump;
	[SerializeField] private bool fire1;
	[SerializeField] private bool fire2;
	[SerializeField] private bool fire3;
	[SerializeField] private bool jumpHold;
	[SerializeField] private bool fire1Hold;
	[SerializeField] private bool fire2Hold;
	[SerializeField] private bool fire3Hold;
	[SerializeField] private bool pause;

	#endregion
	#region Unity Methods

	void Awake() {
        if (instance == null)
            instance = this;
		if (instance != this)
			Destroy(this);
    }
    
    void Start() { }
    
    void Update() {
		UpdateKeys();	
		if (useJoystick)
			UpdateJoystick();
    }

	#endregion
	#region Static Properties and Methods

	public static bool Enabled {
		get { return (instance == null) ? false : instance.enabled; }
		set { if (instance != null) instance.enabled = value; }
	}
	public static bool Up {
		get { return instance.up && !instance.down && Enabled; }
	}
	public static bool Down {
		get { return instance.down && !instance.up && Enabled; }
	}
	public static bool Left {
		get { return instance.left && !instance.right && Enabled; }
	}
	public static bool Right {
		get { return instance.right && !instance.left && Enabled; }
	}
	public static bool Jump {
		get { return instance.jump && Enabled; }
	}
	public static bool Fire1 {
		get { return instance.fire1 && Enabled; }
	}
	public static bool Fire2 {
		get { return instance.fire2 && Enabled; }
	}
	public static bool Fire3 {
		get { return instance.fire3 && Enabled; }
	}
	public static bool JumpHold {
		get { return instance.jumpHold && Enabled; }
	}
	public static bool Fire1Hold {
		get { return instance.fire1Hold && Enabled; }
	}
	public static bool Fire2Hold {
		get { return instance.fire2Hold && Enabled; }
	}
	public static bool Fire3Hold {
		get { return instance.fire3Hold && Enabled; }
	}
	public static bool Pause {
		get { return instance.pause && Enabled; }
	}
	public static float XAxis {
		get {
			if (!Enabled) return 0f;
			if (Left) return -1f;
			if (Right) return +1f;
			return 0f;
		}
	}
	public static float YAxis {
		get {
			if (!Enabled) return 0f;
			if (Up) return +1f;
			if (Down) return -1f;
			return 0f;
		}
	}

	#endregion
	#region Private Methods

	private void UpdateKeys() {
		up = Input.GetKey(controlData.up);
		down = Input.GetKey(controlData.down);
		left = Input.GetKey(controlData.left);
		right = Input.GetKey(controlData.right);
		jumpHold = Input.GetKey(controlData.jump);
		fire1Hold = Input.GetKey(controlData.fire1);
		fire2Hold = Input.GetKey(controlData.fire2);
		fire3Hold = Input.GetKey(controlData.fire3);
		jump = Input.GetKeyDown(controlData.jump);
		fire1 = Input.GetKeyDown(controlData.fire1);
		fire2 = Input.GetKeyDown(controlData.fire2);
		fire3 = Input.GetKeyDown(controlData.fire3);
		pause = Input.GetKeyDown(controlData.pause);
	}

	private void UpdateJoystick() {
		float x = Input.GetAxisRaw(JOY_AXIS_X);
		float y = Input.GetAxisRaw(JOY_AXIS_Y);
		up |= (y < -JOY_DEADZONE);
		down |= (y > JOY_DEADZONE);
		left |= (x < -JOY_DEADZONE);
		right |= (x > JOY_DEADZONE);
	}

	#endregion
}
