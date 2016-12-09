using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class GameSave : MonoBehaviour
{
    public static GameSave control;
    private Player player;
     float x;
     float y;
     float z;
     float health;
     float experience;
     float oxygen;
     float upgradeLvl;
     float playerLevel;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (control == null)
        {
            DontDestroyOnLoad(this);
            control = this;
        }
        else if(control !=this)
        {
            Destroy(gameObject);
        }
    }
   void Update()
    {
        if(Input.GetKey(KeyCode.F5))
        {
            Save();
        }
        if (Input.GetKey(KeyCode.F9))
        {
            Load();
        }
    }

    public void Save()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.health = player.health.CurrentVal;
        data.experience = player.exp.CurrentVal;
        data.x = player.transform.position.x;
        data.y = player.transform.position.y;
        data.z = player.transform.position.z;
        data.oxygen = player.oxygen.CurrentVal;
        data.upgradeLvl = player.laserUpgrade.CurrentVal;
        data.playerLevel = player.playerLevel.CurrentVal;
    bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            player.health.CurrentVal = data.health;
            player.exp.CurrentVal = data.experience;
            player.transform.position = new Vector3(data.x, data.y, data.z);
            player.playerLevel.CurrentVal = data.playerLevel;
            player.oxygen.CurrentVal = data.oxygen;
            player.laserUpgrade.CurrentVal = data.upgradeLvl;
        }
    }

    [Serializable]
    class PlayerData
    {
        public float playerLevel;
        public float x;
        public float y;
        public float z;
        public float health;
        public float experience;
        public float oxygen;
        public float upgradeLvl;
    }

}
