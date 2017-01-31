using UnityEngine;
using System.Collections;
using System;

public class IKHandler : MonoBehaviour {
	Animator animator;
	Vector3 IK_lookPos;
	ThirdPersonCharacter pl;

	public float updateLookPosThreshold = 2;
	//weight - jak duzo ciala zaangazowane jest w poruszanie
	public float lookWeight = 1;
	public float bodyWeight = .9f;
	public float headWeight = 1;
	public float clampWeight = 1;

	public Transform rightHandTarget;
	public Transform leftHandTarget;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator> ();
		pl = GetComponent<ThirdPersonCharacter> ();
	}

	void OnAnimatorIK( )
	{
		animator.SetIKPositionWeight (AvatarIKGoal.RightHand, 1);
		animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1);

		animator.SetIKPosition (AvatarIKGoal.RightHand, rightHandTarget.position);
		animator.SetIKPosition (AvatarIKGoal.LeftHand, leftHandTarget.position);

		float distanceFromPlayer = Vector3.Distance (pl.lookPos, pl.headTarget.position);
		//poruszaj glowa jesli dystans kursora i glowy jest wiekszy od zmiennej updateLookPosThreshold
		if (distanceFromPlayer > updateLookPosThreshold && (pl.aimAngle < 0.9f || pl.aimAngle > -0.9f)) {
			//plynne poruszanie glowa
			IK_lookPos = Vector3.Lerp (IK_lookPos, pl.lookPos, Time.deltaTime * 15f);

			animator.SetLookAtWeight (lookWeight, bodyWeight, headWeight, headWeight, clampWeight);
			animator.SetLookAtPosition (pl.lookPos);
		}
	}
}
