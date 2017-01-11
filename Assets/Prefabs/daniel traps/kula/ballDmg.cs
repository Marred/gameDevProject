using UnityEngine;
using System.Collections;

public class ballDmg : MonoBehaviour
{   [SerializeField]
    private float fireRate=1f;
    [SerializeField]
    private float nextFire = 0.5f;
    [SerializeField] float dmg = 35f;
    private Player player;
    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Player" && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.health.CurrentVal -= dmg;
            
        }
    }
}