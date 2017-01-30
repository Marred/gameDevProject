using UnityEngine;
using System.Collections;

public class BossDeath : MonoBehaviour {
    
    public EnemyHP boss;
    [SerializeField]private GameObject grave;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject bossCanvas;
    private CanvasGroup canvasGroup;
    [SerializeField] private bool isDead = false;
    
    void Start () {
       
        canvasGroup = bossCanvas.GetComponent<CanvasGroup>();
        bossCanvas.SetActive(false);
    }
	
	void Update () {
     
        if (boss.enemyHealth.CurrentVal <= 0 && isDead == false )
        {
            Debug.Log(boss.enemyHealth.CurrentVal + " ile ma ");
         
            Death();
            isDead = true;
        }
        if(isDead == true& canvasGroup.alpha < +0.5f)
        {
            canvasGroup.alpha = canvasGroup.alpha + 0.01f;
        }
	}

    void Death()
    {
        Instantiate(grave, boss.transform.position+new Vector3(0, 0.12f, 0.75f), Quaternion.Euler(0, 135, 0));
        Instantiate(smoke, boss.transform.position, Quaternion.Euler(-90, 0, 0));
        boss.transform.localScale = new Vector3(0, 0, 0);
        bossCanvas.SetActive(true);
        DestroyObject(boss.gameObject);
    }
}
