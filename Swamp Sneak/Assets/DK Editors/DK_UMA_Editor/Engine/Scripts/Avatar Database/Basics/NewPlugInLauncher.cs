using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
// Here you have to change the name of your Plug-In's class AND give the same name to both the class and the script
public class NewPlugInLauncher : ScriptableObject{

//	[System.Serializable]
	public string Name;
	public string Author;
	public string Copyright;
	public string Web;
	public string Type;
	public string WinName;
	public float Version;
	public string Description;

	void OpenPlugIn (){
		// Here you have to change the name of your Plug-In's window

	//	GetWindow(typeof(/*Write the name Here*/), false, "Processing...");
	}

}