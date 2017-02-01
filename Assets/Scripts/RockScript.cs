using UnityEngine;
using System;
using System.Collections;

public class RockScript : MonoBehaviour {

    public GameObject particles;
    private GameObject player;
    private Player playerScript;
    private float speed;

    private System.Random rnd;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        //unityengine.random, bo systemowy nie wspiera floatów
        speed = UnityEngine.Random.Range(0.1f, 0.3f);
    }
	
	void Update () {
        this.gameObject.transform.Translate(-speed, 0, 0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        //jeśli trafia gracza, odejmuje hp i odpycha go
        if (collider.tag == "Player")
        {
            playerScript.health.CurrentVal -= 5;
            player.GetComponent<Rigidbody>().AddForce(-20, 0, 0, ForceMode.Impulse);
        }
        //jeśli trafia przeciwnika, od razu go zabija
        else if (collider.tag == "Enemy")
        {
            Destroy(collider.gameObject);
        }
        //niszczenie kamienia i particle system
        Destroy(this.gameObject);
        Instantiate(particles, transform.position, Quaternion.identity);
    }
}
