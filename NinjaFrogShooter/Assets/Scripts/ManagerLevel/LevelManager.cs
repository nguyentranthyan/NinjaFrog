using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public static Action<PlayerMotor> OnPlayerSpawn;
    private PlayerMotor m_currentPlayer;

    [Header("LevelManager")]
    [SerializeField] private Transform levelStartPoint;

    [SerializeField] private GameObject playerPrefabs;
    [SerializeField] private int m_countDeath = 3;
    [SerializeField] private string SceneGameOver;
    [SerializeField] private string SceneGameComplete;

    [Header("Levels")]
    [SerializeField] private int startingLevel = 0;
    [SerializeField] private Level[] levels;
    private int _nextLevel;

    private void Awake()
	{
        SpawnPlayer(playerPrefabs, levels[startingLevel].SpawnPoint);
    }
    private void Start()
    {
        // Call Event
        OnPlayerSpawn?.Invoke(m_currentPlayer);
    }

	private void Update()
	{
		if(Input.GetMouseButtonDown(1)){
            RevivePlayer();
        }
	}

	private void InitLevel(int levelIndex)
    {
        foreach (Level level in levels)
        {
            level.gameObject.SetActive(false);
        }

        levels[levelIndex].gameObject.SetActive(true);
    }
    private void SpawnPlayer(GameObject player, Transform spawnPoint)
	{
		if (player != null)
		{
            m_currentPlayer = Instantiate(playerPrefabs, spawnPoint.position, Quaternion.identity).GetComponent<PlayerMotor>();
            m_currentPlayer.GetComponent<PlayerHealth>().ResetLifes();
            //Call event
            OnPlayerSpawn?.Invoke(m_currentPlayer);
        }
	}

	private void RevivePlayer()
    {
		if (m_currentPlayer != null)
		{
            m_currentPlayer.gameObject.SetActive(true);
            //m_currentPlayer.SpawnPlayer(levelStartPoint);
            m_currentPlayer.transform.position = GameManager.Instance.lastCheckPointPos;
            m_currentPlayer.GetComponent<PlayerHealth>().ResetLifes();
            m_currentPlayer.GetComponent<PlayerHealth>().Revive();
        }
    }

    public void PlayerDeath(PlayerMotor player)
    {
        m_currentPlayer.gameObject.SetActive(false);
        StartCoroutine(IECheckRevivePlayer());
    }

	IEnumerator IECheckRevivePlayer()
	{
		yield return new WaitForSeconds(1f);
		m_countDeath -= 1;

		if (m_countDeath > 0)
		{
			RevivePlayer();
		}

		else if (m_countDeath <= 0)
		{
			SceneManager.LoadScene(SceneGameOver);
		}
	}
	private void MovePlayerToStartPosition(Transform newSpawnPoint)
    {
        if (m_currentPlayer!= null)
        {
            m_currentPlayer.transform.position = new Vector3(newSpawnPoint.position.x, newSpawnPoint.position.y, 0f);
        }
    }

    private void LoadLevel()
    {
        GameManager.Instance.GameState = GameManager.GameStates.LevelLoad;
        _nextLevel = GameManager.Instance.CurrentLevelCompleted + 1;
        if (_nextLevel > 3)
		{
            SceneManager.LoadScene(SceneGameComplete);
        }
		else
		{
            InitLevel(_nextLevel);
            MovePlayerToStartPosition(levels[_nextLevel].SpawnPoint);
        }
    }

    private void OnEnable()
    {
        PlayerHealth.OnDeath += PlayerDeath;
        GameManager.OnLoadNextLevel += LoadLevel;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDeath -= PlayerDeath;
        GameManager.OnLoadNextLevel -= LoadLevel;
    }
}
