using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tem.Action;
public interface ISSActionCallback {
	void SSEventAction(SSAction source, SSActionEventType events = SSActionEventType.COMPLETED,
		int intParam = 0, string strParam = null, Object objParam = null);
}
