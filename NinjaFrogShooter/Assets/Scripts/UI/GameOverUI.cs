using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
	public void OnButtonRestartClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		SceneManager.LoadScene(1);
	}
	public void OnButtonQuitClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		Application.Quit();
	}
}
