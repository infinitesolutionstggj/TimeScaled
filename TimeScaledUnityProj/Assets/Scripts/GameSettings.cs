using UnityEngine;
using System.Collections;

public static class GameSettings
{
	public const float MAX_TIME_SCALE = 10.0f;
	public const float MIN_TIME_SCALE = 0f;
	public static bool IsPaused { get { return Time.timeScale == 0; } }

	public const float CAMERA_PAN_SPEED = 7.5f;
	public const float MIN_SCREEN_BOUNDS = 0.25f;
	public const float MAX_SCREEN_BOUNDS = 0.75f;

	public static void Pause()
	{
		Time.timeScale = 0;
	}

	public static void Resume()
	{
		Time.timeScale = 1.0f;
	}
}
