using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleComponent : MonoBehaviour, IDamageable
{
	[Header("Setting")]
	[SerializeField] protected bool instanceKill;

	public virtual void Damage(PlayerMotor player)
	{
		if (player != null)
		{
			if (instanceKill)
			{
				//kill method
				Camera2DShake.Instance.Shake();
				player.GetComponent<PlayerHealth>().KillPlayer();
			}
			else
			{
				Camera2DShake.Instance.Shake();
				player.GetComponent<PlayerHealth>().LoseLifes();
			}
		}
	}
}
