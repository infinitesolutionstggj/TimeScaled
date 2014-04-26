using UnityEngine;
using System.Collections;

public class TankSelector : MonoBehaviour
{
	public TankType type;
	public int PlayerID;
	public Vector3 location;
	public int selectedTank = 0;
	// <<< visual indicator here >>>

	// Use this for initialization
	void Start () 
	{
		location = TankSelectionMenu.tankModels[selectedTank];
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void MoveDir(int dir, bool isHorizontal)
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

		location = TankSelectionMenu.tankModels[selectedTank];
	}
}
