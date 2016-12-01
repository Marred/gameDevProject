using System;
using UnityEngine;

[RequireComponent (typeof(ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
	private ThirdPersonCharacter m_Character;
	private Vector3 m_Move;
	private bool m_Jump;

        
	private void Start ()
	{
		// Sprawdza, czy doczepiony został skrypt ThirdPersonCharacter.cs
		m_Character = GetComponent<ThirdPersonCharacter> ();
	}


	private void Update ()
	{
		//Ten if został użyty w Update, a nie w FixedUpdate, ponieważ spacja naciskana jest tylko raz, więc musi być wyłapywana przez każdą klatkę.
		m_Jump = Input.GetButtonDown ("Jump");
	}


	// Fixed update is called in sync with physics
	private void FixedUpdate ()
	{
		//Poruszanie się horyzontalnie. Przyciski: A i D
		float h = Input.GetAxis ("Horizontal");

		//Kucanie
		bool crouch = Input.GetKey (KeyCode.C);

		//Wyzerowanie wektora Y i Z - postać może poruszać się tylko po osi X
		m_Move = h * Vector3.right;

		// Chód/Sprint (0.5 / 2)
		if (Input.GetKey (KeyCode.LeftShift))
			m_Move *= 0.5f;

		// pass all parameters to the character control script
		m_Character.Move (m_Move, crouch, m_Jump);
		m_Jump = false;
	}
}