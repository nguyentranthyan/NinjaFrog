using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    private float waitedTime;
    [SerializeField] private float waitTimeToAttack = 1;
	[SerializeField] private bool canfire;
    [SerializeField] Animator animator;
	[SerializeField] private bool facingRight;

	[SerializeField] private float chaseRadius;
	[SerializeField] private float attackRadius;

	[SerializeField] private int health = 5;
	[SerializeField] private Slider healthSlider;


	[Header("Shooting")]
    [SerializeField] private Transform firePoint;
	private Transform target;

	/// <summary>
	/// Reference Pooler
	/// </summary>
	public ObjectPool pooler;

	// Use this for initialization 
	private void Start()
	{
		healthSlider.maxValue = health;
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
		BulletBoss bullet = newbullet.GetComponent<BulletBoss>();
		bullet.SetDirection(facingRight ? Vector3.right : Vector3.left);
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.EnemyProjectileClip);
	}

	private void Collision(Collider2D objectCollided)
	{
		if (health == 0)
		{
			animator.SetBool("Death", true);
			objectCollided.gameObject.SetActive(false); 
			GameManager.Instance.UpdateScore(GameManager.Item.Enemy);
			SoundManager.Instance.PlaySound(AudioLibrary.Instance.EnemyExplode);
		}
		if (health > 0)
		{
			health --;
			healthSlider.value = health;
			animator.SetTrigger("Hit");
			SoundManager.Instance.PlaySound(AudioLibrary.Instance.EnemyHit);
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
		BulletBossObjectPool.OnBulletCollision += CollisionBulet;
	}

	private void OnDisable()
	{
		ProjectTilePooler.OnProjectileCollision -= Collision;
		BulletBossObjectPool.OnBulletCollision -= CollisionBulet;
	}
}
