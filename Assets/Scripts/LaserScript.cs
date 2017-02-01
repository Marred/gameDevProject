using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    private Player playerScript;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    } 
    private void OnTriggerEnter(Collider collider)
    {
        //zabija gracza jeśli wejdzie w laser
        Debug.Log(collider);
        if (collider.tag == "Player") playerScript.health.CurrentVal = 0;
        else Destroy(collider.gameObject);

    }
}
