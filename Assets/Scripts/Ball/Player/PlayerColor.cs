using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Color", menuName = "Player Color")]
public class PlayerColor : ScriptableObject {

	public bool available;
	public Color mainColor;
	public Color secondaryColor;
}
