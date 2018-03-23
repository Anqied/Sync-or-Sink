using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//396
//404
//421 slash 167
//425
//436
//451

public class levelClickMenuTemp : MonoBehaviour
{
    public string song;
    public bool songButton;
    public string sceneToLoad;
    public Scene sceneLoading;
    public bool clicked = false;
    public string path;
    public string textName;
    // Use this for initialization
    void Start()
    {
        Debug.Log(AudioListener.pause);
        AudioListener.pause = false;

        //		Debug.Log (Input.mousePosition.x);
        //	objPos = cam.WorldToScreenPoint(gameObject.transform.position);

    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SceneManager.GetActiveScene().name != "Level_Select")
        {
            print("Scene loaded");
            Destroy(this.gameObject);
        }
    }
    public IEnumerator LoadThings()
    {
		StaticVariables.custom = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Resources/Scenes/" + sceneToLoad, LoadSceneMode.Single);
        Debug.Log("loading" + sceneToLoad);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (songButton)
        {
            GameObject receptableObj = GameObject.FindGameObjectWithTag("Bubble_Controller");
            receptableObj.GetComponent<ReadBubbleFile>().SongFile(song);
            receptableObj.GetComponent<ReadBubbleFile>().SongReady();
        }
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void beginLoading(){
        Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);
        if (cubeHit.collider.gameObject == this.gameObject)
        {
            Debug.Log("clicked is sent");

            GameObject[] objects = GameObject.FindGameObjectsWithTag("Clickable");
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SendMessage("thingClicked");
            }
            Debug.Log("True");
            getSong();
        }
        Debug.Log("Begun loading scene");

    }
    void getSong(){
        UniFileBrowser.use.SetPath(Application.dataPath);
        UniFileBrowser.use.SetWindowTitle("Choose your song.");
        UniFileBrowser.use.OpenFileWindow(OpenFile);
        UniFileBrowser.use.SendWindowCloseMessage(WindowCloseMessage);

    }
    void WindowCloseMessage(){
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Clickable");
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SendMessage("thingUnClicked");
        }

    }
    void OpenFile(string pathToFile)
    {
        var fileName = (pathToFile);
        Debug.Log("file open is " + fileName);
        path = fileName;
        songSplit();
    }

    void songSplit(){
        string songName = "";
        if(path.Contains("/")){
            string[] slashArray = path.Split("/"[0]);
            songName = slashArray[slashArray.Length - 1];

        }
        if(path.Contains("\\")){
            string[] slashArray = path.Split("\\"[0]);
            songName = slashArray[slashArray.Length - 1];

        }
        textName = (songName.Split("."[0]))[0];
        song = textName;
        StartCoroutine(LoadThings());

    }
    private void Update()
    {

        if (!clicked)
        {
            if (SceneManager.GetActiveScene().name != "Level_Select"){
                if (songButton)
                {
                    try
                    {
                        GameObject receptableObj = GameObject.FindGameObjectWithTag("Bubble_Controller");
                        receptableObj.GetComponent<ReadBubbleFile>().SongFile(song);
                        receptableObj.GetComponent<ReadBubbleFile>().SongReady();

                    }catch(Exception e){
                        Debug.Log("critical error in loading song stuff but not on the readbubblefile");
                        SceneManager.LoadScene("Level_Select");
                    }
                }

            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("clicked is sent");
                beginLoading();

            }
        }
        if (SceneManager.GetActiveScene().name != "Level_Select")
        {
            print("Scene loaded");

            Destroy(this.gameObject);
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
