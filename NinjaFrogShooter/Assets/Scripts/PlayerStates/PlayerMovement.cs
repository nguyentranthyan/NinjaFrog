using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerStates
{
	[Header("Player Movement")]
	[SerializeField] private float m_speed = 5f;
	public float Speed { get; set; }
	public float InitialSpeed => m_speed;

	private float m_hMove;
	private float m_movement;

	private int idleAnim = Animator.StringToHash("Idle");
	private int runAnim = Animator.StringToHash("Run");

	protected override void InitStart()
	{
		base.InitStart();
		Speed = m_speed;
	}

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
		moveSpeed = EvaluateFriction(moveSpeed);
		m_playerController.SetHorizontalForce(moveSpeed);
	}

	public override void SetAnimation()
	{
		m_Animator.SetBool(idleAnim, m_hMove == 0 && m_playerController.Conditions.IsCollidingBellow);
		m_Animator.SetBool(runAnim, Mathf.Abs(m_horizontalInput) > 0.1f && m_playerController.Conditions.IsCollidingBellow);
	}

	private float EvaluateFriction(float moveSpeed)
	{
		if (m_playerController.Friction > 0)
		{
			moveSpeed = Mathf.Lerp(m_playerController.Force.x, moveSpeed,
									Time.deltaTime * 10f * m_playerController.Friction);
		}
		return moveSpeed;
	}
}
