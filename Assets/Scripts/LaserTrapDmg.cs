using UnityEngine;
using System.Collections;

public class LaserTrapDmg : MonoBehaviour {

    private Player playerScript;

    void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerScript.health.CurrentVal -= 1;
        }
    }
}
