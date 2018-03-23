using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMemberAnimation : MonoBehaviour {
	public Animator crowdMemberAnim;
	// Use this for initialization
	void Start () {
		crowdMemberAnim = GetComponent<Animator> ();
		crowdMemberAnim.speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void changeAnimationSpeed(float speed){
		crowdMemberAnim.speed = speed;
	}
}
