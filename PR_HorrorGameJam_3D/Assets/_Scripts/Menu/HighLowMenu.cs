using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighLowMenu : HidableMenu {

	[SerializeField] private TMP_Text oracleText;

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

	public void SetOracleRoll(int value) {
		oracleText.text = value.ToString();
	}

}
