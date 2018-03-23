using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems;


public class GoToLevel : MonoBehaviour {
	public GameObject importObj;
	public GameObject recordObj;
	// Use this for initialization
	void Start () {
		StaticVariables.needTransition = true;
		//Screen.SetResolution (1920, 1080, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void LevelSelect(){
		SceneManager.LoadScene ("Level_Select");
		EventSystem.current.SetSelectedGameObject(null);
	}
	public void import(){
		importObj.GetComponent<Import_Script> ().doStuff ();
		EventSystem.current.SetSelectedGameObject(null);
	}
	public void record(){
		recordObj.GetComponent<levelClick> ().beginLoading ();
		EventSystem.current.SetSelectedGameObject(null);
	}
	public void exit(){
		Application.Quit ();
		EventSystem.current.SetSelectedGameObject(null);
	}
}
