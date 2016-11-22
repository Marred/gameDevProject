using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public bool isPaused;
    public GameObject pauseMenu;
    public GameObject hud;

    void Update()
    {
        // włącza menu pauzy,wyłącza HUD, zatrzymuje grę
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            hud.SetActive(false);
            Time.timeScale = 0;
        }
        //wyłącza menu pauzy, włącza HUD, wznawia grę
        else
        {
            pauseMenu.SetActive(false);
            hud.SetActive(true);
            Time.timeScale = 1;
        }

        //pauzowanie klawiszem escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    //wznawianie poprzez opcję w menu
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
