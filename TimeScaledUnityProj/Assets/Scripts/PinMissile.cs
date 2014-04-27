using UnityEngine;
using System.Collections;

public class PinMissile : Bullet
{
	public static PinMissile PrefabMissile;

	public float knockbackDuration;

	public static new float Radius
	{
		get
		{
			CheckLoadPrefab();

			return PrefabMissile.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static PinMissile Spawn(Vector3 position, float angle, float speed, float lifeSpan, float knockbackStrength, float knockBackDuration)
	{
		CheckLoadPrefab();

		PinMissile output = Instantiate(PrefabMissile, position, Quaternion.Euler(0, 0, angle)) as PinMissile;
		output.speed = speed;
		output.angle = angle;
		output.lifeTime = lifeSpan;
		output.KnockbackStrength = knockbackStrength;
		output.knockbackDuration = knockBackDuration;
		return output;
	}

	public static new void CheckLoadPrefab()
	{
		if (PrefabMissile == null)
			PrefabMissile = Resources.Load<PinMissile>("PinMissile");
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
			p.ApplyKnockback(Velocity, KnockbackStrength, knockbackDuration - Age);
		}
		Detonate();
	}
}
