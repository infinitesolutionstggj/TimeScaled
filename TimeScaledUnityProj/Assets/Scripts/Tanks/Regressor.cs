using UnityEngine;
using System.Collections;

public class RegressorHS : TankSpecialHS
{

}

public class Regressor : TankSpecial<RegressorHS>
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

	protected override RegressorHS GetCurrentHistoryState()
	{
		TankSpecialHS input = _GetCurrentHistoryState();
		RegressorHS output = new RegressorHS();
		output.coolDownX = input.coolDownX;
		output.coolDownY = input.coolDownY;
		output.coolDownB = input.coolDownB;
		return output;
	}

	protected override void ApplyHistoryState(RegressorHS state)
	{
		_ApplyHistoryState(state);
	}
}
