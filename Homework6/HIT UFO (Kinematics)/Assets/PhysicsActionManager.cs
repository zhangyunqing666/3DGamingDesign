﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsActionManager : SSActionManager, ISSActionCallback, IActionManager {
	public FirstSceneController sceneController;
	public List<PhysicsEmitAction> seq = new List<PhysicsEmitAction>();
	public UserClickAction userClickAction;
	public UFOFactory factory;

	protected void Start()
	{
		sceneController = (FirstSceneController)SSDirector.getInstance().current;
		sceneController.actionManager = this;
		factory = Singleton<UFOFactory>.Instance;
	}
	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
	{
		factory.Recycleufo(source.gameObject);
		seq.Remove(source as PhysicsEmitAction);
		source.destory = true;
		if (FirstSceneController.times >= 30)
			sceneController.flag = 1;
	}
	public void CheckEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
	{
	}
	public void Pause()
	{
		if (sceneController.flag == 0)
		{
			foreach (var k in seq)
			{
				k.speed = k.transform.GetComponent<Rigidbody>().velocity;
				k.transform.GetComponent<Rigidbody>().isKinematic = true;
			}
			sceneController.flag = 2;
		}
		else if (sceneController.flag == 2)
		{
			foreach (var k in seq)
			{
				k.transform.GetComponent<Rigidbody>().isKinematic = false;
				k.transform.GetComponent<Rigidbody>().velocity = k.speed;
			}
			sceneController.flag = 0;
		}
	}
	public void Play()
	{
		if (factory.used_ufos.Count > 0)
		{
			GameObject disk = factory.used_ufos[0];
			float x = Random.Range(-5, 5);
			disk.GetComponent<Rigidbody>().isKinematic = false;
			disk.GetComponent<Rigidbody>().velocity = new Vector3(x, 8 * (Mathf.CeilToInt(FirstSceneController.times / 10) + 1), 6);
			disk.GetComponent<Rigidbody>().AddForce(new Vector3(0,8.8f, 0),ForceMode.Force);
			PhysicsEmitAction physicsEmitAction = PhysicsEmitAction.GetSSAction();
			seq.Add(physicsEmitAction);
			this.RunAction(disk, physicsEmitAction, this);
			factory.used_ufos.RemoveAt(0);
		}
		if (Input.GetMouseButtonDown(0) && sceneController.flag == 0)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitGameObject;
			if (Physics.Raycast(ray, out hitGameObject))
			{
				GameObject gameObject = hitGameObject.collider.gameObject;
				Debug.Log(gameObject.tag);
				if (gameObject.tag == "ufo")
				{
					gameObject.transform.position=new Vector3(0,1,-10);
					if(gameObject.transform.GetComponent<Renderer>().material.color==Color.red)
						userClickAction = UserClickAction.GetSSAction(3);
					else if(gameObject.transform.GetComponent<Renderer>().material.color==Color.green)
						userClickAction = UserClickAction.GetSSAction(2);
					else if(gameObject.transform.GetComponent<Renderer>().material.color==Color.blue)
						userClickAction = UserClickAction.GetSSAction(1);					
					this.RunAction(gameObject, userClickAction, this);
				}
			}
		}
		base.Update();
	}
}