using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

	[SerializeField] GameObject objectToFollow;
	
	void Update () {
		transform.position = new Vector3 (
			objectToFollow.transform.position.x,
			objectToFollow.transform.position.y,
			transform.position.z)
		;
	}
}
