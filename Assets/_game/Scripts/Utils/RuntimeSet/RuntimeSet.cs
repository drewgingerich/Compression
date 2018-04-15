using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeSet<T> : ScriptableObject {

	public event System.Action<T> OnRegisterItem = delegate { };
	public event System.Action<T> OnUnregisterItem = delegate { };

	[HideInInspector] public List<T> items;
	[SerializeField] List<T> initialItems;

	public virtual void RegisterItem(T item) {
		items.Add(item);
		OnRegisterItem(item);
	}

	public virtual void UnregisterItem(T item) {
		items.Remove(item);
		OnUnregisterItem(item);
	}

	void OnEnable() {
		items = new List<T>(initialItems);
		hideFlags = HideFlags.DontUnloadUnusedAsset;
	}

	void OnDisable() {
		items = null;
	}
}