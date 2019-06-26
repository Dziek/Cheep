using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Tidy up the duplication of isCheeping in this class and BirdGraphics
public class BaseBird : MonoBehaviour {
	
	public bool isCurrentlyCheeping;
	
	public AudioSource birdAS;
	public BirdGraphics birdGraphicsScript;
	
	public GameSection currentGameSection;
	
	// [HideInInspector] public bool cancelCheep;
		
	public virtual void Awake () {
		birdAS = GetComponentInChildren<AudioSource>();
		birdGraphicsScript = GetComponent<BirdGraphics>();
	}
	
	public void SetCurrentGameSection (GameSection gS) {
		currentGameSection = gS;
	}
	
	public void Stun () {
		birdGraphicsScript.StartStun();
	}
}
