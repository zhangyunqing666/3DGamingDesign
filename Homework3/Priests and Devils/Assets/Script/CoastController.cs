using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoastController{
	private GameObject coast;
	private Vector3 From = new Vector3 (8, 0, 0);
	private Vector3 To = new Vector3(-7,0,0);
	private Vector3[] positions;
	private int is_from;
	private CharacterController[] characters;
	public CoastController(string Where){
		positions = new Vector3[] {new Vector3 (5, 2, 0),new Vector3 (6.2F, 2, 0), new Vector3 (7.4F, 2, 0),new Vector3 (8.6F, 2, 0),new Vector3 (9.8F, 2, 0),new Vector3 (11F, 2, 0)
		};
		characters = new CharacterController[6];
		if (Where == "from") {
			coast = Object.Instantiate (Resources.Load ("Prefab/Coast", typeof(GameObject)), From, Quaternion.identity, null) as GameObject;
			coast.name = "from";
			is_from = 1;
		} else if(Where == "to"){
			coast = Object.Instantiate (Resources.Load ("Prefab/Coast", typeof(GameObject)), To, Quaternion.identity, null) as GameObject;
			coast.name = "to";
			is_from = -1;
		}
	}
	public Vector3 getEmptyPos(){
		int index = -1;
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] == null) {
				index = i;
				break;
			}
		}
		Vector3 pos = positions [index];

		pos.x *= is_from;
		return pos;
	}
	public void getOnCoast(CharacterController character_ctrl){
		int index = -1;
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] == null) {
				index = i;
				break;
			}
		}
		characters [index] = character_ctrl;
	}

	public CharacterController getOffCoast(string character_name){
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] != null && characters [i].getName () == character_name) {
				CharacterController cc = characters [i];
				characters [i] = null;
				return cc;
			}
		}
		return null;
	}
	public int get_is_from(){
		return is_from;
	}
	public int[] get_character_num(){
		int[] num = { 0, 0 };
		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] == null)
				continue;
			if (!characters[i].Is_Devil ()) {
				num [0]++;
			} else {
				num [1]++;
			}
		}
		return num;
	}
	public void reset(){
		characters = new CharacterController[6];
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
