using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class NewPlugInWin : EditorWindow {
	Vector2 scroll;

	string Name = "";
	string Type = "";
	string Author = "";
	string Copyright = "";
	string Web = "";
	string WinName = "";
	string LauncherName = "";
	string WindowName = "";
	string _Folder = "";
	float Version = 0;
	string _Version = "1000";
	string Description = "";
	bool Ok1 = false;
	bool Ok2 = false;
	bool Ok3 = false;
	bool Ok4 = false;
	bool Ok5 = false;
	bool Ok6 = false;

	PlugInData _PlugInData;
	PlugInData _NewPlugInLauncher;
	ScriptableObject _NewWin;

//	Vector2 scroll;

	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);

	void OnGUI () {
		Repaint ();
		this.minSize = new Vector2 (300, 500);
		#region fonts variables
		var bold = new GUIStyle ("label");
		var boldFold = new GUIStyle ("foldout");
		bold.fontStyle = FontStyle.Bold;
		bold.fontSize = 14;
		boldFold.fontStyle = FontStyle.Bold;
		var Slim = new GUIStyle ("label");
		Slim.fontStyle = FontStyle.Normal;
		Slim.fontSize = 10;	
		var style = new GUIStyle ("label");
		style.wordWrap = true;
		#endregion fonts variables
		GUILayout.Label ( "DK UMA Plug-In Creator", "toolbarbutton", GUILayout.ExpandWidth (true));
		GUILayout.Space(5);	
		if (Name == "" || WinName == "" || Author == "" || Copyright == "") {
			GUI.color = Color.white;
			GUILayout.TextField ("- Step 0 : Fill all the red textfields.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		}
		GUI.color = Green;
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Name :", GUILayout.Width (80));
			if ( Name == "" ) GUI.color = Red;
			else GUI.color = Green;
			Name = GUILayout.TextField( Name, 256, GUILayout.ExpandWidth (true));
		}
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Version :", GUILayout.Width (80));
			_Version = GUILayout.TextField( _Version, 256, GUILayout.ExpandWidth (true));
		}
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Type :", GUILayout.Width (80));
			Type = GUILayout.TextField( Type, 256, GUILayout.ExpandWidth (true));
		}
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Author :", GUILayout.Width (80));
			if ( Author == "" ) GUI.color = Red;
			else GUI.color = Green;
			Author = GUILayout.TextField( Author, 256, GUILayout.ExpandWidth (true));
		}
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Copyright :", GUILayout.Width (80));
			if ( Copyright == "" ) GUI.color = Red;
			else GUI.color = Green;
			Copyright = GUILayout.TextField( Copyright, 256, GUILayout.ExpandWidth (true));
		}
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Web :", GUILayout.Width (80));
			Web = GUILayout.TextField( Web, 256, GUILayout.ExpandWidth (true));
		}
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Window :", GUILayout.Width (80));
			if ( WinName == "" ) GUI.color = Red;
			else GUI.color = Green;
			WinName = GUILayout.TextField( WinName, 256, GUILayout.ExpandWidth (true));
		}
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ("Write a description :", GUILayout.ExpandWidth (true));
		}
		Description = GUILayout.TextField (Description, 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		GUI.color = Color.white;
		GUILayout.Label ( "Steps", "toolbarbutton", GUILayout.ExpandWidth (true));
		// - Step 1
		if (Name != "" && WinName != "" && Author != "" && Copyright != "") {
			if (!Ok1) {
				GUI.color = Color.white;
				GUILayout.TextField ("- Step 1 : Write the scripts for your modification then create the EditorWindow Script to control your Plug-In.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUI.color = Green;
				if (GUILayout.Button ("My scripts and EditorWindow(s) are ready", GUILayout.ExpandWidth (true))) {
					Ok1 = true;
				}
			}
			if (Ok1) {
				GUI.color = Color.yellow;
				if ( GUILayout.Button ( "Cancel", GUILayout.ExpandWidth (true))){
					Cancel();
				}
				using (new ScrollView(ref scroll)) {
					GUI.color = Color.white;
					GUILayout.TextField ("- Step 2 : Create the Launcher for your Plug-In.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					using (new Horizontal()) {
						GUI.color = Color.white;
						GUILayout.Label ("Launcher :", GUILayout.Width (80));
						if (LauncherName == "")
							GUI.color = Red;
						else
							GUI.color = Green;
						GUILayout.TextField (LauncherName, 256, GUILayout.ExpandWidth (true));
						GUI.color = Green;
						if (LauncherName == "" && GUILayout.Button ("Create", GUILayout.ExpandWidth (false))) {
							CreateLauncher ();
						}
						if (LauncherName != "" && GUILayout.Button ("go", GUILayout.ExpandWidth (false))) {
							Selection.activeObject = _NewPlugInLauncher;
						}
					}
					if ( LauncherName != "" ){
						using (new Horizontal()) {
							GUILayout.Label ("Window :", GUILayout.Width (80));
							if (WindowName == "") GUI.color = Red;
							else GUI.color = Green;
							GUILayout.TextField (WindowName, 256, GUILayout.ExpandWidth (true));
							GUI.color = Green;
						
							if (WindowName != "" && GUILayout.Button ("go", GUILayout.ExpandWidth (false))) {
								Selection.activeObject = _NewWin;
							}
						}

						GUILayout.TextField ("- Step 3 :", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
						GUILayout.TextField ("Browse your Project window to gather and move the necessary Scripts for your Plug-In to work properly.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
						if (!Ok2){
							GUILayout.TextField ("Put the Scripts In the newly created Folder, its name is the same than the Plug-In.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							GUILayout.Space (5);
							GUI.color = Green;
							if (GUILayout.Button ("That's done, go to next step", GUILayout.ExpandWidth (true))) {
								Ok2 = true;
							}
						}
						if ( Ok2 ){
							GUILayout.TextField ("- Step 4 :", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							GUILayout.TextField ("Now you have to gather your EditorWindows and drop them into the Editor Folder of your Plug-In Folder.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							if ( !Ok3 && GUILayout.Button ("That's done, go to next step", GUILayout.ExpandWidth (true))) {
								Ok3 = true;
							}
							if ( Ok3 ){
								GUI.color = Color.white;
								GUILayout.TextField ("- Step 5 : ", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
								GUILayout.TextField ("Create the Window ScriptableObject", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
								if ( !Ok4 ){
									GUILayout.TextField ("To Create the Window ScriptableObject, select the newly created Launcher from your Project window and Duplicate it.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
									GUILayout.TextField ("Then move it into your newly created Plug-In folder.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
									if ( !Ok4 && GUILayout.Button ("That's done, go to next step", GUILayout.ExpandWidth (true))) {
										Ok4 = true;
									}
								}
								if ( Ok4 ){
									GUILayout.TextField ("- Step 6 :", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
									GUILayout.TextField ("Add the EditorWindow Script", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
									if ( !Ok5 ){
										GUILayout.TextField ("Once you have created the necessary files, you need to Add the correct EditorWindow's Script to your newly created Window ScriptableObject.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
										GUILayout.TextField ("Now Drag and Drop the desired WindowEditor Script from your Plug-In Folder directly to your EditorWindow ScriptableObject, displayed on the Inspecton Window, drop the script into script's field to replace it.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
										if ( !Ok5 && GUILayout.Button ("That's done, go to next step", GUILayout.ExpandWidth (true))) {
											Ok5 = true;
										}
									}
									if ( Ok5 ){
										GUILayout.TextField ("- Step 7 :", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
										GUILayout.TextField ("Verifying the Plug-In", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
										if ( !Ok6 ){
											GUILayout.TextField ("If all the steps has been followed properly, you should see your new Plug-In displayed in the Plug-In Tab from the DK UMA Editor.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
											GUILayout.TextField ("If it is working properly, go to the next step, otherwise you have to cancel the creation of your Plug-In and verify all your content before restarting the creation process.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
											GUI.color = Red;
											if ( !Ok6 && GUILayout.Button ("Cancel", GUILayout.ExpandWidth (true))) {
												Cancel ();
											}
											GUI.color = Green;
											if ( !Ok6 && GUILayout.Button ("Next step", GUILayout.ExpandWidth (true))) {
												Ok6 = true;
											}
										}
										if ( Ok6 ){
											GUILayout.TextField ("- Step 8 :", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
											GUILayout.TextField ("Exporting the Plug-In", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
											GUILayout.TextField ("You can Export your new Plug-In to share or let it stored in your assets.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
											GUILayout.TextField ("If you only store it, you will have to export it manually to share it.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
											GUI.color = Color.white;
											if ( GUILayout.Button ("Store it", GUILayout.ExpandWidth (true))) {
												this.Close();
											}
											GUI.color = Green;
											if ( GUILayout.Button ("Export to Package", GUILayout.ExpandWidth (true))) {
												ExportPackage ();
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
	void ExportPackage (){
		List<string> _List = new List<string>();
		string _SavePath = "";
		string _LauncherPath = "";
		_LauncherPath = "Assets/DK Editors/DK_UMA_Editor/PlugIns/" + Name + ".asset";
		_List.Add (_Folder);
		_List.Add (_LauncherPath);
		string[] _Array = _List.ToArray ();
		_SavePath = EditorUtility.SaveFilePanel ("Export Package", "", "DK UMA-" + Name + " Plug-In.unitypackage", "unitypackage");
		AssetDatabase.ExportPackage (_Array, _SavePath, ExportPackageOptions.Recurse);
	}
	void CreateLauncher (){
		_NewPlugInLauncher = ScriptableObject.CreateInstance<PlugInData> ();
		_NewPlugInLauncher.Name = Name;
		_NewPlugInLauncher.Type = Type;
		_NewPlugInLauncher.Author = Author;
		_NewPlugInLauncher.Copyright = Copyright;
		_NewPlugInLauncher.Web = Web;
		_NewPlugInLauncher.WinName = WinName;
		_NewPlugInLauncher.Version = Version;
		_NewPlugInLauncher.Description = Description;
		System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Editor/PlugIns/"+Name);
		System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Editor/PlugIns/"+Name+"/Editor");
		System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Editor/PlugIns/"+Name+"/Scripts");
		System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Editor/PlugIns/"+Name+"/Other");
		_Folder = "Assets/DK Editors/DK_UMA_Editor/PlugIns/" + Name;
		string _path = ("Assets/DK Editors/DK_UMA_Editor/PlugIns/"+Name+".asset");
		AssetDatabase.CreateAsset(_NewPlugInLauncher, _path);
		LauncherName = _NewPlugInLauncher.name;
		Selection.activeObject = _NewPlugInLauncher;
		AssetDatabase.Refresh ();
	}

	void CreateNewWin (){
		_NewWin = ScriptableObject.CreateInstance<ScriptableObject> ();
		string _path = (_Folder+"/"+Name+"Window.asset");
		AssetDatabase.CreateAsset(_NewWin, _path);
		_NewPlugInLauncher.Window = _NewWin as EditorWindow;
		WindowName = _NewWin.name;
		Selection.activeObject = _NewWin;
	}
	void Create (){
		_PlugInData = new PlugInData ();
		_PlugInData.Name = Name;
		_PlugInData.Type = Type;
		_PlugInData.Author = Author;
		_PlugInData.Copyright = Copyright;
		_PlugInData.Web = Web;
		_PlugInData.WinName = WinName;
		_PlugInData.Version = Version;
		_PlugInData.Description = Description;
		string _path = ("Assets/DK Editors/DK_UMA_Editor/PlugIns/"+Name+".asset");
		AssetDatabase.CreateAsset(_PlugInData, _path);
		this.Close ();
	}

	void Cancel (){
		string _Name = Name;
		if ( _NewPlugInLauncher != null ) {
			string _path = AssetDatabase.GetAssetPath(_NewPlugInLauncher);
			AssetDatabase.DeleteAsset(_path);
			try{
				System.IO.Directory.Delete("Assets/DK Editors/DK_UMA_Editor/PlugIns/"+_Name); // returns a DirectoryInfo object
			}catch(IOException){}
		}
		if ( _PlugInData != null ) {
			string _path = AssetDatabase.GetAssetPath(_PlugInData);
			AssetDatabase.DeleteAsset(_path);
		}
		if ( _NewWin != null ) {
			string _path = AssetDatabase.GetAssetPath(_NewWin);
			AssetDatabase.DeleteAsset(_path);
			System.IO.Directory.Delete("Assets/DK Editors/DK_UMA_Editor/PlugIns/"+_Name); // returns a DirectoryInfo object
		}
		this.Close();
		Debug.Log ("Cancel : Plug-In Creation Aborted");
	}
}
