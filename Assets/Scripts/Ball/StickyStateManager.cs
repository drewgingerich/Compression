using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StickyStateManager", menuName = "StickyStateManager")]
public class StickyStateManager : ScriptableObject {

	public bool ballIsSticky;
	public float gravity = 1f;
	[System.NonSerialized] public float stickyTime = 0f;

	public void EnableSticky(Ball ball) {
		ballIsSticky = true;
		// Vector2 normal = ball.collisionManager.GetSumContactNormal();
		Rigidbody2D rb2d = ball.collisionManager.gameObject.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 0;
		rb2d.velocity = rb2d.velocity * 0.5f;
		stickyTime = 0f;
	}

	public void DisableSticky(Ball ball) {
		ballIsSticky = false;
		ball.collisionManager.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
	}
}
