using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions {

	public static Vector2 Rotate(this Vector2 v, float degrees) {
		float radians = degrees * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);

		float tx = v.x;
		float ty = v.y;

		return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
	}

	public static Vector2 ClampRotation(this Vector2 vector, Vector2 reference, float maxAngle) {
		float angle = Vector2.SignedAngle(reference, vector);
		if (Mathf.Abs(angle) <= maxAngle)
			return vector;
		float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
		return reference.Rotate(clampedAngle - angle);
	}
}
