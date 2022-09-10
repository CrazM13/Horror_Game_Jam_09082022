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
				// TODO Oracle Roll Anim
				break;
			case GameStateManager.GameStates.VOODOO_PAIN:
				//if (SCORE is WIN) {
				//	TODO Oracle Pain Anim
				//} else {
				//	TODO Oracle Win Anim
				//}
				break;
		}
	}
}
