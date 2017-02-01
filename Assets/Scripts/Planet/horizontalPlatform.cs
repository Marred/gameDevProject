using UnityEngine;
using System.Collections;

public class horizontalPlatform : MonoBehaviour {
	private Vector3 frometh;
	private Vector3 untoeth;
	private float secondsForOneLength = 20f;

	void Start()
	{
		frometh = transform.position;
		untoeth = transform.position + new Vector3 (0f, 15f, 0f);
	}

	void Update()
	{
		transform.position = Vector3.Lerp(frometh, untoeth,
			Mathf.SmoothStep(0f,1f,
				Mathf.PingPong(Time.time/secondsForOneLength, 1f)
			) );
	}
}
