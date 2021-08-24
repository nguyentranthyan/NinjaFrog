using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerStates
{
	[Header("Player Movement")]
	[SerializeField] private float m_speed = 5f;

	private float m_hMove;
	private float m_movement;

	protected override void GetInput()
	{
		m_hMove = m_horizontalInput;
	}

	public override void ExcuteState()
	{
		MovePlayer();
	}

	public void MovePlayer()
	{
		if (Mathf.Abs(m_hMove) > 0.1f)
		{
			m_movement = m_hMove;
		}
		else
		{
			m_movement = 0f;
		}
		float moveSpeed = m_movement * m_speed;
		m_playerController.SetHorizontalForce(moveSpeed);
	}
}
