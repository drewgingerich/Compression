using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour {

	[SerializeField] float baseStrength = 1f;
	[SerializeField] float radialStrength = -1.75f;
	[SerializeField] List<Collider2D> victims;

	public void AddVictim(Collider2D victim) {
		Ball ball = victim.GetComponent<Ball>();
		if (ball == null)
			return;
		victims.Add(victim);
	}

	public void RemoveVictim(Collider2D victim) {
		Ball ball = victim.GetComponent<Ball>();
		if (ball == null)
			return;
		victims.Remove(victim);
		ball.state.BaseGravity = 1f;
	}

	void Awake() {
		victims = new List<Collider2D> ();
	}

	void FixedUpdate() {
		foreach(Collider2D victim in victims) {
			Ball ball = victim.GetComponent<Ball>();
			if (ball == null)
				return;
			ball.state.BaseGravity = 0;
			Vector3 distanceVector = transform.position - victim.transform.position;
			float distance = distanceVector.magnitude;
			float gravityMagnitude = baseStrength * ball.rb2d.mass * Mathf.Pow(distance, radialStrength);
			Vector3 gravityForce = distanceVector.normalized * gravityMagnitude;
			ball.rb2d.AddForce(gravityForce);
		}
	}
}
