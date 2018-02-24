using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class EditorSelectionChange : MonoBehaviour {

	
	public static void OnSelectionChange() {
		
		if ( Selection.activeObject && DK_UMA_Editor.ShowPrepare ){
			if ( Selection.activeObject.GetType().ToString() == "DKRaceData" 
			    || Selection.activeObject.GetType().ToString() == "DKSlotData"  
			    || Selection.activeObject.GetType().ToString() == "DKOverlayData"  ) 
			{
				DK_UMA_Editor.SelectedPrefabName =  Selection.activeObject.name;
				if ( Selection.activeObject.GetType().ToString() == "DKRaceData" ) {
					DKRaceData SelectedData = ( Selection.activeObject as DKRaceData );
					EditorVariables.SelectedElementName = SelectedData.raceName;
					DK_UMA_Editor._Name = SelectedData.Race;
					EditorVariables.SelectedElementGender = SelectedData.Gender;
					EditorVariables.SelectedElementObj = Selection.activeObject as GameObject;
					
				}
				if ( Selection.activeObject.GetType().ToString() == "DKSlotData" ) {
					DKSlotData SelectedData = ( Selection.activeObject as DKSlotData );
					EditorVariables.SelectedElementName = SelectedData.slotName;
					EditorVariables.SelectedElementGender = SelectedData.Gender;
					EditorVariables.overlayList = SelectedData.overlayList;
					EditorVariables.SelectedElementObj = Selection.activeObject as GameObject;
					EditorVariables.SelectedElementOverlayType = SelectedData.OverlayType;
					EditorVariables.SelectedElementWearWeight = SelectedData.WearWeight;
					EditorVariables.Replace = SelectedData.Replace;
					if ( SelectedData.Place != null )  EditorVariables.SelectedElemPlace = SelectedData.Place;
					
				}
				if ( Selection.activeObject.GetType().ToString() == "DKOverlayData" ) {
					DKOverlayData SelectedData = ( Selection.activeObject as DKOverlayData );
					EditorVariables.SelectedElementName = SelectedData.overlayName;
					EditorVariables.SelectedElementGender = SelectedData.Gender;
					EditorVariables.SelectedElementObj = Selection.activeObject as GameObject;
					EditorVariables.SelectedElementOverlayType = SelectedData.OverlayType;
					EditorVariables.SelectedElementWearWeight = SelectedData.WearWeight;
					if ( SelectedData.Place != null )  EditorVariables.SelectedElemPlace = SelectedData.Place;
				}
			}
		}
		if ( DK_UMA_Editor.showModify ) {
			// colors
			GameObject UMACustomObj = GameObject.Find("DKUMACustomization");
			if ( UMACustomObj != null ) 
			{
				EditorVariables.DK_DKUMACustomization =  UMACustomObj.GetComponent<DKUMACustomization>();
				if ( Selection.activeGameObject != null ) foreach ( Transform Child in Selection.activeGameObject.transform ) {
					if ( Child.gameObject.GetComponent< DKUMAData >() != null )
					{
					//	EditorVariables.DK_DKUMACustomization.EditedModel = Child;
						DK_UMA_Editor.EditedModel = Child;
					}
				}
			}
		}
		if ( Selection.activeGameObject && Selection.activeGameObject.GetComponent< DK_Race >() != null ) {
			DK_Race _DK_Race = Selection.activeGameObject.GetComponent< DK_Race >();	
			DK_UMA_Editor._SpawnPerct	= _DK_Race.SpawnPerct.ToString();
		}
		EditorVariables.SelectedElemSlot = null;
		EditorVariables.SelectedElemOvlay = null;
		EditorVariables.SelectedElementObj = null;
	}
}
