using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {
	private Dictionary<int, SSAction> dictionary = new Dictionary<int, SSAction>();
	private List<SSAction> waitingAddAction = new List<SSAction>();
	private List<int> waitingDelete = new List<int>();

	protected void Start() {

	}

	protected void Update() {
		foreach (SSAction ac in waitingAddAction) dictionary[ac.GetInstanceID()] = ac;
		waitingAddAction.Clear();

		foreach (KeyValuePair<int,SSAction> dic in dictionary) {
			SSAction ac = dic.Value;
			if (ac.destory) waitingDelete.Add(ac.GetInstanceID());
			else if (ac.enable) ac.Update();
		}

		foreach (int id in waitingDelete) {
			SSAction ac = dictionary[id];
			dictionary.Remove(id);
			DestroyObject(ac);
		}
			
		waitingDelete.Clear();
	}

	public void runAction(GameObject gameObject,SSAction action,ISSActionCallback callback) {
		action.gameObject = gameObject;
		action.transform = gameObject.transform;
		action.callback = callback;
		waitingAddAction.Add(action);
		action.Start();
	}
}
