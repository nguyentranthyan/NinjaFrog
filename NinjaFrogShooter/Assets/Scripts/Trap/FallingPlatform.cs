using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
	[Header("Falling Platform")]
	[HideInInspector] private Rigidbody2D rigidbody2D; 
	private Vector2 initialPos;
	[SerializeField] private bool isBack;

	private void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		initialPos = transform.position;
	}

	private void Update()
	{
		if (isBack)
			transform.position = Vector2.MoveTowards(transform.position, initialPos, 20f * Time.deltaTime);
		if (transform.position.y == initialPos.y)
			isBack = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>() != null && !isBack)
		{
			Invoke("DropPlatform", 2f);
		}
	}

	private void DropPlatform()
	{
		rigidbody2D.isKinematic = false;
		Invoke("GetPlatformBack", 1f);
	}

	private void GetPlatformBack()
	{
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.isKinematic = true;
		isBack = true;
	}
}
