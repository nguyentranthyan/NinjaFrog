using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
	public enum MoveDirections {LEFT, RIGHT }

    public List<Vector3> points = new List<Vector3>();

	[SerializeField] private float m_moveSpeed = 5f;
	[SerializeField] private float m_minDistanceToPoint = 0.01f;

	public float MoveSpeed => m_moveSpeed;
	public MoveDirections Direction { get; set; }

	private bool m_move;
	private int m_currentPoint = 0;
	private bool m_playing;
	private Vector3 m_currentPosition;
	private Vector3 m_previousPosition;

	private void Start()
	{
		m_playing = true;
		m_previousPosition = transform.position;

		m_currentPosition = transform.position;
		transform.position = m_currentPosition + points[0];
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		//set first position
		if (!m_move)
		{
			transform.position = m_currentPosition + points[0];
			m_currentPoint++;
			m_move = true;
		}

		//move next point
		transform.position = Vector3.MoveTowards(transform.position, m_currentPosition + points[m_currentPoint], Time.deltaTime * m_moveSpeed);

		//Evalute move to next point
		float distanceNextPoint = Vector3.Distance(m_currentPosition + points[m_currentPoint], transform.position);
		if (distanceNextPoint < m_minDistanceToPoint)
		{
			m_previousPosition = transform.position;
			m_currentPoint++;
		}

		//Define move direction
		if (m_previousPosition != Vector3.zero)
		{
			if (transform.position.x > m_previousPosition.x)
			{
				Direction = MoveDirections.RIGHT;
			}
			else if(transform.position.x < m_previousPosition.x)
			{
				Direction = MoveDirections.LEFT;
			}
		}

		//if we are on the last point, reset our position to the first one
		if (m_currentPoint == points.Count)
		{
			m_currentPoint = 0;
		}
	}

	private void OnDrawGizmos()
	{
		if(transform.hasChanged && !m_playing)
		{
			m_currentPosition = transform.position;
		}

		if (points != null)
		{
			for(int i=0; i < points.Count; i++)
			{
				if (i < points.Count)
				{
					//Draw Point
					Gizmos.color = Color.red;
					Gizmos.DrawWireSphere(m_currentPosition + points[i], 0.4f);

					//Draw Lines
					Gizmos.color = Color.black;
					if (i < points.Count - 1)
					{
						Gizmos.DrawLine(m_currentPosition + points[i], m_currentPosition + points[i + 1]);
					}

					//Draw line from last point to first point
					if (i == points.Count-1)
					{
						Gizmos.DrawLine(m_currentPosition + points[i], m_currentPosition + points[0]);
					}
				}
			}
		}
	}
}
