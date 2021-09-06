using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleComponent :  MonoBehaviour, IDamageable
{
	[Header("ObstacleComponent")]
	[SerializeField] protected bool instanceKill;
	
	public virtual void Damage(PlayerMotor player)
	{
		if (player != null)
		{
			if (instanceKill)
			{
				//kill method
				player.GetComponent<PlayerHealth>().KillPlayer();
				SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerDeathClip);
			}
			else
			{
				player.GetComponent<PlayerHealth>().LoseLifes();
				SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerHurtClip);
			}
		}
	}
}
