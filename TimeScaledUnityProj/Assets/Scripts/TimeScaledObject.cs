using UnityEngine;
using System.Collections.Generic;

public class TimeScaledObject : MonoBehaviour 
{
	public float initialTimeScale = 1.0f;
	public float LocalTimeScale { get; set; }
	protected List<TimeBubble> affectingTimeBubbles;

	// Use this for initialization
	protected virtual void Awake () 
	{
		affectingTimeBubbles = new List<TimeBubble>();
		LocalTimeScale = initialTimeScale;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (GameSettings.IsPaused)
			return;

		LocalTimeScale = Mathf.Clamp(LocalTimeScale, GameSettings.MIN_TIME_SCALE, GameSettings.MAX_TIME_SCALE);
		if (affectingTimeBubbles.Count == 0 && LocalTimeScale != 1.0f)
		{
			LocalTimeScale = 1.0f;
		}
		else
		{
			LocalTimeScale = CalculateTimeScale();
		}
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

	protected float CalculateTimeScale()
	{
		// Add timeScale buggery here
		return 1.0f;
	}
}
