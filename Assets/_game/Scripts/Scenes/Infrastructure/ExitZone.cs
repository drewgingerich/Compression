// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class ExitZone : MonoBehaviour {

// 	[SerializeField] PlayerInfoLimitedRuntimeSet playerRoster;

// 	void OnTriggerEnter2D(Collider2D other) {
// 		OnBallDestroyed.Invoke(other.gameObject);
// 		OnPlayerLeave.Invoke(other.GetComponent<Ball>().playerInfo);
// 		Destroy(other.gameObject);
// 	}
// }
