using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds2D : MonoBehaviour
{
    [SerializeField] private bool bounds;
    [SerializeField] Vector2 minCamera, maxCamera;

	void LateUpdate()
    {
		if (bounds)
		{
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamera.x, maxCamera.x),
											Mathf.Clamp(transform.position.y, minCamera.y, maxCamera.y), transform.position.z);
		}
    }
}
