using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_script : MonoBehaviour {
	private int[] arrows = new int[4];
	public static int arrow;
	private SpriteRenderer image;
	public Sprite up;
	public Sprite down;
	public Sprite left;
	public Sprite right;
	// Use this for initialization
	void Start () {
		image = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!PauseMenu.paused) {
			arrow = -1;
			bool[] pressed = new bool[4];
			if (Input.GetKey (KeyCode.UpArrow)) {
				arrows [0]++;
				pressed [0] = true;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				arrows [1]++;
				pressed [1] = true;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				arrows [2]++;
				pressed [2] = true;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				arrows [3]++;
				pressed [3] = true;
			}
			for (int i = 0; i < 4; i++) {
				if (!pressed [i])
					arrows [i] = 0;
			}

			if (pressed [0] || pressed [1] || pressed [2] || pressed [3]) {
				transform.localScale = new Vector3 (1, 1, 1);
				for (int i = 0; i < 4; i++) {
					if (arrows [i] != 0) {
						if (arrow == -1 || arrows [i] < arrows [arrow]) {
							arrow = i;
						}
					}
				}
			} else {
				transform.localScale = new Vector3 (0, 0, 0);
			}



			if (arrow == 0) {
				image.sprite = up;
			}
			if (arrow == 1) {
				image.sprite = down;
			}
			if (arrow == 2) {
				image.sprite = left;
			}
			if (arrow == 3) {
				image.sprite = right;
			}
		}
	}
}
