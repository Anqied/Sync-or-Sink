using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LSMenu : MonoBehaviour {
	public Sprite easy;
	public Sprite medium;
	public Sprite hard;
	public Sprite FLOAT;
	public Sprite ST_CREDITS;
	public Sprite SERENDIPITY;
	public Sprite SPLASH;
	public Sprite C_MON;
	public Sprite CONCHORDIA;
	public Sprite percent0;
	public Sprite percent1;
	public Sprite percent2;
	public Sprite percent3;
	public Sprite percent4;
	public Sprite percent5;
	public Sprite percent6;
	public Sprite percent7;
	public Sprite percent8;
	public Sprite percent9;
	public Sprite combo0;
	public Sprite combo1;
	public Sprite combo2;
	public Sprite combo3;
	public Sprite combo4;
	public Sprite combo5;
	public Sprite combo6;
	public Sprite combo7;
	public Sprite combo8;
	public Sprite combo9;
	public Sprite S;
	public Sprite A;
	public Sprite B;
	public Sprite C;
	public Sprite F;

	private AudioSource slide;
	private Animator menuAnim;
	private int numSongs;
	private string[][] songData = new string[10][];
	private bool transitioning;

	public string nextSongName;
	public string songName;
	public string backSongName;
	public string difficulty;
	public float highScore;
	public int maxCombo;



	public GameObject difficultyObj;
	public GameObject songNameObj;
	public GameObject nextSongObj;
	public GameObject backSongObj;
	public GameObject Percent100;
	public GameObject Percent10;
	public GameObject Percent1;
	public GameObject PercentDecimal;
	public GameObject Combo100;
	public GameObject Combo10;
	public GameObject Combo1;
	public GameObject Letter;

	// Use this for initialization
	void Start () {
		slide = GetComponent<AudioSource> ();
		menuAnim = GetComponent<Animator> ();
		TextAsset ta = Resources.Load<TextAsset> ("SongData");
		StringReader sr = new StringReader (ta.text);
		string[] file = new string[10];
		string s = sr.ReadLine ();
		int count = 0;
		while (s != null) {
			file [count] = s;
			s = sr.ReadLine ();
			count++;
		}
		sr.Close ();
		System.Array.Resize (ref file, count);
		numSongs = file.Length;
		for(int i=0; i<file.Length; i++){
			songData [i] = file [i].Split('*');
			if (!PlayerPrefs.HasKey(songData[i][0] + "HS")) {
				PlayerPrefs.SetFloat(songData[i][0] + "HS" , 0);
			}
			if(!PlayerPrefs.HasKey(songData[i][0] + "Combo")){
				PlayerPrefs.SetInt(songData[i][0] + "Combo" , 0);
			}
		}

		transitioning = false;
		if (StaticVariables.songIndex + 1 == numSongs) {
			nextSongName = songData [0] [0];
		} else {
			nextSongName = songData [StaticVariables.songIndex + 1] [0];
		}
		if (StaticVariables.songIndex == 0) {
			backSongName = songData [numSongs - 1] [0];
		} else {
			backSongName = songData [StaticVariables.songIndex - 1] [0];
		}

		songName = songData [StaticVariables.songIndex] [0];
		difficulty = songData [StaticVariables.songIndex] [1];
		highScore = PlayerPrefs.GetFloat(songData [StaticVariables.songIndex] [0] + "HS");
		maxCombo = PlayerPrefs.GetInt(songData [StaticVariables.songIndex] [0] + "Combo");
		changeText ();
		Debug.Log (songData [StaticVariables.songIndex] [0] );
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			backSong ();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			nextSong ();
		}
	}

	public void nextSong(){
		slide.Play ();
		if (!transitioning) {
			if (StaticVariables.songIndex < numSongs - 1) {
				StaticVariables.songIndex++;
			} else {
				StaticVariables.songIndex = 0;
			}
			if (StaticVariables.songIndex + 1 == numSongs) {
				nextSongName = songData [0] [0];
			} else {
				nextSongName = songData [StaticVariables.songIndex + 1] [0];
			}
			if (StaticVariables.songIndex == 0) {
				backSongName = songData [numSongs - 1] [0];
			} else {
				backSongName = songData [StaticVariables.songIndex - 1] [0];
			}

			songName = songData [StaticVariables.songIndex] [0];
			difficulty = songData [StaticVariables.songIndex] [1];
			highScore = PlayerPrefs.GetFloat(songData [StaticVariables.songIndex] [0] + "HS");
			maxCombo = PlayerPrefs.GetInt(songData [StaticVariables.songIndex] [0] + "Combo");
			menuAnim.SetTrigger ("Change");
		}
	}
	public void backSong(){
		slide.Play ();
		if (!transitioning) {
			if (StaticVariables.songIndex > 0) {
				StaticVariables.songIndex--;
			} else {
				StaticVariables.songIndex = numSongs - 1;
			}
			if (StaticVariables.songIndex + 1 == numSongs) {
				nextSongName = songData [0] [0];
			} else {
				nextSongName = songData [StaticVariables.songIndex + 1] [0];
			}
			if (StaticVariables.songIndex == 0) {
				backSongName = songData [numSongs - 1] [0];
			} else {
				backSongName = songData [StaticVariables.songIndex - 1] [0];
			}
			songName = songData [StaticVariables.songIndex] [0];
			difficulty = songData [StaticVariables.songIndex] [1];
			highScore = PlayerPrefs.GetFloat(songData [StaticVariables.songIndex] [0] + "HS");
			maxCombo = PlayerPrefs.GetInt(songData [StaticVariables.songIndex] [0] + "Combo");
			menuAnim.SetTrigger ("Change");
		}
	}

	public void changeText(){
		switch (songName) {
		case "Float":
			songNameObj.GetComponent<SpriteRenderer> ().sprite = FLOAT;
			StaticVariables.SONG = "Float";
			break;
		case "Credits - ST": 
			songNameObj.GetComponent<SpriteRenderer> ().sprite = ST_CREDITS;
			StaticVariables.SONG = "Credits - ST";
			break;
		case "Serendipity": 
			songNameObj.GetComponent<SpriteRenderer> ().sprite = SERENDIPITY;
			StaticVariables.SONG = "Serendipity";
			break;
		case "Splash!": 
			songNameObj.GetComponent<SpriteRenderer> ().sprite = SPLASH;
			StaticVariables.SONG = "Splash!";
			break;
		case "c'mon": 
			songNameObj.GetComponent<SpriteRenderer> ().sprite = C_MON;
			StaticVariables.SONG = "c'mon";
			break;
		case "Conchordia":
			songNameObj.GetComponent<SpriteRenderer> ().sprite = CONCHORDIA;
			StaticVariables.SONG = "Conchordia";
			break;
		}
		switch (nextSongName) {
		case "Float":
			nextSongObj.GetComponent<SpriteRenderer> ().sprite = FLOAT;
			break;
		case "Credits - ST": 
			nextSongObj.GetComponent<SpriteRenderer> ().sprite = ST_CREDITS;
			break;
		case "Serendipity": 
			nextSongObj.GetComponent<SpriteRenderer> ().sprite = SERENDIPITY;
			break;
		case "Splash!": 
			nextSongObj.GetComponent<SpriteRenderer> ().sprite = SPLASH;
			break;
		case "c'mon": 
			nextSongObj.GetComponent<SpriteRenderer> ().sprite = C_MON;
			break;
		case "Conchordia":
			nextSongObj.GetComponent<SpriteRenderer> ().sprite = CONCHORDIA;
			break;
		}
		switch (backSongName) {
		case "Float":
			backSongObj.GetComponent<SpriteRenderer> ().sprite = FLOAT;
			break;
		case "Credits - ST": 
			backSongObj.GetComponent<SpriteRenderer> ().sprite = ST_CREDITS;
			break;
		case "Serendipity": 
			backSongObj.GetComponent<SpriteRenderer> ().sprite = SERENDIPITY;
			break;
		case "Splash!": 
			backSongObj.GetComponent<SpriteRenderer> ().sprite = SPLASH;
			break;
		case "c'mon": 
			backSongObj.GetComponent<SpriteRenderer> ().sprite = C_MON;
			break;
		case "Conchordia":
			backSongObj.GetComponent<SpriteRenderer> ().sprite = CONCHORDIA;
			break;
		}
		switch(difficulty){
		case "Easy": difficultyObj.GetComponent<SpriteRenderer> ().sprite = easy;
			break;
		case "Medium": difficultyObj.GetComponent<SpriteRenderer> ().sprite = medium;
			break;
		case "Hard": difficultyObj.GetComponent<SpriteRenderer> ().sprite = hard;
			break;
		}

		if (highScore < 100) {
			Percent100.GetComponent<SpriteRenderer> ().sprite = null;
		} else {
			if (highScore == 100) {
				Percent100.GetComponent<SpriteRenderer> ().sprite = percent1;
			} else {
				Percent100.GetComponent<SpriteRenderer> ().sprite = percent0;
				highScore = 0;
			}
		}

		switch((int)(((highScore/10) % 10))){
		case 0:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent0;
			break;
		case 1:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent1;
			break;
		case 2:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent2;
			break;
		case 3:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent3;
			break;
		case 4:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent4;
			break;
		case 5:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent5;
			break;
		case 6:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent6;
			break;
		case 7:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent7;
			break;
		case 8:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent8;
			break;
		case 9:
			Percent10.GetComponent<SpriteRenderer> ().sprite = percent9;
			break;
		}

		switch((int)((highScore % 10))){
		case 0:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent0;
			break;
		case 1:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent1;
			break;
		case 2:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent2;
			break;
		case 3:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent3;
			break;
		case 4:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent4;
			break;
		case 5:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent5;
			break;
		case 6:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent6;
			break;
		case 7:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent7;
			break;
		case 8:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent8;
			break;
		case 9:
			Percent1.GetComponent<SpriteRenderer> ().sprite = percent9;
			break;
		}

		switch((int)((highScore * 10) % 10)){
		case 0:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent0;
			break;
		case 1:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent1;
			break;
		case 2:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent2;
			break;
		case 3:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent3;
			break;
		case 4:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent4;
			break;
		case 5:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent5;
			break;
		case 6:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent6;
			break;
		case 7:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent7;
			break;
		case 8:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent8;
			break;
		case 9:
			PercentDecimal.GetComponent<SpriteRenderer> ().sprite = percent9;
			break;
		}

		switch((int)(((maxCombo/100) % 10))){
		case 0:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo0;
			break;
		case 1:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo1;
			break;
		case 2:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo2;
			break;
		case 3:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo3;
			break;
		case 4:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo4;
			break;
		case 5:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo5;
			break;
		case 6:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo6;
			break;
		case 7:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo7;
			break;
		case 8:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo8;
			break;
		case 9:
			Combo100.GetComponent<SpriteRenderer> ().sprite = combo9;
			break;
		}
		switch((int)(((maxCombo/10) % 10))){
		case 0:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo0;
			break;
		case 1:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo1;
			break;
		case 2:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo2;
			break;
		case 3:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo3;
			break;
		case 4:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo4;
			break;
		case 5:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo5;
			break;
		case 6:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo6;
			break;
		case 7:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo7;
			break;
		case 8:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo8;
			break;
		case 9:
			Combo10.GetComponent<SpriteRenderer> ().sprite = combo9;
			break;
		}

		switch((int)((maxCombo % 10))){
		case 0:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo0;
			break;
		case 1:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo1;
			break;
		case 2:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo2;
			break;
		case 3:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo3;
			break;
		case 4:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo4;
			break;
		case 5:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo5;
			break;
		case 6:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo6;
			break;
		case 7:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo7;
			break;
		case 8:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo8;
			break;
		case 9:
			Combo1.GetComponent<SpriteRenderer> ().sprite = combo9;
			break;
		}

		if (highScore >= 0 && highScore < 70) {
			Letter.GetComponent<SpriteRenderer> ().sprite = F;
		}
		if (highScore >= 70 && highScore < 80) {
			Letter.GetComponent<SpriteRenderer> ().sprite = C;
		}
		if (highScore >= 80 && highScore < 90) {
			Letter.GetComponent<SpriteRenderer> ().sprite = B;
		}
		if (highScore >= 90 && highScore < 95) {
			Letter.GetComponent<SpriteRenderer> ().sprite = A;
		}
		if (highScore >= 95 && highScore <= 100) {
			Letter.GetComponent<SpriteRenderer> ().sprite = S;
		}

	}
	public void transitionOn(){
		transitioning = true;
	}
	public void transitionOff(){
		transitioning = false;
	}


	public void saveData(){


	}
}
