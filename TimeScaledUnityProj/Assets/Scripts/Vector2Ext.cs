using UnityEngine;
using System.Collections;

public static class Vector2Ext
{
	public static Vector3 ToVector3(this Vector2 vec, float z)
	{
		return new Vector3(vec.x, vec.y, z);
	}
}
