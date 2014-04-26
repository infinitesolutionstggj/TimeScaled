using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	public static NetworkManager Main = null;
	private int lastLevelPrefix = 0;

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

	public void SpawnPlayer(GameObject obj, Transform objTransform, int group = 0)
	{
		Network.Instantiate(obj, objTransform.position, objTransform.rotation, group);
	}
	
	public void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
		QueueLoadLevel("TestScene");
	}
	
	public void OnConnectedToServer()
	{
		QueueLoadLevel("TestScene");
	}

	public void QueueLoadLevel(string levelName)
	{
		Network.RemoveRPCsInGroup(0);
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
