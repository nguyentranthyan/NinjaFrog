using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private float m_speed = 10f;
    private Vector3 m_shootDirection;

	public float Speed { get; set; }

	public Vector3 ShootDirection => m_shootDirection;
    
    public Gun GunEquipp { get; set; }

	private void Awake()
	{
		Speed = m_speed;
	}

	/// <summary>
	/// Set direction player shooter
	/// </summary>
	/// <param name="newDirection"></param>
	public void SetDirection(Vector3 newDirection)
	{
        m_shootDirection = newDirection;
	}

	// Update is called once per frame
	void Update()
    {
        transform.Translate(m_shootDirection * Speed * Time.deltaTime);
    }
}
