using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusCamera : MonoBehaviour {

	[SerializeField] float maxMovementSpeed = 1;
	[SerializeField] Transform fallback;
	List<GameObject> registeredBalls;

	public void RegisterBall(GameObject ball) {
		registeredBalls.Add(ball);
	}

	public void DeregisterBall(GameObject ball) {
		registeredBalls.Remove(ball);
	}

	void Awake() {
		registeredBalls = new List<GameObject>();
	}

	void Update() {
		Vector3 centerPosition = FindCenterPosition();
		transform.position = SmoothMovement(transform.position, centerPosition, maxMovementSpeed);
	}

	Vector3 FindCenterPosition() {
		if (registeredBalls.Count == 0)
			return new Vector3 (fallback.position.x, fallback.position.y, transform.position.z);
		Vector3 centerPosition = Vector3.zero;
		foreach (GameObject ball in registeredBalls) {
			centerPosition += ball.transform.position;
		}
		centerPosition *= 1f / registeredBalls.Count;
		centerPosition.z = transform.position.z;
		return centerPosition;
	}
	
	Vector3 SmoothMovement(Vector3 position, Vector3 targetPosition, float maxMovementSpeed) {
		float maxMovement = maxMovementSpeed * Time.deltaTime;
		Vector3 movementVector = targetPosition - position;
		float movement = movementVector.magnitude;
		if (movement >= maxMovement)
			targetPosition = transform.position + movementVector * maxMovement;
		return targetPosition;	
	}
}
