using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerMotor>() != null)
		{
			animator.SetBool("IsCP", true);
			GameManager.Instance.lastCheckPointPos = transform.position;
		}
	}
}
