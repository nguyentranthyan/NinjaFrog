using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : ObstacleComponent
{
	//[SerializeField] private Animator animator;

	private void Collision(Collider2D objectCollided)
	{
		if (objectCollided.GetComponent<EnemyDamage>() != null)
		{
			objectCollided.gameObject.SetActive(false);
			GameManager.Instance.UpdateScore(GameManager.Item.Enemy);
			SoundManager.Instance.PlaySound(AudioLibrary.Instance.EnemyExplode);
		}
	}

	private void CollisionBulet(Collider2D objectCollided)
	{
		if (objectCollided.GetComponent<PlayerMotor>() != null)
		{
			Camera2DShake.Instance.Shake();
			objectCollided.GetComponent<PlayerHealth>().LoseLifes();
			SoundManager.Instance.PlaySound(AudioLibrary.Instance.ProjectileCollisionClip);
		}
		else
		{
			SoundManager.Instance.PlaySound(AudioLibrary.Instance.ProjectileCollisionClip);
		}
	}

	private void OnEnable()
	{
		ProjectTilePooler.OnProjectileCollision += Collision;
		BulletObjectPool.OnBulletCollision += CollisionBulet;
	}

	private void OnDisable()
	{
		ProjectTilePooler.OnProjectileCollision -= Collision;
		BulletObjectPool.OnBulletCollision -= CollisionBulet;
	}
}
