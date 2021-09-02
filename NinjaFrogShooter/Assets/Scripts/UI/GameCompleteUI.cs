using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteUI : MonoBehaviour
{
	public void OnButtonHomeClicked()
	{
		SceneManager.LoadScene(0);
	}
}
