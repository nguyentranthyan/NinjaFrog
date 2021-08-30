using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : ObstacleComponent
{
	[SerializeField] private Animator animator;

	private void Collision(Collider2D objectCollided)
	{
		if (objectCollided.GetComponent<EnemyDamage>() != null)
		{
			objectCollided.gameObject.SetActive(false);
		}
		if (objectCollided.GetComponent<PlayerMotor>() != null)
		{
			Camera2DShake.Instance.Shake();
			objectCollided.GetComponent<PlayerHealth>().LoseLifes();
		}
	}

	private void OnEnable()
	{
		ProjectTilePooler.OnProjectileCollision += Collision;
		BulletObjectPool.OnBulletCollision += Collision;
	}

	private void OnDisable()
	{
		ProjectTilePooler.OnProjectileCollision -= Collision;
		BulletObjectPool.OnBulletCollision -= Collision;
	}
}
