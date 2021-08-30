using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GunController : MonoBehaviour
{
    [Header("Gun Controller")]
    [SerializeField] private Gun m_gun;
    [SerializeField] private Transform m_holder;

    private Gun m_gunEquipp;

	public PlayerController PlayerController { get; set; }

	private void Start()
	{
		PlayerController = GetComponent<PlayerController>();
		EquippGun(m_gun);
	}
	private void Update()
	{
		if (CrossPlatformInputManager.GetButton("Shoot"))
		{
			if (m_gunEquipp != null)
			{
				Shoot();
				m_gunEquipp.Reload(false);
			}
		}
	}

	private void Shoot()
	{
		m_gunEquipp.Shoot();
	}

	public void EquippGun(Gun newGun)
    {
		if (m_gunEquipp == null)
		{
            m_gunEquipp = Instantiate(newGun, m_holder.position, Quaternion.identity);
			m_gunEquipp.GunController = this;
            m_gunEquipp.transform.SetParent(m_holder);  
		}
    }
}
