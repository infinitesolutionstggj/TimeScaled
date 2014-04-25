using UnityEngine;
using System.Collections;

public class TimeScaledObject : MonoBehaviour 
{
	public float initialTimeScale = 1.0f;
	public float LocalTimeScale { get; set; }

	// Use this for initialization
	protected virtual void Awake () 
	{
		LocalTimeScale = initialTimeScale;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (GameSettings.IsPaused)
			return;

		LocalTimeScale = Mathf.Clamp(LocalTimeScale, GameSettings.MIN_TIME_SCALE, GameSettings.MAX_TIME_SCALE);
	}

	protected virtual void LateUpdate()
	{

	}

	protected virtual void FixedUpdate()
	{

	}


}
