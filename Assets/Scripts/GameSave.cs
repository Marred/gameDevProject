using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
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
    Scene scene;
    bool beingLoaded = false;
    public bool controler = true;
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name != "mainMenu") { player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); }
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name != "mainMenu")
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (!beingLoaded && player != null && player.health != null )
            {
                beingLoaded = true;
                PlayerData save = readFile();
                if(save != null && save.beingLoaded )
                    Load();
            }
        }

        if (Input.GetKey(KeyCode.F5))
        {
            Save();
        }
        if (Input.GetKey(KeyCode.F9))
        {
            Load();
        }
        //  if (beingLoaded) { Load(); }
    }


    private void saveFile( PlayerData save )
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        bf.Serialize(file, save);
        file.Close();
    }
	public void Save( bool loadOnNewScene=false )
    {
        Debug.Log("zapis na scenie o nazwie: " + scene.name);
        PlayerData data = new PlayerData();
        data.beingLoaded = loadOnNewScene;
        data.scena = scene.name;
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
        saveFile(data);
    }
    private PlayerData readFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/playerInfo.dat")) return null;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        PlayerData data = (PlayerData)bf.Deserialize(file);
        file.Close();
        return data;
    }
    // bool loadScene = false
    public void LoadScene()
    {
        PlayerData data = readFile();
        if (data == null) return;

        data.beingLoaded = true;
        saveFile(data);
        SceneManager.LoadScene(data.scena);
    }

    public void Load()
    {
        PlayerData data = readFile();
        if (data == null) return;

        player.health.CurrentVal = data.health;
        player.playerLevel.CurrentVal = data.playerLevel;
        player.exp.CurrentVal = data.experience;
        //poprawka kaje:
        player.exp.MaxVal = player.playerLevel.CurrentVal * 5;
        player.playerLevelText.text = player.playerLevel.CurrentVal.ToString() + " lvl";
        if (data.scena == scene.name) {
            player.transform.position = new Vector3(data.x, data.y, data.z);
        }


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
        Save();
		Time.timeScale = 1;
    }
    [Serializable]
    class PlayerData
    {
        public bool beingLoaded;
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
        public string scena;
    }

}
