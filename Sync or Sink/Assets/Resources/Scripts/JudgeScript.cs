using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JudgeScript : MonoBehaviour {
	public GameObject scoreboard;
	public GameObject Kat;
	public GameObject CuiChuanAn;
	public GameObject FriedrichWilhelmVonRichtofenIII;
	private ScoreWriter score;
	private bool scoreGiven;

	public AudioClip S;
	public AudioClip A;
	public AudioClip B;
	public AudioClip C;
	public AudioClip F;

	// Use this for initialization
	void Start () {
		score=scoreboard.GetComponent<ScoreWriter> ();
		System.Random r = new System.Random ();
	}
	
	// Update is called once per frame
	void Update () {
		if (scoreGiven == true && !GetComponent<AudioSource>().isPlaying) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			SceneManager.LoadScene ("Level_Select");
		}
	}
	public void giveScore(string song, int fullCombo){
		Debug.Log ("giveScore");
		int katScore = (int)(score.percent/10 +.5); 
		int cuiScore = (int)(Mathf.Sqrt (score.maxCombo * 100f / fullCombo) + .5);
		int FWVRIIIScore = (int)((Mathf.Sqrt (score.maxCombo * 100f / fullCombo) + score.percent / 10) / 2 + .5);

		Kat.GetComponent<Animator> ().SetInteger ("Score", katScore);
		CuiChuanAn.GetComponent<Animator> ().SetInteger ("Score", cuiScore);
		FriedrichWilhelmVonRichtofenIII.GetComponent<Animator> ().SetInteger ("Score", FWVRIIIScore);
		PlayerPrefs.SetFloat (song + "HS", score.percent);
		PlayerPrefs.SetInt (song + "Combo", score.maxCombo);
		Debug.Log (song);
		if (score.percent >= 0 && score.percent < 70) {
			GetComponent<AudioSource> ().clip = F;
		}
		if (score.percent >= 70 && score.percent < 80) {
			GetComponent<AudioSource> ().clip = C;
		}
		if (score.percent >= 80 && score.percent < 90) {
			GetComponent<AudioSource> ().clip = B;
		}
		if (score.percent >= 90 && score.percent < 95) {
			GetComponent<AudioSource> ().clip = A;
		}
		if (score.percent >= 95 && score.percent <= 100) {
			GetComponent<AudioSource> ().clip = S;
		}
		GetComponent<AudioSource> ().Play ();
		scoreGiven = true;

	}
}
