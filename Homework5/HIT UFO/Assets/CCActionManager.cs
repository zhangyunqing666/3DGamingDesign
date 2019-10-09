using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {
	public FirstSceneController sceneController;
	public List<CCMoveToAction> seq = new List<CCMoveToAction>();
	public UserClickAction userClickAction;
	public UFOFactory factory;

	protected new void Start()
	{
		sceneController = (FirstSceneController)SSDirector.getInstance().current;
		sceneController.actionManager = this;
		factory = Singleton<UFOFactory>.Instance;
	}
	protected new void Update()
	{
		if(factory.used_ufos.Count > 0)
		{
			GameObject disk = factory.used_ufos[0].gameObject;
			float x = Random.Range(-10, 10);
			CCMoveToAction moveToAction = CCMoveToAction.GetSSAction(new Vector3(x, 12, 0), (Mathf.CeilToInt(FirstSceneController.times / 10) + 1) * Time.deltaTime);
			if (disk.transform.GetComponent<Renderer>().material.color == Color.red)
				moveToAction = CCMoveToAction.GetSSAction(new Vector3(x, 12, 0), 5 * (Mathf.CeilToInt(FirstSceneController.times / 10) + 1) * Time.deltaTime);
			else if(disk.transform.GetComponent<Renderer>().material.color == Color.green)
				moveToAction = CCMoveToAction.GetSSAction(new Vector3(x, 12, 0), 4 * (Mathf.CeilToInt(FirstSceneController.times / 10) + 1) * Time.deltaTime);
			else if(disk.transform.GetComponent<Renderer>().material.color == Color.blue)
				moveToAction = CCMoveToAction.GetSSAction(new Vector3(x, 12, 0), 3 * (Mathf.CeilToInt(FirstSceneController.times / 10) + 1) * Time.deltaTime);
			
			seq.Add(moveToAction);
			this.RunAction(disk, moveToAction, this);
			factory.used_ufos.RemoveAt(0);
		}
		if (Input.GetMouseButtonDown(0) && sceneController.flag == 0)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitGameObject;
			if (Physics.Raycast(ray, out hitGameObject))
			{
				GameObject gameObject = hitGameObject.collider.gameObject;
				if (gameObject.tag == "ufo")
				{
					foreach(var k in seq)
					{
						if (k.gameObject == gameObject)
							k.transform.position = k.target;
					}
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
	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
	{
		factory.Recycleufo(source.gameObject);
		seq.Remove(source as CCMoveToAction);
		source.destory = true;
		if (FirstSceneController.times >= 30)
			sceneController.flag = 1;
		
	}
	public void CheckEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, Object objParam = null)
	{
	}
	public void Pause()
	{
		if(sceneController.flag == 0)
		{
			foreach (var k in seq)
			{
				k.enable = false;
			}
			sceneController.flag = 2;
		}
		else if(sceneController.flag == 2)
		{
			foreach (var k in seq)
			{
				k.enable = true;
			}
			sceneController.flag = 0;
		}
	}
}