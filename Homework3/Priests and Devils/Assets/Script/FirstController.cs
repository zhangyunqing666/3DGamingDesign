using System.Collections;
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

	void Awake() {
		Director director = Director.getInstace ();
		director.current = this;
		userGUI = gameObject.AddComponent <UserGUI>() as UserGUI;
		characters = new CharacterController[6];
		loadResources ();
	}

	public void loadResources() {
		GameObject River = Instantiate (Resources.Load ("Prefab/River", typeof(GameObject)), river, Quaternion.identity, null) as GameObject;
		River.name = "River";
		fromCoast = new CoastController ("from");
		toCoast = new CoastController ("to");
		boat = new BoatController ();

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
		boat.Move_to ();
		userGUI.status = check_game_over ();
	}

	public void characterIsClicked(CharacterController characterCtrl) {
		if (characterCtrl.Is_On_Boat ()) {
			CoastController whichCoast;
			if (boat.get_is_from () == -1) {
				whichCoast = toCoast;
			} else {
				whichCoast = fromCoast;
			}

			boat.GetOffBoat (characterCtrl.getName());
			characterCtrl.Move_to_pos (whichCoast.getEmptyPos ());
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
			characterCtrl.Move_to_pos (boat.getEmptyPosition());
			characterCtrl.getOnBoat (boat);
			boat.GetOnBoat (characterCtrl);
		}
		userGUI.status = check_game_over ();
	}

	int check_game_over() {	
		int from_priest = 0;
		int from_devil = 0;
		int to_priest = 0;
		int to_devil = 0;

		int[] fromCount = fromCoast.get_character_num ();
		from_priest += fromCount[0];
		from_devil += fromCount[1];

		int[] toCount = toCoast.get_character_num ();
		to_priest += toCount[0];
		to_devil += toCount[1];

		if (to_priest + to_devil == 6)		
			return 2;

		int[] boatCount = boat.getCharacterNum ();
		if (boat.get_is_from () == -1) {	
			to_priest += boatCount[0];
			to_devil += boatCount[1];
		} else {	
			from_priest += boatCount[0];
			from_devil += boatCount[1];
		}
		if (from_priest < from_devil && from_priest > 0) {	
			return 1;
		}
		if (to_priest < to_devil && to_priest > 0) {
			return 1;
		}
		return 0;	
	}

	public void restart() {
		boat.reset ();
		fromCoast.reset ();
		toCoast.reset ();
		for (int i = 0; i < characters.Length; i++) {
			characters [i].reset ();
		}
	}
}