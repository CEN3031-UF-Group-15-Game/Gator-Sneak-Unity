using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UMA;
using UMA.CharacterSystem;
using System.Collections.Generic;
using System;
using System.Linq;

public class DKPopulateAllLists : MonoBehaviour {

	public static DK_UMA_GameSettings GameSettings;
	public static DK_RPG_UMA_Generator _DK_RPG_UMA_Generator = FindObjectOfType<DK_RPG_UMA_Generator>();
	public static DK_UMACrowd _DK_UMACrowd = FindObjectOfType<DK_UMACrowd>();
	public static GameObject DK_UMA;
	public static DKUMA_Variables _DKUMA_Variables;


	public static void PopulateAllLists (){
		FindVariables ();
		FindAllAssets ();
		_DK_RPG_UMA_Generator.RPGPrepared = false;
		_DK_RPG_UMA_Generator.PopulateAllLists();
		_DK_UMACrowd.CleanLibraries ();

		AddToLib ();
		AddToUMALib ();

		_DKUMA_Variables.PopulateLibraries ();
	}

	public static void FindAllAssets (){
		FindAssets("DKRaceData");
		FindAssets("UMA.RaceData");
		FindAssets("DKSlotData");
		FindAssets("DKOverlayData");
		FindAssets("UMA.OverlayDataAsset");
		FindAssets("UMA.SlotDataAsset");
	}

	public static void FindVariables (){
		_DK_RPG_UMA_Generator = FindObjectOfType<DK_RPG_UMA_Generator>();
		_DK_UMACrowd = FindObjectOfType<DK_UMACrowd>();
		DK_UMA = GameObject.Find("DK_UMA");
		_DKUMA_Variables = DK_UMA.transform.GetComponent<DKUMA_Variables>();
		GameSettings =_DKUMA_Variables._DK_UMA_GameSettings;
	}

	public static void AddToLib (){
		FindVariables ();

		// Slots
		for (int i = 0; i < _DKUMA_Variables.DKSlotsList.Count; i++) {
			if ( _DKUMA_Variables.DKSlotsList[i] == null )  
				_DKUMA_Variables.DKSlotsList.Remove ( _DKUMA_Variables.DKSlotsList[i]);
			else {
				if ( _DK_UMACrowd.slotLibrary.slotDictionary.ContainsValue(_DKUMA_Variables.DKSlotsList[i] as DKSlotData) == false){
					_DK_UMACrowd.slotLibrary.AddSlot( _DKUMA_Variables.DKSlotsList[i].name, _DKUMA_Variables.DKSlotsList[i] as DKSlotData);
				}
				if ( GameSettings != null 
					&& GameSettings._GameLibraries.DkSlotsLibrary.Contains(_DKUMA_Variables.DKSlotsList[i] as DKSlotData) == false )
				{
					GameSettings._GameLibraries.DkSlotsLibrary.Add ( _DKUMA_Variables.DKSlotsList[i] as DKSlotData );
				}
			}
		}
		// Overlays
		for (int i = 0; i < _DKUMA_Variables.DKOverlaysList.Count; i++) {
			if ( _DKUMA_Variables.DKOverlaysList[i] == null )  
				_DKUMA_Variables.DKOverlaysList.Remove ( _DKUMA_Variables.DKOverlaysList[i]);
			else {
				if ( _DK_UMACrowd.overlayLibrary.overlayDictionary.ContainsValue(_DKUMA_Variables.DKOverlaysList[i] as DKOverlayData) == false){
					_DK_UMACrowd.overlayLibrary.AddOverlay( _DKUMA_Variables.DKOverlaysList[i].overlayName, _DKUMA_Variables.DKOverlaysList[i] as DKOverlayData);
				}
				if ( GameSettings != null 
					&& GameSettings._GameLibraries.DkOverlaysLibrary.Contains(_DKUMA_Variables.DKOverlaysList[i] as DKOverlayData) == false )
				{
					GameSettings._GameLibraries.DkOverlaysLibrary.Add ( _DKUMA_Variables.DKOverlaysList[i] as DKOverlayData );
				}
			}
		}
		// Races
		for (int i = 0; i < GameSettings._GameLibraries.DkRacesLibrary.Count; i++) {
			if ( GameSettings._GameLibraries.DkRacesLibrary[i] == null )  
				GameSettings._GameLibraries.DkRacesLibrary.Remove ( GameSettings._GameLibraries.DkRacesLibrary[i]);
			else {
				if ( _DK_UMACrowd.raceLibrary.raceDictionary.ContainsValue(GameSettings._GameLibraries.DkRacesLibrary[i] as DKRaceData) == false
					&& GameSettings._GameLibraries.DkRacesLibrary[i].name.Contains ("Default") == false
					&& GameSettings._GameLibraries.DkRacesLibrary[i].Active == true ){
					_DK_UMACrowd.raceLibrary.AddRace( GameSettings._GameLibraries.DkRacesLibrary[i] as DKRaceData);
				}
			}
		}

		EditorUtility.SetDirty(GameSettings);
		EditorUtility.SetDirty(_DK_UMACrowd.overlayLibrary);
		EditorUtility.SetDirty(_DK_UMACrowd.slotLibrary);
		EditorUtility.SetDirty(_DK_UMACrowd.raceLibrary);
		AssetDatabase.SaveAssets();
	}

	public static void AddToUMALib (){
		FindVariables ();

		SlotLibrary _SlotLibrary = null;
		OverlayLibrary _OverlayLibrary = null;

		if ( FindObjectOfType<UMAGenerator>() != null ){
			if ( _SlotLibrary == null ) _SlotLibrary = FindObjectOfType<DynamicSlotLibrary>();
			if ( _OverlayLibrary == null ) _OverlayLibrary = FindObjectOfType<DynamicOverlayLibrary>();

			List<UMA.SlotDataAsset> LibSlotsList = new List<UMA.SlotDataAsset>();
			if ( _SlotLibrary != null ) LibSlotsList = _SlotLibrary.GetAllSlotAssets ().ToList ();

			// Slots
			if ( _SlotLibrary != null ) for (int i = 0; i < _DKUMA_Variables.UMASlotsList.Count; i++) {
					if ( LibSlotsList.Contains(_DKUMA_Variables.UMASlotsList[i] as UMA.SlotDataAsset) == false)
						_SlotLibrary.AddSlotAsset( _DKUMA_Variables.UMASlotsList[i] as UMA.SlotDataAsset);

					// lib wizard
					if ( GameSettings != null
						&& GameSettings._GameLibraries.UmaSlotsLibrary.Contains(_DKUMA_Variables.UMASlotsList[i] as UMA.SlotDataAsset) == false )
					{
						GameSettings._GameLibraries.UmaSlotsLibrary.Add ( _DKUMA_Variables.UMASlotsList[i] as UMA.SlotDataAsset );
					}
				}

			List<UMA.OverlayDataAsset> LibOvsList = new List<UMA.OverlayDataAsset>();
			if ( _OverlayLibrary != null ) LibOvsList = _OverlayLibrary.GetAllOverlayAssets ().ToList ();

			// Overlays
			if ( _OverlayLibrary != null )for (int i = 0; i < _DKUMA_Variables.UMAOverlaysList.Count; i++) {
					if ( LibOvsList.Contains(_DKUMA_Variables.UMAOverlaysList[i] as UMA.OverlayDataAsset) == false)
						_OverlayLibrary.AddOverlayAsset( _DKUMA_Variables.UMAOverlaysList[i] as UMA.OverlayDataAsset);

					// lib wizard
					if ( GameSettings != null 
						&& GameSettings._GameLibraries.UmaOverlaysLibrary.Contains(_DKUMA_Variables.UMAOverlaysList[i] as UMA.OverlayDataAsset) == false )
					{
						GameSettings._GameLibraries.UmaOverlaysLibrary.Add ( _DKUMA_Variables.UMAOverlaysList[i] as UMA.OverlayDataAsset );
					}
				}

			// Races
			RaceLibrary UMARaceLibrary = FindObjectOfType<DynamicRaceLibrary>();
			List<UMA.RaceData> LibRacesList = new List<UMA.RaceData>();
			if ( UMARaceLibrary != null ) LibRacesList = UMARaceLibrary.GetAllRaces ().ToList ();

			if ( UMARaceLibrary != null ) for (int i = 0; i <GameSettings._GameLibraries.UmaRacesLibrary.Count; i++) {
					if ( LibRacesList.Contains(GameSettings._GameLibraries.UmaRacesLibrary[i] as UMA.RaceData) == false)
						UMARaceLibrary.AddRace(GameSettings._GameLibraries.UmaRacesLibrary[i] as UMA.RaceData);
				}	
			Debug.Log ("All the UMA elements added to the UMA libraries.");
			//	Debug.Log ("DK Helper : You are now able to generate a UMA clone of every new DK avatar.");
			EditorUtility.SetDirty(GameSettings);
			AssetDatabase.SaveAssets();
		}
		else Debug.LogError ( "DK UMA : the UMA object is not present in your scene. Install it and retry to prepare the libraries." );
	}

	public static void FindAssets(string type) {
		GameSettings = FindObjectOfType<DKUMA_Variables>()._DK_UMA_GameSettings;

		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:"+type, lookFor);

		// for DK Races
		if ( type == "DKRaceData" ){
			GameSettings._GameLibraries.DkRacesLibrary.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				DKRaceData element = (DKRaceData)AssetDatabase.LoadAssetAtPath(path, typeof(DKRaceData));
				if ( GameSettings != null && GameSettings._GameLibraries.DkRacesLibrary.Contains (element) == false ){
					GameSettings._GameLibraries.DkRacesLibrary.Add (element);
					EditorUtility.SetDirty (GameSettings);
				}
				if ( element.UMA != null && element.UMAraceName == "" ) {
					element.UMAraceName = element.UMA.name;
					EditorUtility.SetDirty (element);
				}
			}
		}

		// for DK Races
		else if ( type == "UMA.RaceData" || type == "RaceData" ){
			GameSettings._GameLibraries.UmaRacesLibrary.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				UMA.RaceData element = (UMA.RaceData)AssetDatabase.LoadAssetAtPath(path, typeof(UMA.RaceData));
				if ( GameSettings != null && GameSettings._GameLibraries.UmaRacesLibrary.Contains (element) == false ){
					GameSettings._GameLibraries.UmaRacesLibrary.Add (element);
					EditorUtility.SetDirty (GameSettings);
				}
			}
		}

		// for DK ColorPresets
		else if ( type == "ColorPresetData" ){
			GameSettings._GameLibraries.ColorPresetsList.Clear ();
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				ColorPresetData element = (ColorPresetData)AssetDatabase.LoadAssetAtPath(path, typeof(ColorPresetData));
				if ( GameSettings != null && GameSettings._GameLibraries.ColorPresetsList.Contains (element) == false ){
					GameSettings._GameLibraries.ColorPresetsList.Add (element);
					EditorUtility.SetDirty (GameSettings);
				}
			}
			//	Debug.Log ("DK Corrector UMARacesList.Count : "+UMARacesList.Count);
		}

		// for DKSlotData elements
		else if ( type == "DKSlotData" ){
			GameSettings._GameLibraries.DkSlotsLibrary.Clear ();
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
					if ( GameSettings != null && GameSettings._GameLibraries.DkSlotsLibrary.Contains (element) == false ){
						GameSettings._GameLibraries.DkSlotsLibrary.Add (element);
						EditorUtility.SetDirty (GameSettings);
					}
					if ( element._UMA != null && element._UMAslotName == "" ) {
						element._UMAslotName = element._UMA.name;
						EditorUtility.SetDirty (element);
					}
				}
			}
		}

		// for DKOverlayData elements
		else if ( type == "DKOverlayData" ){
			GameSettings._GameLibraries.DkOverlaysLibrary.Clear ();
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
					if ( GameSettings != null && GameSettings._GameLibraries.DkOverlaysLibrary.Contains (element) == false ){
						GameSettings._GameLibraries.DkOverlaysLibrary.Add (element);
						EditorUtility.SetDirty (GameSettings);
					}
					if ( element._UMA != null && element._UMAoverlayName == "" ) {
						element._UMAoverlayName = element._UMA.name;
						EditorUtility.SetDirty (element);
					}
				}
			}
		}

		// for UMA.OverlayDataAsset elements
		else if ( type == "UMA.OverlayDataAsset" || type == "OverlayDataAsset" ){
			GameSettings._GameLibraries.UmaOverlaysLibrary.Clear ();
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
					if ( GameSettings != null && GameSettings._GameLibraries.UmaOverlaysLibrary.Contains (element) == false ){
						GameSettings._GameLibraries.UmaOverlaysLibrary.Add (element);
						EditorUtility.SetDirty (GameSettings);
					}
				}
			}
		}

		// for UMA.SlotDataAsset elements
		else if ( type == "UMA.SlotDataAsset" || type == "SlotDataAsset" ){
			GameSettings._GameLibraries.UmaSlotsLibrary.Clear ();
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
					if ( GameSettings != null && GameSettings._GameLibraries.UmaSlotsLibrary.Contains (element) == false ){
						GameSettings._GameLibraries.UmaSlotsLibrary.Add (element);
						EditorUtility.SetDirty (GameSettings);
					}
				}
			}
		}
	}
}
