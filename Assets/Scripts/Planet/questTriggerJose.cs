using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class questTriggerJose : MonoBehaviour
{
    private bool isColliding;
    private bool informed = false;
    [SerializeField]
    private Player player;
    [SerializeField]private GameSave mySave;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;

        if (other.gameObject.tag == "Player")
        {
            mySave.Save(true);
            SceneManager.LoadScene("Daniel");
            //Application.LoadLevel ("Daniel"); //albo main menu (chyba trzeba zasaveowac)
        }
    }

    void Update()
    {
        isColliding = false;
    }
}
