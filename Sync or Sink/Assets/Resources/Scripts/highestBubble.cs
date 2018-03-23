using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highestBubble : MonoBehaviour
{
    public GameObject highestA;
    public GameObject highestS;
    public GameObject highestD;
    public GameObject highestF;

    // Use this for initialization
    void Start()
    {

    }

    GameObject HighestX(float xPos, GameObject xHigh)
    {
        GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        List<GameObject> bubblesWithXList = new List<GameObject>();
        for (int i = 0; i < bubbles.Length; i++)
        {
            float absoluteDistanceValue = (Mathf.Abs(4.28f - bubbles[i].transform.position.y));
            float xLaneValue = Mathf.Abs(bubbles[i].transform.position.x - xPos);
            if(absoluteDistanceValue<2f&&xLaneValue<.05f&&!(bubbles[i].transform.position.y>5.4)){
                bubblesWithXList.Add(bubbles[i]);

            }
        }

        GameObject[] bubblesWithX = bubblesWithXList.ToArray();
        if(bubblesWithX.Length>1){
            Array.Sort(bubblesWithX, delegate (GameObject obj1, GameObject obj2)
            {
                return obj1.transform.position.y.CompareTo(obj2.transform.position.y);

            });

        }
        List<GameObject> bubblesWithXUnpoppedList = new List<GameObject>();
        for (int i = 0; i < bubblesWithX.Length; i++){
            if(bubblesWithX[i].GetComponent<BubbleAppear>().popped==false){
                bubblesWithXUnpoppedList.Add(bubblesWithX[i]);
            }
        }
        GameObject[] bubblesWithXUnpopped = bubblesWithXUnpoppedList.ToArray();
        GameObject highestObj;
        if(!(bubblesWithXUnpopped.Length==0)){
             highestObj = bubblesWithXUnpopped[bubblesWithXUnpopped.Length - 1];
        }else{
             highestObj = null;
        }
        //   print("HIGHEST POS = " + bubblesWithX[0].transform.position.y);
        return highestObj;

    }
    // Update is called once per frame
    void Update()
    {
        highestA = HighestX(-8.6f, highestA);
        highestS = HighestX(-6.76f, highestS);
        highestD = HighestX(-4.92f, highestD);
        highestF = HighestX(-3.08f, highestF);
    }
    public GameObject highestGet(string letter)
    {
        switch (letter)
        {
            case "A":
                return highestA;
            case "S":
                return highestS;
            case "D":
                return highestD;
            case "F":
                return highestF;
            default:
                return null;

        }
    }
}