using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BallObjectEvent : UnityEvent<GameObject> { }

public class BallSpawner : MonoBehaviour {

	public BallObjectEvent OnSpawnBall;

	[SerializeField] List<Transform> spawnPoints;
	[SerializeField] Ball ballPrefab;
	
	int numberSpawned = 0;

	public void SpawnBall(PlayerInfo player) {
		Ball newBall = Instantiate(ballPrefab);
		Transform activeSpawn = spawnPoints[numberSpawned];
		numberSpawned = (numberSpawned + 1) % spawnPoints.Count;
		newBall.transform.position = new Vector3(activeSpawn.position.x, activeSpawn.position.y, newBall.transform.position.z);
		newBall.playerInfo = player;
		newBall.transform.parent = transform;
		OnSpawnBall.Invoke(newBall.gameObject);
	}
}
