using UnityEngine;
using System.Collections;

public class TankSelector : MonoBehaviour
{
	public GameSettings.TankType type;
	public int PlayerID;
	public Vector3 location;
	public int selectedTank = 0;
	// <<< visual indicator here >>>

	// Use this for initialization
	void Start () 
	{
		//location = TankSelectionMenu.tankModelPositions[selectedTank];
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void MoveDir(int dir, bool isHorizontal)
	{
		if (isHorizontal)
		{
			if(dir > 0)
				selectedTank++;
			else
				selectedTank--;
		}
		else
		{

		}

		//location = TankSelectionMenu.tankModelPositions[selectedTank];
	}
}
