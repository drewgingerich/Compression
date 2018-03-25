using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public event System.Action<GameObject> OnDie = delegate { };

	public InputScheme inputScheme;
	public BallCollisionManager collisionManager;
	public StickyStateManager stickyManager;
	public Animator animator;
	public AimBarUI aimBar;

	[SerializeField] BallController controller;

	void Awake() {
		controller = new AirbornController();
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