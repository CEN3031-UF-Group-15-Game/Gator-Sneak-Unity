using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0472 // Null is true.

public class Anatomy_Editor : EditorWindow {
	#region Variables
	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);
	bool Helper = false;
	bool ShowSelectAnatomy = false;
	DK_SlotsAnatomyElement SelectedAnaPart;
	GameObject SelectedAnaPartObj;
	string NewAnatomyName = "";
	bool ChooseLink = false;
	DK_SlotExpressionsElement AnaToLink;
	string NewPresetName =  "";
	string NewPresetGender =  "Both";
	DK_Race DK_Race;
	string _SpawnPerct = "";
	string SearchString = "";
	string SelectedRace = "Both";
	string SelectedPlace = "";
	string OverlayType =  "";
	string SelectedPrefabName = "";
	public static GameObject SlotsAnatomyLibraryObj;	
	public static DK_SlotsAnatomyLibrary _SlotsAnatomyLibrary;
	public static DK_SlotsAnatomyLibrary TMP_SlotsAnatomyLibrary;
	public static GameObject DK_UMA;
	bool ElemAlreadyIn = false;
	Vector2 scroll;
	string myPath = "";

	#endregion Variables

	public static void OpenRaceSelectEditor()
	{
		GetWindow(typeof(DK_UMA_RaceSelect_Editor), false, "Races List");
	}

	void OnGUI (){
		this.minSize = new Vector2(320, 500);
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

		if ( _SlotsAnatomyLibrary == null && EditorVariables.SlotsAnatomyLibraryObj ) 
			_SlotsAnatomyLibrary =  EditorVariables.SlotsAnatomyLibraryObj.GetComponent<DK_SlotsAnatomyLibrary>();

		GUILayout.Space(5);
		using (new Horizontal()) {
			GUI.color = Color.white ;
			GUILayout.Label("Modify Anatomy Slots", "toolbarbutton", GUILayout.ExpandWidth (true));
			GUI.color = Red;
		//	if (  GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {

		//	}
		}
		GUILayout.Space(5);
		GUI.color = Color.white ;
		if ( Helper ) GUILayout.TextField("The Anatomy Parts are used during the creation of the UMA Model. " +
		                                  "The engine check all the Anatomy parts to select the torso and its slot and overlays. " +
		                                  "Each part determines what slot Element will be generated to be placed on it. " +
		                                  "It is also used by the Overlay Element to associate with a slot." , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		
		if ( SelectedAnaPart != null ){
			using (new Horizontal()) {
				GUI.color = Color.white ;
				GUILayout.Label("Anatomy Part :", GUILayout.ExpandWidth (false));
				if ( NewAnatomyName != "" ) GUI.color = Green;
				else GUI.color = Red;
				NewAnatomyName = GUILayout.TextField(NewAnatomyName, 50, GUILayout.ExpandWidth (true));
				if (  GUILayout.Button ( "Change", GUILayout.ExpandWidth (false))) {
					Debug.Log ( "Anatomy " +SelectedAnaPart.name+ " changed to " +NewAnatomyName+ ".");
					DK_SlotsAnatomyElement _DK_SlotsAnatomyElement = SelectedAnaPart.GetComponent<DK_SlotsAnatomyElement>();
					_DK_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = NewAnatomyName;
					SelectedAnaPart.name = NewAnatomyName;
				//	SelectedAnaPart.gameObject.name = NewAnatomyName;
					Selection.activeObject.name = NewAnatomyName;
					AssetDatabase.RenameAsset ( AssetDatabase.GetAssetPath(Selection.activeObject), NewAnatomyName+".prefab");
					Debug.Log ( AssetDatabase.GetAssetPath(Selection.activeObject));
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
			}
			GUI.color = Color.yellow ;
			if ( Helper ) GUILayout.TextField("You can link 2 anatomy parts and configure a slot to remove the linked Anatomy part (ex.: Remove the feet when generating a boots slot)." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			
			if ( SelectedAnaPart != null ) using (new Horizontal()) {
				GUI.color = Color.white ;
				GUILayout.Label("Link to :", GUILayout.ExpandWidth (false));
				if ( SelectedAnaPart.GetComponent<DK_SlotsAnatomyElement>().dk_SlotsAnatomyElement.Place != null ) GUILayout.TextField(SelectedAnaPart.GetComponent<DK_SlotsAnatomyElement>().dk_SlotsAnatomyElement.Place.name , 50, GUILayout.ExpandWidth (true));
				else {
					GUI.color = Color.white ;
					GUILayout.Label("No Link", GUILayout.ExpandWidth (true));
				}
				if ( !ChooseLink ){
					if (GUILayout.Button ( "Select", GUILayout.ExpandWidth (false))) {
						ChooseLink = true;
					}
					GUI.color = Red;
					if (GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) {
						SelectedAnaPart.GetComponent<DK_SlotsAnatomyElement>().dk_SlotsAnatomyElement.Place = null;
					}
				}
				else {
					GUI.color = Color.yellow ;
					GUILayout.Label("Click an Element from the list bellow.", GUILayout.ExpandWidth (true));
					GUI.color = Color.white ;
					if (GUILayout.Button ( "Cancel", GUILayout.ExpandWidth (false))) {
						ChooseLink = false;
					}
				}
			}
			// Gender
			GUI.color = Color.yellow;
			if ( SelectedAnaPart != null && Helper ) GUILayout.TextField("You can specify a Gender or let it be usable by both (Both is recommended)." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			if ( SelectedAnaPart != null ) using (new Horizontal()) {
				if ( NewPresetGender == "Both" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Both", GUILayout.ExpandWidth (true))) {
					NewPresetGender = "Both";
					DK_Race = SelectedAnaPart.GetComponent("DK_Race") as DK_Race;
					DK_Race.Gender = NewPresetGender;
					DK_SlotsAnatomyElement _DK_SlotsAnatomyElement = SelectedAnaPart.GetComponent<DK_SlotsAnatomyElement>();
					_DK_SlotsAnatomyElement.dk_SlotsAnatomyElement.ForGender = NewPresetGender;
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
				if ( NewPresetGender == "Female" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Female", GUILayout.ExpandWidth (true))) {
					NewPresetGender = "Female";
					DK_Race = SelectedAnaPart.GetComponent("DK_Race") as DK_Race;
					DK_Race.Gender = NewPresetGender;
					DK_SlotsAnatomyElement _DK_SlotsAnatomyElement = SelectedAnaPart.GetComponent<DK_SlotsAnatomyElement>();
					_DK_SlotsAnatomyElement.dk_SlotsAnatomyElement.ForGender = NewPresetGender;
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
				if ( NewPresetGender == "Male" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Male", GUILayout.ExpandWidth (true))) {
					NewPresetGender = "Male";
					DK_Race = SelectedAnaPart.GetComponent("DK_Race") as DK_Race;
					DK_Race.Gender = NewPresetGender;
					DK_SlotsAnatomyElement _DK_SlotsAnatomyElement = SelectedAnaPart.GetComponent<DK_SlotsAnatomyElement>();
					_DK_SlotsAnatomyElement.dk_SlotsAnatomyElement.ForGender = NewPresetGender;
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
			}
			GUI.color = Color.yellow ;
			if ( Helper ) GUILayout.TextField("Select the race(s) for the Anatomy Part." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			if ( Selection.activeGameObject != null ) using (new Horizontal()) {
				GUI.color = Color.white ;
				GUILayout.Label("For Race :", GUILayout.ExpandWidth (false));
				if ( NewAnatomyName != "" ) GUI.color = Green;
				else GUI.color = Red;
				if ( GUILayout.Button ( "Open List", GUILayout.ExpandWidth (true))) {
					OpenRaceSelectEditor();
				}
				GUILayout.Label("Spawn %", GUILayout.ExpandWidth (false));
				if ( _SpawnPerct != "" && _SpawnPerct != null  ) {
					_SpawnPerct = GUILayout.TextField(_SpawnPerct, 5, GUILayout.Width (30));
					_SpawnPerct = Regex.Replace(_SpawnPerct, "[^0-9]", "");
				}
				if ( _SpawnPerct == "" ) _SpawnPerct = "1";
				
				if ( Convert.ToInt32(_SpawnPerct) > 100 ) _SpawnPerct = "100";
				if (  GUILayout.Button ( "Change", GUILayout.ExpandWidth (false))) {
					DK_Race _DK_Race = Selection.activeGameObject.GetComponent< DK_Race >();	
					Selection.activeGameObject.GetComponent< DK_SlotsAnatomyData >().SpawnPerct = Convert.ToInt32(_SpawnPerct);
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
			}
			GUILayout.Space(5);
			// Head ?
			GUI.color = Color.yellow;
			if ( Helper ) GUILayout.TextField("The 'Head' part is used by the generator to place all the face elements such as the hair or the beard. Beware You need to assign a single 'Head' Anatomy part to only one slot." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label ( "Is ", GUILayout.ExpandWidth (false)); 
				if ( OverlayType == "Is Head Part" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Head", GUILayout.ExpandWidth (true))) {
					OverlayType = "Is Head Part";
					DK_Race = SelectedAnaPart.GetComponent("DK_Race") as DK_Race;
					DK_Race.OverlayType = OverlayType;
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
					
				}
				GUI.color = Color.white;
				GUILayout.Label ( "? Is ", GUILayout.ExpandWidth (false)); 
				if ( OverlayType == "Is Head Elem" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Head Elem", GUILayout.ExpandWidth (true))) {
					OverlayType = "Is Head Elem";
					DK_Race = SelectedAnaPart.GetComponent("DK_Race") as DK_Race;
					DK_Race.OverlayType = OverlayType;
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
					
				}
				GUI.color = Color.white;
				GUILayout.Label ( "? Is ", GUILayout.ExpandWidth (false)); 
				if ( OverlayType == "Is Torso Part" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Torso", GUILayout.ExpandWidth (true))) {
					OverlayType = "Is Torso Part";
					DK_Race = SelectedAnaPart.GetComponent("DK_Race") as DK_Race;
					DK_Race.OverlayType = OverlayType;
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
				GUI.color = Color.white;
				GUILayout.Label ( "?", GUILayout.ExpandWidth (false)); 
			}
			// Overlay Type
			GUI.color = Color.yellow;
			if ( Helper ) GUILayout.TextField("The Overlay Type is used by the Generator during the Model's creation. " +
			                                  "All the 'Naked body parts' must be of the 'Flesh' Type, the Head slot must be of the 'Face' type." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label ( "Overlay Type :", GUILayout.ExpandWidth (false));
				if ( OverlayType == "Flesh" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Flesh", GUILayout.ExpandWidth (true))) {
					OverlayType = "Flesh";
					DK_Race _DK_Race = SelectedAnaPart.gameObject.GetComponent<DK_Race>();
					_DK_Race.OverlayType = "Flesh"; 
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
				if ( OverlayType == "Face" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Face", GUILayout.ExpandWidth (true))) {
					OverlayType = "Face";
					DK_Race _DK_Race = SelectedAnaPart.gameObject.GetComponent<DK_Race>();
					_DK_Race.OverlayType = "Face"; 
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
				if ( OverlayType == "Eyes" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Eyes", GUILayout.ExpandWidth (true))) {
					OverlayType = "Eyes";
					DK_Race _DK_Race = SelectedAnaPart.gameObject.GetComponent<DK_Race>();
					_DK_Race.OverlayType = "Eyes"; 
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
				if ( OverlayType == "Hair" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Hair", GUILayout.ExpandWidth (true))) {
					OverlayType = "Hair";
					DK_Race _DK_Race = SelectedAnaPart.gameObject.GetComponent<DK_Race>();
					_DK_Race.OverlayType = "Hair"; 
					EditorUtility.SetDirty(SelectedAnaPart.gameObject);
					AssetDatabase.SaveAssets();
				}
			}
		}
		// List
		GUI.color = Color.yellow ;
		if ( Helper ) GUILayout.TextField("Select an Anatomy Part from the following list. The 'P' letter is about Prefab, gray means that the Anatomy has no Prefab, cyan means that it is ok. Click on a gray 'P' to create a prefab." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		using (new Horizontal()) {
			GUI.color = Color.white ;
			if (  GUILayout.Button ( "Add New", GUILayout.ExpandWidth (true))) {
				GameObject NewExpressionObj =  (GameObject) Instantiate(Resources.Load("New_AnatomyPart"), Vector3.zero, Quaternion.identity);
				NewExpressionObj.name = "New Anatomy Part (Rename)";
				Selection.activeGameObject = NewExpressionObj;
				PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy/" + NewExpressionObj.name + ".prefab", NewExpressionObj.gameObject );
				
				if ( _SlotsAnatomyLibrary ) DestroyImmediate (_SlotsAnatomyLibrary.gameObject);
				SlotsAnatomyLibraryObj = (GameObject) Instantiate(Resources.Load("DK_SlotsAnatomyLibrary"), Vector3.zero, Quaternion.identity);
				SlotsAnatomyLibraryObj.name = "DK_SlotsAnatomyLibrary";
				_SlotsAnatomyLibrary =  SlotsAnatomyLibraryObj.GetComponent<DK_SlotsAnatomyLibrary>();
				DK_UMA = GameObject.Find("DK_UMA");
				if ( DK_UMA == null ) {
					var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
					DK_UMA = GameObject.Find("DK_UMA");
				}
				SlotsAnatomyLibraryObj.transform.parent = DK_UMA.transform;
				// Find prefabs
				DirectoryInfo dir = new DirectoryInfo("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy");
				// Assign Prefabs
				FileInfo[] info = dir.GetFiles("*.prefab");
				info.Select(f => f.FullName).ToArray();
				// Action
				// Verify Folders objects
				GameObject SlotsAnatomy = GameObject.Find("Slots Anatomy");
				if ( DK_UMA == null ) {
					var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
					var goSlotsAnatomy = new GameObject();	goSlotsAnatomy.name = "Slots Anatomy";
					goSlotsAnatomy.transform.parent = goDK_UMA.transform;
				}
				else if ( SlotsAnatomy == null ) {
					SlotsAnatomy = new GameObject();	SlotsAnatomy.name = "Slots Anatomy";
					SlotsAnatomy.transform.parent = DK_UMA.transform;
				}
				SlotsAnatomy = GameObject.Find("Slots Anatomy");
				foreach (FileInfo f in info)
				{
					// Instantiate the Element Prefab
					GameObject clone = PrefabUtility.InstantiateAttachedAsset( AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy/"+ f.Name, typeof(GameObject)) ) as GameObject;
					clone.name = f.Name;
					if ( clone.name.Contains(".prefab")) clone.name = clone.name.Replace(".prefab", "");
					foreach ( Transform child in SlotsAnatomy.transform )
					{
						if ( clone != null && clone.name == child.name ) GameObject.DestroyImmediate( clone );
					}
					if ( clone != null )
					{
						clone.transform.parent = SlotsAnatomy.transform;
						// add the Element to the List		
						DK_SlotsAnatomyElement _SlotsAnatomyElement =  clone.GetComponent<DK_SlotsAnatomyElement>();
						_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = clone.name;
						if ( _SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn == false )
						{
							for (int i = 0; i < _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length ; i++)
							{
								if (_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement != null && _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.ElemAlreadyIn == true )
								{
									
								}
							}
							
							if ( _SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn == false )
							{
								var list = new DK_SlotsAnatomyElement[_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length + 1];
								Array.Copy(_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList, list, _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length );
								
								GameObject _Prefab = PrefabUtility.GetPrefabParent( _SlotsAnatomyElement.gameObject) as GameObject;	
								DK_SlotsAnatomyElement dk_SlotsAnatomyElement = _Prefab.GetComponent<DK_SlotsAnatomyElement>();
								// add to list
								list[_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length] = dk_SlotsAnatomyElement;
								_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList = list;
								// modify dk_SlotsAnatomyName if necessary
								if  ( dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName == "" ) dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = dk_SlotsAnatomyElement.transform.name;
								// Add to Dictonary
								if ( !ElemAlreadyIn )
								{
									_SlotsAnatomyLibrary.dk_SlotsAnatomyDictionary.Add( _SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName, dk_SlotsAnatomyElement.dk_SlotsAnatomyElement );
									dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn = false;
								}
							}
						}
					}
				}
				DestroyImmediate (SlotsAnatomy);
				DestroyImmediate ( NewExpressionObj);
			}
			// Copy
			if (  GUILayout.Button ( "Copy Selected", GUILayout.ExpandWidth (true))) {
				string Path = "Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy/";
				SelectedAnaPart.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = SelectedAnaPart.name + " (Copy)";
				AssetDatabase.CopyAsset(Path + SelectedAnaPart.gameObject.name + ".prefab", Path +SelectedAnaPart.gameObject.name + " (Copy).prefab" );
				SelectedAnaPart.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = SelectedAnaPart.dk_SlotsAnatomyElement.dk_SlotsAnatomyName.Replace(" (Copy)", "");
				AssetDatabase.Refresh();
				if ( _SlotsAnatomyLibrary ) DestroyImmediate (_SlotsAnatomyLibrary.gameObject);
				SlotsAnatomyLibraryObj = (GameObject) Instantiate(Resources.Load("DK_SlotsAnatomyLibrary"), Vector3.zero, Quaternion.identity);
				SlotsAnatomyLibraryObj.name = "DK_SlotsAnatomyLibrary";
				_SlotsAnatomyLibrary =  SlotsAnatomyLibraryObj.GetComponent<DK_SlotsAnatomyLibrary>();
				DK_UMA = GameObject.Find("DK_UMA");
				if ( DK_UMA == null ) {
					var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
					DK_UMA = GameObject.Find("DK_UMA");
				}
				SlotsAnatomyLibraryObj.transform.parent = DK_UMA.transform;
				// Find prefabs
				DirectoryInfo dir = new DirectoryInfo("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy");
				// Assign Prefabs
				FileInfo[] info = dir.GetFiles("*.prefab");
				info.Select(f => f.FullName).ToArray();
				// Action
				// Verify Folders objects
				GameObject SlotsAnatomy = GameObject.Find("Slots Anatomy");
				if ( DK_UMA == null ) {
					var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
					var goSlotsAnatomy = new GameObject();	goSlotsAnatomy.name = "Slots Anatomy";
					goSlotsAnatomy.transform.parent = goDK_UMA.transform;
				}
				else if ( SlotsAnatomy == null ) {
					SlotsAnatomy = new GameObject();	SlotsAnatomy.name = "Slots Anatomy";
					SlotsAnatomy.transform.parent = DK_UMA.transform;
				}
				SlotsAnatomy = GameObject.Find("Slots Anatomy");
				foreach (FileInfo f in info)
				{
					// Instantiate the Element Prefab
					GameObject clone = PrefabUtility.InstantiateAttachedAsset( AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy/"+ f.Name, typeof(GameObject)) ) as GameObject;
					clone.name = f.Name;
					if ( clone.name.Contains(".prefab")) clone.name = clone.name.Replace(".prefab", "");
					foreach ( Transform child in SlotsAnatomy.transform )
					{
						if ( clone != null && clone.name == child.name ) GameObject.DestroyImmediate( clone );
					}
					if ( clone != null )
					{
						clone.transform.parent = SlotsAnatomy.transform;
						// add the Element to the List		
						DK_SlotsAnatomyElement _SlotsAnatomyElement =  clone.GetComponent<DK_SlotsAnatomyElement>();
						_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = clone.name;
						if ( _SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn == false )
						{
							for (int i = 0; i < _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length ; i++)
							{
								if (_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement != null && _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.ElemAlreadyIn == true )
								{
									
								}
							}
							if ( _SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn == false )
							{
								var list = new DK_SlotsAnatomyElement[_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length + 1];
								Array.Copy(_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList, list, _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length );
								
								GameObject _Prefab = PrefabUtility.GetPrefabParent( _SlotsAnatomyElement.gameObject) as GameObject;	
								DK_SlotsAnatomyElement dk_SlotsAnatomyElement = _Prefab.GetComponent<DK_SlotsAnatomyElement>();
								// add to list
								list[_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length] = dk_SlotsAnatomyElement;
								_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList = list;
								// modify dk_SlotsAnatomyName if necessary
								if  ( dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName == "" ) dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = dk_SlotsAnatomyElement.transform.name;
								// Add to Dictonary
								if ( !ElemAlreadyIn )
								{
									_SlotsAnatomyLibrary.dk_SlotsAnatomyDictionary.Add( _SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName, dk_SlotsAnatomyElement.dk_SlotsAnatomyElement );
									dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn = false;
								}
							}
						}
					}
				}
				DestroyImmediate (SlotsAnatomy);
				
			}
			// Delete
			GUI.color = Red;
			if (  GUILayout.Button ( "Delete Selected", GUILayout.ExpandWidth (true))) {
				AssetDatabase.DeleteAsset("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy/" + SelectedAnaPart.gameObject.name + ".prefab" );
				AssetDatabase.Refresh();
				
				if ( _SlotsAnatomyLibrary ) DestroyImmediate (_SlotsAnatomyLibrary.gameObject);
				SlotsAnatomyLibraryObj = (GameObject) Instantiate(Resources.Load("DK_SlotsAnatomyLibrary"), Vector3.zero, Quaternion.identity);
				SlotsAnatomyLibraryObj.name = "DK_SlotsAnatomyLibrary";
				_SlotsAnatomyLibrary =  SlotsAnatomyLibraryObj.GetComponent<DK_SlotsAnatomyLibrary>();
				DK_UMA = GameObject.Find("DK_UMA");
				if ( DK_UMA == null ) {
					var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
					DK_UMA = GameObject.Find("DK_UMA");
				}
				SlotsAnatomyLibraryObj.transform.parent = DK_UMA.transform;
				// Find prefabs
				DirectoryInfo dir = new DirectoryInfo("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy");
				// Assign Prefabs
				FileInfo[] info = dir.GetFiles("*.prefab");
				info.Select(f => f.FullName).ToArray();
				// Action
				// Verify Folders objects
				GameObject SlotsAnatomy = GameObject.Find("Slots Anatomy");
				if ( DK_UMA == null ) {
					var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
					var goSlotsAnatomy = new GameObject();	goSlotsAnatomy.name = "Slots Anatomy";
					goSlotsAnatomy.transform.parent = goDK_UMA.transform;
				}
				else if ( SlotsAnatomy == null ) {
					SlotsAnatomy = new GameObject();	SlotsAnatomy.name = "Slots Anatomy";
					SlotsAnatomy.transform.parent = DK_UMA.transform;
				}
				SlotsAnatomy = GameObject.Find("Slots Anatomy");
				foreach (FileInfo f in info)
				{
					// Instantiate the Element Prefab
					GameObject clone = PrefabUtility.InstantiateAttachedAsset( AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy/"+ f.Name, typeof(GameObject)) ) as GameObject;
					clone.name = f.Name;
					if ( clone.name.Contains(".prefab")) clone.name = clone.name.Replace(".prefab", "");
					foreach ( Transform child in SlotsAnatomy.transform )
					{
						if ( clone != null && clone.name == child.name ) GameObject.DestroyImmediate( clone );
					}
					if ( clone != null )
					{
						clone.transform.parent = SlotsAnatomy.transform;
						// add the Element to the List		
						DK_SlotsAnatomyElement _SlotsAnatomyElement =  clone.GetComponent<DK_SlotsAnatomyElement>();
						_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = clone.name;
						if ( _SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn == false )
						{
							for (int i = 0; i < _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length ; i++)
							{
								if (_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement != null && _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.ElemAlreadyIn == true )
								{
									
								}
							}
							if ( _SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn == false )
							{
								var list = new DK_SlotsAnatomyElement[_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length + 1];
								Array.Copy(_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList, list, _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length );
								
								GameObject _Prefab = PrefabUtility.GetPrefabParent( _SlotsAnatomyElement.gameObject) as GameObject;	
								DK_SlotsAnatomyElement dk_SlotsAnatomyElement = _Prefab.GetComponent<DK_SlotsAnatomyElement>();
								// add to list
								list[_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length] = dk_SlotsAnatomyElement;
								_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList = list;
								// modify dk_SlotsAnatomyName if necessary
								if  ( dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName == "" ) dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = dk_SlotsAnatomyElement.transform.name;
								// Add to Dictonary
								if ( !ElemAlreadyIn )
								{
									_SlotsAnatomyLibrary.dk_SlotsAnatomyDictionary.Add( _SlotsAnatomyElement.dk_SlotsAnatomyElement.dk_SlotsAnatomyName, dk_SlotsAnatomyElement.dk_SlotsAnatomyElement );
									dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn = false;
								}
							}
						}
					}
				}
				DestroyImmediate (SlotsAnatomy);
			}
		}
		
		#region Search
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label("Search for :", GUILayout.ExpandWidth (false));
			SearchString = GUILayout.TextField(SearchString, 100, GUILayout.ExpandWidth (true));
			
		}
		#endregion Search
		
		GUILayout.Space(5);
		using (new Horizontal()) {
			GUI.color = Color.cyan ;
			GUILayout.Label("Anatomy Part", "toolbarbutton", GUILayout.Width (112));
			GUILayout.Label("Race", "toolbarbutton", GUILayout.Width (50));
			GUILayout.Label("Gender", "toolbarbutton", GUILayout.Width (50));
			GUILayout.Label("Type", "toolbarbutton", GUILayout.Width (80));
			GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
		}
		if ( _SlotsAnatomyLibrary != null ) using (new ScrollView(ref scroll)) 
		{
			for(int i = 0; i < _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
				
				DK_SlotsAnatomyElement dk_SlotsAnatomyElement = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i];
				if ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i] != null )
				{
					_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.ElemAlreadyIn = false;
					_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = false;
					
					if ( ( SearchString == "" || _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].name.ToLower().Contains(SearchString.ToLower()) ) && dk_SlotsAnatomyElement != null ) using (new Horizontal(GUILayout.Width (80))) {
						// Prefab Verification
						myPath = PrefabUtility.GetPrefabObject( dk_SlotsAnatomyElement.gameObject ).ToString() ;
						if ( myPath == "null" ) GUI.color = Color.gray;
						else GUI.color = Color.cyan;
						if ( GUILayout.Button ( "P", "toolbarbutton", GUILayout.ExpandWidth (false))) {
							// Create Prefab
							if ( myPath == "null" ) 
							{
									PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy/" + dk_SlotsAnatomyElement.name + ".prefab", dk_SlotsAnatomyElement.gameObject );
									GameObject clone = PrefabUtility.InstantiateAttachedAsset( AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Content/DK_SlotsAnatomy/"+ dk_SlotsAnatomyElement.name + ".prefab", typeof(GameObject)) ) as GameObject;										
								clone.name = dk_SlotsAnatomyElement.name;
								clone.transform.parent = dk_SlotsAnatomyElement.transform.parent;
								DestroyImmediate ( dk_SlotsAnatomyElement.gameObject ) ;
							}
						}
						// Element
						if ( SelectedAnaPart == _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i] )GUI.color = Color.yellow ;
						else GUI.color = Color.white ;
						if ( ChooseLink && GUILayout.Button ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName , "toolbarbutton", GUILayout.Width (95))) {
							SelectedAnaPart.dk_SlotsAnatomyElement.Place = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i];
							EditorUtility.SetDirty(SelectedAnaPart.gameObject);
							AssetDatabase.SaveAssets();
							ChooseLink = false;
						}
						if ( !ChooseLink && GUILayout.Button ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName , "toolbarbutton", GUILayout.Width (95))) {
							SelectedAnaPart = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i];
							Selection.activeGameObject = SelectedAnaPart.gameObject;
							NewAnatomyName = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName;
							NewPresetGender = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.ForGender;
							DK_Race = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
							OverlayType = DK_Race.OverlayType; 
							_SpawnPerct = Selection.activeGameObject.GetComponent< DK_SlotsAnatomyData >().SpawnPerct.ToString();
							if ( DK_Race.Place != null ) SelectedPlace = DK_Race.Place.ToString();
							else SelectedPlace = "";
						}
						// Race
						if ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
							_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
						}
						DK_Race = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
						if ( DK_Race.Race.Count == 0 ) GUI.color = Red;
						if ( DK_Race.Race.Count == 0 && GUILayout.Button ( "No Race" , "toolbarbutton", GUILayout.Width (50))) {
							
						}
						GUI.color = Green;
						if ( DK_Race.Race.Count == 1 && GUILayout.Button ( DK_Race.Race[0] , "toolbarbutton", GUILayout.Width (50))) {
							
						}
						if ( DK_Race.Race.Count > 1 && GUILayout.Button ( "Multi" , "toolbarbutton", GUILayout.Width (50))) {
							
						}
						// Gender
						if ( DK_Race.Gender == "" ) GUI.color = Red;
						if ( DK_Race.Gender == "" ) GUILayout.Label ( "N" , "Button") ;
						GUI.color = Green;
						if ( DK_Race.Gender != "" && DK_Race.Gender == "Female" ) GUILayout.Label ( "Female" , "toolbarbutton", GUILayout.Width (50) );
						if ( DK_Race.Gender != "" && DK_Race.Gender == "Male" ) GUILayout.Label ( "Male" , "toolbarbutton", GUILayout.Width (50) );
						if ( DK_Race.Gender != "" && DK_Race.Gender == "Both" ) GUILayout.Label ( "Both" , "toolbarbutton", GUILayout.Width (50) );
						// OverlayType
						if ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
							_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
						}
						DK_Race = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
						if ( DK_Race.OverlayType == "" ) GUI.color = Color.gray;
						if ( DK_Race.OverlayType == "" && GUILayout.Button ( "No Type" , "toolbarbutton", GUILayout.Width (80))) {
						}
						GUI.color = Green;
						if ( DK_Race.OverlayType != "" && GUILayout.Button ( DK_Race.OverlayType , "toolbarbutton", GUILayout.Width (80))) {
						}
					}
				}
			}
		}
	}
}
