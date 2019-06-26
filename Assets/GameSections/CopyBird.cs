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
	
	// Use this for initialization
	void Start () {
		// otherBirdGO = GameObject.Instantiate(otherBirdGO);
		computerBirdScript = otherBirdGO.GetComponent<ComputerBirdScript>();
		
		computerBirdScript.SetCurrentGameSection(this);
		playerBirdScript.SetCurrentGameSection(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// IEnumerator Phase1 () {
		
	// }
}
