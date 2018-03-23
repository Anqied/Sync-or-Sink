using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAppear : MonoBehaviour {
	public int bpmDefault;
	public int BPM;
	public Animator bubbleController;
	public float distance;
	public float absoluteDistanceValue;
	public bool popped;
	public GameObject scoreboard;
	public static float bubbleSpeed; //unity units per second
	public float bubbleSpeedPublic;
	public int keyDirection;
    public bool HighestA = false;
    public bool HighestS = false;
    public bool HighestD = false;
    public bool HighestF = false;
	private bool bpmSwitched = false;
	private Vector3 positionS = new Vector3 (-8.6f,3.5f,0);
	private Vector3 positionD = new Vector3 (-6.76f,3.5f,0);
	private Vector3 positionF = new Vector3 (-4.92f,3.5f,0);
	private Vector3 positionSpace = new Vector3 (-3.08f,3.5f,0);
    public GameObject mommy;

	// Use this for initialization
	void Start () {
		popped = false;
		BPM = bpmDefault;
		bubbleController = GetComponent<Animator> ();
		bubbleSpeed = 8;
        GameObject[] allObj = FindObjectsOfType<GameObject>();
        for (int i = 0; i < allObj.Length; i++)
        {
            if (allObj[i].name == "Bubble_Controller")
            {
                mommy = allObj[i];
            }
        }

	}

	public void arrow(string direction){

		switch (direction) {
		case ("up"):
			bubbleController.SetBool ("Up", true);
			keyDirection = 0;
			break;
		case ("down"):
			bubbleController.SetBool ("Down", true);
			keyDirection = 1;
			break;
		case ("left"):
			bubbleController.SetBool ("Left", true);
			keyDirection = 2;
			break;
		case ("right"):
			bubbleController.SetBool ("Right", true);
			keyDirection = 3;
			break;
		default:
			keyDirection = -1;
			break;
		}
	}

	public void bpmDefaultChange(string bpmchange){
		bpmDefault = int.Parse(bpmchange);
	}

    // Update is called once per frame
    void Update()
    {
        bubbleSpeedPublic = bubbleSpeed;
        if (!popped)
        {
            transform.Translate(Vector3.up * Time.deltaTime * bubbleSpeed);
        }
        else
        {
            transform.Translate(Vector3.zero);
            destroySelfAfterPop();
        }

        distance = gameObject.transform.position.y + 5.4f;
        if (distance > 11.8 && distance < 23)
        {
            scoreboard.BroadcastMessage("comboReset");
            Destroy(gameObject);
        }
        if (mommy.GetComponent<highestBubble>().highestGet("A") == gameObject && !popped)
        {
            HighestA = true;
        }
        if (mommy.GetComponent<highestBubble>().highestGet("S") == gameObject && !popped)
        {
            HighestS = true;
        }
        if (mommy.GetComponent<highestBubble>().highestGet("D") == gameObject && !popped)
        {
            HighestD = true;
        }
        if (mommy.GetComponent<highestBubble>().highestGet("F") == gameObject && !popped)
        {
            HighestF = true;
        }
        absoluteDistanceValue = (Mathf.Abs(4.28f - gameObject.transform.position.y));

        if (keyDirection == arrow_script.arrow || !true)//true will be arrows on/off
        {
            if (absoluteDistanceValue / bubbleSpeed <= .07f && !popped && !PauseMenu.paused)//perfect
            {
                if (Input.GetKeyDown(KeyCode.A) && HighestA && gameObject.transform.position.x == -8.6f)
                {
                    bubbleController.SetTrigger("Perfect");
                    popped = true;
                    scoreboard.BroadcastMessage("comboIncrease");
                    scoreboard.BroadcastMessage("percentIncrease", 1d);
					gameObject.transform.position = new Vector3 (-8.6f,4.28f);
                }
                if (Input.GetKeyDown(KeyCode.S) && HighestS && gameObject.transform.position.x == -6.76f)
                {
                    bubbleController.SetTrigger("Perfect");
                    popped = true;
                    scoreboard.BroadcastMessage("comboIncrease");
					scoreboard.BroadcastMessage("percentIncrease", 1d);
					gameObject.transform.position = new Vector3 (-6.76f,4.28f);
                }
                if (Input.GetKeyDown(KeyCode.D) && HighestD && gameObject.transform.position.x == -4.92f)
                {
                    bubbleController.SetTrigger("Perfect");
                    popped = true;
                    scoreboard.BroadcastMessage("comboIncrease");
					scoreboard.BroadcastMessage("percentIncrease", 1d);
					gameObject.transform.position = new Vector3 (-4.92f,4.28f);
                }

                if (Input.GetKeyDown(KeyCode.F) && HighestF && gameObject.transform.position.x == -3.08f)
                {
                    bubbleController.SetTrigger("Perfect");
                    popped = true;
                    scoreboard.BroadcastMessage("comboIncrease");
					scoreboard.BroadcastMessage("percentIncrease", 1d);
					gameObject.transform.position = new Vector3 (-3.08f,4.28f);
                }
            }
            else if (absoluteDistanceValue / bubbleSpeed > .07f && absoluteDistanceValue / bubbleSpeed < .14f && !popped && !PauseMenu.paused)//good
            {
                if (Input.GetKeyDown(KeyCode.A) && HighestA && gameObject.transform.position.x == -8.6f)
                {
                    bubbleController.SetTrigger("Good");
                    popped = true;
                    scoreboard.BroadcastMessage("comboIncrease");
                    scoreboard.BroadcastMessage("percentIncrease", .8d);
                }
                if (Input.GetKeyDown(KeyCode.S) && HighestS && gameObject.transform.position.x == -6.76f)
                {
                    bubbleController.SetTrigger("Good");
                    popped = true;
                    scoreboard.BroadcastMessage("comboIncrease");
                    scoreboard.BroadcastMessage("percentIncrease", .8d);
                }

                if (Input.GetKeyDown(KeyCode.D) && HighestD && gameObject.transform.position.x == -4.92f)
                {
                    bubbleController.SetTrigger("Good");
                    popped = true;
                    scoreboard.BroadcastMessage("comboIncrease");
                    scoreboard.BroadcastMessage("percentIncrease", .8d);
                }

                if (Input.GetKeyDown(KeyCode.F) && HighestF && gameObject.transform.position.x == -3.08f)
                {
                    bubbleController.SetTrigger("Good");
                    popped = true;
                    scoreboard.BroadcastMessage("comboIncrease");
                    scoreboard.BroadcastMessage("percentIncrease", .8d);
                }
            }
        }
		
	}
	public void destroySelfAfterPop(){
		Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
	}

	public void BPMChange (int newBPM) {
		//if (!bpmSwitched) {
			BPM = newBPM;
			bpmSwitched = true;
		//}
	}
}
