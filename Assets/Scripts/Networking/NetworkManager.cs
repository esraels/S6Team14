using UnityEngine;
using System.Collections;
using System;

public class NetworkManager : MonoBehaviour
{
	private string m_gameName = "";
	private string m_gamePort = "25566";

	private const int k_maxPlayers = 2;
	private const string k_gameTypeName = "S6Team14Server";

	private Rect m_gameCreationRect = new Rect(20, 20, 200, 150);
	private Rect m_gameListRect = new Rect(20, 190, 200, 200);

	private void Start ()
	{
	
	}

	private void Update ()
	{
	
	}

	private void OnGUI ()
	{
		m_gameCreationRect = GUI.Window(0, m_gameCreationRect, GameCreationWindow, "Create Game");
		m_gameListRect = GUI.Window(1, m_gameListRect, GameListWindow, "Select Game");
	}

	private void GameCreationWindow (int p_id)
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			GUILayout.Label("Game Name");
			m_gameName = GUILayout.TextField(m_gameName);
			
			GUILayout.Label("Port");
			m_gamePort = GUILayout.TextField(m_gamePort);
			
			if (GUILayout.Button("Create"))
			{
				Debug.Log("Creating Server...");
				try
				{
					Network.InitializeServer(k_maxPlayers, int.Parse(m_gamePort), !Network.HavePublicAddress());
					MasterServer.RegisterHost(k_gameTypeName, m_gameName);
				}
				catch (Exception e)
				{
					Debug.Log(e.Message);
				}
			}
		}
		else
		{
			if (GUILayout.Button("Disconnect"))
			{
				Network.Disconnect();
			}
		}
	}

	private void GameListWindow (int p_id)
	{
		if (GUILayout.Button("Refresh"))
		{
			MasterServer.RequestHostList("S6Team14Server");
		}

		if (MasterServer.PollHostList().Length != 0)
		{
			HostData[] hostList = MasterServer.PollHostList();
			foreach (HostData host in hostList)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Box(host.gameName);
				if (GUILayout.Button("Connect"))
				{
					Network.Connect(host);
				}
				GUILayout.EndHorizontal();
			}
		}
	}

	private void OnServerInitialized ()
	{
		Debug.Log("Server Initialized");
	}
}
