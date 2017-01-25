using UnityEngine;
using System.Collections;

public class triggerKill : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" ) 
		{
			Player player = other.gameObject.GetComponent<Player> ();
			player.health.CurrentVal = 0;

		}
	}
}