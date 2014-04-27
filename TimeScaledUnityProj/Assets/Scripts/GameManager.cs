using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public GameObject testPrefab;
	public GameObject detonator;
	public GameObject playerPrefab;

	void Awake()
	{
		
	}

	void OnDestroy()
	{
	}

	// Use this for initialization
	void Start () 
	{
		Instantiate(playerPrefab, new Vector3(0, 0, -2), Quaternion.identity);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (AudioManager.IsPlaying)
			{
				AudioManager.StopBGMusic();
			}
			else
			{
				AudioManager.PlayBGMusicByIndex(0);
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			AudioManager.PlayClipByIndex(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			AudioManager.PlayClipByIndex(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			AudioManager.PlayClipByIndex(2);
		}
		if (Input.GetKeyDown(KeyCode.Return))
			detonator.GetComponent<TimeBubbleSpawner>().Detonate();
	}
}
