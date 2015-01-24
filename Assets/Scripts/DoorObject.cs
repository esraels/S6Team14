using UnityEngine;
using System.Collections;

public class DoorObject : MonoBehaviour {

	enum State {
		CLOSING,
		OPENING
	};

	public string m_passcode = "1234";
	bool m_bIsOpening;

	Animator m_animator = null;

	// Use this for initialization
	void Start () {
		if(m_animator == null){
			m_animator = gameObject.GetComponent<Animator>();
		}
		m_animator.Play("DoorStayClose");

	}
	
	// Update is called once per frame
	void Update () {
	
		//???: test code only
		if(Input.GetKeyUp("z")){
			OpenDoor();
		}
		else if(Input.GetKeyUp("x")){
			CloseDoor();
		}

	}

	void OnTriggerEnter2D(Collider2D p_collidedObj){
		PlayerInField player = p_collidedObj.gameObject.GetComponent<PlayerInField>();
		if(player){
			player.ShowPasscode(m_passcode);
		}
	}

	void OnTriggerExit2D(Collider2D p_collidedObj){
		//=================================
		// If player goes outside the range 
		//   of this door, hide passcode.
		//---------------------------------
		PlayerInField player = p_collidedObj.gameObject.GetComponent<PlayerInField>();
		if(player){
			CircleCollider2D bounds = GetComponent<CircleCollider2D>();
			Vector2 posP = player.transform.position;
			Vector2 posG = transform.position;
			float radius = bounds.radius * transform.localScale.x;

			if((posP - posG).sqrMagnitude > radius * radius){
				Debug.Log("  --player went outside the boundary");
				player.HidePasscode();
			}
		}
	}

	public void OpenDoor(){
		m_animator.SetInteger("DoorState", (int)State.OPENING );
	}

	public void CloseDoor(){
		m_animator.SetInteger("DoorState", (int)State.CLOSING );
	}


}
