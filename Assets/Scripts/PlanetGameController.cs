using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlanetGameController : MonoBehaviour {

    private GameObject player;
    private Player playerScript;

    public GameObject blockShip;
    public GameObject shipBackground;

    private System.Random rnd;
    public GameObject rock;
    public GameObject ufoEnemy;
    public GameObject meleeEnemy;

    public GameObject coverFloor;
    public GameObject elevator;
    public GameObject floor0;
    public GameObject floor2;
    public GameObject floor4;
    public GameObject floor6;
    public GameObject floor8;
    public List<GameObject> floors = new List<GameObject>();
    bool elevatorStarted = false;

    public Light sLight;
    public AudioSource sAudio;
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
        sLight = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Light>();
        sAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

        floors.Add(floor0);
        floors.Add(floor2);
        floors.Add(floor4);
        floors.Add(floor6);
        floors.Add(floor8);
        

        InvokeRepeating("spawnRock", 0f, 5f);
        InvokeRepeating("spawnEnemy", 0f, 4f);
    }
	
	void Update () {
	
        //wywoływane gdy gracz jest na statku
        if(player.transform.position.x <= 71)
        {
            //upewnia się, że wyjście jest otwarte a statek nie jest zasłonięty
            blockShip.SetActive(false);
            shipBackground.SetActive(true);
            //liczy czas, zabija gracza jeśli się skończy i wyświetla pozostały czas - math.floor aby wyświetlać tylko pełne sekundy
            timer -= Time.deltaTime;
            if (timer <= 0) playerScript.health.CurrentVal = 0;
            timerLabel.text = Math.Floor(timer).ToString() + "s";

            //migające światło, mathf.movetowards(zmieniana wartość, wartość którą chcemy osiągnąć, o ile zmieniamy wartość)
            currentIntensity = Mathf.MoveTowards(sLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
            //jeżeli osiągniemy bądź przekroczymy maksymalną, upewniamy się że jest dokładnie równa maksymalnej dla dokładności obliczeń, po czym podmieniamy targetIntensity z równania wyżej
            if (currentIntensity >= maxIntensity)
            {
                currentIntensity = maxIntensity;
                targetIntensity = minIntensity;
            //to samo dla minimalnej
            }
            else if (currentIntensity <= minIntensity)
            {
                currentIntensity = minIntensity;
                targetIntensity = maxIntensity;
            }
            //ustawiamy obliczoną wartość
            sLight.intensity = currentIntensity;


            //lasery włączone gdy reszta z dzielenia timera przez trapsdelay daje 0, więc co trapsDelay sekund
            trapsTimer += Time.deltaTime;
            if (Math.Floor(trapsTimer) % trapsDelay == 0)
            {
                traps.SetActive(true);
            }
            else traps.SetActive(false);

        }
        else
        { 
            //jeśli gracz opuści statek:
            //wyciszamy syrenę, usuwamy pozostały czas z HUDa, zmieniamy kolor światła, zasłaniamy statek i blokujemy wejście
            sAudio.volume = Mathf.MoveTowards(sAudio.volume, 0f, Time.deltaTime / 3);
            timerLabel.text = "";
            sLight.color = Color.white;
            blockShip.SetActive(true);
            shipBackground.SetActive(false);
        }

        //włącza windę, jeśli gracz osiągnie odpowiednią pozycję i nie jest jeszcze włączona
        if(player.transform.position.x > 216 && !elevatorStarted)
        {
            //neguje warunek z ifa
            elevatorStarted = true;
            //wywołuje ściany, laser i podpięty dźwięk
            elevator.SetActive(true);
            //wywołuje korutynę aby odczekać do zakończenia dźwięku przed startem windy
            StartCoroutine("ElevatorStarter");
        }
	}

    IEnumerator ElevatorStarter()
    {
        //czeka 3 sekundy, usuwa pełną podłogę z windy, wywołuje funkcję generowania podłóg oraz generuje kilka pierwszych podłóg
        yield return new WaitForSeconds(3);
        coverFloor.SetActive(false);
        InvokeRepeating("spawnFloor", 0f, 1.45f);
        instantiateFloor(0.05f);
        instantiateFloor(-5f);
        instantiateFloor(-10f);
        instantiateFloor(-15f);
        instantiateFloor(-20f);
        instantiateFloor(-25f);
    }

    void spawnEnemy()
    {
        float spawnHeight = randomHeight();
        //jeżeli gracz nie jest ani na statku ani w windzie, generuje losowo jednego z dwóch przeciwników 20 jednostek przed graczem na losowym poziomie
        if (190 >= player.transform.position.x && player.transform.position.x >= 71) {
            if (rnd.Next(2) == 0) Instantiate(meleeEnemy, new Vector3(player.transform.position.x + 20, spawnHeight, 0), Quaternion.identity);
            else Instantiate(ufoEnemy, new Vector3(player.transform.position.x + 20, spawnHeight, 0), Quaternion.identity);
        }

    }

    void spawnRock()
    {
        float spawnHeight = randomHeight();
        //jeżeli gracz nie jest ani na statku ani w windzie, generuje kamień 20 jednostek przed graczem na losowym poziomie
        if (190 >= player.transform.position.x && player.transform.position.x >= 71) Instantiate(rock, new Vector3(player.transform.position.x + 20, spawnHeight, 0), Quaternion.identity);
    }

    float randomHeight()
    {
        //zwraca losowo jedną z 3 wysokości
        switch (rnd.Next(3))
        {
            case 0:
                return 1f;
            case 1:
                return 4.5f;
            case 2:
                return 8f;
        }
        return 1f;
    }

    void spawnFloor()
    {
        //generuje podłogi na samym dole windy
        if (player.transform.position.x >= 211.5) instantiateFloor(-30f);
    }

    void instantiateFloor(float x)
    {
        //generuje losową podłogę na x wysokości
        Instantiate(floors[rnd.Next(floors.Count)], new Vector3(217, x, 0), Quaternion.identity);
    }
}
