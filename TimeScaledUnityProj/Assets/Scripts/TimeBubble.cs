using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class TimeBubble : MonoBehaviour 
{
	public float timeScaleMultiplier = 1.0f;
	public float innerRadius = 0;
	public float OuterRadius { get { return ((CircleCollider2D)collider2D).radius * Mathf.Max(transform.localScale.x, transform.localScale.y); } }

	void Awake () 
	{
		collider2D.isTrigger = true;
		if (innerRadius > OuterRadius)
		{
			innerRadius = OuterRadius;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(this.transform.position, innerRadius);
		Gizmos.DrawWireSphere(this.transform.position, OuterRadius);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Trigger Entered");

		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			obj.AddTimeBubble(this);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		Debug.Log("Trigger Exited");

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
		float t = Mathf.InverseLerp(OuterRadius, innerRadius, Vector2.Distance(position, gameObjPos));

		return Mathf.Lerp(1.0f, timeScaleMultiplier, Mathf.Clamp01(t));
	}
}
