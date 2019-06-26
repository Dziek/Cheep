using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBirdScript : BaseBird {
	
	// private PlayerBirdScript playerBirdScript;
	
	public override void Awake () {
		base.Awake();
		// playerBirdScript = GameObject.Find("PlayerBird").GetComponent<PlayerBirdScript>();
	}
	
	public void Cheep (float time) {
		StartCoroutine("CheepCoroutine", time);
	}
	
	IEnumerator CheepCoroutine (float time) {
		
		float t = 0;
		birdAS.Play();
		birdGraphicsScript.StartCheep();
		currentGameSection.OBCheepStarted();
		isCurrentlyCheeping = true;
		
		while (t < time && isCurrentlyCheeping == true)
		{
			t += Time.deltaTime;
			yield return null;
		}
			
		birdAS.Stop();
		birdGraphicsScript.StopCheep();
		isCurrentlyCheeping = false;
		currentGameSection.OBCheepFinished();
	}
}
