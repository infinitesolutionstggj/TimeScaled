using UnityEngine;
using System.Collections;

public class CryoHS : TankSpecialHS
{

}

public class Cryo : TankSpecial<CryoHS>
{
	public ReverseBubbleDesc reverseDesc;
	public float pulsePower;
	public float knockBackPower;
	public float missileSpeed;
	public float missileLifeTime;

	protected override void ExecuteSpecialX()
	{
		Bullet.Spawn(transform.position + MathLib.FromPolar(Player.Radius + Bullet.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, missileSpeed, missileLifeTime, knockBackPower);
	}
	protected override void ExecuteSpecialY()
	{
		ReverseBubbleSpawner.Spawn(transform.position + MathLib.FromPolar(Player.Radius + ReverseBubbleSpawner.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, Player.shotSpeed, reverseDesc);
	}
	protected override void ExecuteSpecialB()
	{
		foreach (var player in Player.All)
			player.ApplyKnockback(player.transform.position - transform.position, pulsePower, GameSettings.PLAYER_KNOCKBACK_DURATION);
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
