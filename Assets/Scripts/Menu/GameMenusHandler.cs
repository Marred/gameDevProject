using UnityEngine;
using System.Collections;

public class GameMenusHandler : MonoBehaviour
{
    public bool isPaused;
	public bool statsMenuOpened;

	// INITIALIZE
	public GameObject statsMenu;
	public GameObject pauseMenu;
	public GameObject hud;

    void Update()
    {
        //pauzowanie klawiszem escape
		//jezeli pause otwarte, to nie pozwol na stats
		//jezeli pause otwarte, to pozwol na escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (!pauseMenu.active) {
				if (!statsMenu.active) {
					openMenu (pauseMenu);
				} else if(statsMenu.active) {
					closeMenu (statsMenu);
				}
			}
			else {
				closeMenu (pauseMenu);
			}
        }

		if (Input.GetKeyDown(KeyCode.B))
		{
			if (!pauseMenu.active){
				if (!statsMenu.active) {
					openMenu (statsMenu);
				} else {
					closeMenu (statsMenu);
				}
			}
		}
    }



	public void openMenu( GameObject menu ){
		//aby wywolac metode onEnable
		///foreach (MonoBehaviour comp in menu.GetComponents<MonoBehaviour>())
		//	comp.enabled = true;
		
		menu.SetActive(true);
		hud.SetActive(false);
		Time.timeScale = 0;
	}

	public void closeMenu( GameObject menu ){
		//aby wywolac metode onDisable
		//foreach (MonoBehaviour comp in menu.GetComponents<MonoBehaviour>())
		//	comp.enabled = false;
		
		menu.SetActive(false);
		hud.SetActive(true);
		Time.timeScale = 1;

	}

}
