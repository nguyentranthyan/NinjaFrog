using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action<PlayerMotor> OnPlayerSpawn;

    [Header("LevelManager")]
    [SerializeField] private Transform levelStartPoint;
    [SerializeField] private GameObject playerPrefabs;
    

    private PlayerMotor m_currentPlayer;

	private void Awake()
	{
        SpawnPlayer(playerPrefabs);
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
            RevivePlayer();
        }
	}

    private void SpawnPlayer(GameObject player)
	{
		if (player != null)
		{
            m_currentPlayer = Instantiate(playerPrefabs, levelStartPoint.position, Quaternion.identity).GetComponent<PlayerMotor>();
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
            m_currentPlayer.SpawnPlayer(levelStartPoint);
            m_currentPlayer.GetComponent<PlayerHealth>().ResetLifes();
            m_currentPlayer.GetComponent<PlayerHealth>().Revive();
        }
    }

    private void PlayerDeath(PlayerMotor player)
    {
        m_currentPlayer.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerHealth.OnDeath += PlayerDeath;
    }

	private void OnDisable()
    {
        PlayerHealth.OnDeath -= PlayerDeath;
    }
}
