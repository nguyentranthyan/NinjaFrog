using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippGun : Collectable
{
	[Header("Setting")]
	[SerializeField] private Gun m_gunPrefabs;

	public override void Collect()
	{
		CollectGuns();
	}

	private void CollectGuns()
	{
		if (m_playerMotor.GetComponent<GunController>() != null)
		{
			m_playerMotor.GetComponent<GunController>().EquippGun(m_gunPrefabs);
		}
	}
}
