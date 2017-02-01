using UnityEngine;
using System.Collections;

public class questTrigger : MonoBehaviour {
	private bool isColliding;
	private bool informed = false;
	public Player player;

	void OnTriggerEnter(Collider other) {
		if(isColliding) return;
		isColliding = true;

		if (other.gameObject.tag == "Player" ) 
		{
			if (player.playerLevel.CurrentVal >= 3) {
				Application.LoadLevel ("Daniel"); //albo main menu (chyba trzeba zasaveowac)
			} else if (!informed) {
				StartCoroutine (inform ());
			}
		}
	}

	IEnumerator inform(){
		player.makeAnnouncement ("general", "You need 3rd level");
		informed = true;
		yield return new WaitForSeconds (10);
		informed = false;

	}

	void Update () {
		isColliding = false;
	}
}
