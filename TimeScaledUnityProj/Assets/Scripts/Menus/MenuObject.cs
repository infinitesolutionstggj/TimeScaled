using UnityEngine;
using System.Collections;

public class MenuObject : MonoBehaviour 
{
	public Vector3 Position {get; private set;}

	public GameSettings.TankType ModelType {get; private set;}

	void Awake()
	{
		Position = Vector3.zero;
		ModelType = GameSettings.TankType.None;
	}

	// Use this for initialization
	void Start () 
	{
		Position = transform.position;

		switch(gameObject.name)
		{
		case "CryoTankSelection":
			ModelType = GameSettings.TankType.Cryo;
			break;
		case "DisruptorTankSelection":
			ModelType = GameSettings.TankType.Disruptor;
			break;
		case "MammothTankSelection":
			ModelType = GameSettings.TankType.Mammoth;
			break;
		case "PrejudiceTankSelection":
			ModelType = GameSettings.TankType.Prejudice;
			break;
		case "PrismTankSelection":
			ModelType = GameSettings.TankType.Prism;
			break;
		case "RegressorTankSelection":
			ModelType = GameSettings.TankType.Regressor;
			break;
		default:
			ModelType = GameSettings.TankType.None;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
