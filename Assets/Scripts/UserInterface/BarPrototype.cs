using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Klasa obsługująca paski statystyk typu Filled i Sliced.
/// </summary>
public class BarPrototype : MonoBehaviour 
{
	//Wypełnienie paska od 0 do 1
	private float fillAmount;

	[SerializeField] private float lerpSpeed; //szybkosc uzupelniania

	//Pasek -> Sprite
	[SerializeField] private Image content;

	//Tekst -> Sprite
	[SerializeField] private Text valueText;

	/// <summary>
	/// <para>Umożliwia przejście z koloru do koloru.</para>
	/// <para>Przykład: pasek życia przechodzi z zielonego do czerwonego wraz ze zmniejszeniem wartości</para>
	/// </summary>
	[SerializeField] private bool lerpColors;
	[SerializeField] private Color fullColor;
	[SerializeField] private Color lowColor;


	public float MaxValue { get; set; }

	public float Value 
	{
		set 
		{
			if( valueText != null ) valueText.text = value.ToString();

			fillAmount = Map (value, 0, MaxValue, 0, 1);
		}
	}
	// Use this for initialization
	void Start () {
		if (lerpColors) {
			content.color = fullColor;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleBar ();
	}


	private void HandleBar()
	{
		float curSize = 0;
		// Zwraca typ elementu. Ponizej uzylem tego do odroznienia paskow (Sliced) i okregow (Filled)
		string typeOfElem = content.type.ToString ();

		if ( typeOfElem == "Sliced")		 curSize = content.rectTransform.localScale.x; //dla Sliced X-em jest skala.x
		else if ( typeOfElem == "Filled") 	 curSize = content.fillAmount; //dla Filled X-em jest fillAmount

		//Update tylko, gdy tego trzeba. Nie nadwrężajmy procesora
		if (fillAmount != curSize)
		{
			//Lerp dzieli ułamkami odleglosc miedzy dwoma wartosciami
			float newSize = Mathf.Lerp (curSize, fillAmount, Time.deltaTime * lerpSpeed);

			if (typeOfElem == "Sliced")
				content.rectTransform.localScale = new Vector3 (newSize, 1, 1); //Trzeba uzyc wektora, by zmienic skale
			else if (typeOfElem == "Filled")
				content.fillAmount = newSize;

		}	
		//jesli zaznaczy sie bool lerpColors
		if( lerpColors )
			content.color = Color.Lerp (lowColor, fullColor, fillAmount); //Funkcja ktora bierze 'srodkowa' (fillAmount) miedzy dwoma kolorami
	}


	/// <summary>
	/// Skaluje wartości do wybranej. Algorytm nieautorski.
	/// </summary>
	/// <param name="value">Aktualna wartość</param>
	/// <param name="inMin">Minimalna wartość</param>
	/// <param name="inMax">Maksymalna wartość</param>
	/// <param name="outMin">Wyjściowa minimalna</param>
	/// <param name="outMax">Wyjściowa maksymalna</param>
	private float Map( float value, float inMin, float inMax, float outMin, float outMax )
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
		/*  PRZYKŁAD obliczania skali dla dowolnych wartości
			Max Health: 	100
			Current Health: 80
			Min Health:		0
			Skala Życia od 0 do 1

			(Current Health - Min Health) * (Skala max. - Skala min.) / (Max Health - Min Health) + Skala min.
			(80 - 0) * (1 - 0) / (100 - 0) + 0
			   80    *    1    /     100
			= 0.8


			Max Health: 	240
			Current Health: 78
			Min Health:		0
			Skala Życia od 0 do 1

			(Current Health - Min Health) * (Skala max. - Skala min.) / (Max Health - Min Health) + Skala min.
			(78 - 0) * (1 - 0) / (240 - 0) + 0
			   78    *    1    /     240		= 0.325
			
		*/
	}
		
}
