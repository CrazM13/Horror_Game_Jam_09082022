using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
	[SerializeField] private new Animator animation;

	void Start() {
		ServiceLocator.GameManager.OnChangedState.AddListener(OnChangedState);
	}

	private void OnChangedState(GameStateManager gameManager) {
		switch (gameManager.CurrentState) {
			case GameStateManager.GameStates.PLAYER_ROLL:
				animation.SetTrigger("Roll");
				break;
		}
	}
}
