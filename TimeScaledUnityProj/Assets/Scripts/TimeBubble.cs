using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeBubble : Bubble 
{
	public float timeScaleMultiplier = 1.0f;
	public float innerRadiusPercent = 0.5f;
	public float InnerRadius { get { return OuterRadius * innerRadiusPercent; } }

	protected override void Awake ()
	{
		base.Awake();

		affectedObjects = new List<TimeScaledObject>();
		innerRadiusPercent = Mathf.Clamp01(innerRadiusPercent);
	}

	void OnDestroy()
	{
		foreach (TimeScaledObject obj in affectedObjects)
		{
			obj.RemoveTimeBubble(this);
		}
		affectedObjects.Clear();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			Destroy(this.gameObject);
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.DrawWireSphere(this.transform.position, InnerRadius);
	}
	
	protected override void OnTriggerEnter2D(Collider2D col)
	{
		base.OnTriggerEnter2D(col);

		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			obj.AddTimeBubble(this);
		}
	}

	protected override void OnTriggerExit2D(Collider2D col)
	{
		base.OnTriggerExit2D(col);

		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			obj.RemoveTimeBubble(this);
		}
	}

	public float GetTimeScaleForObject(GameObject gameObj)
	{
		Vector2 position = (Vector2)transform.position;
		Vector2 gameObjPos = (Vector2)gameObj.transform.position;
		float t = Mathf.InverseLerp(OuterRadius, InnerRadius, Vector2.Distance(position, gameObjPos));

		return Mathf.Lerp(1.0f, timeScaleMultiplier, Mathf.Clamp01(t));
	}
}
