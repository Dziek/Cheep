using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Tidy this whole thing up
public class BirdGraphics : MonoBehaviour {
	
	public GameObject bodyGO;
	public GameObject eyePupilGO;
	public GameObject eyeWhiteGO;
	public GameObject upperBeakGO;
	public GameObject lowerBeakGO;
	
	private bool cheeping;
	
	public void StartCheep () {
		cheeping = true;
		StartCoroutine("Cheep");
	}
	
	public void StopCheep () {
		// StopCoroutine("Cheep");
		cheeping = false;
	}
	
	IEnumerator Cheep () {
		
		// I think I'll eventually get all this in a custom class, and not have to do it for each one
		
		Vector2 bodyDefaultScale = bodyGO.transform.localScale;
		Vector2 bodyDefaultPos = bodyGO.transform.position;
		
		Vector2 eyePupilDefaultScale = eyePupilGO.transform.localScale;
		Vector2 eyePupilDefaultPos = eyePupilGO.transform.position;
		
		Vector2 eyeWhiteDefaultScale = eyeWhiteGO.transform.localScale;
		Vector2 eyeWhiteDefaultPos = eyeWhiteGO.transform.position;
		
		Vector2 upperBeakDefaultPos = upperBeakGO.transform.position;
		
		Vector2 lowerBeakDefaultPos = lowerBeakGO.transform.position;
		
		float tempScale;
		
		// one time adjustments
		
		float dir = Mathf.Sign(upperBeakGO.transform.position.x - transform.position.x);
		
		tempScale = bodyDefaultScale.x * 1.1f;
		bodyGO.transform.localScale = new Vector2(tempScale, tempScale);
		
		tempScale = eyePupilDefaultScale.x * 0.5f;
		eyePupilGO.transform.localScale = new Vector2(tempScale, tempScale);
		
		eyePupilGO.transform.Translate(new Vector2(dir * 1,1) * 0.25f, Space.World);
		eyeWhiteGO.transform.Translate(new Vector2(dir * 1,1) * 0.25f, Space.World);
		
		upperBeakGO.transform.Translate(Vector3.right * dir * 0.2f, Space.World);
		lowerBeakGO.transform.Translate(Vector3.right * dir * 0.2f, Space.World);
		
		upperBeakGO.transform.RotateAround(bodyGO.transform.position, Vector3.forward, 10);
		lowerBeakGO.transform.RotateAround(bodyGO.transform.position, Vector3.forward, -10);
		
		// while (Input.GetButton("Jump"))
		while (cheeping)
		{
			
			
			
			
			yield return null;
		}
		
		
		bodyGO.transform.localScale = bodyDefaultScale;
		eyePupilGO.transform.localScale = eyePupilDefaultScale;
		
		eyePupilGO.transform.position = eyePupilDefaultPos;
		eyeWhiteGO.transform.position = eyeWhiteDefaultPos;
		
		upperBeakGO.transform.position = upperBeakDefaultPos;
		lowerBeakGO.transform.position = lowerBeakDefaultPos;
		
		upperBeakGO.transform.eulerAngles = new Vector3(0, 0, dir * -90);
		lowerBeakGO.transform.eulerAngles = new Vector3(0, 0, dir * -90);
	}
}
