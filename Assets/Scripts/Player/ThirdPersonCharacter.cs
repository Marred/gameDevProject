using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
[RequireComponent (typeof(Animator))]
public class ThirdPersonCharacter : MonoBehaviour
{
	[SerializeField] float m_JumpPower = 11f;
	[Range (1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_RunCycleLegOffset = 0.2f;

	[SerializeField] float m_GroundCheckDistance = 0.1f;

	Rigidbody m_Rigidbody;
	Animator m_Animator;
	public bool m_IsGrounded;
	float m_OrigGroundCheckDistance;
	const float k_Half = 0.5f;

	float m_ForwardAmount;
	float m_CapsuleHeight;
	Vector3 m_CapsuleCenter;
	CapsuleCollider m_Capsule;
	bool m_Crouching;

	public Transform headTarget;
	public Transform shoulderTrans;
	public Transform rightShoulder;

	GameObject rsp;
	public Vector3 lookPos;

	public bool hittingPlayer = false;
	public float aimAngle = 0f;


	void Start ()
	{
		m_Animator = GetComponent<Animator> ();
		m_Rigidbody = GetComponent<Rigidbody> ();
		m_Capsule = GetComponent<CapsuleCollider> ();
		m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;

		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
			RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
		
		m_OrigGroundCheckDistance = m_GroundCheckDistance;

		rsp = new GameObject ();
		rsp.name = transform.root.name + " Right Shoulder IK Helper";
	}

	void FixedUpdate ()
	{
		HandleRotation ();
		HandleAimingPos ();
		HandleShoulder ();

	}

	// pobiera wspolrzedne myszki w trojwymiarze
	void HandleAimingPos ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			Vector3 lookP = hit.point;
			lookP.z = transform.position.z;
			lookPos = lookP;
		}
	}

	//jesli gracz najezdza myszka na gracza, to nie przesuwaj
	void OnMouseOver(){
		if (gameObject.name == "Player")
			hittingPlayer = true;
	}
	void OnMouseExit(){
		if (gameObject.name == "Player")
			hittingPlayer = false;
	}

	//obraca postac w strone myszki z wyzerowanym wektorem Y i Z rownym graczowi
	void HandleRotation()
	{
		//lookPos - wspolrzedne punktu odbicia myszki raycastem od obiektu w swiecie 3d z wektorem Z gracza
		Vector3 directionToLook = lookPos - transform.position;	
		directionToLook.y = 0;

		if (directionToLook == Vector3.zero) return;
		Quaternion targetRotation = Quaternion.LookRotation (directionToLook); //wspolrzedne rotacji docelowej

		// plynnie obraca gracza
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);
	}
	void HandleShoulder ()
	{
		//Obliczanie kątu między trzema punktami - wierzcholkiem jest mysz, punktami gracz i glowa
		aimAngle = (Mathf.Atan2 (transform.position.x - lookPos.x, transform.position.y - lookPos.y) - 
			Mathf.Atan2 (headTarget.position.x - lookPos.x, headTarget.position.y - lookPos.y));

		if (!hittingPlayer && (aimAngle < 0.9f || aimAngle > -0.9f)) {
			shoulderTrans.LookAt (lookPos);

			//ustawia ramie w pozycji startowej
			Vector3 rightShoulderPos = rightShoulder.TransformPoint (Vector3.zero);
			rsp.transform.position = rightShoulderPos;
			rsp.transform.parent = transform;

			shoulderTrans.position = rightShoulder.TransformPoint (Vector3.zero);
		}

	}

	public void Move (Vector3 move, bool crouch, bool jump)
	{
		// wielkosc wektora. niepotrzebne?
		//if (move.magnitude > 1f)
		//	move.Normalize ();

		//przeksztalca wektor ze skali globalnej do lokalnej obiektu
		move = transform.InverseTransformDirection (move);
		CheckGroundStatus ();
		m_ForwardAmount = move.z;

		if (m_IsGrounded) {
			HandleGroundedMovement (crouch, jump);
		} else {
			HandleAirborneMovement ();
		}

		ScaleCapsuleForCrouching (crouch);
		UpdateAnimator (move);
	}

	//skaluje kapsule gracza o polowe
	void ScaleCapsuleForCrouching (bool crouch)
	{
		//jesli jest na podlodze i naciska C, to zmniejsz kapsule o polowe
		if (m_IsGrounded && crouch) {
			
			if (m_Crouching)
				return;
			
			m_Capsule.height = m_Capsule.height / 2f;
			m_Capsule.center = m_Capsule.center / 2f;
			m_Crouching = true;
		} else { //jesli nie naciska lub nie jest na podlodze, to sprawdz, czy cos nie naciska na jego glowe (jesli naciska, to kucnij, jesli nie to wstan)
			Ray crouchRay = new Ray (m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if (Physics.SphereCast (crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore)) {
				m_Crouching = true;
				return;
			}
			m_Capsule.height = m_CapsuleHeight;
			m_Capsule.center = m_CapsuleCenter;
			m_Crouching = false;
		}
	}
	//przesylanie danych do animatora i obliczanie z ktorej nogi wyskoczyc
	void UpdateAnimator (Vector3 move)
	{
		m_Animator.SetFloat ("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		m_Animator.SetBool ("Crouch", m_Crouching);
		m_Animator.SetBool ("OnGround", m_IsGrounded);

		if (!m_IsGrounded) {
			m_Animator.SetFloat ("Jump", m_Rigidbody.velocity.y);
		}

		// calculate which leg is behind, so as to leave that leg trailing in the jump animation
		// (This code is reliant on the specific run cycle offset in our animations,
		// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		float runCycle =
			Mathf.Repeat (
				m_Animator.GetCurrentAnimatorStateInfo (0).normalizedTime + m_RunCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
		if (m_IsGrounded) {
			m_Animator.SetFloat ("JumpLeg", jumpLeg);
		}

		m_Animator.speed = 1;
	}
	// operujemy graczem, gdy jest w powietrzu
	void HandleAirborneMovement ()
	{
		//pozwalamy graczowi poruszac sie w trakcie lotu
		Vector3 airMove = new Vector3 (Input.GetAxis ("Horizontal") * 6f, m_Rigidbody.velocity.y, 0);
		//spowalnia gracza, gdy chce leciec w kierunku odwrotnym do tego, z ktorego wyskoczyl
		m_Rigidbody.velocity = Vector3.Lerp (m_Rigidbody.velocity, airMove, Time.deltaTime*2f); 

		//nadajmy grawitacje
		Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
		m_Rigidbody.AddForce (extraGravityForce);

		//jezeli gracz juz nie spada, to ustalmy wartosci sprawdzajacej wysokosc gracza na normalna
		m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.001f;
	}

	//operujemy graczem gdy stoi
	void HandleGroundedMovement (bool crouch, bool jump)
	{
		// sprawdzenie, czy mozna skakac
		if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo (0).IsName ("Grounded")) {
			// nadajemy wyskok
			m_Rigidbody.velocity = new Vector3 (m_Rigidbody.velocity.x, m_JumpPower, 0); //EDYTOWANE
			m_IsGrounded = false;
			m_GroundCheckDistance = 0.1f;
		}
	}

	// Nadpisujemy funkcje domyslnego poruszania, aby zmienic predkosc postaci przed jej nadaniem
	public void OnAnimatorMove ()
	{
		// Time.deltaTime > 0 to sprawdzenie czy timescale nie jest 0
		if ( m_IsGrounded && Time.deltaTime > 0) {
			Vector3 v = (m_Animator.deltaPosition * 0.8f) / Time.deltaTime;
			// zostawiamy wysokosc
			v.y = m_Rigidbody.velocity.y;

			//Wyzerowanie wektora Z naprawia niepożadądane poruszanie postaci się wertykalnie
			m_Rigidbody.velocity = new Vector3 (v.x, v.y, 0); 
		}
	}


	void CheckGroundStatus ()
	{
		RaycastHit hitInfo;

		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		Vector3 p1 = transform.position + m_Capsule.center;
		if (Physics.SphereCast (p1 + (Vector3.up * 0.2f), m_Capsule.height / 2, Vector3.down, out hitInfo, m_GroundCheckDistance)) {
			m_IsGrounded = true;
		} else {
			m_IsGrounded = false;
		}
	}
}
