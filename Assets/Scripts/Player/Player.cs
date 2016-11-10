using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	[SerializeField] private Stat health; //Aktualna wartość: health.CurrentVal. Maksymalna wartość: health.MaxVal
	[SerializeField] private Stat exp;
	[SerializeField] private Stat oxygen;
	[SerializeField] private Stat playerLevel;
	[SerializeField] private Text playerLevelText;

	void Awake() {
		//Ustawienie wartości z inspektora
		health.Initialize ();
		exp.Initialize ();
		oxygen.Initialize ();
		playerLevel.Initialize ();

		//Wstępnie. System doświadczenia. Dla każdego levela ilość wymaganego doświadczenia wzrasta 5-krotnie
		exp.MaxVal = playerLevel.CurrentVal * 5;
		exp.CurrentVal = 0; //zmienic gdyby wprowadzone save'y
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Testowe odejmowanie życia
		/*if (Input.GetKeyDown (KeyCode.Q)) {
			health.CurrentVal -= 10;
		}*/
	
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
		playerLevel.CurrentVal += 1;
		exp.MaxVal = playerLevel.CurrentVal * 5;
		exp.CurrentVal = 0;

		playerLevelText.text = playerLevel.CurrentVal.ToString() + " lvl";
		//strenght += playerLevel;
		//stamina += playerLevel;


	}
}
