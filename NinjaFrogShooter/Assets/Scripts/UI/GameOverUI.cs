using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
	public void OnButtonRestartClicked()
	{
		SceneManager.LoadScene(1);
	}
	public void OnButtonQuitClicked()
	{
		Application.Quit();
	}
}
