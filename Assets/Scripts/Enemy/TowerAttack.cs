using UnityEngine;
using System.Collections;

public class TowerAttack : MonoBehaviour {//skrypt przeznaczony do strzelania przez przeciwników z funkcją LookAt - ruszające się dzialko
    private GameObject player;
    
    [SerializeField]private float positionControll;//0 dla ufo, 1 dla tower


    private Vector3 positionController;
    private Quaternion rotControler;
    RaycastHit hit;
    [SerializeField]private float nextFire = 0.5f;
    [SerializeField]private float fireRate;
    [SerializeField]private int distance = 7;
    [SerializeField]private GameObject bolt;
    [SerializeField]private Transform spawn; //należy dołączyć pusty obiekt spawn obrot dostosowac do enemy
 
    void Start () {
        player = GameObject.FindWithTag("Player");
     
        positionController = new Vector3(0, positionControll, 0);
            
    }

    void Update () {
        this.transform.LookAt(player.transform.position+ positionController);
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * distance;
        Debug.DrawRay(transform.position , fwd, Color.green);

        if (Physics.Raycast(transform.position , fwd, out hit,distance))
        {
            if (hit.collider.tag == "Player" && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject bolt3 = Instantiate(bolt, spawn.position, spawn.rotation ) as GameObject;
                Destroy(bolt3, 3.0f);
                
            }
        }
    }
}
