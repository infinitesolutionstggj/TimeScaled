using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PointOfInterest : MonoBehaviour
{
	public static List<PointOfInterest> All = new List<PointOfInterest>();

	void Start ()
	{
		All.Add(this);
	}

	void OnDestroy()
	{
		All.Remove(this);
	}

	public static Rect? Bounds
	{
		get
		{
			if (All.Count == 0)
				return null;

			float xMin = Mathf.Infinity;
			float yMin = Mathf.Infinity;
			float xMax = Mathf.NegativeInfinity;
			float yMax = Mathf.NegativeInfinity;

			foreach (var poi in All)
			{
				xMin = Mathf.Min(xMin, poi.transform.position.x);
				yMin = Mathf.Min(yMin, poi.transform.position.y);
				xMax = Mathf.Max(xMax, poi.transform.position.x);
				yMax = Mathf.Max(yMax, poi.transform.position.y);
			}

			return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
		}
	}
}
