using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	public float repeatTime = 12f;

	// Use this for initialization
	void Start () {
		//rozpocznij powtarzanie, metoda, czas do startu, czas między powtórzeniami
		InvokeRepeating ("Spawn", 0f, repeatTime);
	}
	
	// Update is called once per frame
	void Spawn () {
		//wywołaj, wywoływany obiekt, pozycja, obrót
		Instantiate (enemy, transform.position, Quaternion.identity);
	}
}
