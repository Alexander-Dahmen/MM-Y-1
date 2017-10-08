using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealthManager : HealthManager {

	private static bool applicationIsQuitting = false;
	private static PlayerHealthManager instance = null;
	public static PlayerHealthManager Instance { get { return instance; } }

	[SerializeField] private BarController healthBar;
	[SerializeField] private float invincibleTime;
	[SerializeField] private float invincibleBlinkSpeed;
	[SerializeField] private string invincibleLayerName;

	private int invincibleLayer;
	private int defaultLayer;

    protected override void Awake() {
        base.Awake();

		// Singleton
		if (instance == null)
			instance = this;
		Debug.AssertFormat(
			instance == this,
			"Duplicate PlayerHealthManager: {0}, {1}", instance, this);
    }

	protected override void Start() {
		base.Start();
		invincibleLayer = LayerMask.NameToLayer(invincibleLayerName);
		defaultLayer = gameObject.layer;

	}
}