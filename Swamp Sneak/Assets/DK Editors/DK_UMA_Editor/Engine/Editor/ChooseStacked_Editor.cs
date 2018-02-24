using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0472 // Null is true.

public class ChooseStacked_Editor : EditorWindow {

	public static string Action = "";
	bool LinkedOverlayList;
//	public static  List<DKOverlayData> overlayList = new List<DKOverlayData>();
	DKOverlayData CurrentOverlay;

	Vector2 LinkedOverlayListScroll = new Vector2();
	Vector2 LinkedSlotListScroll = new Vector2();
	Vector2 scroll = new Vector2();
	string SearchString = "";

	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);

	void OnEnable (){
		CurrentOverlay = ScriptableObject.CreateInstance<DKOverlayData>();
	}

	void OnGUI () {
		this.minSize = new Vector2(460, 500);
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

		#region choose Overlay Tab
		Repaint();
		if ( Selection.activeObject && Selection.activeObject.GetType().ToString() != "DKOverlayData" ){
				using (new Horizontal()) {	
					GUI.color = Color.yellow;
					GUILayout.Label("First you need to select an overlay.", GUILayout.ExpandWidth (false));
					GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					
				}
				if ( GUILayout.Button ( "Close", "toolbarbutton", GUILayout.ExpandWidth (true))){
					Action = "";
					this.Close();
				}
			}
	

		if ( Selection.activeObject && Selection.activeObject.GetType().ToString() == "DKOverlayData" ){
			DKOverlayData _OverlayElement = Selection.activeObject as DKOverlayData;
		
				
			#region Linked Overlay List
				GUILayout.Space(5);
					

				if ( CurrentOverlay == null ) {
					using (new HorizontalCentered()) {	
						GUI.color = Color.yellow ;
					GUILayout.Label("No "+Action+" Overlay assigned. Select one from the list.", GUILayout.ExpandWidth (true));
					}
				}					
				GUI.color = Color.white ;
				using (new Horizontal()) {
					GUILayout.Label( Action+" Overlay :", "toolbarbutton", GUILayout.ExpandWidth (true));
				}
				using (new Horizontal()) {
				if ( Action == "Stacked01" ) CurrentOverlay = (Selection.activeObject as DKOverlayData).Opt01;
				else if ( Action == "Stacked02" ) CurrentOverlay = (Selection.activeObject as DKOverlayData).Opt02;
				else if ( Action == "Dirt01" ) CurrentOverlay = (Selection.activeObject as DKOverlayData).Dirt01;
				else if ( Action == "Dirt02" ) CurrentOverlay = (Selection.activeObject as DKOverlayData).Dirt02;

				if ( CurrentOverlay != null ) GUILayout.Label( CurrentOverlay.name, GUILayout.ExpandWidth (true)); 
					GUI.color = new Color (0.9f, 0.5f, 0.5f);
				if ( CurrentOverlay != null && GUILayout.Button ( "X" , "toolbarbutton", GUILayout.Width (20))) {
						if ( Action == "Stacked01" ) (Selection.activeObject as DKOverlayData).Opt01 = null;
						else if ( Action == "Stacked02" ) (Selection.activeObject as DKOverlayData).Opt02 = null;
						else if ( Action == "Dirt01" ) (Selection.activeObject as DKOverlayData).Dirt01 = null;
						else if ( Action == "Dirt02" ) (Selection.activeObject as DKOverlayData).Dirt02 = null;
						EditorUtility.SetDirty(Selection.activeObject);
						AssetDatabase.SaveAssets();
					}
				}
				#endregion
				
				GUILayout.Space(5);
				#region Overlay List
			

				if ( Selection.activeObject.GetType().ToString() == "DKOverlayData" ){
					using (new Horizontal()) {	
						GUI.color = Color.white ;
						GUILayout.Label("Select an Overlay for the Slot", "toolbarbutton", GUILayout.ExpandWidth (true));
					}
					

					using (new Horizontal()) {
					GUI.color = Color.white ;
					GUILayout.Label("Selected Overlay :", GUILayout.ExpandWidth (false));
					
						if ( EditorVariables.SelectedLinkedOvlay != null ) {
							GUI.color = new Color (0.8f, 1f, 0.8f, 1);
							GUILayout.Label(EditorVariables.SelectedLinkedOvlay.overlayName, GUILayout.ExpandWidth (true));
						
							if ( GUILayout.Button ( "Assign it", GUILayout.Width (90))) {
								DKOverlayData SelectedElement =  Selection.activeObject as DKOverlayData;
								if ( SelectedElement != null ){
									if (Action == "Stacked01" ) 
									(Selection.activeObject as DKOverlayData).Opt01 = EditorVariables.SelectedLinkedOvlay;
									if (Action == "Stacked02" ) 
										(Selection.activeObject as DKOverlayData).Opt02 = EditorVariables.SelectedLinkedOvlay;
							
								}
							EditorUtility.SetDirty(Selection.activeObject);
							AssetDatabase.SaveAssets();
						}
					}
						else {
						GUI.color = Color.yellow ;
						GUILayout.Label("Select an Overlay in the following List.", GUILayout.ExpandWidth (true));
					}
				}

					using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label ( "Overlay Library :", GUILayout.Width (110));
						GUI.color = Color.white;
						GUILayout.TextField ( EditorVariables.OverlayLibraryObj.name, GUILayout.ExpandWidth (true));
						if ( GUILayout.Button ( "Change", GUILayout.Width (60))){
							DK_UMA_Editor.OpenLibrariesWindow();
							ChangeLibrary.CurrentLibN = EditorVariables.OverlayLibraryObj.name;
							ChangeLibrary.CurrentLibrary = EditorVariables.OverlayLibraryObj;
							ChangeLibrary.Action = "";
						}
					}	
					#region Search
					using (new Horizontal()) {
						GUI.color = Color.white;
						GUILayout.Label("Search for :", GUILayout.Width (75));
						SearchString = GUILayout.TextField(SearchString, 100, GUILayout.ExpandWidth (true));
						
					}
					#endregion Search

					GUI.color = Color.cyan ;
					using (new Horizontal()) {
					GUILayout.Label("U", "toolbarbutton", GUILayout.Width (20));
					GUILayout.Label("Overlay", "toolbarbutton", GUILayout.Width (140));
				//	GUILayout.Label("Race", "toolbarbutton", GUILayout.Width (70));
					GUILayout.Label("Gender", "toolbarbutton", GUILayout.Width (70));
						GUILayout.Label("Place", "toolbarbutton", GUILayout.Width (70));
						GUILayout.Label("Overlay Type", "toolbarbutton", GUILayout.Width (90));
						GUILayout.Label("WearWeight", "toolbarbutton", GUILayout.Width (70));
						GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
					}
					using (new ScrollView(ref scroll)) 
					{
					DK_UMA_GameSettings GameSettings = GameObject.Find("DK_UMA").GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;
						#region Overlays
					/*	using (new Horizontal()) {	
							GUI.color = Color.yellow ;
							GUILayout.Label ( "Overlays Library :"+EditorVariables._OverlayLibrary.name, GUILayout.ExpandWidth (false));
						}*/
					for(int i = 0; i < GameSettings._GameLibraries.DkOverlaysLibrary.Count; i ++){
							if (GameSettings._GameLibraries.DkOverlaysLibrary[i] != null
							    && (SearchString == "" 
							    || GameSettings._GameLibraries.DkOverlaysLibrary[i].overlayName.ToLower().Contains(SearchString.ToLower())) )
							using (new Horizontal()) {
								DKOverlayData _DK_Race = GameSettings._GameLibraries.DkOverlaysLibrary[i];
								if ( _DK_Race.Active == true ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
								else GUI.color = Color.gray ;
							if (GUILayout.Button ( "U", "toolbarbutton",  GUILayout.Width (20))){
									if ( _DK_Race.Active == true ) _DK_Race.Active = false;
									else _DK_Race.Active = true;
									AssetDatabase.SaveAssets();
								} 
								
								if ( CurrentOverlay == GameSettings._GameLibraries.DkOverlaysLibrary[i] )  GUI.color = Color.yellow;
								else GUI.color = Color.white;
							if ( GameSettings._GameLibraries.DkOverlaysLibrary[i] == EditorVariables.SelectedLinkedOvlay ) GUI.color = Green;
						
							if (GUILayout.Button ( GameSettings._GameLibraries.DkOverlaysLibrary[i].overlayName , "toolbarbutton", GUILayout.Width (140))) {
									if ( CurrentOverlay != GameSettings._GameLibraries.DkOverlaysLibrary[i] )
									EditorVariables.SelectedLinkedOvlay = GameSettings._GameLibraries.DkOverlaysLibrary[i];
									
								}
							DKOverlayData Overlay = GameSettings._GameLibraries.DkOverlaysLibrary[i];
							if ( Overlay.Gender == "" && GUILayout.Button ( "No Gender" , "toolbarbutton", GUILayout.Width (70))) {
									EditorVariables.SelectedElemOvlay = GameSettings._GameLibraries.DkOverlaysLibrary[i];
								}
						
							if ( Overlay.Gender != "" && GUILayout.Button ( Overlay.Gender , "toolbarbutton", GUILayout.Width (70))) {
									EditorVariables.SelectedElemOvlay = GameSettings._GameLibraries.DkOverlaysLibrary[i];
								}
								// Place
							if ( Overlay.Place == null && GUILayout.Button ( "No Place" , "toolbarbutton", GUILayout.Width (70))) {
									
								}
						
							if ( Overlay.Place != null && GUILayout.Button ( Overlay.Place.name , "toolbarbutton", GUILayout.Width (70))) {
									EditorVariables.SelectedElemOvlay = GameSettings._GameLibraries.DkOverlaysLibrary[i];
								}
								// Overlay Type
							if ( Overlay.OverlayType == "" && GUILayout.Button ( "No Type" , "toolbarbutton", GUILayout.Width (90))) {
									EditorVariables.SelectedElemOvlay = GameSettings._GameLibraries.DkOverlaysLibrary[i];
								}
						
							if ( Overlay.OverlayType != "" && GUILayout.Button ( Overlay.OverlayType , "toolbarbutton", GUILayout.Width (90))) {
									EditorVariables.SelectedElemOvlay = GameSettings._GameLibraries.DkOverlaysLibrary[i];
								}
								// WearWeight
							if (  Overlay.WearWeight == "" ) 
								{
										GUILayout.Label ( "No Weight");
								}
								else {
								GUILayout.Label( Overlay.WearWeight , "toolbarbutton", GUILayout.Width (70));
								}
							}
						#endregion
					#endregion
					}
				}
			}
			#endregion
		}
	}
}
