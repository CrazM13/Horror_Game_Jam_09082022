using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OracleAnimationController : MonoBehaviour {

	[SerializeField] private new Animator animation;

	void Start() {
		ServiceLocator.GameManager.OnChangedState.AddListener(OnChangedState);
	}

	private void OnChangedState(GameStateManager gameManager) {
		switch (gameManager.CurrentState) {
			case GameStateManager.GameStates.ORACLE_ROLL:
				animation.SetTrigger("Roll");
				break;
			case GameStateManager.GameStates.VOODOO_PAIN:
				if (gameManager.Score.getResult() == "Player_Win") {
					animation.SetTrigger("Hurt");
				} else if (gameManager.Score.getResult() == "Player_Lose") {
					animation.SetTrigger("Flair");
				}
				break;
		}
	}
}
