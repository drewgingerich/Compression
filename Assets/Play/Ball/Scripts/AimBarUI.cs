using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBarUI : MonoBehaviour {

	LineRenderer lineRenderer;

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
		float scale = compressionVector.magnitude;
		Vector2 normalized = compressionVector.normalized;
		Vector3 direction3D	= new Vector3(normalized.x, normalized.y, 0);
		lineRenderer.SetPosition(0, direction3D * 0.15f);
		lineRenderer.SetPosition(1, direction3D * 0.6f);
		// lineObject.transform.localScale = new Vector3 (20, scale * 4, 1);
		Color tempColor = lineRenderer.startColor;
		tempColor.a = scale;
		lineRenderer.startColor = tempColor;
	}
}
