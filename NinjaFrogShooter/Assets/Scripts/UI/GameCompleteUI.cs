using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteUI : MonoBehaviour
{
	public void OnButtonHomeClicked()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.Btnpop);
		SceneManager.LoadScene(0);
	}
}
