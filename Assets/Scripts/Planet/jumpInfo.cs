using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class jumpInfo : MonoBehaviour {
	[SerializeField] private Text jumpText;
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Player")
			return;
		
		Destroy (jumpText);
		Destroy (this);
	}

}
