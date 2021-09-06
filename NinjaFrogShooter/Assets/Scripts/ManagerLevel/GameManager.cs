using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO; //working with files
using System.Runtime.Serialization.Formatters.Binary;//RSFB help Serialization

public class GameManager : Singleton<GameManager>
{
	public enum GameStates
	{
		GameStart,
		LevelLoad,
		LevelComleted,
		GameOver
	}

	#region HUD
	[SerializeField] private UI ui;
	private float m_currentJetpackFuel;
	private float m_jetpackFuel;
	#endregion

	#region Level
	[Header("CheckPoint")]
	public Vector2 lastCheckPointPos;

	[Header("Level")]
	public static Action OnLoadNextLevel;
	public GameStates GameState { get; set; }
	public int CurrentLevelCompleted { get; set; }
	#endregion

	#region data
	[Header("Data")]
	[SerializeField] private int coinValue;
	[SerializeField] private float maxTime; //max time allowed to complete the level

	public GameData data; //work with gameData inspector
	string dataFilePath; //path the data file 
	BinaryFormatter bf; //Saving and loading to binary file
	float timeLeft; //time left before the timer goes off
	private PlayerMotor player;
	#endregion

	protected override void Awake()
	{
		base.Awake();
		bf = new BinaryFormatter();
		dataFilePath = Application.persistentDataPath + "/game.dat";
		Debug.Log(dataFilePath);

		GameState = GameStates.GameStart;
		timeLeft = maxTime;
		InitialData();
	}

	private void LevelComplete(int levelIndex)
	{
		CurrentLevelCompleted = levelIndex;
		GameState = GameStates.LevelComleted;
		OnLoadNextLevel?.Invoke();
	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ResetData();
		}
		InternalJectpackUpdate();

		if (timeLeft > 0)
			UpdateTime();
	}

	void InitialData()
	{
		data.coinCount = 0;
		data.keyfound[0] = false;
		data.keyfound[1] = false;
		data.keyfound[2] = false;
		data.score = 0;
		data.isFirst = false;
	}

	/// <summary>
	/// Controller HUD:fuel, heart, ammo
	/// </summary>
	/// <param name="currentFuel"></param>
	/// <param name="maxFuel"></param>
	#region HUD Controller
	public void UpdateFuel(float currentFuel, float maxFuel)
	{
		m_currentJetpackFuel = currentFuel;
		m_jetpackFuel = maxFuel;
	}

	private void InternalJectpackUpdate()
	{
		ui.fuelImage.fillAmount = Mathf.Lerp(ui.fuelImage.fillAmount, m_currentJetpackFuel / m_jetpackFuel,
			Time.deltaTime * 10f);
		ui.fuelText.text = "Fuel: " + (int)m_currentJetpackFuel + " %";
	}

	private void OnPlayerLives(int currentlife)
	{
		for (int i = 0; i < ui.playerLifes.Length; i++)
		{
			if (i < currentlife)
			{
				ui.playerLifes[i].gameObject.SetActive(true);
			}
			else
			{
				ui.playerLifes[i].gameObject.SetActive(false);
			}
		}
	}

	private void OnPlayerGun(int m_projectileRemaining)
	{
		ui.ammoText.text = " " + m_projectileRemaining + "v";
	}

	#endregion

	/// <summary>
	/// Data game using  Binary Formatter
	/// </summary>
	#region data
	public void UpdateCoin()
	{
		data.coinCount += 1;
		ui.txtCoinCount.text = "x " + data.coinCount;
		UpdateScore(coinValue);
	}

	public void UpdateScore(int value)
	{
		data.score += value;
		ui.txtScore.text = "Score: " + data.score;
	}

	public void UpdateTime()
	{
		timeLeft -= Time.deltaTime;
		ui.txtTime.text = "Time: " + (int) timeLeft;

		if (timeLeft <= 0)
		{
			ui.txtTime.text = "Time: 0";
			LevelManager.Instance.PlayerDeath(player);
		}
	}

	public void UpdateKey(int keyNumber)
	{
		data.keyfound[keyNumber] = true;
		switch (keyNumber)
		{
			case 0:
				Debug.Log("0");
				ui.imageKey0.sprite = ui.keyFull0;
				break;
			case 1:
				ui.imageKey1.sprite = ui.keyFull1;
				break;
			case 2:
				ui.imageKey2.sprite = ui.keyFull2;
				break;
		}
	}

	public void SaveData()
	{
		FileStream fs = new FileStream(dataFilePath, FileMode.Create);
		bf.Serialize(fs, data);
		fs.Close();
	}

	public void LoadData()
	{
		if (File.Exists(dataFilePath))
		{
			FileStream fs = new FileStream(dataFilePath, FileMode.Open);
			data = (GameData)bf.Deserialize(fs);
			ui.txtCoinCount.text = "x " + data.coinCount;
			ui.txtScore.text = "Score: " + data.score;
			Debug.Log("Num of coins " + data.coinCount);
			fs.Close();
		}
	}

	public void ResetData()
	{
		FileStream fs = new FileStream(dataFilePath, FileMode.Create);
		//Reset all data Items
		data.coinCount = 0;
		data.score = 0;

		for(int keyNumber = 0; keyNumber <= 2; keyNumber++)
		{
			data.keyfound[keyNumber] = false;
		}

		bf.Serialize(fs, data);
		ui.txtCoinCount.text = "x 0";
		ui.txtScore.text = "Score: 0";
		Debug.Log("Data Reset");
		fs.Close();
	}

	#endregion
	private void OnEnable()
	{
		PlayerHealth.OnLifesChange += OnPlayerLives;
		Gun.OnAmmoChange += OnPlayerGun;
		CoinManager.OnLevelComplete += LevelComplete;
		Debug.Log("Data Loaded");
		LoadData();
	}

	private void OnDisable()
	{
		PlayerHealth.OnLifesChange -= OnPlayerLives;
		Gun.OnAmmoChange -= OnPlayerGun;
		CoinManager.OnLevelComplete -= LevelComplete;
		Debug.Log("Data Save");
		SaveData();
	}
}
