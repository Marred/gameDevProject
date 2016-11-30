using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    RaycastHit hit;
    [SerializeField]private GameObject bolt;
    [SerializeField]private float nextFire = 0.5f;
    [SerializeField]private float fireRate;
    [SerializeField]private Transform spawn; //należy dołączyć pusty obiekt spawn obrot dostosowac do enemy
	void Update () {
<<<<<<< origin/master
<<<<<<< HEAD
        Vector3 fwd = transform.TransformDirection(Vector3.forward ) *8;
=======
        Vector3 fwd = transform.TransformDirection(Vector3.forward ) *10;
>>>>>>> fa66925e0b947a4abe92554b084825c7322a41fe
=======

        Vector3 fwd = transform.TransformDirection(Vector3.forward ) *8;

>>>>>>> local
        Vector3 poprawka = new Vector3(0, 0);
        Debug.DrawRay(transform.position +poprawka, fwd , Color.green);
        if (Physics.Raycast(transform.position +poprawka,  fwd,out hit))
        {
            if(hit.collider.tag=="Player" && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate; 
               GameObject bolt3 = Instantiate(bolt, spawn.position,spawn.rotation) as GameObject ; 
                Destroy(bolt3, 3.0f);
            }
        }
    }
}
