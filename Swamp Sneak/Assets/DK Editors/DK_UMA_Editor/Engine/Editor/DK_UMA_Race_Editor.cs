using UnityEngine;
using UnityEditor;
using System;


public class DK_UMA_Race_Editor : EditorWindow {

	public static string Race = "";
	public static string RaceName = "";
	public static DKRaceData _RaceData;
	public static string Gender;
	
	
	Vector2 scroll;
	Vector2 scroll1;

		
	public static void OpenColorPresetWin()
    {
		GetWindow(typeof(ColorPreset_Editor), false, "Color Presets");
		ColorPreset_Editor.Statut = "ApplyTo";
	}
	public static void OpenDNAConverterWin()
    {
	//	GetWindow(typeof(DK_DNA_Convertor_Editor), false, "DNA Converter");
	}
	void OnGUI () {
		this.minSize = new Vector2(330, 450);
		#region fonts variables
		var bold = new GUIStyle ("label");
		var boldFold = new GUIStyle ("foldout");
		bold.fontStyle = FontStyle.Bold;
		bold.fontSize = 14;
		boldFold.fontStyle = FontStyle.Bold;
	//	var someMatched = false;
		
		var Slim = new GUIStyle ("label");
		Slim.fontStyle = FontStyle.Normal;
		Slim.fontSize = 10;	
		
		var style = new GUIStyle ("label");
				style.wordWrap = true;
		
		#endregion fonts variables
		
		Repaint();
	
		if ( _RaceData ) {
			RaceName = _RaceData.raceName;
			Race = _RaceData.Race;
		}
		using (new Horizontal()) {
			GUILayout.Label ( "DK UMA Race Editor", "toolbarbutton", GUILayout.ExpandWidth (true));
		}
		
		#region No race Selected
		if ( _RaceData == null ) {
			GUI.color = Color.yellow;
			GUILayout.TextField ( "Select a DK Race in the Project window.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			
		}
		#endregion No race Selected
		
		
		if ( _RaceData != null ) {
			using (new Horizontal()) {	
				GUILayout.Label ( "Name :", GUILayout.Width (50));
				RaceName = GUILayout.TextField(RaceName, 50, GUILayout.ExpandWidth (true));
			
			}
			using (new Horizontal()) {	
				GUILayout.Label ( "Race :", GUILayout.Width (50));
				Race = GUILayout.TextField(Race, 50, bold, GUILayout.ExpandWidth (true));
			}
			using (new Horizontal()) {	
				GUILayout.Label ( "Gender :", GUILayout.Width (50));
				GUILayout.Label ( _RaceData.Gender, GUILayout.Width (50));
							
			}
			GUI.color = new Color (0.8f, 1f, 0.8f, 1);
			if (GUILayout.Button ( "Apply All", GUILayout.ExpandWidth (true))) {
				_RaceData.Race = Race;
				_RaceData.name = RaceName;
			}
			GUILayout.Space(10);
			GUI.color = Color.white;
			using (new Horizontal()) {
				GUILayout.Label ( "DNA Converter", "toolbarbutton", GUILayout.ExpandWidth (true));
			}
		
			if ( _RaceData.dnaConverterList.Length > 0 ) 
			{
				for(int i = 0; i < _RaceData.dnaConverterList.Length; i ++){
					using (new Horizontal()) {
					
						GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					/*	if (GUILayout.Button ( "Edit DNA at your own risk", "toolbarbutton", GUILayout.ExpandWidth (false))) {
							OpenDNAConverterWin();
							DK_DNA_Convertor_Editor._DnaConverterBehaviour = _RaceData.dnaConverterList[i];
							DK_DNA_Convertor_Editor._RaceData = _RaceData;
							DK_DNA_Convertor_Editor.DNAName = DK_DNA_Convertor_Editor._DnaConverterBehaviour.name;
						
						}*/
						GUI.color = Color.white;
						if (GUILayout.Button ( _RaceData.dnaConverterList[i].name, Slim, GUILayout.ExpandWidth (true))) {
								
						}
					}
				}
			}
			
			GUI.color = Color.white;
			using (new Horizontal()) {
				GUILayout.Label ( "Color Presets", "toolbarbutton", GUILayout.ExpandWidth (true));
			}
			if (GUILayout.Button ( "Add New Color Presets", GUILayout.ExpandWidth (true))) {
				OpenColorPresetWin();
				ColorPreset_Editor._RaceData =  _RaceData;
				ColorPreset_Editor.Statut = "ApplyToRace";

			}

			if ( _RaceData ) using (new ScrollView(ref scroll)) 
			{
				// debug
				for(int i = 0; i < _RaceData.ColorPresetDataList.Count; i ++){
					using (new Horizontal()) {
						GUI.color = new Color (0.9f, 0.5f, 0.5f);
						if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
							_RaceData.ColorPresetDataList.Remove(_RaceData.ColorPresetDataList[i] );
						}
						GUI.color = Color.white;
						try {
							if (GUILayout.Button ( _RaceData.ColorPresetDataList[i].name, Slim, GUILayout.Width (150))) {
									
							}
							_RaceData.ColorPresetDataList[i].PresetColor = EditorGUILayout.ColorField("", _RaceData.ColorPresetDataList[i].PresetColor, GUILayout.Width (70));
							GUI.color = Color.white;
							GUILayout.Label ( _RaceData.ColorPresetDataList[i].OverlayType, GUILayout.ExpandWidth (false));
						}catch(ArgumentOutOfRangeException){}
					}
				}
			}
		}
	}
	
	void OnSelectionChange() {
		if ( Selection.activeObject && Selection.activeObject.GetType().ToString()  == "DKRaceData" ) {
			_RaceData = ( Selection.activeObject as DKRaceData );
			RaceName = _RaceData.raceName;
			Race = _RaceData.Race;
		}
	}
}
