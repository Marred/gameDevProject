using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed; // przypisanie w menu minusowej predkosci
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;// poruszanie w osi Z czyli do przodu 

        
    }
	
}
