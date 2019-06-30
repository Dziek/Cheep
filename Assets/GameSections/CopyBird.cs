using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Here a call, have to copy it offscreen first?
	Camera moves, see bird waiting for you.
	Have to copy four sequences. 
	Something happens when you mess up, but unsure.
*/

public class CopyBird : GameSection {
	
	public GameObject cameraGO;
	public GameObject otherBirdGO;
	
	public PlayerBirdScript playerBirdScript;
	private ComputerBirdScript computerBirdScript;
	
	private int noOfSuccessfulCopies;
	private int noOfCopiesNeeded = 5;
	
	private float[] cheepLengths = new float[]{1, 3, 2, 0.5f, 5};
	private float currentCheepLength;
	
	// Use this for initialization
	void Start () {
		// otherBirdGO = GameObject.Instantiate(otherBirdGO);
		computerBirdScript = otherBirdGO.GetComponent<ComputerBirdScript>();
		
		computerBirdScript.SetCurrentGameSection(this);
		playerBirdScript.SetCurrentGameSection(this);
		
		// CallOut();
		// Invoke("CallOut", Random.Range(1, 4));
		// InvokeRepeating("WaitForReply", 0, 0.5f);
		InvokeRepeating("WaitForFirstCheep", 0, 0.5f);
	}
	
	void WaitForFirstCheep () {
		if (playerBirdScript.GetCurrentPause() > 2)
		{
			CancelInvoke();
			
			GetNewCheepLength();
			CallOut();
		}
	}
	
	// Gets a new cheep length, makes sure it's not the same as the last one
	void GetNewCheepLength () {
		
		float temp = currentCheepLength;
		
		while (temp == currentCheepLength)
		{
			temp = cheepLengths[Random.Range(0, cheepLengths.Length)];
		}
		
		currentCheepLength = temp;
	}
	
	void CallOut () {
		computerBirdScript.Cheep(currentCheepLength);
		playerBirdScript.ResetCurrentPause();
		InvokeRepeating("WaitForReply", currentCheepLength, 0.5f);
		
	}
	
	void WaitForReply () {
		// if (playerBirdScript.isCurrentlyCheeping == false)
		if (playerBirdScript.GetCurrentPause() > 1)
		{
			CancelInvoke();
			CheckReply();
		}
	}
	
	void CheckReply () {
		float allowance = 1f;
		
		Debug.Log("Time Difference: " + Mathf.Abs(playerBirdScript.GetLastCheepLength() - currentCheepLength));
		if (Mathf.Abs(playerBirdScript.GetLastCheepLength() - currentCheepLength) < allowance)
		{
			Debug.Log("Success");
			noOfSuccessfulCopies++;
			
			if (noOfSuccessfulCopies == 1)
			{
				StartCoroutine("Phase1");
			}
			
			if (noOfSuccessfulCopies >= noOfCopiesNeeded)
			{
				StartCoroutine("Phase2");
			}
			
			GetNewCheepLength();
			// CallOut();
			Invoke("CallOut", Random.Range(1, 4));
			
		}else{
			// CallOut();
			Invoke("CallOut", Random.Range(1, 4));
		}
	}
	
	// Update is called once per frame
	// void Update () {
		// if (playerBirdScript.isCurrentlyCheeping == false)
		// {
			
		// }
	// }
	
	IEnumerator Phase1 () {
		
		// cameraMoving = true;
		
		float timeToMoveCamera = 1;
		float t = 0;
		
		Vector3 startPos = cameraGO.transform.position;
		Vector3 endPos = cameraGO.transform.position + Vector3.right * 6.5f;
		
		while (t < timeToMoveCamera)
		{
			cameraGO.transform.position = Vector3.Lerp(startPos, endPos, t / timeToMoveCamera);
			t += Time.deltaTime;
			
			yield return null;
		}
		
		cameraGO.transform.position = endPos;
	}
	
	IEnumerator Phase2 () {
		
		float timeToMoveCamera = 1;
		float t = 0;
		
		Vector3 startPos = cameraGO.transform.position;
		Vector3 endPos = cameraGO.transform.position + Vector3.right * -6.5f;
		
		while (t < timeToMoveCamera)
		{
			cameraGO.transform.position = Vector3.Lerp(startPos, endPos, t / timeToMoveCamera);
			t += Time.deltaTime;
			
			yield return null;
		}
		
		cameraGO.transform.position = endPos;
		
		SectionEnd();
	}
	
	public override void SectionEnd () {
		otherBirdGO.SetActive(false);
		
		base.SectionEnd();
	}
}
