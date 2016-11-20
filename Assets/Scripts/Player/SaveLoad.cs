using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour
{

    public Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Save()
    {

        PlayerPrefs.SetFloat("X", transform.position.x);
        PlayerPrefs.SetFloat("Y", transform.position.y);
        PlayerPrefs.SetFloat("Z", transform.position.z);

        PlayerPrefs.SetFloat("Health", Player.Health.CurrentVal);
        PlayerPrefs.SetFloat("Exp", Player.Exp.CurrentVal);
        PlayerPrefs.SetFloat("Oxygen", Player.Oxygen.CurrentVal);
        PlayerPrefs.SetFloat("PlayerLevel", Player.PlayerLevel.CurrentVal);

        Debug.Log("Saved");


    }

    public void Load()
    {
        float x = PlayerPrefs.GetFloat("X");
        float y = PlayerPrefs.GetFloat("Y");
        float z = PlayerPrefs.GetFloat("Z");

        Player.Health.CurrentVal = PlayerPrefs.GetFloat("Health");
        Player.Exp.CurrentVal = PlayerPrefs.GetFloat("Exp");
        Player.Oxygen.CurrentVal = PlayerPrefs.GetFloat("Oxygen");
        Player.PlayerLevel.CurrentVal = PlayerPrefs.GetFloat("PlayerLevel");

        transform.position = new Vector3(x, y, z);

        Debug.Log("Loaded");
    }
}