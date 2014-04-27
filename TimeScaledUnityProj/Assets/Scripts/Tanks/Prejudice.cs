using UnityEngine;
using System.Collections;

public class PrejudiceHS : TankSpecialHS
{

}

public class Prejudice : TankSpecial<PrejudiceHS>
{
	public bool Stealth { get; private set; }
	public float stealthDuration;

	public override float LocalFixedDeltaTime
	{
		get
		{
			return Player.LocalFixedDeltaTime;
		}
	}

	protected override void ExecuteSpecialX()
	{
	}
	protected override void ExecuteSpecialY()
	{
	}
	protected override void ExecuteSpecialB()
	{
		StartCoroutine(BecomeStealthy());
	}

	protected override PrejudiceHS GetCurrentHistoryState()
	{
		TankSpecialHS input = _GetCurrentHistoryState();
		PrejudiceHS output = new PrejudiceHS();
		output.coolDownX = input.coolDownX;
		output.coolDownY = input.coolDownY;
		output.coolDownB = input.coolDownB;
		return output;
	}

	protected override void ApplyHistoryState(PrejudiceHS state)
	{
		_ApplyHistoryState(state);
	}

	private IEnumerator BecomeStealthy()
	{
		Stealth = true;
		yield return new WaitForSeconds(stealthDuration);
		Stealth = false;
	}
}
