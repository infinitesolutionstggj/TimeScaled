using UnityEngine;
using System.Collections;

public class CryoHS : TankSpecialHS
{

}

public class Cryo : TankSpecial<CryoHS>
{
	public float stasisDuration;
	public float freezeDuration;
	public float projectileDuration;
	public float projectileLifeSpan;
	public float projectileSpeed;

	protected override void ExecuteSpecialX()
	{
		foreach (var player in Player.All)
			player.FreezeFor(freezeDuration);
	}
	protected override void ExecuteSpecialY()
	{
		FreezeMissile.Spawn(transform.position + MathLib.FromPolar(Player.Radius + ReverseBubbleSpawner.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, Player.shotSpeed, projectileLifeSpan, projectileDuration);
	}
	protected override void ExecuteSpecialB()
	{
		Player.FreezeFor(stasisDuration);
	}

	protected override CryoHS GetCurrentHistoryState()
	{
		TankSpecialHS input = _GetCurrentHistoryState();
		CryoHS output = new CryoHS();
		output.coolDownX = input.coolDownX;
		output.coolDownY = input.coolDownY;
		output.coolDownB = input.coolDownB;
		return output;
	}

	protected override void ApplyHistoryState(CryoHS state)
	{
		_ApplyHistoryState(state);
	}
}
