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
#pragma warning disable 0649 // Never Assigned.

public class ColorPreset_Editor : EditorWindow {
	
	public static string ColorPresetName = "";
	public static GameObject ColorPresetObj;
	public static Color CurrentElementColor;
	public static Color SelectedElementColor;
	
	public static string SelectedElement;
	public static string SelectedPresetOverlayType;
	ColorPresetData SelectedColorPreset;
	ColorPreset SelectedColorPreset0;
	PresetRaceAssign _PresetRaceAssignToMod;
	public int Tone1;
	public int Tone2;
	public int Tone3;
	string myPath;
	string SearchString = "";

	public static string Statut;
	public ColorPreset _ColorPreset;
	GameObject ColorPresets;
	GameObject DK_UMA;
	Vector2 scroll;
	Vector2 scroll2;

	GameObject LibraryObj;
	public static DKRaceData _RaceData;
	public static DKOverlayData _OverlayData;

	public enum TypeToShowEnum {All, Hair, Eyes, Flesh, Lips, InnerMouth, Cloth, Leather, Metal }
	public TypeToShowEnum TypeToShow;

	bool ShowPresets = false;

	GUIContent _Prefab = new GUIContent("P", "Create or Delete an Asset.");
	GUIContent _AutoDelMiss = new GUIContent("Auto Delete Missings", "Verify the Library and delete the missing fields, multiple clicks.");
	GUIContent _Delete = new GUIContent("X", "Delete.");
	GUIContent _Duplic = new GUIContent("C", "Create a copy.");
	GUIContent _Group = new GUIContent("G", "Add the Model to the selected Group. If the Model is already in a Group, the model is removed and placed at the Root.");
	GUIContent _Inst = new GUIContent("Instantiate", "Create an Instance of the Asset.");
	
	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);

	DK_UMA_GameSettings settings;

	private static Index<string, Index<string, List<object>>> _assetStoreCP = new Index<string, Index<string, List<object>>>();
	private Dictionary<string, bool> openCP = new Dictionary<string, bool> ();

	// Use this for initialization
	void OnEnable () {
		DetectColorPresets ();
		ColorPresets = GameObject.Find("Color Presets");
		if ( ColorPresets == null ) 
		{
			DK_UMA = GameObject.Find("DK_UMA");
			
			ColorPresets = (GameObject) Instantiate(Resources.Load("Color Presets"), Vector3.zero, Quaternion.identity);
			ColorPresets.name = "Color Presets";
			ColorPresets = GameObject.Find("Color Presets");
			try{
				ColorPresets.transform.parent = DK_UMA.transform;
			}catch(NullReferenceException){}
		}
	}

	void DetectColorPresets (){
		DK_UMA = GameObject.Find("DK_UMA");
		if ( DK_UMA != null ){
			settings = DK_UMA.GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;
			settings._GameLibraries.ColorPresetsList.Clear();
			string[] lookFor = new string[] {"Assets"};
			string[] guids2 = AssetDatabase.FindAssets ("t:ColorPresetData", lookFor);

			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				ColorPresetData element = (ColorPresetData)AssetDatabase.LoadAssetAtPath(path, typeof(ColorPresetData));
				if ( settings._GameLibraries.ColorPresetsList.Contains(element) == false ){
					settings._GameLibraries.ColorPresetsList.Add ( element );
				}
			}
			EditorUtility.SetDirty(settings);
			AssetDatabase.SaveAssets();
		}
		else Debug.LogError ("Your Scene is not ready for using DK UMA. Please Open the DK UMA Editor to auto install the necessary objects to your scene.");
	}

	void SetAlpha0 (){
		foreach (ColorPresetData preset in settings._GameLibraries.ColorPresetsList ){
			preset.PresetColor = new Color ( preset.PresetColor.r, preset.PresetColor.g, preset.PresetColor.b, 0 );
			EditorUtility.SetDirty(preset);
		}
		AssetDatabase.SaveAssets();
	}
	void SetAlpha255 (){
		foreach (ColorPresetData preset in settings._GameLibraries.ColorPresetsList ){
			preset.PresetColor = new Color ( preset.PresetColor.r, preset.PresetColor.g, preset.PresetColor.b, 255 );
			EditorUtility.SetDirty(preset);
		}
		AssetDatabase.SaveAssets();
	}

	void OnGUI () {
		this.minSize = new Vector2(420, 400);

		if ( LibraryObj == null ) LibraryObj = GameObject.Find("Color Presets");

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
		
		Repaint();

		using (new Horizontal()) {
			GUILayout.Label ( "Color Preset Editor", "toolbarbutton", GUILayout.ExpandWidth (true));
		}
		if ( Statut == "ToOverlay" && _OverlayData ) using (new Horizontal()) {
			GUILayout.Label ( "Selected Overlay :");
			GUILayout.Label ( _OverlayData.overlayName, bold);	
		}

		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Modify Alpha for all presets", "toolbarbutton", GUILayout.ExpandWidth (true));
		}

		using (new Horizontal()) {
			if ( GUILayout.Button ( "Set Alpha to 0", GUILayout.ExpandWidth (true))) {
				SetAlpha0 ();
			}
			if ( GUILayout.Button ( "Set Alpha to 255", GUILayout.ExpandWidth (true))) {
				SetAlpha255 ();
			}
		}


		if ( Statut == "ToOverlay" && _OverlayData && _OverlayData.ColorPresets.Count > 0 ){
			using (new Horizontal()) {	
				GUI.color = Color.white;
				if ( GUILayout.Button ( "Assigned Color Presets", "toolbarbutton"
					, GUILayout.ExpandWidth (true))) {
					if ( ShowPresets ) ShowPresets = false;
					else ShowPresets = true;
				}
				if ( ShowPresets ) GUI.color = Green;
				else GUI.color = Color.gray;
			    if ( GUILayout.Button ( "Show Color Presets", "toolbarbutton", GUILayout.ExpandWidth (false))) {
					if ( ShowPresets ) ShowPresets = false;
					else ShowPresets = true;
				}
			}

		}

		if ( Statut == "ToOverlay" && _OverlayData && _OverlayData.ColorPresets.Count > 0 && ShowPresets ) {
			GUI.color = Color.white;
			using (new Horizontal()) {
				using (new Vertical()) {
					GUILayout.Space (200);
				}
				using (new Vertical()) {
					using (new Horizontal()) {
						GUI.color = Color.white;
						using (new ScrollView(ref scroll2)) 
						{
							for (int i = 0; i < _OverlayData.ColorPresets.Count ; i++)
							{
								ColorPresetData preset = _OverlayData.ColorPresets[i];
								using (new Horizontal()) {
									GUI.color = Color.white;
									GUILayout.Label ( preset.ColorPresetName, "HelpBox", GUILayout.Width (200));
									EditorGUILayout.ColorField("", preset.PresetColor, GUILayout.Width (100));
									GUI.color = Red;
									if ( GUILayout.Button ( _Delete, "toolbarbutton", GUILayout.ExpandWidth (false))) {
										_OverlayData.ColorPresets.Remove (preset);
										EditorUtility.SetDirty(_OverlayData);
										AssetDatabase.SaveAssets();
									}
								}
							}
						}
					}
				}
			}
		}

		#region Color presets
		GUI.color = Color.white;
		if (LibraryObj.GetComponent<ColorPresetLibrary>() == null ){
			LibraryObj.AddComponent<ColorPresetLibrary>();
		}
		if ( DK_UMA == null ) DK_UMA = GameObject.Find("DK_UMA");
		settings = DK_UMA.GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;

		List<ColorPresetData> Library = settings._GameLibraries.ColorPresetsList;
		List<string> NamesList = new List<string>();
		#region Apply to Color Element Only

		//This draws a Line to seperate the Controls
		GUI.color = Color.white;
		GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

		if ( Statut == "ApplyTo" ) {
			using (new Horizontal()) {	
				GUILayout.Label ( "Color Element to modify :");
				GUILayout.Label ( SelectedElement, bold);
			
			}
			using (new Horizontal()) {
				GUILayout.Label ( "Current Color :", GUILayout.ExpandWidth (false));
				CurrentElementColor = EditorGUILayout.ColorField("", CurrentElementColor, GUILayout.ExpandWidth (true));
			
			}
		}
		#endregion Apply to Color Element Only
		
		#region Edit Color Presets Only
			
		#region Edit Race Only
			
		if ( Statut == "ApplyToRace" ) {
			string RaceName = _RaceData.raceName;
			string Race = _RaceData.Race;
			using (new Horizontal()) {
				GUILayout.Label ( "Name :");
				GUILayout.Label ( RaceName, bold);
			}
			using (new Horizontal()) {
				GUILayout.Label ( "Race :");
				GUILayout.Label ( Race, bold);	
			}
			if ( GUILayout.Button ( "Apply selected Preset to the Race", GUILayout.ExpandWidth (true))) {
				bool AlreadyIn = false;
				if ( _RaceData.ColorPresetDataList.Contains(SelectedColorPreset) == false ) {
					_RaceData.ColorPresetDataList.Add(SelectedColorPreset);
					if ( SelectedColorPreset.RacesList.Contains(_RaceData) == false ) {
						SelectedColorPreset.RacesList.Add(_RaceData);
					}
					EditorUtility.SetDirty(SelectedColorPreset);
					EditorUtility.SetDirty(_RaceData);
					AssetDatabase.SaveAssets();
				//	this.Close();
					Repaint();
				}
				else{
					Debug.Log ( SelectedColorPreset.ColorPresetName+" already in "+_RaceData.raceName);

				}
			}
		}
		#endregion Edit Race Only

		#region Edit Overlay Only
		if ( Selection.activeObject && ColorPresetName != "" && Statut == "ToOverlay" && settings != null && settings._GameLibraries.ColorPresetsList.Count > 0 ) {
		//	string RaceName = _OverlayData.overlayName;
			GUILayout.Space(10);
			if (SelectedColorPreset && GUILayout.Button ( "Apply to Overlay", GUILayout.ExpandWidth (true))) {
				bool AlreadyIn = false;
				if ( _OverlayData.ColorPresets.Contains(SelectedColorPreset) == false ) {
					_OverlayData.ColorPresets.Add(SelectedColorPreset);
				
					EditorUtility.SetDirty(_OverlayData);
					AssetDatabase.SaveAssets();
					Selection.activeObject = _OverlayData;
					this.Close();
					Repaint();
				}
				else{
					Debug.Log ( SelectedColorPreset.ColorPresetName+" already in "+_OverlayData.overlayName);
				}
			}
			if (SelectedColorPreset 
			    && SelectedColorPreset.OverlayType == "Metal"
			    && GUILayout.Button ( "Add all Metal Materials", GUILayout.ExpandWidth (true))) {

				foreach ( ColorPresetData preset in Library ) {
					if ( preset.OverlayType == "Metal" 
					    && _OverlayData.ColorPresets.Contains(preset) == false ) {
						_OverlayData.ColorPresets.Add(preset);
					}
				}
				EditorUtility.SetDirty(_OverlayData);
				AssetDatabase.SaveAssets();
			}
			else if (SelectedColorPreset 
			         && SelectedColorPreset.OverlayType == "Leather"
			         && GUILayout.Button ( "Add all Leather Materials", GUILayout.ExpandWidth (true))) {
				
				foreach ( ColorPresetData preset in Library ) {
					if ( preset.OverlayType == "Leather" 
					    && _OverlayData.ColorPresets.Contains(preset) == false ) {
						_OverlayData.ColorPresets.Add(preset);
					}
				}
				EditorUtility.SetDirty(_OverlayData);
				AssetDatabase.SaveAssets();
			}
			else if (SelectedColorPreset 
			         && SelectedColorPreset.OverlayType == "Cloth"
			         && GUILayout.Button ( "Add all Cloth Materials", GUILayout.ExpandWidth (true))) {
				
				foreach ( ColorPresetData preset in Library ) {
					if ( preset.OverlayType == "Cloth" 
					    && _OverlayData.ColorPresets.Contains(preset) == false ) {
						_OverlayData.ColorPresets.Add(preset);
					}
				}
				EditorUtility.SetDirty(_OverlayData);
				AssetDatabase.SaveAssets();
			}
		}
		try{
			#endregion Edit Overlay Only
			if ( /*Selection.activeObject &&*/ ColorPresetName != "" && SelectedColorPreset ) {
				if ( Statut == "ApplyTo" ) {
					if (GUILayout.Button ( "Apply Preset", GUILayout.ExpandWidth (true))) {
						try{
						if ( SelectedElement == "HeadWear" ) {
								EditorVariables.HeadWColorPresetName = ColorPresetName;

								EditorVariables.HeadWearColor = SelectedColorPreset.PresetColor;
								
							this.Close();
						}
						if ( SelectedElement == "TorsoWear" ) {
								EditorVariables.TorsoWColorPresetName = ColorPresetName;
							EditorVariables.TorsoWearColor = SelectedColorPreset.PresetColor;
							this.Close();
						}
						if ( SelectedElement == "LegsWear" ) {
								EditorVariables.LegsWColorPresetName = ColorPresetName;
							EditorVariables.LegsWearColor = SelectedColorPreset.PresetColor;
							this.Close();
						}
						if ( SelectedElement == "HandWear" ) {
								EditorVariables.HandWColorPresetName = ColorPresetName;
							EditorVariables.HandWearColor = SelectedColorPreset.PresetColor;
							this.Close();
						}
						if ( SelectedElement == "BeltWear" ) {
								EditorVariables.BeltWColorPresetName = ColorPresetName;
							EditorVariables.BeltWearColor = SelectedColorPreset.PresetColor;
							this.Close();
						}
						if ( SelectedElement == "FeetWear" ) {
								EditorVariables.FeetWColorPresetName = ColorPresetName;
							EditorVariables.FeetWearColor = SelectedColorPreset.PresetColor;
							this.Close();
						}
						if ( SelectedElement == "Skin" ) {
								EditorVariables.SkinColorPresetName = ColorPresetName;
							EditorVariables.SkinColor = SelectedColorPreset.PresetColor;
							this.Close();
						}
						if ( SelectedElement == "Hair" ) {
								EditorVariables.HairColorPresetName = ColorPresetName;
							EditorVariables.HairColor = SelectedColorPreset.PresetColor;
							this.Close();
						}
						if ( SelectedElement == "Eyes" ) {
								EditorVariables.EyesColorPresetName = ColorPresetName;
							EditorVariables.EyesColor = SelectedColorPreset.PresetColor;
							this.Close();
						}
							}catch(NullReferenceException){Debug.Log("Select a color preset to apply.");}
					/*	if ( SelectedElement == "InnerMouth" ) {
							EditorVariables.InnerMouthColorPresetName = ColorPresetName;
							EditorVariables.InnerMouthColor = SelectedColorPreset.PresetColor;
							this.Close();
						}
					*/
						
					}
				}
				if ( SelectedColorPreset ) using (new Horizontal()) {	
					GUILayout.Label ( "Selected Color Preset :");
					ColorPresetName= GUILayout.TextField(ColorPresetName, 50, bold, GUILayout.ExpandWidth (true));
					if (GUILayout.Button ( "Rename", GUILayout.ExpandWidth (false))) {
						SelectedColorPreset.name = ColorPresetName;
						SelectedColorPreset.ColorPresetName = ColorPresetName;
							string path = AssetDatabase.GetAssetPath(SelectedColorPreset);
							AssetDatabase.RenameAsset(path , ColorPresetName );
					}
				}
				if ( SelectedColorPreset && SelectedColorPreset.PresetColor != null ) using (new Horizontal()) {
					GUILayout.Label ( "Preset Color :", GUILayout.ExpandWidth (false));
					SelectedColorPreset.PresetColor = EditorGUILayout.ColorField("", SelectedColorPreset.PresetColor, GUILayout.ExpandWidth (true));
				}
				#region Edit Color Preset Overlay
				if ( SelectedColorPreset )
					SelectedPresetOverlayType = SelectedColorPreset.OverlayType;
				using (new Horizontal()) {
					GUI.color = Color.white;
					GUILayout.Label ( "Body :", GUILayout.ExpandWidth (false));
					if ( SelectedPresetOverlayType == "Flesh" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Flesh", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "Flesh";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "Lips" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Lips", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "Lips";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
						
					}
					if ( SelectedPresetOverlayType == "InnerMouth" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Inner Mouth", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "InnerMouth";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "Eyes" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Eyes", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "Eyes";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}	
					if ( SelectedPresetOverlayType == "Hair" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Hair", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "Hair";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
				}
				using (new Horizontal()) {
					GUI.color = Color.white;
					GUILayout.Label ( "Wears :", GUILayout.ExpandWidth (false));
					if ( SelectedPresetOverlayType == "TorsoWear" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Torso", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "TorsoWear";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "LegsWear" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Legs", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "LegsWear";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "FeetWear" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Feet", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "FeetWear";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "HandsWear" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Hands", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "HandsWear";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "HeadWear" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Head", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "HeadWear";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "Underwear" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Underwear", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "Underwear";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "None", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
				}
				using (new Horizontal()) {
					GUI.color = Color.white;
					GUILayout.Label ( "Material :", GUILayout.ExpandWidth (false));
					if ( SelectedPresetOverlayType == "Metal" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Metal", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "Metal";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "Cloth" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Cloth", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "Cloth";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "Leather" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Leather", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "Leather";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
					if ( SelectedPresetOverlayType == "Wood" ) GUI.color = new Color (0.8f, 1f, 0.8f, 1);
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Wood", GUILayout.ExpandWidth (true))) {
						SelectedPresetOverlayType = "Wood";
						SelectedColorPreset =   Selection.activeObject as ColorPresetData;
						if ( SelectedColorPreset != null ) 
						{
							SelectedColorPreset.OverlayType = SelectedPresetOverlayType;
							EditorUtility.SetDirty(SelectedColorPreset);
							AssetDatabase.SaveAssets();
						}
					}
				}
				#endregion Edit Color Preset Overlay
			}
			else {

				using (new HorizontalCentered()) {
					GUI.color = Color.yellow;
					GUILayout.Label ( "Select a Color Preset from the list bellow.", GUILayout.ExpandWidth (false));
				}	
				GUILayout.Space(10);
			}
			using (new Horizontal()) {
				GUI.color = Color.white;
				if ( GUILayout.Button ( "Create a New Color Preset", GUILayout.ExpandWidth (true))) {
					ColorPresetData _NewColorPreset;
					_NewColorPreset = ScriptableObject.CreateInstance("ColorPresetData") as ColorPresetData;
					if ( Statut != "ApplyToRace" ) Selection.activeObject = _NewColorPreset;
					else SelectedColorPreset = _NewColorPreset;
					_NewColorPreset.name = "New Color Preset";
					_NewColorPreset.ColorPresetName = "New Color Preset";
					AssetDatabase.CreateAsset(_NewColorPreset as UnityEngine.Object, "Assets/DK Editors/DK_UMA_Content/Color Presets/" + _NewColorPreset.name + ".asset");
					DetectColorPresets ();
					Debug.Log ("creating "+_NewColorPreset.name );
				}
			}
			GUILayout.Space(5);
			#region Search
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label("Search for :", GUILayout.ExpandWidth (false));
				SearchString = GUILayout.TextField(SearchString, 100, GUILayout.ExpandWidth (true));
			}
			using (new Horizontal()) {
				GUILayout.Label("Type To Show :", GUILayout.ExpandWidth (false)); 
				TypeToShow = (TypeToShowEnum) EditorGUILayout.EnumPopup("", TypeToShow);
			}
			#endregion Search
			GUILayout.Space(5);

			#region Color Presets List
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label ( "Color Presets List", "toolbarbutton", GUILayout.ExpandWidth (true));
			}

			if (settings._GameLibraries.ColorPresetsList.Count > 0){
				using (new ScrollView(ref scroll)) {		
					for (int i = 0; i < settings._GameLibraries.ColorPresetsList.Count ; i++){
						ColorPresetData preset = settings._GameLibraries.ColorPresetsList[i];
						if ( preset != null
							&& ( TypeToShow.ToString() == "All" || preset.OverlayType == "" || preset.OverlayType == TypeToShow.ToString() )
							&& ( preset.name.ToLower().Contains (SearchString.ToLower()) || SearchString == "" ) )
						using (new Horizontal()) {
							if ( SelectedColorPreset == preset ) GUI.color = Color.yellow;
							else GUI.color = Color.white;
							
								if ( preset != null && GUILayout.Button (preset.ColorPresetName, "HelpBox", GUILayout.ExpandWidth (true))){
									if (  Statut != "ToOverlay" &&  Statut != "ApplyToRace" ) Selection.activeObject = preset;
								SelectedColorPreset = preset;
								ColorPresetName = preset.name;
							}
								if ( preset != null && GUILayout.Button (">", "HelpBox", GUILayout.ExpandWidth (false))){
									bool AlreadyIn = false;
									if ( _RaceData.ColorPresetDataList.Contains(preset) == false ) {
										_RaceData.ColorPresetDataList.Add(preset);
										if ( preset.RacesList.Contains(_RaceData) == false ) {
											preset.RacesList.Add(_RaceData);
										}
										EditorUtility.SetDirty(preset);
										EditorUtility.SetDirty(_RaceData);
										AssetDatabase.SaveAssets();
										//	this.Close();
										Repaint();
									}
									else{
										Debug.Log ( SelectedColorPreset.ColorPresetName+" already in "+_RaceData.raceName);

									}
								}
								if ( preset != null && GUILayout.Button (preset.OverlayType, "HelpBox", GUILayout.Width (70))){
									if (  Statut != "ToOverlay" &&  Statut != "ApplyToRace" ) Selection.activeObject = preset;
									SelectedColorPreset = preset;
									ColorPresetName = preset.name;
								}

							EditorGUILayout.ColorField("", preset.PresetColor, GUILayout.Width (40));
							if ( GUILayout.Button ("X", "toolbarbutton", GUILayout.ExpandWidth (false))){

								AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(preset));
								DetectColorPresets();
							}
						}
					}
				}
			}
			}catch(ArgumentException){}
			#endregion

		#endregion Edit Color Presets Only
		
		#endregion HeadWear Color
	}

	void OnSelectionChange() {
		if ( Selection.activeObject ){
			if ( Selection.activeObject.GetType().ToString() == "DKRaceData" && Statut == "ApplyToRace" ) 
				_RaceData = ( Selection.activeObject as DKRaceData );

			if ( Selection.activeObject.GetType().ToString() == "ColorPresetData" ){
				SelectedColorPreset = Selection.activeObject as ColorPresetData;
				ColorPresetName = SelectedColorPreset.name;
				SelectedPresetOverlayType = SelectedColorPreset.OverlayType;
				CurrentElementColor = SelectedColorPreset.PresetColor;
			}
		}
	}
}
