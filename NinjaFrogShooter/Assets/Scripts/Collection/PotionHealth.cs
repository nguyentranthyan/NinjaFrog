using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : Collectable
{
	public override void Collect()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.CollectableClip);
		SFXManager.Instance.ShowItemSparkle(this.gameObject.transform.position);
		AddLifte();
	}

	public void AddLifte()
	{
		if (m_playerMotor.GetComponent<PlayerHealth>() == null)
		{
			return;
		}
		PlayerHealth playerHeath = m_playerMotor.GetComponent<PlayerHealth>();
		if (playerHeath.CurrentLifes < playerHeath.MaxLife)
		{
			playerHeath.AddLifes();
		}
	}
}
