using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerJoinedEvent : UnityEvent<InputScheme> { }

[System.Serializable]
public class OnPlayerLeftEvent : UnityEvent<InputScheme> { }

public class ActivePlayerManager : MonoBehaviour {

	public OnPlayerJoinedEvent onPlayerJoined;
	public OnPlayerLeftEvent onPlayerLeft;

	public static List<InputScheme> players;

	[SerializeField] float maxNumberOfPlayers;

	void Awake() {
		players = new List<InputScheme>();
	}

	public void AddPlayer(InputScheme player) {
		if (players.Count < maxNumberOfPlayers) {
			players.Add(player);
			onPlayerJoined.Invoke(player);
		}
	}

	public void RemovePlayer(GameObject player) {
		players.Remove(player.GetComponent<Ball>().inputScheme);
		onPlayerLeft.Invoke(player.GetComponent<Ball>().inputScheme);
	}
}
