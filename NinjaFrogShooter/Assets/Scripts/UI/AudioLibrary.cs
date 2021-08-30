using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : Singleton<AudioLibrary>
{
    [Header("Clips")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip collectableClip;
    [SerializeField] private AudioClip projectileClip;
    [SerializeField] private AudioClip enemyProjectileClip;
    [SerializeField] private AudioClip playerDeathClip;
    [SerializeField] private AudioClip playerHurtClip;
    [SerializeField] private AudioClip enemyExplode;
    [SerializeField] private AudioClip projectileCollisionClip;

    public AudioClip JumpClip => jumpClip;
    public AudioClip CollectableClip => collectableClip;
    public AudioClip ProjectileClip => projectileClip;
    public AudioClip EnemyProjectileClip => enemyProjectileClip;
    public AudioClip PlayerDeathClip => playerDeathClip;
    public AudioClip PlayerHurtClip => playerHurtClip;
    public AudioClip EnemyExplode => enemyExplode;
    public AudioClip ProjectileCollisionClip => projectileCollisionClip;
}
