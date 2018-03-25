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
	
	int numberSpawned = 0;

	public void SpawnBall(InputScheme player) {
		Ball newBall = Instantiate(ballPrefab);
		Transform activeSpawn = spawnPoints[numberSpawned];
		numberSpawned = (numberSpawned + 1) % spawnPoints.Count;
		newBall.transform.position = new Vector3(activeSpawn.position.x, activeSpawn.position.y, newBall.transform.position.z);
		newBall.inputScheme = player;
		newBall.transform.parent = transform;
		onBallSpawned.Invoke(newBall.collisionManager.gameObject);
	}
}
