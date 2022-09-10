using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLowMenu : HidableMenu {

	

	public void SetHigh() {
		ServiceLocator.GameManager.SetHighLow(true);
		IsShowing = false;
		ServiceLocator.GameManager.CurrentState = GameStateManager.GameStates.PLAYER_ROLL;
	}

	public void SetLow() {
		ServiceLocator.GameManager.SetHighLow(false);
		IsShowing = false;
		ServiceLocator.GameManager.CurrentState = GameStateManager.GameStates.PLAYER_ROLL;
	}

}
