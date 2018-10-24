using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Actor : MonoBehaviour {

	private const float shellWidth = 0.01f;

	private Rigidbody2D rb2d;
	private RaycastHit2D[] hitBuffer = new RaycastHit2D[8];
	private ContactFilter2D contactFilter;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
		contactFilter = new ContactFilter2D();
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
	}

	public void Move(Vector2 move, System.Action<RaycastHit2D> onCollide) {
		Debug.Log(move.magnitude);
		Vector2 direction = move.normalized;
		float distance = move.magnitude;
		int count = rb2d.Cast(direction, contactFilter, hitBuffer, distance);
		if (count > 0) {
			distance = hitBuffer[0].distance - shellWidth;
		}
		rb2d.position += direction * distance;
		if (count > 0 && onCollide != null) {
			onCollide(hitBuffer[0]);
		}
	}
}
