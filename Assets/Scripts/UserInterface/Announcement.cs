using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

/// <summary>
/// <para>Announcement.</para>
/// <para></para>
/// <para>Klasa obsługująca animowany tekst.</para>
/// <para>Używa się jej przez stworzenie obiektu z Prefaba i wywołaniu funkcji setText</para>
/// </summary>
public class Announcement : MonoBehaviour {
	private int frequency = 2; // Co ile klatek zmieniać znak.
	private int curFrame;

	public static int step = 8; // Ile razy znak ma być zmieniony
	private int curStep; // Tymczasowa zmienna

	private bool isFinished = false; // Czy zakończono animację tekstu
	private float finishTimer; // Liczy do 2 sekund - możliwość dodania konfiguracji

	private string toSet; // Łańcuch znaków przypisywany do komponentu Text

	// Tablice z klasami - wyjaśnienie w klasach
	private CharAnim[] text;
	private CharType[] Types;

	private int[] removeRandom; // Tablica zawierająca losowo ułożone indeksy zmiennej CharAnim[] Text
	private int removeCur = 0;

	// Inspektor
	private Text textcmp { get { return GetComponent<Text> (); } }
	private AudioSource source { get { return GetComponent<AudioSource> (); } }
	[SerializeField] private AudioClip BlipSound; //Dzwięk odgrywany przy zmianie znaku
	[SerializeField] private AudioClip DeleteSound; //Dźwięk przy usuwaniu tekstu
	private bool deletePlayed = false; // Czy odegrano powyższy dźwięk

	// Zbiór reguł do wykrywania znaków i generowania losowego znaku dla danego zakresu
	private CharType Space 		= new CharType( " ", " " );
	private CharType Dot 		= new CharType( @"\.", "." );
	private CharType LowerCase 	= new CharType( "[a-zżźćńółęąś]", "qwertyuiopasdfghjklzxcvbnm0123456789" );
	private CharType UpperCase 	= new CharType( "[A-ZŻŹĆŃÓŁĘĄŚ]", "QWERTYUIOPASDFGHJKLZXCVBNM0123456789" );
	private CharType Number 	= new CharType( "[0-9]", "0123456789" );
	private CharType Symbol 	= new CharType( @"$-/:-?{-~!\""^-'\\[\\]]", "!$%^&*()_+|~-=`{}[]:\";'<>?,./" );

	void Awake(){
		// Przypisanie reguł do klasy Types (nie działa przy deklaracji)
		Types = new CharType[]{ Space, Dot, LowerCase, UpperCase, Number, Symbol };
	}

	/// <summary>
	/// Rozpoczyna animację.
	/// </summary>
	/// <param name="ann">Tekst</param>
	/// <param name="fontSize">Rozmiar czcionki</param>
	/// <param name="align">Ułożenie.</param>
	public void setText( string ann, int fontSize = 35, TextAnchor align = TextAnchor.MiddleLeft ){
		if (text != null) return; // Jeżeli ktoś błędnie użyje dwa razy setText na tym samym elemencie, to return
		
		textcmp.fontSize = fontSize;
		textcmp.alignment = align;
		//textcmp.rectTransform - później

		// Przekonwertowanie string do CharAnim[]
		text = new CharAnim[ ann.Length ];
		for (int i = 0; i < ann.Length; i++) {
			CharType type = Symbol;

			foreach (CharType t in Types)
			{
				if (t.isType (ann [i])) {
					type = t;
					break;
				}
			}
			
			text [i] = new CharAnim (ann [i], type);
		}

		// Stworzenie tablicy zawierająca losowo ułożone indeksy zmiennej CharAnim[] Text
		removeRandom = new int[ text.Length ];
		for (int i = 0; i < text.Length; i++) {
			removeRandom [i] = i;
		}
		// LINQ -> miesza tablicę
		removeRandom = removeRandom.OrderBy (
			x => Random.Range (0,text.Length)
		).ToArray ();
	}
	void FixedUpdate () {
		/*if (Input.GetKeyDown (KeyCode.V)){
			setText ("No elo co tam?", 35, TextAnchor.MiddleCenter);
			Debug.Log("tst");
		}*/

		// Jeśli zainicjowano obiekt, ale nie ustawiono tekstu, to wróć.
		if (text == null)
			return;

		// Jeśli skończono animację tekstu, to zacznij procedurę usuwania.
		if (isFinished) {
			EmptyText ();
			return;
		}

		// Iteracja klatek
		curFrame++;
		if (curFrame % frequency != 0 ) return;
	
		toSet = "";

		// Algorytm zmieniania tablicy ze znakami na losowe
		for (int i = 0; i < text.Length; i++) {
			// Jeśli to 1 znak, to kontynuuj; jeśli nie i zmieniono poprzedni znak mniej niż połowę stepu, to skończ pętlę
			if (i != 0 && text [i - 1].step < (int)(Announcement.step/2))
				break;
			
			text [i].changeChar ();

			// Dodaj zmieniony znak do stringa toSet
			toSet += text[ i ].curChar;
		}

		source.PlayOneShot (BlipSound);
		textcmp.text = toSet;

		// Jeśli ostatni znak przekroczył ilość zmian, to zakończ animację.
		if (text [text.Length - 1].step == Announcement.step+1) {
			isFinished = true;

		}
	}
	/// <summary>
	/// Usuwa losowo wszystkie znaki z łańcucha i usuwa obiekt
	/// </summary>
	void EmptyText(){
		// Zostaw tekst na 2 sekundy
		finishTimer += Time.deltaTime;
		if (finishTimer < 2) {
			return;
		}

		// Jeśli nie zagrano dźwięku, to zagraj
		if (!deletePlayed) {
			source.PlayOneShot (DeleteSound);
			deletePlayed = true;
		}

		//Jeśli usunięto wszystkie znaki, to zniszcz obiekt
		if (removeCur == text.Length) {
			
			Destroy (this.gameObject);
			return; //FIX! 2017-01-30
		}
		//Debug.Log (removeCur + "i " + text.Length);
		// Usuwaj znaki co daną ilość klatek - hardcoded
		curFrame++;
		if (curFrame % (10-(text.Length/3)) != 0) // Modulo z prymitywnym wzorem odjecia 3 czesci dlugosci tekstu od liczby 10.
			return;

		// Ustawienie losowego znaku w tablicy jako pusty
		//if( removeRandom [removeCur] <= text.Length ) //poprawa bledu, ktory wywalal przy wylaczeniu mapy (?)
		//Debug.Log("lol"); - blad powoduje wyrkzyknik
			text [removeRandom [removeCur]].curChar = ' '; // Działa, ale czy to najefektywniejsze rozwiązanie?
		
		removeCur++;

		// Tablica -> łańcuch znaków
		toSet = "";
		for (int i = 0; i < text.Length; i++) {
			toSet += text[ i ].curChar;
		}

		textcmp.text = toSet;
	}
}
/// <summary>
/// Klasa rezprezentująca każdy znak osobno z animacji. Ściśle związana z klasą Announcement.
/// </summary>
public class CharAnim {
	public int step = 0;
	public char curChar;
	public char goalChar;
	public CharType type;

	/// <summary>
	/// Initializes a new instance of the <see cref="CharAnim"/> class.
	/// </summary>
	/// <param name="ch">Znak</param>
	/// <param name="t">Rodzaj znaku</param>
	public CharAnim( char ch, CharType t ){
		this.goalChar = ch;
		this.type = t;
	}

	/// <summary>
	/// Changes the char.
	/// </summary>
	public void changeChar(){
		if (step > Announcement.step)
			return;

		if (Announcement.step != step)
			curChar = type.getRandomChar ();
		else
			curChar = goalChar;			

		step++;
	}

}
/// <summary>
/// Zbiór reguł do wykrywania znaków i generowania losowego znaku dla danego zakresu 
/// </summary>
public class CharType {
	private Regex reg;
	private string chars; 

	/// <summary>
	/// Initializes a new instance of the <see cref="CharType"/> class.
	/// </summary>
	/// <param name="reg">Reguła REGEX</param>
	/// <param name="chars">Zbiór znaków pasujących do reguły</param>
	public CharType( string reg=@"", string chars="" ){
		this.reg = new Regex( reg );
		this.chars = chars;
	}

	/// <summary>
	/// Ises the type.
	/// </summary>
	/// <returns><c>true</c>, jeśli reguła pasuje, <c>false</c> jeśli nie.</returns>
	/// <param name="c">C.</param>
	public bool isType( char c ){
		return reg.IsMatch (c.ToString());
	}

	/// <summary>
	/// Gets the random char.
	/// </summary>
	/// <returns>The random char.</returns>
	public char getRandomChar()
	{
		int index = Random.Range(0, chars.Length);
		//Debug.Log ("rnd: " + chars [index]);
		return chars[index];
	}
}