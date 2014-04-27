using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	public static HUD Instance { get; private set; }

	public static Texture2D Background;
	public static Texture2D LockA;
	public static Texture2D LockX;
	public static Texture2D LockY;
	public static Texture2D LockB;

	public Player[] players;

	public float screenEdgeBuffer;
	public float playerCardWidth;
	public float playerCardHeight;
	private Rect[] ScreenRect;

	public void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	public void Start()
	{
		ScreenRect = new Rect[4];
		for (int i = 0; i < 4; i++)
		{
			if (i % 2 == 0)
				ScreenRect[i].x = screenEdgeBuffer;
			else
				ScreenRect[i].x = Screen.width - playerCardWidth - screenEdgeBuffer;

			if (i / 2 == 0)
				ScreenRect[i].y = screenEdgeBuffer;
			else
				ScreenRect[i].y = Screen.height - playerCardHeight - screenEdgeBuffer;

			ScreenRect[i].width = playerCardWidth;
			ScreenRect[i].height = playerCardHeight;
		}
	}

	public void OnGUI()
	{
		foreach (var player in players)
		{

		}
	}
}
