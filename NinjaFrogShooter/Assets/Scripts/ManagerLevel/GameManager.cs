using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public enum GameStates
	{
		GameStart,
		LevelLoad,
		LevelComleted,
		GameOver
	}

	public GameStates GameState { get; set; }
	public int CurrentLevelCompleted { get; set; }
	public static Action OnLoadNextLevel;

	protected override void Awake()
	{
		base.Awake();
		GameState = GameStates.GameStart;
	}

	private void LevelComplete(int levelIndex)
	{
		CurrentLevelCompleted = levelIndex;
		GameState = GameStates.LevelComleted;
		OnLoadNextLevel?.Invoke();
	}

	private void OnEnable()
	{
		CoinManager.OnLevelComplete += LevelComplete;
	}

	private void OnDisable()
	{
		CoinManager.OnLevelComplete -= LevelComplete;
	}
}
