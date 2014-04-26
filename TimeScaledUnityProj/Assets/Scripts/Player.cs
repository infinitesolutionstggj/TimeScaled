using UnityEngine;
using System.Collections;

public class PlayerHS
{
	public Vector3 position;
	public Quaternion rotation;
	public float currentSpeed;
	public float bodyAngle;
	public float turretAngle;
	public Quaternion turretRotation;
}

public class Player : HistoricalComponent<PlayerHS>
{
	public float linearDrag;
	public float linearAcceleration;
	public float maxTurretSpeed;
	public float maxTurnSpeed;

	public float shotSpeed;

	private float currentSpeed;
	private float bodyAngle;
	private float turretAngle;

	public float Radius
	{
		get
		{
			return gameObject.GetComponent<CircleCollider2D>().radius;
		}
	}

	// Use this for initialization
	protected override void Awake () 
	{
		base.Awake();
	}

	void OnDestroy()
	{
	}
	
	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		currentSpeed = Mathf.Lerp(currentSpeed, 0, linearDrag * LocalFixedDeltaTime);
		Thrust(Input.GetAxis("Vertical"));
		RotateTurretTo(turretAngle + 90);

		bodyAngle -= maxTurnSpeed * currentSpeed * Input.GetAxis("Horizontal") * LocalFixedDeltaTime;
		transform.position += new Vector2(MathLib.Cosd(bodyAngle), MathLib.Sind(bodyAngle)).ToVector3() * currentSpeed * LocalFixedDeltaTime;
		transform.rotation = Quaternion.Euler(0, 0, bodyAngle);
		transform.GetChild(0).rotation = Quaternion.Euler(0, 0, turretAngle);

		if (Input.GetButtonDown("Fire1"))
			ShootBullet();
	}

	// Thrust forward/backward a given proportion of max speed
	protected void Thrust(float amount)
	{
		currentSpeed += linearAcceleration * amount * LocalFixedDeltaTime;
	}

	protected void RotateTurretTo(float angle)
	{
		turretAngle = Mathf.MoveTowardsAngle(turretAngle, angle, maxTurretSpeed * LocalFixedDeltaTime);
	}

	protected void ShootBullet()
	{
		Bullet.Spawn(transform.position + MathLib.FromPolar(Radius + Bullet.Radius, turretAngle).ToVector3(),
			turretAngle, shotSpeed);
	}

	protected override PlayerHS GetCurrentHistoryState()
	{
		PlayerHS output = new PlayerHS();
		output.position = transform.position;
		output.rotation = transform.rotation;
		output.currentSpeed = currentSpeed;
		output.bodyAngle = bodyAngle;
		output.turretAngle = turretAngle;
		output.turretRotation = transform.GetChild(0).rotation;
		return output;
	}

	protected override void ApplyHistoryState(PlayerHS state)
	{
		transform.position = state.position;
		transform.rotation = state.rotation;
		currentSpeed = state.currentSpeed;
		bodyAngle = state.bodyAngle;
		turretAngle = state.turretAngle;
		transform.GetChild(0).rotation = state.turretRotation;
	}
}
