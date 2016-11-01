using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject player;
	Camera myCamera;
	public float cameraOffset = 5.0f;

	// Use this for initialization
	void Start () {
		myCamera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (player.transform.position.x);
		Vector3 cameraPosition = new Vector3 (player.transform.position.x, myCamera.transform.position.y, myCamera.transform.position.z);
		myCamera.transform.position = cameraPosition;
	}
}
