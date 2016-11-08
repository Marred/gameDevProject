using UnityEngine;
using System.Collections;
using System;

[Serializable] //to samo co SerializeField, tylko SerializeField nie dziala po usunieciu MonoBehavior
public class Stat
{
	[SerializeField] private BarPrototype bar;
	[SerializeField] private float maxVal;
	[SerializeField] private float currentVal;

	//pozwala na jednoczesne ustawienie paska w healthbarze itp itd
	public float CurrentVal 
	{
		get 
		{
			return currentVal;
		}
		set 
		{
			this.currentVal = Mathf.Clamp (value, 0, MaxVal);
			if( bar != null )
				bar.Value = this.currentVal;
		}
	}

	//pozwala na jednoczesne ustawienie maxVal w healthbarze itp itd
	public float MaxVal
	{
		get 
		{
			return maxVal;
		}
		set 
		{
			this.maxVal = value;
			if( bar != null )
				bar.MaxValue = maxVal;
		}
	}

	//po ustaleniu wartosci w inspektorze, trzeba wywolac powyzsze funkcje
	public void Initialize()
	{
		this.MaxVal = maxVal;
		this.CurrentVal = currentVal;
	}
}
