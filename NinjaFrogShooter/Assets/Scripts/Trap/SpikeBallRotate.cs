using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallRotate : MonoBehaviour
{
    [Header("Auto Rotate")]
    [SerializeField] private Vector3 rotateAxis;
    [SerializeField] private float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(rotateAxis * speed * Time.deltaTime);
    }
}
