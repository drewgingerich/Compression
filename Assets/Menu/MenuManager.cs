using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public event System.Action OnStart;

	void Update () {
		if (Input.GetButtonDown ("Submit")) {
			if (OnStart != null)
				OnStart ();
		}
	}
}
