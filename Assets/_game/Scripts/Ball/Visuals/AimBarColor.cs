using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBarColor : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] LineRenderer lineSprite;

	void Start() {
		ball.playerInfo.colorScheme.OnChange += ChangeColor;
	}

	void ChangeColor(PlayerColorScheme playerColor) {
		lineSprite.startColor = playerColor.aimBarColor;
		lineSprite.endColor = playerColor.aimBarColor;
	}
}
