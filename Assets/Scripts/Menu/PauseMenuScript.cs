using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Kajetan");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
