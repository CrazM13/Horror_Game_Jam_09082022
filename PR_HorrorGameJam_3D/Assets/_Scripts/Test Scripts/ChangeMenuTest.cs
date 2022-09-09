using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuTest : MonoBehaviour {

	private void Start() {
		Debug.LogWarning("WARNING! Debug Scene Management Test is active on object! \nU - \"Main Menu\"\nI - \"Win Scene\"\nO - \"Lose Scene\"\nP - \"Pause\"");
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.U)) ServiceLocator.SceneManager.LoadSceneByName("Main Menu");
		if (Input.GetKeyDown(KeyCode.I)) ServiceLocator.SceneManager.LoadSceneByName("Win Scene");
		if (Input.GetKeyDown(KeyCode.O)) ServiceLocator.SceneManager.LoadSceneByName("Lose Scene");
		if (Input.GetKeyDown(KeyCode.P)) Clock.IsPaused = !Clock.IsPaused;
	}
}
