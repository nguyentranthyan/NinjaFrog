using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBoxItem : MonoBehaviour
{
    private PlayerMotor m_playerMotor;
    [SerializeField] private Animator animator;

    [Header("Variable's item random drop")]
    [SerializeField] private GameObject[] dropItems;
    [SerializeField] private float dropRate = 0.5f; //50% random Item

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMotor>() != null)
        {
            m_playerMotor = collision.gameObject.GetComponent<PlayerMotor>();
             animator.SetBool("Hit", true);
            StartCoroutine(IEDisableCollectable());
        }
       
    }

    IEnumerator DropRateItem()
    {
        float dropSelect = Random.Range(0f, 1f); //0-100%
        if (dropSelect <= dropRate)
        {
            int indexToDrop = Random.Range(0, dropItems.Length);
            Instantiate(dropItems[indexToDrop], this.transform.position, this.transform.rotation);
        }
        yield return new WaitForSeconds(.1f);
    }

    IEnumerator IEDisableCollectable()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        StartCoroutine(DropRateItem());
    }
}
