using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusStarter : MonoBehaviour {

	public PlayerInfoEvent OnAddPlayer;

	[SerializeField] PlayerInfoLimitedRuntimeSet playerRoster;
	[SerializeField] InputSchemeMonitor inputSchemeMonitor;

	void Start() {
		if (playerRoster.items.Count == 0)
			return;
		inputSchemeMonitor.enabled = false;
		foreach (PlayerInfo playerInfo in playerRoster.items) {
			OnAddPlayer.Invoke(playerInfo);
		}
	}
}
