using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public event System.Action<PlayerManager> OnPlayerLeave;
	public event System.Action OnRequestStart;

	

	public Player player;
	public bool playerReady = false;

	public void LoadPlayer (Player player) {
		this.player = player;
	}

	public void CheckPlayerStatus () {
		Vector2 inputdirection = player.GetInputDirection ();
		if (Vector2.Dot (Vector2.right, inputdirection) > 0) {
			if (!playerReady) {
				playerReady = true;
			} else {
				if (OnRequestStart != null)
					OnRequestStart ();
			}
		} else if (Vector2.Dot (Vector2.right, inputdirection) < 0) {
			if (!playerReady) {
				if (OnPlayerLeave != null)
					OnPlayerLeave (this);
			} else {
				playerReady = false;
			}
		}
	}
}
