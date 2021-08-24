using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DShake : Singleton<Camera2DShake>
{
    [Header("Setting")]
    [SerializeField] private float shakeVibrato = 10f;
    [SerializeField] private float shakeRandomness = 0.2f;
    [SerializeField] private float shakeTime = 0.01f;

  //  // Update is called once per frame
  //  void Update()
  //  {
		//if (Input.GetKeyDown(KeyCode.V))
		//{
  //          Shake();
		//}
  //  }

	public void Shake()
	{
        StartCoroutine(IEShake());
	}

    private IEnumerator IEShake()
	{
        Vector3 currentPos = transform.position;
        for(int i=0; i < shakeVibrato; i++)
		{
            Vector3 shakePos = currentPos + UnityEngine.Random.onUnitSphere * shakeRandomness;
            yield return new WaitForSeconds(shakeTime);
            transform.position = shakePos;
		}
	}
}
