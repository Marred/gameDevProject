using UnityEngine;
using System.Collections;

public class MoveFloor : MonoBehaviour {
	void Update () {
        //przesuwanie podłogi
        this.gameObject.transform.Translate(Vector3.up * Time.deltaTime * 3f);
        //niszczenie podłogi gdy dojdzie do lasera
        if(this.gameObject.transform.position.y > 9.5)
        {
            Destroy(this.gameObject);
        }
	}
}
