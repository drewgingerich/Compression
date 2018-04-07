using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResizeForObjects : MonoBehaviour {

	[SerializeField] bool pixelPerfect;
	[SerializeField] int pixPerUnit;
	[SerializeField] float convergenceTime = 0.5f;
	[SerializeField] float baseOrthographicSize = 6;
	[SerializeField] float buffer = 2.5f;
	[SerializeField] List<GameObject> objects;

	float pixSize;
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
		pixSize = 1f / pixPerUnit;
		Debug.Log(Screen.width);
	}

	public void Update () {
		float aspectRatio = (float)Screen.width / Screen.height;
		float newOrthographicSize = FindOrthographicSize(objects, aspectRatio);
		float smoothedSize = SmoothSizeChange(camera.orthographicSize, newOrthographicSize, convergenceTime);
		if (pixelPerfect)
			smoothedSize -= smoothedSize % pixSize;
		camera.orthographicSize = smoothedSize;	
	}

	float FindOrthographicSize(List<GameObject> objects, float aspectRatio) {
		Vector2 cornerVector = FindCornerVector(transform.position, objects);
		cornerVector.x = cornerVector.x / aspectRatio;
		cornerVector += new Vector2(buffer, buffer);
		float verticalSize = Mathf.Max(cornerVector.y, baseOrthographicSize);
		float horizontalSize = Mathf.Max(cornerVector.x, baseOrthographicSize);
		return Mathf.Max(verticalSize, horizontalSize);
	}

	Vector2 FindCornerVector(Vector3 position, List<GameObject> objects) {
		Vector2 cornerVector = Vector2.zero;
		foreach (GameObject obj in objects) {
			float verticalDistance = Mathf.Abs(position.y - obj.transform.position.y);
			if (verticalDistance > cornerVector.y)
				cornerVector.y = verticalDistance;
			float horizontalDistance = Mathf.Abs(position.x - obj.transform.position.x);
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
