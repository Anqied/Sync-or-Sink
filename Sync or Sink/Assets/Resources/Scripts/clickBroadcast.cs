using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickBroadcast : MonoBehaviour {
    public string methodToCall;
    public bool broadcast;
    public bool arguments;
    public bool upwards;
    public object arguementsString;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);

            if (cubeHit)
            {
                if (arguments)
                {
                    if (broadcast)
                    {
                        BroadcastMessage(methodToCall,arguementsString);
                    }
                    else
                    {
                        if (upwards)
                        {
                            SendMessageUpwards(methodToCall, arguementsString);
                        }
                        else
                        {
                            gameObject.SendMessage(methodToCall, arguementsString);
                        }



                    }
                }
                else
                {
                    if (broadcast)
                    {
                        BroadcastMessage(methodToCall);
                    }
                    else
                    {
                        if (upwards)
                        {
                            SendMessageUpwards(methodToCall);
                        }
                        else
                        {
                            gameObject.SendMessage(methodToCall);

                        }

                    }
                }
                
            }
        }

    }
}
