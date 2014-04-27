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

		innerRadiusPercent = Mathf.Clamp01(innerRadiusPercent);
	}

	protected override void Start()
	{
		base.Start();

		ResetMaterial();
	}

	void OnDestroy()
	{
		foreach (TimeScaledObject obj in AffectedObjects)
		{
			obj.RemoveTimeBubble(this);
		}
		AffectedObjects.Clear();
	}

	void Update()
	{
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
		t = Mathf.Lerp(1.0f, timeScaleMultiplier, Mathf.Clamp01(t));

		foreach (var prism in Prism.All)
			if (prism.BubbleTimeScale)
			{
				if (t < 1)
					t /= prism.bubbleTimeScaleMultiplier;
				else
					t *= prism.bubbleTimeScaleMultiplier;
			}

		return t;
	}

	public void ResetMaterial()
	{
		if (timeScaleMultiplier < 1)
			renderer.material = SlowMat;
		else
			renderer.material = FastMat;
	}
}
