using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerJetpack : PlayerStates
{
	[Header("PlayerJetpack")]
	[SerializeField] private float jetpackForce = 5f;
	[SerializeField] private float jetpackFuel = 100f;

	public float JetpackFuel { get; set; }
	public float FuelLeft { get; set; }
	public float InitialFuel => jetpackFuel;

	private float m_fuelDurectionLeft;
	private bool m_stillHaveFuel = true;

	private int jetpackAnim = Animator.StringToHash("Jetpack");

	protected override void InitStart()
	{
		base.InitStart();
		JetpackFuel = jetpackFuel;
		m_fuelDurectionLeft = jetpackFuel;
		FuelLeft = jetpackFuel;
		UiManager.Instance.UpdateFuel(FuelLeft, JetpackFuel);
	}

	protected override void GetInput()
	{
		if (CrossPlatformInputManager.GetButton("Jetpack"))
		{
			JetPack();
		}

		if (CrossPlatformInputManager.GetButtonUp("Jetpack"))
		{
			EndJetPack();
		}
	}

	private void JetPack()
	{
		if (!m_stillHaveFuel)
		{
			return;
		}

		if (FuelLeft <= 0.001f)
		{
			EndJetPack();
			m_stillHaveFuel = false;
			return;
		}

		m_playerController.SetVerticalForce(jetpackForce);
		m_playerController.Conditions.IsJetpacking = true;
		StartCoroutine(BrunFuel());
	}

	private void EndJetPack()
	{
		m_playerController.Conditions.IsJetpacking = false;
		StartCoroutine(Refill());
	}

	private IEnumerator BrunFuel()
	{
		float fuelConsumed = FuelLeft;
		if (fuelConsumed > 0 && m_playerController.Conditions.IsJetpacking && FuelLeft <= fuelConsumed)
		{
			fuelConsumed -= Time.deltaTime * 20;
			FuelLeft = fuelConsumed;
			UiManager.Instance.UpdateFuel(FuelLeft, JetpackFuel);
			yield return null;
		}	
	}

	private IEnumerator Refill()
	{
		yield return new WaitForSeconds(0.5f);
		float fuel = FuelLeft;
		while (fuel < JetpackFuel && !m_playerController.Conditions.IsJetpacking)
		{
			fuel += Time.deltaTime * 5;
			FuelLeft = fuel;
			UiManager.Instance.UpdateFuel(FuelLeft, JetpackFuel);

			if (!m_stillHaveFuel && fuel > 0.2f)
			{
				m_stillHaveFuel = true;
			}
			yield return null;
		}
	}

	public override void SetAnimation()
	{
		m_Animator.SetBool(jetpackAnim, m_playerController.Conditions.IsJetpacking);
	}
}
