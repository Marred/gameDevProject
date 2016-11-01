using UnityEngine;
using System.Collections;

//DODANE
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	public Image myHealthBar;
	public Text myHealthPoints;

	private float hitPoint = 150; //HP na start
	private float maxHitPoint = 150; //maksymalne HP

	private void Start()
	{
		UpdateHealthBar ();
	}

	private void UpdateHealthBar()
	{
		float ratio = hitPoint / maxHitPoint;
		//skalowanie paska zycia
		myHealthBar.rectTransform.localScale = new Vector3 (ratio, 1, 1);
		myHealthPoints.text = hitPoint.ToString();

	}


	//do napisania
	private void TakeDamage( float damage )
	{


	}
}
