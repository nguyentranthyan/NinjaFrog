using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : PlayerStates
{
	[Header("Setting")]
	[SerializeField] private int lifes = 5;
	private Animator anim;

	private int m_maxLifes;
	private int m_currentLifes;
	public int MaxLife => m_maxLifes;
	public int CurrentLifes => m_currentLifes;

	public static Action<int> OnLifesChange;
	public static Action<PlayerMotor> OnDeath;
	public static Action<PlayerMotor> OnRevive;

	private void Awake()
	{
		m_maxLifes = lifes;
		ResetLifes();
	}

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	public void AddLifes()
	{
		m_currentLifes += 1;
		if (m_currentLifes > m_maxLifes)
		{
			m_currentLifes = m_maxLifes;
		}
		UpdateLifesUI();
	}

	public void LoseLifes()
	{
		m_currentLifes -= 1;
		if (m_currentLifes <= 0)
		{
			m_currentLifes = 0;
			OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
		}
		UpdateLifesUI();
		anim.SetTrigger("Hit");
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerHurtClip);
	}

	public void KillPlayer()
	{
		m_currentLifes = 0;
		OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
		UpdateLifesUI();
		anim.SetTrigger("Hit");
		SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerDeathClip);
	}

	public void Revive()
	{
		OnRevive?.Invoke(gameObject.GetComponent<PlayerMotor>());
	}

	public void ResetLifes()
	{
		m_currentLifes = lifes;
		UpdateLifesUI();
	}

	private void UpdateLifesUI()
	{
		OnLifesChange?.Invoke(m_currentLifes);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.GetComponent<IDamageable>() != null)
		{
			collision.GetComponent<IDamageable>().Damage(gameObject.GetComponent<PlayerMotor>());
		}
	}
}
