﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class Bubble : MonoBehaviour
{
	public float OuterRadius { get { return ((CircleCollider2D)collider2D).radius * Mathf.Max(transform.localScale.x, transform.localScale.y); } }
	public List<TimeScaledObject> affectedObjects;

	protected virtual void Awake()
	{
		collider2D.isTrigger = true;
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
