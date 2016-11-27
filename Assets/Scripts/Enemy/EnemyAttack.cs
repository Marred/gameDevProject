using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    RaycastHit hit;
    [SerializeField]private GameObject bolt;
    [SerializeField]private float nextFire = 0.5f;
    [SerializeField]private float fireRate;
    [SerializeField]private Transform spawn; //należy dołączyć pusty obiekt spawn obrot y90 z-90
	void Update () {
        Vector3 fwd = transform.TransformDirection(Vector3.forward ) *10;
        Vector3 poprawka = new Vector3(0, 1);
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
