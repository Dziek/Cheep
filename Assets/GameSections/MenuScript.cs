using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Consider making this not a GameSection. Unsure what I want to happen if you quit the game mid way through (the monsters)
public class MenuScript : GameSection {

	public GameObject menuCanvasGO;

	public Text titleText;
	public Text creditText;
	public Text beginText;
	
	public GameObject beginTextGO;
	public AudioSource menuMusicAS;
	public AudioSource beginSoundAS;

	// Use this for initialization
	void Start () {
		StartCoroutine("StartUp");
	}
	
	IEnumerator StartUp () {
		
		menuCanvasGO.SetActive(true);
		menuMusicAS.Play();
		
		MakeTextAlphaZero(titleText);
		MakeTextAlphaZero(creditText);
		MakeTextAlphaZero(beginText);
		
		float t = 0;
		float timeToAppear = 2;
		
		Color startColor = titleText.color;
		Color endColor = Color.white;
		
		while (t < timeToAppear)
		{
			titleText.color = Color.Lerp(startColor, endColor, t / timeToAppear);
			
			t += Time.deltaTime;
			yield return null;
		}
		
		t = 0;
		timeToAppear = 2;
		
		startColor = creditText.color;
		endColor = Color.white;
		
		while (t < timeToAppear)
		{
			creditText.color = Color.Lerp(startColor, endColor, t / timeToAppear);
			
			t += Time.deltaTime;
			yield return null;
		}
		
		t = 0;
		timeToAppear = 2;
		
		startColor = beginText.color;
		endColor = Color.white;
		
		while (t < timeToAppear)
		{
			beginText.color = Color.Lerp(startColor, endColor, t / timeToAppear);
			
			t += Time.deltaTime;
			yield return null;
		}
		
		StartCoroutine("WaitForCommand");
	}
	
	IEnumerator WaitForCommand () {
		// menuMusicAS.Play();
		
		Vector3 startPos = beginTextGO.transform.position;
		Vector3 endPos = beginTextGO.transform.position + Vector3.up * 0.5f;
		
		while (!Input.GetButton("Jump"))
		{
			beginTextGO.transform.position = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time, 1));
			
			yield return null;
		}
		
		beginSoundAS.Play();
		
		float t = 0;
		float fadeOutTime = 0.5f;
		
		//TODO: Make sure fadeOutTime is longer than beginSound. Maybe use the length of that as fadeOutTime?
		while (t < fadeOutTime)
		{
			menuMusicAS.volume = Mathf.Lerp(1, 0, t / fadeOutTime);
			
			t += Time.deltaTime;
			yield return null;
		}
		
		menuMusicAS.Stop();
		menuMusicAS.volume = 1;
		
		SectionEnd();
	}
	
	public override void SectionEnd () {
		menuCanvasGO.SetActive(false);
		
		base.SectionEnd();
	}
	
	//TODO: Make an extension
	void MakeTextAlphaZero (Text t) {
	t.color = new Color(t.color.r, t.color.g, t.color.b, 0);
}
}
