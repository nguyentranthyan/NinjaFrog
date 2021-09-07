using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	protected PlayerMotor m_playerMotor;
	protected SpriteRenderer m_spriteRenderer;
	protected Collider2D m_collider2D;

	private void Start()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		m_collider2D = GetComponent<Collider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerMotor>() != null)
		{
			m_playerMotor = collision.gameObject.GetComponent<PlayerMotor>();
			CollectLogic();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		m_playerMotor = null;
	}

	private void CollectLogic()
	{
		if (!CanPicked())
		{
			return ;
		}
		Collect();
		
		DisableCollectable();
	}

	private void DisableCollectable()
	{
		m_collider2D.enabled = false;
		m_spriteRenderer.enabled = false;
	}

	public virtual void Collect()
	{
		
	}

	private bool CanPicked()
	{
		return m_playerMotor != null;
	}
}
