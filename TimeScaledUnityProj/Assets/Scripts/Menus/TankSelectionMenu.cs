using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;

public class TankSelectionMenu : MonoBehaviour
{
	List<MenuObject> tankMenuObjects = new List<MenuObject>();

	int[] playerSelections = new int[4];

	bool[] selectorCoolDowns = new bool[4];

	void Awake()
	{
		for(int i = 0; i < playerSelections.Length; i++)
			playerSelections[i] = 0;

		for(int i = 0; i < selectorCoolDowns.Length; i++)
			selectorCoolDowns[i] = false;
	}

	void Start () 
	{
		GameObject[] foundObjects;
		foundObjects = GameObject.FindGameObjectsWithTag("MenuObject");

		for(int i = 0; i < foundObjects.Length; i++)
		{
			if(foundObjects[i].GetComponent<MenuObject>() != null && foundObjects[i].activeInHierarchy)
			{
				tankMenuObjects.Add(foundObjects[i].GetComponent<MenuObject>());
			}
		}
	}

	void Update ()
	{
		for (int i = 0; i < playerSelections.Length; i++)
		{
			if(!selectorCoolDowns[i])
			{
				if (XCI.GetAxis(XboxAxis.LeftStickX, i+1) > 0.5)
				{
					//playerSelectors[i-1].MoveDir((int)Mathf.Sign(XCI.GetAxis(XboxAxis.RightStickX, i)), true);
					playerSelections[i]++;
					if(playerSelections[i] >= tankMenuObjects.Count)
						playerSelections[i] = 0;
					else if(playerSelections[i] < 0)
						playerSelections[i] = tankMenuObjects.Count - 1;

					selectorCoolDowns[i] = true;
					StartCoroutine(SelectionWait(i));
					Debug.Log("Player " + i + " index: " + playerSelections[i]);
				}
				else if(XCI.GetAxis(XboxAxis.LeftStickX, i+1) < -0.5)
				{
					playerSelections[i]--;
					if(playerSelections[i] >= tankMenuObjects.Count)
						playerSelections[i] = 0;
					else if(playerSelections[i] < 0)
						playerSelections[i] = tankMenuObjects.Count - 1;

					selectorCoolDowns[i] = true;
					StartCoroutine(SelectionWait(i));
					Debug.Log("Player " + i + " index: " + playerSelections[i]);
				}
			}
		}
	}

	IEnumerator SelectionWait(int playerIndex)
	{
		while(Mathf.Abs(XCI.GetAxis(XboxAxis.LeftStickX, playerIndex+1)) > 0.5)
		{
			yield return null;
		}

		selectorCoolDowns[playerIndex] = false;
	}
}
