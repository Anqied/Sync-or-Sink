// Example of open/save usage with UniFileBrowser
// This script is free to use in any manner
#pragma strict
import System.IO;

private var message = "";
private var alpha = 1.0;

function OnGUI () {
	if (GUI.Button (Rect(100, 50, 95, 35), "Open")) {
		if (UniFileBrowser.use.allowMultiSelect) {
			UniFileBrowser.use.OpenFileWindow (OpenFiles);
		}
		else {
			UniFileBrowser.use.OpenFileWindow (OpenFile);
		}
	}
	if (GUI.Button (Rect(100, 125, 95, 35), "Save")) {
		UniFileBrowser.use.SaveFileWindow (SaveFile);
	}
	if (GUI.Button (Rect(100, 200, 95, 35), "Open Folder")) {
		UniFileBrowser.use.OpenFolderWindow (true, OpenFolder);
	}
	GUI.color.a = alpha;
	GUI.Label (Rect(100, 275, 500, 1000), message);
	GUI.color.a = 1.0;
}

function OpenFile (pathToFile : String) {
	var fileName = Path.GetFileName (pathToFile);
	message = "You selected file: " + fileName;
	Fade();
}

function OpenFiles (pathsToFiles : String[]) {
	message = "You selected these files:\n";
	for (var i = 0; i < pathsToFiles.Length; i++) {
		message += Path.GetFileName (pathsToFiles[i]) + "\n";
	}
	Fade();
}

function SaveFile (pathToFile : String) {
	var fileName = Path.GetFileName (pathToFile);
	message = "You're saving file: " + fileName;
	Fade();
}

function OpenFolder (pathToFolder : String) {
	message = "You selected folder: " + pathToFolder;
	Fade();
}

function Fade () {
	StopCoroutine ("FadeAlpha");	// Interrupt FadeAlpha if it's already running, so only one instance at a time can run
	StartCoroutine ("FadeAlpha");
}

function FadeAlpha () {
	alpha = 1.0;
	yield WaitForSeconds (5.0);
	for (alpha = 1.0; alpha > 0.0; alpha -= Time.deltaTime/4) {
		 yield;
	}
	message = "";
}