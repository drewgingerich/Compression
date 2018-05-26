using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BallActions {

	public static void LaunchBall(BallState state, Rigidbody2D rb2d, Vector2 launchDirection) {
		launchDirection = launchDirection.SnapRotation(32, Vector2.right);
		float launchDistance = 2f;
		float launchForce = Mathf.Sqrt(2 * Physics2D.gravity.magnitude * launchDistance);
		Vector2 launchVector = launchDirection * launchForce;
		rb2d.AddForce(launchVector, ForceMode2D.Impulse);

		// float maxDistance = 2.6f;
		// float minDistance = 1.2f;
		// float boostDistance = 0.7f;
		// float limitDistance = 3.5f;
		// float distance = Mathf.Pow(state.impactMagnitude.Value, 2) / (Physics2D.gravity.magnitude * 2);
		// if (distance == 0)
		// 	distance = minDistance;
		// else if (distance < maxDistance - boostDistance)
		// 	distance += boostDistance;
		// else if (distance < maxDistance)
		// 	distance = maxDistance;
		// else if (distance > limitDistance)
		// 	distance = limitDistance + (distance - limitDistance) * 0.5f;
		// // distance += 1.25f;
		// float launchForce = Mathf.Sqrt(2 * Physics2D.gravity.magnitude * distance);
		// Vector2 launchVector = launchDirection * launchForce;
		// Debug.Log(state.impactMagnitude.Value.ToString() + " " + launchVector.magnitude.ToString());
		// rb2d.AddForce(launchVector, ForceMode2D.Impulse);
		// rb2d.velocity = launchVector;
	}

}
