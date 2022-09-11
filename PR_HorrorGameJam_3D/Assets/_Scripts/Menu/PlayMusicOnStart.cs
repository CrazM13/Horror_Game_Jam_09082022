using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour {
	


	void Start() {
		ServiceLocator.AudioManager.PlayRandomGlobal("Music", true);
	}

}
