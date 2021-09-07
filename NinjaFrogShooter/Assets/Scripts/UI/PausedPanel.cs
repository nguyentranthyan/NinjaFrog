using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedPanel : MonoBehaviour
{
	[SerializeField] private GameObject m_PausePanel;
	[SerializeField] private GameObject m_InstructionPanel;
	[SerializeField] private GameObject m_SettingSoundPanel;

	private void Start()
	{
		m_PausePanel.SetActive(false);
		m_InstructionPanel.SetActive(false);
	}

	public void OnButtonPausedClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		if (!m_PausePanel.activeInHierarchy)
		{
			m_PausePanel.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			m_PausePanel.SetActive(false);
			m_InstructionPanel.SetActive(false);
			m_SettingSoundPanel.SetActive(false);
			Time.timeScale = 1f;
		}	
	}

	public void OnButtonHomeClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

	public void OnButtonAudioClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		if (!m_SettingSoundPanel.activeInHierarchy)
		{
			m_SettingSoundPanel.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			m_InstructionPanel.SetActive(false);
			m_PausePanel.SetActive(false);
			m_SettingSoundPanel.SetActive(false);
			Time.timeScale = 1f;
		}
	}

	public void OnButtonInstructionCliked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		if (!m_InstructionPanel.activeInHierarchy)
		{
			m_InstructionPanel.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			m_InstructionPanel.SetActive(false);
			m_PausePanel.SetActive(false);
			m_SettingSoundPanel.SetActive(false);
			Time.timeScale = 1f;
		}
	}
}
