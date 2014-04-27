using UnityEngine;
using System.Collections;

public static class Rigidbody2DExt
{
	private const float MAX_DISTANCE_DELTA = 0.25f;

	public static void MovePosition(this Rigidbody2D rb, Vector2 displacement)
	{
		float steps = Mathf.Ceil(displacement.magnitude / MAX_DISTANCE_DELTA);
		Vector2 stepDisplacement = displacement / steps;
		for (int i = 0; i < steps; i++)
		{
			rb.transform.position += stepDisplacement.ToVector3();
		}
	}

	public static void MovePositionTo(this Rigidbody2D rb, Vector2 targetPosition)
	{
		rb.MovePosition(targetPosition.ToVector3() - rb.transform.position);
	}
}
