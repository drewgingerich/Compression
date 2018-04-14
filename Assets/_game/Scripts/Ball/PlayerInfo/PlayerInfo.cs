using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Customization/PlayerInfo", fileName = "New PlayerInfo")]
public class PlayerInfo : ScriptableObject {

	public EventValue<InputScheme> inputScheme;
	public EventValue<int> id;
	public EventValue<PlayerColorScheme> colorScheme;
}
