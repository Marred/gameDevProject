using UnityEngine;
using System.Collections;


public class SpawnBolt : MonoBehaviour {


    [SerializeField]private GameObject bolt;
    [SerializeField]private GameObject bolt1;
    [SerializeField]private GameObject bolt2;
    [SerializeField]private GameObject bolt3;
    [SerializeField]private GameObject bolt4;
    [SerializeField] private AudioSource fireAudio;
    [SerializeField] private Transform spawn;
    private Player player;
    
    private float nextFire = 0.5f;
    [SerializeField]private float fireRate;
 
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        fireAudio = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) //Time.time jest to czas od rozpoczęcia gry i ma ta same wartosc jezeli                                            
        {    //[..]jest uzywany kilka razy w tej samej klatce
            nextFire = Time.time + fireRate;

            fireAudio.Play();
            float level = player.laserUpgrade.CurrentVal;

            if (level == 1)
            {
                bolt = bolt1;
<<<<<<< origin/master
<<<<<<< HEAD
                Debug.Log("poziom 1 laseru");
=======
>>>>>>> fa66925e0b947a4abe92554b084825c7322a41fe
=======

>>>>>>> local
            }
            else if(level==2)
            {
                bolt = bolt2;
            }
            else if(level==3)
            {
                bolt = bolt3;
            }
            else if(level==4)
            {
                bolt = bolt4;
            }
            GameObject boltObject = Instantiate(bolt, spawn.position, spawn.rotation) as GameObject;
            // inicjalizacja pocisku obiekt pozycja spawnu i jego ustawienie
            boltObject.transform.Rotate(Vector3.right); // korekta obrotu pocisku 
            Destroy(boltObject, 4.0f);  // po 4 sekundach  pocisk znika
        }
    }
}
