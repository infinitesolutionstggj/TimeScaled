using UnityEngine;
using System.Collections;

public class RegressorHS : TankSpecialHS
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
