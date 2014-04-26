﻿using UnityEngine;
using System.Collections;

public static class GameSettings
{
	public const float MAX_TIME_SCALE = 10.0f;	// The maximum time scale at which a TimeScaledObject can progress
	public const float MIN_TIME_SCALE = 0.1f;		// Ditto minimum
	public const float FIXED_DELTA_TIME = 0.02f;	// Constant for adjusting Time.fixedDeltaTime
	public static bool IsPaused { get { return Time.timeScale == 0; } }	// Is the game currently paused?

	public const float CAMERA_PAN_SPEED = 7.5f;		// The movement speed of the camera
	public const float MIN_SCREEN_BUFFER = 0.25f;	// The fraction of the left of the screen that forces the camera to move left/up
	public const float MAX_SCREEN_BUFFER = 1 - MIN_SCREEN_BUFFER;	// Ditto for right/bottom side

	public static int MaxHistoryStates { get { return Mathf.CeilToInt(10 / FIXED_DELTA_TIME); } }

	public static void Pause()
	{
		Time.timeScale = 0;
		Time.fixedDeltaTime = 0;
	}

	public static void Resume()
	{
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = FIXED_DELTA_TIME;
	}
}
