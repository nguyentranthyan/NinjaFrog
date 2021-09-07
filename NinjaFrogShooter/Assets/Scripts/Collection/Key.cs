using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectable
{
	[SerializeField] private int keyNumber;

	public override void Collect()
	{
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.KeyFound);
		SFXManager.Instance.ShowItemSparkle(this.gameObject.transform.position);
		AddKey();
	}
	private void AddKey()
	{
		GameManager.Instance.UpdateKey(keyNumber);
	}
}
