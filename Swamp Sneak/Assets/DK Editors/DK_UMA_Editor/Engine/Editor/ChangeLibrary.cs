using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UMA;
using UMA.CharacterSystem;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0472 // Null is true.

public class ChangeLibrary : EditorWindow {
	public static string LibType = "";
	public static string Action = "";
	public static string CurrentLibN = "";
	public static string SelectedLibN = "";
	public static GameObject CurrentLibrary;
	public static GameObject SelectedLibrary;
	public static bool EditorMode;
	public static bool ShowSlots;
	public static bool ShowOverlays;
	public static bool ShowRaces;

	string New_Name;
	string _Type;
	string SearchString;

	GameObject DK_UMA;
	GameObject UMA;

	Vector2 scroll;

	GUIContent _Prefab = new GUIContent("P", "Create or Delete an Asset.");
	GUIContent _AutoDelMiss = new GUIContent("Auto Delete Missings", "Verify the Library and delete the missing fields, multiple clicks.");
	GUIContent _Delete = new GUIContent("X", "Delete.");
	GUIContent _Duplic = new GUIContent("C", "Create a copy.");
	GUIContent _Group = new GUIContent("G", "Add the Model to the selected Group. If the Model is already in a Group, the model is removed and placed at the Root.");
	GUIContent _Inst = new GUIContent("Instantiate", "Create an Instance of the Asset.");

	void OnEnable (){
		DK_UMA = GameObject.Find("DK_UMA");
		UMA = GameObject.Find("UMA_DCS");
		if ( UMA == null ) UMA = GameObject.Find("UMA");
	//	EditorMode = true;
	}

	void OnGUI () {
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
		
		//	Repaint();
		using (new Horizontal()) {
			GUI.color = Color.yellow;
			GUILayout.Label ( "Open the list and change the library.", GUILayout.ExpandWidth (true));
		}
		GUI.color = Color.white;
		using (new Horizontal()) {
			GUILayout.Label ( "Current :", GUILayout.Width (75));
			GUILayout.Label ( CurrentLibN, GUILayout.ExpandWidth (true));
			if ( !DK_UMA || !UMA ) if ( GUILayout.Button ( "Open List", GUILayout.ExpandWidth (true))) {
				DK_UMA = GameObject.Find("DK_UMA");
				UMA = GameObject.Find("UMA_DCS");
				if ( UMA == null ) UMA = GameObject.Find("UMA");
			}
			if ( EditorMode ) GUI.color = Color.yellow;
			else GUI.color = Color.white;
			if ( GUILayout.Button ( "Editor Mode", GUILayout.ExpandWidth (false))) 
			{
				if ( EditorMode ) EditorMode = false;
				else EditorMode = true;
			}
		}
		// Editor
		if ( EditorMode ) {
			GUI.color = Color.white;
			GUILayout.Label ( "Editor", "toolbarbutton", GUILayout.ExpandWidth (true));

			using (new Horizontal()) {
				string _Name;
				GUILayout.Label ( "Selected :", GUILayout.ExpandWidth (false));
				if ( Selection.activeGameObject
				   && ( Selection.activeGameObject.gameObject.GetComponent<DKSlotLibrary>() == true
				    || Selection.activeGameObject.gameObject.GetComponent<DKOverlayLibrary>() == true
				    || Selection.activeGameObject.gameObject.GetComponent<DKRaceLibrary>() == true ))
				{
					_Name = Selection.activeGameObject.name;
					GUILayout.Label ( _Name, GUILayout.ExpandWidth (true));
					if ( Selection.activeGameObject.gameObject.GetComponent<DKSlotLibrary>() == true ) _Type = "DKSlotLibrary";
					if ( Selection.activeGameObject.gameObject.GetComponent<DKOverlayLibrary>() == true ) _Type = "DKOverlayLibrary";
					if ( Selection.activeGameObject.gameObject.GetComponent<DKRaceLibrary>() == true ) _Type = "DKRaceLibrary";
					GUILayout.TextField ( _Type, GUILayout.ExpandWidth (false));
				}
				else {
					GUI.color = Color.yellow;
					GUILayout.Label ( "Selection is not a Library !", GUILayout.ExpandWidth (true));
				}

			}
			if ( Selection.activeGameObject
			    && ( Selection.activeGameObject.gameObject.GetComponent<DKSlotLibrary>() == true
			    || Selection.activeGameObject.gameObject.GetComponent<DKOverlayLibrary>() == true
			    || Selection.activeGameObject.gameObject.GetComponent<DKRaceLibrary>() == true ))
			{
				using (new Horizontal()) {
					if ( New_Name == null ) New_Name = "";
					GUILayout.Label ( "New Name :", GUILayout.ExpandWidth (false));
					New_Name = GUILayout.TextField ( New_Name, GUILayout.ExpandWidth (true));
					if ( New_Name != "" && GUILayout.Button ( "Rename", GUILayout.ExpandWidth (false))) 
					{
						if ( Selection.activeGameObject
						    && ( Selection.activeGameObject.gameObject.GetComponent<DKSlotLibrary>() == true
						    || Selection.activeGameObject.gameObject.GetComponent<DKOverlayLibrary>() == true
						    || Selection.activeGameObject.gameObject.GetComponent<DKRaceLibrary>() == true ))
						{
							Selection.activeGameObject.name = New_Name;
							EditorUtility.SetDirty(Selection.activeGameObject);
							AssetDatabase.SaveAssets();
						}
					}
				}
			
				using (new Horizontal()) {
					if ( GUILayout.Button ( new GUIContent(_AutoDelMiss), GUILayout.ExpandWidth (true))) 
					{
						AutoDel();
					}
					if ( Selection.activeGameObject.gameObject.GetComponent<DKSlotLibrary>() == true
					    || Selection.activeGameObject.gameObject.GetComponent<DKOverlayLibrary>() == true
					    || Selection.activeGameObject.gameObject.GetComponent<DKRaceLibrary>() == true )
					{
						GUI.color = new Color (0.9f, 0.5f, 0.5f);
						if ( GUILayout.Button ( new GUIContent("Clear", "Clear the Library to make it umpty."), GUILayout.ExpandWidth (false))) 
						{
							DeleteAsset.ProcessName = "Clearing Library";
							DeleteAsset.UMAModel = false;
							DeleteAsset.MultiUMAModel = false;
							DeleteAsset.Action = "";
							DK_UMA_Editor.OpenDeleteAsset();
						
						}
					}
				}
			}
			// editor List
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label ( "List", "toolbarbutton", GUILayout.ExpandWidth (true));
				if ( ShowRaces ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( new GUIContent("Races", "Display Races Libraries"), "toolbarbutton", GUILayout.Width (80))) 
				{
					if ( ShowRaces ) ShowRaces = false;
					else ShowRaces = true;
				}
				if ( ShowSlots ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
				else GUI.color = Color.gray;
				if ( GUILayout.Button (new GUIContent("Slot", "Display Slot Libraries"), "toolbarbutton", GUILayout.Width (80))) 
				{
					if ( ShowSlots ) ShowSlots = false;
					else ShowSlots = true;
				}
				if ( ShowOverlays ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( new GUIContent("Overlay", "Display Overlay Libraries"), "toolbarbutton", GUILayout.Width (80))) 
				{
					if ( ShowOverlays ) ShowOverlays = false;
					else ShowOverlays = true;
				}
			}
			#region Search
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label("Search for :", GUILayout.ExpandWidth (false));
				if ( SearchString == null ) SearchString = "";
				SearchString = GUILayout.TextField(SearchString, 100, GUILayout.ExpandWidth (true));
			}
			#endregion Search
		}
	
		if ( (DK_UMA || UMA) && EditorMode ) {
			using (new ScrollView(ref scroll)){
				if ( ShowRaces ){
					GUI.color = Color.yellow;
					GUILayout.Label ( "Race Libraries :", "toolbarbutton", GUILayout.ExpandWidth (true));
					DKRaceLibrary[]  LibraryList = FindObjectsOfType(typeof(DKRaceLibrary)) as DKRaceLibrary[];
					List<string> NamesList = new List<string>();
					for(int i = 0; i < LibraryList.Length; i ++){
						if ( PrefabUtility.GetPrefabParent( LibraryList[i].gameObject ) != null )
							NamesList.Add(PrefabUtility.GetPrefabParent( LibraryList[i].gameObject ).name+".prefab");
						if ( PrefabUtility.GetPrefabType(LibraryList[i]) != null 
						    && PrefabUtility.GetPrefabObject(LibraryList[i]) == null){
						}
					}
					DirectoryInfo dir = new DirectoryInfo("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Race Libraries/");
					FileInfo[] info = dir.GetFiles("*.prefab");
					foreach (FileInfo f in info)
					{
						if ( NamesList.Contains(f.Name) == false ) 
						using (new Horizontal()) {
							GUI.color = Color.cyan;
							if ( GUILayout.Button ( new GUIContent(_Inst), GUILayout.ExpandWidth (false))){
								string myPath = ("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Race Libraries/"+f.Name.ToString());
								GameObject New =  PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(myPath , typeof(GameObject))  ) as GameObject;
								New.transform.parent = EditorVariables.UMAObj.transform;
								PrefabUtility.ReconnectToLastPrefab(New);
							}
							if ( GUILayout.Button ( f.Name, Slim, GUILayout.ExpandWidth (false))) {
							
							}
						}
					}
				
					foreach (Transform Lib in DK_UMA.transform)
					{
						if ( Lib && ( Lib.gameObject.GetComponent<DKRaceLibrary>() == true 
						             && ( Lib.name.ToLower().Contains(SearchString.ToLower()) || SearchString == "" ))){
							using (new Horizontal()) {
								// Prefab
								// Prefab Verification
								string myPath;
								if ( PrefabUtility.GetPrefabParent( Lib.gameObject ) != null )
									myPath = PrefabUtility.GetPrefabParent( Lib.gameObject ).ToString() ;
								else myPath = "null";
								if ( myPath == "null" ) GUI.color = Color.gray;
								else GUI.color = Color.cyan;
								if ( GUILayout.Button ( new GUIContent(_Prefab), "toolbarbutton", GUILayout.ExpandWidth (false))) {
									
									// Create Prefab
									if ( myPath == "null" ) 
									{
										PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Race Libraries/" + Lib.name + ".prefab", Lib.gameObject,ReplacePrefabOptions.ConnectToPrefab  );
										
									}
									else {
										Selection.activeGameObject = Lib.gameObject;
										DelAsset();

									}
								}
								GUI.color = Color.yellow;
								if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
								{
									Selection.activeGameObject = Lib.gameObject;
									Duplicating();
								}
								if ( Lib && Lib.gameObject && GUILayout.Button ( Lib.name+"( DK "+Lib.gameObject.GetComponent<DKRaceLibrary>().raceElementList.Length+")", Slim, GUILayout.ExpandWidth (true))){
									if ( EditorMode ){
										Selection.activeObject = Lib.gameObject;
										EditorVariables.RaceLibraryObj = Lib.gameObject;
										Editor_Global.Variables.RaceLibraryObj = Lib.gameObject;
										EditorVariables._RaceLibrary = Lib.gameObject.GetComponent<DKRaceLibrary>();
										EditorVariables.DK_UMACrowd.raceLibrary = EditorVariables._RaceLibrary;
									}
								}
								GUI.color = Color.white;
								if ( Lib 
								    && Lib.gameObject.GetComponent<DKRaceLibrary>() != null
								    && Lib.gameObject.GetComponent<DKRaceLibrary>().raceElementList.Length > 0 )
									if ( CurrentLibrary && CurrentLibrary.gameObject.GetComponent<DKRaceLibrary>() == true) 
										if ( GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
									{
										if ( Action == "" ){
											EditorVariables.RaceLibraryObj = Lib.gameObject;
											Editor_Global.Variables.RaceLibraryObj = Lib.gameObject;
											EditorVariables._RaceLibrary = Lib.gameObject.GetComponent<DKRaceLibrary>();
											EditorVariables.DK_UMACrowd.raceLibrary = EditorVariables._RaceLibrary;
										}
									}


								GUI.color = new Color (0.9f, 0.5f, 0.5f);
								if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
								{
									DestroyImmediate( Lib.gameObject );
								}
							}
						}
					}
					foreach (Transform Lib in UMA.transform)
					{
						if ( Lib && ( Lib.gameObject.GetComponent<DynamicRaceLibrary>() == true
						             && ( Lib.name.ToLower().Contains(SearchString.ToLower()) || SearchString == "" ))){
							using (new Horizontal()) {
								// Prefab
								// Prefab Verification
								string myPath;
								if ( PrefabUtility.GetPrefabParent( Lib.gameObject ) != null )
									myPath = PrefabUtility.GetPrefabParent( Lib.gameObject ).ToString() ;
								else myPath = "null";
								if ( myPath == "null" ) GUI.color = Color.gray;
								else GUI.color = Color.cyan;
								if ( GUILayout.Button ( new GUIContent(_Prefab), "toolbarbutton", GUILayout.ExpandWidth (false))) {
									
									// Create Prefab
									if ( myPath == "null" ) 
									{
										PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Race Libraries/" + Lib.name + ".prefab", Lib.gameObject,ReplacePrefabOptions.ConnectToPrefab  );
										
									}
									else {
										Selection.activeGameObject = Lib.gameObject;
										DelAsset();
									}
								}
								GUI.color = Color.yellow;
								if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
								{
									Selection.activeGameObject = Lib.gameObject;
									Duplicating();
								}
								try {
									if ( Selection.activeObject == Lib.gameObject )GUI.color = Color.yellow;
									else GUI.color = Color.white; 
									if ( Lib && Lib.gameObject && GUILayout.Button ( Lib.name+"( UMA "/*+Lib.gameObject.GetComponent<RaceLibrary>().raceElementList.Length*/+")", Slim, GUILayout.ExpandWidth (true))){
										if ( EditorMode ){
											Selection.activeObject = Lib.gameObject;
										//	EditorVariables.RaceLibraryObj = Lib.gameObject;
										//	Editor_Global.Variables.RaceLibraryObj = Lib.gameObject;
										//	EditorVariables._RaceLibrary = Lib.gameObject.GetComponent<DKRaceLibrary>();
										//	EditorVariables.DK_UMACrowd.raceLibrary = EditorVariables._RaceLibrary;
										}
									}
									GUI.color = Color.white;
									if ( CurrentLibrary && CurrentLibrary.gameObject.GetComponent<DynamicRaceLibrary>() == true ) 
									{
										if (Action == "" && GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
										{
											if ( Action == "" ){
												DKUMA_Variables._RaceLibrary = Lib.gameObject.GetComponent<DynamicRaceLibrary>();
												this.Close ();
											}
											else if ( EditorMode ){
												Selection.activeGameObject = Lib.gameObject ;
												this.Close ();
											}
										}
									
									}

									GUI.color = new Color (0.9f, 0.5f, 0.5f);
									if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
									{
										if ( Lib && Lib.gameObject ) DestroyImmediate( Lib.gameObject );
									}
								}catch(MissingReferenceException){}
							}
						}
					}
				}


				if ( ShowSlots ){
					GUI.color = Color.yellow;
					GUILayout.Label ( "Slot Libraries :", "toolbarbutton", GUILayout.ExpandWidth (true));
					DKSlotLibrary[]  LibraryList = FindObjectsOfType(typeof(DKSlotLibrary)) as DKSlotLibrary[];
					List<string> NamesList = new List<string>();
					for(int i = 0; i < LibraryList.Length; i ++){
						if ( PrefabUtility.GetPrefabParent( LibraryList[i].gameObject ) != null )
							NamesList.Add(PrefabUtility.GetPrefabParent( LibraryList[i].gameObject ).name+".prefab");
						if ( PrefabUtility.GetPrefabType(LibraryList[i]) != null 
						    && PrefabUtility.GetPrefabObject(LibraryList[i]) == null){
						}
					}
					DirectoryInfo dir = new DirectoryInfo("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Slot Libraries/");
					FileInfo[] info = dir.GetFiles("*.prefab");
					foreach (FileInfo f in info)
					{
						if ( NamesList.Contains(f.Name) == false ) 
						using (new Horizontal()) {
							GUI.color = Color.cyan;
							if ( GUILayout.Button ( new GUIContent(_Inst), GUILayout.ExpandWidth (false))){
								string myPath = ("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Slot Libraries/"+f.Name.ToString());
								GameObject New =  PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(myPath , typeof(GameObject))  ) as GameObject;
								New.transform.parent = EditorVariables.UMAObj.transform;
								PrefabUtility.ReconnectToLastPrefab(New);
							}
							if ( GUILayout.Button ( f.Name, Slim, GUILayout.ExpandWidth (false))) {
								
							}
						}
					}

					foreach (Transform Lib in DK_UMA.transform)
					{
						if ( Lib && ( Lib.gameObject.GetComponent<DKSlotLibrary>() == true 
						&& ( Lib.name.ToLower().Contains(SearchString.ToLower()) || SearchString == "" ))){
						    using (new Horizontal()) {
								// Prefab
								// Prefab Verification
								string myPath;
								if ( PrefabUtility.GetPrefabParent( Lib.gameObject ) != null )
									myPath = PrefabUtility.GetPrefabParent( Lib.gameObject ).ToString() ;
								else myPath = "null";
								if ( myPath == "null" ) GUI.color = Color.gray;
								else GUI.color = Color.cyan;
								if ( GUILayout.Button ( new GUIContent(_Prefab), "toolbarbutton", GUILayout.ExpandWidth (false))) {
									
									// Create Prefab
									if ( myPath == "null" ) 
									{
										PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Slot Libraries/" + Lib.name + ".prefab", Lib.gameObject,ReplacePrefabOptions.ConnectToPrefab  );
										
									}
									else {
										Selection.activeGameObject = Lib.gameObject;
										DelAsset();
									}
								}
								GUI.color = Color.yellow;
								if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
								{
									Selection.activeGameObject = Lib.gameObject;
									Duplicating();
								}
								GUI.color = Color.white;
								if ( Lib && Lib.gameObject && GUILayout.Button ( Lib.name+"( DK "+/*Lib.gameObject.GetComponent<DKSlotLibrary>().slotElementList.Length+*/")", Slim, GUILayout.ExpandWidth (true))){
									if ( EditorMode ){
										Selection.activeObject = Lib.gameObject;
										EditorVariables.DKSlotLibraryObj = Lib.gameObject;
										Editor_Global.Variables.DKSlotLibraryObj = Lib.gameObject;
										EditorVariables._DKSlotLibrary = Lib.gameObject.GetComponent<DKSlotLibrary>();
										EditorVariables.DK_UMACrowd.slotLibrary = EditorVariables._DKSlotLibrary;
									}
								}
								GUI.color = Color.cyan;
								if ( Lib.gameObject.GetComponent<DKSlotLibrary>().slotElementList.Length > 0 )//{
									if ( CurrentLibrary && CurrentLibrary.gameObject.GetComponent<DKSlotLibrary>() == true 
									    && Lib.gameObject.GetComponent<DKSlotLibrary>() == true 
									    && Lib.gameObject.GetComponent<DKSlotLibrary>().slotElementList.Length > 0 )

									if ( GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
									{
										if ( Action == "" ){
											EditorVariables.DKSlotLibraryObj = Lib.gameObject;
											Editor_Global.Variables.DKSlotLibraryObj = Lib.gameObject;
											EditorVariables._DKSlotLibrary = Lib.gameObject.GetComponent<DKSlotLibrary>();
											EditorVariables.DK_UMACrowd.slotLibrary = EditorVariables._DKSlotLibrary;
										}
									}


								GUI.color = new Color (0.9f, 0.5f, 0.5f);
								if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
								{
									DestroyImmediate( Lib.gameObject );
								}
							}
						}
					}
					foreach (Transform Lib in UMA.transform)
					{
						if ( Lib && ( Lib.gameObject.GetComponent<DynamicSlotLibrary>() == true
						&& ( Lib.name.ToLower().Contains(SearchString.ToLower()) || SearchString == "" ))){
							using (new Horizontal()) {
								// Prefab
								// Prefab Verification
								string myPath;
								if ( PrefabUtility.GetPrefabParent( Lib.gameObject ) != null )
									myPath = PrefabUtility.GetPrefabParent( Lib.gameObject ).ToString() ;
								else myPath = "null";
								if ( myPath == "null" ) GUI.color = Color.gray;
								else GUI.color = Color.cyan;
								if ( GUILayout.Button ( new GUIContent(_Prefab), "toolbarbutton", GUILayout.ExpandWidth (false))) {
									
									// Create Prefab
									if ( myPath == "null" ) 
									{
										PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Slot Libraries/" + Lib.name + ".prefab", Lib.gameObject,ReplacePrefabOptions.ConnectToPrefab  );
										
									}
									else {
										Selection.activeGameObject = Lib.gameObject;
										DelAsset();
									}
								}
								GUI.color = Color.yellow;
								if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
								{
									Selection.activeGameObject = Lib.gameObject;
									Duplicating();
								}
								try {
									if ( Selection.activeObject == Lib.gameObject )GUI.color = Color.yellow;
									else GUI.color = Color.white; 
									if ( Lib && Lib.gameObject && GUILayout.Button ( Lib.name+"( UMA )", Slim, GUILayout.ExpandWidth (true))){
										if ( EditorMode ){
											Selection.activeObject = Lib.gameObject;
										
										}
									}
									GUI.color = Color.white;
									if ( CurrentLibrary && CurrentLibrary.gameObject.GetComponent<DynamicSlotLibrary>() == true ) 
									{
										if (Action == "" && GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
										{
											if ( Action == "" ){
												DKUMA_Variables._SlotLibrary = Lib.gameObject.GetComponent<DynamicSlotLibrary>();
												this.Close ();
											}
											else if ( EditorMode ){
												Selection.activeGameObject = Lib.gameObject ;
												this.Close ();
											}
										}
									
									}

									GUI.color = new Color (0.9f, 0.5f, 0.5f);
									if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
									{
										if ( Lib && Lib.gameObject ) DestroyImmediate( Lib.gameObject );
									}
								}catch(MissingReferenceException){}
							}
						}
					}
				}
			
				if ( ShowOverlays ) {
					GUI.color = Color.yellow; 
					GUILayout.Label ( "Overlay Libraries :", "toolbarbutton", GUILayout.ExpandWidth (true));
					DKOverlayLibrary[]  LibraryList = FindObjectsOfType(typeof(DKOverlayLibrary)) as DKOverlayLibrary[];
					List<string> NamesList = new List<string>();
					for(int i = 0; i < LibraryList.Length; i ++){
						if ( PrefabUtility.GetPrefabParent( LibraryList[i].gameObject ) != null )
							NamesList.Add(PrefabUtility.GetPrefabParent( LibraryList[i].gameObject ).name+".prefab");
						if ( PrefabUtility.GetPrefabType(LibraryList[i]) != null 
						    && PrefabUtility.GetPrefabObject(LibraryList[i]) == null){
						}
					}
					DirectoryInfo dir = new DirectoryInfo("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Overlay Libraries/");
					FileInfo[] info = dir.GetFiles("*.prefab");
					foreach (FileInfo f in info)
					{
						if ( NamesList.Contains(f.Name) == false ) 
						using (new Horizontal()) {
							GUI.color = Color.cyan;
							if ( GUILayout.Button ( new GUIContent(_Inst), GUILayout.ExpandWidth (false))){
								string myPath = ("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Overlay Libraries/"+f.Name.ToString());
								GameObject New =  PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(myPath , typeof(GameObject))  ) as GameObject;
								New.transform.parent = EditorVariables.UMAObj.transform;
								PrefabUtility.ReconnectToLastPrefab(New);
							}
							if ( GUILayout.Button ( f.Name, Slim, GUILayout.ExpandWidth (false))) {
								
							}
						}
					}

					foreach (Transform Lib in DK_UMA.transform){
						if ( Lib && ( Lib.gameObject.GetComponent<DKOverlayLibrary>() == true 
						&& ( Lib.name.ToLower().Contains(SearchString.ToLower()) || SearchString == "" ))){
							using (new Horizontal()) {
								// Prefab
								// Prefab Verification
								string myPath;
								if ( PrefabUtility.GetPrefabParent( Lib.gameObject ) != null )
									myPath = PrefabUtility.GetPrefabParent( Lib.gameObject ).ToString() ;
								else myPath = "null";
								if ( myPath == "null" ) GUI.color = Color.gray;
								else GUI.color = Color.cyan;
								if ( GUILayout.Button ( new GUIContent(_Prefab), "toolbarbutton", GUILayout.ExpandWidth (false))) {
									
									// Create Prefab
									if ( myPath == "null" ) 
									{
										PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Overlay Libraries/" + Lib.name + ".prefab", Lib.gameObject,ReplacePrefabOptions.ConnectToPrefab  );
										
									}
									else {
										Selection.activeGameObject = Lib.gameObject;
										DelAsset();
										
									}
								}
								GUI.color = Color.yellow; 
								if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
								{
									Selection.activeGameObject = Lib.gameObject;
									Duplicating();
									
								}
								GUI.color = Color.white;
								if (GUILayout.Button ( Lib.name+"( DK "+Lib.gameObject.GetComponent<DKOverlayLibrary>().overlayElementList.Length+")", Slim, GUILayout.ExpandWidth (true))){
									if ( EditorMode ){
										Selection.activeObject = Lib.gameObject;
										EditorVariables.OverlayLibraryObj = Lib.gameObject;
										Editor_Global.Variables.OverlayLibraryObj = Lib.gameObject;
										EditorVariables._OverlayLibrary = Lib.gameObject.GetComponent<DKOverlayLibrary>();
										EditorVariables.DK_UMACrowd.overlayLibrary = EditorVariables._OverlayLibrary;
									}
								}
								GUI.color = Color.cyan;
								if ( Lib.gameObject.GetComponent<DKOverlayLibrary>().overlayElementList.Length > 0 ){
									if ( CurrentLibrary && CurrentLibrary.gameObject.GetComponent<DKOverlayLibrary>() == true 
								    && Lib.gameObject.GetComponent<DKOverlayLibrary>() == true 
								    && Lib.gameObject.GetComponent<DKOverlayLibrary>().overlayElementList.Length > 0 )
									if ( Action == "" && GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
									{
										EditorVariables.OverlayLibraryObj = Lib.gameObject;
										Editor_Global.Variables.OverlayLibraryObj = Lib.gameObject;
										EditorVariables._OverlayLibrary = Lib.gameObject.GetComponent<DKOverlayLibrary>();
										EditorVariables.DK_UMACrowd.overlayLibrary = EditorVariables._OverlayLibrary;
										this.Close ();
									}

								}
							
								GUI.color = new Color (0.9f, 0.5f, 0.5f);
								if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
								{
									if ( Lib && Lib.gameObject ) DestroyImmediate( Lib.gameObject );
								}
							}
						}
					}
					foreach (Transform Lib in UMA.transform)
					{
						if ( Lib && ( Lib.gameObject.GetComponent<DynamicOverlayLibrary>() == true
						&& ( Lib.name.ToLower().Contains(SearchString.ToLower()) || SearchString == "" ))){
							using (new Horizontal()) {
								// Prefab
								// Prefab Verification
								string myPath;
								if ( PrefabUtility.GetPrefabParent( Lib.gameObject ) != null )
									myPath = PrefabUtility.GetPrefabParent( Lib.gameObject ).ToString() ;
								else myPath = "null";
								if ( myPath == "null" ) GUI.color = Color.gray;
								else GUI.color = Color.cyan;
								if ( GUILayout.Button ( new GUIContent(_Prefab), "toolbarbutton", GUILayout.ExpandWidth (false))) {
									
									// Create Prefab
									if ( myPath == "null" ) 
									{
										PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Overlay Libraries/" + Lib.name + ".prefab", Lib.gameObject,ReplacePrefabOptions.ConnectToPrefab  );
									}
									else {
										Selection.activeGameObject = Lib.gameObject;
										DelAsset();
									}
								}
								GUI.color = Color.yellow;
								if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
								{
									Selection.activeGameObject = Lib.gameObject;
									Duplicating();
								}
								try {
									if ( Selection.activeObject == Lib.gameObject )GUI.color = Color.yellow;
									else GUI.color = Color.white; 
									if (GUILayout.Button ( Lib.name+"( UMA "+/*Lib.gameObject.GetComponent<OverlayLibrary>().overlayElementList.Length+*/")", Slim, GUILayout.ExpandWidth (true))){
										if ( EditorMode ){
											Selection.activeObject = Lib.gameObject;
										//	EditorVariables.OverlayLibraryObj = Lib.gameObject;
										//	Editor_Global.Variables.OverlayLibraryObj = Lib.gameObject;
										//	EditorVariables._OverlayLibrary = Lib.gameObject.GetComponent<OverlayLibrary>();
										//	EditorVariables.DK_UMACrowd.overlayLibrary = EditorVariables._OverlayLibrary;
										}
									}

									if ( CurrentLibrary && CurrentLibrary.gameObject.GetComponent<DynamicOverlayLibrary>() == true ){
										GUI.color = Color.white;
										if ( Action == "" && GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
										{
											if ( Action == "" ){
												DKUMA_Variables._OverlayLibrary = Lib.gameObject.GetComponent<DynamicOverlayLibrary>();
												this.Close ();
											}
										}
										if ( Action == "MoveTo" && GUILayout.Button ( "Move To", GUILayout.ExpandWidth (false))) 
										{
										//	DK_UMA_Browser.MoveElement( Selection.activeObject, Lib.gameObject);
											this.Close ();
										}
									}

									GUI.color = new Color (0.9f, 0.5f, 0.5f);
									if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
									{
										DestroyImmediate( Lib.gameObject );
									}
								}catch(MissingReferenceException){}
							}
						}
					}
				}
			}
		}

		// list
		if ( (DK_UMA || UMA) && !EditorMode ) {
			GUI.color = Color.white;
			GUILayout.Label ( "List", "toolbarbutton", GUILayout.ExpandWidth (true));
			using (new ScrollView(ref scroll)){
				if ( CurrentLibrary ){
					foreach (Transform Lib in DK_UMA.transform)
					{
						if ( CurrentLibrary.gameObject.GetComponent<DKRaceLibrary>() == true 
						    && Lib.gameObject.GetComponent<DKRaceLibrary>() == true ) 
							
						using (new Horizontal()) {
							GUI.color = new Color (0.9f, 0.5f, 0.5f);
							if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
							{
								DestroyImmediate( Lib.gameObject );
							}
							GUILayout.Label ( Lib.name, GUILayout.ExpandWidth (true));
							GUI.color = Color.white;
							if ( Lib.gameObject.GetComponent<DKRaceLibrary>().raceElementList.Length > 0 ){
								if ( GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
								{
									if ( Action == "" ){
										EditorVariables.RaceLibraryObj = Lib.gameObject;
										Editor_Global.Variables.RaceLibraryObj = Lib.gameObject;
										EditorVariables._RaceLibrary = Lib.gameObject.GetComponent<DKRaceLibrary>();
										EditorVariables.DK_UMACrowd.raceLibrary = EditorVariables._RaceLibrary;
									}
									
								}
							}
							else {
								GUI.color = Color.yellow;
								GUILayout.Label ( "Is umpty", GUILayout.ExpandWidth (false));
							}
						}
						if ( CurrentLibrary.gameObject.GetComponent<DKSlotLibrary>() == true 
						&& Lib.gameObject.GetComponent<DKSlotLibrary>() == true ) 
						
							using (new Horizontal()) {
							GUI.color = new Color (0.9f, 0.5f, 0.5f);
							if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
							{
								DestroyImmediate( Lib.gameObject );
							}
							GUILayout.Label ( Lib.name, GUILayout.ExpandWidth (true));
							GUI.color = Color.white;
							if ( Lib.gameObject.GetComponent<DKSlotLibrary>().slotElementList.Length > 0 ){
								if ( GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
								{
									if ( Action == "" ){
										EditorVariables.DKSlotLibraryObj = Lib.gameObject;
										Editor_Global.Variables.DKSlotLibraryObj = Lib.gameObject;
										EditorVariables._DKSlotLibrary = Lib.gameObject.GetComponent<DKSlotLibrary>();
										EditorVariables.DK_UMACrowd.slotLibrary = EditorVariables._DKSlotLibrary;
									}
								
								}

							}
							else {
								GUI.color = Color.yellow;
								GUILayout.Label ( "Is umpty", GUILayout.ExpandWidth (false));
							}
						}
						if ( CurrentLibrary.gameObject.GetComponent<DKOverlayLibrary>() == true 
						    && Lib.gameObject.GetComponent<DKOverlayLibrary>() == true ) 
						using (new Horizontal()) {
							if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
							{
								Selection.activeGameObject = Lib.gameObject;
								Duplicating();
							}
							GUI.color = Color.white;
							GUILayout.Label ( Lib.name, GUILayout.ExpandWidth (true));
							if ( Lib.gameObject.GetComponent<DKOverlayLibrary>().overlayElementList.Length > 0 ){
								if ( Action == "" && GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
								{
									if ( Action == "" ){
										EditorVariables.OverlayLibraryObj = Lib.gameObject;
										Editor_Global.Variables.OverlayLibraryObj = Lib.gameObject;
										EditorVariables._OverlayLibrary = Lib.gameObject.GetComponent<DKOverlayLibrary>();
										EditorVariables.DK_UMACrowd.overlayLibrary = EditorVariables._OverlayLibrary;
										this.Close ();
									}
									
								
								}
							}
						}
					}
					foreach (Transform Lib in UMA.transform)
					{
						if ( CurrentLibrary.gameObject.GetComponent<DynamicRaceLibrary>() == true 
							&& Lib.gameObject.GetComponent<DynamicRaceLibrary>() == true ) 
						using (new Horizontal()) {
							GUI.color = new Color (0.9f, 0.5f, 0.5f);
							if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
							{
								DestroyImmediate( Lib.gameObject );
							}
							GUI.color = Color.yellow;
							if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
							{
								Selection.activeGameObject = Lib.gameObject;
								Duplicating();
							}
							try {
								if ( Selection.activeObject == Lib.gameObject )GUI.color = Color.yellow;
								else GUI.color = Color.white; 
								if ( GUILayout.Button ( Lib.name+"( UMA "+/*Lib.gameObject.GetComponent<RaceLibrary>().raceElementList.Length+*/")", Slim, GUILayout.ExpandWidth (true))){
									Selection.activeObject = Lib.gameObject;
								}
								GUI.color = Color.white; 
									if ( Lib.gameObject.GetComponent<DynamicRaceLibrary>().GetAllRaces().Length >  0 ){
									if (Action == "" && GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
									{
										if ( Action == "" ){
												DKUMA_Variables._RaceLibrary = Lib.gameObject.GetComponent<DynamicRaceLibrary>();
											this.Close ();
										}
										else if ( EditorMode ){
											Selection.activeGameObject = Lib.gameObject ;
											this.Close ();
										}
									}


									GUI.color = Color.white; 
									if ( Action == "MoveTo" && GUILayout.Button ( "Move To", GUILayout.ExpandWidth (false))) 
									{
									//	DK_UMA_Browser.MoveElement( Selection.activeObject, Lib.gameObject);
										this.Close ();
									}								
								}
								else {
									GUI.color = Color.yellow;
									GUILayout.Label ( "Is umpty", GUILayout.ExpandWidth (false));
								}
							}catch(MissingReferenceException){}
						}
						if ( CurrentLibrary.gameObject.GetComponent<DynamicSlotLibrary>() == true 
							&& Lib.gameObject.GetComponent<DynamicSlotLibrary>() == true ) 
						using (new Horizontal()) {
							GUI.color = new Color (0.9f, 0.5f, 0.5f);
							if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
							{
								DestroyImmediate( Lib.gameObject );
							}
							try {
								if ( Selection.activeObject == Lib.gameObject )GUI.color = Color.yellow;
								else GUI.color = Color.white; 
								if ( GUILayout.Button ( Lib.name+"()", Slim, GUILayout.ExpandWidth (true))){
									Selection.activeObject = Lib.gameObject;
								}
								GUI.color = Color.white; 
									if ( Lib.gameObject.GetComponent<DynamicSlotLibrary>().GetAllSlotAssets().Length > 0 ){
									if (Action == "" && GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
									{
										if ( Action == "" ){
												DKUMA_Variables._SlotLibrary = Lib.gameObject.GetComponent<DynamicSlotLibrary>();
											this.Close ();
										}
										else if ( EditorMode ){
											Selection.activeGameObject = Lib.gameObject ;
											this.Close ();
										}
									}
									GUI.color = Color.yellow;
									if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
									{
										Selection.activeGameObject = Lib.gameObject;
										Duplicating();
									}
									GUI.color = Color.white; 
									if ( Action == "MoveTo" && GUILayout.Button ( "Move To", GUILayout.ExpandWidth (false))) 
									{
									//	DK_UMA_Browser.MoveElement( Selection.activeObject, Lib.gameObject);
										this.Close ();
									}								
								}
								else {
									GUI.color = Color.yellow;
									GUILayout.Label ( "Is umpty", GUILayout.ExpandWidth (false));
								}
							}catch(MissingReferenceException){}
						}
						if ( CurrentLibrary.gameObject.GetComponent<OverlayLibrary>() == true 
						    && Lib.gameObject.GetComponent<OverlayLibrary>() == true ) 
						using (new Horizontal()) {
							GUI.color = new Color (0.9f, 0.5f, 0.5f);
							if ( EditorMode && GUILayout.Button( new GUIContent(_Delete), Slim, GUILayout.ExpandWidth (false)))
							{
								DestroyImmediate( Lib.gameObject );
							}
							try {
								if ( Selection.activeObject == Lib.gameObject )GUI.color = Color.yellow;
								else GUI.color = Color.white; 
								if (GUILayout.Button ( Lib.name+"("+/*Lib.gameObject.GetComponent<OverlayLibrary>().overlayElementList.Length+*/")", Slim, GUILayout.ExpandWidth (true))){
									Selection.activeObject = Lib.gameObject;
								}
									if ( Lib.gameObject.GetComponent<DynamicOverlayLibrary>().GetAllOverlayAssets().Length > 0 ){
									GUI.color = Color.white;
									if ( Action == "" && GUILayout.Button ( "Assign", GUILayout.ExpandWidth (false))) 
									{
										if ( Action == "" ){
												DKUMA_Variables._OverlayLibrary = Lib.gameObject.GetComponent<DynamicOverlayLibrary>();
											this.Close ();
										}
									}
									if ( Action == "MoveTo" && GUILayout.Button ( "Move To", GUILayout.ExpandWidth (false))) 
									{
									//	DK_UMA_Browser.MoveElement( Selection.activeObject, Lib.gameObject);
										this.Close ();
									}
									GUI.color = Color.yellow;
									if ( EditorMode && GUILayout.Button ( new GUIContent(_Duplic), "toolbarbutton", GUILayout.ExpandWidth (false))) 
									{
										Selection.activeGameObject = Lib.gameObject;
										Duplicating();
									}
								}
								else {
									GUI.color = Color.yellow;
									GUILayout.Label ( "Is umpty", GUILayout.ExpandWidth (false));
								}
						
							}catch(MissingReferenceException){}
						}
					}
				}
			}
		}
	}

	void DelAsset(){
		DeleteAsset.Action = "";
		DeleteAsset.ProcessName = "Delete Asset";
		DeleteAsset.MultiUMAModel = false;
		DeleteAsset.UMAModel = true;
		DK_UMA_Editor.OpenDeleteAsset();
	}

	void Duplicating (){
		GameObject _Copy = Instantiate(Selection.activeGameObject) as GameObject;
		_Copy.name = Selection.activeGameObject.name + " (Copy)";
		_Copy.transform.parent = Selection.activeGameObject.transform.parent;
		_Copy.transform.position = new Vector3(0, 0, 0 );
		Selection.activeGameObject = _Copy.gameObject;
	}

	void AutoDel () {
		List<DKRaceData> tmpList0 = new List<DKRaceData>();
		tmpList0 = EditorVariables.DK_UMACrowd.raceLibrary.raceElementList.ToList();
		for(int i = 0; i < tmpList0.Count; i ++){
			if ( tmpList0[i] == null ) tmpList0.Remove(tmpList0[i]);
		}
		EditorVariables.DK_UMACrowd.raceLibrary.raceElementList = tmpList0.ToArray();
		List<DKSlotData> tmpList = new List<DKSlotData>();
		tmpList = EditorVariables.DK_UMACrowd.slotLibrary.slotElementList.ToList();
		for(int i = 0; i < tmpList.Count; i ++){
			if ( tmpList[i] == null ) tmpList.Remove(tmpList[i]);
		}
		EditorVariables.DK_UMACrowd.slotLibrary.slotElementList = tmpList.ToArray();
		
		List<DKOverlayData> tmpList2 = new List<DKOverlayData>();
		tmpList2 = EditorVariables.DK_UMACrowd.overlayLibrary.overlayElementList.ToList();
		for(int i = 0; i < tmpList2.Count; i ++){
			if ( tmpList2[i] == null ) tmpList2.Remove(tmpList2[i]);
		}
		EditorVariables.DK_UMACrowd.overlayLibrary.overlayElementList = tmpList2.ToArray();
		
	}


}
