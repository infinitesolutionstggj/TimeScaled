using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GameObject testPrefab;
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () 
	{
		NetworkManager.Main.SpawnObject(playerPrefab, Vector3.zero, Quaternion.identity);
	}
	
	void Update()
	{
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
