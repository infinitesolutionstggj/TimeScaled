using UnityEngine;
using System.Collections;

public static class CameraExt
{
	public static float OrthographicWidth(this Camera cam)
	{
		return cam.orthographicSize * cam.aspect * 2;
	}

	public static float OrthographicHeight(this Camera cam)
	{
		return cam.orthographicSize * 2;
	}
}
