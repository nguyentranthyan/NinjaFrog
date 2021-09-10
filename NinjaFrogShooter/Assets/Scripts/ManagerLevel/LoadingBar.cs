using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : Singleton<LoadingBar>
{
    public GameObject panelLoading;

	public void ShowLoading()
	{
		panelLoading.SetActive(true);
	}
}
