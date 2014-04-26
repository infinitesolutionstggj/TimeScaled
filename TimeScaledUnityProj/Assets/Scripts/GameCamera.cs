using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
	public float buffer;
	public float lerpSpeed;

	void FixedUpdate ()
	{
		Rect? b = PointOfInterest.Bounds;

		if (b == null)
			return;

		Rect bounds = b.Value.Expand(buffer);

		Vector2 targetPan = bounds.center;
		float targetZoom;
		if (Camera.main.aspect > bounds.Aspect())	// Limited by vertical
		{
			targetZoom = bounds.height;
		}
		else										// Limited by horizontal
		{
			targetZoom = bounds.width / Camera.main.aspect;
		}

		transform.position = Vector3.Lerp(transform.position, targetPan.ToVector3(-100), Mathf.Clamp01(lerpSpeed * Time.fixedDeltaTime));
		Camera.main.orthographicSize = targetZoom / 2;
	}
}
