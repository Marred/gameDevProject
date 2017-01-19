using UnityEngine;
using System.Collections;

public class activeTrapd1 : MonoBehaviour {
    [SerializeField]GameObject trap;
    private bool isCollide = false;
    [SerializeField]bool allowShotActive = false;
    void Update()
    {
        isCollide = false;
    }
  
    void OnTriggerEnter(Collider other)
    {
        if (isCollide) return;
        isCollide = true;
        if (other.gameObject.tag == "Player" || (other.gameObject.tag == "Bullet" && allowShotActive)) 
        {
            ActiveTrap();
            this.gameObject.SetActive(false);
            
           
        }
        
    }
    void ActiveTrap()
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
       
    }
}
