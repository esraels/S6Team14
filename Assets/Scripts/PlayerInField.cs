using UnityEngine;
using System.Collections;

public class PlayerInField : MonoBehaviour {

	enum PlayerAniMove{
		IDLE,
		WALK_UP,
		WALK_DOWN,
		WALK_RIGHT,
		WALK_LEFT
	};

	public float m_speed = 1;
	public float m_speedLimit = 1;
	public Animator m_animator = null;

	Transform m_camera = null;

	// Use this for initialization
	void Start () {
	
		if(m_camera == null){
			m_camera = GameObject.Find("Camera").transform;
		}
		if(m_animator == null){
			m_animator = transform.FindChild("walkanimation").gameObject.GetComponent<Animator>();
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

		//=================================
		// set animation based on movement
		//---------------------------------
		PlayerAniMove aniValue = 0; //default to idle animation

		if(Mathf.Abs(move.x) > Mathf.Abs(move.y)){
			if(move.x > 0){
				aniValue = PlayerAniMove.WALK_RIGHT;
			}
			else if(move.x < 0){
				aniValue = PlayerAniMove.WALK_LEFT;
			}
		}
		else {
			if(move.y > 0){
				aniValue = PlayerAniMove.WALK_UP;
			}
			else if(move.y < 0){
				aniValue = PlayerAniMove.WALK_DOWN;
			}
		}

		m_animator.SetInteger("move_dir", (int)aniValue);

	}


	public void ShowPasscode(int p_passcode){

		//TODO: show UI above the player character that displays the passcode.
		Debug.Log("The passcode is" + p_passcode);

	}

}
