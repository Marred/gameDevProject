using UnityEngine;
using System.Collections;

public class ballDmg : MonoBehaviour
{
    private float fireRate=1f;
    private float nextFire = 0.5f;
    private Player player;
    void OnTriggerEnter(Collider other)
    {
        float dmg = 35f;
        if (other.gameObject.tag == "Player" && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.health.CurrentVal -= dmg;
            
        }
    }
}