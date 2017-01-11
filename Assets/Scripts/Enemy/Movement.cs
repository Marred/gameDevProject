using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public LayerMask collisionLayer;

    public GameObject player;
    Rigidbody rb;

    public float speed=2;
    public float collisionModifier;
    public float heightDifference;
    public float searchDistance;
    public float stopDistance;

    Vector3 myPosition;
    Vector3 targetPosition;
    Vector3 nullPosition;

    Vector3 right;
    Vector3 offsetRight;
    Vector3 left;
    Vector3 offsetLeft;
    Vector3 direction;
    Vector3 offset;
    Vector3 offsetBack;
    Vector3 heightModifier;

    public bool isColliding;
    public bool onEdge;
    bool goingRight = true;

    bool ignoreEdge = false;
    bool canJump;
    public bool isGrounded;
    bool goingUp;

    Vector3 patrolStartPosition;
    public float patrolDistance;

    void Start()
    {
        SetDirection(goingRight);
        heightModifier = new Vector3(0, heightDifference, 0);
        myPosition = transform.position + heightModifier;
        patrolStartPosition = myPosition;
        //Ustawia targetPosition na pozycję niemożliwą do osiągnięcia, pozwala zacząć pętlę GoTo jeśli targetPosition jest inne niż nullPosition i zakończyć, jeśli takie same
        nullPosition = new Vector3(0, -100f, 0);
        targetPosition = nullPosition;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

	void FixedUpdate()
    {
        myPosition = transform.position + heightModifier;
        Debug.DrawLine(myPosition, offsetBack, Color.blue);
        Debug.DrawLine(myPosition, offset, Color.red);
        CheckForObstacles();

        //if hit ustaw targetPosition na obecną pozycję gracza

        CheckForPlayer();
        
        if (targetPosition != nullPosition)
        {
            GoTo(targetPosition);
        }

        SetDirection(goingRight);

        Move(direction);
    }

    //Udaj się do wskazanej pozycji
    void GoTo(Vector3 targetPosition)
    {
        
        //Sprawdza, czy dotarł do celu, jesli tak, przerywa pętlę GoTo unieważniając warunek w funkcji Update
        if(myPosition == targetPosition)
        {
            targetPosition = nullPosition;
            patrolStartPosition = myPosition;
        }
        //Sprawdza, czy gracz jest z lewej czy prawej strony i ustawia kierunek
        if ((myPosition.x > targetPosition.x ? myPosition.x - targetPosition.x : targetPosition.x - myPosition.x)<0.05)
        {
            goingUp = true;
            //Debug.Log("Szukam drogi do góry");
        }
        if (myPosition.x > targetPosition.x & goingUp == false)
        {
            goingRight =false;
        }
        else if(goingUp == false)
        {
            goingRight =true;
        }
        //Sprawdza pozycję gracza w osi Y i ustala, czy skoczyć/spaść tylko, jeśli stoi na ziemi
        if (isGrounded)
        {
            ignoreEdge = false;
            //Sprawdza, czy gracz poniżej, jeśli tak ignoruje krawędzie i spada niżej
            if ((myPosition.y - targetPosition.y) > 0.5)
            {
                //Debug.Log("Gracz jest poniżej" + (transform.position.y - targetPosition.y));
                ignoreEdge = true;
            }
            else if ((targetPosition.y - myPosition.y) > 2.5)
            {
                //Debug.Log("Gracz jest powyżej");
                canJump = (Physics.CheckBox(offset, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), groundLayer)
                & !Physics.CheckBox(myPosition + new Vector3(0f, 3f, 0), new Vector3(1.3f, 0.5f, 0), Quaternion.Euler(0, 0, 0), groundLayer));

                if (canJump)
                {
                    //Debug.Log("Skok w przód");
                    rb.velocity = new Vector3(0, 9f, 0);
                }
                if (goingUp & (Physics.CheckBox(offsetBack, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), groundLayer)
                & !Physics.CheckBox(myPosition + new Vector3(0f, 3f, 0), new Vector3(1.35f, 0.5f, 0), Quaternion.Euler(0, 0, 0), groundLayer)))
                {
                    //Debug.Log("Obrót");
                    goingUp = false;
                    goingRight = !goingRight;
                    SetDirection(goingRight);
                }
            }
            else
            {
                goingUp = false;
                //Debug.Log("Gracz jest na tym samym poziomie");
                /*if (onEdge)
                {
                    rb.velocity = new Vector3(1f, 1f, 0);
                }*/
                ignoreEdge = true;
            }
        }
    }


    //sprawdza, czy gracz jest w zasięgu, jeśli tak ustawia targetPosition na niego
    void CheckForPlayer()
    {
        if(Vector3.Distance(myPosition, player.transform.position)<=searchDistance)
        {
            targetPosition = player.transform.position;
        }
    }

    //sprawdza, czy przed nim nie ma kolizji bądź nie kończy się krawędź, jeśli tak, zmienia kierunek
    void CheckForObstacles()
    {
        isGrounded = Physics.CheckBox(myPosition, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), groundLayer);
        onEdge = !Physics.CheckBox(direction, new Vector3(0, 0, -0.05f), Quaternion.Euler(0, 0, 0), groundLayer);
        isColliding = Physics.CheckBox(direction + new Vector3(collisionModifier, 0.75f, 0), new Vector3(0, 0.7f, 0.35f), Quaternion.Euler(0, 0, 0), collisionLayer);
        if (isGrounded)
        {
            if (((onEdge && !ignoreEdge) | isColliding))
            {
                goingRight = !goingRight;
                //Debug.Log("Zawracam" + isGrounded+onEdge+isColliding);
            }
        }
    }

    //ustawia kierunki right i left, ustawia direction przed sobą
    void SetDirection(bool goingRight)
    {
        right = myPosition + new Vector3(0.5f, 0, 0);
        left = myPosition - new Vector3(0.5f, 0, 0);
        offsetRight = right + new Vector3(1.8f, 3f, 0);
        offsetLeft = left + new Vector3(-1.8f, 3f, 0);

        if (goingRight == true)
        {
            direction = right;
            transform.rotation = Quaternion.Euler(0, 90, 0);
            offset = offsetRight;
            offsetBack = offsetLeft;
        }
        else
        {
            direction = left;
            transform.rotation = Quaternion.Euler(0, 270, 0);
            offset = offsetLeft;
            offsetBack = offsetRight;
        }
    }

    //wykonuje ruch w kierunku direction
    void Move(Vector3 direction)
    {
        
        if (Vector3.Distance(myPosition, targetPosition) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction-heightModifier, speed*Time.deltaTime);
        }

    }
    
}
