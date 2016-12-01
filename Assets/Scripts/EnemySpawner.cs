using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	public float repeatTime = 5f;
    public int counter= 0;
	// Use this for initialization
	void Start () {
        //rozpocznij powtarzanie, metoda, czas do startu, czas między powtórzeniami
        InvokeRepeating ("Spawn",3,repeatTime);
        
	}
	
	// Update is called once per frame
	void Spawn () {
        //wywołaj, wywoływany obiekt, pozycja, obrót
        if (counter <= 4)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);

        }
        

        counter++;
	}
}
