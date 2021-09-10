using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
	public void OnButtonBackClicked(string nameScene)
	{
		SceneManager.LoadScene(nameScene);
	}
	public void OnButtonSaveClicked()
	{
		DataManager.instance.SaveData();
	}
}
