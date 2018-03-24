using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerLeaveEvent : UnityEvent<GameObject> {}

public class ExitZone : MonoBehaviour {

	public OnPlayerLeaveEvent onPlayerLeave;
	void OnTriggerEnter2D(Collider2D other) {
		onPlayerLeave.Invoke(other.gameObject);
		Destroy(other.gameObject);
	}
}
