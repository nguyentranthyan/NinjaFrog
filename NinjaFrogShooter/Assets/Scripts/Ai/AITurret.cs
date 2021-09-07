using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurret : MonoBehaviour
{
	private float waitedTime;
	[SerializeField] private float waitTimeToAttack = 3;
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
		GameObject newbullet = pooler.GetObjectToPool();
		if (newbullet == null) return;
		newbullet.transform.position = firePoint.position;
		newbullet.SetActive(true);

		//Get projectile
		Bullet bullet = newbullet.GetComponent<Bullet>();
		bullet.SetDirection(facingRight ? Vector3.right : Vector3.left);
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.EnemyProjectileClip);
	}
}
