using UnityEngine;
using System.Collections;

public class FreezeMissile : Bullet
{
	public static FreezeMissile PrefabMissile;

	public float freezeDuration;

	public static new float Radius
	{
		get
		{
			CheckLoadPrefab();

			return PrefabMissile.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static new FreezeMissile Spawn(Vector3 position, float angle, float speed, float lifeSpan, float freezeDuration)
	{
		CheckLoadPrefab();

		FreezeMissile output = Instantiate(PrefabMissile, position, Quaternion.Euler(0, 0, angle)) as FreezeMissile;
		output.speed = speed;
		output.angle = angle;
		output.lifeTime = lifeSpan;
		output.freezeDuration = freezeDuration;
		return output;
	}

	public static new void CheckLoadPrefab()
	{
		if (PrefabMissile == null)
			PrefabMissile = Resources.Load<FreezeMissile>("FreezeMissile");
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
			p.FreezeFor(freezeDuration);
		}
		Detonate();
	}
}
