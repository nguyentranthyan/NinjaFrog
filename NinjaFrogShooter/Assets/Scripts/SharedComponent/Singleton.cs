using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	public static T instance;

	public static T Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<T>();
				if (instance == null)
				{
					GameObject objectSearch = new GameObject();
					instance = objectSearch.AddComponent<T>();
				}
			}
			return instance;
		}
	}

	protected virtual void Awake()
	{
		instance = this as T;
	}
}
