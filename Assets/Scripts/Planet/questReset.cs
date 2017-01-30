using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class questReset : MonoBehaviour {
	private bool isColliding;
	private Rigidbody control;
	public CanvasGroup black;
	public GameObject sun;
	private Vector3 startPosition;
	private Quaternion startRotation;
	public Player player;

	private bool isInBase = false;
	private bool fadeIn = false;
	private bool fadeOut = false;

	// Use this for initialization
	void Start () {
		startPosition = sun.transform.position;
		startRotation = sun.transform.rotation;

		player.makeAnnouncement ("quest", "Get in the base until dawn!");
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" && !isInBase ) 
		{		
			if(isColliding) return;
			isColliding = true;

			StartCoroutine (inBase( other ));
		}
	}

	IEnumerator inBase( Collider other ){
		isInBase = true;
		control = other.gameObject.GetComponent<Rigidbody> ();
		Debug.Log (control.constraints);
		control.constraints = RigidbodyConstraints.FreezeAll;
		fadeIn = true;
		yield return new WaitForSeconds (2);

		sun.transform.position = startPosition;
		sun.transform.rotation = startRotation;

		yield return new WaitForSeconds (1);

		player.makeAnnouncement ("general", "You wake up rested");
		control.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
			RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
		fadeOut = true;
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player" ) 
		{
			isInBase = false;
			Debug.Log ("Bye player");

		}
	}
	// Update is called once per frame
	void Update () {
		isColliding = false;
		if (fadeIn) {
			black.gameObject.SetActive (true);
			black.alpha = black.alpha + 0.05f;

			if (black.alpha >= 1f)
				fadeIn = false;
		}		
		if (fadeOut ) {
			black.gameObject.SetActive (false);
			black.alpha = black.alpha - 0.05f;

			if (black.alpha <= 0f)
				fadeOut = false;
		}
		//Debug.Log (sun.transform.position.y);
		if (sun.transform.position.y < -120f) {
			player.health.CurrentVal -= 1;
		}
	}
}
