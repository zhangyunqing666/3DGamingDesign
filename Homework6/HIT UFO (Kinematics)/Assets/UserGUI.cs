using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

	private FirstSceneController action;
	private GUIStyle fontstyle1 = new GUIStyle();
	// Use this for initialization
	void Start () {
		action = SSDirector.getInstance().current as FirstSceneController;
		fontstyle1.fontSize = 50;
	}

	// Update is called once per frame
	private void OnGUI()
	{
		if (GUI.Button(new Rect(0, 80, 80, 60), "RESTART"))
		{
			action.Restart();
		}
		if (GUI.Button(new Rect(0, 160, 80, 60), "Pause"))
		{
			action.Pause();
		}
		if (action.flag == 0)
		{
			
			fontstyle1.normal.textColor = Color.yellow;
			fontstyle1.fontSize = 32;
			GUI.Label(new Rect(0 , 0, 300, 100), "Score: " +
				action.score + ", Round: " + (Mathf.CeilToInt(FirstSceneController.times / 10) + 1), fontstyle1);
			GUI.Label(new Rect(0 , 30, 320, 100), "Left ufos:"+(10-Mathf.CeilToInt(FirstSceneController.times)%10), fontstyle1);
		}
		else if (action.flag == 1)
		{
			fontstyle1.normal.textColor = Color.red;
			GUI.Label(new Rect(Screen.width / 2 - 150, 0, 300, 100), "Your score is : " + action.score, fontstyle1);
		}
		else
		{
			fontstyle1.normal.textColor = Color.yellow;
			GUI.Label(new Rect(0, 0, 300, 100), "Score: " +
				action.score + ", Round: " + (Mathf.CeilToInt(FirstSceneController.times )%11 + 1), fontstyle1);
			fontstyle1.normal.textColor = Color.red;
			GUI.Label(new Rect(0,30, 320, 100), "Pause!", fontstyle1);
		}
	}
}