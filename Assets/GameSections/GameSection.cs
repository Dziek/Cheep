using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Consider adding a parent class to this for just SectionStart and SectionEnd that I can use in Menu and Prologue
// TODO: Comment it all!
public class GameSection : MonoBehaviour {
	
	public bool displayTitle;
	public string title;
	
	[HideInInspector] public bool playerCheeping;
	[HideInInspector] public bool otherBirdCheeping;
	
	private GameManager gameManager;
	
	public virtual void Awake () {
		gameManager = GetComponentInParent<GameManager>();
	}
	
	public virtual void OBCheepStarted () {
		otherBirdCheeping = true;
	}
	
	public virtual void OBCheepFinished () {
		otherBirdCheeping = false;
	}
	
	public bool IsOtherBirdCheeping () {
		return otherBirdCheeping;
	}
	
	public bool BothBirdsCheeping () {
		if (playerCheeping == false || otherBirdCheeping == false)
		{
			return false;
		}
		
		return true;
	}
	
	public virtual void SectionStart () {
		
	}
	
	public virtual void SectionEnd () {
		gameManager.SectionFinished();
		// gameManager.LoadNextSection();
		
		Debug.Log("SE");
	}
}
