using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
	public GameObject panelAbout;

	public void OnButtonStartClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		SceneManager.LoadScene(1);
	}

	public void OnButtonAboutClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		if (!panelAbout.activeInHierarchy)
		{
			panelAbout.SetActive(true);
		}
	}

	public void OnButtonCloseAboutClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		if (panelAbout.activeInHierarchy)
		{
			panelAbout.SetActive(false);
		}
	}

	public void OnButtonQuitClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		Application.Quit();
	}
}
