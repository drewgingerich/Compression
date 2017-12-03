using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectManager : MonoBehaviour {

	public event System.Action<List<Player>> OnStart;

	[SerializeField] GameObject playerManagerPrefab;

	ControlSchemeDetector controlSchemeDetector;
	List<PlayerManager> playerManagers;

	void Awake () {
		playerManagers = new List<PlayerManager> ();
		controlSchemeDetector = new ControlSchemeDetector ();
		controlSchemeDetector.OnDetect += AddPlayer;
	}

	void Update () {
		foreach (PlayerManager manager in playerManagers)
			manager.CheckPlayerStatus ();
	}

	void AddPlayer (ControlScheme controlScheme) {
		if (playerManagers.Count >= 4)
			return;
		Player newPlayer = new Player (controlScheme);
		PlayerManager newManager = Instantiate (playerManagerPrefab).GetComponent<PlayerManager> ();
		newManager.LoadPlayer (newPlayer);
		newManager.OnPlayerLeave += RemovePlayer;
		newManager.OnRequestStart += RequestStart;
		playerManagers.Add (newManager);
	}

	void RemovePlayer (PlayerManager manager) {
		playerManagers.Remove (manager);
		Destroy (manager.gameObject);
	}

	void RequestStart () {
		foreach (PlayerManager manager in playerManagers) {
			if (!manager.playerReady)
				return;
		}
		List<Player> players = new List<Player> ();
		foreach (PlayerManager manager in playerManagers) {
			players.Add (manager.player);
		}
		if (OnStart != null)
			OnStart (players);
	}
}
