using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimBar : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] LineRenderer lineRenderer;
	float lineStartX = 0.15f;
	float lineEndX = 0.6f;

	void Awake() {
		ball.state.compressionDirection.OnChange += UpdateAimBar;
	}

	void Start() {
		Hide();
	}

	public void Show() {
		lineRenderer.enabled = true;
	}

	public void Hide() {
		lineRenderer.enabled = false;
	}

	public void UpdateAimBar(Vector2 compressionVector) {
		float scale = Mathf.Pow(compressionVector.magnitude, 2);
		Vector2 normalized = compressionVector.normalized;
		Vector3 direction3D	= new Vector3(normalized.x, normalized.y, 0);
		lineRenderer.SetPosition(0, direction3D * lineStartX);
		lineRenderer.SetPosition(1, direction3D * lineEndX * scale);
	}
}
