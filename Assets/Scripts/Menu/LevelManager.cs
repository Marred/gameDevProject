using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void SetScene () {

        SceneManager.LoadScene(1);

    }

	public void Quit()
	{
		Application.Quit();
	}

}
