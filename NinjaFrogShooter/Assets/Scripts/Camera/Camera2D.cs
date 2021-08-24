using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private bool horizontalFollow = true;
    [SerializeField] private bool verticalFollow = true;

    [Header("Horizontal")]
    [SerializeField][Range(0, 1)] private float horizontalInfluence = 1f;
    [SerializeField]private float horizontalOffset = 0f;
    [SerializeField]private float horizontalSmoothness = 3f;

    [Header("Vertical")]
    [SerializeField] [Range(0, 1)] private float verticalInfluence = 1f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float verticalSmoothness = 3f;

    public PlayerMotor Target { get; set; }
    public Vector3 TargetPosition { get; set; }
    public Vector3 CameraTargetPosition { get; set; }

    private float targetHorizontalSmoothFollow;
    private float targetVerticalSmoothFollow;

    private void Update()
	{
        MoveCamera();
    }

    private void MoveCamera()
	{
		if (Target == null)
		{
            return;
		}

        TargetPosition = GetTargetPosition(Target);

        //Caculate Position
        CameraTargetPosition = new Vector3(TargetPosition.x, TargetPosition.y, 0f);

        //follow on select Axis
        float xPos = horizontalFollow ? CameraTargetPosition.x : transform.localPosition.x;
        float yPos = verticalFollow ? CameraTargetPosition.y : transform.localPosition.y;

        //set offset
        CameraTargetPosition += new Vector3(horizontalFollow ? horizontalOffset : 0f, verticalFollow ? verticalOffset : 0f, 0f);

        //set smooth value
        targetHorizontalSmoothFollow = Mathf.Lerp(targetHorizontalSmoothFollow, CameraTargetPosition.x, horizontalSmoothness * Time.deltaTime);
        targetVerticalSmoothFollow = Mathf.Lerp(targetVerticalSmoothFollow, CameraTargetPosition.y, verticalSmoothness * Time.deltaTime);

        //get direction towards target pos
        float xDirection = targetHorizontalSmoothFollow - transform.localPosition.x;
        float yDirection = targetVerticalSmoothFollow - transform.localPosition.y;
        Vector3 deltaPosition = new Vector3(xDirection, yDirection, 0f);

        //new position
        Vector3 newCameraPosition = transform.localPosition + deltaPosition;

        //apply new position
        transform.localPosition = new Vector3(newCameraPosition.x, newCameraPosition.y, transform.localPosition.z);
    }

	private Vector3 GetTargetPosition(PlayerMotor player)
	{
        float xPos = 0f;
        float yPos = 0f;

        xPos += (player.transform.position.x + horizontalOffset) * horizontalInfluence;
        yPos += (player.transform.position.y + verticalOffset) * verticalInfluence;

        Vector3 positionTarget = new Vector3(xPos, yPos, transform.position.z);
        return positionTarget;
	}

    private void CenterToTarget(PlayerMotor player)
	{
        Target = player;
        Vector3 targetPos = GetTargetPosition(Target);
        targetHorizontalSmoothFollow = targetPos.x;
        targetVerticalSmoothFollow = targetPos.y;
        transform.localPosition = targetPos;
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;
        Vector3 camPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 2f);
        Gizmos.DrawWireSphere(camPosition, 0.5f);
	}

    private void StopFollow(PlayerMotor player)
	{
        Target = null;
	}
    private void StartFollowing(PlayerMotor player)
    {
        Target = player;
        CenterToTarget(Target);
    }

	private void OnEnable()
	{
		LevelManager.OnPlayerSpawn += CenterToTarget;
		PlayerHealth.OnDeath += StopFollow;
        PlayerHealth.OnRevive += StartFollowing;
	}

	private void OnDisable()
	{
		LevelManager.OnPlayerSpawn -= CenterToTarget;
        PlayerHealth.OnDeath -= StopFollow;
        PlayerHealth.OnRevive -= StartFollowing;
	}
}
