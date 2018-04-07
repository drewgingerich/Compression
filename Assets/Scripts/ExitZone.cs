using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitZone : MonoBehaviour {

	public BallObjectEvent OnBallDestroyed;
	public PlayerEvent OnPlayerLeave;

	void OnTriggerEnter2D(Collider2D other) {
		OnBallDestroyed.Invoke(other.gameObject);
		OnPlayerLeave.Invoke(other.GetComponent<Ball>().playerInfo);
		Destroy(other.gameObject);
	}
}
