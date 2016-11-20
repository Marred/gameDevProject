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

        PlayerPrefs.SetFloat("Health", player.Health.CurrentVal);
        PlayerPrefs.SetFloat("Exp", player.Exp.CurrentVal);
        PlayerPrefs.SetFloat("Oxygen", player.Oxygen.CurrentVal);
        PlayerPrefs.SetFloat("PlayerLevel", player.PlayerLevel.CurrentVal);

        Debug.Log("Saved");


    }

    public void Load()
    {
        float x = PlayerPrefs.GetFloat("X");
        float y = PlayerPrefs.GetFloat("Y");
        float z = PlayerPrefs.GetFloat("Z");

        player.Health.CurrentVal = PlayerPrefs.GetFloat("Health");
        player.Exp.CurrentVal = PlayerPrefs.GetFloat("Exp");
        player.Oxygen.CurrentVal = PlayerPrefs.GetFloat("Oxygen");
        player.PlayerLevel.CurrentVal = PlayerPrefs.GetFloat("PlayerLevel");

        transform.position = new Vector3(x, y, z);

        Debug.Log("Loaded");
    }
}