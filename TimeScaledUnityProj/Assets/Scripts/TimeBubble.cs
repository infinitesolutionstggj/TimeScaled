using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class TimeBubble : MonoBehaviour 
{
	public float timeScaleMultiplier = 1.0f;

	void Awake () 
	{
		collider2D.isTrigger = true;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Trigger Entered");

		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			obj.LocalTimeScale *= timeScaleMultiplier;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		Debug.Log("Trigger Exited");

		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			obj.LocalTimeScale /= timeScaleMultiplier;
		}
	}
}
