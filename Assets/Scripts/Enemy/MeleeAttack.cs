using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {

    public float attackDistance = 2;
    bool isAttacking;
    float rotationSpeed;

    public GameObject saw;
    GameObject playerObject;
    Player playerScript;

    void Start () {

        playerObject = GameObject.FindWithTag("Player");
        playerScript = playerObject.GetComponent<Player>();
    }

	void Update () {

        rotationSpeed = 1f - (Vector3.Distance(transform.position, playerObject.transform.position) - 1f);

        if (Vector3.Distance(transform.position, playerObject.transform.position) < attackDistance)
        {

            saw.transform.Rotate(Vector3.up, 500f * rotationSpeed * Time.deltaTime);

            if (!isAttacking)
            {
                InvokeRepeating("Attack", 0f, 2f);
                isAttacking = true;
            }
        }
        else
        {
            CancelInvoke();
            isAttacking = false;
        }
    }

    void Attack ()
    {
        Debug.Log("Atakuje");
        playerScript.health.CurrentVal -= 5;
    }
}
