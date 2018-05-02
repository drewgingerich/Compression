using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResizeForObjects : MonoBehaviour {

	[SerializeField] TransformRuntimeSet objectTransformSet;
	[SerializeField] float convergenceTime = 0.5f;
	[SerializeField] float baseOrthographicSize = 3;
	[SerializeField] float fallbackSize = 6f;
	[SerializeField] float buffer = 2.5f;

	new Camera camera;

	void Awake() {
		camera = GetComponent<Camera>();
	}

	public void Update () {
		float aspectRatio = (float)Screen.width / Screen.height;
		float newOrthographicSize = FindOrthographicSize(objectTransformSet.items, aspectRatio);
		float smoothedSize = SmoothSizeChange(camera.orthographicSize, newOrthographicSize, convergenceTime);
		camera.orthographicSize = smoothedSize;	
	}

	float FindOrthographicSize(List<Transform> objectTransforms, float aspectRatio) {
		if (objectTransforms.Count == 0)
			return fallbackSize;
		Vector2 cornerVector = FindCornerVector(transform.position, objectTransforms);
		cornerVector.x = cornerVector.x / aspectRatio;
		cornerVector += new Vector2(buffer, buffer);
		float verticalSize = Mathf.Max(cornerVector.y, baseOrthographicSize);
		float horizontalSize = Mathf.Max(cornerVector.x, baseOrthographicSize);
		return Mathf.Max(verticalSize, horizontalSize);
	}

	Vector2 FindCornerVector(Vector3 position, List<Transform> objectTransforms) {
		Vector2 cornerVector = Vector2.zero;
		foreach (Transform objTransform in objectTransforms) {
			float verticalDistance = Mathf.Abs(position.y - objTransform.position.y);
			if (verticalDistance > cornerVector.y)
				cornerVector.y = verticalDistance;
			float horizontalDistance = Mathf.Abs(position.x - objTransform.position.x);
			if (horizontalDistance > cornerVector.x)
				cornerVector.x = horizontalDistance;
		}
		return cornerVector;
	}

	float SmoothSizeChange(float size, float targetSize, float convergenceTime) {
		float change = targetSize - size;
		float velocity = change / convergenceTime;
		return size + velocity * Time.deltaTime;
	}
}
