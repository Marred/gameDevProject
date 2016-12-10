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
        data.experienceMaxVal = player.exp.MaxVal;//
        data.healthSkill = player.healthSkill.CurrentVal;
        data.oxygenSkill = player.oxygenSkill.CurrentVal;
        data.speedSkill = player.speedSkill.CurrentVal;
        data.strengthSkill = player.strengthSkill.CurrentVal;
        data.dropSkill = player.dropSkill.CurrentVal;
        data.skillPoints = player.skillPoints;
        data.playedTime = player.playedTime;
        data.expOrbsPicked = player.expOrbsPicked;
        data.enemiesKilled = player.enemiesKilled;
        data.deaths = player.deaths;
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
			player.playerLevel.CurrentVal = data.playerLevel;
            player.exp.CurrentVal = data.experience;
			//poprawka kaje:
			player.exp.MaxVal = player.playerLevel.CurrentVal * 5;  
			player.playerLevelText.text = player.playerLevel.CurrentVal.ToString() + " lvl";

            player.transform.position = new Vector3(data.x, data.y, data.z);

            player.oxygen.CurrentVal = data.oxygen;
            player.laserUpgrade.CurrentVal = data.upgradeLvl;
            player.exp.MaxVal = data.experienceMaxVal;
            //
            player.healthSkill.CurrentVal = data.healthSkill;
            player.oxygenSkill.CurrentVal = data.oxygenSkill;
            player.speedSkill.CurrentVal = data.speedSkill;
            player.strengthSkill.CurrentVal = data.strengthSkill;
            player.dropSkill.CurrentVal = data.dropSkill;
            player.skillPoints = data.skillPoints;
            player.playedTime = data.playedTime;
            player.expOrbsPicked = data.expOrbsPicked;
            player.enemiesKilled = data.enemiesKilled;
            player.deaths = data.deaths;
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
        public float experienceMaxVal;//
        public float healthSkill;
        public int skillPoints;
        public float oxygenSkill;
        public float speedSkill;
        public float strengthSkill;
        public float dropSkill;
        public float playedTime;
        public int expOrbsPicked;
        public int enemiesKilled;
        public int deaths;
    }

}
