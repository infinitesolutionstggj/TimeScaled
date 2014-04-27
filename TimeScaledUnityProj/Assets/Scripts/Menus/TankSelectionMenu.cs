using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;

public class TankSelectionMenu : MonoBehaviour
{
	public List<MenuObject> tankMenuObjects = new List<MenuObject>();

    GameObject[] selectors = new GameObject[4];
    public GameObject MenuSelectorPrefab;
	int[] playerSelections = new int[4];

	bool[] selectorCoolDowns = new bool[4];

	void Awake()
	{
		for(int i = 0; i < playerSelections.Length; i++)
			playerSelections[i] = 0;

		for(int i = 0; i < selectorCoolDowns.Length; i++)
			selectorCoolDowns[i] = false;
	}

    void Start() 
	{
        for (int i = 0; i < selectors.Length; i++)
        {
            selectors[i] = Instantiate(MenuSelectorPrefab, tankMenuObjects[i].Position, Quaternion.identity) as GameObject;
            playerSelections[i] = i;
        }
	}

	void Update ()
	{
        int nextSlot = -1;
		for (int i = 0; i < playerSelections.Length; i++)
		{
			if(!selectorCoolDowns[i])
			{
				if (XCI.GetAxis(XboxAxis.LeftStickX, i+1) > 0.5)
				{
                    nextSlot = playerSelections[i];
                    do
                    {
                        nextSlot++;
                        if (nextSlot >= tankMenuObjects.Count)
                            nextSlot = 0;
                        else if (nextSlot < 0)
                            nextSlot = tankMenuObjects.Count - 1;
                    } while (isOccupied(nextSlot));

                    playerSelections[i] = nextSlot;

                    selectors[i].transform.position = tankMenuObjects[playerSelections[i]].Position;

					selectorCoolDowns[i] = true;
					StartCoroutine(SelectionWait(i));
					Debug.Log("Player " + i + " index: " + playerSelections[i]);
				}
				else if(XCI.GetAxis(XboxAxis.LeftStickX, i+1) < -0.5)
				{
                    nextSlot = playerSelections[i];
                    do
                    {
                        nextSlot--;
                        if (nextSlot >= tankMenuObjects.Count)
                            nextSlot = 0;
                        else if (nextSlot < 0)
                            nextSlot = tankMenuObjects.Count - 1;
                    } while (isOccupied(nextSlot));

                    playerSelections[i] = nextSlot;

                    selectors[i].transform.position = tankMenuObjects[playerSelections[i]].Position;

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

    bool isOccupied(int idx)
    {
        for (int i = 0; i < playerSelections.Length; i++)
        {
            if (playerSelections[i] == idx) return true;
        }

        return false;
    }
}
