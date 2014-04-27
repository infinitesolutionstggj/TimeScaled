using UnityEngine;
using System.Collections;

public class RegressorHS
{

}

public class Regressor : TankSpecial<RegressorHS>
{
	protected override void SpecialX()
	{
		base.SpecialX();
	}
	protected override void SpecialY()
	{
		base.SpecialY();
	}
	protected override void SpecialB()
	{
		base.SpecialB();
	}

	protected override RegressorHS GetCurrentHistoryState()
	{
		return new RegressorHS();
	}

	protected override void ApplyHistoryState(RegressorHS state)
	{

	}
}
