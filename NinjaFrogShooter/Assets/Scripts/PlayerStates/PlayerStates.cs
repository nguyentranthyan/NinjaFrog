using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    protected float m_horizontalInput; 
    protected float m_verticalInput;
	protected PlayerController m_playerController;

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
	}

	public virtual void ExcuteState()
	{

	}
    public virtual void LocalInput()
	{
		m_horizontalInput = Input.GetAxisRaw("Horizontal");
		m_verticalInput = Input.GetAxisRaw("Vertical");
		GetInput();
	}

	protected virtual void GetInput()
	{

	}
}
