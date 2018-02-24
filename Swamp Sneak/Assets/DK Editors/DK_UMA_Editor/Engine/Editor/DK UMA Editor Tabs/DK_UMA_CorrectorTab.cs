using UnityEngine;
using System;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using UMA;

public class DK_UMA_CorrectorTab : EditorWindow {
	public static Color Green = new Color (0.8f, 1f, 0.8f, 1);
	public static Color Red = new Color (0.9f, 0.5f, 0.5f);

	public static Vector2 scroll;

	public static List<DK_SlotsAnatomyElement> PlacesList = new List<DK_SlotsAnatomyElement>();
	public static List<DKRaceData> DKRacesList = new List<DKRaceData>();
	public static List<DKSlotData> DKSlotsList = new List<DKSlotData>();
	public static List<DKOverlayData> DKOverlaysList = new List<DKOverlayData>();
	public static List<UMA.RaceData> UMARacesList = new List<UMA.RaceData>();
	public static List<UMA.SlotDataAsset> UMASlotsList = new List<UMA.SlotDataAsset>();
	public static List<UMA.OverlayDataAsset> UMAOverlaysList = new List<UMA.OverlayDataAsset>();

	public static DK_UMA_GameSettings Database;

	public static void OnGUI () {
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

		if ( (GameObject.Find ("DK_UMA") == null && GameObject.Find ("UMA") == null)
			|| (GameObject.Find ("DK_UMA") == null && GameObject.Find ("UMA_DCS") == null) )
			EditorOptions.DisplayWelcome ();

		using (new HorizontalCentered()) 
		{
			GUI.color = Color.white;
			GUILayout.Label ( "Correctors", bold);
		}
		EditorGUILayout.HelpBox("Use those Correctors to try to fixe the various parts of the engine.", UnityEditor.MessageType.Info);
		using (new ScrollView(ref scroll)) 	{
			GUILayout.Label("Places Corrector", "toolbarbutton", GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("This corrector will reassign the properties of the Places used by DK UMA to assign the UMA elements on the avatar.", UnityEditor.MessageType.Info);
			if (GUILayout.Button ("Correct the Places (Anatomy Parts)")) {
				CorrectPlaces ();
			}

			GUILayout.Space(15);
			GUILayout.Label("DK UMA elements and their linked UMA elements", "toolbarbutton", GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("This corrector will try to reassign the proper linked UMA element (slot/overlay) to the DK UMA elements.", UnityEditor.MessageType.Info);
			if (GUILayout.Button ("Correct the linked UMA elements")) {
				CorrectElements ();
			}

			GUILayout.Space(15);
			GUILayout.Label("UMA & DK UMA Elements Detectors", "toolbarbutton", GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("Detect all the DK UMA Elements from your project and store them in the Database.", UnityEditor.MessageType.Info);
			using (new Horizontal()) {
				if (GUILayout.Button ("DK UMA Races")) {
					FindAssets ( "DKRaceData" );
					SaveAssets ();
				}

				if (GUILayout.Button ("DK UMA Slots")) {
					FindAssets ( "DKSlotData" );
					SaveAssets ();
				}

				if (GUILayout.Button ("DK UMA Overlays")) {
					FindAssets ( "DKOverlayData" );
					SaveAssets ();
				}
			}
			using (new Horizontal()) {
				if (GUILayout.Button ("UMA Races")) {
					FindAssets ( "RaceData" );
					SaveAssets ();
				}
				if (GUILayout.Button ("UMA Slots")) {
					FindAssets ( "SlotDataAsset" );
					SaveAssets ();
				}
				if (GUILayout.Button ("UMA Overlays")) {
					FindAssets ( "OverlayDataAsset" );
					SaveAssets ();
				}
			}
			using (new Horizontal()) {
				if (GUILayout.Button ("DK Color Presets")) {
					FindAssets ( "ColorPresetData" );
					SaveAssets ();
				}
				if (GUILayout.Button ("DK Places")) {
					FindAssets ( "DK_SlotAnatomyElement" );
					SaveAssets ();
				}
				if (GUILayout.Button ("All the UMA and DK UMA Elements")) {
					FindAssets("DKRaceData");
					FindAssets("UMA.RaceData");
					FindAssets("DKSlotData");
					FindAssets("DKOverlayData");
					FindAssets("UMA.OverlayDataAsset");
					FindAssets("UMA.SlotDataAsset");
					FindAssets ( "ColorPresetData" );
					FindAssets ( "DK_SlotAnatomyElement" );
					SaveAssets ();
				}
			}

			GUILayout.Space(15);
			GUILayout.Label("RPG Lists", "toolbarbutton", GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("The RPG Lists of your project are essential to use DK UMA. Here you can construct / populate them.", UnityEditor.MessageType.Info);
			if (GUILayout.Button ("Populate the RPG Lists")) {
				DKPopulateAllLists.PopulateAllLists ();
			}

		}
	}

	#region places
	public static void CorrectPlaces (){
		Debug.Log ("DK Corrector : Correcting DK Places, please wait ..." );
		DetectPlaces ();
		FixePlaces ();
		SaveAssets ();
		Debug.Log ("DK Corrector : Correction of the DK Places completed." );
	}

	public static void DetectPlaces (){
		Database._GameLibraries.PlacesList.Clear();
		PlacesList.Clear();

		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:GameObject", lookFor);
		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			GameObject element = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
			// detect all places
			if ( element.GetComponent<DK_SlotsAnatomyElement>() != null ){
				PlacesList.Add ( element.GetComponent<DK_SlotsAnatomyElement>() );
				if ( Database != null && Database._GameLibraries.PlacesList.Contains (element.GetComponent<DK_SlotsAnatomyElement>()) == false ){
					Database._GameLibraries.PlacesList.Add (element.GetComponent<DK_SlotsAnatomyElement>());
					EditorUtility.SetDirty (Database);
				}
			}
		}
	}

	public static void FixePlaces (){
		if ( PlacesList.Count > 0 ){
			for (int i = 0; i < PlacesList.Count ; i++){
				DK_SlotsAnatomyElement place = PlacesList[i];

				if ( place.name == "ArmbandWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "ArmbandWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "ArmbandWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Backpack" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Backpack";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Beard" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Beard";
					place.dk_SlotsAnatomyElement.ForGender = "Male";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Beard";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "BeltWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "BeltWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "BeltWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "CloakWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "CloakWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "CloakWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Collar" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Collar";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Ears" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Ears";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Face";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = true;
				}
				else if ( place.name == "Eyebrows" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Eyebrows";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Eyelash" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Eyelash";
					place.dk_SlotsAnatomyElement.ForGender = "Female";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "eyelid" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "eyelid";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Face";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = true;
				}
				else if ( place.name == "Eyes" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Eyes";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Eyes";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "EyesIris" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "EyesIris";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Feet" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Feet";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Flesh";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "FeetWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "FeetWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "FeetWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "GlassesWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "GlassesWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "GlassesWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Hair" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Hair";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Hair";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Hair_Module" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Hair_Module";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Hair";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = true;
				}
				else if ( place.name == "HandledLeft" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "HandledLeft";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "HandledRight" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "HandledRight";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Hands" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Hands";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Flesh";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "HandWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "HandWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "HandsWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Head" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Head";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Face";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "HeadWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "HeadWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "HeadWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Horns" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Horns";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Horns";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "InnerMouth" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "InnerMouth";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "LegBandWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "LegBandWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "LegBandWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Legs" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Legs";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Flesh";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "LegsWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "LegsWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "LegsWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "MaskWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "MaskWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "MaskWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Mouth" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Mouth";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Face";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = true;
				}
				else if ( place.name == "Nose" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Nose";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Face";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = true;
				}
				else if ( place.name == "RingLeft" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "RingLeft";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "RingRight" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "RingLeft";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "ShoulderWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "ShoulderWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "ShoulderWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Tail" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Tail";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Torso" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Torso";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "Flesh";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "TorsoWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "TorsoWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "TorsoWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "Wings" ){
					place.IsEquipment = false;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "Wings";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}
				else if ( place.name == "WristWear" ){
					place.IsEquipment = true;
					place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName = "WristWear";
					place.dk_SlotsAnatomyElement.ForGender = "Both";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType = "WristWear";
					place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem = false;
				}

				EditorUtility.SetDirty (place);
			}

		}
	}
	#endregion places

	#region Elements
	public static void CorrectElements (){
		Debug.Log ("DK Corrector : Correcting DK elements, please wait ..." );
		DKRacesList.Clear ();
		UMARacesList.Clear ();
		DKSlotsList.Clear ();
		DKOverlaysList.Clear ();
		UMAOverlaysList.Clear ();
		UMASlotsList.Clear ();

		FindAssets("DKRaceData");
		FindAssets("UMA.RaceData");
		FindAssets("DKSlotData");
		FindAssets("DKOverlayData");
		FindAssets("UMA.OverlayDataAsset");
		FindAssets("UMA.SlotDataAsset");

		FixeElements ();
	}

	public static void FixeElements (){
		FixeRaces ();
		FixeSlots ();
		FixeOverlays ();
		SaveAssets ();
		Debug.Log ("DK Corrector : Correction of the DK elements completed." );
	}

	public static void FixeRaces (){
		for (int i = 0; i < DKRacesList.Count ; i++){
			DKRaceData element = DKRacesList[i];

			if ( element.UMA == null && element.UMAraceName != "" ){
				for (int i1 = 0; i1 < UMARacesList.Count ; i1++){
					if ( UMARacesList[i1].name == element.UMAraceName ) {
						element.UMA = UMARacesList[i1];
						EditorUtility.SetDirty (element);
						Debug.Log ("DK Corrector : Race '"+element.name+"' fixed about its linked UMA element." );
					}
				}
			}
		}
	}

	public static void FixeSlots (){
		for (int i = 0; i < DKSlotsList.Count ; i++){
			DKSlotData element = DKSlotsList[i];

			if ( element._UMA == null && element._UMAslotName != "" ){
				for (int i1 = 0; i1 < UMASlotsList.Count ; i1++){
					if ( UMASlotsList[i1].name == element._UMAslotName ) {
						element._UMA = UMASlotsList[i1];
						EditorUtility.SetDirty (element);
						Debug.Log ("DK Corrector : Slot '"+element.name+"' fixed about its linked UMA element." );
					}
				}
			}		
		}
	}

	public static void FixeOverlays (){
		for (int i = 0; i < DKOverlaysList.Count ; i++){
			DKOverlayData element = DKOverlaysList[i];

			if ( element._UMA == null && element._UMAoverlayName != "" ){
				for (int i1 = 0; i1 < UMAOverlaysList.Count ; i1++){
					if ( UMAOverlaysList[i1].name == element._UMAoverlayName ) {
						element._UMA = UMAOverlaysList[i1];
						EditorUtility.SetDirty (element);
						Debug.Log ("DK Corrector : Overlay '"+element.name+"' fixed about its linked UMA element." );
					}
				}
			}
		}
	}

	public static void FindAssets(string type) {
		Database = FindObjectOfType<DKUMA_Variables>()._DK_UMA_GameSettings;

		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:"+type, lookFor);

		// for DK Races
		if ( type == "DKRaceData" ){
			DKRacesList.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				DKRaceData element = (DKRaceData)AssetDatabase.LoadAssetAtPath(path, typeof(DKRaceData));
				DKRacesList.Add ( element );
				if ( Database != null && Database._GameLibraries.DkRacesLibrary.Contains (element) == false ){
					Database._GameLibraries.DkRacesLibrary.Add (element);
					EditorUtility.SetDirty (Database);
				}
				if ( element.UMA != null && element.UMAraceName == "" ) {
					element.UMAraceName = element.UMA.name;
					EditorUtility.SetDirty (element);
				}
			}
			Debug.Log ("DK Corrector DKRacesList.Count : "+DKRacesList.Count);
		}

		// for DK Races
		else if ( type == "UMA.RaceData" || type == "RaceData" ){
			UMARacesList.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				UMA.RaceData element = (UMA.RaceData)AssetDatabase.LoadAssetAtPath(path, typeof(UMA.RaceData));
				UMARacesList.Add ( element );
				if ( Database != null && Database._GameLibraries.UmaRacesLibrary.Contains (element) == false ){
					Database._GameLibraries.UmaRacesLibrary.Add (element);
					EditorUtility.SetDirty (Database);
				}
			}
			Debug.Log ("DK Corrector UMARacesList.Count : "+UMARacesList.Count);
		}

		// for DK ColorPresets
		else if ( type == "ColorPresetData" ){
			Database._GameLibraries.ColorPresetsList.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				ColorPresetData element = (ColorPresetData)AssetDatabase.LoadAssetAtPath(path, typeof(ColorPresetData));
				if ( Database != null && Database._GameLibraries.ColorPresetsList.Contains (element) == false ){
					Database._GameLibraries.ColorPresetsList.Add (element);
					EditorUtility.SetDirty (Database);
				}
			}
		//	Debug.Log ("DK Corrector UMARacesList.Count : "+UMARacesList.Count);
		}

		// for DKSlotData elements
		else if ( type == "DKSlotData" ){
			DKSlotsList.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				DKSlotData element = (DKSlotData)AssetDatabase.LoadAssetAtPath(path, typeof(DKSlotData));
				if ( element.name.Contains("DefaultDK") == false 
					&& element.name.Contains("DefaultSlotType") == false
					&& element.name.Contains("DefaultOverlayType") == false
					&& element.name.Contains("DefaultUMA") == false
					&& element.name.Contains("CapsuleCollider") == false
					&& element.name.Contains("ExpressionSlot") == false
					&& element.name.Contains("ForearmTwist") == false
					&& element.name.Contains("Locomotion") == false ){
					DKSlotsList.Add ( element );
					if ( Database != null && Database._GameLibraries.DkSlotsLibrary.Contains (element) == false ){
						Database._GameLibraries.DkSlotsLibrary.Add (element);
						EditorUtility.SetDirty (Database);
					}
					if ( element._UMA != null && element._UMAslotName == "" ) {
						element._UMAslotName = element._UMA.name;
						EditorUtility.SetDirty (element);
					}
				}
			}
			Debug.Log ("DK Corrector DKSlotsList.Count : "+DKSlotsList.Count);
		}

		// for DKOverlayData elements
		else if ( type == "DKOverlayData" ){
			DKOverlaysList.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				DKOverlayData element = (DKOverlayData)AssetDatabase.LoadAssetAtPath(path, typeof(DKOverlayData));
				if ( element.name.Contains("DefaultDK") == false 
					&& element.name.Contains("DefaultSlotType") == false
					&& element.name.Contains("DefaultOverlayType") == false
					&& element.name.Contains("DefaultUMA") == false
					&& element.name.Contains("CapsuleCollider") == false
					&& element.name.Contains("ExpressionSlot") == false
					&& element.name.Contains("ForearmTwist") == false
					&& element.name.Contains("Locomotion") == false ){
					DKOverlaysList.Add ( element );
					if ( Database != null && Database._GameLibraries.DkOverlaysLibrary.Contains (element) == false ){
						Database._GameLibraries.DkOverlaysLibrary.Add (element);
						EditorUtility.SetDirty (Database);
					}
					if ( element._UMA != null && element._UMAoverlayName == "" ) {
						element._UMAoverlayName = element._UMA.name;
						EditorUtility.SetDirty (element);
					}
				}
			}
			Debug.Log ("DK Corrector DKOverlaysList.Count : "+DKOverlaysList.Count);
		}

		// for UMA.OverlayDataAsset elements
		else if ( type == "UMA.OverlayDataAsset" || type == "OverlayDataAsset" ){
			UMAOverlaysList.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				UMA.OverlayDataAsset element = (UMA.OverlayDataAsset)AssetDatabase.LoadAssetAtPath(path, typeof(UMA.OverlayDataAsset));
				if ( element.name.Contains("DefaultDK") == false 
					&& element.name.Contains("DefaultSlotType") == false
					&& element.name.Contains("DefaultOverlayType") == false
					&& element.name.Contains("DefaultUMA") == false
					&& element.name.Contains("CapsuleCollider") == false
					&& element.name.Contains("ExpressionSlot") == false
					&& element.name.Contains("ForearmTwist") == false
					&& element.name.Contains("Locomotion") == false ){
					UMAOverlaysList.Add ( element );
					if ( Database != null && Database._GameLibraries.UmaOverlaysLibrary.Contains (element) == false ){
						Database._GameLibraries.UmaOverlaysLibrary.Add (element);
						EditorUtility.SetDirty (Database);
					}
				}
			}
			Debug.Log ("DK Corrector UMAOverlaysList.Count : "+UMAOverlaysList.Count);
		}

		// for UMA.SlotDataAsset elements
		else if ( type == "UMA.SlotDataAsset" || type == "SlotDataAsset" ){
			UMASlotsList.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				UMA.SlotDataAsset element = (UMA.SlotDataAsset)AssetDatabase.LoadAssetAtPath(path, typeof(UMA.SlotDataAsset));
				if ( element.name.Contains("DefaultDK") == false 
					&& element.name.Contains("DefaultSlotType") == false
					&& element.name.Contains("DefaultOverlayType") == false
					&& element.name.Contains("DefaultUMA") == false
					&& element.name.Contains("CapsuleCollider") == false
					&& element.name.Contains("ExpressionSlot") == false
					&& element.name.Contains("ForearmTwist") == false
					&& element.name.Contains("Locomotion") == false ){
					UMASlotsList.Add ( element );
					if ( Database != null && Database._GameLibraries.UmaSlotsLibrary.Contains (element) == false ){
						Database._GameLibraries.UmaSlotsLibrary.Add (element);
						EditorUtility.SetDirty (Database);
					}
				}
			}
			Debug.Log ("DK Corrector UMASlotsList.Count : "+UMASlotsList.Count);
		}
	}
	#endregion Elements

	public static void SaveAssets (){
		AssetDatabase.SaveAssets ();

	}
}
