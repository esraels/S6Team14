using UnityEngine;
using System.Collections;

public class SampleServerPlayer : MonoBehaviour
{
	private Rect m_controlPanel = new Rect(240, 20, 200, 110);

	private void OnGUI ()
	{
		if (NetworkManager.Instance.IsHosting)
		{
			m_controlPanel = GUI.Window(0, m_controlPanel, ControlPanel, "");
		}
	}

	private void ControlPanel (int p_id)
	{
		if (GUILayout.Button("Control 1"))
		{
			networkView.RPC("Control1", RPCMode.Others);
		}
		if (GUILayout.Button("Control 2"))
		{
			networkView.RPC("Control2", RPCMode.Others);
		}
	}

	[RPC] private void Control1 ()
	{
		DebugLogs.Log("Control1");
	}

	[RPC] private void Control2 ()
	{
		DebugLogs.Log("Control2");
	}
}
