using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static Action<PlayerMotor> OnPlayerSpawn;
    public static Action OnGameOver;
    private PlayerMotor m_currentPlayer;

    [Header("LevelManager")]
    [SerializeField] private Transform levelStartPoint;
    [SerializeField] private GameObject playerPrefabs;

    public Transform LevelStartPoint => levelStartPoint;

    [Header("Panel")]
	public GameObject panelGameOver;
	public GameObject panelRevive;
	[SerializeField] private Slider timeSlider;
	[SerializeField] private float timeRevive;
    [SerializeField] private bool stopTimer;

    public float restartDelay;
    public bool revived;

    /// <summary>
    /// Spawn player when start game
    /// </summary>
	private void Awake()
	{
        timeSlider.maxValue = timeRevive;
        timeSlider.value = timeRevive;
        SpawnPlayer(playerPrefabs);
    }

	private void Start()
	{
        OnPlayerSpawn?.Invoke(m_currentPlayer);
        stopTimer = false;
        revived = false;
    }

	private void Update()
	{
		if (panelRevive.activeInHierarchy)
		{
           timeRevive-= Time.deltaTime;
            if (timeRevive <= 0)
            {
                SoundManager.Instance.PlaySound(AudioLibrary.Instance.Timeralarm);
                stopTimer = true;
                Invoke(nameof(GameOver), 1f);
            }
            else
            {
                stopTimer = false;
                timeSlider.value = timeRevive;
            }
        }
    }


    /// <summary>
    /// Spawn player when start game
    /// </summary>
    private void SpawnPlayer(GameObject player)
	{
		if (player != null)
		{
            m_currentPlayer = Instantiate(playerPrefabs, levelStartPoint.position, Quaternion.identity).GetComponent<PlayerMotor>();
            m_currentPlayer.GetComponent<PlayerHealth>().ResetLifes();
		}
	}


    #region Revive
    /// <summary>
    /// You Want To Revive
    /// </summary>
    private void YouWantToRevive()
	{
		if (revived)
		{
            Invoke(nameof(GameOver), 1f);
        }
		else
		{
            panelRevive.SetActive(true);
        }
    }

    public void GameOver()
    {
        SoundManager.Instance.PlaySound(AudioLibrary.Instance.Gameover);
        panelRevive.SetActive(false);
        panelGameOver.SetActive(true);
    }
  
    /// <summary>
    /// Revive player when player death
    /// </summary>
    private void RevivePlayer()
    {
        if (m_currentPlayer != null)
		{
            m_currentPlayer.gameObject.SetActive(true);
           // m_currentPlayer.SpawnPlayer(levelStartPoint);
            m_currentPlayer.transform.position = GameManager.Instance.lastCheckPointPos;
            m_currentPlayer.GetComponent<PlayerHealth>().ResetLifes();
            m_currentPlayer.GetComponent<PlayerHealth>().Revive();
        }
        revived = true;
    }

    public void OnButtonReviveClicked()
    {
        panelRevive.SetActive(false);
        Invoke(nameof(RevivePlayer), restartDelay);
    }
    #endregion

    public void PlayerDeath(PlayerMotor player)
    {
        m_currentPlayer.gameObject.SetActive(false);
        DataManager.instance.SaveData(GameManager.instance.data);
        YouWantToRevive();
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
