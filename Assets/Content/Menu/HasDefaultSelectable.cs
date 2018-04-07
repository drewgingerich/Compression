using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HasDefaultSelectable : MonoBehaviour {

	public Selectable defaultSelectable;

	void OnEnable() {
		StartCoroutine(SelectDefaultRoutine());
	}

	IEnumerator SelectDefaultRoutine() {
		yield return new WaitForEndOfFrame();
		defaultSelectable.Select();
	}
}
