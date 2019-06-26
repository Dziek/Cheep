using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public GameObject interstitialCanvasGO;
	public Text interstitialCanvasText;
	
	private GameSection[] gameSections;
	private int currentGameSection;
	
	void Awake () {
		gameSections = GetComponentsInChildren<GameSection>();
		
		for (int i = 0; i < gameSections.Length; i++)
		{
			gameSections[i].gameObject.SetActive(false);
		}
	}
	
	// Use this for initialization
	void Start () {
		LoadCurrentSection();
	}
	
	void SaveCurrentSection () {
		//TODO: Make this actually save
	}
	
	void LoadCurrentSection () {
		//TODO: Make this actually load
		
		currentGameSection = 0;
		
		gameSections[currentGameSection].gameObject.SetActive(true);
	}
	
	public void SectionFinished () {
		
		gameSections[currentGameSection].gameObject.SetActive(false);
		
		currentGameSection++;
		SaveCurrentSection();
		
		Invoke("StartNextSection", 6);
	}
	
	void StartNextSection () {
		if (gameSections[currentGameSection].displayTitle == true)
		{
			StartCoroutine("DisplayTitle");
			return;
		}
		
		gameSections[currentGameSection].gameObject.SetActive(true);
	}
	
	IEnumerator DisplayTitle () {
		// float t = 0;
		float timeToDisplay = 2;
		
		interstitialCanvasGO.SetActive(true);
		interstitialCanvasText.text = "\"" + gameSections[currentGameSection].title + "\"";
		
		yield return new WaitForSeconds(timeToDisplay);
		
		interstitialCanvasGO.SetActive(false);
		gameSections[currentGameSection].gameObject.SetActive(true);
	}
}
