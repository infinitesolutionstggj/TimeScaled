using UnityEngine;
using System.Collections;

public class ReverseBubble : Bubble
{
	protected override void Awake()
	{
		base.Awake();

		if (lifeSpan <= 0 || lifeSpan > 10f)
			lifeSpan = 10f;

		renderer.material = ReverseMat;
	}

	void OnDestroy()
	{
		foreach (TimeScaledObject obj in affectedObjects)
		{
			obj.RemoveReverseBubble(this);
		}
		affectedObjects.Clear();
	}

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Reverse Trigger Entered");

		base.OnTriggerEnter2D(col);

		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			obj.AddReverseBubble(this);
		}
	}

	protected override void OnTriggerExit2D(Collider2D col)
	{
		Debug.Log("Reverse Trigger Exited");

		base.OnTriggerExit2D(col);

		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			obj.RemoveReverseBubble(this);
		}
	}
}
