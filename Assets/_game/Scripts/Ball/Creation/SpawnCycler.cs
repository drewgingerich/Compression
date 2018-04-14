using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCycler : MonoBehaviour {

	[SerializeField] PlayerInfoLimitedRuntimeSet playerRoster;
	[SerializeField] List<BallSpawner> spawnPoints;

	int spawnIndex = 0;

	public void SpawnBall(PlayerInfo playerInfo) {
		spawnPoints[spawnIndex].SpawnBall(playerInfo);
		spawnIndex = (spawnIndex + 1) % spawnPoints.Count;
	}

	void OnEnable() {
		playerRoster.OnRegisterItem += SpawnBall;
	}

	void OnDisable() {
		playerRoster.OnRegisterItem -= SpawnBall;
	}
}
