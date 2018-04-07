using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Color Provider", menuName = "Color Provider")]
public class PlayerColorProvider : ScriptableObject {

	[SerializeField] List<PlayerColor> colors;

	public PlayerColor GetRandomColor() {
		return colors[Random.Range(0, colors.Count)];
	}
}
