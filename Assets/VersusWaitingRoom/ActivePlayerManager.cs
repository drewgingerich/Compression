using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerJoinedEvent : UnityEvent<InputScheme> { }

public class ActivePlayerManager : MonoBehaviour {

	public OnPlayerJoinedEvent onPlayerJoined;

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
}
