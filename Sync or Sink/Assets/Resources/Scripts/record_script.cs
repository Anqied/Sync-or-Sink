using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class record_script : MonoBehaviour {
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
    public int buttonCount;
    public bool fileOpened = false;
    public bool bpmWindow = false;
    public InputField mainInputField;
    public string BPMValue = "120";
    public int BPMINTValue = 120;
    public GameObject inputObj;
    public GameObject parentObj;
    public bool AbleToRecord = false;
    // Use this for initialization
    void Start()
    {
        buttonCount = 0;
        bubbleSong = GetComponent<AudioSource>();
        UniFileBrowser.use.SetPath(Application.dataPath);
        UniFileBrowser.use.OpenFileWindow(OpenFile);

    }
    void getMP3(){
        
        //path = UnityEditor.EditorUtility.OpenFilePanel("Choose song", Application.dataPath + "/Resources/Music/", "mp3");
        Debug.Log("song imported is " + path);

        string[] slashArray = path.Split("\\"[0]);
        string songName = slashArray[slashArray.Length - 1];
        textName = (songName.Split("."[0]))[0];
        Debug.Log("song name is " + songName);
        bubbleClip = Resources.Load(("Music/" + textName), typeof(AudioClip)) as AudioClip;
        bubbleSong.clip = bubbleClip; //load song from song name
        StreamReader fileReader = new StreamReader(Application.dataPath + textName + ".txt");
        string firstLine = fileReader.ReadLine();
        string[] lines = new string[1];
        lines[0] = firstLine;
        fileReader.Close();
        if(File.Exists(Application.dataPath + textName + ".txt"))
        {
            File.Delete(Application.dataPath + textName + ".txt");
            FileStream fs = File.Create(Application.dataPath + textName + ".txt");
            fs.Close();

        }
        using (StreamWriter fs = File.AppendText(Application.dataPath + textName + ".txt"))
        {
            fs.WriteLine(firstLine);
        }
        bpmWindow = true;
        parentObj.SetActive(true);  

    }
    void OpenFile(string pathToFile)
    {
        fileOpened = true;

        var fileName = (pathToFile);
        Debug.Log("file open is " + fileName);
        path = fileName;

        getMP3();
    }

    //this is cause alex hates life
    void record()
    {
        Lines.Add("");
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.F))
        {
            
            double time = millisecondCount;
            currentLine = time + " ";
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentLine = currentLine + "s,";
                buttonCount++;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentLine = currentLine + "d,";
                buttonCount++;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                buttonCount++;
                currentLine = currentLine + "f,";
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentLine = currentLine + "a,";
                buttonCount++;
            }

            if (currentLine.EndsWith(","))
            {
                print("removing comma");
                Truncate(currentLine,(currentLine.Length-2));
                print("removed comma, line is " + currentLine);
            }
            //right> left > down> up
            string addLetter = "";

                if (arrow_script.arrow == 0)
                {
                    addLetter = "up";

                }
                if (arrow_script.arrow == 1)
                {
                    addLetter = "down";

                }
                if (arrow_script.arrow == 2)
                {
                    addLetter = "left";

                }
                if (arrow_script.arrow == 3)
                {
                    addLetter = "right";

                }
                if(arrow_script.arrow!=0&&arrow_script.arrow != 1 &&arrow_script.arrow != 2 &&arrow_script.arrow != 3){
                    addLetter = "none";

                }
               


            if (!Input.GetKeyDown(KeyCode.L))
            {


                using (StreamWriter fs = File.AppendText(Application.dataPath + textName + ".txt"))
                {
                    fs.WriteLine(currentLine + " " + addLetter);
                }
            }
            //StreamWriter fs = new StreamWriter(Application.dataPath + "/Resources/Songs/" + textName + ".txt");
        }

        // gameFile.AppendText(currentLine);
    }


    // Update is called once per frame
    void Update () {
        if(!fileOpened){
            UniFileBrowser.use.gameObject.GetComponent<UniFileBrowser>().enabled = true;

        }
        if (recording) //check if currently recording
        {
            millisecondCount = (float)(((AudioSettings.dspTime - beginningTime))*BPMINTValue / 60)+1;

            record();

            if (Input.GetKeyDown(KeyCode.L))
            {
                recording = false; //if you are and you hit space, stop
                string[] lines = System.IO.File.ReadAllLines(Application.dataPath + textName + ".txt");
                //   float testthing = (float) (1000 * (10.28 / BubbleAppear.bubbleSpeed));
                lines[0] = textName + "*" + BPMValue + "*" + "0";/* replace with whatever you need */
                AudioListener.pause = true;
                System.IO.File.WriteAllLines(Application.dataPath+ textName + ".txt", lines);
                using (StreamWriter fs = File.AppendText(Application.dataPath + textName + ".txt"))
                {
                    fs.WriteLine(buttonCount);
                }
            }


        }
       else if (Input.GetKeyDown(KeyCode.L))
        {
            if (AbleToRecord)
            {
                recording = true;
                beginningTime = AudioSettings.dspTime;
                AudioListener.pause = false;

                //  audio.UnPause();
                bubbleSong.Play();

            }
        }

    }
    public static string Truncate(string source, int length)
    {
        if (source.Length > length)
        {
            return source.Substring(0, length);
        }
        else { return source; }
        
    }
    public void setBPM(){
        int BPMInt = int.Parse((string)inputObj.GetComponent<Text>().text);
        if(BPMInt>59&&BPMInt<301){
            BPMValue = (string)inputObj.GetComponent<Text>().text;
            parentObj.SetActive(false);
            BPMINTValue = BPMInt;
            AbleToRecord = true;
        }
    }
}
