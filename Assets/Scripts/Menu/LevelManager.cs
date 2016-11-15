using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void SetScene () {

        Application.LoadLevel(1);

    }

	public void Quit()
	{
		Application.Quit();
	}

}
