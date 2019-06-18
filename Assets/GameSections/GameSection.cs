using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSection : MonoBehaviour {
	
	public bool playerCheeping;
	public bool otherBirdCheeping;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
