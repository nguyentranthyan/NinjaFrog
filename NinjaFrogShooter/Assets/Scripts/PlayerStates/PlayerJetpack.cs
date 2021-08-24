using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetpack : PlayerStates
{
	[Header("PlayerWallCling")]
	[SerializeField] private float jetpackForce = 3f;

	protected override void GetInput()
	{
		if (Input.GetKey(KeyCode.J))
		{
			Jetpack();
		}
		if (Input.GetKeyUp(KeyCode.J))
		{
			EndJetpack();
		}
	}

	private void Jetpack()
	{

	}

	private void EndJetpack()
	{

	}


}
