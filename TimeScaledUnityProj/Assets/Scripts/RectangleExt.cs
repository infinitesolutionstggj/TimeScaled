using UnityEngine;
using System.Collections;

public static class RectangleExt
{
	public static Rect Expand(this Rect rect, float amount)
	{
		return new Rect(rect.xMin - amount, rect.yMin - amount, rect.width + amount * 2, rect.height + amount * 2);
	}

	public static float Aspect(this Rect rect)
	{
		return rect.width / rect.height;
	}
}
