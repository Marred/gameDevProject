using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Klasa pozwalająca na uniwersalne ustawienie dowolnych statystyk
/// <para>Funkcje to m.in.</para>
/// <para>- Ograniczenie wartości od 0 do MaxVal</para>
/// <para>- Kompatybilność z <c>BarPrototype</c></para>
/// <para>- Łatwa konfiguracja w Inspektorze</para>
/// </summary>
[Serializable] //to samo co SerializeField, tylko SerializeField nie dziala po usunieciu MonoBehavior
public class Stat
{
	[SerializeField] private BarPrototype bar;
	[SerializeField] private float maxVal;
	[SerializeField] private float currentVal;

	/// <summary>
	/// Pobiera lub ustawia wartość danej statystyki przy czym ustawia wartość paska, jeśli istnieje
	/// </summary>
	/// <value>The current value.</value>
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

	/// <summary>
	/// Pobiera lub ustawia maksymalną wartość dla danej statystyki, przy czym przesyła wartość do paska.
	/// </summary>
	/// <value>The max value.</value>
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

	/// <summary>
	/// Inicjalizuje wartości ustawione w inspektorze.
	/// </summary>
	public void Initialize()
	{
		this.MaxVal = maxVal;
		this.CurrentVal = currentVal;
	}
}
