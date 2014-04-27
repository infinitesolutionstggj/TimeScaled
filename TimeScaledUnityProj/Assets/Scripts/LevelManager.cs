using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    //bool lastTankStanding = false;

	// Use this for initialization
	void Start ()
    {
	
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
