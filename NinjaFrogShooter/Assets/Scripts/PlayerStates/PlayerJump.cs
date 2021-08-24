using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerStates
{
	[Header("PlayerJump")]
	[SerializeField] private float jumpHeight = 5f;
	[SerializeField] private int maxJumps = 2;
	public int JumpLeft { get; set; }

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
		}
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

}
