using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheIntroduction : GameSection {
	
	/*
		The Introduction, the first event in CheepCheep.
		Starts off with IntroBird (IB) cheeping off screen after PlayerBird (PB) has cheeped a few times, but then stopped.
		A back and forth happens for a bit. Maybe twice?
		The camera shifts, and IB flies onto screen. They look at each other.
		They take turns cheeping at each other. After a while IB flies away, and the camera recentres.
	*/
	
	public GameObject cameraGO;
	public GameObject otherBirdGO;
	
	public PlayerBirdScript playerBirdScript;
	public ComputerBirdScript computerBirdScript;
	
	private int offScreenChatterCount; // how many times
	private int onScreenChatterCount; // how many times
	private int chatterCount; // how many times they've cheeped back and forth
	
	private int chatterCountTarget = 3; // how many times they need to back and forth to complete section
	
	void Start () {
		// InvokeRepeating("WaitForPlayerToPause", 10, 1); // TODO: The 10 second wait for invoke will change to be a 10 second wait to introduce this section
		InvokeRepeating("WaitForPlayerToPause", 0, 1); // FT!
		
		computerBirdScript.SetCurrentGameSection(this);
		playerBirdScript.SetCurrentGameSection(this);
	}
	
	
	void WaitForPlayerToPause () {
		if (playerBirdScript.GetCurrentPause() > 2)
		{
			computerBirdScript.Cheep(Random.Range(0.2f, 1.5f));
			CancelInvoke();
		}
	}
	
	public override void OBCheepFinished () {
		
		base.OBCheepFinished();
		
		playerBirdScript.ResetCurrentPause();
		InvokeRepeating("WaitForPlayerToPause", 2, 1);
		
		chatterCount++;
		Debug.Log(chatterCount);
		
		if (chatterCount == 3)
		{
			// Phase1();
			StartCoroutine("Phase1");
		}	
		
		if (chatterCount == 6)
		{
			// Phase2();
			StartCoroutine("Phase2");
			CancelInvoke();
		}
	}
	
	// moves comBird on screen and starts more regular chatter
	// void Phase1 () {
		// cameraGO
	// }	
	
	IEnumerator Phase1 () {
		float timeToMoveCamera = 1;
		float t = 0;
		
		Vector3 startPos = cameraGO.transform.position;
		Vector3 endPos = cameraGO.transform.position + Vector3.right * 5;
		
		while (t < timeToMoveCamera)
		{
			cameraGO.transform.position = Vector3.Lerp(startPos, endPos, t / timeToMoveCamera);
			t += Time.deltaTime;
			
			yield return null;
		}
		
		cameraGO.transform.position = endPos;
		
		float timeToMoveBird = 2;
		t = 0;
		
		startPos = otherBirdGO.transform.position;
		endPos = new Vector3(10, 0, 0);
		
		while (t < timeToMoveBird)
		{
			otherBirdGO.transform.position = Vector3.Lerp(startPos, endPos, t / timeToMoveBird);
			t += Time.deltaTime;
			
			yield return null;
		}
		
		otherBirdGO.transform.position = endPos;
	}
	
	IEnumerator Phase2 () {
		float timeToMoveBird = 2;
		float t = 0;
		
		Vector3 startPos = otherBirdGO.transform.position;
		Vector3 endPos = new Vector3(17, 7, 0);
		
		while (t < timeToMoveBird)
		{
			otherBirdGO.transform.position = Vector3.Lerp(startPos, endPos, t / timeToMoveBird);
			t += Time.deltaTime;
			
			yield return null;
		}
		
		otherBirdGO.transform.position = endPos;
		
		float timeToMoveCamera = 1;
		t = 0;
		
		startPos = cameraGO.transform.position;
		endPos = cameraGO.transform.position + Vector3.right * -5;
		
		while (t < timeToMoveCamera)
		{
			cameraGO.transform.position = Vector3.Lerp(startPos, endPos, t / timeToMoveCamera);
			t += Time.deltaTime;
			
			yield return null;
		}
		
		cameraGO.transform.position = endPos;
	}
}
