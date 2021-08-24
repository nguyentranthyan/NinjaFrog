using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerStates
{
	[Header("PlayerJump")]
	[SerializeField] private float jumpHeight = 5f;
	[SerializeField] private int maxJumps = 2;
	public int JumpLeft { get; set; }

	private int jumpAnim = Animator.StringToHash("Jump");
	private int doubleJumpAnim = Animator.StringToHash("DoubleJump");
	private int fallAnim = Animator.StringToHash("Fall");

	protected override void InitStart()
	{
		base.InitStart();
		JumpLeft = maxJumps;
	}

	protected override void GetInput()
	{
		if (Input.GetButtonDown("Jump"))
		{
			Jump();
		}
	}

	public override void ExcuteState()
	{
		if(m_playerController.Conditions.IsCollidingBellow && m_playerController.Force.y == 0)
		{
			JumpLeft = maxJumps;
			m_playerController.Conditions.IsJumping = false;
		}
	}
	private bool CanJump()
	{
		if (!m_playerController.Conditions.IsCollidingBellow && JumpLeft <= 0)
		{
			return false;
		}

		if (m_playerController.Conditions.IsCollidingBellow && JumpLeft <= 0)
		{
			return false;
		}
		return true;
	}

	private void Jump()
	{
		if (!CanJump())
		{
			return;
		}
		if (JumpLeft == 0)
		{
			return;
		}
		JumpLeft -= 1;

		float jumpForce = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(m_playerController.Gravity));
		m_playerController.SetVerticalForce(jumpForce);
		m_playerController.Conditions.IsJumping = true;
	}


	public override void SetAnimation()
	{
		m_Animator.SetBool(jumpAnim, m_playerController.Conditions.IsJumping
									&& !m_playerController.Conditions.IsCollidingBellow
									&& !m_playerController.Conditions.IsFalling
									&& !m_playerController.Conditions.IsJetpacking
									&& JumpLeft > 0);

		m_Animator.SetBool(doubleJumpAnim, m_playerController.Conditions.IsJumping
									&& !m_playerController.Conditions.IsCollidingBellow
									&& !m_playerController.Conditions.IsFalling
									&& !m_playerController.Conditions.IsJetpacking
									&& JumpLeft == 0);

		m_Animator.SetBool(fallAnim, m_playerController.Conditions.IsFalling
									&& m_playerController.Conditions.IsJumping
									&& !m_playerController.Conditions.IsJetpacking
									&& !m_playerController.Conditions.IsCollidingBellow);
	}

	private void JumpResponse(float jump)
	{
		m_playerController.SetVerticalForce(Mathf.Sqrt(2f * jump * Mathf.Abs(m_playerController.Gravity)));
	}

	private void OnEnable()
	{
		Jumper.OnJump += JumpResponse;
	}

	private void OnDisable()
	{
		Jumper.OnJump -= JumpResponse;
	}
}
