using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    //bool lastTankStanding = false;

	// Use this for initialization
	void Start ()
    {
	    // spawn players
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn Point");
        string tankName = "";
        GameObject playerPrefab = null;
        GameObject spawnedObj = null;

        //if (GameManager.Main)
            //playerPrefab = GameManager.Main.playerPrefab;

        //if (playerPrefab)
        //{
        Debug.Log(spawns.Length);
            for (int i = 0; i < spawns.Length; i++)
            {
                Debug.Log("mooooo    " + GameSettings.PlayerInfos[i].TankType);
                if (GameSettings.PlayerInfos[i].TankType != GameSettings.TankType.None)
                {
                    switch (GameSettings.PlayerInfos[i].TankType)
                    {
                        case GameSettings.TankType.Cryo:
                            tankName = "Tanks/Cryo";
                            break;
                        case GameSettings.TankType.Disruptor:
                            tankName = "Tanks/Disruptor";
                            break;
                        case GameSettings.TankType.Mammoth:
                            tankName = "Tanks/Mammoth";
                            break;
                        case GameSettings.TankType.Prejudice:
                            tankName = "Tanks/Prejudice";
                            break;
                        case GameSettings.TankType.Prism:
                            tankName = "Tanks/Prism";
                            break;
                        case GameSettings.TankType.Regressor:
                            tankName = "Tanks/Regressor";
                            break;
                        default:
                            //ModelType = GameSettings.TankType.None;
                            break;
                    }

                    if (tankName != "")
                    {
                        Debug.Log(GameSettings.PlayerInfos[i].PlayerID);
                        playerPrefab = Resources.Load(tankName) as GameObject;
                        spawnedObj = Instantiate(playerPrefab, spawns[i].transform.position, Quaternion.identity) as GameObject;
                        spawnedObj.GetComponent<Player>().playerNumber = GameSettings.PlayerInfos[i].PlayerID;
                        tankName = "";
                    }
                }
            }
        //}
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
