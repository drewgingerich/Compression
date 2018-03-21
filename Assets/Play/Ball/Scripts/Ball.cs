using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	// public event System.Action<Ball> OnDie;

	// public AimBarBehavior aimBar;
	// public BallSpriteBehavior ballSprite;
	// public Animator animator;

	public InputScheme inputScheme;
	public BallCollisionManager collisionManager;
	public StickyStateManager stickyManager;
	public Animator animator;
	public AimBarUI aimBar;

	[SerializeField] BallController controller;

	void Awake() {
		inputScheme = new KeyboardInputScheme("Horizontal", "Vertical"); 
		controller = new AirbornController();
	}

	// public void Die () {
	// 	if (OnDie != null)
	// 		OnDie (this);
	// 	Destroy (gameObject);
	// }

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
}