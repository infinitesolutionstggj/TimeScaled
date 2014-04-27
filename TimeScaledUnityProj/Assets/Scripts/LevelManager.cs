using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    //bool lastTankStanding = false;

	// Use this for initialization
	void Start ()
    {
        //Player tempPlayer;
	    // spawn players
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn Point");
        GameObject playerPrefab = GameObject.Find("GameManager").GetComponent<GameManager>().playerPrefab;
        if (playerPrefab)
        {
            Debug.Log("mooo   " + spawns.Length);
            for (int i = 0; i < spawns.Length; i++)
            {
                Debug.Log("weeee    " + i);
                //tempPlayer = Instantiate(playerPrefab, spawns[i].transform.position, Quaternion.identity) as Player;
                //tempPlayer.playerNumber = i + 1;
                playerPrefab.GetComponent<Player>().playerNumber = i+1;
                Instantiate(playerPrefab, spawns[i].transform.position, Quaternion.identity);
            }
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
