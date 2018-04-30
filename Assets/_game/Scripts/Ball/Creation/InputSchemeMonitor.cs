using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSchemeMonitor : MonoBehaviour {

	public PlayerInfoEvent OnDetectNewPlayer;

	[SerializeField] bool startOnAwake;

	[SerializeField] InputSchemeRuntimeSet availableInputSchemes;
	[SerializeField] InputSchemeRuntimeSet takenInputSchemes;

	[SerializeField] PlayerInfo defaultPlayerInfo;
	[SerializeField] PlayerInfoLimitedRuntimeSet playerRoster;

	void Awake() {
		if (startOnAwake)
			StartMonitor();
	}

	void Update () {
		for (int i = availableInputSchemes.items.Count - 1; i >= 0; i--) {
			if (playerRoster.FreeSlots() == 0) {
				PauseMonitor();
				break;
			}
			InputScheme inputScheme = availableInputSchemes.items[i];
			if (inputScheme.GetInputDirection() != Vector2.zero) {
				availableInputSchemes.UnregisterItem(inputScheme);
				takenInputSchemes.RegisterItem(inputScheme);
				PlayerInfo playerInfo = Instantiate(defaultPlayerInfo);
				playerInfo.inputScheme.Value = inputScheme;
				playerRoster.RegisterItem(playerInfo);
				OnDetectNewPlayer.Invoke(playerInfo);
			}
		}
	}

	public void StartMonitor() {
		playerRoster.OnNoVacancy += PauseMonitor;
		enabled = true;
	}

	public void PauseMonitor() {
		StopMonitor();
		playerRoster.OnVacancy += StartMonitor;
	}

	public void StopMonitor() {
		playerRoster.OnNoVacancy -= PauseMonitor;
		enabled = false;
	}
}
