using UnityEngine;
using System.Collections;

public class activeTrapd1 : MonoBehaviour {
    [SerializeField]GameObject trap;
    private bool isCollide = false;
    void Update()
    {
        isCollide = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (isCollide) return;
        isCollide = true;
        if (other.gameObject.tag == "Player")
        {
            if (trap.activeSelf == true)
            {
                trap.SetActive(false);
                Debug.Log("dezaktywuje");
            }
            else
            {
                trap.SetActive(true);
                Debug.Log("aktywuje");
            }
            Destroy(this.gameObject);

        }
    }
}
