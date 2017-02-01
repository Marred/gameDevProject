using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

    private Player player;
    public GameObject grave;
    public GameObject smoke;
    public GameObject deathCanvas;
    private CanvasGroup canvasGroup;
    public bool isDead = false;
    
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        canvasGroup = deathCanvas.GetComponent<CanvasGroup>();
        deathCanvas.SetActive(false);
    }
	
	void FixedUpdate () {
        //gdy hp spadnie do 0 aktywuj Death()
	    if(player.health.CurrentVal == 0 && isDead == false)
        {
            Death();
            isDead = true;
        }
        //stopniowo przyciemnia ekran po śmierci gracza
        if(isDead == true & canvasGroup.alpha < 1)
        {
            canvasGroup.alpha = canvasGroup.alpha + 0.01f;
        }
	}

    void Death()
    {
        //generuje grób i dym w miejscu śmierci, zmniejsza gracza i włącza canvas z informacją o śmierci
        Instantiate(grave, player.transform.position+new Vector3(0, 0.12f, 0.75f), Quaternion.Euler(0, 135, 0));
        Instantiate(smoke, player.transform.position, Quaternion.Euler(-90, 0, 0));
        player.transform.localScale = new Vector3(0, 0, 0);
        deathCanvas.SetActive(true);
    }
}
