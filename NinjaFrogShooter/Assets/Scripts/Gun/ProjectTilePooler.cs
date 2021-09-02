using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTilePooler : MonoBehaviour
{
    /// <summary>
    /// Event raised when colliding
    /// </summary>
    public static Action<Collider2D> OnProjectileCollision;

    [Header("Setting")]
    [SerializeField] private LayerMask m_coliderWith;
    private ProjectTile m_projectTile;
  

    // Start is called before the first frame update
    void Start()
    {
        m_projectTile = GetComponent<ProjectTile>();
    }

	// Update is called once per frame
	void Update()
    {
        CheckCollision();
    }

    private void CheckCollision()
	{
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_projectTile.ShootDirection,
                                            m_projectTile.Speed * Time.deltaTime + 0.2f, m_coliderWith);
		if (hit)
		{
            OnProjectileCollision?.Invoke(hit.collider);
            gameObject.SetActive(false);
            SoundManager.Instance.PlaySound(AudioLibrary.Instance.ProjectileCollisionClip);
        }
	}
}
