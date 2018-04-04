using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour {

	[System.Serializable]
	public class GravityEffect {
		public float strength = 1f;
		public float radialDependency = 0f;
	}

	[SerializeField] List<GravityEffect> gravityEffects;
	[SerializeField] List<Collider2D> victims;

	public void AddVictim(Collider2D victim) {
		Ball ball = victim.GetComponent<Ball>();
		if (ball == null)
			return;
		victims.Add(victim);
		ball.state.BaseGravity = 0f;
		ball.state.CurrentGravity = 0f;
	}

	public void RemoveVictim(Collider2D victim) {
		Ball ball = victim.GetComponent<Ball>();
		if (ball == null)
			return;
		victims.Remove(victim);
		ball.state.BaseGravity = 1f;
		ball.state.CurrentGravity = 1f;
	}

	void Awake() {
		victims = new List<Collider2D> ();
	}

	void FixedUpdate() {
		foreach(Collider2D victim in victims) {
			Ball ball = victim.GetComponent<Ball>();
			if (ball == null)
				return;
			Vector3 distanceVector = transform.position - victim.transform.position;
			float distance = distanceVector.magnitude;
			Debug.Log(distance);
			Vector3 direction = distanceVector.normalized;
			Vector3 totalForce = Vector3.zero;
			foreach (GravityEffect effect in gravityEffects) {
				float scaledStrength = effect.radialDependency * 50 * effect.strength / (Mathf.Pow(10, effect.radialDependency));
				float effectMagnitude = scaledStrength * Mathf.Pow(distance, effect.radialDependency);
				Vector3 effectVector = direction * effectMagnitude;
				totalForce += effectVector;
			}
			ball.rb2d.AddForce(ball.rb2d.mass * totalForce);
		}
	}
}
