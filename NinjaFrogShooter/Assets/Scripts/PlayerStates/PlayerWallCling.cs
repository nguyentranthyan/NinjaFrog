using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCling : PlayerStates
{
	[Header("PlayerWallCling")]
	[SerializeField] private float fallFactor = 0.5f;
	private int WallClingAnim = Animator.StringToHash("WallCling");

	protected override void GetInput()
	{
		if(m_horizontalInput <= -0.1f || m_horizontalInput >= 0.1f)
		{
			WallCling();
		}
	}

	public override void ExcuteState()
	{
		ExitWallCling();
	}

	private void WallCling()
	{
		if(m_playerController.Conditions.IsCollidingBellow || m_playerController.Force.y >= 0)
		{
			return;
		}

		if (m_playerController.Conditions.IsCollidingLeft && m_horizontalInput <= -0.1f ||
			m_playerController.Conditions.IsCollidingRight && m_horizontalInput >= 0.1f)
		{
			m_playerController.SetWallClingMultiplier(fallFactor);
			m_playerController.Conditions.IsWallCling = true;
		}
	}

	private void ExitWallCling()
	{
		if (m_playerController.Conditions.IsWallCling)
		{
			if(m_playerController.Conditions.IsCollidingBellow || m_playerController.Force.y >= 0)
			{
				m_playerController.SetWallClingMultiplier(0f);
				m_playerController.Conditions.IsWallCling = false;
			}

			if (m_playerController.FacingRight)
			{
				if (m_horizontalInput <= -0.1f || m_horizontalInput <= 0.1f)
				{
					m_playerController.SetWallClingMultiplier(0f);
					m_playerController.Conditions.IsWallCling = false;
				}
			}
			else
			{
				if (m_horizontalInput >= 0.1f || m_horizontalInput >= -0.1f)
				{
					m_playerController.SetWallClingMultiplier(0f);
					m_playerController.Conditions.IsWallCling = false;
				}
			}
		}
	}

	public override void SetAnimation()
	{
		m_Animator.SetBool(WallClingAnim, m_playerController.Conditions.IsWallCling 
											&&(m_playerController.Conditions.IsCollidingLeft 
											|| m_playerController.Conditions.IsCollidingRight));
	}
}
