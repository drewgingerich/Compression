using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] Animator anim;

	int squishedHash;
	int impactHash;

	int noSquishHash;
	int lowSquishHash;
	int medSquishHash;
	int highSquishHash;

	void Awake() {
		noSquishHash = Animator.StringToHash("NoSquish");
		lowSquishHash = Animator.StringToHash("LowSquish");
		medSquishHash = Animator.StringToHash("MedSquish");
		highSquishHash = Animator.StringToHash("HighSquish");

		ball.state.stateName.OnChange += AnimateBall;
	}

	void AnimateBall(StateName stateName) {
		if (stateName == StateName.Compressed) {
			anim.SetTrigger(medSquishHash);
		} else if (stateName == StateName.Rebound) {
			anim.SetTrigger(highSquishHash);
		} else if (stateName == StateName.Impact) {
			anim.SetTrigger(medSquishHash);
		} else if (stateName == StateName.Grounded) {
			anim.SetTrigger(lowSquishHash);
		} else if (stateName == StateName.Airborn) {
			anim.SetTrigger(noSquishHash);
		}
	}
}
