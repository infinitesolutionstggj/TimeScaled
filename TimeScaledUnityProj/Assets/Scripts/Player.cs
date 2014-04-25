using UnityEngine;
using System.Collections;

public class Player : TimeScaledObject
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
	
	protected override void Update () 
	{
		base.Update();

		HandleMovement();
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	void HandleMovement()
	{
		Vector3 moveVec = Vector3.zero;
		moveVec.x = Input.GetAxis("Horizontal");
		moveVec.y = Input.GetAxis("Vertical");

		transform.Translate(moveVec * speed * Time.deltaTime * LocalTimeScale);
	}
}
