using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;

public class SwimController : MonoBehaviour {
	public Animator splashAnim;
	Animator swimAnim;
	SpriteRenderer swimSprite;
	private string swimPose = "UpperNormal";
	private bool poseUp;
	private bool specialSnowflake;
	private Sprite currentSprite;
	private System.Random r;
	public GameObject bubbleFile;
	public int seed;


	public Sprite UpperNormal;
	public Sprite Open;
	public Sprite LlamaLeft;
	public Sprite LlamaRight;
	public Sprite SpanishLeft;
	public Sprite SpanishRight;
	public Sprite CraneLeft;
	public Sprite CraneRight;
	public Sprite VerticalLeft;
	public Sprite VerticalRight;
	public Sprite FlamingoLeft;
	public Sprite FlamingoRight;
	public Sprite SplitLeft;
	public Sprite SplitRight;
	public Sprite BentKneeLeft;
	public Sprite BentKneeRight; 
	public Sprite None;

	public Sprite ani0;
	public Sprite ani1;
	public Sprite ani2;
	public Sprite ani3;
	public Sprite upNorm;
	private bool firstTime = true;

	// Use this for initialization
	void Start () {
		swimAnim = GetComponent<Animator> ();
		swimSprite = GetComponent<SpriteRenderer> ();
		poseUp = true;
		r = new System.Random (seed);
	}

	void firstPose(){
		if (firstTime) {
			swimSprite.sprite = UpperNormal;
			currentSprite = UpperNormal;
			firstTime = false;
		}
	}


	// Update is called once per frame
	void Update () {
		if ((bubbleFile.GetComponent<ReadBubbleFile>()).songReady) {
			swimAnim.SetTrigger ("Start");
		}
		if (seed % 10 == 0) {
			swimAnim.SetFloat ("Speed", 
				(float)(r.NextDouble () * 0.4 + .8));
		}
		seed++;
	}


	public void changePose(string pose, double timeshift){
		StartCoroutine (changePoseReal (pose, timeshift));
	}

	public void splish(){
		splashAnim.SetTrigger ("Splash");
	}

	public IEnumerator changePoseReal(string pose, double timeshift){
		yield return new WaitForSeconds ((float)timeshift);
		switch (pose) {
		case "UpperNormal":
		case "Open":
		case "LlamaLeft":
		case "LlamaRight":
		case "SpanishLeft":
		case "SpanishRight":
		case "CraneLeft":
		case "CraneRight":
			if (!poseUp) {
				swimAnim.SetTrigger ("Down");
				splashAnim.SetTrigger ("Splash");
				poseUp = true;
			} else {
				swimAnim.SetTrigger ("Flip");
			}
			if (specialSnowflake) {
				swimAnim.SetTrigger ("Down");
				splashAnim.SetTrigger ("Splash");
				specialSnowflake = false;
			}
			break;
		case "VerticalLeft":
		case "VerticalRight":
		case "SplitLeft":
		case "SplitRight":
		case "FlamingoLeft":
		case "FlamingoRight":
		case "BentKneeLeft":
		case "BentKneeRight":
			if (poseUp) {
				swimAnim.SetTrigger ("Down");
				splashAnim.SetTrigger ("Splash");
				poseUp = false;	
			} else {
				swimAnim.SetTrigger ("Flip");
			}
			if (specialSnowflake) {
				swimAnim.SetTrigger ("Down");
				splashAnim.SetTrigger ("Splash");
				specialSnowflake = false;
			}
			break;
		case "None":
			swimAnim.SetTrigger ("Down");
			splashAnim.SetTrigger ("Splash");
			specialSnowflake = true;
			break;
		default: 
			break;
		}
		swimPose = pose;
	}

	void LateUpdate(){

		if(currentSprite != null)
			swimSprite.sprite = currentSprite;
		switch (swimSprite.sprite.ToString().Substring(0,17)) {
		case "animationcpu10000":
			swimSprite.sprite = ani0;
			break;
		case "animationcpu10001":
			swimSprite.sprite = ani1;
			break;
		case "animationcpu10002":
			swimSprite.sprite = ani2;
			break;
		case "animationcpu10003":
			swimSprite.sprite = ani3;
			break;
		case "uppernormal0001 (":
			swimSprite.sprite = upNorm;
			break;
		}
	}

	void timeToChangePose(){
		switch (swimPose) {
		case "UpperNormal":
			swimSprite.sprite = UpperNormal;
			currentSprite = UpperNormal;
			break;
		case "Open":
			swimSprite.sprite = Open;
			currentSprite = Open;
			break;
		case "LlamaLeft":
			swimSprite.sprite = LlamaLeft;
			currentSprite = LlamaLeft;
			break;
		case "LlamaRight":
			swimSprite.sprite = LlamaRight;
			currentSprite = LlamaRight;
			break;
		case "SpanishLeft":
			swimSprite.sprite = SpanishLeft;
			currentSprite = SpanishLeft;
			break;
		case "SpanishRight":
			swimSprite.sprite = SpanishRight;
			currentSprite = SpanishRight;
			break;
		case "CraneLeft":
			swimSprite.sprite = CraneLeft;
			currentSprite = CraneLeft;
			break;
		case "CraneRight":
			swimSprite.sprite = CraneRight;
			currentSprite = CraneRight;
			break;
		case "VerticalLeft":
			swimSprite.sprite = VerticalLeft;
			currentSprite = VerticalLeft;
			break;
		case "VerticalRight":
			swimSprite.sprite = VerticalRight;
			currentSprite = VerticalRight;
			break;
		case "SplitLeft":
			swimSprite.sprite = SplitLeft;
			currentSprite = SplitLeft;
			break;
		case "SplitRight":
			swimSprite.sprite = SplitRight;
			currentSprite = SplitRight;
			break;
		case "FlamingoLeft":
			swimSprite.sprite = FlamingoLeft;
			currentSprite = FlamingoLeft;
			break;
		case "FlamingoRight":
			swimSprite.sprite = FlamingoRight;
			currentSprite = FlamingoRight;
			break;
		case "BentKneeLeft":
			swimSprite.sprite = BentKneeLeft;
			currentSprite = BentKneeLeft;
			break;
		case "BentKneeRight":
			swimSprite.sprite = BentKneeRight;
			currentSprite = BentKneeRight;
			break;
		case "None":
			swimSprite.sprite = None;
			currentSprite = None;
			break;
		}
	}



}
