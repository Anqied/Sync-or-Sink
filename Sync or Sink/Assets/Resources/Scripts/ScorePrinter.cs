using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePrinter : MonoBehaviour {
	public string digit; //100/10/1/Decimal
	public string digitType; //percent or combo or percent100
	public string number; //0-9
	private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		number = "Empty";

	}
	
	// Update is called once per frame
	void Update () {
		sr.sprite = Resources.Load ("Sprites/" + digitType + number, typeof(Sprite))as Sprite;//sprites/percent100Empty
	}
	public void changeNum(string[] whichDigitNum){
		if (digit == whichDigitNum[0]) {
			number = whichDigitNum[1];
		}
	}
}
