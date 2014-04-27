using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class PlayerHS
{
	public Vector3 position;
	public Quaternion rotation;
	public float currentSpeed;
	public float bodyAngle;
	public float turretAngle;
	public Quaternion turretRotation;

	public float coolDownA;
}

public class Player : HistoricalComponent<PlayerHS>
{
	public int playerNumber;
	public ITankSpecial tankSpecial;

	public float linearDrag;
	public float linearAcceleration;
	public float maxTurretSpeed;
	public float maxTurnSpeed;

	public float shotSpeed;

	private float currentSpeed;
	private float bodyAngle;
	private float turretAngle;

	public TimeBubbleLimits timeBubbleLimits;
	private TimeBubbleSpawner currentSlowShot;
	private TimeBubbleSpawner currentFastShot;

	public float coolDownA;

	public float CoolDownA { get; private set; }

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
		Thrust(XCI.GetAxis(XboxAxis.RightTrigger, playerNumber) - XCI.GetAxis(XboxAxis.LeftTrigger, playerNumber));
		if (Mathf.Abs(XCI.GetAxis(XboxAxis.RightStickX, playerNumber)) > 0.1 || Mathf.Abs(XCI.GetAxis(XboxAxis.RightStickY, playerNumber)) > 0.1)
			RotateTurretTo(MathLib.Atand2(XCI.GetAxis(XboxAxis.RightStickY, playerNumber), XCI.GetAxis(XboxAxis.RightStickX, playerNumber)));

		bodyAngle -= maxTurnSpeed * currentSpeed * XCI.GetAxis(XboxAxis.LeftStickX, playerNumber) * LocalFixedDeltaTime;
		transform.position += new Vector2(MathLib.Cosd(bodyAngle), MathLib.Sind(bodyAngle)).ToVector3() * currentSpeed * LocalFixedDeltaTime;
		transform.rotation = Quaternion.Euler(0, 0, bodyAngle);
		transform.GetChild(0).rotation = Quaternion.Euler(0, 0, turretAngle);

		if (XCI.GetButtonDown(XboxButton.LeftBumper, playerNumber))
			currentSlowShot = ShootSlowBubble();
		if (XCI.GetButtonDown(XboxButton.RightBumper, playerNumber))
			currentFastShot = ShootFastBubble();
		if (XCI.GetButtonDown(XboxButton.A, playerNumber))
			ShootBullet();
		if (XCI.GetButtonDown(XboxButton.X, playerNumber))
			tankSpecial.SpecialX();
		if (XCI.GetButtonDown(XboxButton.Y, playerNumber))
			tankSpecial.SpecialY();
		if (XCI.GetButtonDown(XboxButton.B, playerNumber))
			tankSpecial.SpecialB();

		if (currentSlowShot != null && XCI.GetButtonUp(XboxButton.LeftBumper, playerNumber))
			currentSlowShot.Detonate();
		if (currentFastShot != null && XCI.GetButtonUp(XboxButton.RightBumper, playerNumber))
			currentFastShot.Detonate();

		coolDownA -= LocalFixedDeltaTime;
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
			turretAngle, shotSpeed, 2);
	}

	protected TimeBubbleSpawner ShootFastBubble()
	{
		return TimeBubbleSpawner.Spawn(transform.position + MathLib.FromPolar(Radius + TimeBubbleSpawner.Radius, turretAngle).ToVector3(), false, turretAngle, shotSpeed, 5, timeBubbleLimits);
	}

	protected TimeBubbleSpawner ShootSlowBubble()
	{
		return TimeBubbleSpawner.Spawn(transform.position + MathLib.FromPolar(Radius + TimeBubbleSpawner.Radius, turretAngle).ToVector3(), true, turretAngle, shotSpeed, 5, timeBubbleLimits);
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
