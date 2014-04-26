using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private float btnX;
	private float btnY;
	private float btnW;
	private float btnH;

	private string gameName = "Networking_Tutorial_ChronoTank";
	private bool refreshing;
	private HostData[] hostData;

	public GameObject playerPrefab;
	public Transform spawnObject;


	// Use this for initialization
	void Awake () 
	{
		btnX = Screen.width * 0.05f;
		btnY = Screen.width * 0.05f;
		btnW = Screen.width * 0.1f;
		btnH = Screen.width * 0.1f;
	}

	public void StartServer()
	{
		Network.InitializeServer(32, 25001, !Network.HavePublicAddress());
		MasterServer.RegisterHost(gameName, "MY AWESOME GAME BECAUSE IM AWESOME.", "This is our test server");
	}

	public void RefreshHostList()
	{
		MasterServer.RequestHostList(gameName);
		refreshing = true;
	}

	public void OnMasterServerEvent(MasterServerEvent mse)
	{
		if(mse == MasterServerEvent.RegistrationSucceeded)
		{
			Debug.Log("Registered Server");
		}
	}

	void Update ()
	{
		if(refreshing)
			if(MasterServer.PollHostList().Length > 0)
		{
			refreshing = false;
			hostData = MasterServer.PollHostList();
		}
	}

	public void spawnPlayer()
	{
		Network.Instantiate(playerPrefab, spawnObject.position, Quaternion.identity, 0);
	}
	
	public void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
		spawnPlayer();
	}
	
	public void OnConnectedToServer()
	{
		spawnPlayer();
	}

	public void OnGUI()
	{
		if(!Network.isClient && !Network.isServer)
		{
			if(GUI.Button(new Rect(btnX, btnY, btnW, btnH), "Start Server"))
			{
				Debug.Log("Starting");
				StartServer();
			}
			if(GUI.Button(new Rect(btnX, btnY * 1.2f + btnH, btnW, btnH), "Refresh Hosts"))
			{
				Debug.Log("Refreshing");
				RefreshHostList();
			}
			if(hostData != null)
			{
				for(int i = 0; i < hostData.Length; i++)
				{
					if(GUI.Button(new Rect(btnX * 1.5f + btnW, btnY * 1.2f + (btnH * i), btnW * 3f, btnH), hostData[i].gameName))
					{
						Network.Connect(hostData[i]);
					}
				}
			}
		}
	}
}
