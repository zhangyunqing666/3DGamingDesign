using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAction : SSAction {

	// 移动速度和目标地点
	private float speed;
	private Vector3 target;
	private Animator ani;

	public static WalkAction GetWalkAction(Vector3 target,float speed,Animator ani) {
		WalkAction currentAction = ScriptableObject.CreateInstance<WalkAction>();
		currentAction.speed = speed;
		currentAction.target = target;
		currentAction.ani = ani;
		return currentAction;
	}

	public override void Start() {
		ani.SetFloat("Speed", 0.5f);
	}

	public override void Update() {
		Quaternion rotation = Quaternion.LookRotation(target - transform.position);
		// 进行转向，转向目标方向
		if (transform.rotation != rotation) transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed * 5);
		//沿着直线方向走到目标位置
		this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
		if (this.transform.position == target) {
			this.destory = true;
			this.callback.SSEventAction(this);
		}
	}
}
