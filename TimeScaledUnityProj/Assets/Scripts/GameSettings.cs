using UnityEngine;
using System.Collections;

public static class GameSettings
{
	public const float MAX_TIME_SCALE = 10.0f;
	public const float MIN_TIME_SCALE = 0f;
	public const float FIXED_DELTA_TIME = 0.02f;
	public static bool IsPaused { get { return Time.timeScale == 0; } }

	public const float CAMERA_PAN_SPEED = 7.5f;
	public const float MIN_SCREEN_BOUNDS = 0.25f;
	public const float MAX_SCREEN_BOUNDS = 0.75f;

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
