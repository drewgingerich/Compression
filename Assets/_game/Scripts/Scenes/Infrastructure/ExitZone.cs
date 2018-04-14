using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitZone : MonoBehaviour {

	public UnityEvent OnAllPlayersLeft;

	[SerializeField] PlayerInfoLimitedRuntimeSet playerRoster;

	void OnTriggerEnter2D(Collider2D other) {
		Destroy(other.gameObject);
		if (playerRoster.items.Count == 0)
			OnAllPlayersLeft.Invoke();
	}
}
