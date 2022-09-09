using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : HidableMenu {

	private void Awake() {
		Clock.OnPause.AddListener(Show);
		Clock.OnUnpause.AddListener(Hide);
	}

	private void OnDestroy() {
		Clock.OnPause.RemoveListener(Show);
		Clock.OnUnpause.RemoveListener(Hide);
	}

}
