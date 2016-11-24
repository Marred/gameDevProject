using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextSizeOptions : MonoBehaviour
{
    
    private System.Random damageRange = new System.Random();
    private Text dmgText;
  

    void Start ()
    {
        dmgText = GetComponent<Text>();

         // mnożenie przez wysokość ekranu w celu kalibracji rozmiaru czcionki
        dmgText.fontSize = damageRange.Next(3, 4)*Screen.height/65;  
	}
    void Update()
    {

    }


}
