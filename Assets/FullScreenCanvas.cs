using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenCanvas : MonoBehaviour {
	
	public PlayerBirdScript playerBirdScript;
	
	void OnEnable () {
		playerBirdScript.DisableCheeps();
	}
	
	void OnDisable () {
		playerBirdScript.EnableCheeps();
	}
}
