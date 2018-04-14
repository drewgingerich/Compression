using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeSetMember<T> : MonoBehaviour {

	[SerializeField] protected T item;
	[SerializeField] protected RuntimeSet<T> set;

	void OnEnable() {
		set.RegisterItem(item);
	}

	void OnDisable() {
		set.UnregisterItem(item);
	}
}
