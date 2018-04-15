using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSchemeMonitor : MonoBehaviour {

	[SerializeField] InputSchemeRuntimeSet availableInputSchemes;
	[SerializeField] PlayerInfo defaultPlayerInfo;
	[SerializeField] PlayerInfoLimitedRuntimeSet playerRoster;
	
	bool active = true;

	void Update () {
		if (!active || playerRoster.FreeSlots() == 0)
			return;
		for (int i = availableInputSchemes.items.Count - 1; i >= 0; i--) {
			InputScheme inputScheme = availableInputSchemes.items[i];
			if (inputScheme.GetInputDirection() == Vector2.zero)
				continue;
			PlayerInfo newPlayerInfo = Instantiate(defaultPlayerInfo);
			newPlayerInfo.inputScheme.Value = inputScheme;
			playerRoster.RegisterItem(newPlayerInfo);
			availableInputSchemes.UnregisterItem(inputScheme);
			if (!active)
				return;
		}
	}

	void OnEnable() {
		playerRoster.OnVacancy += StartMonitor;
		playerRoster.OnNoVacancy += StopMonitor;
		playerRoster.OnUnregisterItem += MonitorInputScheme;
	}

	void OnDisable() {
		playerRoster.OnVacancy -= StartMonitor;
		playerRoster.OnNoVacancy -= StopMonitor;
	}
	
	void MonitorInputScheme(PlayerInfo playerInfo) {
		if (!availableInputSchemes.items.Contains(playerInfo.inputScheme.Value))
			availableInputSchemes.RegisterItem(playerInfo.inputScheme.Value);
	}

	void StopMonitor() {
		gameObject.SetActive(false);
		active = false;
	}

	void StartMonitor() {
		gameObject.SetActive(true);
		active = true;
	}
}
