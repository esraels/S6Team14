using UnityEngine;
using System.Collections;

public class NetworkDataManager : MonoBehaviour
{
	private static NetworkDataManager m_instance;
	public static NetworkDataManager Instance
	{
		get
		{
			if ( m_instance == null )
			{
				m_instance = GameObject.FindObjectOfType<NetworkDataManager>();
			}
			return m_instance;
		}
	}

	private Vector3 m_fieldPlayerPos;
	public Vector3 FieldPlayerPos
	{
		get { return m_fieldPlayerPos; }
		set { m_fieldPlayerPos = value; }
	}
	private Vector3 m_fieldPlayerSyncPos;

	private void OnSerializeNetworkView (BitStream p_bitStream, NetworkMessageInfo p_info)
	{
		if (p_bitStream.isWriting)
		{
			if (NetworkManager.Instance.IsClient())
			{
				m_fieldPlayerSyncPos = m_fieldPlayerPos;
				p_bitStream.Serialize(ref m_fieldPlayerSyncPos);
			}
		}
		else
		{
			if (NetworkManager.Instance.IsServer())
			{
				p_bitStream.Serialize(ref m_fieldPlayerSyncPos);
				m_fieldPlayerPos = m_fieldPlayerSyncPos;
				Debug.Log("Pos Updated: " + m_fieldPlayerPos.y.ToString("F2"));
			}
		}
	}
}
