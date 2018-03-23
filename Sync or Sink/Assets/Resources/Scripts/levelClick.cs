using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//396
//404
//421 slash 167
//425
//436
//451

public class levelClick : MonoBehaviour
{
    public string song;
    public bool songButton;
    public string sceneToLoad;
    public Scene sceneLoading;
    public bool clicked = false;
    public GameObject receptableObj;
    public string sceneToBeUsedOn;

    // Use this for initialization
    void Start()
    {
        //Debug.Log(AudioListener.pause);
        AudioListener.pause = false;

        //		Debug.Log (Input.mousePosition.x);
        //	objPos = cam.WorldToScreenPoint(gameObject.transform.position);

    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SceneManager.GetActiveScene().name != sceneToBeUsedOn)
        {
            //Debug.Log(SceneManager.GetActiveScene().name + " loaded from " + sceneToBeUsedOn);

            //Destroy(this.gameObject);
        }
    }
    public IEnumerator LoadThings()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Resources/Scenes/" + sceneToLoad, LoadSceneMode.Single);
        //Debug.Log("loading " + sceneToLoad);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
			//Debug.Log ("in the while");
            yield return null;
        }
		//Debug.Log (songButton + "in load things");
        /*if (songButton)
        {
            GameObject receptableObj = GameObject.FindGameObjectWithTag("Bubble_Controller");
			Debug.Log ("sending song");
            receptableObj.GetComponent<ReadBubbleFile>().SongFile(song);
            receptableObj.GetComponent<ReadBubbleFile>().SongReady();
        }*/
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    public void beginLoading(){
        /*Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);
		if ()cubeHit.collider.gameObject == this.gameObject)
        {*/
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Button");
            for (int i = 0; i < objects.Length; i++)
            {
				objects[i].GetComponent<Button>().interactable = true;
				//objects[i].SendMessage("thingClicked");
            }
            StartCoroutine(LoadThings());
        //}
    }
    public void RecieveSong(string songname){
        //song = songname;
    }
    private void Update()
    {
		//Debug.Log (song);
        /*if (!clicked)
        {

            if (Input.GetMouseButtonDown(0))
            {
                beginLoading();

            }
        }*/
        if (SceneManager.GetActiveScene().name != sceneToBeUsedOn)
        {
            //Debug.Log("Scene loaded, destroying self");
            //Destroy(this.gameObject);
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
