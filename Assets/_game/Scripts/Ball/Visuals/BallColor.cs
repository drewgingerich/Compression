using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColor : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] SpriteRenderer ballSprite;

	void Start() {
		ball.playerInfo.colorScheme.OnChange += ChangeColor;
	}

	void ChangeColor(PlayerColorScheme playerColor) {
		ballSprite.color = playerColor.ballColor;
	}
}
