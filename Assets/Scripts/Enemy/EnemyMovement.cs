using UnityEngine;
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
    public float rangeOfVision = 100f;
    //Odstęp w jakim zatrzyma się obiekt wroga od gracza.
    public float distanceToStop = 4f;
    //Siła skoku
    public float jumpSpeed = 3f;
    //True jeśli pokonuje przeszkodę, blokuje ponowny skok przy tej samej przeszkodzie
    public bool isTriggered = false;
    public bool isAttacking = false;

    private Transform enemy;
    private Transform playertransform;
    //True obraca w stronę gracza
    private bool lookAtMe = false;
    //Współrzędne gracza
    private Vector3 playerXYZ;
    //Komponent rigidbody na przeciwniku
    Rigidbody rb;

    private Player player;

    //public Animator animator;

    void Start()
    {
        enemy = transform;
        //Utrzymywanie przeciwnika w pionie
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Pobranie obiektu gracza.
        playertransform = GameObject.FindWithTag("Player").transform;

        //Pobranie pozycji gracza.
        playerXYZ = new Vector3(playertransform.position.x, enemy.position.y, playertransform.position.z);

        //Pobranie dystansu dzielącego wroga od obiektu gracza.
        float distance = Vector3.Distance(enemy.position, playertransform.position);

        lookAtMe = false; //Wróg nie patrzy na gracza bo jeszcze nie wiadomo czy jest w zasięgu wzroku.

        //Sprawdzenie czy gracz jest w zasięgu wzroku wroga.
        if (distance <= rangeOfVision && distance > distanceToStop)
        {
            lookAtMe = true;//Gracz w zasiegu wzroku wiec na neigo patrzymy

            //Vector3.MoveTowards - pozwala na zdefiniowanie nowej pozycji gracza oraz wykonanie animacji.
            //Pierwszy parametr obecna pozycja drugi parametr pozycja do jakiej dążymy (czyli pozycja gracza).
            //Trzeci parametr określa z jaką prędkością animacja/ruch ma zostać wykonany.
            enemy.position = Vector3.MoveTowards(enemy.position, playerXYZ, movementSpeed * Time.deltaTime);

            //animator.SetFloat("speed", 5f);

            //może znów wywołać metodę Attack
            isAttacking = false;

        }
        else if (distance <= distanceToStop)
        { //Jeżeli wróg jest tuż przy graczu to niech ciągle na niego patrzy mimo że nie musi się już poruszać.
            lookAtMe = true;

            //atakuje gracza, jeżeli jeszcze go nie atakuje-gdyby nie warunek isAttacking, InvokeRepeating byłoby wywoływane co klatkę
            if (isAttacking == false)
            {
                InvokeRepeating("Attack", 0f, 5f);
                isAttacking = true;
            }
        }

        LookAtMe();

        //trzyma przeciwnika w osi Z
        enemy.position = new Vector3(enemy.position.x, enemy.position.y, 0);
    }

    //Wróg może nie mieć potrzeby sie pruszać bo jest blisko gracza ale niech się obraca w jego stronę.
    void LookAtMe()
    {
        if (smoothRotation && lookAtMe == true)
        {

            //Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji. 
            //Potrzebujemy go aby obrócić wroga w stronę gracza.
			Vector3 directionToLook = playerXYZ - enemy.position;
			if (directionToLook == Vector3.zero)
				return;
			
            Quaternion rotation = Quaternion.LookRotation(directionToLook);
            //Obracamy wroga w stronę gracza.
            enemy.rotation = Quaternion.Slerp(enemy.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
        else if (!smoothRotation && lookAtMe == true)
        { //Jeżeli nie chcemy gładkiego obracania się wroga tylko błyskawicznego obrotu.

            //Błyskawiczny obrót wroga.
            transform.LookAt(playerXYZ);
        }

    }

    void Attack()
    {
        player.health.CurrentVal -= 2;
    }

    void OnTriggerEnter(Collider coll)
    {
        //sprawdza, czy element powodujący kolizję to przeszkoda do przeskoczenia
        if (coll.tag == "Obstacle")
        {
            //sprawdza, czy przy tej kolizji nie zostało jeszcze wykonane AddForce
            if (isTriggered == false)
            {
                //dodaje siłę na obiekt: transform.kierunek * siła, ForceMode.TypSiły
                rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
                isTriggered = true;
            }
        }
    }

    //resetuje isTriggered po pokonaniu przeszkody, przeciwnik gotowy do pokonania następnej
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Obstacle")
        {
            isTriggered = false;
        }
    }
}