using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GameObject testPrefab;
	public GameObject playerPrefab;
	bool spawnedPlayer = false;

	void Awake()
	{
		
	}

	void OnDestroy()
	{
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	void Update()
	{
		Debug.Log(networkView.isMine.ToString());
		if (!networkView.isMine)
		{
			enabled = false;
			return;
		}
		else
		{
			if (!spawnedPlayer)
			{
				NetworkManager.SpawnObject(playerPrefab, Vector3.zero, Quaternion.identity);
				spawnedPlayer = true;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			var tempObj = Instantiate(testPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tempObj.GetComponent<ReverseBubble>().lifeSpan = 300f;
			Vector3 objScale = Vector3.one * 30;
			objScale.z = 10f;
			tempObj.transform.localScale = objScale;
		}
	}
}
