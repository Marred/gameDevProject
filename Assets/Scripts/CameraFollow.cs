using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject player;
	Camera myCamera;

	public float cameraOffset = 1.0f;
	public float deadZone = 0.05f;
	public float speed = 1.5f;


	// Use this for initialization
	void Start () {
		myCamera = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		//Śledzenie postaci wzdłuż wektora X i Y z offsetem

		Vector3 checkVector = new Vector3 (player.transform.position.x, player.transform.position.y + cameraOffset, myCamera.transform.position.z);

		if (Vector3.Distance (checkVector, myCamera.transform.position) > deadZone)
			myCamera.transform.position = Vector3.Lerp (myCamera.transform.position, checkVector, speed * Time.deltaTime);
		
			//new Vector3 (player.transform.position.x, player.transform.position.y + cameraOffset, player.transform.position.z)
		//Vector3 cameraPosition = new Vector3 (player.transform.position.x, player.transform.position.y + cameraOffset, myCamera.transform.position.z);

		//myCamera.transform.position = cameraPosition;
	}
}
