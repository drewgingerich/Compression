using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCycler : MonoBehaviour {

	[SerializeField] List<BallSpawner> spawners;

	int spawnIndex = 0;

	public void SpawnBall(PlayerInfo playerInfo) {
		spawners[spawnIndex].SpawnBall(playerInfo);
		spawnIndex = (spawnIndex + 1) % spawners.Count;
	}
}
