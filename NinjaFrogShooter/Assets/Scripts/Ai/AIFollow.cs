using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private Vector2 homePos;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Transform target;

    // Use this for initialization 
    void Start () 
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; 
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        CheckDistance();
    }

    /// <summary>
    /// Check distance player and enemy
    /// </summary>
    public virtual void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Debug.DrawLine(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            animator.SetBool("Idle", true);
            if (transform.position.x > target.position.x)
            {
                transform.DOMove(target.position, speed, false);
                spriteRenderer.flipX = false;
                
            }
            else if (transform.position.x < target.position.x)
            {
                transform.DOMove(target.position, speed, false);
                spriteRenderer.flipX = true;
            }
        }
        else if (distance > chaseRadius)
        {
            goHome();
        }
    }

    public void goHome()
    {
        transform.position = Vector3.MoveTowards(transform.position, homePos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, homePos) == 0)
        {
            animator.SetBool("Idle", false);
        }
    }
}
