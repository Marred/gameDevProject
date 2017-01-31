using UnityEngine;
using System.Collections;
using System;
public class IKHandler : MonoBehaviour {
	Animator animator;
	Vector3 lookPos;
	Vector3 IK_lookPos;
	Vector3 targetPos;
	ThirdPersonCharacter pl;

	public float lerpRate = 15;
	public float updateLookPosThreshold = 2;
	public float lookWeight = 1;
	public float bodyWeight = .9f;
	public float headWeight = 1;
	public float clampWeight = 1;

	public float rightHandWeight = 1;
	public float leftHandWeight = 1;

	public Transform rightHandTarget;
	public Transform rightElbowTarget;
	public Transform leftHandTarget;
	public Transform leftElbowTarget;




	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator> ();
		pl = GetComponent<ThirdPersonCharacter> ();
	//	Debug.Log ("Lol");
	}

	void OnAnimatorIK( )
	{

		//Debug.Log ("test");
		animator.SetIKPositionWeight (AvatarIKGoal.RightHand, rightHandWeight);
		animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, leftHandWeight);

		animator.SetIKPosition (AvatarIKGoal.RightHand, rightHandTarget.position);
		animator.SetIKPosition (AvatarIKGoal.LeftHand, leftHandTarget.position);

		animator.SetIKRotationWeight (AvatarIKGoal.RightHand, rightHandWeight);
		animator.SetIKRotationWeight (AvatarIKGoal.LeftHand, leftHandWeight);

		animator.SetIKRotation (AvatarIKGoal.RightHand, rightHandTarget.rotation);
		animator.SetIKRotation (AvatarIKGoal.LeftHand, leftHandTarget.rotation);

		animator.SetIKHintPositionWeight (AvatarIKHint.RightElbow, rightHandWeight);
		animator.SetIKHintPositionWeight (AvatarIKHint.LeftElbow, leftHandWeight);

		animator.SetIKHintPosition (AvatarIKHint.RightElbow, rightElbowTarget.position);
		animator.SetIKHintPosition (AvatarIKHint.LeftElbow, leftElbowTarget.position);

	

		this.lookPos = pl.lookPos;
		lookPos.z = transform.position.z; //chyba niepotrzebne

		//Debug.Log (AngleSigned(transform.position, lookPos, transform.position + new Vector3(0f,1.6f,0f)  ));
		//Debug.Log( transform.position );
		float distanceFromPlayer = Vector3.Distance (lookPos, pl.headTarget.position);

		if (distanceFromPlayer > updateLookPosThreshold && (pl.aimAngle < 0.9f || pl.aimAngle > -0.9f)) {
			targetPos = lookPos;
		
			//Debug.Log ("Distance:" + distanceFromPlayer);
			IK_lookPos = Vector3.Lerp (IK_lookPos, targetPos, Time.deltaTime * lerpRate);

			animator.SetLookAtWeight (lookWeight, bodyWeight, headWeight, headWeight, clampWeight);
			animator.SetLookAtPosition (lookPos);
		}
	}

	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(v1, v2)),
			Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
	}
	// Update is called once per frame
	void Update () {

	}
}
