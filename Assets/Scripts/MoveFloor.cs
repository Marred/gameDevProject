using UnityEngine;
using System.Collections;

public class MoveFloor : MonoBehaviour {
	void Update () {
        this.gameObject.transform.Translate(Vector3.up * Time.deltaTime * 3f);
        if(this.gameObject.transform.position.y > 9.5)
        {
            Destroy(this.gameObject);
        }
	}
}
