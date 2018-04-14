﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour {

	public void RotateToNormal (Vector2 newUp) {
		float rotation = Vector2.Angle (Vector2.right, newUp);
		if (Vector2.Dot (newUp, Vector2.up) < 0)
			rotation *= -1;
		transform.rotation = Quaternion.Euler (0, 0, rotation - 90);
	}
}
