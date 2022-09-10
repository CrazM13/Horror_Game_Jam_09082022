using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour {

	[SerializeField] private Score_Tracker score;
	[SerializeField] private HighLowMenu highLowMenu;
	[SerializeField] private Transform victim;

	[SerializeField] private Dice[] playerDice;
	[SerializeField] private Dice[] oracleDice;

	int oracleScore = 0;

	public Score_Tracker Score => score;

	private bool IsHigher;

	private int playerWins;
	private int oracleWins;

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

		if (Input.GetKeyDown(KeyCode.P)) {
			Clock.IsPaused = !Clock.IsPaused;
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
				ServiceLocator.AudioManager.PlayRandomLocal(victim.position, "Victim");

				if (oracleWins >= 3) {
					ServiceLocator.SceneManager.LoadSceneByName("Lose Scene");
				} else if (playerWins >= 3) {
					ServiceLocator.SceneManager.LoadSceneByName("Win Scene");
				}

				foreach(Dice dice in oracleDice) {
					dice.RollDice();
				}
				
				break;
			case GameStates.ORACLE_INSTRUCT:
				ServiceLocator.AudioManager.PlayRandomGlobal("High Low");

				oracleScore = 0;
				foreach (Dice dice in oracleDice) {
					oracleScore += dice.diceValue;
				}

				ScheduleStateChange(GameStates.PLAYER_HIGH_LOW, 5f);
				break;
			case GameStates.PLAYER_HIGH_LOW:
				highLowMenu.IsShowing = true;
				break;
			case GameStates.PLAYER_ROLL:
				foreach (Dice dice in playerDice) {
					dice.RollDice();
				}

				break;
			case GameStates.SCORE:

				int playerScore = 0;
				foreach (Dice dice in playerDice) {
					playerScore += dice.diceValue;
				}

				Score.setDice(playerScore, oracleScore, IsHigher);

				ScheduleStateChange(GameStates.VOODOO_SELECT, 2f);
				break;
			case GameStates.VOODOO_SELECT:
				if (Score.getResult() == "Tie") {
					CurrentState = GameStates.PLAYER_ROLL;
				}
				break;
			case GameStates.VOODOO_PAIN:
				if (Score.getResult() == "Player_Win") {
					ServiceLocator.AudioManager.PlayRandomGlobal("Oracle Hurt");
					playerWins++;
				} else if (Score.getResult() == "Player_Lose") {
					ServiceLocator.AudioManager.PlayRandomGlobal("Player Lose");
					ServiceLocator.AudioManager.PlayRandomLocal(victim.position, "Victim");
					oracleWins++;
				}

				ScheduleStateChange(GameStates.ORACLE_ROLL, 5f);
				break;
		}
	}

	private void UpdateStates() {
		switch (CurrentState) {
			case GameStates.PLAYER_ROLL:
				bool isDone = true;
				foreach (Dice dice in playerDice) {
					if (!dice.IsDone) isDone = false;
				}
				if (isDone && timeUntilChangeState < 0) ScheduleStateChange(GameStates.SCORE, 2f);
				break;
			case GameStates.ORACLE_ROLL:
				bool isOracleDone = true;
				foreach (Dice dice in oracleDice) {
					if (!dice.IsDone) isOracleDone = false;
				}
				if (isOracleDone && timeUntilChangeState < 0) ScheduleStateChange(GameStates.ORACLE_INSTRUCT, 2f);
				break;
		}
	}

	public void SetHighLow(bool isHigher) {
		this.IsHigher = isHigher;
	}
}
