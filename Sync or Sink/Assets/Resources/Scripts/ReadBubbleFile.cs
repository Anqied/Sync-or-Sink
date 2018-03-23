using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System;

[RequireComponent(typeof(AudioSource))]
public class ReadBubbleFile : MonoBehaviour {

	public GameObject Judges;
	public string bubbleFileName;
	public AudioSource bubbleSong;// moved initialization to start()
	public AudioClip bubbleClip;
	public string[][] bubbleTrack;
	public double millisecondCount = 0;
	public static double millisecondCountStatic = 0;
	public string[] letters;
	public int BPM;
	public float timeShift;
	public static int totalNotes;
	public int notesSpawned;
	public int noteArrayCount;
	public double beginningTime;
	public GameObject crowdController;
	public GameObject swimController;
	public GameObject swim1;
	public GameObject swim2;
	public GameObject swim3;
	public GameObject swim4;
    public bool songReady = false;
	public string otherBubbleFileName;
	public bool songStarted;
	public string[] poseList;
	public static bool givenScore;

    public void SongFile(string song)
    {
        bubbleFileName = song;
        otherBubbleFileName = song;
    }
    void Start ()
    {
		//testing code, comment out for cross-scene testing
		bubbleFileName = StaticVariables.SONG;
		otherBubbleFileName = StaticVariables.SONG;
		SongReady ();
		givenScore = false;
		//
    }
    private void Awake()
    {
        bubbleFileName = otherBubbleFileName;  

    }
    public void SongReady()
    {
        songReady = true;
        readingStuff();
    }
    void readingStuff () {
        //Debug.Break ();

		bubbleSong = GetComponent<AudioSource>(); 

		string[] allLines;
		if (!StaticVariables.custom) {
			TextAsset ta = Resources.Load<TextAsset> ("Songs/" + bubbleFileName) as TextAsset;
			StringReader fileReader = new StringReader (ta.text);

			allLines = new string[10000];
			string s = fileReader.ReadLine ();
			int count = 0;
			while (s != null) {
				allLines [count] = s;
				s = fileReader.ReadLine ();
				count++;
			}
			Array.Resize (ref allLines, count);
			fileReader.Close (); //get file
		} else {
			StreamReader fileReader = new StreamReader (Application.dataPath + bubbleFileName);
			allLines = new string[10000];
			string s = fileReader.ReadLine ();
			int count = 0;
			while (s != null) {
				allLines [count] = s;
				s = fileReader.ReadLine ();
				count++;
			}
			Array.Resize (ref allLines, count);
			fileReader.Close (); //get file

		}


		totalNotes = int.Parse (allLines [allLines.Length - 1]);//total number of notes from last line
		for (int i = 2; totalNotes == 0 && i<allLines.Length;i++) {
			int.Parse(allLines[allLines.Length - i]);
		}

        string[] firstLine = allLines [0].Split('*'); //get array of first line elements
		BPM = int.Parse(firstLine [1]);//get bmp from first line
		crowdController.BroadcastMessage ("changeBPM", BPM);//change bpm

		bubbleClip = Resources.Load(("Music/" + firstLine[0]),typeof(AudioClip)) as AudioClip;
		bubbleSong.clip = bubbleClip; //load song from song name
            timeShift = (float.Parse(firstLine[2])) * 1000;//get timeshift in ms

		string[] linesOfNotes = new string[allLines.Length-2]; //create array of notes
		for (int i = 1; i < allLines.Length-1; i++) {
			linesOfNotes [i-1] = allLines [i];
		}

		bubbleTrack = new string[linesOfNotes.Length] []; //2D array
		poseList = new string[linesOfNotes.Length];
		for (int k = 0; k < linesOfNotes.Length; k++) { 
			
			string[] split = linesOfNotes [k].Split(" " [0]); //split each line into parts

			float timeOfNotes = (60000/BPM) * (float.Parse (split [0])-1); //first split is note time
			
			string[] letters = split [1].Split ("," [0]); //second split is all the letters
			bubbleTrack [k] = new string[letters.Length + 2];
			bubbleTrack [k] [0] = (timeOfNotes).ToString();

			for (int i = 0; i < letters.Length; i++) {
				bubbleTrack [k] [i + 1] = letters [i];
			}

			bubbleTrack[k] [letters.Length + 1] = split [2]; //third split is direction


			if (split.Length > 3 && split [3] != "") {
				poseList [k] = split [3];
			}

		}
		//bubble track holds time Of Notes(ms), letters, and direction
		/*
		 * bubbleTrack[1] = {1000, d, f, j, k, none}
		 * bubbleTrack[2] = {2000, d, f, up}
		 * etc...
		 */

		//Debug.Break ();


		/*
		 * time from beginning of room to first note popped:
		 * 1. time for first note to spawn: depends on first note time
		 * 2. time for note to travel up: depends on bubble speed
		 */

		noteArrayCount = 0;
		notesSpawned = 0;
		StartCoroutine (startSong());
		//songStarted = true;
		//beginningTime = AudioSettings.dspTime;
		//bubbleSong.PlayScheduled (beginningTime + 10.28f/BubbleAppear.bubbleSpeed - timeShift); 
	}

	IEnumerator startSong (){
		yield return new WaitForSeconds (1);

		songStarted = true;
		beginningTime = AudioSettings.dspTime;
        //bubbleSong.Play();
		bubbleSong.PlayScheduled (beginningTime + 10.28f/BubbleAppear.bubbleSpeed - timeShift); 
	}
    // Update is called once per frame
    void Update()
    {
		if (songReady && songStarted)
        {
			millisecondCount = (AudioSettings.dspTime - beginningTime) *1000; // get time
            millisecondCountStatic = millisecondCount; 
            string[] letters = null;
			if (notesSpawned <= totalNotes && noteArrayCount < bubbleTrack.Length) { // if song not over
				letters = bubbleTrack [noteArrayCount];
				if (letters != null && float.Parse (letters [0]) <= millisecondCount) { // if notes at line
					BroadcastMessage ("timeDisplacement", millisecondCount - float.Parse (letters [0])); // send time displacement
					BroadcastMessage ("direction", letters [letters.Length - 1]);//send direction
					for (int j = 1; j < letters.Length - 1; j++) {//broadcast all letters
						switch (letters [j]) {
						case ("a"):
							BroadcastMessage ("letter", "a");
							notesSpawned++;
							break;
						case ("s"):
							BroadcastMessage ("letter", "s");
							notesSpawned++;
							break;
						case ("d"):
							BroadcastMessage ("letter", "d");
							notesSpawned++;
							break;
						case ("f"):
							BroadcastMessage ("letter", "f");
							notesSpawned++;
							break;
						default:
							break;
						}
					}
					if (poseList [noteArrayCount] != null && poseList [noteArrayCount] != "") {
						swimController.GetComponent<SwimController> ().changePose (poseList [noteArrayCount], 10.28 / BubbleAppear.bubbleSpeed - ((float.Parse (letters [0])) - millisecondCount) / 1000);
						swim1.GetComponent<SwimController> ().changePose (poseList [noteArrayCount], 10.28 / BubbleAppear.bubbleSpeed - ((float.Parse (letters [0])) - millisecondCount) / 1000);
						swim2.GetComponent<SwimController> ().changePose (poseList [noteArrayCount], 10.28 / BubbleAppear.bubbleSpeed - ((float.Parse (letters [0])) - millisecondCount) / 1000);
						swim3.GetComponent<SwimController> ().changePose (poseList [noteArrayCount], 10.28 / BubbleAppear.bubbleSpeed - ((float.Parse (letters [0])) - millisecondCount) / 1000);
						swim4.GetComponent<SwimController> ().changePose (poseList [noteArrayCount], 10.28 / BubbleAppear.bubbleSpeed - ((float.Parse (letters [0])) - millisecondCount) / 1000);
					}
					noteArrayCount++;
				}

			} else {
				if (!bubbleSong.isPlaying && !PauseMenu.paused&&!givenScore) {
					Debug.Log ("give score");
					Judges.GetComponent<JudgeScript> ().giveScore (bubbleFileName, totalNotes);
					givenScore = true;
				}
			}
        }
    }
}
