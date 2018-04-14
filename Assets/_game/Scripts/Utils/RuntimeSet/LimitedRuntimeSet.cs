using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedRuntimeSet<T> : RuntimeSet<T> {

	public event System.Action OnVacancy = delegate { };
	public event System.Action OnNoVacancy = delegate { };

	[SerializeField] int maxSlots;

	public int FreeSlots() {
		return maxSlots - items.Count;
	}

	public override void RegisterItem(T item) {
		Debug.Assert(FreeSlots() >= 0);
		base.RegisterItem(item);
		if (FreeSlots() == 0)
			OnNoVacancy();
	}

	public override void UnregisterItem(T item) {
		base.UnregisterItem(item);
		if (FreeSlots() > 0)
			OnVacancy();
	}
}