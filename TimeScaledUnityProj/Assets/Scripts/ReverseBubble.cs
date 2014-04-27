using UnityEngine;
using System.Collections;

public class ReverseBubble : Bubble
{
	protected override void Awake()
	{
		base.Awake();

		renderer.material = ReverseMat;
	}

	protected override void Start()
	{
		if (lifeSpan <= 0 || lifeSpan > 10f)
			lifeSpan = 10f;

		base.Start();
	}

	void OnDestroy()
	{
		foreach (TimeScaledObject obj in AffectedObjects)
		{
			obj.RemoveReverseBubble(this);
		}
		AffectedObjects.Clear();
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
