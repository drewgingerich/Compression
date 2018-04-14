using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallSpawner : MonoBehaviour {

	[SerializeField] Ball ballPrefab;
	[SerializeField] Transform ballParent;

	public void SpawnBall(PlayerInfo playerInfo) {
		Ball newBall = Instantiate(ballPrefab);
		float x = transform.position.x;
		float y = transform.position.y;
		float z = newBall.transform.position.z;
		newBall.transform.position = new Vector3(x, y, z);
		newBall.playerInfo = playerInfo;
		newBall.transform.parent = ballParent.transform;
	}
}
