using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour
{
	[SerializeField] Ball ball;
	[SerializeField] Animator anim;

	int squishedHash;
	int impactHash;

	void Awake()
	{
		squishedHash = Animator.StringToHash("Squished");
		impactHash = Animator.StringToHash("Impact");
		ball.state.stateName.OnChange += AnimateBall;
	}

	void AnimateBall(StateName stateName)
	{
		if (stateName == StateName.Compressed || stateName == StateName.Rebound)
		{
			anim.SetBool(squishedHash, true);
		}
		else if (stateName == StateName.Impact)
		{
			anim.SetBool(impactHash, true);
		}
		else
		{
			anim.SetBool(squishedHash, false);
			anim.SetBool(impactHash, false);
		}
	}
}
