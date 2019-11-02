using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActorState { ENTER_AREA, DEATH }
public interface Observer {
    void notified(ActorState state, int pos, GameObject actor);
}

