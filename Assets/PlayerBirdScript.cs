using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Can move all of cheep records into BaseBird but currently I don't need to
// TODO: Comment it all!
public class PlayerBirdScript : BaseBird {
	
	private Cheep[] cheepRecords = new Cheep[20];
	// private float currentPause;
	
	private float lastCheepStartTimeStamp = 0;
	private float lastPauseStartTimeStamp = 0;
	
	// name will probably change, but this is a reset for between sections. Means that events that are pause dependent will have to wait for PB to cheep once
	// to start the timer again
	// private bool reset = true;
	
	private bool canCheep = true;
	
	public override void Awake () {
		base.Awake();
	}
	
	// Update is called once per frame
	void Update () {
		if (canCheep)
		{
			if (Input.GetButtonDown("Jump"))
			{
				// StartCheep();
				birdGraphicsScript.StartCheep();
				isCurrentlyCheeping = true;
				StartCheep();
			}
			
			if (Input.GetButtonUp("Jump"))
			{
				birdGraphicsScript.StopCheep();
				isCurrentlyCheeping = false;
			}
		}
	}
	
	void StartCheep () {
		// reset = false;
		StartCoroutine("Cheep");
	}
	
	void StopCheep () {
		// StopCoroutine("Cheep");
	}
	
	IEnumerator Cheep () {
		
		AddPauseToRecord();
		lastCheepStartTimeStamp = Time.time;
		birdAS.Play();
		
		// while (Input.GetButton("Jump"))
		while (isCurrentlyCheeping)
		{
			
			
			
			lastPauseStartTimeStamp = 0; //TODO: Test this doesn't break EVERYTHING
			yield return null;
		}
		
		
		birdAS.Stop();
		AddNewCheepToRecord();
		lastPauseStartTimeStamp = Time.time;
	}
	
	void AddNewCheepToRecord () {
		
		// get length of cheep by taking current time - the time the cheep started
		// float newCheepTime = Time.time - lastCheepStartTimeStamp;
		
		// check if array is already full
		if (cheepRecords[cheepRecords.Length-1].cheepLength != 0)
		{
			// shift array down
			Cheep[] tempArray = new Cheep[cheepRecords.Length];
			for(int i = 0; i < cheepRecords.Length; i++)
			{
				if(i < cheepRecords.Length - 1)
					tempArray[i] = cheepRecords[i + 1];
				else
					tempArray[i].cheepLength = GetCurrentCheep();
			}
			
			cheepRecords = tempArray;
		}else{
			// find an empty array position
			for (int i = 0; i < cheepRecords.Length; i++)
			{
				if (cheepRecords[i].cheepLength == 0)
				{
					cheepRecords[i].cheepLength = GetCurrentCheep();
					break;
				}
			}
		}
	}
	
	void AddPauseToRecord () {
		
		// we don't need the first pause, it doesn't matter in the game at all
		if (cheepRecords[0].cheepLength == 0)
		{
			return;
		}
		
		// check if array is already full - no need for a shift because pause will always be after a cheep
		if (cheepRecords[cheepRecords.Length-1].cheepLength != 0)
		{
			cheepRecords[cheepRecords.Length-1].pauseAfterCheepLength = GetCurrentPause();
		}else{
			// find an empty array position
			for (int i = 0; i < cheepRecords.Length; i++)
			{
				if (cheepRecords[i].pauseAfterCheepLength == 0)
				{
					cheepRecords[i].pauseAfterCheepLength = GetCurrentPause();
					break;
				}
			}
		}
	}
	
	// get length of cheep by taking current time - the time the cheep started
	public float GetCurrentCheep () {
		return Time.time - lastCheepStartTimeStamp;
	}
	
	// get length of pause by taking current time - the time the pause started
	public float GetCurrentPause () {
		// if (reset == true)
		
		if (lastPauseStartTimeStamp == 0 || isCurrentlyCheeping)
		{
			return 0;
		}
		
		return Time.time - lastPauseStartTimeStamp;
	}
	
	// this is so you can't catch two pauses in one long pause. Ensures a cheep needs to happen again to register a new pause
	public void ResetCurrentPause () {
		lastPauseStartTimeStamp = 0;
	}
	
	public void DisableCheeps () {
		canCheep = false;
		birdAS.volume = 0;
	}
	
	public void EnableCheeps () {
		canCheep = true;
		birdAS.volume = 1;
	}
}

[System.Serializable]
public struct Cheep {
	public float cheepLength;
	public float pauseAfterCheepLength;
}
