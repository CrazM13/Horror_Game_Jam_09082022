using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionMenu : MonoBehaviour {

	void Update() {
		if (Input.anyKeyDown) ServiceLocator.SceneManager.LoadSceneByName("Game Scene");
	}
}
