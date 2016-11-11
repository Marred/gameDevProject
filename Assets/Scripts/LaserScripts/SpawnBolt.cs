using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnBolt : MonoBehaviour {


    public GameObject bolt;
     [SerializeField] AudioSource audio;
    public Transform spawn;
    
    private float nextFire = 0.5f;
    public float fireRate;
    // Use this for initialization
    void Start()
    {
        audio =GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) //Time.time jest to czas od rozpoczęcia gry i ma ta same wartosc jezeli                                            
        {    //[..]jest uzywany kilka razy w tej samej klatce
            nextFire = Time.time + fireRate;

            audio.Play();
            //GameObject clone = 
           GameObject bolt1 = Instantiate(bolt, spawn.position, spawn.rotation) as GameObject; //as GameObject;//inicjalizacja jako gameObject
                                                                                             // inicjalizacja pocisku obiekt pozycja spawnu i jego ustawienie
            bolt1.transform.Rotate(Vector3.right); // korekta obrotu pocisku 
            Destroy(bolt1, 4.0f);  // po 4 sekundach  pocisk znika
        }
    }
}
