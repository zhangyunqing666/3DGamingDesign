using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDir;
public class ClickOp : MonoBehaviour {
	private UserAction action;
	CharacterController character_ctrl;
	public void setController(CharacterController character){
		character_ctrl = character;
	}

	// Use this for initialization
	void Start () {
		action = Director.getInstace().current as UserAction;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown(){
		if (gameObject.name == "boat") {
			action.moveBoat ();
		} else {
			action.characterIsClicked (character_ctrl);
		}
	}
}
