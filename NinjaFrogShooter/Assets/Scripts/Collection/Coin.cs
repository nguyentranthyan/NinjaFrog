using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

	public override void Collect()
	{
		AddCoin();
	}

	private void AddCoin()
	{
		Destroy(gameObject);
	}
}
