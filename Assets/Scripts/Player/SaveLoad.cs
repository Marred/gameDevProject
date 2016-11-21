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

        PlayerPrefs.SetFloat("Health", player.health.CurrentVal);
        PlayerPrefs.SetFloat("Exp", player.exp.CurrentVal);
        PlayerPrefs.SetFloat("Oxygen", player.oxygen.CurrentVal);
        PlayerPrefs.SetFloat("PlayerLevel", player.playerLevel.CurrentVal);

        Debug.Log("Saved");


    }

    public void Load()
    {
        float x = PlayerPrefs.GetFloat("X");
        float y = PlayerPrefs.GetFloat("Y");
        float z = PlayerPrefs.GetFloat("Z");

        player.health.CurrentVal = PlayerPrefs.GetFloat("Health");
        player.exp.CurrentVal = PlayerPrefs.GetFloat("Exp");
        player.oxygen.CurrentVal = PlayerPrefs.GetFloat("Oxygen");
        player.playerLevel.CurrentVal = PlayerPrefs.GetFloat("PlayerLevel");

        transform.position = new Vector3(x, y, z);

        Debug.Log("Loaded");
    }
}