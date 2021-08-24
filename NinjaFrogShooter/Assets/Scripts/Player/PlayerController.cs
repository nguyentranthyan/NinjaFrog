using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region RayCast Player 
    private BoxCollider2D m_boxcolider2D;

    [Header("Bounds BoxCollider Player")]
    private Vector2 m_boundsTopLeft;
    private Vector2 m_boundsTopRight;
    private Vector2 m_boundsBottomLeft;
    private Vector2 m_boundsBottomRight;

    private float m_boundsWidth;
    private float m_boundsHeight;

	public float BoundsWidth { get => m_boundsWidth; set => m_boundsWidth = value; }
	public float BoundsHeight { get => m_boundsHeight; set => m_boundsHeight = value; }
    #endregion

    #region Gravity Player
    [Header("Gravity Player")]
    [SerializeField] private float gravity = -20f;

    private float m_currentGravity;

    //Jump
    private float m_fallMultiplier = 1.5f;

    //Wallcling
    private float m_WallMultiplier;

    private Vector2 m_force; // the force apply in both X and Y axis
    private Vector2 m_movePosition;

    public float Gravity => gravity;
    public Vector2 Force => m_force;
    #endregion

    #region Collision
    [Header("Collisions")]
    [SerializeField] private LayerMask coliderWith;
    [SerializeField] private int verticalRayAmount = 4;
    [SerializeField] private int horizontalRayAmount = 4;

    private float m_padding = 0.05f; // Use a tiny number, like 0.1f
    public bool FacingRight { get; set; }
    private int facingRightDirection = 1;
    #endregion

    #region PlayerConditions
    private PlayerConditions m_conditions;
    public PlayerConditions Conditions => m_conditions;
	#endregion

	// Start is called before the first frame update
	void Start()
    {
        m_boxcolider2D = GetComponent<BoxCollider2D>();
        m_conditions = new PlayerConditions();
        m_conditions.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        StartMovement();

        SetRayOrigins();
        GetFaceDirection();

        CollisionBellow();
        CollisionAbove();
        transform.Translate(m_movePosition, Space.Self);

        SetRayOrigins();
        CalculateMovement();
    }

    #region SetForce
    public void SetHorizontalForce(float xForce)
    {
        m_force.x = xForce;
    }

    public void SetVerticalForce(float yForce)
    {
        m_force.y = yForce;
    }
    public void SetWallClingMultiplier(float fallM)
    {
        m_WallMultiplier = fallM;
    }
    #endregion

    #region Set Movement Gravity when Start
    private void StartMovement()
	{
        m_movePosition = m_force * Time.deltaTime;
        m_conditions.Reset();
    }

    private void ApplyGravity()
	{
        m_currentGravity = gravity;
        if (m_force.y < 0)
		{
            m_currentGravity *= m_fallMultiplier;
		}

		if (m_WallMultiplier != 0)
		{
            m_force.y *= m_WallMultiplier;
        }

        m_force.y += m_currentGravity * Time.deltaTime;
    }

    private void CalculateMovement()
	{
		if (Time.deltaTime > 0)
		{
            m_force = m_movePosition / Time.deltaTime;
		}
	}
	#endregion

	#region Ray Origins
	private void SetRayOrigins()
    {
        Bounds playerBounds = m_boxcolider2D.bounds;

        m_boundsTopLeft = new Vector2(playerBounds.min.x, playerBounds.max.y);
        m_boundsTopRight = new Vector2(playerBounds.max.x, playerBounds.max.y);

        m_boundsBottomLeft = new Vector2(playerBounds.min.x, playerBounds.min.y);
        m_boundsBottomRight = new Vector2(playerBounds.max.x, playerBounds.min.y);

        BoundsHeight = Vector2.Distance(m_boundsBottomLeft, m_boundsTopLeft);
        BoundsWidth = Vector2.Distance(m_boundsBottomLeft, m_boundsBottomRight);
    }
	#endregion

	#region RayCast Collision Bellow
    private void CollisionBellow()
	{
        if (m_movePosition.y < -0.0001f)
        {
            m_conditions.IsFalling = true;
        }
        else
        {
            m_conditions.IsFalling = false;
        }

        if (!m_conditions.IsFalling)
        {
            m_conditions.IsCollidingBellow = false;
            return;
        }

        //Calculate ray lenght
        float rayLenght = m_boundsHeight / 2f + m_padding;
		if (m_movePosition.y < 0)
		{
            rayLenght += Mathf.Abs(m_movePosition.y);
		}

        //Calculate ray origin
        Vector2 leftOrigin = (m_boundsBottomLeft + m_boundsTopLeft) / 2f;
        Vector2 rightOrigin = (m_boundsBottomRight + m_boundsTopRight) / 2f;

        leftOrigin += (Vector2) (transform.up * m_padding) + (Vector2)(transform.right * m_movePosition.x);
        rightOrigin += (Vector2) (transform.up * m_padding) + (Vector2)(transform.right * m_movePosition.x);

        //RayCast
        for(int i = 0; i < verticalRayAmount; i++)
		{
            Vector2 rayOrigin = Vector2.Lerp(leftOrigin, rightOrigin, (float) i / (float)(verticalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -transform.up, rayLenght, coliderWith);
            Debug.DrawRay(rayOrigin, -transform.up * rayLenght, Color.green);

			if (hit)
			{
				if (m_force.y > 0)
				{
                    m_movePosition.y = m_force.y * Time.deltaTime;
                    m_conditions.IsCollidingBellow = false;
				}
				else
				{
                    m_movePosition.y = -hit.distance + m_boundsHeight / 2f + m_padding;
                }

                m_conditions.IsCollidingBellow = true;
                m_conditions.IsFalling = false;

                if (Mathf.Abs(m_movePosition.y) < 0.0001f)
				{
                    m_movePosition.y = 0f;
				}
			}
		}
    }
    #endregion

    #region Get Face Direction
    private void GetFaceDirection()
    {
        FacingRight = true;
        FacingRight = facingRightDirection == 1;

        if (m_force.x > 0.0001f)
        {
            FacingRight = true;
            facingRightDirection = 1;
            CollisionHorizontal(facingRightDirection);
        }
        else if (m_force.x < -0.0001f)

        {
            FacingRight = false;
            facingRightDirection = -1;
            CollisionHorizontal(facingRightDirection);
        }
    }
    #endregion

    #region RayCast Collision Horizontal
    private void CollisionHorizontal(int direction)
    {
        //Calculate ray lenght
        float rayLenght = Mathf.Abs(m_force.x * Time.deltaTime) + m_boundsWidth / 2f + m_padding * 2f;

        //Calculate ray origin
        Vector2 hRayTop = (m_boundsTopLeft + m_boundsTopRight) / 2f;
        Vector2 hRayBottom = (m_boundsBottomLeft + m_boundsBottomRight) / 2f;

        hRayTop -= (Vector2)transform.up * m_padding;
        hRayBottom += (Vector2)transform.up * m_padding;

        //RayCast
        for (int i = 0; i < horizontalRayAmount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(hRayTop, hRayBottom, (float)i / (float)(horizontalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction * transform.right, rayLenght, coliderWith);
            Debug.DrawRay(rayOrigin, direction * transform.right * rayLenght, Color.cyan);

            if (hit)
            {
                if (direction >= 0)
                {
                    m_movePosition.x = hit.distance - m_boundsWidth / 2f - m_padding * 2f;
                    m_conditions.IsCollidingRight = true;
                }
				else
				{
                    m_movePosition.x = - hit.distance + m_boundsWidth / 2f + m_padding * 2f;
                    m_conditions.IsCollidingLeft = true;
                }
                m_force.x = 0f;
            }
        }
    }
    #endregion

    #region RayCast Collision Above
    private void CollisionAbove()
    {
        //Calculate ray lenght
        float rayLenght = m_movePosition.y + m_boundsHeight / 2f;

        if (m_movePosition.y < 0)
        {
            return;
        }

        //Calculate ray origin
        Vector2 vRayTLeft = (m_boundsBottomLeft + m_boundsTopLeft) / 2f;
        Vector2 vRayTRight = (m_boundsBottomRight + m_boundsTopRight) / 2f;

        vRayTLeft += (Vector2) transform.right * m_movePosition.x;
        vRayTRight += (Vector2) transform.right *m_movePosition.x;

        //RayCast
        for (int i = 0; i < verticalRayAmount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(vRayTLeft, vRayTRight, (float)i / (float)(verticalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, transform.up, rayLenght, coliderWith);
            Debug.DrawRay(rayOrigin, transform.up * rayLenght, Color.red);

            if (hit)
            {
                m_movePosition.y = hit.distance - m_boundsHeight / 2f;
                m_conditions.IsCollidingAbove = true;
            }
        }
    }
    #endregion

}
