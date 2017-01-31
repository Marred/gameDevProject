using UnityEngine;
using System;
using System.Collections;

public class RockRotate : MonoBehaviour {

    private int spinx;
    private int spiny;
    private int spinz;
    private System.Random rnd;

    void Start () {
        rnd = new System.Random();
        spinx = rnd.Next(5);
        spiny = rnd.Next(5);
        spinz = rnd.Next(5);
    }
	
	void Update () {
        this.gameObject.transform.Rotate(spinx, spiny, spinz);
    }
}
