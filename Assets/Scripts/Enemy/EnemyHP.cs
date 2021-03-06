﻿using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
	// DELEGAT ŚMIERCI PRZECIWNIKA
	public delegate void Death( GameObject enemy );
	Death myDeath;
    [SerializeField]private bool isBoss;
    // ENEMY TRANSFORM
    private Transform enemyTransform;

	// DAMAGE
	[SerializeField]private GameObject dmgtext;
	[SerializeField]public Stat enemyHealth; //edited
    // DMG TEXT KOREKTA
    private Vector3 polozenie = new Vector3(0, 0, -1); 

	// DROP
	private Vector3 shift = new Vector3(0.5f, 0, 0);

	[SerializeField]GameObject expOrb;
	[SerializeField]GameObject oxygenOrb;
	[SerializeField]GameObject healthOrb;


	private Player player;


	void Awake(){
		enemyHealth.Initialize();
	}

	void Start()
	{
		player = GameObject.FindWithTag ("Player").GetComponent<Player> ();

		//dodanie delegatów
		myDeath += DropItems;
		myDeath += player.enemyDeath;
		myDeath += Destroy;

		enemyTransform = GetComponent<Transform>();
	}


	void Damage(float dmg)
	{
		GameObject canvas = GameObject.Find("EnemyCanvas");
		GameObject ObjTextDmg = Instantiate(dmgtext, enemyTransform.position + polozenie, Quaternion.identity) as GameObject;

		//przyporządkowanie naszego tekstu dla pobranego canvasa
		ObjTextDmg.transform.SetParent(canvas.transform); 

		//wyslanie wiadomości do naszego obiektu(tekst) z nazwa funkcji do wykonania oraz wartoscia obrazen
		ObjTextDmg.SendMessage("SetTextDmg", dmg.ToString());
        Debug.Log(enemyHealth.CurrentVal);
		//zadawanie obrazen
		enemyHealth.CurrentVal -= dmg;

		if (enemyHealth.CurrentVal <= 0 && myDeath != null &&!isBoss) {
			myDeath (this.gameObject);
		}
	}

	// Wywoływane przez delegat Death
	void DropItems( GameObject enemy ){
		int randomChance = Random.Range(0, 101);
		randomChance -= (int)player.dropSkill.CurrentVal * 5;

		// 50% szans
		if(randomChance<50)
		{
			Instantiate(expOrb, transform.position, Quaternion.identity);
		}
		// 35% szans
		else if(randomChance<35)
		{
			Instantiate(expOrb, transform.position+shift, Quaternion.identity);
			Instantiate(oxygenOrb, transform.position-shift, Quaternion.identity);
		}
		// 25% szans
		else if(randomChance<25)
		{
			Instantiate(expOrb, transform.position+shift, Quaternion.identity);
			Instantiate(healthOrb, transform.position-shift, Quaternion.identity);
		}

	}
}
