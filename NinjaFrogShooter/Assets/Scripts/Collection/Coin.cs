using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
	public override void Collect()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.CollectableClip);
		SFXManager.Instance.ShowItemSparkle(this.gameObject.transform.position);
		AddCoin();
	}

	private void AddCoin()
	{
		GameManager.Instance.UpdateCoin();
	}
}
