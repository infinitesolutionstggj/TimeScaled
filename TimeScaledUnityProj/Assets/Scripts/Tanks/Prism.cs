using UnityEngine;
using System.Collections;

public class PrismHS : TankSpecialHS
{

}

public class Prism : TankSpecial<PrismHS>
{
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
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		CurrentElapsedBoost += LocalFixedDeltaTime;
	}

	protected override PrismHS GetCurrentHistoryState()
	{
		TankSpecialHS input = _GetCurrentHistoryState();
		PrismHS output = new PrismHS();
		output.coolDownX = input.coolDownX;
		output.coolDownY = input.coolDownY;
		output.coolDownB = input.coolDownB;
		return output;
	}

	protected override void ApplyHistoryState(PrismHS state)
	{
		_ApplyHistoryState(state);
	}
}
