// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ColorPool : MonoBehaviour {

// 	[SerializeField] PlayerColorProvider colorProvider;
// 	SpriteRenderer spriteRenderer;
// 	PlayerColorScheme color;
// 	Collider2D swimmer;
// 	bool active = true;

// 	void Start() {
// 		spriteRenderer = GetComponent<SpriteRenderer>();
// 	}

// 	void OnTriggerEnter2D (Collider2D other) {
// 		if (swimmer != null)
// 			return;
// 		ColorPlayer(other);
// 		Deactivate();
// 	}

// 	void OnTriggerExit2D(Collider2D other) {
// 		if (other == swimmer)
// 			FindNewColor();
// 	}

// 	void FindNewColor() {
// 		color = colorProvider.GetRandomColor();
// 		spriteRenderer.color = color.ballColor;
// 		active = true;
// 	}
	
// 	void Deactivate() {
// 		spriteRenderer.color = Color.black;
// 		active = false;
// 	}

// 	void ColorPlayer(Collider2D other) {
// 		swimmer = other;
// 		Ball ball = swimmer.GetComponent<Ball>();
// 		ball.playerInfo.colorScheme.Value = color;
// 	}
// }
