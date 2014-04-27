using UnityEngine;
using System.Collections;

public class PolarityReverser : Bullet
{
	public static PolarityReverser PrefabReverser;

	public static new float Radius
	{
		get
		{
			CheckLoadPrefab();

			return PrefabReverser.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static PolarityReverser Spawn(Vector3 position, float angle, float speed)
	{
		CheckLoadPrefab();

		PolarityReverser output = Instantiate(PrefabReverser, position, Quaternion.Euler(0, 0, angle)) as PolarityReverser;
		output.speed = speed;
		output.angle = angle;
		output.lifeTime = 2;
		return output;
	}

	public static new void CheckLoadPrefab()
	{
		if (PrefabReverser == null)
			PrefabReverser = Resources.Load<PolarityReverser>("PolarityReverser");
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();
	}

	public override void Detonate()
	{
		CheckLoadPrefab();

		foreach (var bubble in affectingTimeBubbles)
		{
			bubble.timeScaleMultiplier = 1 / bubble.timeScaleMultiplier;
			bubble.ResetMaterial();
		}

		Destroy(gameObject);
	}
}
