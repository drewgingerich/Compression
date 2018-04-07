using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public event System.Action<GameObject> OnDie = delegate { };

	public BallState state;
	public PlayerInfo playerInfo;
	public BallCollisionManager collisionManager;
	public Rigidbody2D rb2d;
	public StickyStateManager stickyManager;
	public Animator animator;
	public AimBarUI aimBar;
	public SpriteRenderer spriteRenderer;

	[SerializeField] BallController controller;

	void Awake() {
		controller = new AirbornController();
		state = new BallState(this);
	}

	void FixedUpdate () {
		bool checkForTransition = true;
		while (checkForTransition) {
			checkForTransition = CheckForNewState();
		}
		controller.Update (this);
	}

	bool CheckForNewState() {
		BallController newController = controller.CheckTransitions(this);
		if (newController != null) {
			controller.Exit(this);
			controller = newController;
			controller.Enter(this);
			return true;
		} else {
			return false;
		}
	}

	public void Die(){
		OnDie(gameObject);
		Destroy(gameObject);
	}
}