using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour {

	[SerializeField] Ball ball;

	[SerializeField] AudioClip launchSound;

	void Awake() {
		ball.state.previousState.OnChange += PlayBallSound;
	}

	void PlayBallSound(StateName previousStateName) {
		if (previousStateName == StateName.Compressed || previousStateName == StateName.Rebound) {
			AudioSource.PlayClipAtPoint(launchSound, transform.position);
		}
	}
}
