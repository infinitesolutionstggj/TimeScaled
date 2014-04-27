using UnityEngine;
using System.Collections;

public class ExtraDamageMarker : Bullet
{
	public static ExtraDamageMarker PrefabMarker;

	public Disruptor owner;

	public static new float Radius
	{
		get
		{
			CheckLoadPrefab();

			return PrefabMarker.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static ExtraDamageMarker Spawn(Vector3 position, float angle, float speed, Disruptor owner)
	{
		CheckLoadPrefab();

		ExtraDamageMarker output = Instantiate(PrefabMarker, position, Quaternion.Euler(0, 0, angle)) as ExtraDamageMarker;
		output.speed = speed;
		output.angle = angle;
		output.lifeTime = 2;
		output.owner = owner;
		return output;
	}

	public static new void CheckLoadPrefab()
	{
		if (PrefabMarker == null)
			PrefabMarker = Resources.Load<ExtraDamageMarker>("ExtraDamageMarker");
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();
	}

	public override void Detonate()
	{
		CheckLoadPrefab();

		owner.targets = AffectingTimeBubbles.ToArray();

		Destroy(gameObject);
	}
}
