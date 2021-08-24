using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [Header("Fuel's jetpack")]
    [SerializeField] private Image fuelImage;
    [SerializeField] private Text fuelText;

    private float m_currentJetpackFuel;
    private float m_jetpackFuel;

    [Header("Heart")]
    [SerializeField] private GameObject[] playerLifes;

    // Update is called once per frame
    void Update()
    {
        InternalJectpackUpdate();
    }

    public void UpdateFuel(float currentFuel, float maxFuel)
	{
        m_currentJetpackFuel = currentFuel;
        m_jetpackFuel = maxFuel;
	}

    private void InternalJectpackUpdate()
    {
        fuelImage.fillAmount = Mathf.Lerp(fuelImage.fillAmount, m_currentJetpackFuel / m_jetpackFuel,
            Time.deltaTime * 10f);
        fuelText.text ="Fuel: "+ (int) m_currentJetpackFuel + " %";
    }

    private void OnPlayerLives(int currentlife)
    {
        for (int i = 0; i < playerLifes.Length; i++)
        {
            if (i < currentlife)
            {
                playerLifes[i].gameObject.SetActive(true);
            }
            else
            {
                playerLifes[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
	{
        PlayerHealth.OnLifesChange += OnPlayerLives;
	}

	private void OnDisable()
	{
        PlayerHealth.OnLifesChange -= OnPlayerLives;
    }

   
}
