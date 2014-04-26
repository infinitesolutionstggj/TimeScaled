using UnityEngine;
using System.Collections;

public static class MathLib
{
	public const float TAU = Mathf.PI * 2;

	public static float Sind(float theta)
	{
		return Mathf.Sin(theta * Mathf.Deg2Rad);
	}
	public static float Cosd(float theta)
	{
		return Mathf.Cos(theta * Mathf.Deg2Rad);
	}
	public static float Tand(float theta)
	{
		return Mathf.Tan(theta * Mathf.Deg2Rad);
	}
	public static float Asind(float x)
	{
		return Mathf.Asin(x) * Mathf.Rad2Deg;
	}
	public static float Acosd(float x)
	{
		return Mathf.Acos(x) * Mathf.Rad2Deg;
	}
	public static float Atand(float x)
	{
		return Mathf.Atan(x) * Mathf.Rad2Deg;
	}
	public static float Atand2(float y, float x)
	{
		return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
	}

	public static Vector2 FromPolar(float magnitude, float angle)
	{
		return new Vector2(Cosd(angle), Sind(angle)) * magnitude;
	}

	private static float SweetSpotAccuracyHelper(float t)
	{
		if (t <= 1)
			return (1 - Mathf.Cos(t * Mathf.PI)) / 2;
		else
			return Mathf.Pow(Mathf.Sin(1 / (t - 1 + 2 / Mathf.PI)), 3);
	}
	public static float SweetSpotAccuracy(float sensitivity, float t, float sweetSpot)
	{
		return Mathf.Pow(SweetSpotAccuracyHelper(t / sweetSpot), sensitivity);
	}
}
