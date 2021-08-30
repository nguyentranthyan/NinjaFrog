using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBee : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float startWaitTime = 2;
    [SerializeField] private Transform[] movePos;
    private float waiTime;

    private int i = 0;
    private Vector2 actualPos;


    // Start is called before the first frame update
    void Start()
    {
        waiTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(IECheckEnemyMoving());

        transform.position = Vector2.MoveTowards(transform.position, movePos[i].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].transform.position) < 0.1f)
        {
            if (waiTime < 0)
            {
                if (movePos[i] != movePos[movePos.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }

                waiTime = startWaitTime;
            }
            else
            {
                waiTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator IECheckEnemyMoving()
    {
        actualPos = transform.position;
        yield return new WaitForSeconds(0.5f);
        if (transform.position.x > actualPos.x)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Idle", false);
        }
        else if (transform.position.x < actualPos.x)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Idle", false);
        }
        else if (transform.position.x == actualPos.x)
        {
            animator.SetBool("Idle", true);
        }
    }
}
