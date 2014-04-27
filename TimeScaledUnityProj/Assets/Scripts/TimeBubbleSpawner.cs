using UnityEngine;
using System.Collections;

[System.Serializable]
public class TimeBubbleLimits
{
	public float sweetSpotAge;
	public float sweetSpotSensitivity;
	public float minRadius;
	public float maxRadius;
	public float minLifeSpan;
	public float maxLifeSpan;
	public float minInnerRadiusPercentage;
	public float maxInnerRadiusPercentage;
	public float minTimeScaleMultiplier;
	public float maxTimeScaleMultiplier;
}

public class TimeBubbleSpawner : Bullet
{
	public static TimeBubble prefabBubble;
	public static TimeBubbleSpawner prefabSpawner;

	public static new float Radius
	{
		get
		{
			CheckLoadPrefab();

			return prefabBubble.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static TimeBubbleSpawner Spawn(Vector3 position, bool isSlowBubble, float angle, float speed, float lifeTime, TimeBubbleLimits limits)
	{
		CheckLoadPrefab();

		TimeBubbleSpawner output = Instantiate(prefabSpawner, position, Quaternion.Euler(0, 0, angle)) as TimeBubbleSpawner;
		output.speed = speed;
		output.angle = angle;
		output.isSlowBubble = isSlowBubble;
		output.lifeTime = lifeTime;
		output.limits = limits;
		return output;
	}

	public static new void CheckLoadPrefab()
	{
		if (prefabBubble == null)
			prefabBubble = Resources.Load<TimeBubble>("TimeBubble");
		if (prefabSpawner == null)
			prefabSpawner = Resources.Load<TimeBubbleSpawner>("TimeBubbleSpawner");
	}

	public bool isSlowBubble;
	public TimeBubbleLimits limits;
	private float baseScale;

	public float Quality
	{
		get
		{
			return MathLib.SweetSpotAccuracy(limits.sweetSpotSensitivity, Age, limits.sweetSpotAge);
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
		CheckLoadPrefab();

		float quality = Quality;

		TimeBubble bubble = Instantiate(prefabBubble, transform.position, Quaternion.identity) as TimeBubble;
		bubble.transform.localScale = Vector3.one * Mathf.Lerp(limits.minRadius, limits.maxRadius, quality) * 2;
		bubble.innerRadiusPercent = Mathf.Lerp(limits.minInnerRadiusPercentage, limits.maxInnerRadiusPercentage, quality);
		bubble.lifeSpan = Mathf.Lerp(limits.minLifeSpan, limits.maxLifeSpan, quality);

		if (isSlowBubble)
			bubble.timeScaleMultiplier = 1 / Mathf.Lerp(limits.minTimeScaleMultiplier, limits.maxTimeScaleMultiplier, quality);
		else
			bubble.timeScaleMultiplier = Mathf.Lerp(limits.minTimeScaleMultiplier, limits.maxTimeScaleMultiplier, quality);

		Destroy(gameObject);
	}
}
