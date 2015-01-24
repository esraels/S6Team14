using UnityEngine;
using System.Collections;

public class SampleClientPlayer : MonoBehaviour
{
	private Vector3 m_syncPosition;

	private void Start ()
	{
	
	}

	private void Update ()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			transform.Translate(Vector3.up * 10);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			transform.Translate(Vector3.down * 10);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.Translate(Vector3.left * 10);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			transform.Translate(Vector3.right * 10);
		}
	}

	private void OnSerializeNetworkView (BitStream p_stream, NetworkMessageInfo p_info)
	{
		if (p_stream.isWriting)
		{
			Vector3 syncPos = transform.localPosition;
			p_stream.Serialize(ref syncPos);
		}
		else if (p_stream.isReading)
		{
			DebugLogs.Log("Reading");
			Vector3 syncPos = transform.localPosition;
			p_stream.Serialize(ref syncPos);
		}
	}
}
