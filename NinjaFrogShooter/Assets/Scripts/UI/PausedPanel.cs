using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedPanel : MonoBehaviour
{
	[SerializeField] private GameObject m_PausePanel;
	[SerializeField] private GameObject m_InstructionPanel;

	private void Start()
	{
		m_PausePanel.SetActive(false);
		m_InstructionPanel.SetActive(false);
	}

	public void OnButtonPausedClicked()
	{
		if (!m_PausePanel.activeInHierarchy)
		{
			m_PausePanel.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			m_PausePanel.SetActive(false);
			Time.timeScale = 1f;
		}	
	}

	public void OnButtonHomeClicked()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

	public void OnButtonInstructionCliked()
	{
		if (!m_InstructionPanel.activeInHierarchy)
		{
			m_InstructionPanel.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			m_InstructionPanel.SetActive(false);
			m_PausePanel.SetActive(false);
			Time.timeScale = 1f;
		}
	}
}
