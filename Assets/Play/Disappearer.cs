using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disappearer : MonoBehaviour {

	SpriteRenderer sprite;
	[SerializeField] float disappearTime = 2f;
	float time = 0;

	// Use this for initialization
	void  Start () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
		StartCoroutine (Disappear ());
	}

	IEnumerator Disappear () {
		while (true) {
			time += Time.deltaTime;
			if (time >= disappearTime)
				Destroy (gameObject);
			yield return new WaitForEndOfFrame ();
		}
	}
}
