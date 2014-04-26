using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	public static NetworkManager Main = null;
	private int lastLevelPrefix = 0;
	public GameObject playerPrefab;

	void Awake()
	{
		if (Main == null)
		{
			Main = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(this.gameObject);
	}

	void OnDestroy()
	{
		if (Main == this)
			Main = null;
	}

	public static Object SpawnObject(Object obj, Vector3 position, Quaternion rotation, int group = 0)
	{
		return Network.Instantiate(obj, position, rotation, group);
	}
	
	public void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
		QueueLoadLevel("TestScene");
	}
	
	public void OnConnectedToServer()
	{
		QueueLoadLevel("TestScene");
		NetworkManager.SpawnObject(playerPrefab, Vector3.zero, Quaternion.identity);
	}

	public void QueueLoadLevel(string levelName)
	{
		Network.RemoveRPCsInGroup(0);
		Network.RemoveRPCsInGroup(1);
		networkView.RPC("LoadLevel", RPCMode.AllBuffered, levelName, lastLevelPrefix + 1);
	}

	[RPC]
	public void LoadLevel(string levelName, int levelPrefix)
	{
		StartCoroutine(LoadLevelCoroutine(levelName, levelPrefix));
	}

	public IEnumerator LoadLevelCoroutine(string levelName, int levelPrefix)
	{
		lastLevelPrefix = levelPrefix;

		Network.SetSendingEnabled(0, false);
		Network.isMessageQueueRunning = false;
		Network.SetLevelPrefix(levelPrefix);
		Application.LoadLevel(levelName);
		yield return null;
		yield return null;

		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled(0, true);

		foreach (GameObject go in FindObjectsOfType<GameObject>())
		{
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnDisconnectedFromServer()
	{

	}
}
