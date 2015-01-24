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
	public Vector2 m_PasscodePos = new Vector2(0,0);

	string m_curPasscode = "";

	Animator m_animator;
	Transform m_callout;
	Transform m_camera = null;

	// Use this for initialization
	void Start () {
	
		if(m_camera == null){
			m_camera = GameObject.Find("Camera").transform;
		}
		if(m_animator == null){
			m_animator = transform.FindChild("walkanimation").gameObject.GetComponent<Animator>();
		}

		m_callout = transform.FindChild("callout");
		m_callout.gameObject.SetActive(false); //hide initially

		//m_texCallout = m_callout.GetComponent<SpriteRenderer>().guiTexture.texture as Texture2D;

	}
	
	// Update is called once per frame
	void Update () {
	
		//=================================
		// move the player by user input
		// note: can collide in walls
		//---------------------------------
		Vector3 move;
		move.x = Input.GetAxis("Horizontal");
		move.y = Input.GetAxis("Vertical");
		move.z = 0;

		rigidbody2D.velocity = move;

		//=================================
		// make the camera follow the player
		//---------------------------------
		Vector3 newPosCam = transform.position;
		newPosCam.z = m_camera.position.z;

		m_camera.position = newPosCam;

		//=================================
		// set animation based on movement
		//---------------------------------
		PlayerAniMove aniValue = 0; //default to idle animation

		if(Mathf.Abs(move.x) > Mathf.Abs(move.y)){
			if(move.x > 0)
				aniValue = PlayerAniMove.WALK_RIGHT;
			else if(move.x < 0)
				aniValue = PlayerAniMove.WALK_LEFT;
		}
		else {
			if(move.y > 0)
				aniValue = PlayerAniMove.WALK_UP;
			else if(move.y < 0)
				aniValue = PlayerAniMove.WALK_DOWN;
		}

		m_animator.SetInteger("move_dir", (int)aniValue);

	}

	public void OnGUI(){
		//=================================
		// Show passcode via unity gui
		//---------------------------------
		if(m_curPasscode != ""){
			Camera cam = m_camera.GetComponent<Camera>();
			Vector3 screenPos = cam.WorldToScreenPoint(m_callout.position);
			screenPos.y = Screen.height - screenPos.y;
			//screenPos -= new Vector3(20,20,0);

			//Vector2 sizeC = new Vector2(50,50);

//			CircleCollider2D bounds = GetComponent<CircleCollider2D>();
//			float playerHeight = bounds.radius;

			//Vector2 posC = new Vector2(screenPos.x, screenPos.y - playerHeight - sizeC.y);
			//Vector2 posText = new Vector2(posC.x + sizeC.x * 0.5f, posC.y + sizeC.y * 0.5f);

			//GUI.Label(new Rect(posC.x, posC.y, sizeC.x, sizeC.y), m_texCallout);

			Vector2 pos = new Vector2(screenPos.x + m_PasscodePos.x, screenPos.y - m_PasscodePos.y);
			GUI.Label(new Rect(pos.x, pos.y, 50, 50), m_curPasscode);
		}

	}

	public void ShowPasscode(string p_passcode){

		m_curPasscode = p_passcode;
		m_callout.gameObject.SetActive(true);
		//TODO: show UI above the player character that displays the passcode.
		Debug.Log("The passcode is" + p_passcode);

	}

	public void HidePasscode(){
		m_curPasscode = "";
		m_callout.gameObject.SetActive(false);
	}

}
