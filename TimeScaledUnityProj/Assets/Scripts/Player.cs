using UnityEngine;
using System.Collections;

public class PlayerHS
{
	public Vector3 position;
	public Quaternion rotation;
}

public class Player : HistoricalComponent<PlayerHS>
{
	public static Player Main = null;
	public float speed = 5.0f;

	// Use this for initialization
	protected override void Awake () 
	{
		base.Awake();

		if(Main == null)
		{
			Main = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void OnDestroy()
	{
		if (Main == this)
		{
			Main = null;
		}
	}
	

	protected override void LateUpdate()
	{
		base.LateUpdate();
	}

	protected override void NewFixedUpdate()
	{
		base.NewFixedUpdate();

		HandleMovement();
	}

	void HandleMovement()
	{
		Vector3 moveVec = Vector3.zero;
		moveVec.x = Input.GetAxis("Horizontal");
		moveVec.y = Input.GetAxis("Vertical");

		transform.Translate(moveVec * speed * Time.fixedDeltaTime * LocalTimeScale);
	}

	protected override PlayerHS GetCurrentHistoryState()
	{
		PlayerHS output = new PlayerHS();
		output.position = transform.position;
		output.rotation = transform.rotation;
		return output;
	}

	protected override void ApplyHistoryState(PlayerHS state)
	{
		transform.position = state.position;
		transform.rotation = state.rotation;
	}
}
