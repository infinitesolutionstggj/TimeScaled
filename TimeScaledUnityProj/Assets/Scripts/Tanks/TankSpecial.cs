using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITankSpecial
{
	public float coolDownX;
	public float coolDownY;
	public float coolDownB;

	public float CoolDownX { get; protected set; }
	public float CoolDownY { get; protected set; }
	public float CoolDownB { get; protected set; }

	protected override void NewFixedUpdate()
	{
		CoolDownX -= LocalFixedDeltaTime;
		CoolDownY -= LocalFixedDeltaTime;
		CoolDownB -= LocalFixedDeltaTime;
	}

	protected virtual void SpecialX()
	{
		StartCoolDownX();
	}
	protected virtual void SpecialY()
	{
		StartCoolDownY();
	}
	protected virtual void SpecialB()
	{
		StartCoolDownB();
	}

	void StartCoolDownX()
	{
		CoolDownX = coolDownX;
	}
	void StartCoolDownY()
	{
		CoolDownY = coolDownY;
	}
	void StartCoolDownB()
	{
		CoolDownB = coolDownY;
	}
}
