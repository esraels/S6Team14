﻿using UnityEngine;
using System.Collections;

public class SensorObject : MonoBehaviour {

	enum State {
		TURNING_OFF,
		TURNING_ON
	};

	public string m_passcode;

	Animator m_animator = null;

	// Use this for initialization
	void Start () {
		if(m_animator == null){
			m_animator = gameObject.GetComponent<Animator>();
		}
		m_animator.Play("SensorStayOff");
	}
	
	// Update is called once per frame
	void Update () {
		//???: test code only
		if(Input.GetKeyUp("c")){
			OpenSensor();
		}
		else if(Input.GetKeyUp("v")){
			CloseSensor();
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
				player.HidePasscode();
			}
		}
	}

	public void OpenSensor(){
		m_animator.SetInteger("SensorState", (int)State.TURNING_ON );
	}
	
	public void CloseSensor(){
		m_animator.SetInteger("SensorState", (int)State.TURNING_OFF );
	}


}
