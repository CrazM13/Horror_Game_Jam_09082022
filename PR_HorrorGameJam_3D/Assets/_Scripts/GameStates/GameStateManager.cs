using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour {

	[SerializeField] private Score_Tracker score;
	[SerializeField] private HighLowMenu highLowMenu;

	public Score_Tracker Score => score;

	private bool IsHigher;

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
			OnChangeState();
		}
	}

	public UnityEvent<GameStateManager> OnChangedState { get; private set; } = new UnityEvent<GameStateManager>();

	void Start() {
		CurrentState = GameStates.INTRODUCTION;
	}

	void Update() {

		if (timeUntilChangeState > 0) {
			timeUntilChangeState -= Clock.DeltaTime;
			if (timeUntilChangeState <= 0) {
				CurrentState = newGameState;
			}
		}

		UpdateStates();
	}

	public void ScheduleStateChange(GameStates newState, float timeInSeconds) {
		this.newGameState = newState;
		this.timeUntilChangeState = timeInSeconds;
	}

	private void OnChangeState() {
		switch (CurrentState) {
			case GameStates.INTRODUCTION:
				ServiceLocator.AudioManager.PlayRandomGlobal("Turn Start");
				ScheduleStateChange(GameStates.ORACLE_ROLL, 5f);
				break;
			case GameStates.ORACLE_ROLL:
				if (Score.GetOracleWins() >= 3) {
					ServiceLocator.SceneManager.LoadSceneByName("Lose Scene");
				} else if (Score.GetPlayerWins() >= 3) {
					ServiceLocator.SceneManager.LoadSceneByName("Win Scene");
				}

				// TODO

				ScheduleStateChange(GameStates.ORACLE_INSTRUCT, 10f);
				break;
			case GameStates.ORACLE_INSTRUCT:
				ServiceLocator.AudioManager.PlayRandomGlobal("High Low");
				ScheduleStateChange(GameStates.PLAYER_HIGH_LOW, 5f);
				break;
			case GameStates.PLAYER_HIGH_LOW:
				highLowMenu.IsShowing = true;
				break;
			case GameStates.PLAYER_ROLL:

				// TODO

				ScheduleStateChange(GameStates.SCORE, 5f);
				break;
			case GameStates.SCORE:

				Score.setDice(Random.Range(1, 7), Random.Range(1, 7), IsHigher);

				ScheduleStateChange(GameStates.VOODOO_SELECT, 5f);
				break;
			case GameStates.VOODOO_SELECT:
				// TODO
				break;
			case GameStates.VOODOO_PAIN:
				if (Score.getResult() == "Player_Win") {
					ServiceLocator.AudioManager.PlayRandomGlobal("Oracle Hurt");
				} else if (Score.getResult() == "Player_Lose") {
					ServiceLocator.AudioManager.PlayRandomGlobal("Player Lose");
				} else {
					CurrentState = GameStates.PLAYER_ROLL;
				}

				ScheduleStateChange(GameStates.ORACLE_ROLL, 5f);
				break;
		}
	}

	private void UpdateStates() {

	}

	public void SetHighLow(bool isHigher) {
		this.IsHigher = isHigher;
	}
}
