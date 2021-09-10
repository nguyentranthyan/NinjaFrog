using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO; //working with files
using System.Runtime.Serialization.Formatters.Binary;//RSFB help Serialization

public class GameManager : Singleton<GameManager>
{
	public enum Item
	{
		Coin, Enemy
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
	//public static Action OnLoadNextLevel;
	public GameObject levelCompleteMenu;
	private LevelManager levelManager;
	#endregion

	#region data
	[Header("Data")]
	[SerializeField] private int coinValue;
	[SerializeField] private int enemyValue;
	[SerializeField] private float maxTime; //max time allowed to complete the level

	public GameData data; //work with gameData inspector
	string dataFilePath; //path the data file 
	BinaryFormatter bf; //Saving and loading to binary file
	float timeLeft; //time left before the timer goes off
	#endregion

	protected override void Awake()
	{
		base.Awake();
		bf = new BinaryFormatter();
		dataFilePath = Application.persistentDataPath + "/game.dat";
		Debug.Log(dataFilePath);
	}

	private void Start()
	{
		DataManager.instance.RefreshData();
		data = DataManager.instance.data;
		RefreshUI();
		timeLeft = maxTime;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		lastCheckPointPos = levelManager.LevelStartPoint.position;
		InitialData();
	}

	// Update is called once per frame
	void Update()
	{
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
		UpdateScore(Item.Coin);
	}

	public void UpdateScore(Item item)
	{
		int itemValue = 0;
		switch (item)
		{
			case Item.Coin:
				itemValue = coinValue;
				break;
			case Item.Enemy:
				itemValue = enemyValue;
				break;
		}
		data.score += itemValue;
		ui.txtScore.text = "Score: " + data.score;
	}

	public void UpdateTime()
	{
		timeLeft -= Time.deltaTime;
		ui.txtTime.text = "Time: " + (int) timeLeft;

		if (timeLeft <= 0)
		{
			SoundManager.Instance.PlaySound(AudioLibrary.Instance.Timeralarm);
			ui.txtTime.text = "Time: 0";
			DataManager.instance.SaveData(data);
			levelManager.GameOver();
		}
	}

	public void UpdateKey(int keyNumber)
	{
		data.keyfound[keyNumber] = true;
		switch (keyNumber)
		{
			case 0:
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

	public void RefreshUI()
	{
		ui.txtCoinCount.text = "x " + data.coinCount;
		ui.txtScore.text = "Score: " + data.score;
	}
	#endregion

	#region Level
	public int GetScore()
	{
		return data.score;
	}

	public void SetStarsAwarded(int levelNumber, int numOfStar)
	{
		data.levelDatas[levelNumber].starsAwarded = numOfStar;
	}
	
	public void UnlockLevel(int levelNumber)
	{
		data.levelDatas[levelNumber].isUnlocked = true;
	}
	public void LevelComplete()
	{
		levelCompleteMenu.SetActive(true);
	}
	#endregion

	private void OnEnable()
	{
		PlayerHealth.OnLifesChange += OnPlayerLives;
		Gun.OnAmmoChange += OnPlayerGun;
		Debug.Log("Data Loaded");
		RefreshUI();
	}

	private void OnDisable()
	{
		PlayerHealth.OnLifesChange -= OnPlayerLives;
		Gun.OnAmmoChange -= OnPlayerGun;
		Debug.Log("Data Save");
		DataManager.instance.SaveData(data);
	}
}
