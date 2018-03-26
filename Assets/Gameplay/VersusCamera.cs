using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusCamera : MonoBehaviour {

	[SerializeField] float maxMovementSpeed = 1;
	[SerializeField] float maxSizeSpeed = 1;
	[SerializeField] float baseOrthographicSize = 6;
	[SerializeField] float buffer = 0.5f;
	[SerializeField] Transform fallback;

	new Camera camera;
	List<GameObject> registeredBalls;
	float aspectRatio;

	public void RegisterBall(GameObject ball) {
		registeredBalls.Add(ball);
	}

	public void DeregisterBall(GameObject ball) {
		registeredBalls.Remove(ball);
	}

	void Awake() {
		registeredBalls = new List<GameObject>();
		camera = GetComponent<Camera>();
		aspectRatio = (float)Screen.width / Screen.height;
	}

	void Update() {
		if (registeredBalls.Count == 0)
			return;
		int i = 0;
		while (true) {
			if (registeredBalls[i] == null)
				registeredBalls.RemoveAt(i);
			else
				i++;
			if (i < registeredBalls.Count)
				break;
		}
		Vector3 centerPosition = FindCenterPosition();
		Vector3 smoothedPosition = SmoothMovement(transform.position, centerPosition, maxMovementSpeed);
		transform.position = smoothedPosition;
		float orthographicSize = FindOrthographicSize(smoothedPosition, aspectRatio);
		float smoothedOrthographicSize = SmoothSizeChange(camera.orthographicSize, orthographicSize, maxSizeSpeed);
		camera.orthographicSize = smoothedOrthographicSize;
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
		if (movement > maxMovement)
			targetPosition = transform.position + movementVector * maxMovement;
		return targetPosition;	
	}
	
	float FindOrthographicSize(Vector3 centerPosition, float aspectRatio) {
		Vector2 cornerVector = Vector2.zero;
		foreach (GameObject ball in registeredBalls) {
			float verticalDistance = Mathf.Abs(centerPosition.y - ball.transform.position.y);
			if (verticalDistance > cornerVector.y) {
				cornerVector.y = verticalDistance;
			}
			float horizontalDistance = Mathf.Abs(centerPosition.x - ball.transform.position.x);
			float scaleHorizontalDistance = horizontalDistance / aspectRatio;
			if (scaleHorizontalDistance > cornerVector.x)
				cornerVector.x = scaleHorizontalDistance;
		}
		cornerVector += new Vector2(buffer, buffer);
		float verticalSize = Mathf.Max(cornerVector.y, baseOrthographicSize);
		float horizontalSize = Mathf.Max(cornerVector.x, baseOrthographicSize);
		return Mathf.Max(verticalSize, horizontalSize);
	}

	float SmoothSizeChange(float size, float targetSize, float maxSizeSpeed) {
		float maxMovement = maxSizeSpeed * Time.deltaTime;
		float movement = targetSize - size;
		if (movement > maxMovement)
			targetSize = size + maxMovement;
		return targetSize;
	}
}
