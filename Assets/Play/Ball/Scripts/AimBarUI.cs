using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimBarUI : MonoBehaviour {

	LineRenderer lineRenderer;
	float lineStartX = 0.15f;
	float lineEndX = 0.6f;

	void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Start() {
		Hide();
	}

	public void Show() {
		gameObject.SetActive(true);
	}

	public void Hide() {
		gameObject.SetActive(false);
	}

	public void UpdatePosition(Vector2 compressionVector) {
		float scale = Mathf.Pow(compressionVector.magnitude, 2);
		Vector2 normalized = compressionVector.normalized;
		Vector3 direction3D	= new Vector3(normalized.x, normalized.y, 0);
		lineRenderer.SetPosition(0, direction3D * lineStartX);
		lineRenderer.SetPosition(1, direction3D * lineEndX * scale);
		// lineObject.transform.localScale = new Vector3 (20, scale * 4, 1);
		// Color tempColor = lineRenderer.startColor;
		// tempColor.a = scale;
		// lineRenderer.startColor = tempColor;
	}
}
