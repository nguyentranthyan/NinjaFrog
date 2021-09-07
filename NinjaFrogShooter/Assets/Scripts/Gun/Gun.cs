using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun's Player")]
    [SerializeField] private ProjectTile m_projectilePrefabs;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private Animator m_animator;
    [SerializeField] private SpriteRenderer m_gunModel;

    [Header("Gun Setting")]
    [SerializeField] private float m_msBetweenShoots = 100;
    [SerializeField] private float m_nextShortTime;

    [Header("Ammo")]
    public static Action<int> OnAmmoChange;

    [SerializeField] private int m_magazineSize = 30;
    [SerializeField] private bool autoReload = true;
    [SerializeField] private float m_reloadTimer = 1f;
   
    private bool m_isReLoading;
    private int m_projectileRemaining;

    private ObjectPool m_pool;
    public GunController GunController { get; set; }

	private void Start()
	{
        m_pool = GetComponent<ObjectPool>();
        m_projectileRemaining = m_magazineSize;
        UpdateAmmoUI();
    }

	private void Update()
	{
		if (autoReload)
		{
            Reload(true);
        }
       
    }

    /// <summary>
    /// Shoot Projectile
    /// </summary>
    public void Shoot()
    {
        if (Time.time > m_nextShortTime && !m_isReLoading && m_projectileRemaining > 0)
        {
            m_nextShortTime = Time.time + m_msBetweenShoots / 1000f;
            FireProjectile();
            m_projectileRemaining--;
            SoundManager.Instance.PlaySound(AudioLibrary.Instance.ProjectileClip);
            UpdateAmmoUI();
        }
    }


    private void FireProjectile()
	{
        //Get Object from pool
        GameObject newProjectile = m_pool.GetObjectToPool();
        if(newProjectile == null)return;
        newProjectile.transform.position = m_firePoint.position;
        newProjectile.SetActive(true);

        //Get projectile
        ProjectTile projectTile = newProjectile.GetComponent<ProjectTile>();
        projectTile.GunEquipp = this;
        projectTile.SetDirection(GunController.PlayerController.FacingRight ? Vector3.right : Vector3.left);
	}

    /// <summary>
    /// Update Ammo Ui
    /// </summary>
    private void UpdateAmmoUI()
    {
        OnAmmoChange?.Invoke(m_projectileRemaining);
    }

    /// <summary>
    /// Reload Ammo
    /// </summary>
    /// <param name="autoReload"></param>
    public void Reload(bool autoReload)
	{
        if(m_projectileRemaining > 0 && m_projectileRemaining <= m_magazineSize && !m_isReLoading && !this.autoReload)
		{
          StartCoroutine(IEWaitForReload());
        }

        if (m_projectileRemaining <= 0 && !m_isReLoading)
		{
          StartCoroutine(IEWaitForReload());
        }
    }

    private IEnumerator IEWaitForReload()
	{
        m_isReLoading = true;
        yield return new WaitForSeconds(m_reloadTimer);
        m_projectileRemaining = m_magazineSize;
        m_isReLoading = false;
        UpdateAmmoUI();
    }
}
