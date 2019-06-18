using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBird : MonoBehaviour {
	
	public bool isCurrentlyCheeping;
	
	public AudioSource birdAS;
	public BirdGraphics birdGraphicsScript;
	
	public GameSection currentGameSection;
		
	public virtual void Awake () {
		birdAS = GetComponentInChildren<AudioSource>();
		birdGraphicsScript = GetComponent<BirdGraphics>();
	}
	
	public void SetCurrentGameSection (GameSection gS) {
		currentGameSection = gS;
	}
}
