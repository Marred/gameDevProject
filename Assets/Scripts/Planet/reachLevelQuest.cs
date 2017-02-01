using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class reachLevelQuest : MonoBehaviour {
	[SerializeField] private GameObject tower;
	[SerializeField] private GameObject head;
	[SerializeField] private GameObject expOrb;
	[SerializeField] private Text shootText;
	[SerializeField] private Text skillText;
	[SerializeField] private Player player;
	[SerializeField] private GameObject skillsMenu;


	private Vector3 expPos;
	private bool droppedItems = false;
	private bool shownSkills = false;
	private bool openedSkills = false;

	private Vector3 shift;

	// Use this for initialization
	void Start () {
		expPos = head.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (head == null && !droppedItems) {
			droppedItems = true;

			Destroy (tower);
			Destroy (shootText);
			for (int i = 0; i < 6; i++) {
				shift = new Vector3 (Random.Range (-1f, 1f), Random.Range (0f, 0.5f), 0);

				Instantiate (expOrb, expPos + shift, Quaternion.identity);
			}
		} else if (droppedItems && !shownSkills && player.playerLevel.CurrentVal == 2) {
			shownSkills = true;
			Vector3 setAtPlayer = new Vector3 (player.transform.position.x, skillText.transform.position.y, skillText.transform.position.z);

			skillText.transform.position = setAtPlayer;
			skillText.gameObject.SetActive (true);
		} else if (shownSkills && !openedSkills) {
			if (skillsMenu.activeSelf == true) {
				openedSkills = true;
				Destroy (skillText);
			}

		}
	}
}
