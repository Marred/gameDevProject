using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void SetScene (string name) {

        Application.LoadLevel(name);

    }

}
