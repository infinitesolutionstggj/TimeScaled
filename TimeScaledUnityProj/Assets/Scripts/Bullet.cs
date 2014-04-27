using UnityEngine;
using System.Collections;

public class BulletHS
{
	public Vector3 position;
	public float age;
}

public class Bullet : HistoricalComponent<BulletHS>
{
	public float lifeTime;
	public float angle;
	public float speed;
	public float Age { get; set; }
	public float KnockbackStrength { get; set; }

	public Vector3 Velocity
	{
		get { return MathLib.FromPolar(speed, angle).ToVector3(); }
	}
	public static float Radius
	{
		get
		{
			CheckLoadPrefab();

			return prefabBullet.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static Bullet prefabBullet;

	public static void CheckLoadPrefab()
	{
		if (prefabBullet == null)
			prefabBullet = Resources.Load<GameObject>("Bullet").GetComponent<Bullet>();
	}

	public static Bullet Spawn(Vector3 position, float angle, float speed, float lifeTime, float knockbackStrength)
	{
		CheckLoadPrefab();

		Bullet output = Instantiate(prefabBullet, position, Quaternion.Euler(0, 0, angle)) as Bullet;
		output.speed = speed;
		output.angle = angle;
		output.lifeTime = lifeTime;
		output.KnockbackStrength = knockbackStrength;
		return output;
	}

	protected override void Awake()
	{
		base.Awake();

		Age = 0;
	}

	protected virtual void OnDestroy()
	{
		if (!IsRewinding)
			Detonate();
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		transform.position += Velocity * LocalFixedDeltaTime;
		Age += LocalFixedDeltaTime;

		if (lifeTime > 0 && Age >= lifeTime)
			Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Player p = col.gameObject.GetComponent<Player>();
		if (p) // bullet has collided with a player
		{
			p.ApplyKnockback(Velocity, KnockbackStrength, GameSettings.PLAYER_KNOCKBACK_DURATION);
		}
		Detonate();
	}

	public virtual void Detonate()
	{
		Destroy(gameObject);
	}

	protected override BulletHS GetCurrentHistoryState()
	{
		BulletHS output = new BulletHS();
		output.position = transform.position;
		output.age = Age;
		return output;
	}

	protected override void ApplyHistoryState(BulletHS state)
	{
		transform.position = state.position;
		Age = state.age;
	}
}
