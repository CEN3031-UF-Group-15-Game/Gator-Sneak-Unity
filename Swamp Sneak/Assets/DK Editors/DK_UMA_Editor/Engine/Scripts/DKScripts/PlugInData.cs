using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

[Serializable]
public class PlugInData : ScriptableObject{

//	[System.Serializable]
	public string Name;
	public string Author;
	public string Copyright;
	public string Web;
	public string Type;
	public string WinName;
	public float Version;
	public string Description;
	#if UNITY_EDITOR
	public EditorWindow Window;
	#endif


}