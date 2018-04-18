using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Customization/PlayerInfo", fileName = "New PlayerInfo")]
public class PlayerInfo : ScriptableObject {

	public InputSchemeEventValue inputScheme;
	public IntEventValue id;
	public PlayerColorSchemeEventValue colorScheme;
	
	public void LoadFromReference(PlayerInfo playerInfo) {
		inputScheme.Value = playerInfo.inputScheme.Value;
		id.Value = playerInfo.id.Value;
		colorScheme.Value = playerInfo.colorScheme.Value;
	}
}
