using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITankSpecial
{
	void SpecialX();
	void SpecialY();
	void SpecialB();

	Texture2D TextOverlay { get; }
	float CoolDownX { get; }
	float CoolDownY { get; }
	float CoolDownB { get; }
}
