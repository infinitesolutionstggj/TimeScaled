using UnityEngine;
using System.Collections;

public class BulletHS
{
	public Vector3 position;
	public float age;
}

public class Bullet : HistoricalComponent<BulletHS>
{
	public const float LifeTime = 5;

	public float angle;
	public float speed;
	float age;

	public Vector3 Velocity
	{
		get { return MathLib.FromPolar(speed, angle).ToVector3(); }
	}
	public static float Radius
	{
		get
		{
			CheckLoadPrefab();

			return prefab.gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	public static Bullet prefab;

	public static void CheckLoadPrefab()
	{
		if (prefab == null)
			prefab = Resources.Load<GameObject>("Bullet").GetComponent<Bullet>();
	}

	public static Bullet Spawn(Vector3 position, float angle, float speed)
	{
		CheckLoadPrefab();

		Bullet output = Network.Instantiate(prefab, position, Quaternion.Euler(0, 0, angle), 0) as Bullet;
		output.speed = speed;
		output.angle = angle;
		return output;
	}

	protected override void Awake()
	{
		base.Awake();

		age = 0;
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		transform.position += Velocity * LocalFixedDeltaTime;
		age += LocalFixedDeltaTime;

		if (age >= LifeTime)
			Destroy(gameObject);
	}

	protected override BulletHS GetCurrentHistoryState()
	{
		BulletHS output = new BulletHS();
		output.position = transform.position;
		output.age = age;
		return output;
	}

	protected override void ApplyHistoryState(BulletHS state)
	{
		transform.position = state.position;
		age = state.age;
	}
}
