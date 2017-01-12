using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SkillHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private Image bg;
	private Text info;

	private AudioSource source;

	Color blackA = new Color32 (0, 0, 0, 132);
	Color greyA = new Color32 (35, 35, 35, 132);

	[SerializeField] private string hoverInfo;
	[SerializeField] private AudioClip sound;

	private string staticText;

	public void OnPointerEnter(PointerEventData eventData){

		source.PlayOneShot (sound);
		info.text = hoverInfo;
		bg.color = greyA;
	}
	public void OnPointerExit(PointerEventData eventData ){
		bg.color = blackA;
		info.text = staticText;
	}

	// Use this for initialization
	void Start () {
		source = gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		source.playOnAwake = false;
		source.volume = 0.3f;
			
		bg = GetComponent<Image>();
		info = GameObject.Find ("Info/Description").GetComponent<Text> ();

		staticText = info.text;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
