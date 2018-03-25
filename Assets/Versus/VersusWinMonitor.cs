using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VersusWinMonitor : MonoBehaviour {

	public UnityEvent OnGameOver;

	List<GameObject> players;

	void Awake() {
		players = new List<GameObject>();
	}

	public void RegisterPlayer(GameObject player) {
		players.Add(player);
		player.GetComponent<Ball>().OnDie += DeregisterPlayer;
	}

	public void DeregisterPlayer(GameObject player) {
		players.Remove(player);
		if (players.Count == 1)
			OnGameOver.Invoke();
	}
}
