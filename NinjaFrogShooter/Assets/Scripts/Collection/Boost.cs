using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Collectable
{
	private PlayerMovement m_playerMovement;
	[SerializeField] private float bootsSpeed = 15f;
	[SerializeField] private float bootsTime = 3f;

	public override void Collect()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.CollectableClip);
		SFXManager.Instance.ShowItemSparkle(this.gameObject.transform.position);
		ApplyMovement();
	}

	private void ApplyMovement()
	{
		m_playerMovement = m_playerMotor.GetComponent<PlayerMovement>();
		if (m_playerMovement != null)
		{
			StartCoroutine(IEBoost());
		}
	}

	private IEnumerator IEBoost()
	{
		m_playerMovement.Speed = bootsSpeed;
		yield return new WaitForSeconds(bootsTime);
		m_playerMovement.Speed = m_playerMovement.InitialSpeed;
	}
}
