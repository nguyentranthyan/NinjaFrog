using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Text coinText;
	[SerializeField] private int levelIndex;
	private int coinCollect;
	public static Action<int> OnLevelComplete;

	[SerializeField] private Text txtComplete;

	private void Start()
	{
		txtComplete.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
        AllCoinCollected();
		coinCollect = transform.childCount;
		coinText.text = coinCollect.ToString();
    }

    public void AllCoinCollected()
	{
		if (transform.childCount == 0)
		{
			txtComplete.gameObject.SetActive(true);
			Invoke(nameof(ChangeScene), 2f);
		}
	}

	private void ChangeScene()
	{
		OnLevelComplete?.Invoke(levelIndex);
	}
}
