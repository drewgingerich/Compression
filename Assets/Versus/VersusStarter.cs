using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusStarter : MonoBehaviour {

	public OnPlayerJoinedEvent onPlayerJoined;

	void Start() {
		foreach (InputScheme input in PlayerManager.players)
			onPlayerJoined.Invoke(input);
	}
}
