using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeSetMember<T1, T2> : MonoBehaviour where T2 : RuntimeSet<T1> {

	[SerializeField] protected T1 item;
	[SerializeField] protected T2 runtimeSet;

	void OnEnable() {
		runtimeSet.RegisterItem(item);
	}

	void OnDisable() {
		runtimeSet.UnregisterItem(item);
	}
}
