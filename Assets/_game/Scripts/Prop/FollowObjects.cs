using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjects : MonoBehaviour {

	[SerializeField] TransformRuntimeSet objectTransformSet;
	[SerializeField] Transform fallbackTransform;
	[SerializeField] Vector2 fallbackPosition;
	[SerializeField] float convergenceTime = 0.5f;

	void Update() {
		Vector3 centerPosition = FindCenterPosition(objectTransformSet.items);
		Vector3 smoothedPosition = SmoothMovement(transform.position, centerPosition, convergenceTime);
		transform.position = smoothedPosition;
	}

	Vector3 FindCenterPosition(List<Transform> objectTransforms) {
		Vector3 centerPosition;
		if (objectTransforms.Count == 0) {
			if (fallbackTransform != null)
				centerPosition = fallbackTransform.position;
			else
				centerPosition = new Vector3(fallbackPosition.x, fallbackPosition.y, 0);
		} else {
			centerPosition = Vector3.zero;
			foreach (Transform objTransform in objectTransforms) {
				centerPosition += objTransform.position;
			}
			centerPosition *= 1f / objectTransforms.Count;
		}
		centerPosition.z = transform.position.z;
		return centerPosition;
	}

	Vector3 SmoothMovement(Vector3 position, Vector3 targetPosition, float convergenceTime) {
		Vector3 change = targetPosition - position;
		Vector3 velocity = change / convergenceTime;
		return position + velocity * Time.deltaTime;
	}
}
