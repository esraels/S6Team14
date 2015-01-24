using UnityEngine;
using System.Collections;

public class DoorObject : MonoBehaviour {

	enum State {
		CLOSING,
		OPENING
	};

	public int m_passcode = 1234;
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

	public void OpenDoor(){
		m_animator.SetInteger("DoorState", (int)State.OPENING );
	}

	public void CloseDoor(){
		m_animator.SetInteger("DoorState", (int)State.CLOSING );
	}


}
