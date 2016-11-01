using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject player;
	Camera myCamera;
	public float cameraOffset = 1.0f;

	// Use this for initialization
	void Start () {
		myCamera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Śledzenie postaci wzdłuż wektora X i Y z offsetem
		Vector3 cameraPosition = new Vector3 (player.transform.position.x, player.transform.position.y + cameraOffset, myCamera.transform.position.z);

		myCamera.transform.position = cameraPosition;
	}
}
