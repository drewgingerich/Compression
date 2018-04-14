using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerColorScheme", menuName = "Player Customization/PlayerColorScheme")]
public class PlayerColorScheme : ScriptableObject {

	public bool available;
	public Color ballColor;
	public Color aimBarColor;
}
