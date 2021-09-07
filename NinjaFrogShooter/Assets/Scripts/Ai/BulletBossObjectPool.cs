using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBossObjectPool : MonoBehaviour
{
    /// <summary>
    /// Event raised when colliding
    /// </summary>
    public static Action<Collider2D> OnBulletCollision;

    [Header("Setting")]
    [SerializeField] private LayerMask m_coliderWith;
    private BulletBoss m_bullet;


    // Start is called before the first frame update
    void Start()
    {
        m_bullet = GetComponent<BulletBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
    }

    private void CheckCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_bullet.ShootDirection,
                                            m_bullet.Speed * Time.deltaTime + 0.2f, m_coliderWith);
        if (hit)
        {
            OnBulletCollision?.Invoke(hit.collider);
            gameObject.SetActive(false);
        }
    }
}
