using UnityEngine;
using System.Collections;

public class rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 70 * Time.deltaTime, 0);
    }
}
