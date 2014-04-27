using UnityEngine;
using System.Collections;

public class MammothHS : TankSpecialHS
{

}

public class Mammoth : TankSpecial<MammothHS>
{
	public float radiusMultiplier;
	public float lifeSpanMultiplier;
	public float timeScaleMultiplier;

	private TimeBubble currentShotX;
	private TimeBubble currentShotY;
	private TimeBubble currentShotB;

	bool isSlowBubble;

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		if (Player.CurrentFastShot != null)
			isSlowBubble = false;
		if (Player.CurrentSlowShot != null)
			isSlowBubble = true;
	}

	protected override void ExecuteSpecialX()
	{
		TimeBubbleLimits tbl = ClonePlayerTBL();
		tbl.minRadius *= radiusMultiplier;
		tbl.maxRadius *= radiusMultiplier;
		StartCoroutine(PopAtPeak(TimeBubbleSpawner.Spawn(transform.position + MathLib.FromPolar(Player.Radius + TimeBubbleSpawner.Radius, Player.TurretAngle).ToVector3(), isSlowBubble, Player.TurretAngle, Player.shotSpeed, 2, tbl)));
	}
	protected override void ExecuteSpecialY()
	{
		TimeBubbleLimits tbl = ClonePlayerTBL();
		tbl.minLifeSpan *= lifeSpanMultiplier;
		tbl.maxLifeSpan *= lifeSpanMultiplier;
		StartCoroutine(PopAtPeak(TimeBubbleSpawner.Spawn(transform.position + MathLib.FromPolar(Player.Radius + TimeBubbleSpawner.Radius, Player.TurretAngle).ToVector3(), isSlowBubble, Player.TurretAngle, Player.shotSpeed, 2, tbl)));
	}
	protected override void ExecuteSpecialB()
	{
		TimeBubbleLimits tbl = ClonePlayerTBL();
		tbl.minTimeScaleMultiplier *= timeScaleMultiplier;
		tbl.maxTimeScaleMultiplier *= timeScaleMultiplier;
		StartCoroutine(PopAtPeak(TimeBubbleSpawner.Spawn(transform.position + MathLib.FromPolar(Player.Radius + TimeBubbleSpawner.Radius, Player.TurretAngle).ToVector3(), isSlowBubble, Player.TurretAngle, Player.shotSpeed, 2, tbl)));
	}

	protected override MammothHS GetCurrentHistoryState()
	{
		TankSpecialHS input = _GetCurrentHistoryState();
		MammothHS output = new MammothHS();
		output.coolDownX = input.coolDownX;
		output.coolDownY = input.coolDownY;
		output.coolDownB = input.coolDownB;
		return output;
	}

	protected override void ApplyHistoryState(MammothHS state)
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

	private IEnumerator PopAtPeak(TimeBubbleSpawner spawner)
	{
		while (spawner.Age < 1)
			yield return null;
		spawner.Detonate();
	}
}
