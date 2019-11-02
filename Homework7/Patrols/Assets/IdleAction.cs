using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : SSAction {
	// 站立持续时间
	private float time;
	private Animator ani;

	public static IdleAction GetIdleAction(float time,Animator ani) {
		IdleAction currentAction = ScriptableObject.CreateInstance<IdleAction>();
		currentAction.time = time;
		currentAction.ani = ani;
		return currentAction;
	}

	public override void Start() {
		// 进入站立状态
		ani.SetFloat("Speed", 0);
	}

	public override void Update() {
		// 永久站立
		if (time == -1) return;
		// 减去站立时间
		time -= Time.deltaTime;
		if (time < 0) {
			this.destory = true;
			this.callback.SSEventAction(this);
		}
	}
}