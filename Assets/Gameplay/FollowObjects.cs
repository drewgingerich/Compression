using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjects : MonoBehaviour {

	[SerializeField] float speed = 1;
	[SerializeField] GameObject fallbackObject;
	[SerializeField] List<GameObject> objectsToFollow;

	public void AddObject(GameObject gameObject) {
		objectsToFollow.Add(gameObject);
	}

	public void RemoveObject(GameObject gameObject) {
		objectsToFollow.Remove(gameObject);
	}

	void Awake() {
		objectsToFollow = new List<GameObject>();
	}
	
	void Update() {
		Vector3 centerPosition = FindCenterPosition();
		Vector3 smoothedPosition = SmoothMovement(transform.position, centerPosition, speed);
		transform.position = smoothedPosition;
	}

	Vector3 FindCenterPosition() {
		Vector3 centerPosition = Vector3.zero;
		foreach (GameObject objectToFollow in objectsToFollow) {
			if (objectToFollow != null)
				centerPosition += objectToFollow.transform.position;
		}
		if (centerPosition == Vector3.zero) {
			Vector3 fallbackPosition = fallbackObject.transform.position;
			float currentDepth = transform.position.z;
			return new Vector3(fallbackPosition.x, fallbackPosition.y, currentDepth);
		} else {
			centerPosition *= 1f / objectsToFollow.Count;
			centerPosition.z = transform.position.z;
			return centerPosition;
		}
	}

	Vector3 SmoothMovement(Vector3 position, Vector3 targetPosition, float maxMovementSpeed) {
		float maxMovement = maxMovementSpeed * Time.deltaTime;
		Vector3 movementVector = targetPosition - position;
		float movement = movementVector.magnitude;
		if (movement > maxMovement)
			targetPosition = transform.position + movementVector * maxMovement;
		return targetPosition;
	}
}
