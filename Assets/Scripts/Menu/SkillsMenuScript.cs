using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillsMenuScript : MonoBehaviour {
	// PLAYER
	public Player player;


	// SKILLPOINTS
	private skillPoint health	 = new skillPoint();
	private skillPoint oxygen 	 = new skillPoint();
	private skillPoint speed 	 = new skillPoint();
	private skillPoint strength	 = new skillPoint();
	private skillPoint drop 	 = new skillPoint();

	// Dynamiczne pobranie AudioSource (jest też tworzone dynamicznie, więc nie można bezposrednio przypisać
	private AudioSource addSkillSndSource { get { return GetComponent<AudioSource> (); } }
	// Klip audio odgrywany podczas kliknięcia przycisku PLUS
	[SerializeField] private AudioClip addSkillSnd;

	// Tablica klas umiejętności
	private skillPoint[] skillPoints;

	private Text skillPointsText;
	private Text healthSkill;


	// STATISTICS
	private Text timePlayed;
	private Text enemiesKilled;
	private Text pickedExp;
	private Text exp;
	private Text deaths;


	//nie Start, bo start uruchamia sie dopiero gdy component jest wlaczony
	void Awake(){
		gameObject.AddComponent<AudioSource> ();
		addSkillSndSource.clip = addSkillSnd;
		addSkillSndSource.playOnAwake = false;
		addSkillSndSource.volume = 0.8f;

		//player = GameObject.Find ("Player").GetComponent<Player>();

		health.skill = player.healthSkill;
		health.skillName = "health"; //nazwy przydadza sie do zapisywania(?)

		oxygen.skill = player.oxygenSkill;
		oxygen.skillName = "oxygen";

		speed.skill  = player.speedSkill;
		speed.skillName = "speed";

		strength.skill = player.strengthSkill;
		strength.skillName = "strength";

		drop.skill = player.dropSkill;
		drop.skillName = "drop";


		skillPoints = new skillPoint[]{ health, oxygen, speed, strength, drop };

		//menu jest inactive na starcie, wiec trzeba tym sposobem wyciagnac elementy
		Text[] statsTexts = GetComponentsInChildren<Text>( true );

		foreach (Text statsText in statsTexts) {
			if (statsText.gameObject.name != "Value")
				continue;

			switch (statsText.transform.parent.name) 
			{
			case "Skillpoints":		skillPointsText = statsText; break;

				//statistics
			case "TimePlayed": 		timePlayed 		= statsText; break;
			case "EnemiesKilled": 	enemiesKilled 	= statsText; break;
			case "Exp": 			exp 			= statsText; break;	
			case "Deaths": 			deaths			= statsText; break;	
			case "PickedExp": 		pickedExp 		= statsText; break;	

				//skillpoints
			case "HealthSkill":		health.skillPointText 	= statsText; break;
			case "OxygenSkill":		oxygen.skillPointText 	= statsText; break;
			case "SpeedSkill":		speed.skillPointText	= statsText; break;
			case "StrengthSkill":	strength.skillPointText	= statsText; break;
			case "DropSkill":		drop.skillPointText	= statsText; break;

			}
		}

		Button[] buttonsArr = GetComponentsInChildren<Button>( true );
		foreach (Button skillButton in buttonsArr) {
			if (skillButton.gameObject.name != "AddButton")
				continue;

			switch (skillButton.transform.parent.name) {
			case "HealthSkill":
				health.skillPointButton = skillButton;
				skillButton.onClick.AddListener (() => addSkillPoint( health ));
				break;
			case "OxygenSkill":
				oxygen.skillPointButton = skillButton;
				skillButton.onClick.AddListener (() => addSkillPoint( oxygen ));
				break;
			case "SpeedSkill":
				speed.skillPointButton = skillButton;
				skillButton.onClick.AddListener (() => addSkillPoint( speed ));
				break;
			case "StrengthSkill":
				strength.skillPointButton = skillButton;
				skillButton.onClick.AddListener (() => addSkillPoint( strength ));
				break;
			case "DropSkill":
				drop.skillPointButton = skillButton;
				skillButton.onClick.AddListener (() => addSkillPoint( drop ));
				break;
			}
		}
	}

	//nie dodaje tego do klasy, bo musze wiedziec ile zostalo skillPointow i w razie > 1, wszystkie musza zostac wlaczone
	void addSkillPoint( skillPoint chosenSkill ){
		addSkillSndSource.PlayOneShot (addSkillSnd);
		player.skillPoints--;

		chosenSkill.skill.CurrentVal++;
		chosenSkill.updateText ();

		switch (chosenSkill.skillName) {
			case "health":
				player.health.MaxVal = 100 + (chosenSkill.skill.CurrentVal * 20);
				break;
			case "oxygen":
				player.oxygen.MaxVal = 100 + (chosenSkill.skill.CurrentVal * 20);
				break;
		}


		if (player.skillPoints == 0)
			disableSkillButtons ();
		else if (chosenSkill.skill.CurrentVal == chosenSkill.skill.MaxVal)
			chosenSkill.skillPointButton.interactable = false;

		updateSkillTexts ();

	}


	void disableSkillButtons(){
		foreach (skillPoint sk in skillPoints) {
			sk.skillPointButton.interactable = false;
		}
	}

	void enableSkillButtons(){
		foreach (skillPoint sk in skillPoints) {
			if (sk.skill.CurrentVal != sk.skill.MaxVal)
				sk.skillPointButton.interactable = true;
		}
	}

	//inicjalizacja tekstow z inspektora do menu
	void updateSkillTexts(){
		skillPointsText.text = string.Format( "Upgrade: Spend Skillpoints ({0})", player.skillPoints.ToString() );

		foreach (skillPoint sk in skillPoints)
			sk.updateText ();
	}

	void OnEnable(){
		//update curval/maxval text
		updateSkillTexts ();

		if (player.skillPoints == 0) disableSkillButtons ();
		else enableSkillButtons ();

		//	CZAS
		System.TimeSpan t = System.TimeSpan.FromSeconds (player.playedTime);
		timePlayed.text = string.Format ("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);

		//  EXP NEEDED
		exp.text = string.Format("{0}/{1}", player.exp.CurrentVal.ToString(), player.exp.MaxVal.ToString() );

		//  EXP ORBS
		pickedExp.text = player.expOrbsPicked.ToString();

		//  ENEMIES KILLED
		enemiesKilled.text = player.enemiesKilled.ToString();

		//  DEATHS
		deaths.text = player.deaths.ToString();
	}


}



public class skillPoint{
	public Text skillPointText;
	public Button skillPointButton;
	public Stat skill;
	public string skillName;

	/*public void disableButton(){
		this.skillPointButton.interactable = false;
	}

	public void enableButton(){
		this.skillPointButton.interactable = skill.CurrentVal == skill.MaxVal ? false : true;
	}*/

	public void updateText(){
		this.skillPointText.text = this.skill.CurrentVal + "/" + this.skill.MaxVal;

	}
}