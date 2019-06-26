using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Prologue : GameSection {
	
	public GameObject prologueCanvasGO;
	public Text prologueText;
	
	[TextArea(15,20)]
	public string prologueSpeech;
	
	private string[] prologueLines;
	private int linesPos;
	
	public override void Awake () {
		base.Awake();
		
		prologueLines = prologueSpeech.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
	}
	
	// Use this for initialization
	void Start () {
		prologueCanvasGO.SetActive(true);
		
		InvokeRepeating("NextLine", 0, 1f);
	}
	
	void NextLine () {
		
		if (linesPos >= prologueLines.Length)
		{
			CancelInvoke();
			SectionEnd();
			return;
		}
		
		prologueText.text = prologueLines[linesPos];
		
		linesPos++;
	}
	
	public override void SectionEnd () {
		prologueCanvasGO.SetActive(false);
		
		base.SectionEnd();
	}
	
}


/*To be read by one of those autospeaker things
		
Once Upon A Time
A Bird
A Nest
An Egg

Red Feathers
Burst!
Of Noise
Cheep

A Lonely Bird
Alone
By Themselves
Alone

Awaits Instruction
*/
