using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResizeForObjects : MonoBehaviour {

	[SerializeField] float growSpeed = 2; 
	[SerializeField] float shrinkSpeed = 0.5f;
	[SerializeField] float baseOrthographicSize = 6;
	[SerializeField] float buffer = 2.5f;
	[SerializeField] List<GameObject> objects;

	new Camera camera;

	public void AddObject(GameObject gameObject) {
		objects.Add(gameObject);
	}

	public void RemoveObject(GameObject gameObject) {
		objects.Remove(gameObject);
	}

	void Awake() {
		objects = new List<GameObject>();
		camera = GetComponent<Camera>();
	}

	public void Update () {
		float aspectRatio = (float)Screen.width / Screen.height;
		float orthographicSize = FindOrthographicSize(aspectRatio);
		float smoothedSize = SmoothSizeChange(camera.orthographicSize, orthographicSize, growSpeed, shrinkSpeed);
		camera.orthographicSize = smoothedSize;	
	}

	float FindOrthographicSize(float aspectRatio) {
		float currentX = transform.position.x;
		float currentY = transform.position.y;
		Vector2 cornerVector = Vector2.zero;
		foreach (GameObject obj in objects) {
			float verticalDistance = Mathf.Abs(currentY - obj.transform.position.y);
			if (verticalDistance > cornerVector.y) {
				cornerVector.y = verticalDistance;
			}
			float horizontalDistance = Mathf.Abs(currentX - obj.transform.position.x);
			float scaledHorizontalDistance = horizontalDistance / aspectRatio;
			if (scaledHorizontalDistance > cornerVector.x)
				cornerVector.x = scaledHorizontalDistance;
		}
		cornerVector += new Vector2(buffer, buffer);
		float verticalSize = Mathf.Max(cornerVector.y, baseOrthographicSize);
		float horizontalSize = Mathf.Max(cornerVector.x, baseOrthographicSize);
		return Mathf.Max(verticalSize, horizontalSize);
	}

	float SmoothSizeChange(float size, float targetSize, float growSpeed, float shrinkSpeed) {
		float change = targetSize - size;
		float maxGrowth = growSpeed * Time.deltaTime;
		float maxShrink = -shrinkSpeed * Time.deltaTime;
		if (change > maxGrowth) {
			return size + maxGrowth;
		} else if (change < maxShrink) {
			return size + maxShrink;
		} else {
			return targetSize;
		}
	}
}
