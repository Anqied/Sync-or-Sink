using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawning : MonoBehaviour {

	public GameObject Bubble;

	public Vector3 positionA;
	public Vector3 positionS;
	public Vector3 positionD;
	public Vector3 positionF;
	GameObject bubble;
	public GameObject[] bubbleArray;

	private string aDirection;
	// Use this for initialization
	void Start () {
		positionA = new Vector2 (-8.6f,-6); 
		positionS = new Vector2 (-6.76f,-6);
		positionD = new Vector2 (-4.92f,-6);
		positionF = new Vector2 (-3.08f,-6);
		//bubble.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void letter (string letter) {
		switch(letter){
		case ("a"):
			bubble = (GameObject)Instantiate (Bubble, positionA,transform.rotation);
			break;
		case ("s"):
			bubble = (GameObject)Instantiate (Bubble, positionS, transform.rotation);
			break;
		case ("d"):
			bubble = (GameObject)Instantiate (Bubble, positionD,transform.rotation);
			break;
		case ("f"):
			bubble = (GameObject)Instantiate (Bubble, positionF,transform.rotation);
			break;
		default:
			break;
		}
        if(true){
            (bubble.GetComponent<BubbleAppear>()).arrow(aDirection);

        }
	}

	public void direction (string arrowDirection) {
		aDirection = arrowDirection;
	}

	public void timeDisplacement(float time){
		positionA = new Vector2 (-8.6f,-6+time*BubbleAppear.bubbleSpeed/1000); 
		positionS = new Vector2 (-6.76f,-6+time*BubbleAppear.bubbleSpeed/1000);
		positionD = new Vector2 (-4.92f,-6+time*BubbleAppear.bubbleSpeed/1000);
		positionF = new Vector2 (-3.08f,-6+time*BubbleAppear.bubbleSpeed/1000);
	}
}
