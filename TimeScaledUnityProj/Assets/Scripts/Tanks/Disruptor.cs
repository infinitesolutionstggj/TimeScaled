using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class DisruptorHS : TankSpecialHS
{

}

public class Disruptor : TankSpecial<DisruptorHS>
{
	public TimeBubble[] targets;

	protected override void Awake()
	{
		base.Awake();

		targets = null;
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		if (targets != null)
			foreach (var target in targets)
				if (target != null)
					foreach (var player in target.AffectedObjects.OfType<Player>())
						player.ApplyDamage(GameSettings.BUBBLE_DAMAGE_PER_SECOND * Time.fixedDeltaTime);
	}

	protected override void ExecuteSpecialX()
	{
		AudioManager.PlayClipByName("Shot2");
		ExtraDamageMarker.Spawn(transform.position + MathLib.FromPolar(Player.Radius + ExtraDamageMarker.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, Player.shotSpeed, this);
	}
	protected override void ExecuteSpecialY()
	{
		AudioManager.PlayClipByName("Shot2");
		BubbleDestroyer.Spawn(transform.position + MathLib.FromPolar(Player.Radius + BubbleDestroyer.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, Player.shotSpeed);
	}
	protected override void ExecuteSpecialB()
	{
		AudioManager.PlayClipByName("Shot2");
		PolarityReverser.Spawn(transform.position + MathLib.FromPolar(Player.Radius + PolarityReverser.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, Player.shotSpeed);
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
