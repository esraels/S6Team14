using UnityEngine;
using System.Collections;
using System;

public class NetworkManager : MonoBehaviour
{
	private static NetworkManager m_instance;
	public static NetworkManager Instance
	{
		get
		{
			if ( m_instance == null )
			{
				m_instance = GameObject.FindObjectOfType<NetworkManager>();
			}
			return m_instance;
		}
	}

	private const string m_ipAddress = "127.0.0.1";
	private const int m_gamePort = 25000;
	private const string m_gameName = "Team14";
	private const string m_gameTypeName = "S6Team14Server";
	private const int m_maxPlayers = 2;

	private HostData[] m_hostList;
	private bool m_isConnectedToServer;
	public bool IsConnectedToServer
	{
		get { return m_isConnectedToServer; }
	}

	private Rect m_connectionWindow = new Rect(20, 20, 200, 110);
	private Rect m_fireDataWindow = new Rect(20, 150, 200, 100);

	private void Start ()
	{
		// TODO: Enable this portion if using local server
		//MasterServer.ipAddress = m_ipAddress;
		//MasterServer.port = m_gamePort;

		m_hostList = new HostData[0];
		m_isConnectedToServer = false;
	}

	private void Update ()
	{
	
	}

	private void OnGUI ()
	{
		m_connectionWindow = GUI.Window(0, m_connectionWindow, ConnectionWindow, "");
	}

	private void ConnectionWindow (int p_id)
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

	private void FireDataWindow ()
	{
		if (GUILayout.Button("Send Position"))
		{

		}

		if (GUILayout.Button("Send Data"))
		{
			
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

	private void OnMasterServerEvent (MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			Debug.Log("Server List Refreshed");
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
		m_isConnectedToServer = true;
	}

	public bool IsServer ()
	{
		return Network.isServer;
	}

	public bool IsClient ()
	{
		return Network.isClient;
	}
}
