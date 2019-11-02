using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAction : SSAction {
	// 移动速度和人物的transform
	private float speed;
	private Transform target;
	private Animator ani;

	public static RunAction GetRunAction(Transform target,float speed,Animator ani) {
		RunAction currentAction = ScriptableObject.CreateInstance<RunAction>();
		currentAction.speed = speed;
		currentAction.target = target;
		currentAction.ani = ani;
		return currentAction;
	}

	public override void Start() {
		// 进入跑步状态
		ani.SetFloat("Speed", 1);
	}

	public override void Update() {
		Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
		// 进行转向，转向目标方向
		if (transform.rotation != rotation) transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed * 5);

		this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
		if (Vector3.Distance(this.transform.position, target.position) < 0.5) {
			this.destory = true;
			this.callback.SSEventAction(this);
		}
	}
}