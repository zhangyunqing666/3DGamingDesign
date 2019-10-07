using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController {
	private GameObject boat;
	private Vector3 From = new Vector3 (2, -0.3F, 0);
	private Vector3 To = new Vector3 (-1, -0.3F, 0);
	private Vector3[] from_positions;
	private Vector3[] to_positions;
	int is_from;
	CharacterController[] characters = new CharacterController[2];

	private Move move;
	public float speed = 30;


	public BoatController() {
		is_from = 1;

		from_positions = new Vector3[] { new Vector3 (3.5F, 0.45F, 0), new Vector3 (0.5F, 0.45F, 0) };
		to_positions = new Vector3[] { new Vector3 (0.5F, 0.45F, 0), new Vector3 (-2.5F, 0.45F, 0) };

		boat = Object.Instantiate (Resources.Load ("Prefab/Boat", typeof(GameObject)), From, Quaternion.identity, null) as GameObject;
		boat.name = "boat";

		move = boat.AddComponent (typeof(Move)) as Move;
		boat.AddComponent (typeof(ClickOp));
	}
	public bool is_empty(){
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] != null) {
				return false;
			}
		}
		return true;
	}
	//######################################	
	public Vector3 Move_to() {
		if (is_from == -1) {
			is_from = 1;
			return From;
		} else {
			is_from = -1;
			return To;
		}
	}
	//#######################################
	public int getEmptyIndex() {
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] == null) {
				return i;
			}
		}
		return -1;
	}

	public Vector3 getEmptyPosition() {
		Vector3 pos;
		int emptyIndex = -1;
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] == null) {
				emptyIndex = i;
			}
		}
		if (is_from == -1) {
			pos = to_positions[emptyIndex];
		} else {
			pos = from_positions[emptyIndex];
		}
		return pos;
	}

	public void GetOnBoat(CharacterController characterCtrl) {
		int index = -1;
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] == null) {
				index = i;

			}
		}
		characters [index] = characterCtrl;
	}

	public CharacterController GetOffBoat(string characters_name) {
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] != null && characters [i].getName () == characters_name) {
				CharacterController cc = characters [i];
				characters [i] = null;
				return cc;
			}
		}
		return null;
	}

	public GameObject getGameobj() {
		return boat;
	}

	public int get_is_from() { 
		return is_from;
	}

	public int[] getCharacterNum() {
		int[] count = {0, 0};
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] == null)
				continue;
			if (!characters [i].Is_Devil ()) {
				count[0]++;
			} else {
				count[1]++;
			}
		}
		return count;
	}

	public void reset() {
		if (is_from == -1) {
			Move_to ();
		}
		boat.transform.position = From;
		characters = new CharacterController[2];
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
