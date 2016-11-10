﻿using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    //Predkosc obrotu przeciwnika.
    public float rotationSpeed = 6.0f;

    //Gładki obrót przeciwnika
    public bool smoothRotation = true;


    //Prędkość poruszania się przeciwnika.
    public float movementSpeed = 5.0f;
    //Odległość na jaką widzi przeciwnik.
    public float rangeOfVision = 10f;
    //Odstęp w jakim zatrzyma się obiekt wroga od gracza.
    public float distanceToStop = 2f;

    public float jumpSpeed = 5f;

    public bool isTriggered = false;

    private Transform enemy;
    private Transform player;
    private bool lookAtMe = false;
    private Vector3 playerXYZ;

    Rigidbody rb;

    void Start()
    {
        enemy = transform;
        //Utrzymywanie przeciwnika w pionie
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Pobranie obiektu gracza.
        player = GameObject.FindWithTag("Player").transform;

        //Pobranie pozycji gracza.
        playerXYZ = new Vector3(player.position.x, enemy.position.y, player.position.z);

        //Pobranie dystansu dzielącego wroga od obiektu gracza.
        float distance = Vector3.Distance(enemy.position, player.position);

        lookAtMe = false; //Wróg nie patrzy na gracza bo jeszcze nie wiadomo czy jest w zasięgu wzroku.

        //Sprawdzenie czy gracz jest w zasięgu wzroku wroga.
        if (distance <= rangeOfVision && distance > distanceToStop)
        {
            lookAtMe = true;//Gracz w zasiegu wzroku wiec na neigo patrzymy

            //Vector3.MoveTowards - pozwala na zdefiniowanie nowej pozycji gracza oraz wykonanie animacji.
            //Pierwszy parametr obecna pozycja drugi parametr pozycja do jakiej dążymy (czyli pozycja gracza).
            //Trzeci parametr określa z jaką prędkością animacja/ruch ma zostać wykonany.
            enemy.position = Vector3.MoveTowards(enemy.position, playerXYZ, movementSpeed * Time.deltaTime);

        }
        else if (distance <= distanceToStop)
        { //Jeżeli wróg jest tuż przy graczu to niech ciągle na niego patrzy mimo że nie musi się już poruszać.
            lookAtMe = true;
        }

        patrzNaMnie();
    }

    //Wróg może nie mieć potrzeby sie pruszać bo jest blisko gracza ale niech się obraca w jego stronę.
    void patrzNaMnie()
    {
        if (smoothRotation && lookAtMe == true)
        {

            //Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji. 
            //Potrzebujemy go aby obrócić wroga w stronę gracza.
            Quaternion rotation = Quaternion.LookRotation(playerXYZ - enemy.position);
            //Obracamy wroga w stronę gracza.
            enemy.rotation = Quaternion.Slerp(enemy.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
        else if (!smoothRotation && lookAtMe == true)
        { //Jeżeli nie chcemy gładkiego obracania się wroga tylko błyskawicznego obrotu.

            //Błyskawiczny obrót wroga.
            transform.LookAt(playerXYZ);
        }

    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Obstacle")
        {
            if (isTriggered == false)
            {
                //dodaje siłę na obiekt: transform.kierunek * siła, ForceMode.TypSiły
                rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
                isTriggered = true;
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Obstacle")
        {
            isTriggered = false;
        }
    }
}