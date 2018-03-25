using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnBallSpawnedEvent : UnityEvent<GameObject> {}

public class BallSpawner : MonoBehaviour {

	public OnBallSpawnedEvent onBallSpawned;

	[SerializeField] List<Transform> spawnPoints;
	[SerializeField] Ball ballPrefab;
	public void SpawnBall(InputScheme player) {
		Ball newBall = Instantiate(ballPrefab);
		newBall.transform.position = new Vector3(spawnPoints[0].position.x, spawnPoints[0].position.y, -1);
		newBall.inputScheme = player;
		newBall.transform.parent = transform;
		onBallSpawned.Invoke(newBall.collisionManager.gameObject);
	}
}
