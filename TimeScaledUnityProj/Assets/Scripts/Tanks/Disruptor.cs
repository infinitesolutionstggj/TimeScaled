using UnityEngine;
using System.Collections;

public class DisruptorHS : TankSpecialHS
{

}

public class Disruptor : TankSpecial<DisruptorHS>
{
	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();
	}

	protected override void ExecuteSpecialX()
	{
	}
	protected override void ExecuteSpecialY()
	{
		BubbleDestroyer.Spawn(transform.position + MathLib.FromPolar(Player.Radius + ReverseBubbleSpawner.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, Player.shotSpeed);
	}
	protected override void ExecuteSpecialB()
	{
	}

	protected override DisruptorHS GetCurrentHistoryState()
	{
		TankSpecialHS input = _GetCurrentHistoryState();
		DisruptorHS output = new DisruptorHS();
		output.coolDownX = input.coolDownX;
		output.coolDownY = input.coolDownY;
		output.coolDownB = input.coolDownB;
		return output;
	}

	protected override void ApplyHistoryState(DisruptorHS state)
	{
		_ApplyHistoryState(state);
	}

	private TimeBubbleLimits ClonePlayerTBL()
	{
		TimeBubbleLimits tbl = new TimeBubbleLimits();
		tbl.sweetSpotAge = Player.timeBubbleLimits.sweetSpotAge;
		tbl.sweetSpotSensitivity = Player.timeBubbleLimits.sweetSpotSensitivity;
		tbl.minRadius = Player.timeBubbleLimits.minRadius;
		tbl.maxRadius = Player.timeBubbleLimits.maxRadius;
		tbl.minLifeSpan = Player.timeBubbleLimits.minLifeSpan;
		tbl.maxLifeSpan = Player.timeBubbleLimits.maxLifeSpan;
		tbl.minInnerRadiusPercentage = Player.timeBubbleLimits.minInnerRadiusPercentage;
		tbl.maxInnerRadiusPercentage = Player.timeBubbleLimits.maxInnerRadiusPercentage;
		tbl.minTimeScaleMultiplier = Player.timeBubbleLimits.minTimeScaleMultiplier;
		tbl.maxTimeScaleMultiplier = Player.timeBubbleLimits.maxTimeScaleMultiplier;
		return tbl;
	}
}
