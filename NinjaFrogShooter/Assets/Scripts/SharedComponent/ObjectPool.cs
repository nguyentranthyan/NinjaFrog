using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private GameObject m_objectPrefabs;
    [SerializeField] private int m_poolSize = 30;
    [SerializeField] private bool m_poolCanExpand = true;

    private List<GameObject> m_poolObject;
    private GameObject m_poolContainer;

    // Start is called before the first frame update
    void Start()
    {
        m_poolContainer = new GameObject("Pooler" + m_objectPrefabs.name);
        CreatePool();
    }

    public void CreatePool()
    {
        m_poolObject = new List<GameObject>();
        for(int i = 0; i < m_poolSize; i++)
		{
            AddObjectToPool();
		}
	}

    public GameObject AddObjectToPool()
	{
        GameObject newObject = Instantiate(m_objectPrefabs);
        newObject.SetActive(false);
        newObject.transform.SetParent(m_poolContainer.transform);
        m_poolObject.Add(newObject);
        return newObject;
	}

    public GameObject GetObjectToPool()
    {
        for (int i = 0; i < m_poolObject.Count; i++)
        {
			if (!m_poolObject[i].activeInHierarchy)
			{
                return m_poolObject[i];
			}
        }

		if (m_poolCanExpand)
		{
			return AddObjectToPool();
		}
		return null;
    }
}
