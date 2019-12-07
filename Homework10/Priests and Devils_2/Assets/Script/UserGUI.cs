using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDir;
public class UserGUI : MonoBehaviour {
	private UserAction action;
	public int status = 0;
	GUIStyle style;
	GUIStyle button;
	// Use this for initialization
	void Start () {
		Debug.Log("here2");
		action = Director.getInstace ().current as UserAction;
		style = new GUIStyle ();
		style.alignment = TextAnchor.MiddleCenter;
		button = new GUIStyle ("button");
		button.fontSize = 30;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		if (status == 1) {
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "GameOver!", style);
			if (GUI.Button (new Rect (Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", button)) {
				status = 0;
				Debug.Log("here1");
				action.restart ();
			}
		} else if (status == 2) {
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You Win", style);
			if (GUI.Button (new Rect (Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", button)) {
				status = 0;
				action.restart ();
			}

		}
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 +100, 170, 30), "Tips:NextAction"))
        {
            action.NextActionAI();
        }
    }
}
