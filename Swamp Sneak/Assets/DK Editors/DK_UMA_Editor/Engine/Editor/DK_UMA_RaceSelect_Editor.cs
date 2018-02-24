using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class DK_UMA_RaceSelect_Editor : EditorWindow {
	
	public static GameObject SelectedElemObj;
	public static string SelectedElemName = "";
	public static string SelectedElemType = "";
	public static string SelectedRaceName = "";
	public static DKRaceData _RaceData;
	public static string Gender;
	GameObject tmpObj;
	
	Vector2 scroll;
	Vector2 scroll1;
	
	List<string> RaceDataList = new List<string>();

	DK_UMA_GameSettings Settings;

	void OnEnable (){
		Settings = GameObject.Find ( "DK_UMA" ).GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;
	}

	void OnGUI () {

			EditorVariables._RaceLibrary = GameObject.Find ( "DKUMACrowd" ).GetComponent<DK_UMACrowd>().raceLibrary;

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
		
		using (new Horizontal()) {
			GUILayout.Label ( "Races List", "toolbarbutton", GUILayout.ExpandWidth (true));
		}
		tmpObj = (Selection.activeObject as GameObject);
		if ( Selection.activeObject && Selection.activeObject.GetType().ToString() == "DKRaceData"){ 
			GUI.color = Color.yellow;
			GUILayout.Label ( "Selection is a Race. Select a Slot or an Overlay.", GUILayout.ExpandWidth (false));
		
		}
		GUI.color = Color.white;
		if ( EditorVariables.AutoDetLib == false 
		    && Selection.activeObject
		    && Selection.activeObject.GetType().ToString() != "DKRaceData"
		    && ( Selection.activeObject.GetType().ToString() == "DKSlotData" 
		    || Selection.activeObject.GetType().ToString() == "DKOverlayData"
		    || ( tmpObj && tmpObj.GetComponent<DK_SlotsAnatomyElement>() != null)))
		{
			SelectedElemObj =  ( Selection.activeObject as GameObject);
			using (new Horizontal()) {
				GUILayout.Label ( "Element :", GUILayout.ExpandWidth (false));
				if ( Selection.activeObject.GetType().ToString() == "DKSlotData" ) {
					SelectedElemName = Selection.activeObject.name;
					SelectedElemType = "DKSlotData";
				}
				else
				if ( Selection.activeObject.GetType().ToString() == "DKOverlayData" ) {
					SelectedElemName = Selection.activeObject.name;
					SelectedElemType = "DKOverlayData";
				}
				else{

					if ( tmpObj.GetComponent<DK_SlotsAnatomyElement>() != null ) {
						SelectedElemName = Selection.activeObject.name;
						SelectedElemType = "Anatomy Part";
					}
				}
				GUILayout.Label ( SelectedElemName, GUILayout.Width (120));
				GUILayout.Label ( "Type :", GUILayout.ExpandWidth (false));
				GUILayout.Label ( SelectedElemType, GUILayout.Width (120));
			}
		
			using (new Horizontal()) {
				GUILayout.Label ( "Element's Races List", "toolbarbutton", GUILayout.ExpandWidth (true));
			}
			// Slots Only
			if ( (Selection.activeObject.GetType().ToString() == "DKSlotData")){
				DKSlotData _SlotData = (Selection.activeObject as DKSlotData);
				// clear
				using (new Horizontal()) {
					if ( GUILayout.Button ( "clear Element's list",  GUILayout.ExpandWidth (true))) {
						_SlotData.Race.Clear();
					}
					if ( GUILayout.Button ( "clear Races list", GUILayout.ExpandWidth (true))) {
						RaceDataList.Clear();
					}
					if ( RaceDataList.Count != 0 
						&& _SlotData.Race.Count == 00 
						&& GUILayout.Button ( "Add All", GUILayout.ExpandWidth (true))) 
					{
						if ( RaceDataList.Count != 0 && _SlotData.Race.Count == 00 ) {
							_SlotData.Race = RaceDataList;
							EditorUtility.SetDirty(_SlotData);
							AssetDatabase.SaveAssets();
						}
					}
				}
				for(int i = 0; i < Settings._GameLibraries.DkRacesLibrary.Count; i ++){
					if ( RaceDataList.Contains( Settings._GameLibraries.DkRacesLibrary[i].Race ) == false
						&& Settings._GameLibraries.DkRacesLibrary[i].Active ) RaceDataList.Add( Settings._GameLibraries.DkRacesLibrary[i].Race );
				}	
				using (new Horizontal()) {
					if (_SlotData.Race.Count > 0 )using (new ScrollView(ref scroll1)) 
					{
							
						for(int i = 0; i < _SlotData.Race.Count; i ++){
							using (new Horizontal()) {
								GUI.color = new Color (0.9f, 0.5f, 0.5f);
								if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
									_SlotData.Race.Remove(_SlotData.Race[i]);
									EditorUtility.SetDirty(_SlotData);
									AssetDatabase.SaveAssets();
								}
								GUI.color = Color.white;
								if ( i < _SlotData.Race.Count ) if ( GUILayout.Button ( _SlotData.Race[i], Slim, GUILayout.ExpandWidth (true))) {
									
								}
							}
						}
					}

					if (RaceDataList.Count > 0 )using (new ScrollView(ref scroll)) 
					{
							
						for(int i = 0; i < RaceDataList.Count; i ++){
							using (new Horizontal()) {
								if ( _SlotData.Race.Contains(RaceDataList[i]) ) GUI.color = Color.gray;
								else GUI.color = new Color (0.8f, 1f, 0.8f, 1);
								if ( GUILayout.Button ( "<", "toolbarbutton", GUILayout.ExpandWidth (false))) {
									_SlotData.Race.Add(RaceDataList[i]);
									EditorUtility.SetDirty(_SlotData);
									AssetDatabase.SaveAssets();
								}
								if ( _SlotData.Race.Contains(RaceDataList[i]) ) GUI.color = Color.gray;
								else GUI.color = Color.white;
								if ( GUILayout.Button ( RaceDataList[i], Slim, GUILayout.ExpandWidth (true))) {
									
								}
							}
						}
					}
				}
			}
			// Overlays Only
			if ( (Selection.activeObject.GetType().ToString() == "DKOverlayData")){
				DKOverlayData _DKOverlayData = (Selection.activeObject as DKOverlayData);
				// clear
				using (new Horizontal()) {
					if ( GUILayout.Button ( "clear Element's list",  GUILayout.ExpandWidth (true))) {
						_DKOverlayData.Race.Clear();
					}
					if ( GUILayout.Button ( "clear Races list", GUILayout.ExpandWidth (true))) {
						RaceDataList.Clear();
					}
					if ( RaceDataList.Count != 0 
						&& _DKOverlayData.Race.Count == 00 
						&& GUILayout.Button ( "Add All", GUILayout.ExpandWidth (true))) 
					{
						if ( RaceDataList.Count != 0 && _DKOverlayData.Race.Count == 00 ) _DKOverlayData.Race = RaceDataList;
					}
				}
				for(int i = 0; i < Settings._GameLibraries.DkRacesLibrary.Count; i ++){
					if ( RaceDataList.Contains( Settings._GameLibraries.DkRacesLibrary[i].Race ) == false 
						&& Settings._GameLibraries.DkRacesLibrary[i].Active )  
						RaceDataList.Add( Settings._GameLibraries.DkRacesLibrary[i].Race );
				}	
				using (new Horizontal()) {
					if (_DKOverlayData.Race.Count > 0 )using (new ScrollView(ref scroll1)) 
					{
							
						for(int i = 0; i < _DKOverlayData.Race.Count; i ++){
							using (new Horizontal()) {
								GUI.color = new Color (0.9f, 0.5f, 0.5f);
								if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
									_DKOverlayData.Race.Remove(_DKOverlayData.Race[i]);
									EditorUtility.SetDirty(_DKOverlayData);
									AssetDatabase.SaveAssets();
								}
								GUI.color = Color.white;
								if ( i < _DKOverlayData.Race.Count ) if ( GUILayout.Button ( _DKOverlayData.Race[i], Slim, GUILayout.ExpandWidth (true))) {
									
								}
							}
						}
					}
					if (RaceDataList.Count > 0 )using (new ScrollView(ref scroll)) 
					{
							
						for(int i = 0; i < RaceDataList.Count; i ++){
							using (new Horizontal()) {
								if ( _DKOverlayData.Race.Contains(RaceDataList[i]) ) GUI.color = Color.gray;
								else GUI.color = new Color (0.8f, 1f, 0.8f, 1);
								if ( GUILayout.Button ( "<", "toolbarbutton", GUILayout.ExpandWidth (false))) {
									_DKOverlayData.Race.Add(RaceDataList[i]);
									EditorUtility.SetDirty(_DKOverlayData);
									AssetDatabase.SaveAssets();
								}
								if ( _DKOverlayData.Race.Contains(RaceDataList[i]) ) GUI.color = Color.gray;
								else GUI.color = Color.white;
								if ( GUILayout.Button ( RaceDataList[i], Slim, GUILayout.ExpandWidth (true))) {
									
								}
							}
						}
					}
				}
			}
		}
		// Anatomy Only
		if ( EditorVariables.AutoDetLib == false && Selection.activeObject as GameObject && ((Selection.activeObject as GameObject).GetComponent<DK_Race>() != null)){
			DK_Race _DK_Race = (Selection.activeObject as GameObject).GetComponent<DK_Race>();

			// clear
			using (new Horizontal()) {
				if ( GUILayout.Button ( "clear Element's list",  GUILayout.ExpandWidth (true))) {
					_DK_Race.Race.Clear();
				}
				if ( GUILayout.Button ( "clear Races list", GUILayout.ExpandWidth (true))) {
					RaceDataList.Clear();
				}
				if ( RaceDataList.Count != 0 
					&& _DK_Race.Race.Count == 00 
					&& GUILayout.Button ( "Add All", GUILayout.ExpandWidth (true))) 
				{
					if ( RaceDataList.Count != 0 && _DK_Race.Race.Count == 00 ) _DK_Race.Race = RaceDataList;
				}
			}
			for(int i = 0; i < Settings._GameLibraries.DkRacesLibrary.Count; i ++){
				if ( RaceDataList.Contains( Settings._GameLibraries.DkRacesLibrary[i].Race ) == false
					&& Settings._GameLibraries.DkRacesLibrary[i].Active ) 
					RaceDataList.Add( Settings._GameLibraries.DkRacesLibrary[i].Race );
			}	
			using (new Horizontal()) {
				if (_DK_Race.Race.Count > 0 )using (new ScrollView(ref scroll1)) 
				{
						
					for(int i = 0; i < _DK_Race.Race.Count; i ++){
						using (new Horizontal()) {
							GUI.color = new Color (0.9f, 0.5f, 0.5f);
							if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
								_DK_Race.Race.Remove(_DK_Race.Race[i]);
								EditorUtility.SetDirty(_DK_Race);
								AssetDatabase.SaveAssets();
							}
							GUI.color = Color.white;
							if ( i < _DK_Race.Race.Count ) if ( GUILayout.Button ( _DK_Race.Race[i], Slim, GUILayout.ExpandWidth (true))) {
								
							}
						}
					}
				}
				if (RaceDataList.Count > 0 )using (new ScrollView(ref scroll)) 
				{
						
					for(int i = 0; i < RaceDataList.Count; i ++){
						using (new Horizontal()) {
							if ( _DK_Race.Race.Contains(RaceDataList[i]) ) GUI.color = Color.gray;
							else GUI.color = new Color (0.8f, 1f, 0.8f, 1);
							if ( GUILayout.Button ( "<", "toolbarbutton", GUILayout.ExpandWidth (false))) {
								_DK_Race.Race.Add(RaceDataList[i]);
								EditorUtility.SetDirty(_DK_Race);
								AssetDatabase.SaveAssets();
							}
							if ( _DK_Race.Race.Contains(RaceDataList[i]) ) GUI.color = Color.gray;
							else GUI.color = Color.white;
							if ( GUILayout.Button ( RaceDataList[i], Slim, GUILayout.ExpandWidth (true))) {
								
							}
						}
					}
				}
			}
		}
		// AutoDetectLib Only
		if ( EditorVariables.AutoDetLib == true ) {
			GUILayout.Label("AutoDetect Process Race's Section", GUILayout.ExpandWidth (true));
			if ( GUILayout.Button ( "Done",  GUILayout.ExpandWidth (true))) {
				EditorVariables.NewRaceName = "";
				for(int i = 0; i < EditorVariables.RaceToApplyList.Count; i ++){
					EditorVariables.NewRaceName = EditorVariables.NewRaceName+" -"+EditorVariables.RaceToApplyList[i];
				}
				this.Close();
			}
			// clear
			try{
			using (new Horizontal()) {
				if ( GUILayout.Button ( "clear Destination's list",  GUILayout.ExpandWidth (true))) {
				EditorVariables.RaceToApplyList.Clear();
				}
				if ( GUILayout.Button ( "clear Races list", GUILayout.ExpandWidth (true))) {
					RaceDataList.Clear();
				}
				if ( RaceDataList.Count != 0 
			 	   && EditorVariables.RaceToApplyList.Count == 00 
				    && GUILayout.Button ( "Add All", GUILayout.ExpandWidth (true))) 
				{
				if ( RaceDataList.Count != 0 && EditorVariables.RaceToApplyList.Count == 00 ) EditorVariables.RaceToApplyList = RaceDataList;
				}
			}
			}catch(ArgumentException){}
			for(int i = 0; i < Settings._GameLibraries.DkRacesLibrary.Count; i ++){
				if ( RaceDataList.Contains( Settings._GameLibraries.DkRacesLibrary[i].Race) == false
					&& Settings._GameLibraries.DkRacesLibrary[i].Active ) 
					RaceDataList.Add( Settings._GameLibraries.DkRacesLibrary[i].Race );
			}	
			using (new Horizontal()) {
			
				if (EditorVariables.RaceToApplyList.Count > 0 )using (new ScrollView(ref scroll1)) 
			
				{
					for(int i = 0; i < EditorVariables.RaceToApplyList.Count; i ++){
						using (new Horizontal()) {
							GUI.color = new Color (0.9f, 0.5f, 0.5f);
							if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
							EditorVariables.RaceToApplyList.Remove(EditorVariables.RaceToApplyList[i]);

							}
							GUI.color = Color.white;
						if ( i < EditorVariables.RaceToApplyList.Count ) if ( GUILayout.Button ( EditorVariables.RaceToApplyList[i], Slim, GUILayout.ExpandWidth (true))) {
								
							}
						}
					}
				}
				if (RaceDataList.Count > 0 )using (new ScrollView(ref scroll)) 
				{
					for(int i = 0; i < RaceDataList.Count; i ++){
						using (new Horizontal()) {
							if ( EditorVariables.RaceToApplyList.Contains(RaceDataList[i]) ) GUI.color = Color.gray;
							else GUI.color = new Color (0.8f, 1f, 0.8f, 1);
							if ( GUILayout.Button ( "<", "toolbarbutton", GUILayout.ExpandWidth (false))) {
									EditorVariables.RaceToApplyList.Add(RaceDataList[i]);
							}
							if ( EditorVariables.RaceToApplyList.Contains(RaceDataList[i]) ) GUI.color = Color.gray;
							else GUI.color = Color.white;
							if ( GUILayout.Button ( RaceDataList[i], Slim, GUILayout.ExpandWidth (true))) {
							
							}
						}
					}
				}
			}
		}
	}
	void OnSelectionChange (){
		Repaint ();
	}
}
