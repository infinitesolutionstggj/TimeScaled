using UnityEngine;
using System.Collections;

public class Eliminator : Bullet
{
	public static Eliminator PrefabEliminator;

	public static new float Radius
	{
		get
		{
			CheckLoadPrefab();

			return PrefabEliminator.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static Eliminator Spawn(Vector3 position, float angle, float speed, float lifeSpan)
	{
		CheckLoadPrefab();

		Eliminator output = Instantiate(PrefabEliminator, position, Quaternion.Euler(0, 0, angle)) as Eliminator;
		output.speed = speed;
		output.angle = angle;
		output.lifeTime = lifeSpan;
		return output;
	}

	public static new void CheckLoadPrefab()
	{
		if (PrefabEliminator == null)
			PrefabEliminator = Resources.Load<Eliminator>("Eliminator");
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();
	}

	protected override void OnCollisionEnter2D(Collision2D col)
	{
		Player p = col.gameObject.GetComponent<Player>();
		if (p) // bullet has collided with a player
		{
			foreach (var bubble in p.AffectingTimeBubbles)
				if (bubble.timeScaleMultiplier > 1)
				{
					Destroy(p.gameObject);
				}
		}
		Detonate();
	}
}
