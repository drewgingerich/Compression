using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour {

	public event System.Action OnEndPlay;

	[SerializeField] GameObject player1WinImage;
	[SerializeField] GameObject player2WinImage;
	[SerializeField] GameObject ballPrefab;
	[SerializeField] Transform spawn1;
	[SerializeField] Transform spawn2;

	bool gameDone = false;
	BallBehavior player1;
	BallBehavior player2;

	void Awake () {
		ControlScheme controlScheme1 = new ControlScheme ("Horizontal_wasd", "Vertical_wasd");
		player1 = Instantiate (ballPrefab).GetComponent<BallBehavior> ();
		player1.transform.SetParent (gameObject.transform);
		player1.gameObject.transform.position = spawn1.position;
		player1.LoadControlScheme (controlScheme1);
		player1.OnDie += DeclareWinner;

		ControlScheme controlScheme2 = new ControlScheme ("Horizontal", "Vertical");
		player2 = Instantiate (ballPrefab).GetComponent<BallBehavior> ();
		player2.transform.SetParent (gameObject.transform);
		player2.gameObject.transform.position = spawn2.position;
		player2.LoadControlScheme (controlScheme2);
		player2.OnDie += DeclareWinner;
	}

	void Update () {
		if (gameDone) {
			if (Input.GetKeyDown (KeyCode.Delete) || Input.GetKeyDown (KeyCode.Escape)) {
				if (OnEndPlay != null)
					OnEndPlay ();
			}
		}
	}

	void DeclareWinner (BallBehavior loser) {
		if (gameDone && OnEndPlay != null)
			OnEndPlay ();
		if (loser == player1) {
			player2WinImage.SetActive (true);
		} else if (loser == player2) {
			player1WinImage.SetActive (true);
		}
		gameDone = true;
	}
}
