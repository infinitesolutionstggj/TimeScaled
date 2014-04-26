using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class HistoricalComponent<T> : TimeScaledObject
{
	public LinkedList<T> history;
	public bool IsFull { get; private set; }
	public bool isRewinding;

	protected override void Awake()
	{
		base.Awake();

		history = new LinkedList<T>();
		IsFull = false;
	}

	protected sealed override void FixedUpdate()
	{
		base.FixedUpdate();

		history.AddLast(GetCurrentHistoryState());

		while (history.Count > GameSettings.MaxHistoryStates)
		{
			if (!IsFull)
				IsFull = true;

			history.RemoveFirst();
		}

		if (isRewinding)
		{
			// TODO
		}
		else
		{
			NewFixedUpdate();
		}
	}

	protected sealed override void Update()
	{
		base.Update();
	}

	protected virtual void NewFixedUpdate()
	{

	}

	protected abstract T GetCurrentHistoryState();
}
