using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserClickAction : SSAction {
	public int add_score = 0;
	public static UserClickAction GetSSAction(int s)
	{
		UserClickAction action = CreateInstance<UserClickAction>();
		action.add_score = s;
		return action;
	}
	public override void Start(){
	}	

	public override void Update()
	{
		if(enable)
		{
			FirstSceneController sc = SSDirector.getInstance().current as FirstSceneController;
			sc.score = sc.score + Mathf.CeilToInt(FirstSceneController.times/10) + add_score*Mathf.FloorToInt(120 / (transform.rotation.x + 30));
			destory = true;
		}
	}
}