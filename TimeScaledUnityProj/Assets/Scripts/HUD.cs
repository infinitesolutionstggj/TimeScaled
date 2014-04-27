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

	public static void CheckLoadTextures()
	{
		if (Background == null)
			Background = Resources.Load<Texture2D>("HUD_BG");
		if (LockA == null)
			LockA = Resources.Load<Texture2D>("HUD_LockA");
		if (LockX == null)
			LockX = Resources.Load<Texture2D>("HUD_LockX");
		if (LockY == null)
			LockY = Resources.Load<Texture2D>("HUD_LockY");
		if (LockB == null)
			LockB = Resources.Load<Texture2D>("HUD_LockB");
	}

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

		CheckLoadTextures();
	}

	public void OnGUI()
	{
		foreach (var player in players)
		{
			if (player == null)
				continue;

			GUI.DrawTexture(ScreenRect[player.playerNumber - 1], Background);

			if (player.CoolDownA > 0)
				GUI.DrawTexture(ScreenRect[player.playerNumber - 1], LockA);
			if (player.tankSpecial.CoolDownX > 0)
				GUI.DrawTexture(ScreenRect[player.playerNumber - 1], LockX);
			if (player.tankSpecial.CoolDownY > 0)
				GUI.DrawTexture(ScreenRect[player.playerNumber - 1], LockY);
			if (player.tankSpecial.CoolDownB > 0)
				GUI.DrawTexture(ScreenRect[player.playerNumber - 1], LockB);
			
			GUI.DrawTexture(ScreenRect[player.playerNumber - 1], player.tankSpecial.TextOverlay);
		}
	}
}
