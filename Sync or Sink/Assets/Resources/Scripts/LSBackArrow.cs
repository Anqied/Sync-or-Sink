using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSBackArrow : MonoBehaviour {
	public bool clicked;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)&&!clicked) {
			Vector2 cubeRay = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D cubeHit = Physics2D.Raycast (cubeRay, Vector2.zero);
			if (cubeHit.collider != null && cubeHit.collider.gameObject != null&&cubeHit.collider.gameObject == this.gameObject) {
				GetComponentInParent<LSMenu> ().backSong ();
			}
		}
	}
	public void thingClicked()
	{
		clicked = true;
	}
	public void thingUnClicked()
	{
		clicked = false;
	}
}
