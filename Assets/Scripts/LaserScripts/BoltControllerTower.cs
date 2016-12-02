using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class BoltControllerTower : MonoBehaviour
{
    [SerializeField]private System.Random damageTake = new System.Random(); 
    [SerializeField]private float speed; // należy przypisać w inspektorze prędkość pocisku ()
    private Rigidbody rb;
    [SerializeField]private int fireMin; //minimalne DMG
    [SerializeField]private int fireMax; //maksymalne DMG
    private Player player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.velocity = transform.forward * speed;
        Destroy(this.gameObject, 5);
    }
    void OnTriggerEnter(Collider other)
      {
        float dmg = damageTake.Next(fireMin, fireMax);
          if (other.gameObject.tag == "Player")
          {
           player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.health.CurrentVal -= dmg;
            Destroy(this.gameObject);
          }
      }
    void OnCollisionEnter()
    {   
            Destroy(this.gameObject);    
    }
}
