using UnityEngine;
using System.Collections;

public class SamplePlayer : MonoBehaviour
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
			NetworkDataManager.Instance.FieldPlayerPos = transform.localPosition;
		}
	}
}
