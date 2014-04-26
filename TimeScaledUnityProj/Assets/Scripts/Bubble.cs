﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class Bubble : MonoBehaviour
{
	public static Material SlowMat;
	public static Material FastMat;
	public static Material ReverseMat;

	public float OuterRadius { get { return ((CircleCollider2D)collider2D).radius * Mathf.Max(transform.localScale.x, transform.localScale.y); } }
	protected List<TimeScaledObject> affectedObjects;
	public float lifeSpan = 0f;

	protected virtual void Awake()
	{
		if (SlowMat == null || FastMat == null || ReverseMat == null)
		{
			SlowMat = Resources.Load<Material>("SlowBubbleMat");
			FastMat = Resources.Load<Material>("FastBubbleMat");
			ReverseMat = Resources.Load<Material>("ReverseBubbleMat");
		}
		collider2D.isTrigger = true;
		affectedObjects = new List<TimeScaledObject>();
	}

	protected virtual void Start()
	{
		if (lifeSpan > 0)
			Destroy(this.gameObject, lifeSpan);
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			affectedObjects.Add(obj);
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D col)
	{
		TimeScaledObject obj = col.gameObject.GetComponent<TimeScaledObject>();

		if (obj)
		{
			affectedObjects.Remove(obj);
		}
	}

	protected virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		Gizmos.DrawWireSphere(this.transform.position, OuterRadius);
	}
}
