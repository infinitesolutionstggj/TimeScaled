using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GameObject testPrefab;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			var tempObj = Instantiate(testPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tempObj.GetComponent<ReverseBubble>().lifeSpan = 3f;
			Vector3 objScale = Vector3.one * 30;
			objScale.z = 10f;
			tempObj.transform.localScale = objScale;
		}
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

		if (Camera.main.WorldToViewportPoint(playerPos).x > GameSettings.MIN_SCREEN_BUFFER && Camera.main.WorldToViewportPoint(playerPos).x < GameSettings.MAX_SCREEN_BUFFER)
		{
			moveVec.x = Camera.main.transform.position.x;
		}

		if (Camera.main.WorldToViewportPoint(playerPos).y > GameSettings.MIN_SCREEN_BUFFER && Camera.main.WorldToViewportPoint(playerPos).y < GameSettings.MAX_SCREEN_BUFFER)
		{
			moveVec.y = Camera.main.transform.position.y;
		}

		if (moveVec != Camera.main.transform.position)
		{
			Camera.main.transform.position = moveVec;
		}
	}
}
