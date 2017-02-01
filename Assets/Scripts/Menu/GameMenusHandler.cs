using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameMenusHandler : MonoBehaviour
{
    public bool isPaused;
	public bool statsMenuOpened;

	// INITIALIZE
	public GameObject statsMenu;
	public GameObject pauseMenu;
	public GameObject hud;
    public GameObject howToPlay;

	private AudioSource[] allAudioSources;

    void Update()
    {
        //pauzowanie klawiszem escape
		//jezeli pause otwarte, to nie pozwol na stats
		//jezeli pause otwarte, to pozwol na escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (!pauseMenu.activeSelf) {
				if (!statsMenu.activeSelf && !howToPlay.activeSelf) {
					openMenu (pauseMenu);
				} else if(statsMenu.activeSelf) {
					closeMenu (statsMenu);
				}  else if (howToPlay.activeSelf) {
                    closeMenu(howToPlay);
                }
			}
			else {
				closeMenu (pauseMenu);
			}
        }

		if (Input.GetKeyDown(KeyCode.B))
		{
			if (!pauseMenu.activeSelf && !howToPlay.activeSelf)
            {
				if (!statsMenu.activeSelf) {
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

		//tymczasowy fix?
		allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		foreach( AudioSource audio in allAudioSources) {
			audio.Stop();
		}

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

    public void mainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

}
