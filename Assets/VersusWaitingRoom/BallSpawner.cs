using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

	[SerializeField] List<Transform> spawnPoints;
	[SerializeField] Ball ballPrefab;
	public void SpawnBall(InputScheme player) {
		Ball newBall = Instantiate(ballPrefab, spawnPoints[0]);
		newBall.inputScheme = player;
	}
}
