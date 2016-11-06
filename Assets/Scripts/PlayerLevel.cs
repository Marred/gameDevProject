using UnityEngine;
using System.Collections;
using UnityEngine.UI; //!
public class PlayerLevel : MonoBehaviour {
    private int expierience;
    private int playerLevel = 1;
    private int expNeeded;
    //statystyki
    private int stamina;
    private int strenght;
    //wyswietlanie
    public Text levelText;
    
    
	void Start ()
    {
        
        playerLevel = 1;
        expierience = 0;
        expNeeded = 5 * playerLevel;
        SetCountText();

	}
    // obiekty do zbierania muszą być rigidbody w celu optymalizacji - należy wówczas zaznaczyć i is kinematic useGravity
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
        expierience++;
        if (expNeeded == expierience)
        {
            expierience = 0;
            LevelUp();
        }
        expNeeded = 5 * playerLevel;

        strenght += playerLevel;
        stamina += playerLevel;
        SetCountText(); //przypisanie do tekstu po aktualizacji
    }
    void LevelUp()
    {
        playerLevel++;

    }
    void SetCountText()
    {
        levelText.text = "Level: " + playerLevel.ToString() +"   Experience: "+expierience.ToString()+ " / " + expNeeded.ToString(); //przypisanie do tekstu poziomu bohatera
        
    }
}
