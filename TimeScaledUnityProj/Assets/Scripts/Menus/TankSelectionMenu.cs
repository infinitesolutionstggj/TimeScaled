using UnityEngine;
using System.Collections;
using XboxCtrlrInput;


public class TankSelectionMenu : MonoBehaviour
{
	TankSelector[] playerSelectors = new TankSelector[4];

	public static Vector3[] tankModels = new Vector3[5];

	// Use this for initialization
	void Start () 
	{
		tankModels[0] = Vector3(-3, 0, 0);
		tankModels[1] = Vector3( 0, 0, 0);
		tankModels[2] = Vector3( 3, 0, 0);
		tankModels[3] = Vector3(-3, 0, -3);
		tankModels[4] = Vector3(0, 0, -3);
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 1; i <= 4; i++)
		{
			if (Mathf.Abs(XCI.GetAxis(XboxAxis.RightStickX, i)) > 0.5)
			{
				playerSelectors[i-1].MoveDir(Mathf.Sign(XCI.GetAxis(XboxAxis.RightStickX, i)), true);
			}

			if (Mathf.Abs(XCI.GetAxis(XboxAxis.RightStickY, i)) > 0.5)
			{
				playerSelectors[i-1].MoveDir(Mathf.Sign(XCI.GetAxis(XboxAxis.RightStickY, i)), false);
			}

			if (Mathf.Abs(XCI.GetAxis(XboxAxis.LeftStickX, i)) > 0.5)
			{
				playerSelectors[i-1].MoveDir(Mathf.Sign(XCI.GetAxis(XboxAxis.LeftStickX, i)), true);
			}

			if (Mathf.Abs(XCI.GetAxis(XboxAxis.LeftStickY, i)) > 0.5)
			{
				playerSelectors[i-1].MoveDir(Mathf.Sign(XCI.GetAxis(XboxAxis.LeftStickY, i)), false);
			}
		}
	}
}
