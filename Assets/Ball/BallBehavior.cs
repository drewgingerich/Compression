using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour {

	public event System.Action OnDie;

	public ControlScheme controlScheme;
	public AimBarBehavior aimBar;
	public BallSpriteBehavior ballSprite;
	public Animator animator;
	Rigidbody2D rigidbody;

	public List<Collision2D> collisions { get; set; }
	BallStateController controller;

	public float gravity = 0.3f;
	public float speed = 0.5f;
	public bool grounded = false;
	public bool sticky = false;
	public float stickyTime = 0f;

	public Vector2 GetSumContactNormal () {
		Vector2 sumNormal = Vector2.zero;
		foreach (Collision2D collision in collisions)
			sumNormal += collision.contacts[0].normal;
		return sumNormal.normalized;
	}

	public void EnableSticky () {
		sticky = true;
		rigidbody.gravityScale = 0;
	}

	public void DisableSticky () {
		sticky = false;
		rigidbody.gravityScale = gravity;
		stickyTime = 0f;
	}

	public void Die () {
		if (OnDie != null)
			OnDie ();
		Destroy (gameObject);
	}

	void Update () {
		BallStateController newController = controller.CheckTransitions ();
		if (newController != null) {
			controller.Exit ();
			controller = newController;
			controller.Enter ();
		}
		controller.Update ();
	}

	void Awake () {
		rigidbody = gameObject.GetComponent<Rigidbody2D> ();
		collisions = new List<Collision2D> ();
		controller = (BallStateController) new AirbornStateController (this);
	}

	void OnCollisionEnter2D (Collision2D collision2D) {
		collisions.Add (collision2D);
		grounded = true;
	}

	void OnCollisionExit2D (Collision2D collision2D) {
		int i = 0;
		while (true) {
			if (collisions[i].collider == collision2D.collider)
				collisions.RemoveAt (i);
			else
				i++;
			if (i < collisions.Count)
				continue;
			else
				break;
		}
		Debug.Log (collisions.Count);
		if (collisions.Count == 0)
			DisableSticky ();
			grounded = false;
	}
}