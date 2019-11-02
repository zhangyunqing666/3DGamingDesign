using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Publisher{
	private delegate void ActionUpdate(ActorState state, int pos, GameObject Actor);
	private ActionUpdate updatelist;

	private static Publisher _instance;
	public static Publisher getInstance() {
		if (_instance == null) _instance = new Publisher();
		return _instance;
	}

	public void notify(ActorState state, int pos, GameObject actor) {
		if (updatelist != null) updatelist(state, pos, actor);
	}

	public void add(Observer observer) {
		updatelist += observer.notified;
	}

	public void delete(Observer observer) {
		updatelist -= observer.notified;
	}
}
