﻿using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class BoltController : MonoBehaviour
{
    
    [SerializeField]private System.Random damageTake = new System.Random(); 
    [SerializeField]private float speed; // należy przypisać w inspektorze prędkość pocisku 
    private Rigidbody rb;
    [SerializeField]private int fireMin; //minimalne DMG
    [SerializeField]private int fireMax; //maksymalne DMG

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*   poruszanie w osi Z czyli do przodu gdy jest .forward ale po korekcie
             obrotu musimy użyć przemieszczanie na ekranie w prawo   */
       rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter(Collider other)
      {
        float dmg = damageTake.Next(fireMin, fireMax);
          if (other.gameObject.tag == "Enemy")
          {
            other.transform.SendMessage("Damage",dmg);
            Destroy(this.gameObject);      
          }
      }
    void OnCollisionEnter()
    {   
           Destroy(this.gameObject);    
    }
}
