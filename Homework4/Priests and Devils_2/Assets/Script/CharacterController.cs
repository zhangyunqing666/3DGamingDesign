using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDir;
public class CharacterController{
	private GameObject character;
	private Move move;
	private ClickOp click;
	private bool is_devil;
	private bool is_on_boat = false;
	private CoastController coast;
	public float move_speed = 30;
	// Use this for initialization
	public CharacterController(string name){
		if(name == "priest"){
			character = Object.Instantiate(Resources.Load("Prefab/Priest",typeof(GameObject)),Vector3.zero,Quaternion.identity,null) as GameObject;
			is_devil = false;
		}else if(name == "devil"){
			character = Object.Instantiate(Resources.Load("Prefab/Devil",typeof(GameObject)),Vector3.zero,Quaternion.identity,null) as GameObject;
			is_devil = true;
		}
		move = character.AddComponent(typeof(Move))as Move;
		click = character.AddComponent(typeof(ClickOp))as ClickOp;
		click.setController(this);
	}
	public void setName(string _name){
		character.name = _name;
	}
	public string getName(){
		return character.name;
	}
	public bool Is_Devil(){
		return is_devil;
	}
	public bool Is_On_Boat(){
		return is_on_boat;
	}
	public void setPos(Vector3 pos){
		character.transform.position = pos;
	}
	public Vector3 getPos(){
		return character.transform.position;
	}
	//######################
	public GameObject getGameobj(){
		return character;
	}


	public CoastController getCoastController() {
		return coast;
	}
	public void getOnBoat(BoatController boatCtrl) {
		coast = null;
		character.transform.parent = boatCtrl.getGameobj().transform;
		is_on_boat = true;
	}

	public void getOnCoast(CoastController coastCtrl) {
		coast = coastCtrl;
		character.transform.parent = null;
		is_on_boat = false;
	}
	public void reset() {
		coast = (Director.getInstace ().current as FirstController).fromCoast;
		getOnCoast (coast);
		setPos (coast.getEmptyPos());
		coast.getOnCoast (this);
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
