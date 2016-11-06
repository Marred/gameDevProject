using UnityEngine;
using System.Collections;

public class SpawnBolt : MonoBehaviour {


    GameObject bolt;
    Transform spawn;
    private float nextFire = 0.5f;
    public float fireRate;
    // Use this for initialization
    void Start () {
	
	}
	
	
	void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) //Time.time jest to czas od rozpoczęcia gry i ma ta same wartosc jezeli                                            
        {    //[..]jest uzywany kilka razy w tej samej klatce
            nextFire = Time.time + fireRate;
            
            //GameObject clone = 
            Instantiate(bolt, spawn.position, spawn.rotation); //as GameObject;//inicjalizacja jako gameObject
            // inicjalizacja pocisku objekt pozycja spawnu i jego ustawienie
        }
    }
}
