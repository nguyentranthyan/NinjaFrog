using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
	public GameObject panelAbout;

	public void OnButtonStartClicked()
	{
		SceneManager.LoadScene(1);
	}

	public void OnButtonAboutClicked()
	{
		if (!panelAbout.activeInHierarchy)
		{
			panelAbout.SetActive(true);
		}
	}

	public void OnButtonCloseAboutClicked()
	{
		if (panelAbout.activeInHierarchy)
		{
			panelAbout.SetActive(false);
		}
	}

	public void OnButtonQuitClicked()
	{
		Application.Quit();
	}
}
