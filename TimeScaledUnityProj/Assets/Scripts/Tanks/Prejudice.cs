using UnityEngine;
using System.Collections;

public class PrejudiceHS : TankSpecialHS
{

}

public class Prejudice : TankSpecial<PrejudiceHS>
{
	public bool Stealth { get; private set; }
	public float stealthDuration;

	public float missileSpeed;
	public float missileDragTime;

	public float eliminatorSpeed;
	public float eliminatorLifeSpan;

	public override float LocalFixedDeltaTime
	{
		get
		{
			return Player.LocalFixedDeltaTime;
		}
	}

	protected override void ExecuteSpecialX()
	{
		AudioManager.PlayClipByName("Shot 2");
		Eliminator.Spawn(transform.position + MathLib.FromPolar(Player.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, eliminatorSpeed, eliminatorLifeSpan);
	}
	protected override void ExecuteSpecialY()
	{
		AudioManager.PlayClipByName("Shot 2");
		PinMissile.Spawn(transform.position + MathLib.FromPolar(Player.Radius + Bullet.Radius, Player.TurretAngle).ToVector3(),
			Player.TurretAngle, missileSpeed, missileDragTime, missileSpeed, missileDragTime);
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
