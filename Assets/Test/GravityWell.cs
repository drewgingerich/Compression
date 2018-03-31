using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour {

	[SerializeField] float strength = 1f;

	[SerializeField] List<Rigidbody2D> victims;

	public void AddVictim(Rigidbody2D victim) {
		victims.Add(victim);
	}

	public void RemoveVictim(Rigidbody2D victim) {
		Debug.Log("Hi");
		victims.Remove(victim);
	}

	void Awake() {
		victims = new List<Rigidbody2D> ();
	}

	void FixedUpdate() {
		foreach(Rigidbody2D victim in victims) {
			Vector3 distanceVector = transform.position - victim.transform.position;
			float distance = distanceVector.magnitude;
			float gravityMagnitude = strength * victim.mass * Mathf.Pow(distance, -2f);
			Vector3 gravityForce = distanceVector.normalized * gravityMagnitude;
			victim.AddForce(gravityForce);
		}
	}
}
