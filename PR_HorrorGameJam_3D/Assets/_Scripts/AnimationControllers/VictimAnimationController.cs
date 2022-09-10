using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimAnimationController : MonoBehaviour {
	[SerializeField] private new Animator animation;

	void Start() {
		ServiceLocator.GameManager.OnChangedState.AddListener(OnChangedState);
	}

	private void OnChangedState(GameStateManager gameManager) {
		switch (gameManager.CurrentState) {
			case GameStateManager.GameStates.VOODOO_PAIN:
				if (gameManager.Score.getResult() == "Player_Lose") {
					animation.SetTrigger("Hurt");
				}
				break;
		}
	}
}
