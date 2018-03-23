using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour {
    public string Name;
	// Use this for initialization
	void Start () {
		
	}
    public void Level_Select(){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Resources/Scenes/Level_Select", LoadSceneMode.Single);

    }
	// Update is called once per frame
	void Update () {
		
	}
}
