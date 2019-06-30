using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Interrupt once off screen (maybe have to interrupt a few times?)
	Camera moves
	Interrupt faster and faster 
	--
	To better visualise needing to interrupt fast, possibly have the other bird shrink / moves towards you
*/

// TODO: Comment it all!
public class InterruptBird : GameSection {

	public GameObject cameraGO;
	public GameObject playerBirdGO;
	public GameObject otherBirdGO;
	
	public PlayerBirdScript playerBirdScript;
	private ComputerBirdScript computerBirdScript;
	
	private bool blathering;
	private int noOfInterrupts;
	private float currentInterruptValue;
	private int noOfInterruptsNeeded = 10;
	
	// private bool interruptReset; // makes sure players stop interrupting before any further action is taken // TODO: Might move to parent class
	
	// Use this for initialization
	void Start () {
		// otherBirdGO = GameObject.Instantiate(otherBirdGO);
		computerBirdScript = otherBirdGO.GetComponent<ComputerBirdScript>();
		
		computerBirdScript.SetCurrentGameSection(this);
		playerBirdScript.SetCurrentGameSection(this);
		
		Invoke("Blather", 0);
		playerBirdScript.Stun();
	}
	
	// Update is called once per frame
	void Update () {
		// if (computerBirdScript.isCurrentlyCheeping && playerBirdScript.isCurrentlyCheeping)
		if (playerBirdScript.isCurrentlyCheeping && blathering == true)
		{
			CancelInvoke();
			computerBirdScript.isCurrentlyCheeping = false;
			blathering = false;
			
			noOfInterrupts++;
			currentInterruptValue += 1.5f;
			
			if (noOfInterrupts == 1)
			{
				StartCoroutine("Phase1");
			}
			
			// if (noOfInterrupts >= noOfInterruptsNeeded)
			if (currentInterruptValue >= noOfInterruptsNeeded)
			{
				//TODO: Put in actual ending. (They get too close and the interruptBird balks and rolls away?)
				// SectionEnd();
				StartCoroutine("Phase2");
				return;
			}
			
			//TODO: Wait for player to stop cheeping first
			// Invoke("Blather", Random.Range(0.5f, (float)noOfInterruptsNeeded / 2 - (float)noOfInterrupts / 2));
			// Invoke("Blather", (float)noOfInterruptsNeeded / 2 - (float)noOfInterrupts / 2);
		}
		
		// If not blathering wait for player to shut up before starting again
		if (blathering == false)
		{
			// if (playerBirdScript.GetCurrentPause() > 2)
			// {
				// Invoke("Blather", (float)noOfInterruptsNeeded / 2 - (float)noOfInterrupts / 2);
			// }
			
			if (playerBirdScript.GetCurrentPause() > (float)noOfInterruptsNeeded / 2 - (float)noOfInterrupts / 2)
			{
				Blather();
			}
		}
		
		if (blathering == true)
		{
			currentInterruptValue = Mathf.Clamp(currentInterruptValue -= Time.deltaTime * currentInterruptValue, 0, noOfInterruptsNeeded);
		}
		
		UpdatePositions();
	}
	
	void Blather () {
		
		blathering = true;
		
		float time = Random.Range(0.2f, 0.5f);
		computerBirdScript.Cheep(time);
		Invoke("Blather", time * 1.2f);
	}
	
	// void MoveCloser () {
		// playerBirdGO.transform.Translate(Vector3.right * 0.5f);
		// otherBirdGO.transform.Translate(Vector3.right * -0.5f);
		// Camera.main.orthographicSize -= 0.5f;
	// }
	
	// void MoveApart () {
		// playerBirdGO.transform.Translate(Vector3.right * -0.5f * Time.deltaTime);
		// otherBirdGO.transform.Translate(Vector3.right * 0.5f * Time.deltaTime);
		// Camera.main.orthographicSize += 0.5f;
	// }
	
	//TODO: This is hilariously broken. Moves everything across but then when a cheep ends it goes back to original positions
	//TODO: Unsure if this is visual enough. Doesn't look it. Sort of just looks like they get bigger too.
	void UpdatePositions () {
		
		float v = Mathf.InverseLerp(0, noOfInterruptsNeeded, currentInterruptValue);
		float newx = Mathf.Lerp(0, 3.2f, v);
		
		playerBirdGO.transform.position = new Vector2(newx, 0);
		otherBirdGO.transform.position = new Vector2(13 - newx, 0);
		
		// float newSize = Mathf.Lerp(5, 3, v);
		// Camera.main.orthographicSize = newSize;
	}
	
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
		// Camera.main.orthographicSize = 5;
		
		SectionEnd();
	}
	
	public override void SectionEnd () {
		otherBirdGO.SetActive(false);
		
		base.SectionEnd();
	}
}
