using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerEvent : UnityEvent<PlayerInfo> { }

public class PlayerManager : MonoBehaviour {

	[SerializeField] int maxNumberOfPlayers;

	public InputSchemeEvent OnInputSchemeTaken;
	public InputSchemeEvent OnInputSchemeFreed;
	public PlayerEvent OnAddPlayer;
	public PlayerEvent OnRemovePlayer;	
	public UnityEvent OnAllPlayersGone;

	public static PlayerInfo[] players;

	void Awake() {
		players = new PlayerInfo[maxNumberOfPlayers];
	}

	public void AddPlayer(InputScheme inputScheme) {
		for (int i = 0; i < players.Length; i++) {
			if (players[i] == null) {
				PlayerInfo playerInfo = new PlayerInfo();
				playerInfo.inputScheme = inputScheme;
				players[i] = playerInfo;
				OnInputSchemeTaken.Invoke(inputScheme);
				OnAddPlayer.Invoke(playerInfo);
				return;
			} else {
				OnInputSchemeFreed.Invoke(inputScheme);
			}
		}
	}

	public void RemovePlayer(PlayerInfo playerInfo) {
		for (int i = 0; i < players.Length; i++) {
			if (playerInfo == players[i])
				players[i] = null;
		}
		OnInputSchemeFreed.Invoke(playerInfo.inputScheme);
		OnRemovePlayer.Invoke(playerInfo);
		foreach (PlayerInfo player in players) {
			if (player != null)
				return;
		}
		OnAllPlayersGone.Invoke();
	}
	
	public static int GetNumberOfPlayers() {
		int numberOfPlayers = 0;
		foreach (PlayerInfo player in players) {
			if (player != null)
				numberOfPlayers++;
		}
		return numberOfPlayers;
	}
}
