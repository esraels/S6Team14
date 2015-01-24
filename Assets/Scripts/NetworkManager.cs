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
	
	[SerializeField] private GameObject m_serverPlayerPrefab;
	[SerializeField] private GameObject m_clientPlayerPrefab;

	private const string m_ipAddress = "127.0.0.1";
	private const int m_gamePort = 25000;
	private const string m_gameName = "Team14";
	private const string m_gameTypeName = "S6Team14Server";
	private const int m_maxPlayers = 4;

	private HostData[] m_hostList;
	private bool m_isHosting;
	public bool IsHosting
	{
		get { return m_isHosting; }
	}
	private bool m_isConnectedToServer;
	public bool IsConnectedToServer
	{
		get { return m_isConnectedToServer; }
	}

	private Rect m_connectionWindow = new Rect(20, 20, 200, 110);

	private void Start ()
	{
		// TODO: Enable this portion if using local server
		//MasterServer.ipAddress = m_ipAddress;
		//MasterServer.port = m_gamePort;

		m_hostList = new HostData[0];
		m_isHosting = false;
		m_isConnectedToServer = false;
	}

	private void Update ()
	{

	}

	private void OnGUI ()
	{
		m_connectionWindow = GUI.Window(1, m_connectionWindow, ConnectionWindow, "");
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

	private void InitializeServer ()
	{
		DebugLogs.Log("Creating Server...");
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
		DebugLogs.Log("Disconnecting from Server...");
		Network.Disconnect();
	}

	private void RefreshServerList ()
	{
		DebugLogs.Log("Refreshing Server List...");
		MasterServer.RequestHostList(m_gameTypeName);
	}

	private void OnMasterServerEvent (MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.RegistrationSucceeded)
		{
			DebugLogs.Log("Registration Succeeded");
		}
		else if (msEvent == MasterServerEvent.HostListReceived)
		{
			DebugLogs.Log("Server List Refreshed");
			m_hostList = MasterServer.PollHostList();
		}
	}

	private void ConnectToHost (HostData p_hostData)
	{
		DebugLogs.Log("Connecting to Server...");
		Network.Connect(p_hostData);
	}

	private void OnServerInitialized ()
	{
		DebugLogs.Log("Server Initialized");		
		m_isHosting = true;
		SpawnServerPlayer();
	}

	private void OnConnectedToServer ()
	{
		DebugLogs.Log("Connected to Server");
		m_isConnectedToServer = true;
		SpawnClientPlayer();
	}

	private void OnFailedToConnect ()
	{
		DebugLogs.Log("Failed to Connect");
	}

	private void OnDisconnectedFromServer ()
	{
		DebugLogs.Log("Disconnected from Server");
	}

	public bool IsServer ()
	{
		return Network.isServer;
	}

	public bool IsClient ()
	{
		return Network.isClient;
	}

	private void SpawnServerPlayer ()
	{
		Network.Instantiate(m_serverPlayerPrefab, Vector3.zero, Quaternion.identity, 0);
	}

	private void SpawnClientPlayer ()
	{
		Network.Instantiate(m_clientPlayerPrefab, Vector3.zero, Quaternion.identity, 0);
	}
}
