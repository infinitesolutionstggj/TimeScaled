using UnityEngine;
using System.Collections;

public class ReverseBubble : Bubble
{
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
		base.OnTriggerEnter2D(col);

		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			obj.RemoveReverseBubble(this);
		}
	}

	protected override void OnTriggerExit2D(Collider2D col)
	{
		base.OnTriggerExit2D(col);

		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			obj.RemoveReverseBubble(this);
		}
	}
}
