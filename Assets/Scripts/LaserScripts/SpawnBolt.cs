using UnityEngine;
using System.Collections;


public class SpawnBolt : MonoBehaviour {


    [SerializeField] private GameObject bolt;
    [SerializeField] private AudioSource fireAudio;
    [SerializeField] private Transform spawn;
    
    private float nextFire = 0.5f;
    [SerializeField]private float fireRate;
 
    void Start()
    {
        fireAudio = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) //Time.time jest to czas od rozpoczęcia gry i ma ta same wartosc jezeli                                            
        {    //[..]jest uzywany kilka razy w tej samej klatce
            nextFire = Time.time + fireRate;

            fireAudio.Play();
            //GameObject clone = 
           GameObject bolt1 = Instantiate(bolt, spawn.position, spawn.rotation) as GameObject; //as GameObject;//inicjalizacja jako gameObject
                                                                                             // inicjalizacja pocisku obiekt pozycja spawnu i jego ustawienie
            bolt1.transform.Rotate(Vector3.right); // korekta obrotu pocisku 
            Destroy(bolt1, 4.0f);  // po 4 sekundach  pocisk znika
        }
    }
}
