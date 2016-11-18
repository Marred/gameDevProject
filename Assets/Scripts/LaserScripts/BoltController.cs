using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class BoltController : MonoBehaviour
{
    [SerializeField]private System.Random damageTake = new System.Random(); 
    [SerializeField]private float speed=13; // przypisanie w menu minusowej predkosci
    private Rigidbody rb;
    [SerializeField]private  GameObject laserBolt; // należy przypisać laserBolt 
    [SerializeField]private int fireMin;
    [SerializeField]private int fireMax;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;// poruszanie w osi Z czyli do przodu gdy jest .forward ale po korekcie
                                              // obrotu musimy użyć przemieszczanie na ekranie w prawo

       ;

    }

    void OnTriggerEnter(Collider other)
      {
     
        float dmg = damageTake.Next(fireMin, fireMax);
          if (other.gameObject.tag == "Enemy")
          {
            other.transform.SendMessage("Damage",dmg);
            Debug.Log("zadane dmg " +dmg);
            Destroy(laserBolt);
          }
      }
    void OnCollisionEnter()
    {   
          Destroy(this.laserBolt);   
            
    }
   

}
