using UnityEngine;
using System.Collections;

public class activeTrapd1 : MonoBehaviour {
    GameObject trap;
    void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Player")
        {

            Destroy(GameObject.FindWithTag("Dtrap1"));

        }
    }
}
