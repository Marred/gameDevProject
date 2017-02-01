using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	//plik chyba nieuzywany. wywalic?
    /*
	public void SetScene () {

        SceneManager.LoadScene(1);

    } 
    public void loadGame()
    {
        if (!File.Exists(Application.persistentDataPath + "/playerInfo.dat")) return;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        PlayerData data = (PlayerData)bf.Deserialize(file);
        file.Close();
        SceneManager.LoadScene(data.scena);
    }
	public void Quit()
	{
		Application.Quit();
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
        public string scena;
    }
*/

}
