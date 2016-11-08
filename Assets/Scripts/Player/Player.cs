using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	[SerializeField] private Stat health;
	[SerializeField] private Stat exp;
	[SerializeField] private Stat oxygen;
	[SerializeField] private Stat playerLevel;
	[SerializeField] private Text playerLevelText;

	void Awake() {
		health.Initialize ();
		exp.Initialize ();
		oxygen.Initialize ();
		playerLevel.Initialize ();

		exp.MaxVal = playerLevel.CurrentVal * 5;
		exp.CurrentVal = 0; //zmienic gdyby wprowadzone save'y
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			health.CurrentVal -= 10;
		}
	
	}


	void OnTriggerEnter(Collider other) // funkcja do wykrywania wejscia na triggera
	{
		if (other.gameObject.CompareTag("ExpOrb"))  //jeśli napotkany trigger ma tag "ExpOrb"
		{
			Destroy(other.gameObject);
			//deaktywujemy objekt który napotkamy

			ExpUp();
		}
	}

	void ExpUp()
	{
		exp.CurrentVal++;
		if (exp.MaxVal == exp.CurrentVal)
			LevelUp();


	}
	void LevelUp()
	{
		playerLevel.CurrentVal += 1;
		exp.MaxVal = playerLevel.CurrentVal * 5;
		exp.CurrentVal = 0;

		playerLevelText.text = playerLevel.CurrentVal.ToString() + " lvl";
		//strenght += playerLevel;
		//stamina += playerLevel;


	}
}
