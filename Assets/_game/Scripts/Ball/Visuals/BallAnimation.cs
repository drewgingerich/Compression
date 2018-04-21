using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour
{
	[SerializeField] Ball ball;
	[SerializeField] Animator anim;

	int squishedHash;

	void Awake()
	{
		squishedHash = Animator.StringToHash("Squished");
		ball.state.stateName.OnChange += AnimateBall;
	}

	void AnimateBall(StateName stateName)
	{
		if (stateName == StateName.Compressed || stateName == StateName.Rebound)
		{
			anim.SetBool(squishedHash, true);
		}
		else
		{
			anim.SetBool(squishedHash, false);
		}
	}
}
