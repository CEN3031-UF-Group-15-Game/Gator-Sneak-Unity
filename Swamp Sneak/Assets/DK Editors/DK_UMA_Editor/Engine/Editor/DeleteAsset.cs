using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class DeleteAsset : EditorWindow {

	public static string ProcessName = "";
	public static float ProcessNumber = 0;
	public static float ProcessCurrent = 0;
	public static bool Processing = false;
	public static bool MultiUMAModel = false;
	public static bool UMAModel = false;
	public static string myPath;
	public static string Action;
	public static string AssetName = "";

	void CloseSelf(){
		try{
		Action = "";
		this.Close();
		}catch(NullReferenceException){}
	}

	public static void OpenProcessWindow()
	{
		GetWindow(typeof(DKUMAProcessWindow), false, "Processing...");
		DKUMAProcessWindow.ProcessName = "Create Prefabs";
		DKUMAProcessWindow.Processing = true;
		DKUMAProcessWindow.ProcessCurrent = 0;
		DKUMAProcessWindow.ProcessNumber = EditorVariables.MFSelectedList.childCount;
	}

	void OnGUI () {
		this.minSize = new Vector2(300, 150);
		this.maxSize = new Vector2(305, 155);
		Repaint();

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

		using (new HorizontalCentered()) {
			GUILayout.Label ( ProcessName, bold, GUILayout.ExpandWidth (true));
		}
		using (new HorizontalCentered())GUILayout.Label ( "Are you sure ?", GUILayout.ExpandWidth (false));
		if ( Action != "DelByPath" && Selection.activeObject && ProcessName != "Delete all models" )using (new HorizontalCentered())GUILayout.Label ( Selection.activeObject.name, bold, GUILayout.ExpandWidth (false));
		if ( Action == "DelByPath" && ProcessName != "Delete all models" )using (new HorizontalCentered())GUILayout.Label ( AssetName, bold, GUILayout.ExpandWidth (false));
		GUILayout.Space(40);
		using (new Horizontal()) {
			GUILayout.Space(20);
			GUI.color = new Color (0.9f, 0.5f, 0.5f);
			if ( ProcessName != "Clearing Library"  && GUILayout.Button ( "Yes, Delete.", GUILayout.ExpandWidth (true))){
				Delete();
			}
			else
			if (ProcessName == "Clearing Library" && GUILayout.Button ( "Yes, Clear it.", GUILayout.ExpandWidth (true))){
				Clearing();
			}
			GUILayout.Space(20);
			GUI.color = new Color (0.8f, 1f, 0.8f, 1);
			if ( GUILayout.Button ( "No, Cancel", GUILayout.ExpandWidth (true))){
				CloseSelf();
			}
			GUILayout.Space(20);
		}
	}
	void Clearing(){
		if ( Selection.activeGameObject.gameObject.GetComponent<DKRaceLibrary>() == true ){
			List<DKRaceData> tmpList = new List<DKRaceData>();
			tmpList = Selection.activeGameObject.gameObject.GetComponent<DKRaceLibrary>().raceElementList.ToList();
			tmpList.Clear();
			Selection.activeGameObject.gameObject.GetComponent<DKRaceLibrary>().raceElementList = tmpList.ToArray();
		
		}
		if ( Selection.activeGameObject.gameObject.GetComponent<DKSlotLibrary>() == true ){
			List<DKSlotData> tmpList = new List<DKSlotData>();
			tmpList = Selection.activeGameObject.gameObject.GetComponent<DKSlotLibrary>().slotElementList.ToList();
			tmpList.Clear();
			Selection.activeGameObject.gameObject.GetComponent<DKSlotLibrary>().slotElementList = tmpList.ToArray();

		}
		if ( Selection.activeGameObject.gameObject.GetComponent<DKOverlayLibrary>() == true ){
			List<DKOverlayData> tmpList = new List<DKOverlayData>();
			tmpList = Selection.activeGameObject.gameObject.GetComponent<DKOverlayLibrary>().overlayElementList.ToList();
			tmpList.Clear();
			Selection.activeGameObject.gameObject.GetComponent<DKOverlayLibrary>().overlayElementList = tmpList.ToArray();
			myPath = "";
			
		}
		CloseSelf();
	}
	void Delete(){
		if ( ProcessName == "Delete all models" ) {
			DestroyImmediate( EditorVariables.MFSelectedList.gameObject );
		}
		else if ( Action == "DelByPath" ) {
		

		}
		else if ( MultiUMAModel && EditorVariables.MFSelectedList != null ) {
			foreach (Transform Model in EditorVariables.MFSelectedList.transform)
			{
				if ( Model.GetComponent<DK_Model>() == true ){
					if ( PrefabUtility.GetPrefabParent( Model.gameObject ) != null ){
						myPath = PrefabUtility.GetPrefabParent( Model.gameObject ).name.ToString() ;
						AssetDatabase.DeleteAsset("Assets/DK Editors/DK_UMA_Editor/Prefabs/Models/"+myPath+".prefab" );
					}
				}
				if ( Model.GetComponent<ModelGrp>() == true ){
					if ( PrefabUtility.GetPrefabParent( Model.gameObject ) != null ){
						myPath = PrefabUtility.GetPrefabParent( Model.gameObject ).name.ToString() ;
						AssetDatabase.DeleteAsset("Assets/DK Editors/DK_UMA_Editor/Prefabs/Models/Groups/"+myPath+".prefab" );
					}
				}
			}
			AssetDatabase.Refresh();
			MultiUMAModel = false;
		}
		else
		if ( UMAModel ) {
			if ( Selection.activeGameObject.GetComponent<DK_Model>() == true ){
				if ( PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ) != null ){
					myPath = PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ).name.ToString() ;
					AssetDatabase.DeleteAsset("Assets/DK Editors/DK_UMA_Editor/Prefabs/Models/"+myPath+".prefab" );
				}
			}
			if ( Selection.activeGameObject.GetComponent<ModelGrp>() == true ){
				if ( PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ) != null ){
					myPath = PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ).name.ToString() ;
					AssetDatabase.DeleteAsset("Assets/DK Editors/DK_UMA_Editor/Prefabs/Models/Groups/"+myPath+".prefab" );
				}
			}
			if ( Selection.activeGameObject.GetComponent<DKSlotLibrary>() == true ){
				if ( PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ) != null ){
					myPath = PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ).name.ToString() ;
					AssetDatabase.DeleteAsset("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Slot Libraries/"+myPath+".prefab" );
			
				}
			}
			if ( Selection.activeGameObject.GetComponent<DKRaceLibrary>() == true ){
				if ( PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ) != null ){
					myPath = PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ).name.ToString() ;
					AssetDatabase.DeleteAsset("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Race Libraries/"+myPath+".prefab" );
				}
			}	
			if ( Selection.activeGameObject.GetComponent<DKOverlayLibrary>() == true ){
				if ( PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ) != null ){
					myPath = PrefabUtility.GetPrefabParent( Selection.activeGameObject.gameObject ).name.ToString() ;
					AssetDatabase.DeleteAsset("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Overlay Libraries/"+myPath+".prefab" );
					
				}
			}
			AssetDatabase.Refresh();
			UMAModel = false;
		}
		else
		// Elements
		if ( Selection.activeObject && Selection.activeObject.GetType().ToString() == "DKRaceData" ) {
			if ( EditorVariables._RaceLibrary.raceElementList.Contains( Selection.activeObject as DKRaceData ) ){
				List<DKRaceData> List;
				List = EditorVariables._RaceLibrary.raceElementList.ToList();
				List.Remove(( Selection.activeObject as DKRaceData ));
				EditorVariables._RaceLibrary.raceElementList = List.ToArray();
			}
		}
		else
		if ( Selection.activeObject && Selection.activeObject.GetType().ToString() == "DKSlotData" ) {
			if ( EditorVariables._DKSlotLibrary.slotElementList.Contains( Selection.activeObject as DKSlotData ) ){
				List<DKSlotData> List;
				List = EditorVariables._DKSlotLibrary.slotElementList.ToList();
				List.Remove(( Selection.activeObject as DKSlotData ));
				EditorVariables._DKSlotLibrary.slotElementList = List.ToArray();
			}
		}
		else
		if ( Selection.activeObject && Selection.activeObject.GetType().ToString() == "DKOverlayData" ) {
			if ( EditorVariables._OverlayLibrary.overlayElementList.Contains( Selection.activeObject as DKOverlayData ) ){
				List<DKOverlayData> List;
				List = EditorVariables._OverlayLibrary.overlayElementList.ToList();
				List.Remove(( Selection.activeObject as DKOverlayData ));
				EditorVariables._OverlayLibrary.overlayElementList = List.ToArray();
			}
		}
		else
		if ( Action != "DelByPath" && ProcessName != "Clearing Library" ) {
			myPath = AssetDatabase.GetAssetPath( Selection.activeObject ); 
			AssetDatabase.DeleteAsset (myPath);
		}
		if ( Action == "DelByPath" ) Action = "";
		CloseSelf();
	}



	void OnSelectionChange() {
		CloseSelf();
	}
}
