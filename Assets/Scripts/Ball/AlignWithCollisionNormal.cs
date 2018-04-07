using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithCollisionNormal : MonoBehaviour {

	[SerializeField] Ball ball;

	void Update() {
		Vector2 normal = ball.state.ContactNormal;
		if (normal == Vector2.zero)
			return;
		RotateToNormal(normal);
	}

	void RotateToNormal (Vector2 normal) {
		float rotation = Vector2.Angle (Vector2.right, normal);
		if (Vector2.Dot (normal, Vector2.up) < 0)
			rotation *= -1;
		transform.rotation = Quaternion.Euler (0, 0, rotation - 90);
	}
}
