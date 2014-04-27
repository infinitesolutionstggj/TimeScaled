using UnityEngine;
using System.Collections;

public class BubbleDestroyer : Bullet
{
	public static BubbleDestroyer PrefabDestroyer;

	public static new float Radius
	{
		get
		{
			CheckLoadPrefab();

			return PrefabDestroyer.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static BubbleDestroyer Spawn(Vector3 position, float angle, float speed)
	{
		CheckLoadPrefab();

		BubbleDestroyer output = Instantiate(PrefabDestroyer, position, Quaternion.Euler(0, 0, angle)) as BubbleDestroyer;
		output.speed = speed;
		output.angle = angle;
		output.lifeTime = 2;
		return output;
	}

	public static new void CheckLoadPrefab()
	{
		if (PrefabDestroyer == null)
			PrefabDestroyer = Resources.Load<BubbleDestroyer>("BubbleDestroyer");
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();
	}

	public override void Detonate()
	{
		CheckLoadPrefab();

		Debug.Log("Detonation");
		foreach (var bubble in affectingTimeBubbles)
			Destroy(bubble.gameObject);

		Destroy(gameObject);
	}
}
