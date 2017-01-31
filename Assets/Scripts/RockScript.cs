using UnityEngine;
using System;
using System.Collections;

public class RockScript : MonoBehaviour {

    private GameObject player;
    private Player playerScript;
    private float speed;

    private System.Random rnd;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        rnd = new System.Random();

        speed = UnityEngine.Random.Range(0.1f, 0.3f);
    }
	
	void Update () {
        this.gameObject.transform.Translate(-speed, 0, 0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerScript.health.CurrentVal -= 5;
            player.GetComponent<Rigidbody>().AddForce(-20, 0, 0, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
        else if (collider.tag == "Enemy")
        {
            //player.GetComponent<Rigidbody>().AddForce(-10, 0, 0, ForceMode.Impulse);
            Destroy(this.gameObject);
            Destroy(collider.gameObject);
        }
        else Destroy(this.gameObject);
    }
}
