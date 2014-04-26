using UnityEngine;
using System.Collections;

public class TimeBubbleSpawner : Bullet
{
	public static TimeBubble prefab;

	public static void CheckLoadPrefab()
	{
		if (prefab == null)
			prefab = Resources.Load<GameObject>("TimeBubble").GetComponent<TimeBubble>();
	}

	public bool isSlowBubble;
	public float sweetSpotAge;
	public float sweetSpotSensitivity;
	public float minSize;
	public float maxSize;
	public float minInnerRadiusPercent;
	public float maxInnerRadiusPercent;
	public float minTimeScale;
	public float maxTimeScale;
	private float baseScale;

	public float Quality
	{
		get
		{
			return MathLib.SweetSpotAccuracy(sweetSpotSensitivity, Age, sweetSpotAge);
		}
	}

	protected override void Awake()
	{
		base.Awake();

		baseScale = transform.localScale.x;
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		transform.localScale = Vector3.one * baseScale * (1 + Quality);
	}

	public void Detonate()
	{

		float quality = Quality;

		TimeBubble bubble = Instantiate(prefab, transform.position, Quaternion.identity) as TimeBubble;
		bubble.transform.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, quality);
		bubble.innerRadiusPercent = Mathf.Lerp(minInnerRadiusPercent, maxInnerRadiusPercent, quality);

		if (isSlowBubble)
			bubble.timeScaleMultiplier = 1 / Mathf.Lerp(minTimeScale, maxTimeScale, quality);
		else
			bubble.timeScaleMultiplier = Mathf.Lerp(minTimeScale, maxTimeScale, quality);
	}
}
