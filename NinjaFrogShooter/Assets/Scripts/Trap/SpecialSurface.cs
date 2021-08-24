using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSurface : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float friction = 0.1f;

    [Header("Movement")]
    [SerializeField] private float horizontalMovement = 4f;

    public float Friction => friction;

    private PlayerController m_playerController;

	private void Update()
	{
		if (m_playerController == null)
		{
			return;
		}
		m_playerController.AddHorizontalForce(horizontalMovement);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>() != null)
		{
			m_playerController = collision.gameObject.GetComponent<PlayerController>();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		m_playerController = null;
	}
}
