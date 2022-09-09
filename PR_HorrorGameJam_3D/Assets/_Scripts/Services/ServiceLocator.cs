using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour {
	// Readonly services
	public static AudioManager @AudioManager { get; set; }
	public static SceneTransition @SceneManager { get; set; }

	// Singleton
	private static ServiceLocator instance;

	private void Awake() {
		if (instance != null && instance != this) {
			Destroy(this);
			return;
		}
		instance = this;
		LocateServices();
	}

	private void LocateServices() {
		@AudioManager = FindObjectOfType<AudioManager>();
		@SceneManager = FindObjectOfType<SceneTransition>();
	}

	private void OnDestroy() {
		@AudioManager = null;
		@SceneManager = null;
	}
}
