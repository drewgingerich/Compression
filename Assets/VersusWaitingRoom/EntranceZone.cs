using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EntranceZone : MonoBehaviour {

	public UnityEvent OnAllPlayersReady;

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
		int numberOfPlayers = PlayerManager.GetNumberOfPlayers();
		if (numberOfPlayers < 2)
			return;
		if (playersReady.Count != numberOfPlayers)
			return;
		OnAllPlayersReady.Invoke();
	}
}
