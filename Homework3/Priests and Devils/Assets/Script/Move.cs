using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public float move_speed = 30;
	private int move_to;
	public Vector3 dest;
	public Vector3 temp;
	private int moveable = 1;
	// Use this for initialization
	void Start () {
		 
	
	}
	
	// Update is called once per frame
	void Update () {
		if (moveable == 0)
			return;
		else {
			if (move_to == 1) {
				transform.position = Vector3.MoveTowards (transform.position, temp, move_speed * Time.deltaTime);
				if (transform.position == temp)
					move_to = 2;
			} else if (move_to == 2) {
				transform.position = Vector3.MoveTowards(transform.position, dest, move_speed*Time.deltaTime);
				if (transform.position == dest)
					move_to = 0;
			}
		}
	}

	public void setDest(Vector3 _dest){
		if (moveable == 0)
			return;
		else {
			//from ------------>temp
			//                  |
			//                  |   
			//                 \|/
			//                 dest
			temp = _dest;
			dest = _dest;
			if (_dest.y == transform.position.y) {
				move_to = 2;
			}
			else if (_dest.y < transform.position.y) {
				temp.y = transform.position.y+3;
			}else{
				temp.x = transform.position.x;
			}
			move_to = 1;
		}
	}

	public void reset(){
		if (moveable == 0)
			return;
		else
			move_to = 0;
	}
}
