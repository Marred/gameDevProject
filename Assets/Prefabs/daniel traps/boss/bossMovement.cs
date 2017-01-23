using UnityEngine;
using System.Collections;

public class bossMovement : MonoBehaviour {
    Animator animator;
    Transform player;               // Reference to the player's position.
   
    NavMeshAgent nav;               // Reference to the nav mesh agent.


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    void SetTransformZ(float n)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, n);
    }
        void Update()
    {
        SetTransformZ(0f);
        
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (5 < distance && distance<14)
        {
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);
            animator.SetBool("isWalking", true);
        }
        else if(distance < 4 )
        {
            transform.position -= transform.forward * Time.deltaTime *4f;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            nav.SetDestination(this.transform.position);
        }
    }





    /* Animator animator;
    Transform player;
   
    int minRange = 6;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        player =  GameObject.FindWithTag("Player").transform;
    }
    private Transform target = null;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { target = other.transform;
             }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") target = null;
    }
    // Update is called once per frame
    void Update () {
        if (target == null) return;
       // transform.LookAt(target);
        float distance = Vector3.Distance(transform.position, target.position);
        bool tooClose = distance < minRange+0.5;
        bool tooClose2 = distance < minRange -0.5;
       // transform.position = Vector3.MoveTowards(transform.position, player.transform.position,3*Time.deltaTime);
      //  /*Vector3 direction = tooClose ? new Vector3(-1, 0) :new Vector3(1,0);
       // this.transform.position += direction *Time.deltaTime;
       if(!tooClose)
        { //animator.SetBool("isWalking",true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 3 * Time.deltaTime);
            animator.SetBool("isWalking", true);

        }
       else if(tooClose2)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -3 * Time.deltaTime);
        }
       else { animator.SetBool("isWalking", false); }
       
   
        


    }*/
}
