using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurret : MonoBehaviour
{
	private float waitedTime;
	public float waitTimeToAttack = 3;
	[SerializeField] Animator animator;
	[SerializeField] private bool facingRight;


	[Header("Shooting")]
	[SerializeField] private Transform firePoint;

	///// <summary>
	///// Reference player
	///// </summary>
	//public PlayerMotor Target { get; set; }

	/// <summary>
	/// Reference Pooler
	/// </summary>
	public ObjectPool pooler;

	private void Update()
	{
		if (waitedTime <= 0)
		{
			waitedTime = waitTimeToAttack;
			animator.SetTrigger("Attack");
			Invoke("LauchBullet", 0.5f);
		}
		else
		{
			waitedTime -= Time.deltaTime;
		}
	}

	public void LauchBullet()
	{
		//Get Object from pool
		GameObject newProjectile = pooler.GetObjectToPool();
		if (newProjectile == null) return;
		newProjectile.transform.position = firePoint.position;
		newProjectile.SetActive(true);

		//Get projectile
		Bullet bullet = newProjectile.GetComponent<Bullet>();
		bullet.SetDirection(facingRight ? Vector3.right : Vector3.left);
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.EnemyProjectileClip);
	}
}
