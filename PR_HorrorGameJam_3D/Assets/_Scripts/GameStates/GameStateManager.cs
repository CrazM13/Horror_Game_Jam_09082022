using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour {

	public enum GameStates {
		INTRODUCTION = 0,
		ORACLE_ROLL = 1,
		ORACLE_INSTRUCT = 2,
		PLAYER_HIGH_LOW = 3,
		PLAYER_ROLL = 4,
		SCORE = 5,
		VOODOO_SELECT = 6,
		VOODOO_PAIN = 7
	}

	private GameStates gameState;

	private float timeUntilChangeState = -1;
	private GameStates newGameState;

	public GameStates CurrentState {
		get => gameState;
		set {
			gameState = value;
			OnChangedState.Invoke(this);
		}
	}

	public UnityEvent<GameStateManager> OnChangedState { get; private set; } = new UnityEvent<GameStateManager>();

	void Awake() {
		CurrentState = GameStates.INTRODUCTION;
	}

	void Update() {

		if (timeUntilChangeState > 0) {
			timeUntilChangeState -= Clock.DeltaTime;
			if (timeUntilChangeState <= 0) {
				CurrentState = newGameState;
			}
		}

		switch(CurrentState) {
			case GameStates.INTRODUCTION:
				//ServiceLocator.AudioManager.PlayGlobal("Voice", "Introduction");
				ScheduleStateChange(GameStates.ORACLE_ROLL, 5f);
				break;
			case GameStates.ORACLE_ROLL:
				// TODO
				ScheduleStateChange(GameStates.ORACLE_INSTRUCT, 10f);
				break;
			case GameStates.ORACLE_INSTRUCT:
				//ServiceLocator.AudioManager.PlayRandomGlobal("Oracle HighLow Voice");
				ScheduleStateChange(GameStates.ORACLE_INSTRUCT, 5f);
				break;
			case GameStates.PLAYER_HIGH_LOW:
				// TODO
				break;
			case GameStates.PLAYER_ROLL:
				// TODO
				ScheduleStateChange(GameStates.SCORE, 5f);
				break;
			case GameStates.SCORE:
				// TODO
				ScheduleStateChange(GameStates.VOODOO_SELECT, 5f);
				break;
			case GameStates.VOODOO_SELECT:
				// TODO
				break;
			case GameStates.VOODOO_PAIN:
				//if (SCORE is WIN) {
				//	ServiceLocator.AudioManager.PlayRandomGlobal("Oracle Pain Voice");
				//} else {
				//	ServiceLocator.AudioManager.PlayRandomGlobal("Victim Pain Voice");
				//}
				ScheduleStateChange(GameStates.ORACLE_ROLL, 5f);
				break;
		}
	}

	public void ScheduleStateChange(GameStates newState, float timeInSeconds) {
		this.newGameState = newState;
		this.timeUntilChangeState = timeInSeconds;
	}
}
