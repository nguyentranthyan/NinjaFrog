using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerStates : MonoBehaviour
{
    protected float m_horizontalInput; 
    protected float m_verticalInput;
	protected PlayerController m_playerController;
	protected Animator m_Animator;

	protected virtual void Start()
	{
		InitStart();
	}

	/// <summary>
	/// Init Get PlayerController
	/// </summary>
	protected virtual void InitStart()
	{
		m_playerController = GetComponent<PlayerController>();
		m_Animator = GetComponent<Animator>();
	}

	protected virtual void GetInput()
	{

	}

	public virtual void ExcuteState()
	{

	}
    public virtual void LocalInput()
	{
		m_horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		m_verticalInput = CrossPlatformInputManager.GetAxisRaw("Vertical");
		GetInput();
	}

	public virtual void SetAnimation()
	{

	}
}
