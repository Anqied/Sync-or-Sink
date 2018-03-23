using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour {
	// Use this for initialization
	public int BPM;
	private int count=0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void changeBPM(int bpm){
		BPM = bpm;
	}
}
