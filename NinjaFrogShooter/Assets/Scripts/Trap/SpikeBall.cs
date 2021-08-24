using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour, IDamageable
{
	[Header("Check trap kill")]
	[SerializeField] private bool instantKill;

	public void Damage(PlayerMotor player)
	{
		if (player != null)
		{
			if (instantKill)
			{
				player.GetComponent<PlayerHealth>().KillPlayer();
				Camera2DShake.Instance.Shake();
			}
			else
			{
				player.GetComponent<PlayerHealth>().LoseLifes();
				Camera2DShake.Instance.Shake();
			}
		}
	}
}
