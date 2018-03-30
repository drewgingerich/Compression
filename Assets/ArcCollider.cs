using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnTrigger2DEvent : UnityEvent<Rigidbody2D> {}

[RequireComponent(typeof(CircleCollider2D))]
public class ArcCollider : MonoBehaviour {

	public OnTrigger2DEvent OnEnterArc;
	public OnTrigger2DEvent OnExitArc;
	public OnTrigger2DEvent OnStayArc;

	[Header("Zero is to the right ->")] 
	[SerializeField] [Range(0, 360)] float arcCenterAngle;
	[SerializeField] [Range(0, 180)] float arcLengthHalfAngle;

	List<Collider2D> others;

	void Awake() {
		others = new List<Collider2D>();
	}

	void OnTriggerStay2D(Collider2D other) {
		float angle = FindAngle(other.transform.position);
		if (angle <= arcLengthHalfAngle) {
			if (!others.Contains(other))
				Add(other);
			else
				OnStayArc.Invoke(other.GetComponent<Rigidbody2D>());
		} else {
			Remove(other);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		float angle = FindAngle(other.transform.position);
		if (angle <= arcLengthHalfAngle)
			Add(other);
	}

	void OnTriggerExit2D(Collider2D other) {
		Remove(other);
	}

	void Add(Collider2D other) {
		others.Add(other);
		Rigidbody2D rb2d = other.GetComponent<Rigidbody2D>();
		OnEnterArc.Invoke(rb2d);
	}

	void Remove(Collider2D other) {
		others.Remove(other);
		Rigidbody2D rb2d = other.GetComponent<Rigidbody2D>();
		OnExitArc.Invoke(rb2d);
	}

	float FindAngle(Vector3 otherPosition) {
		Quaternion rotation = Quaternion.AngleAxis(arcCenterAngle, Vector3.forward);
		Vector2 arcCenterVector = rotation * Vector2.right;
		Vector2 triggerVector = otherPosition - transform.position;
		return Vector3.Angle(arcCenterVector, triggerVector);
	}
}
