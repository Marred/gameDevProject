using UnityEngine;
using System.Collections;

[System.Serializable]
public class BoltController : MonoBehaviour
{
   
    
    public float speed=13; // przypisanie w menu minusowej predkosci
    Rigidbody rb;
    [SerializeField]private  GameObject laserBolt; // należy przypisać laserBolt 
  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;// poruszanie w osi Z czyli do przodu gdy jest .forward ale po korekcie
                                              // obrotu musimy użyć przemieszczanie na ekranie w prawo

       ;

    }

    void OnTriggerEnter(Collider other)
      {
          if (other.gameObject.tag == "Enemy")
          {
            other.transform.SendMessage("Damage",10f);

            Destroy(laserBolt);
          }
      }
    void OnCollisionEnter()
    {   
            Destroy(this.laserBolt);    
    }
   

}
