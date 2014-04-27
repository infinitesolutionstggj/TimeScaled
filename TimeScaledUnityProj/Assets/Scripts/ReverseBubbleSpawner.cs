using UnityEngine;
using System.Collections;

[System.Serializable]
public class ReverseBubbleDesc
{
	public float radius;
	public float lifeSpan;
}

public class ReverseBubbleSpawner : Bullet
{
	public static ReverseBubble PrefabBubble;
	public static ReverseBubbleSpawner PrefabSpawner;

	public static new float Radius
	{
		get
		{
			CheckLoadPrefab();

			return PrefabSpawner.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static ReverseBubbleSpawner Spawn(Vector3 position, float angle, float speed, ReverseBubbleDesc desc)
	{
		CheckLoadPrefab();

		ReverseBubbleSpawner output = Instantiate(PrefabSpawner, position, Quaternion.Euler(0, 0, angle)) as ReverseBubbleSpawner;
		output.speed = speed;
		output.angle = angle;
		output.lifeTime = 1;
		output.desc = desc;
		return output;
	}

	public static new void CheckLoadPrefab()
	{
		if (PrefabBubble == null)
			PrefabBubble = Resources.Load<ReverseBubble>("ReverseBubble");
		if (PrefabSpawner == null)
			PrefabSpawner = Resources.Load<ReverseBubbleSpawner>("ReverseBubbleSpawner");
	}

	public ReverseBubbleDesc desc;

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();
	}

	public override void Detonate()
	{
		CheckLoadPrefab();

		ReverseBubble bubble = Instantiate(PrefabBubble, transform.position, Quaternion.identity) as ReverseBubble;
		bubble.transform.localScale = Vector3.one * desc.radius * 2;
		bubble.lifeSpan = desc.lifeSpan;

		Destroy(gameObject);
	}
}
