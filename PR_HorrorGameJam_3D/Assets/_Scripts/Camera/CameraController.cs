using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] private CinemachineVirtualCamera idleCamera;
	[SerializeField] private CinemachineVirtualCamera tableCamera;
	[SerializeField] private CinemachineFreeLook voodooVictimCamera;
	[SerializeField] private CinemachineFreeLook voodooOracleCamera;

	// Start is called before the first frame update
	void Start() {
		ServiceLocator.GameManager.OnChangedState.AddListener(OnChangedState);
	}

	private void OnChangedState(GameStateManager gameManager) {
		switch (gameManager.CurrentState) {
			case GameStateManager.GameStates.ORACLE_ROLL:
			case GameStateManager.GameStates.PLAYER_ROLL:
				SwitchToTableCamera();
				break;
			case GameStateManager.GameStates.VOODOO_SELECT:
				if (gameManager.Score.getResult() == "Player_Win") {
					SwitchToVoodooOracleCamera();
				} else if (gameManager.Score.getResult() == "Player_Lose") {
					SwitchToVoodooVictimCamera();
				}
				break;
			default:
				SwitchToIdleCamera();
				break;
		}
	}

	public void SwitchToIdleCamera() {
		idleCamera.Priority = 10;
		tableCamera.Priority = 1;
		voodooVictimCamera.Priority = 1;
		voodooOracleCamera.Priority = 1;
	}

	public void SwitchToTableCamera() {
		idleCamera.Priority = 1;
		tableCamera.Priority = 10;
		voodooVictimCamera.Priority = 1;
		voodooOracleCamera.Priority = 1;
	}

	public void SwitchToVoodooVictimCamera() {
		idleCamera.Priority = 1;
		tableCamera.Priority = 1;
		voodooVictimCamera.Priority = 10;
		voodooOracleCamera.Priority = 1;
	}

	public void SwitchToVoodooOracleCamera() {
		idleCamera.Priority = 1;
		tableCamera.Priority = 1;
		voodooVictimCamera.Priority = 1;
		voodooOracleCamera.Priority = 10;
	}
}
