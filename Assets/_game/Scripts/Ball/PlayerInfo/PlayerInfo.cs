using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Customization/PlayerInfo", fileName = "New PlayerInfo")]
public class PlayerInfo : ScriptableObject {

	public InputSchemeEventValue inputScheme;
	public IntEventValue id;
	public PlayerColorSchemeEventValue colorScheme;
}
