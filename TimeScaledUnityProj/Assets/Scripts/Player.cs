using UnityEngine;
using System.Collections;

public class PlayerHS
{
	public Vector3 position;
	public float currentSpeed;
	public float bodyAngle;
	public float turretAngle;
}

public class Player : HistoricalComponent<PlayerHS>
{
	public float linearDrag;
	public float linearAcceleration;
	public float maxTurretSpeed;
	public float maxTurnSpeed;

	private float currentSpeed;
	private float bodyAngle;
	private float turretAngle;

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

	protected override PlayerHS GetCurrentHistoryState()
	{
		PlayerHS output = new PlayerHS();
		output.position = transform.position;
		output.currentSpeed = currentSpeed;
		output.bodyAngle = bodyAngle;
		output.turretAngle = turretAngle;
		return output;
	}

	protected override void ApplyHistoryState(PlayerHS state)
	{
		transform.position = state.position;
		currentSpeed = state.currentSpeed;
		bodyAngle = state.bodyAngle;
		turretAngle = state.turretAngle;
	}
}
