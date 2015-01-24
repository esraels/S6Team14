using UnityEngine;
using System.Collections;

public class DebugLogs : MonoBehaviour
{
	private static string m_message;
	private Rect m_rect;

	private void Start ()
	{
		m_rect = new Rect(240, 20, Screen.width, Screen.height);
		m_message = "Logs:";
	}

	private void OnGUI ()
	{
		GUI.Label(m_rect, m_message);
	}

	public static void Log (string p_message)
	{
		m_message += "\n" + p_message;
	}
}
