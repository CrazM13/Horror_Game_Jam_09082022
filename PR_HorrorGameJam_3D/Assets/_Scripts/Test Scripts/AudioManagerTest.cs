using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTest : MonoBehaviour {

	private void Start() {
		Debug.LogWarning("WARNING! Debug Audio Management Test is active on object! \nJ - \"Music\"\nK - \"Sound FX\"\nL - \"Voice\"");
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.J)) ServiceLocator.AudioManager.PlayGlobal("Music", "Creepy Mix", true);
		if (Input.GetKeyDown(KeyCode.K)) ServiceLocator.AudioManager.PlayRandomLocal(Camera.main.transform.position, "SoundFX Test");
		if (Input.GetKeyDown(KeyCode.L)) ServiceLocator.AudioManager.PlayRandomLocal(Camera.main.transform.position, "Voice Test");
	}
}
