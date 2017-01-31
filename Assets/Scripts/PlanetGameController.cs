using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlanetGameController : MonoBehaviour {

    private GameObject player;
    private Player playerScript;

    public GameObject blockShip;
    public GameObject shipBackground;

    private System.Random rnd;
    public GameObject rock;
    public GameObject ufoEnemy;
    public GameObject meleeEnemy;

    public Light light;
    public AudioSource audio;
    public GameObject traps;
    public int trapsDelay;
    private float trapsTimer;
    public float maxIntensity = 1f;
    public float minIntensity = 0.3f;
    public float pulseSpeed = 0.8f;
    private float targetIntensity = 1f;
    private float currentIntensity;

    public float timer = 30f;
    public Text timerLabel;


	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        rnd = new System.Random();
        light = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Light>();
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

        InvokeRepeating("spawnRock", 0f, 5f);
        InvokeRepeating("spawnEnemy", 0f, 9f);
    }
	
	void Update () {
	
        if(player.transform.position.x <= 71)
        {
            blockShip.SetActive(false);
            shipBackground.SetActive(true);
            timer -= Time.deltaTime;
            if (timer <= 0) playerScript.health.CurrentVal = 0;
            timerLabel.text = Math.Floor(timer).ToString() + "s";

            currentIntensity = Mathf.MoveTowards(light.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
            if (currentIntensity >= maxIntensity)
            {
                currentIntensity = maxIntensity;
                targetIntensity = minIntensity;
            }
            else if (currentIntensity <= minIntensity)
            {
                currentIntensity = minIntensity;
                targetIntensity = maxIntensity;
            }
            light.intensity = currentIntensity;

            trapsTimer += Time.deltaTime;
            if (Math.Floor(trapsTimer) % trapsDelay == 0)
            {
                traps.SetActive(true);
            }
            else traps.SetActive(false);

        }
        else
        {
            audio.volume = Mathf.MoveTowards(audio.volume, 0f, Time.deltaTime / 5);
            timerLabel.text = "";
            blockShip.SetActive(true);
            shipBackground.SetActive(false);
        }

	}

    void spawnEnemy()
    {
        float spawnHeight = 1f;
        switch (rnd.Next(3))
        {
            case 0:
                spawnHeight = 1f;
                break;
            case 1:
                spawnHeight = 4.5f;
                break;
            case 2:
                spawnHeight = 8f;
                break;
        }
        if (player.transform.position.x >= 71) {
            if (rnd.Next(2) == 0) Instantiate(meleeEnemy, new Vector3(player.transform.position.x + 20, spawnHeight, 0), Quaternion.identity);
            else Instantiate(ufoEnemy, new Vector3(player.transform.position.x + 20, spawnHeight, 0), Quaternion.identity);
        }

    }

    void spawnRock()
    {
        float spawnHeight = 1f;
        switch(rnd.Next(3))
        {
            case 0:
                spawnHeight = 1f;
                break;
            case 1:
                spawnHeight = 4.5f;
                break;
            case 2:
                spawnHeight = 8f;
                break;
        }

        if (player.transform.position.x >= 71) Instantiate(rock, new Vector3(player.transform.position.x + 20, spawnHeight, 0), Quaternion.identity);
    }
}
