using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Invoke(nameof(ShowLevelCompleteMenu), 2f);
		}
	}

	public void ShowLevelCompleteMenu()
	{
		GameManager.Instance.LevelComplete();
	}
}