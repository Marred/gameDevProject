using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour
{
	public Transform farEnd;
	private Vector3 frometh;
	private Vector3 untoeth;
	private float secondsForOneLength = 20f;

	void Start()
	{
		frometh = transform.position;
		untoeth = farEnd.position;
	}

	void Update()
	{
		transform.position = Vector3.Lerp(frometh, untoeth,
			Time.time/secondsForOneLength
		);
		//Debug.Log (transform.position);
	}
}