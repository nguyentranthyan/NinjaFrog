using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : PathFollow
{
    public bool CollingWithPlayer { get; set; }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.GetComponent<PlayerController>() != null)
		{
			CollingWithPlayer = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		CollingWithPlayer = false;
	}
}
