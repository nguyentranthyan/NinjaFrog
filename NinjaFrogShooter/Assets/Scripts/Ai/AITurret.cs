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
	[SerializeField] private float chaseRadius;
	[SerializeField] private float attackRadius;
	private Transform target;
	[SerializeField] private bool canfire;
	///// <summary>
	///// Reference player
	///// </summary>
	//public PlayerMotor Target { get; set; }

	/// <summary>
	/// Reference Pooler
	/// </summary>
	public ObjectPool pooler;

	public void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().transform;
		canfire = false;
	}

	private void FixedUpdate()
	{
		CheckDistance();
		if (canfire)
		{
			Reload();
		}
	}

	/// <summary>
	/// Check distance player and enemy
	/// </summary>
	public virtual void CheckDistance()
	{
		float distance = Vector3.Distance(target.position, transform.position);
		Debug.DrawLine(target.position, transform.position);
		if (distance <= chaseRadius && distance > attackRadius)
		{
			canfire = true;
		}
		else if (distance > chaseRadius)
		{
			canfire = false;
		}
	}

	void Reload()
	{
		if (target != null)
		{
			if (waitedTime <= 0)
			{
				waitedTime = waitTimeToAttack;
				animator.SetBool("Attack", true);
				Invoke(nameof(LauchBullet), 0.5f);
			}
			else
			{
				waitedTime -= Time.deltaTime;
			}
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
