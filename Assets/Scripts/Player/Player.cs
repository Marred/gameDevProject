using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	//  PLAYER VARIABLES
	[SerializeField]public Stat health; //Aktualna wartość: health.CurrentVal. Maksymalna wartość: health.MaxVal
	[SerializeField]public Stat exp;
	[SerializeField]public Stat oxygen;
	[SerializeField]public Stat playerLevel;
    [SerializeField]public Stat laserUpgrade;

    //  SKILLPOINTS
    [SerializeField]public Stat healthSkill;
	[SerializeField]public Stat oxygenSkill;
	[SerializeField]public Stat speedSkill;
	[SerializeField]public Stat strengthSkill;
	[SerializeField]public Stat dropSkill;

    //  STATISTICS
    public int skillPoints;
	public float playedTime;
	public int expOrbsPicked;
	public int enemiesKilled;
	public int deaths;

	public GameObject announcePrefab;

    //animacje lvlup  postać musi zawierac prefab canvas "PlayerCanv(LvlUp)"
    [SerializeField]private GameObject lvlupAnimation; //przypisac prefab o tej samej nazwie
    [SerializeField]private GameObject lvlupAnimationText; // przypisac prefab "LvlUpText"
    [SerializeField]private Canvas myCanvas; //PlayerCanv(LvlUp) -  musi być dodany do gracza!

    [SerializeField]public Text playerLevelText;

	//  OXYGEN SETTING
	[SerializeField]private float oxygenDelay = 3f;

    private bool isCollide = false;
    
    void Awake() {
		healthSkill.Initialize ();
		oxygenSkill.Initialize ();
		speedSkill.Initialize ();
		strengthSkill.Initialize ();
		dropSkill.Initialize ();
        laserUpgrade.Initialize();

		//Ustawienie wartości z inspektora
		health.Initialize ();
		exp.Initialize ();
		oxygen.Initialize ();
		playerLevel.Initialize ();

		//Wstępnie. System doświadczenia. Dla każdego levela ilość wymaganego doświadczenia wzrasta 5-krotnie
		exp.MaxVal = playerLevel.CurrentVal * 5;  
	}
	
	void Start () {
        StartCoroutine(OxygenOut());
		makeAnnouncement ("side", "Welcome to our planet");
    }

 
    void Update()
    {
        isCollide = false;

		playedTime += Time.deltaTime;
        if (health.CurrentVal <= 0 && oxygen.CurrentVal == 0)
        {
            StopAllCoroutines();
        }


    }
    IEnumerator OxygenOut()
    {

        while (true)
        {
            yield return new WaitForSeconds(oxygenDelay);
            if (oxygen.CurrentVal>5)
            {
                oxygen.CurrentVal -= 5;
            }
            
            else if (oxygen.CurrentVal<=5)
            {
                oxygen.CurrentVal = 0;
                if (oxygen.CurrentVal==0)
                {
                    health.CurrentVal -= 5;
                }
            }
          
        }
        
    }

	public void enemyDeath( GameObject enemy )
	{
		//Debug.Log ("called enemydeath in enemyplayer");
		enemiesKilled++;
	}

   void OnTriggerEnter(Collider other) 
	{

        if (isCollide) return;
        isCollide = true;
        if (other.gameObject.CompareTag("ExpOrb")) 
		{
			Destroy(other.gameObject);
            expOrbsPicked++;
			ExpUp();
		}
        else if(other.gameObject.CompareTag("OxygenOrb"))
        {
            if (oxygen.CurrentVal>=61f)
            {
                oxygen.CurrentVal = oxygen.MaxVal;
            }
            else
            {
                oxygen.CurrentVal += 40f;
            }
          
            Destroy(other.gameObject);

        }
        else if(other.gameObject.CompareTag("HPOrb"))
        {
            if (health.CurrentVal>=81f)
            {
                health.CurrentVal = health.MaxVal;
            }
            else
            {
                health.CurrentVal += 20;
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("LaserUpgrade"))
        {
            if (laserUpgrade.CurrentVal <= laserUpgrade.MaxVal)
            {
                laserUpgrade.CurrentVal++;
                Debug.Log(laserUpgrade.CurrentVal);
                
               
            }
            else
            {
                Debug.Log("Poziom laseru max");
               
            }
           
            Destroy(other.gameObject);

        }
    }
	public void makeAnnouncement( string type, string announcement ){
		GameObject ann = Instantiate( announcePrefab ) as GameObject;
		Announcement annScript = ann.GetComponent<Announcement> ();
		ann.transform.SetParent(GameObject.Find("HUDCanvas").transform);
		ann.transform.localScale = new Vector2 (1f, 1f);
		switch (type) {
			case "general":
				ann.transform.localPosition = new Vector2 (0, 0);
				annScript.setText (announcement, 45, TextAnchor.MiddleCenter);
				break;
			case "side":
				ann.transform.localPosition = new Vector2 (60, -180);
				annScript.setText (announcement, 35, TextAnchor.MiddleLeft);	
				break;
		}
		//germSpawned.transform.localRotation = spawnRotation;
	}
	/// <summary>
	/// Dodaje doświadczenie.
	/// </summary>
	void ExpUp()
	{
		exp.CurrentVal++;
		if (exp.MaxVal == exp.CurrentVal)
			LevelUp();


	}
	/// <summary>
	/// Dodaje poziom graczowi.
	/// </summary>
	void LevelUp()
	{
		skillPoints++;

        //inicjalizacja animacji lvlUp
        GameObject lvlUpAnim = Instantiate(lvlupAnimation, transform.position, Quaternion.identity,this.transform) as GameObject;
        GameObject lvlupAnimaText = Instantiate(lvlupAnimationText, new Vector3(transform.position.x, transform.position.y, -1) ,Quaternion.identity, myCanvas.transform) as GameObject;
        oxygen.CurrentVal = oxygen.MaxVal;
        health.CurrentVal = health.MaxVal;
        playerLevel.CurrentVal += 1;
		exp.MaxVal = playerLevel.CurrentVal * 5;
		exp.CurrentVal = 0;

		playerLevelText.text = playerLevel.CurrentVal.ToString() + " lvl";
       
    }
}
