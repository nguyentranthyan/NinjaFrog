using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : Singleton<AudioLibrary>
{
    [Header("Clips")]

    //Collect
    [SerializeField] private AudioClip breakCrates;
    [SerializeField] private AudioClip collectableClip;
    [SerializeField] private AudioClip keyFound;
    [SerializeField] private AudioClip powerup;

    //UI
    [SerializeField] private AudioClip btnpop;
    [SerializeField] private AudioClip gameover;
    [SerializeField] private AudioClip stageclear;
    [SerializeField] private AudioClip timeralarm;

    //Enemy
    [SerializeField] private AudioClip enemyExplode;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] private AudioClip enemyProjectileClip;


    //Player
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip playerDeathClip;
    [SerializeField] private AudioClip playerHurtClip;
    [SerializeField] private AudioClip projectileClip;
    [SerializeField] private AudioClip projectileCollisionClip;

    //Collect
    public AudioClip BreakCrates => breakCrates;
    public AudioClip CollectableClip => collectableClip;
    public AudioClip KeyFound => keyFound;
    public AudioClip Powerup => powerup;

    //UI
    public AudioClip Btnpop => btnpop;
    public AudioClip Gameover => gameover;
    public AudioClip Stageclear => stageclear;
    public AudioClip Timeralarm => timeralarm;

    //Enemy
    public AudioClip EnemyExplode => enemyExplode;
    public AudioClip EnemyHit => enemyHit;
    public AudioClip EnemyProjectileClip => enemyProjectileClip;

    //Player
    public AudioClip JumpClip => jumpClip;
    public AudioClip PlayerDeathClip => playerDeathClip;
    public AudioClip PlayerHurtClip => playerHurtClip;
    public AudioClip ProjectileClip => projectileClip;
    public AudioClip ProjectileCollisionClip => projectileCollisionClip;
}
