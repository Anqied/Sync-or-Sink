using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public bool pausedPublic;
	public static bool paused;
	public Image background;
	public GameObject audioPlayer;
	public GameObject mainMenu;
	public GameObject optionsMenu;
	public GameObject countdown;
	private Animator countdownAnim;

	private CanvasGroup mainMenuCanvas;
	private CanvasGroup optionsMenuCanvas;
	private CanvasGroup menuCanvas;
	private bool unpausing;

	private Color grey;
	// Use this for initialization
	void Awake(){
		paused = false;

	}
	void Start () {
		menuCanvas = GetComponent<CanvasGroup> ();
		mainMenuCanvas = mainMenu.GetComponent<CanvasGroup> ();
		optionsMenuCanvas = optionsMenu.GetComponent<CanvasGroup> ();
		countdownAnim = countdown.GetComponent<Animator> ();

		paused = false; //static variable
		background = transform.Find ("MenuBackground").GetComponent<Image> (); //greyed out
		grey = new Color (0f, 0f, 0f, 0.5f);
		background.color = Color.clear;
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false; 
	}
	
	// Update is called once per frame
	void Update () {
		pausedPublic = paused;
		if (Input.GetKeyDown (KeyCode.Escape) && !unpausing) {
			switch ((int)Time.timeScale) {
			case(0):
				unPause ();
				break;
			case(1):
				if(!ReadBubbleFile.givenScore)
					pause ();
				break;
			}
		}

	}
	public void MainMenu(bool show){
		if (show) {

		}
	}

	IEnumerator restartMusic(float delay){
		//yield return new WaitForSeconds (1);
		countdownAnim.SetTrigger("Countdown");
		yield return new WaitForSecondsRealtime (3);
		unpausing = false;
		paused = false; //static variable
		Time.timeScale = 1; //start time
		AudioListener.pause = false; //start music
	}

	public void unPause(){
		unpausing = true;
		background.color = Color.clear; //ungrey out
		menuCanvas.alpha = 0; //disappear menu
		menuCanvas.interactable = false; //no touching
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false; 

		//paused = false; //static variable
		//Time.timeScale = 1; //start time
		//AudioListener.pause = false; //start music
		StartCoroutine (restartMusic (1));
	}
	public void pause(){
		paused = true; //static variable
		Time.timeScale = 0; //stop time
		AudioListener.pause = true; //stop music
		background.color = grey; //grey out
		menuCanvas.alpha = 1; //show menu

		mainMenuCanvas.alpha = 1; // look at me
		optionsMenuCanvas.alpha = 0; // can't see me

		menuCanvas.interactable = true; // yes touching
		mainMenuCanvas.interactable = true; //yes touching
		optionsMenuCanvas.interactable = false; //no touching
        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	public void resume(){
		unPause ();
	}
	public void restart(){
		background.color = Color.clear; //ungrey out
		menuCanvas.alpha = 0; //disappear menu
		menuCanvas.interactable = false; //no touching
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
		paused = false; //static variable
		Time.timeScale = 1; //start time
		AudioListener.pause = false; //start music
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}
	public void menu(){
        background.color = Color.clear; //ungrey out
        menuCanvas.alpha = 0; //disappear menu
        menuCanvas.interactable = false; //no touching
        paused = false; //static variable
        Time.timeScale = 1; //start time
        AudioListener.pause = false; //start music
        SceneManager.LoadScene ("Title_Screen");
	}
	public void options(){
		mainMenuCanvas.alpha = 0;
		mainMenuCanvas.interactable = false;
		optionsMenuCanvas.interactable = true;
		optionsMenuCanvas.alpha = 1;
	}
    public void levelSelect(){
        background.color = Color.clear; //ungrey out
        menuCanvas.alpha = 0; //disappear menu
        menuCanvas.interactable = false; //no touching
        paused = false; //static variable
        Time.timeScale = 1; //start time
        AudioListener.pause = false; //start music
        SceneManager.LoadScene("Level_Select");
    }
}
