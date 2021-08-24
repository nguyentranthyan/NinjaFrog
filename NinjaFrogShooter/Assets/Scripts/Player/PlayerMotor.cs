using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    /// <summary>
    /// Set States Player Controller
    /// </summary>
    private PlayerStates[] m_playerStates;

    // Start is called before the first frame update
    void Start()
    {
        m_playerStates = GetComponents<PlayerStates>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_playerStates.Length != 0)
		{
            foreach(PlayerStates states in m_playerStates)
			{
                states.LocalInput();
                states.ExcuteState();
                states.SetAnimation();
			}
		}
    }

    public void SpawnPlayer(Transform newPosition)
	{
        transform.position = newPosition.position;
	}
}
