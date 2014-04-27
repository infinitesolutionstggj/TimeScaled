using UnityEngine;
using System.Collections;

public class PlayerInfo
{
    public int PlayerID = 0;
    public GameSettings.TankType TankType = GameSettings.TankType.None;
    public string PlayerName = "";
}

public class GameManager : MonoBehaviour
{
	public static GameManager Main = null;

	public bool debugInvincible = false;
	public GameObject testPrefab;
	public GameObject detonator;
	public GameObject playerPrefab;
    public PlayerInfo[] selectedTanks = new PlayerInfo[4];

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

        for (int i = 0; i < 4; i++)
            selectedTanks[i] = new PlayerInfo();
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
