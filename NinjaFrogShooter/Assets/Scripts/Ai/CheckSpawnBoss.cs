using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpawnBoss : MonoBehaviour
{
	[SerializeField] private GameObject bossEnemy;

	void Start()
	{
		bossEnemy.gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			bossEnemy.gameObject.SetActive(true);
		}
	}
}
