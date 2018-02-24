using UnityEngine;
using System;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class DK_UMAPacker : MonoBehaviour {

	// lists
	public static List<string> guidsToPack = new List<string>();

	public static List<DKSlotData> SlotsList = new List<DKSlotData>();
	public static List<DKOverlayData> OverlaysList = new List<DKOverlayData>();
	public static List<DKRaceData> RacesList = new List<DKRaceData>();
	public static List<ColorPresetData> ColorPresetsList = new List<ColorPresetData>();
	public static List<GameObject> PlacesList = new List<GameObject>();
	public static List<DK_UMA_AvatarData> AvatarsDataList = new List<DK_UMA_AvatarData>();
	public static List<DKEquipmentSetData> SetsDataList = new List<DKEquipmentSetData>();
	public static List<DK_UMA_Item> ItemsDataList = new List<DK_UMA_Item>();
	public static List<DK_UMA_GameSettings> DKUMASettingsList = new List<DK_UMA_GameSettings>();

	public static string PackName = "";

	public static void FindSlots() {
		SlotsList.Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:DKSlotData", lookFor);

		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			DKSlotData element = (DKSlotData)AssetDatabase.LoadAssetAtPath(path, typeof(DKSlotData));
			guidsToPack.Add (path);
			SlotsList.Add ( element );
		}
	}

	public static void FindOverlays() {
		OverlaysList.Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:DKOverlayData", lookFor);

		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			DKOverlayData element = (DKOverlayData)AssetDatabase.LoadAssetAtPath(path, typeof(DKOverlayData));
			guidsToPack.Add (path);
			OverlaysList.Add ( element );
		}
	}

	public static void FindRaces() {
		RacesList.Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:DKRaceData", lookFor);

		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			DKRaceData element = (DKRaceData)AssetDatabase.LoadAssetAtPath(path, typeof(DKRaceData));
			guidsToPack.Add (path);
			RacesList.Add ( element );
		}
	}

	public static void FindColorPresets() {
		ColorPresetsList.Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:ColorPresetData", lookFor);

		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			ColorPresetData element = (ColorPresetData)AssetDatabase.LoadAssetAtPath(path, typeof(ColorPresetData));
			guidsToPack.Add (path);
			ColorPresetsList.Add ( element );
		}
	}

	public static void FindAvatars() {
		AvatarsDataList.Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:DK_UMA_AvatarData", lookFor);

		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			DK_UMA_AvatarData element = (DK_UMA_AvatarData)AssetDatabase.LoadAssetAtPath(path, typeof(DK_UMA_AvatarData));
			guidsToPack.Add (path);
			AvatarsDataList.Add ( element );
		}
	}

	public static void FindPlaces() {
		PlacesList.Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:GameObject", lookFor);
		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			GameObject element = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
			if ( element.GetComponent<DK_SlotsAnatomyElement>() != null ){
				guidsToPack.Add (path);
				PlacesList.Add ( element );
			}
		}
	}

	public static void FindSets() {
		SetsDataList.Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:DKEquipmentSetData", lookFor);

		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			DKEquipmentSetData element = (DKEquipmentSetData)AssetDatabase.LoadAssetAtPath(path, typeof(DKEquipmentSetData));
			guidsToPack.Add (path);
			SetsDataList.Add ( element );
		}
	}

	public static void FindItems() {
		ItemsDataList.Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:DK_UMA_Item", lookFor);

		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			DK_UMA_Item element = (DK_UMA_Item)AssetDatabase.LoadAssetAtPath(path, typeof(DK_UMA_Item));
			guidsToPack.Add (path);
			ItemsDataList.Add ( element );
		}
	}

	public static void FindSettings() {
		DKUMASettingsList.Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:DK_UMA_GameSettings", lookFor);

		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			DK_UMA_GameSettings element = (DK_UMA_GameSettings)AssetDatabase.LoadAssetAtPath(path, typeof(DK_UMA_GameSettings));
			guidsToPack.Add (path);
			DKUMASettingsList.Add ( element );
		}
	}


	public static void PackAssets() {
		AssetDatabase.ExportPackage (guidsToPack.ToArray(), PackName+".unityPackage", ExportPackageOptions.Interactive );
	}
}
