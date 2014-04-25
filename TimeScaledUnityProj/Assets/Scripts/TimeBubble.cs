using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class TimeBubble : MonoBehaviour 
{
	public float timeScaleMultiplier = 1.0f;
	public float innerRadius = 0;

	void Awake () 
	{
		collider2D.isTrigger = true;
		if (innerRadius > ((CircleCollider2D)collider2D).radius * Mathf.Max(transform.localScale.x, transform.localScale.y))
		{
			innerRadius = ((CircleCollider2D)collider2D).radius * Mathf.Max(transform.localScale.x, transform.localScale.y);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(this.transform.position, innerRadius);
		Gizmos.DrawWireSphere(this.transform.position, ((CircleCollider2D)collider2D).radius * Mathf.Max(transform.localScale.x, transform.localScale.y));
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
}
