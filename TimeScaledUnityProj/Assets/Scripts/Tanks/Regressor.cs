using UnityEngine;
using System.Collections;

public class RegressorHS
{

}

public class Regressor : TankSpecial<RegressorHS>
{
	protected override void ExecuteSpecialX()
	{
	}
	protected override void ExecuteSpecialY()
	{

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
