using UnityEngine;
using System.Collections;

public class PlayerInField : MonoBehaviour {

	public float m_speed = 1;
	public float m_speedLimit = 1;

	Transform m_camera = null;

	// Use this for initialization
	void Start () {
	
		if(m_camera == null){
			m_camera = GameObject.Find("Camera").transform;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 move;
		move.x = Input.GetAxis("Horizontal");
		move.y = Input.GetAxis("Vertical");
		move.z = 0;

		rigidbody2D.velocity = move;

		Vector3 newPosCam = transform.position;
		newPosCam.z = m_camera.position.z;

		m_camera.position = newPosCam;

//		move = move * m_speed * Time.deltaTime;
//		if(move.magnitude >= m_speedLimit){
//			move = move.normalized * m_speedLimit;
//		}
//		transform.position += move;

		//m_camera.localPosition = move;

	}
}
