using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using NAudio;

public class Import_Script : MonoBehaviour
{
    public double millisecondCount = 0;
    public double secondsCount = 0;
    public bool recording = false;
    public string currentLine = "";
    public string[] currentLetters;
    List<string> Lines = new List<string>();
    string textName;
    string path;
    public AudioSource bubbleSong;
    public AudioClip bubbleClip;
    double beginningTime = 0;
    public bool drawBox = false;
    public int buttonCount;
    public bool clicked = false;
    // Use this for initialization
    void Start()
    {

    }
    void FileWindowClosed()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < objects.Length; i++)
        {
            //objects[i].SendMessage("thingUnClicked");
			objects[i].GetComponent<Button>().interactable = true;
        }
    }

    public void thingClicked()
    {
        clicked = true;
    }
    public void thingUnClicked()
    {
        clicked = false;
        Debug.Log("You can click " + this.name + " again.");
    }
    // Update is called once per frame
    void Update()
    {
        /*if (!clicked&&!Input.GetMouseButtonUp(0)) { 
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);
            if (cubeHit.collider.gameObject == this.gameObject)
            {
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("Clickable");
                    for (int i = 0; i < objects.Length; i++)
                    {
                        objects[i].SendMessage("thingClicked");
                    }*/
				
                    //UniFileBrowser.use.SetPath("");
                    //UniFileBrowser.use.SendWindowCloseMessage(FileWindowClosed);
                //UniFileBrowser.use.OpenFileWindow(OpenFile);
            //}
        //}

    }
	public void doStuff(){
		UniFileBrowser.use.SetPath(Application.dataPath);
		UniFileBrowser.use.SendWindowCloseMessage(FileWindowClosed);
		UniFileBrowser.use.OpenFileWindow(OpenFile);
		GameObject[] objects = GameObject.FindGameObjectsWithTag("Button");
		for (int i = 0; i < objects.Length; i++)
		{
			objects [i].GetComponent<Button> ().interactable = false;;
		}


	}

    void OpenFile(string pathToFile)
    {
        var fileName = (pathToFile);
        Debug.Log("file open is " + fileName);
        path = fileName;
        GetMP3();

    }
    public IEnumerator testThing(WWW testWWW){
        this.gameObject.AddComponent<AudioSource>();
        while (!testWWW.isDone)
        {
            yield return 0;
        }
        this.gameObject.GetComponent<AudioSource>().clip = NAudioPlayer.FromMp3Data(testWWW.bytes);
        Debug.Log("done");
    }
    public void GetMP3()
    {
        bubbleSong = GetComponent<AudioSource>();
        FileInfo pathFile = new FileInfo(path);

       // path = UnityEditor.EditorUtility.OpenFilePanel("Choose song to import", "", "mp3");

        //german shepard

        Debug.Log("song imported is " + path);
        string[] slashArray = path.Split("/"[0]);
        string songName = slashArray[slashArray.Length - 1];
        textName = (songName.Split("."[0]))[0];
        Debug.Log("song name is " + songName);
        WWW test = new WWW(path);
        /*
        if (!File.Exists(Application.dataPath + "/Resources/Music/" + songName))
        {
            //  UnityEditor.FileUtil.CopyFileOrDirectory(path, Application.dataPath + "/Resources/Music/" + songName);
            //File.Copy(path, Application.dataPath + "/Resources/Music/" + songName, true);
            pathFile.CopyTo(Application.dataPath + "/Resources/Music/" + songName);;
            FileStream fs = File.Create(Application.dataPath + "/Resources/Songs/" + textName + ".txt");
            fs.Close();
        }



        using (StreamWriter fsd = File.AppendText(Application.dataPath + "/Resources/Songs/" + textName + ".txt"))
        {
            //        var gameFile = File.CreateText("Resources/Songs/" + textName);

        }


        /*     if (UnityEditor.EditorUtility.DisplayDialog("Imported File", "To Finish the import, you must restart the game.", "Restart", "cancel"))
             {
             }
         */
        drawBox = true;


    }

    public Rect testRect = new Rect(200, 500, 1200, 500);

    void OnGUI()
    {
        if(drawBox)
            testRect = GUI.Window(0, testRect, DoMyWindow, "You must restart the game to finish importing.");
    }
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(0, 20, 600, 230), "Quit"))
        {
            print("Got a click");
            drawBox = false;
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Button");
            for (int i = 0; i < objects.Length; i++)
            {
				objects[i].GetComponent<Button>().interactable = true;
				//objects[i].SendMessage("thingUnClicked");
                //Debug.Log("UnClicking the " + i + " object");
            }
            Application.Quit();

        }


        if (GUI.Button(new Rect(600, 20,600, 230), "Cancel"))
        {
            print("test");
            drawBox = false;
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Button");
            for (int i = 0; i < objects.Length; i++)
            {

				objects[i].GetComponent<Button>().interactable = true;
				//objects[i].SendMessage("thingUnClicked");
                //Debug.Log("UnClicking the " + i + " object");
            }
        }

    }
}

