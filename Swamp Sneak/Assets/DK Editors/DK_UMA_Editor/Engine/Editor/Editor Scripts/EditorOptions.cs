using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class EditorOptions : MonoBehaviour {

	public static void DisplayWelcome () {
		DK_UMA_Editor.showCreate = false;
		DK_UMA_Editor.ShowPrepare = false;
		DK_UMA_Editor.showModify = false;
		DK_UMA_Editor.showList = false;
		DK_UMA_Editor.showSetup = false;
		DK_UMA_Editor.showCorrector = false;
		DK_UMA_Editor.showPlugIn = false;
		DK_UMA_Editor.showAbout = false;
		DK_UMA_Editor.ShowLibraries = false;
		DK_UMA_Editor.ShowGenPreset = false;
		DK_UMA_Editor.ShowDKLibraries = false;
		DK_UMA_Editor.Step0 = false;
		DK_UMA_Editor.ResetSteps ();
	}

	public static void DisplayPrepare () {
		DK_UMA_Editor.showCreate = false;
		DK_UMA_Editor.ShowPrepare = true;
		DK_UMA_Editor.showModify = false;
		DK_UMA_Editor.showList = false;
		DK_UMA_Editor.showSetup = false;
		DK_UMA_Editor.showCorrector = false;
		DK_UMA_Editor.showPlugIn = false;
		DK_UMA_Editor.showAbout = false;
		DK_UMA_Editor.ShowLibraries = false;
		DK_UMA_Editor.ShowGenPreset = false;
		DK_UMA_Editor.ShowDKLibraries = false;
		DK_UMA_Editor.Step0 = true;
		DK_UMA_Editor.ResetSteps ();
	//	DetectAndAddDK.DetectAll ();
	}
	public static void DisplayCreate () {
		DK_UMA_Editor.showCreate = true;
		DK_UMA_Editor.ShowPrepare = false;
		DK_UMA_Editor.showModify = false;
		DK_UMA_Editor.showList = false;
		DK_UMA_Editor.showSetup = false;
		DK_UMA_Editor.showCorrector = false;
		DK_UMA_Editor.showPlugIn = false;
		DK_UMA_Editor.showAbout = false;
		DK_UMA_Editor.ShowLibraries = false;
		DK_UMA_Editor.ShowGenPreset = false;
		DK_UMA_Editor.ShowDKLibraries = false;
		DK_UMA_Editor.Step0 = true;
		DK_UMA_Editor.ResetSteps ();
	//	DetectAndAddDK.DetectAll();
	}

	public static void DisplaySetup () {
		DK_UMA_Editor.showCreate = false;
		DK_UMA_Editor.ShowPrepare = false;
		DK_UMA_Editor.showModify = false;
		DK_UMA_Editor.showList = false;
		DK_UMA_Editor.showSetup = true;
		DK_UMA_Editor.showPlugIn = false;
		DK_UMA_Editor.showAbout = false;
		DK_UMA_Editor.showCorrector = false;
	//	DetectAndAddDK.DetectAll();
	}

	public static void DisplayCorrector () {
		DK_UMA_Editor.showCreate = false;
		DK_UMA_Editor.ShowPrepare = false;
		DK_UMA_Editor.showModify = false;
		DK_UMA_Editor.showList = false;
		DK_UMA_Editor.showSetup = false;
		DK_UMA_Editor.showPlugIn = false;
		DK_UMA_Editor.showAbout = false;
		DK_UMA_Editor.showCorrector = true;
		//	DetectAndAddDK.DetectAll();
	}

	public static void DisplayPlugIn () {
		DK_UMA_Editor.showCreate = false;
		DK_UMA_Editor.ShowPrepare = false;
		DK_UMA_Editor.showModify = false;
		DK_UMA_Editor.showList = false;
		DK_UMA_Editor.showSetup = false;
		DK_UMA_Editor.showPlugIn = true;
		DK_UMA_Editor.showAbout = false;
		DK_UMA_Editor.showCorrector = false;
	}

	public static void DisplayAbout () {
		DK_UMA_Editor.showCreate = false;
		DK_UMA_Editor.ShowPrepare = false;
		DK_UMA_Editor.showModify = false;
		DK_UMA_Editor.showList = false;
		DK_UMA_Editor.showSetup = false;
		DK_UMA_Editor.showPlugIn = false;
		DK_UMA_Editor.showAbout = true;
		DK_UMA_Editor.showCorrector = false;
	}

	public static void ApplyToGeneratorPreset() {
		// Add the elements
		bool ElemAlreadyIn = false;
		Array.Resize<DK_SlotsAnatomyElement>( ref DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList, DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList.Length - DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList.Length );
		DK_UMA_Editor._GeneratorPresetLibrary.UpdateDictionary();
		for(int i = 0; i < EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
			if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i] && EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected == true ) {
				var list = new DK_SlotsAnatomyElement[DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList.Length + 1];
				Array.Copy(DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList, list, DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList.Length );
				
				DK_SlotsAnatomyElement dk_SlotsAnatomyElement;
				
				GameObject _Prefab = PrefabUtility.GetPrefabParent( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject) as GameObject;	
				
				if ( _Prefab ) { dk_SlotsAnatomyElement = _Prefab.GetComponent<DK_SlotsAnatomyElement>();}
				else {dk_SlotsAnatomyElement = EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_SlotsAnatomyElement>();}
				
				list[DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList.Length] = dk_SlotsAnatomyElement;
				DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList = list;
				if ( !ElemAlreadyIn )
				{
					try{
						DK_UMA_Editor._GeneratorPresetLibrary.dk_GeneratorPresetDictionary.Add( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName, dk_SlotsAnatomyElement.dk_SlotsAnatomyElement );
						dk_SlotsAnatomyElement.dk_SlotsAnatomyElement.ElemAlreadyIn = true;
					}catch (Exception e ){Debug.Log (e.ToString());}
				}
			} 
		}
		EditorVariables.PresetToEdit.name = DK_UMA_Editor.NewPresetName;
		DK_UMA_Editor._GeneratorPresetLibrary.PresetName = DK_UMA_Editor.NewPresetName;
		DK_UMA_Editor._GeneratorPresetLibrary.ToGender = DK_UMA_Editor.NewPresetGender;
		EditorVariables.PresetToEdit = null;
	}

	public static void CreateGeneratorPreset() {
		bool ElemAlreadyIn = false;
		// create the Preset Object
		EditorVariables.New_DK_GeneratorPresetLibraryObj = GameObject.Find("New_DK_GeneratorPresetLibrary");
		if ( EditorVariables.New_DK_GeneratorPresetLibraryObj == null ) 
		{
			EditorVariables.New_DK_GeneratorPresetLibraryObj = (GameObject) Instantiate(Resources.Load("New_DK_GeneratorPresetLibrary"), Vector3.zero, Quaternion.identity);
			EditorVariables.New_DK_GeneratorPresetLibraryObj.name = "New_DK_GeneratorPresetLibrary";
			EditorVariables.New_DK_GeneratorPresetLibraryObj = GameObject.Find("New_DK_GeneratorPresetLibrary");
			
		}
		DK_UMA_Editor._GeneratorPresetLibrary =  EditorVariables.New_DK_GeneratorPresetLibraryObj.GetComponent<DK_GeneratorPresetLibrary>();
		GameObject DK_UMA = GameObject.Find("DK_UMA");
		if ( DK_UMA == null ) {
			var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
			DK_UMA = GameObject.Find("DK_UMA");
		}
		EditorVariables.GeneratorPresets = GameObject.Find("Generator Presets");
		if ( EditorVariables.GeneratorPresets == null ) 
		{
			EditorVariables.GeneratorPresets = (GameObject) Instantiate(Resources.Load("Generator Presets"), Vector3.zero, Quaternion.identity);
			EditorVariables.GeneratorPresets.name = "Generator Presets";
			EditorVariables.GeneratorPresets = GameObject.Find("Generator Presets");
			EditorVariables.GeneratorPresets.transform.parent = DK_UMA.transform;
		}
		
		EditorVariables.New_DK_GeneratorPresetLibraryObj.transform.parent = EditorVariables.GeneratorPresets.transform;
		EditorVariables.New_DK_GeneratorPresetLibraryObj.name = DK_UMA_Editor.NewPresetName;
		DK_UMA_Editor._GeneratorPresetLibrary.PresetName = DK_UMA_Editor.NewPresetName;
		DK_UMA_Editor._GeneratorPresetLibrary.ToGender = DK_UMA_Editor.NewPresetGender;
		
		
		// Add the elements
		for(int i = 0; i < EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
			if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected == true ) {
				var list = new DK_SlotsAnatomyElement[DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList.Length + 1];
				Array.Copy(DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList, list, DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList.Length );
				list[DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList.Length] = EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i];
				DK_UMA_Editor._GeneratorPresetLibrary.dk_SlotsAnatomyElementList = list;
				if ( !ElemAlreadyIn )DK_UMA_Editor._GeneratorPresetLibrary.dk_GeneratorPresetDictionary.Add( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName, EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement );
			}
		}
	}
}
