using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public LayerMask collisionLayer;

    public GameObject player;
    Rigidbody rb;

    public float speed;
    public float searchDistance;

    public bool isColliding;
    public bool onEdge;
    bool goingRight = true;
    Vector3 targetPosition;
    Vector3 nullPosition;

    bool ignoreEdge = false;
    bool canJump;
    public bool isGrounded;

    Vector3 right;
    Vector3 left;
    Vector3 direction;

    Vector3 patrolStartPosition;
    public float patrolDistance;

    void Start()
    {
        patrolStartPosition = transform.position;
        //Ustawia targetPosition na pozycję niemożliwą do osiągnięcia, pozwala zacząć pętlę GoTo jeśli targetPosition jest inne niż nullPosition i zakończyć, jeśli takie same
        nullPosition = new Vector3(0, -100f, 0);
        targetPosition = nullPosition;
        rb = GetComponent<Rigidbody>();
    }

	void Update()
    {

        //canJump = (Physics.CheckBox(direction + new Vector3(1.6f, 3f, 0), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), groundLayer)
        //& !Physics.CheckBox(direction + new Vector3(0f, 3f, 0), new Vector3(1f, 1f, 0), Quaternion.Euler(0, 0, 0), groundLayer));
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
        if(transform.position==targetPosition)
        {
            targetPosition = nullPosition;
            patrolStartPosition = transform.position;
        }
        //Sprawdza, czy gracz jest z lewej czy prawej strony i ustawia kierunek
        if(transform.position.x > targetPosition.x)
        {
            goingRight=false;
        }
        else
        {
            goingRight=true;
        }
        //Sprawdza pozycję gracza w osi Y i ustala, czy skoczyć/spaść tylko, jeśli stoi na ziemi
        if (isGrounded)
        {
            ignoreEdge = false;
            //Sprawdza, czy gracz poniżej, jeśli tak ignoruje krawędzie i spada niżej
            if ((transform.position.y - targetPosition.y) > 0.5)
            {
                //Debug.Log("Gracz jest poniżej" + (transform.position.y - targetPosition.y));
                ignoreEdge = true;
            }
            else if ((targetPosition.y - transform.position.y) > 2.5)
            {
                //Debug.Log("Gracz jest powyżej");
                canJump = (Physics.CheckBox(direction + new Vector3(1.6f, 3f, 0), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), groundLayer)
                & !Physics.CheckBox(transform.position + new Vector3(0f, 3f, 0), new Vector3(1f, 1f, 0), Quaternion.Euler(0, 0, 0), groundLayer));

                if (canJump)
                {
                    rb.velocity = new Vector3(0, 9f, 0);
                }
            }
            else
            {
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
        if(Vector3.Distance(transform.position, player.transform.position)<=searchDistance)
        {
            targetPosition = player.transform.position;
        }
    }

    //sprawdza, czy przed nim nie ma kolizji bądź nie kończy się krawędź, jeśli tak, zmienia kierunek
    void CheckForObstacles()
    {
        onEdge = !Physics.CheckBox(direction, new Vector3(0, 0, -0.01f), Quaternion.Euler(0, 0, 0), groundLayer);
        isColliding = Physics.CheckBox(direction + new Vector3(0, 0.75f, 0), new Vector3(0, 0.7f, 0.35f), Quaternion.Euler(0, 0, 0), collisionLayer);
        if ((onEdge&&!ignoreEdge) | isColliding)
        {
            goingRight = !goingRight;
        }
        isGrounded = Physics.CheckBox(transform.position, new Vector3(0, 0, 0));
    }

    //ustawia kierunki right i left, ustawia direction przed sobą
    void SetDirection(bool goingRight)
    {
        right = transform.position + new Vector3(0.5f, 0, 0);
        left = transform.position - new Vector3(0.5f, 0, 0);

        if (goingRight == true)
        {
            direction = right;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            direction = left;
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }

    //wykonuje ruch w kierunku direction
    void Move(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }
    
}
