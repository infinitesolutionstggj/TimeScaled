using UnityEngine;
using System.Collections;

public class RegressorHS
{

}

public class Regressor : TankSpecial<RegressorHS>
{
	public ReverseBubbleDesc reverseDesc;

	protected override void ExecuteSpecialX()
	{
	}
	protected override void ExecuteSpecialY()
	{
		ReverseBubbleSpawner.Spawn(transform.position + MathLib.FromPolar(Player.Radius + ReverseBubbleSpawner.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, Player.shotSpeed, reverseDesc);
	}
	protected override void ExecuteSpecialB()
	{
	}

	protected override RegressorHS GetCurrentHistoryState()
	{
		return new RegressorHS();
	}

	protected override void ApplyHistoryState(RegressorHS state)
	{

	}
}
