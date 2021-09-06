using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectable
{
	[SerializeField] private int keyNumber;

	public override void Collect()
	{
		SFXManager.Instance.ShowItemSparkle(this.gameObject.transform.position);
		AddKey();
	}
	private void AddKey()
	{
		Debug.Log(keyNumber);
		GameManager.Instance.UpdateKey(keyNumber);
	}
}
