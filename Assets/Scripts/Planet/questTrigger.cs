using UnityEngine;
using System.Collections;

public class questTrigger : MonoBehaviour {
	private bool isColliding;
	private bool informed = false;
	public Player player;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(isColliding) return;
		isColliding = true;

		if (other.gameObject.tag == "Player" ) 
		{
			if (player.playerLevel.CurrentVal >= 5) {
				Application.LoadLevel ("Daniel"); //albo main menu (chyba trzeba zasaveowac)
			} else if (!informed) {
				StartCoroutine (inform ());
			}
				
			Debug.Log ("Hello player");
		
		}
	}

	IEnumerator inform(){
		player.makeAnnouncement ("general", "You need 5th level");
		informed = true;
		yield return new WaitForSeconds (5);
		informed = false;

	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player" ) 
		{
			Debug.Log ("Bye player");

		}
	}
	// Update is called once per frame
	void Update () {
		isColliding = false;
	}
}
