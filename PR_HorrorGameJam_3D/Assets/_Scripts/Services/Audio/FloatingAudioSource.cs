using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAudioSource : MonoBehaviour {

	[SerializeField] private AudioSource audioSource;

	private float timeUntilStop = -1;
	private AudioManager manager;

	void Update() {
		if (timeUntilStop > 0) {
			timeUntilStop -= Time.unscaledDeltaTime;
			if (timeUntilStop <= 0) {
				manager.RemoveFloatingAudioSource(gameObject);
			}
		}
	}

	public void Play(AudioManager manager, AudioClip clip) {
		audioSource.clip = clip;
		timeUntilStop = clip.length;
		audioSource.Play();

		this.manager = manager;
	}
}
