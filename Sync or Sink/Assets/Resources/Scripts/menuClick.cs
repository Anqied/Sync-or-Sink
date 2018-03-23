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

public class menuClick : MonoBehaviour
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
        Debug.Log(AudioListener.pause);
        AudioListener.pause = false;

        //		Debug.Log (Input.mousePosition.x);
        //	objPos = cam.WorldToScreenPoint(gameObject.transform.position);

    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SceneManager.GetActiveScene().name != sceneToBeUsedOn)
        {
            print("Scene loaded");

            Destroy(this.gameObject);
        }
    }
    public IEnumerator LoadThings()
    {
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

            GameObject[] objects = GameObject.FindGameObjectsWithTag("Clickable");
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SendMessage("thingClicked");
            }

            StartCoroutine(LoadThings());
        }
        Debug.Log("Begun loading scene");

    }
    private void Update()
    {

        if (!clicked)
        {

            if (Input.GetMouseButtonDown(0))
            {

                beginLoading();

            }
        }
        if (SceneManager.GetActiveScene().name != "Level_Select")
        {
            Debug.Log("Scene loaded");

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
