using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusStarter : MonoBehaviour {

	public PlayerEvent OnActiveInputScheme;

	void Start() {
		foreach (PlayerInfo player in PlayerManager.players) {
			if (player != null)
				OnActiveInputScheme.Invoke(player);
		}
	}
}
