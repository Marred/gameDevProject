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

	    if(player.health.CurrentVal == 0 && isDead == false)
        {
            Death();
            isDead = true;
        }
        if(isDead == true& canvasGroup.alpha < 1)
        {
            canvasGroup.alpha = canvasGroup.alpha + 0.01f;
        }
	}

    void Death()
    {
        Instantiate(grave, player.transform.position+new Vector3(0, 0.12f, 0.75f), Quaternion.Euler(0, 135, 0));
        Instantiate(smoke, player.transform.position, Quaternion.Euler(-90, 0, 0));
        player.transform.localScale = new Vector3(0, 0, 0);
        deathCanvas.SetActive(true);
    }
}
