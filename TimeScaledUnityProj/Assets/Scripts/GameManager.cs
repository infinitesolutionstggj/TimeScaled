using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	void Update()
	{

	}

	void LateUpdate () 
	{
		AdjustCamera();
	}

	void AdjustCamera()
	{
		Vector3 playerPos = Player.Main.transform.position;
		Vector3 moveVec = Vector3.zero;
		Vector3 velocity = Vector3.zero;
		float smoothTime = 0.25f;

		moveVec = Vector3.SmoothDamp(Camera.main.transform.position, playerPos, ref velocity, smoothTime);
		moveVec.z = Camera.main.transform.position.z;

		if (Camera.main.WorldToViewportPoint(playerPos).x > GameSettings.MIN_SCREEN_BOUNDS && Camera.main.WorldToViewportPoint(playerPos).x < GameSettings.MAX_SCREEN_BOUNDS)
		{
			moveVec.x = Camera.main.transform.position.x;
		}

		if (Camera.main.WorldToViewportPoint(playerPos).y > GameSettings.MIN_SCREEN_BOUNDS && Camera.main.WorldToViewportPoint(playerPos).y < GameSettings.MAX_SCREEN_BOUNDS)
		{
			moveVec.y = Camera.main.transform.position.y;
		}

		if (moveVec != Camera.main.transform.position)
		{
			Camera.main.transform.position = moveVec;
		}
	}
}
