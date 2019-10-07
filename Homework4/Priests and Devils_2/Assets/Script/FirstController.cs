﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDir;

public class FirstController : MonoBehaviour, SceneController, UserAction {

	private Vector3 river = new Vector3(0.5F,-1,0);
	UserGUI userGUI;
	public CoastController fromCoast;
	public CoastController toCoast;
	public BoatController boat;
	private CharacterController[] characters;
	public Move actionManager;
	Judge judge;
	void Awake() {
		Director director = Director.getInstace ();
		director.current = this;
		userGUI = gameObject.AddComponent <UserGUI>() as UserGUI;
		characters = new CharacterController[6];
		loadResources ();
		//#########################
		actionManager = gameObject.AddComponent<Move> ()as Move;
	}

	public void loadResources() {
		GameObject River = Instantiate (Resources.Load ("Prefab/River", typeof(GameObject)), river, Quaternion.identity, null) as GameObject;
		River.name = "River";
		fromCoast = new CoastController ("from");
		toCoast = new CoastController ("to");
		boat = new BoatController ();
		judge = new Judge (fromCoast, toCoast, boat);
		loadCharacter ();
	}

	private void loadCharacter() {
		for (int i = 0; i < 3; i++) {
			CharacterController cha = new CharacterController ("priest");
			cha.setName("priest" + i);
			cha.setPos (fromCoast.getEmptyPos ());
			cha.getOnCoast (fromCoast);
			fromCoast.getOnCoast (cha);
			characters [i] = cha;
		}

		for (int i = 0; i < 3; i++) {
			CharacterController cha = new CharacterController ("devil");
			cha.setName("devil" + i);
			cha.setPos (fromCoast.getEmptyPos());
			cha.getOnCoast (fromCoast);
			fromCoast.getOnCoast (cha);
			characters [i+3] = cha;
		}
	}


	public void moveBoat() {
		if (boat.is_empty ())
			return;
		actionManager.moveBoat (boat.getGameobj (), boat.Move_to (), boat.speed);
	}

	public void moveCharacter(CharacterController characterCtrl) {
		if (characterCtrl.Is_On_Boat ()) {
			CoastController whichCoast;
			if (boat.get_is_from () == -1) {
				whichCoast = toCoast;
			} else {
				whichCoast = fromCoast;
			}

			boat.GetOffBoat (characterCtrl.getName());

//######################################################
			Vector3 end_pos = whichCoast.getEmptyPos();
			Vector3 mid_pos = new Vector3 (characterCtrl.getGameobj ().transform.position.x,end_pos.y, end_pos.z);
			actionManager.moveCharacter(characterCtrl.getGameobj(),mid_pos,end_pos,characterCtrl.move_speed);
//######################################################
			characterCtrl.getOnCoast (whichCoast);
			whichCoast.getOnCoast (characterCtrl);

		} else {								
			CoastController whichCoast = characterCtrl.getCoastController ();

			if (boat.getEmptyIndex () == -1) {		
				return;
			}

			if (whichCoast.get_is_from () != boat.get_is_from ())	
				return;

			whichCoast.getOffCoast(characterCtrl.getName());
//######################################################
			Vector3 end_pos = boat.getEmptyPosition();
			Vector3 mid_pos = new Vector3 (end_pos.x,characterCtrl.getGameobj().transform.position.y,end_pos.z);
			actionManager.moveCharacter(characterCtrl.getGameobj(),mid_pos,end_pos,characterCtrl.move_speed);
//######################################################
			characterCtrl.getOnBoat (boat);
			boat.GetOnBoat (characterCtrl);
		}
	}
	public void restart() {
		boat.reset ();
		fromCoast.reset ();
		toCoast.reset ();
		for (int i = 0; i < characters.Length; i++) {
			characters [i].reset ();
		}
	}
	void Update () {
		userGUI.status = judge.check ();
	}
}