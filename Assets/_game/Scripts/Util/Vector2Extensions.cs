using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions {

	public static Vector2 Clamp(this Vector2 vector, Vector2 reference, float maxAngle) {
		float angle = Vector2.SignedAngle(reference, vector);
		if (Mathf.Abs(angle) <= maxAngle)
			return vector;
		float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
		Quaternion rotation = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
		return rotation * reference;
	}
}
