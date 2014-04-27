using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager Main = null;

	public bool debugInvincible = false;
	public GameObject testPrefab;
	public GameObject detonator;
	public GameObject playerPrefab;

	void Awake()
	{
		if (Main == null)
			Main = this;
		else
			Destroy(this.gameObject);

		Time.fixedDeltaTime = GameSettings.FIXED_DELTA_TIME;

		if (!AudioManager.IsPlaying)
		{
			AudioManager.PlayBGMusicByIndex(0);
		}
	}

	void OnDestroy()
	{
		if (Main == this)
			Main = null;
	}

	// Use this for initialization
	void Start () 
	{
		//Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
			detonator.GetComponent<TimeBubbleSpawner>().Detonate();
	}
}
