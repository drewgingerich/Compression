using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EntranceZone : MonoBehaviour {

	public UnityEvent onAllPlayersReady;

	List<GameObject> registeredPlayers;
	List<GameObject> playersReady;

	void Awake() {
		registeredPlayers = new List<GameObject>();
		playersReady = new List<GameObject>();
	}

	public void RegisterPlayer(GameObject player) {
		registeredPlayers.Add(player);
	}

	public void DergisterPlayer(GameObject player) {
		registeredPlayers.Remove(player);
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
		if (playersReady.Count < 2)
			return;
		if (playersReady.Count == registeredPlayers.Count) {
			PlayerManager.players = (from go in playersReady select go.GetComponent<Ball>().inputScheme) as List<InputScheme>;
			onAllPlayersReady.Invoke();
		}
	}
}
