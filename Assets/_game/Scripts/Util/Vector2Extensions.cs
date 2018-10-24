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

	public static Vector2 Perpendicular(this Vector2 v) {
		return Rotate(v, 90);
	}

	public static Vector2 ClampRotation(this Vector2 vector, Vector2 reference, float maxAngle) {
		float angle = Vector2.SignedAngle(reference, vector);
		if (Mathf.Abs(angle) <= maxAngle)
			return vector;
		float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
		return reference.Rotate(clampedAngle);
	}

	public static Vector2 SnapRotation(this Vector2 v, float numPoints, Vector2 referenceDirection) {
		float spacing = 360 / numPoints;
		float angle = Vector2.SignedAngle(referenceDirection, v);
		angle += 180;
		float shift = angle % spacing;
		if (shift <= spacing * 0.5f)
			shift = -shift;
		else
			shift = spacing - shift;
		return v.Rotate(shift);
	}

	public static Vector2 SnapRotation(this Vector2 v, float numPoints) {
		return v.SnapRotation(numPoints, Vector2.right);
	}

	public static Vector2 SnapRotationToAngles(this Vector2 v, List<float> angles, Vector2 referenceDirection) {
		float angleFromReference = Vector2.SignedAngle(referenceDirection, v);
		float selectedAngle = angles[0];
		float lowestDistance = 180;
		foreach (float angle in angles) {
			float difference = Mathf.Abs(angle - angleFromReference);
			if (difference < lowestDistance) {
				selectedAngle = angle;
				lowestDistance = difference;
			}
		}
		return referenceDirection.Rotate(selectedAngle) * v.magnitude;
	}

	public static Vector2 SnapRotationToAngles(this Vector2 v, List<float> angles) {
		return v.SnapRotationToAngles(angles, Vector2.up);
	}
}
