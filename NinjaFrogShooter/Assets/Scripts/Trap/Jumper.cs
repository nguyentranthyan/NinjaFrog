using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
	[Header("Setting")]
    [SerializeField] private float jumpHeight = 5f;
	private Animator m_animator;
	private int m_jumper = Animator.StringToHash("Jumper");

	public static Action<float> OnJump;

	private void Start()
	{
		m_animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerJump>() != null)
		{
			OnJump?.Invoke(jumpHeight);
            m_animator.SetTrigger(m_jumper);
			SoundManager.Instance.PlaySound(AudioLibrary.Instance.JumpClip);
		}
	}
}
