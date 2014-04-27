using UnityEngine;
using System.Collections;

public class Frozen : MonoBehaviour
{
	public float thawTime;

	void Awake()
	{
		enabled = false;
	}

	void OnEnabled()
	{
		StartCoroutine(WaitForThaw());
	}

	private IEnumerator WaitForThaw()
	{
		yield return new WaitForSeconds(thawTime);
		enabled = false;
	}
}
