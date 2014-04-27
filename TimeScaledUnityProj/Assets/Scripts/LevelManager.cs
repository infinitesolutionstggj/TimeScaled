using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    //bool lastTankStanding = false;

	// Use this for initialization
	void Start ()
    {
        Player tempPlayer;
	    // spawn players
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn Point");
        for(int i = 0; i < spawns.Length; i++)
        {
            tempPlayer = Instantiate(GameObject.Find("GameManager").GetComponent<GameManager>().playerPrefab, spawns[i].transform.position, Quaternion.identity) as Player;
            tempPlayer.playerNumber = i + 1;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*if (GameObject.FindObjectsOfType<Player>().Length < 2)
            Application.LoadLevel("End");*/

	    /*foreach (Player p in GameObject.FindObjectsOfType<Player>())
        {

        }*/
	}

    void FixedUpdate()
    {
        if (GameObject.FindObjectsOfType<Player>().Length <= -1)
            Application.LoadLevel("End");
    }
}
