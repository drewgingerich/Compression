using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EntranceZone : MonoBehaviour {

	public UnityEvent OnAllPlayersReady;

	[SerializeField] PlayerInfoLimitedRuntimeSet playerRoster;

	List<GameObject> playersReady;

	void Awake() {
		playersReady = new List<GameObject>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Ball>() != null) {
			playersReady.Add(other.gameObject);
			CheckIfAllPlayersAreReady();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<Ball>() != null)
			playersReady.Remove(other.gameObject);
	}

	void CheckIfAllPlayersAreReady() {
		if (playerRoster.items.Count < 2)
			return;
		if (playersReady.Count != playerRoster.items.Count)
			return;
		OnAllPlayersReady.Invoke();
	}
}
