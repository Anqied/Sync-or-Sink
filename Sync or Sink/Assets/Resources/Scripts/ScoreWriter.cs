using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreWriter : MonoBehaviour {
	public int combo;
	public int maxCombo;
	public float percent;
	private bool display;
	public float percentDisplay;

	private int totalNotes;
	// Use this for initialization
	void Start () {
		combo = 0;
		percent = 0;
		percentDisplay = 0;
		totalNotes = ReadBubbleFile.totalNotes;
		display = totalNotes != 0;
	}
	
	// Update is called once per frame
	void Update () {
		string[] temp;
		if (combo < 100) {
			temp = new string[] { "combo100", "0" };
		} else {
			temp = new string[] { "combo100", (combo / 100).ToString () };
		}
		BroadcastMessage("changeNum", temp);

		if (combo < 10) {
			temp = new string[] { "combo10", "0" };
		} else {
			temp = new string[] { "combo10", ((combo % 100) / 10).ToString () };
		}
		BroadcastMessage("changeNum", temp);

		if (combo < 1) {
			temp = new string[] { "combo1", "0" };
		} else {
			temp = new string[] { "combo1", (combo % 10).ToString () };
		}
		BroadcastMessage("changeNum", temp);

		if (percentDisplay == 99.9f) {
			percentDisplay += .1f;
		}

		if (percentDisplay + .1f <= percent && !PauseMenu.paused) {
			percentDisplay += .1f;
		}

		//percentDisplay = percent;

		if (percentDisplay < 100) {
			temp = new string[] { "percent100", "Empty" };
		} else {
			temp = new string[] { "percent100", ((int)(percentDisplay / 100)).ToString () };
		}
		BroadcastMessage("changeNum", temp);

		if (percentDisplay < 10) {
			temp = new string[] { "percent10", "Empty" };
		} else {
			temp = new string[] { "percent10", ((int)((percentDisplay % 100) / 10)).ToString () };
		}
		BroadcastMessage("changeNum", temp);

		if (percentDisplay < 1) {
			temp = new string[] { "percent1", "Empty" };
		} else {
			temp = new string[] { "percent1", ((int)(percentDisplay % 10)).ToString () };
		}
		BroadcastMessage("changeNum", temp);

		if (percentDisplay < .1) {
			temp = new string[] { "percentDecimal", "Empty" };
		} else {
			temp = new string[] { "percentDecimal", ((int)((percentDisplay % 1f)*10)).ToString () };
		}
		BroadcastMessage("changeNum", temp);
	}
		
	public void percentIncrease(float perfectness){
		if (display) {
			percent += (perfectness / totalNotes) * 100;
		}
	}
	public void comboIncrease(){
		combo++;
		if (combo > maxCombo) {
			maxCombo++;
		}
	}
	public void comboReset(){
		combo = 0;
	}
}
