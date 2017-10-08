using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour {

	#region Singleton

	private static SfxManager instance = null;

	#endregion
	#region Variables

	[SerializeField] private int audioSourceSize;

	private float volumeFraction;
	private List<AudioSource> audioSources;

	#endregion
	#region Unity Methods

	void Awake() {
		// Singleton
		if (instance == null) instance = this;
		Debug.AssertFormat(instance == this, "Duplicate SfxManager: {0}, {1}", instance, this);

	}

	void Start() {
		// Reset volume
		volumeFraction = 1.0f;

		// Initialize
		audioSourceSize = (audioSourceSize > 0) ? audioSourceSize : 1;
		CreateAudioSources();
	}

	#endregion
	#region Static Methods and Properties

	public static void Play(AudioClip clip, bool loop = false) {
		instance.DoPlay(clip, loop);
	}

	public static void Stop(AudioClip clip, bool all = true) {
		instance.DoStop(clip, all);
	}

	public static int Volume {
		get {
			return (int)(instance.volumeFraction * 100f);
		}
		set {
			instance.volumeFraction = ((float)value / 100f);
			instance.ApplyVolume();
		}
	}

	#endregion
	#region Private Methods

	private void DoPlay(AudioClip clip, bool loop) {
		if (clip == null)
			return;

		// Look for available source
		foreach (AudioSource source in audioSources) {
			if (!source.isPlaying || (source.clip == clip)) {
				source.clip = clip;
				source.loop = loop;
				source.Play();
				return;
			}
		}

		// Replace oldest playing clip
		AudioSource replace = audioSources[0];
		foreach (AudioSource audioSource in audioSources) {
			if (audioSource.timeSamples > replace.timeSamples) {
				replace = audioSource;
			}
		}
		replace.Stop();
		replace.clip = clip;
		replace.loop = loop;
		replace.Play();
	}

	private void DoStop(AudioClip clip, bool all) {
		if (clip == null)
			return;

		foreach (AudioSource audioSource in audioSources) {
			if (audioSource.isPlaying && (audioSource.clip == clip)) {
				audioSource.Stop();
				if (!all)
					return;
			}
		}
	}

	private void ApplyVolume() {
		foreach (AudioSource audioSource in audioSources) {
			audioSource.volume = volumeFraction;
		}
	}

	private void CreateAudioSources() {
		audioSources = new List<AudioSource>(audioSourceSize);

		for (int i = 0; i < audioSources.Count; i++) {
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.loop = false;
			audioSource.playOnAwake = false;
			audioSource.spatialBlend = 0f;
			audioSources.Add(audioSource);
		}
	}

	#endregion
}
