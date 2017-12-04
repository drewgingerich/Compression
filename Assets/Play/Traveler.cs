using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveler : MonoBehaviour {

	[SerializeField] Vector3 position1;
	[SerializeField] Vector3 position2;
	[SerializeField] float speed = 1;

	Vector3 deltaVector;

	void Start () {
		deltaVector = position2 - position1;
		StartCoroutine (Travel ());
	}
 
	private IEnumerator Travel () {
		while (true) {
			float progress = (Mathf.Cos(speed * Time.time) + 1) / 2;
			transform.localPosition = position1 + deltaVector * progress;
			yield return new WaitForEndOfFrame ();
		}
	}
}
