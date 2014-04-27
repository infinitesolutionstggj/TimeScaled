﻿using UnityEngine;
using System.Collections;

public abstract class TankSpecial<T> : HistoricalComponent<T>, ITankSpecial
{
	public Player Player { get; private set; }

	public float coolDownX;
	public float coolDownY;
	public float coolDownB;

	public float CoolDownX { get; private set; }
	public float CoolDownY { get; private set; }
	public float CoolDownB { get; private set; }

	protected override void Awake()
	{
		base.Awake();

		Player = gameObject.GetComponent<Player>();
		Player.tankSpecial = this;
	}

	public void SpecialX()
	{
		if (CoolDownX <= 0)
		{
			ExecuteSpecialX();
			CoolDownX = coolDownX;
		}
	}
	public void SpecialY()
	{
		if (CoolDownY <= 0)
		{
			ExecuteSpecialY();
			CoolDownY = coolDownY;
		}
	}
	public void SpecialB()
	{
		if (CoolDownB <= 0)
		{
			ExecuteSpecialB();
			CoolDownB = coolDownB;
		}
	}

	protected abstract void ExecuteSpecialX();
	protected abstract void ExecuteSpecialY();
	protected abstract void ExecuteSpecialB();
}
