﻿using UnityEngine;
using System.Collections.Generic;

public class TimeScaledObject : MonoBehaviour
{
	public float initialTimeScale = 1.0f;
	public float LocalTimeScale { get; set; }
	public float LocalFixedDeltaTime
	{
		get { return Time.fixedDeltaTime * LocalTimeScale; }
	}
	protected List<TimeBubble> affectingTimeBubbles;
	protected List<ReverseBubble> affectingReverseBubbles;

	// Use this for initialization
	protected virtual void Awake () 
	{
		affectingTimeBubbles = new List<TimeBubble>();
		affectingReverseBubbles = new List<ReverseBubble>();
		LocalTimeScale = initialTimeScale;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (GameSettings.IsPaused)
			return;

		if (affectingTimeBubbles.Count == 0 && LocalTimeScale != 1.0f)
		{
			LocalTimeScale = 1.0f;
		}
		else
		{
			LocalTimeScale = CalculateTimeScale();
		}
		LocalTimeScale = Mathf.Clamp(LocalTimeScale, GameSettings.MIN_TIME_SCALE, GameSettings.MAX_TIME_SCALE);
	}

	protected virtual void LateUpdate()
	{

	}

	protected virtual void FixedUpdate()
	{

	}

	public void AddTimeBubble(TimeBubble timeBubble)
	{
		foreach (TimeBubble tb in affectingTimeBubbles)
		{
			if (tb == timeBubble)
				return;
		}

		affectingTimeBubbles.Add(timeBubble);
	}

	public void RemoveTimeBubble(TimeBubble timeBubble)
	{
		affectingTimeBubbles.Remove(timeBubble);
	}

	public void AddReverseBubble(ReverseBubble reverseBubble)
	{
		foreach (ReverseBubble rb in affectingReverseBubbles)
		{
			if (rb == reverseBubble)
				return;
		}

		affectingReverseBubbles.Add(reverseBubble);
	}

	public void RemoveReverseBubble(ReverseBubble reverseBubble)
	{
		affectingReverseBubbles.Remove(reverseBubble);
	}

	protected float CalculateTimeScale()
	{
		// Add timeScale buggery here
		float output = 1.0f;

		foreach (TimeBubble tb in affectingTimeBubbles)
		{
			output *= tb.GetTimeScaleForObject(gameObject);
		}

		return output;
	}
}
