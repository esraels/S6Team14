using UnityEngine;
using System.Collections;
using System;

public class NetworkManager : MonoBehaviour
{
	private const string m_ipAddress = "127.0.0.1";
	private const int m_gamePort = 23466;
	private const string m_gameName = "Team14";
	private const string m_gameTypeName = "S6Team14Server";
	private const int m_maxPlayers = 2;
	private HostData[] m_hostList;

	private Rect windowRect = new Rect(20, 20, 200, 110);

	private void Start ()
	{
		MasterServer.ipAddress = m_ipAddress;
		MasterServer.port = m_gamePort;
		m_hostList = new HostData[0];
	}

	private void Update ()
	{
	
	}

	private void OnGUI ()
	{
		windowRect = GUI.Window(0, windowRect, WindowFunction, "");
	}

	private void WindowFunction (int p_id)
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			if (GUILayout.Button("Create"))
			{
				InitializeServer();
			}
		}
		else
		{
			if (GUILayout.Button("Disconnect"))
			{
				DisconnectFromServer();
			}
		}

		if (GUILayout.Button("Refresh"))
		{
			RefreshServerList();
		}
		
		if (m_hostList.Length > 0)
		{
			foreach (HostData host in m_hostList)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Box(host.gameName);
				if (GUILayout.Button("Connect"))
				{
					ConnectToHost(host);
				}
				GUILayout.EndHorizontal();
			}
		}
	}

	private void InitializeServer ()
	{
		Debug.Log("Creating Server...");
		try
		{
			Network.InitializeServer(m_maxPlayers, m_gamePort, !Network.HavePublicAddress());
			MasterServer.RegisterHost(m_gameTypeName, m_gameName);
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
	}

	private void DisconnectFromServer ()
	{
		Debug.Log("Disconnecting from Server...");
		Network.Disconnect();
	}

	private void RefreshServerList ()
	{
		Debug.Log("Refreshing Server List...");
		MasterServer.RequestHostList(m_gameTypeName);
	}

	void OnMasterServerEvent (MasterServerEvent msEvent)
	{
		Debug.Log("Server List Refreshed");
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			m_hostList = MasterServer.PollHostList();
		}
	}

	private void ConnectToHost (HostData p_hostData)
	{
		Debug.Log("Connecting to Server...");
		Network.Connect(p_hostData);
	}

	private void OnServerInitialized ()
	{
		Debug.Log("Server Initialized");
	}

	private void OnConnectedToServer ()
	{
		Debug.Log("Connected to Server");
	}
}
