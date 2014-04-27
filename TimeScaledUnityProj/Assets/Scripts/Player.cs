using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	public float knockBackTimer;
	public Vector3 knockBackVelocity;
}

public class Player : HistoricalComponent<PlayerHS>
{
	public static List<Player> All = new List<Player>();

	public float Radius;

	public int playerNumber;
	public ITankSpecial tankSpecial;
	public float maxPlayerHealth = 50f;
	public float PlayerHealth { get; protected set; }
	public bool IsDead { get { return PlayerHealth <= 0; } }

	public float linearDrag;
	public float linearAcceleration;
	public float maxTurretSpeed;
	public float maxTurnSpeed;

	public float LinearAcceleration
	{
		get
		{
			if (tankSpecial is Prism && (tankSpecial as Prism).Boost)
				return (tankSpecial as Prism).boostAccelerationMultiplier * linearAcceleration;

			return linearAcceleration;
		}
	}
	public float MaxTurnSpeed
	{
		get
		{
			if (tankSpecial is Prism && (tankSpecial as Prism).Boost)
				return (tankSpecial as Prism).boostTurnSpeedMultiplier * maxTurnSpeed;

			return maxTurnSpeed;
		}
	}

	public float shotSpeed;
	public float shotStrength;

	private float currentSpeed;
	private float bodyAngle;
	public float TurretAngle { get; private set; }

	public TimeBubbleLimits timeBubbleLimits;
	public TimeBubbleSpawner CurrentSlowShot { get; private set; }
	public TimeBubbleSpawner CurrentFastShot { get; private set; }

	public float coolDownA;

	public float CoolDownA { get; private set; }

	// Knockback stuff
	protected Vector3 knockbackVelocity = Vector3.zero;
	protected float knockbackTimer = 0;
	protected bool IsBeingKnockedBack { get { return knockbackTimer > 0; } }

	public override float LocalFixedDeltaTime
	{
		get
		{
			if (IsFrozen)
				return 0;

			if (tankSpecial is Prejudice && (tankSpecial as Prejudice).Stealth)
				return Time.fixedDeltaTime;

			return base.LocalFixedDeltaTime;
		}
	}
	public override bool IsRewinding
	{
		get
		{
			if (tankSpecial is Prejudice && (tankSpecial as Prejudice).Stealth)
				return false;

			return base.IsRewinding;
		}
	}

	public bool IsFrozen
	{
		get
		{
			Frozen f = gameObject.GetComponent<Frozen>();

			return f != null && f.enabled;
		}
	}

	public void FreezeFor(float time)
	{
		Frozen f = gameObject.GetComponent<Frozen>();

		if (f == null)
			f = gameObject.AddComponent<Frozen>();

		f.thawTime = time;
		f.enabled = true;
	}

	// Use this for initialization
	protected override void Awake () 
	{
		base.Awake();
		PlayerHealth = maxPlayerHealth;
		All.Add(this);
	}

	void OnDestroy()
	{
		All.Remove(this);
	}
	
	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		// Apply damage to player if in time bubbles
		if (AffectingTimeBubbles.Count > 0)
		{
			ApplyDamage(GameSettings.BUBBLE_DAMAGE_PER_SECOND * Mathf.Min(AffectingTimeBubbles.Count, GameSettings.MAX_DAMAGING_BUBBLES) * Time.fixedDeltaTime);
		}
		// or heal the player if not in a time bubble
		else
		{
			if (PlayerHealth < maxPlayerHealth)
			{
				PlayerHealth += GameSettings.HEALTH_REGEN_PER_SECOND * Time.fixedDeltaTime;
				if (PlayerHealth > maxPlayerHealth)
					PlayerHealth = maxPlayerHealth;
			}
		}
		// Check to see if the player is dead
		if(IsDead)
		{
			AudioManager.PlayClipByName("PlayerDeath");
			Destroy(this.gameObject);
		}

		if (IsBeingKnockedBack)
		{
			transform.position += knockbackVelocity * LocalFixedDeltaTime;
			bodyAngle = MathLib.Atand2(transform.right.y, transform.right.x);
			knockbackTimer -= LocalFixedDeltaTime;
		}
		else if (!IsFrozen)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, 0, linearDrag * LocalFixedDeltaTime);
			Thrust(XCI.GetAxis(XboxAxis.RightTrigger, playerNumber) - XCI.GetAxis(XboxAxis.LeftTrigger, playerNumber));
			if (Mathf.Abs(XCI.GetAxis(XboxAxis.RightStickX, playerNumber)) > 0.1 || Mathf.Abs(XCI.GetAxis(XboxAxis.RightStickY, playerNumber)) > 0.1)
				RotateTurretTo(MathLib.Atand2(XCI.GetAxis(XboxAxis.RightStickY, playerNumber), XCI.GetAxis(XboxAxis.RightStickX, playerNumber)));

			bodyAngle -= MaxTurnSpeed * currentSpeed * XCI.GetAxis(XboxAxis.LeftStickX, playerNumber) * LocalFixedDeltaTime;
			transform.position += new Vector2(MathLib.Cosd(bodyAngle), MathLib.Sind(bodyAngle)).ToVector3() * currentSpeed * LocalFixedDeltaTime;
			transform.rotation = Quaternion.Euler(0, 0, bodyAngle);
			transform.GetChild(0).rotation = Quaternion.Euler(0, 0, TurretAngle);
		}

		CoolDownA -= LocalFixedDeltaTime;
	}

	protected override void NewUpdate()
	{
		base.NewUpdate();

		if (XCI.GetButtonDown(XboxButton.LeftBumper, playerNumber))
			CurrentSlowShot = ShootSlowBubble();
		if (XCI.GetButtonDown(XboxButton.RightBumper, playerNumber))
			CurrentFastShot = ShootFastBubble();
		if (XCI.GetButtonDown(XboxButton.A, playerNumber) && CoolDownA <= 0)
			ShootBullet();
		if (XCI.GetButtonDown(XboxButton.X, playerNumber))
			tankSpecial.SpecialX();
		if (XCI.GetButtonDown(XboxButton.Y, playerNumber))
			tankSpecial.SpecialY();
		if (XCI.GetButtonDown(XboxButton.B, playerNumber))
			tankSpecial.SpecialB();

		if (CurrentSlowShot != null && XCI.GetButtonUp(XboxButton.LeftBumper, playerNumber))
			CurrentSlowShot.Detonate();
		if (CurrentFastShot != null && XCI.GetButtonUp(XboxButton.RightBumper, playerNumber))
			CurrentFastShot.Detonate();
	}

	// Thrust forward/backward a given proportion of max speed
	protected void Thrust(float amount)
	{
		currentSpeed += LinearAcceleration * amount * LocalFixedDeltaTime;
	}

	public void ApplyKnockback(Vector3 direction, float strength, float duration)
	{
		knockbackVelocity = direction.normalized * strength;
		knockbackTimer = duration;
	}

	protected void RotateTurretTo(float angle)
	{
		TurretAngle = Mathf.MoveTowardsAngle(TurretAngle, angle, maxTurretSpeed * LocalFixedDeltaTime);
	}

	protected void ShootBullet()
	{
		if (tankSpecial is Prism && (tankSpecial as Prism).Boost)
			return;

		Bullet.Spawn(transform.position + MathLib.FromPolar(Radius + Bullet.Radius, TurretAngle).ToVector3(),
			TurretAngle, shotSpeed, 2, shotStrength);
		AudioManager.PlayClipByName("Shot1");
		CoolDownA = coolDownA;
	}

	protected TimeBubbleSpawner ShootFastBubble()
	{
		AudioManager.PlayClipByName("Shot1");
		return TimeBubbleSpawner.Spawn(transform.position + MathLib.FromPolar(Radius + TimeBubbleSpawner.Radius, TurretAngle).ToVector3(), false, TurretAngle, shotSpeed, 5, timeBubbleLimits);
	}

	protected TimeBubbleSpawner ShootSlowBubble()
	{
		AudioManager.PlayClipByName("Shot1");
		return TimeBubbleSpawner.Spawn(transform.position + MathLib.FromPolar(Radius + TimeBubbleSpawner.Radius, TurretAngle).ToVector3(), true, TurretAngle, shotSpeed, 5, timeBubbleLimits);
	}

	public void ApplyDamage(float damage)
	{
		if (GameSettings.DebugInvincible)
			return;

		if (damage <= 0)
			return;

		if (IsFrozen && tankSpecial is Cryo)
			return;
		
		PlayerHealth -= damage;

		if (IsDead)
		{
			AudioManager.PlayClipByName("PlayerDeath");
			Destroy(gameObject);
		}
	}

	protected override PlayerHS GetCurrentHistoryState()
	{
		PlayerHS output = new PlayerHS();
		output.position = transform.position;
		output.rotation = transform.rotation;
		output.currentSpeed = currentSpeed;
		output.bodyAngle = bodyAngle;
		output.turretAngle = TurretAngle;
		output.turretRotation = transform.GetChild(0).rotation;
		output.coolDownA = coolDownA;
		output.knockBackTimer = knockbackTimer;
		output.knockBackVelocity = knockbackVelocity;
		return output;
	}

	protected override void ApplyHistoryState(PlayerHS state)
	{
		transform.position = state.position;
		transform.rotation = state.rotation;
		currentSpeed = state.currentSpeed;
		bodyAngle = state.bodyAngle;
		TurretAngle = state.turretAngle;
		transform.GetChild(0).rotation = state.turretRotation;
		coolDownA = state.coolDownA;
		knockbackTimer = state.knockBackTimer;
		knockbackVelocity = state.knockBackVelocity;
	}
}
