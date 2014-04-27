using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrismHS : TankSpecialHS
{
	public float elapsedBoost;
	public float elapsedBubbleTimeScale;
}

public class Prism : TankSpecial<PrismHS>
{
	public static List<Prism> All = new List<Prism>();

	public float bubbleTimeScaleMultiplier;
	public float bubbleTimeScaleDuration;
	public bool BubbleTimeScale
	{
		get
		{
			return CurrentElapsedBubbleTimeScale < bubbleTimeScaleDuration;
		}
	}
	public float CurrentElapsedBubbleTimeScale { get; private set; }

	public float boostAccelerationMultiplier;
	public float boostTurnSpeedMultiplier;
	public float boostDuration;
	public bool Boost
	{
		get
		{
			return CurrentElapsedBoost < boostDuration;
		}
	}
	public float CurrentElapsedBoost { get; private set; }

	public float blinkDistance;

	protected override void ExecuteSpecialX()
	{
		transform.position += MathLib.FromPolar(blinkDistance, Player.TurretAngle).ToVector3();
	}
	protected override void ExecuteSpecialY()
	{
		CurrentElapsedBoost = 0;
	}
	protected override void ExecuteSpecialB()
	{
		CurrentElapsedBubbleTimeScale = 0;
	}

	protected override void Awake()
	{
		base.Awake();

		CurrentElapsedBoost = boostDuration;
		CurrentElapsedBubbleTimeScale = bubbleTimeScaleDuration;
		All.Add(this);
	}

	void OnDestroy()
	{
		All.Remove(this);
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		CurrentElapsedBoost += LocalFixedDeltaTime;
		CurrentElapsedBubbleTimeScale += LocalFixedDeltaTime;
	}

	protected override PrismHS GetCurrentHistoryState()
	{
		TankSpecialHS input = _GetCurrentHistoryState();
		PrismHS output = new PrismHS();
		output.coolDownX = input.coolDownX;
		output.coolDownY = input.coolDownY;
		output.coolDownB = input.coolDownB;
		output.elapsedBoost = CurrentElapsedBoost;
		output.elapsedBubbleTimeScale = CurrentElapsedBubbleTimeScale;
		return output;
	}

	protected override void ApplyHistoryState(PrismHS state)
	{
		_ApplyHistoryState(state);
	}
}
