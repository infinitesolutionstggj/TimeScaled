using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class HistoricalComponent<T> : TimeScaledObject
{
	public LinkedList<T> history;
	public bool IsFull { get; private set; }
	public virtual bool IsRewinding
	{
		get { return affectingReverseBubbles.Count > 0; }
	}

	protected override void Awake()
	{
		base.Awake();

		history = new LinkedList<T>();
		IsFull = false;
	}

	protected sealed override void FixedUpdate()
	{
		base.FixedUpdate();

		while (history.Count > GameSettings.MaxHistoryStates)
		{
			if (!IsFull)
				IsFull = true;

			history.RemoveFirst();
		}

		if (IsRewinding)
		{
			if (history.Count <= 0)
			{
				if (!IsFull)
				{
					Destroy(gameObject);
				}
			}
			else
			{
				ApplyHistoryState(history.Last.Value);
				history.RemoveLast();
			}
		}
		else
		{
			NewFixedUpdate();
			history.AddLast(GetCurrentHistoryState());
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

	protected abstract void ApplyHistoryState(T state);
}
