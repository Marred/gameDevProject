using UnityEngine;
using System.Collections;

public class CanvasLvlup : MonoBehaviour {
    [SerializeField]private Transform canv;
    [SerializeField]private Transform player;
	// trzyma sztywno canvas (przy obrocie postaci nie ma odbicia lustrzanego napisu)
	void Start ()
    {
        canv.rotation = Quaternion.Euler(canv.rotation.x, 90, canv.rotation.z);
        canv.position = new Vector3(canv.position.x, canv.position.y, 0);

    }
	
	
	void Update () {
        canv.rotation = Quaternion.Euler(canv.rotation.x, 90, canv.rotation.z);
        canv.position = new Vector3(player.position.x, player.position.y, 0);
    }
}
