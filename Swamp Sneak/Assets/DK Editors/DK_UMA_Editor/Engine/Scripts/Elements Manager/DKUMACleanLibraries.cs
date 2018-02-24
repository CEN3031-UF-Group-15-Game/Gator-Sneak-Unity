using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UMA;
#if UNITY_EDITOR
using UnityEditor;
#endif

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0618 // variable declared but not used.

public class DKUMACleanLibraries : MonoBehaviour {
		
	public static void CleanLibraries (){
		GameObject UMA = GameObject.Find("UMA_DCS");
		if ( UMA == null ) UMA = GameObject.Find("UMA");

		if ( GameObject.Find ("DK_UMA") == null ) {
			Debug.LogError ("You need to install DK UMA to your scene ! Open the 'DK UMA Editor' to automaticaly add DK UMA to your scene.");
		}
		else if ( UMA == null ) {
			Debug.LogError ("You need to install UMA to your scene ! Open the 'Elements Manager' and Install UMA to the scene.");
		}
		else {
			// DK UMA
			DKSlotLibrary slotLibrary = GameObject.Find ("DK_UMA").GetComponentInChildren<DKSlotLibrary>();
			DKOverlayLibrary overlayLibrary = GameObject.Find ("DK_UMA").GetComponentInChildren<DKOverlayLibrary>();

			// UMA
			#if UMADCS
			DynamicSlotLibrary UslotLibrary = FindObjectOfType<DynamicSlotLibrary>();
			DynamicOverlayLibrary UoverlayLibrary = FindObjectOfType<DynamicOverlayLibrary>();
			#else
			SlotLibrary UslotLibrary = FindObjectOfType<SlotLibrary>();
			OverlayLibrary UoverlayLibrary = FindObjectOfType<OverlayLibrary>();
			#endif

			if ( slotLibrary != null && overlayLibrary != null && UslotLibrary != null && UoverlayLibrary != null )
				CleanLibraries ( slotLibrary, overlayLibrary, UslotLibrary, UoverlayLibrary );
			else {
				if ( slotLibrary != null  ) Debug.LogError ("The DK UMA Slots Library is missing from your scene !");
				if ( overlayLibrary != null  ) Debug.LogError ("The DK UMA Overlays Library is missing from your scene !");
				if ( UslotLibrary != null  ) Debug.LogError ("The UMA Slots Library is missing from your scene !");
				if ( UoverlayLibrary != null  ) Debug.LogError ("The UMA Overlays Library is missing from your scene !");		
			}
		}
	}
	#if UMADCS
	public static void CleanLibraries ( DKSlotLibrary slotLibrary, DKOverlayLibrary overlayLibrary, DynamicSlotLibrary UslotLibrary, DynamicOverlayLibrary UoverlayLibrary ){
		List<DKSlotData> tmpList = new List<DKSlotData>();
		List<DKSlotData> okList = new List<DKSlotData>();
		List<DKSlotData> RemoveList = new List<DKSlotData>();
		List<DKOverlayData> tmpList2 = new List<DKOverlayData>();
		List<DKOverlayData> removeList2 = new List<DKOverlayData>();

		if ( slotLibrary != null && overlayLibrary != null ){
			tmpList = slotLibrary.slotElementList.ToList();
			for(int i = 0; i < tmpList.Count; i ++){
				if ( tmpList[i] == null ) RemoveList.Add(tmpList[i]);
				else {
					if ( okList.Contains ( tmpList[i]) == false ){
						okList.Add (tmpList[i]);
					}
				}
			}
			tmpList = okList;
			
			tmpList2 = overlayLibrary.overlayElementList.ToList();
			for(int i = 0; i < tmpList2.Count; i ++){
				if ( tmpList2[i] == null ) removeList2.Add(tmpList2[i]);
			}
			foreach ( DKSlotData slot in RemoveList ){
				if ( tmpList.Contains (slot) ) tmpList.Remove (slot);
			}
			foreach ( DKOverlayData overlay in removeList2 ){
				if ( tmpList2.Contains (overlay) ) tmpList2.Remove (overlay);
			}

			slotLibrary.slotElementList = tmpList.ToArray();
			overlayLibrary.overlayElementList = tmpList2.ToArray();
			if ( FindObjectOfType<DynamicSlotLibrary>() != null ){

				UslotLibrary = FindObjectOfType<DynamicSlotLibrary>();
				UoverlayLibrary = FindObjectOfType<DynamicOverlayLibrary>();
				CleanLibrariesUMA ( UslotLibrary, UoverlayLibrary );
			}
			else Debug.LogError ("You need to install UMA to your scene ! Open the 'Elements Manager' and click on 'Install UMA'.");
		
			CleanDKRaceLibrary ();
			CleanUMARaceLibrary ();
			CleanLibWizard ();
			CleanEquipmentSets ();
		}
	}
	#else
	public static void CleanLibraries ( DKSlotLibrary slotLibrary, DKOverlayLibrary overlayLibrary, SlotLibrary UslotLibrary, OverlayLibrary UoverlayLibrary ){
		List<DKSlotData> tmpList = new List<DKSlotData>();
		List<DKSlotData> okList = new List<DKSlotData>();
		List<DKSlotData> RemoveList = new List<DKSlotData>();
		List<DKOverlayData> tmpList2 = new List<DKOverlayData>();
		List<DKOverlayData> removeList2 = new List<DKOverlayData>();

		if ( slotLibrary != null && overlayLibrary != null ){
			tmpList = slotLibrary.slotElementList.ToList();
			for(int i = 0; i < tmpList.Count; i ++){
				if ( tmpList[i] == null ) RemoveList.Add(tmpList[i]);
				else {
					if ( okList.Contains ( tmpList[i]) == false ){
						okList.Add (tmpList[i]);
					}
				}
			}
			tmpList = okList;

			tmpList2 = overlayLibrary.overlayElementList.ToList();
			for(int i = 0; i < tmpList2.Count; i ++){
				if ( tmpList2[i] == null ) removeList2.Add(tmpList2[i]);
			}
			foreach ( DKSlotData slot in RemoveList ){
				if ( tmpList.Contains (slot) ) tmpList.Remove (slot);
			}
			foreach ( DKOverlayData overlay in removeList2 ){
				if ( tmpList2.Contains (overlay) ) tmpList2.Remove (overlay);
			}

			slotLibrary.slotElementList = tmpList.ToArray();
			overlayLibrary.overlayElementList = tmpList2.ToArray();
			if ( FindObjectOfType<SlotLibrary>() != null ){

				UslotLibrary = FindObjectOfType<SlotLibrary>();
				UoverlayLibrary = FindObjectOfType<OverlayLibrary>();
				CleanLibrariesUMA ( UslotLibrary, UoverlayLibrary );
			}
		//	else Debug.LogError ("You need to install UMA to your scene ! Open the 'Elements Manager' and click on 'Install UMA'.");

			CleanDKRaceLibrary ();
			CleanUMARaceLibrary ();
			CleanLibWizard ();
			CleanEquipmentSets ();
		}
	}

	#endif

	public static void CleanDKRaceLibrary (){
		List<DKRaceData> tmpList = new List<DKRaceData>();
		List<DKRaceData> okList = new List<DKRaceData>();
		List<DKRaceData> RemoveList = new List<DKRaceData>();

		if ( GameObject.Find ("DK_UMA") != null ){
			DKRaceLibrary Library = GameObject.Find ("DK_UMA").GetComponentInChildren<DKRaceLibrary>();

			if ( Library != null ){
				tmpList = Library.raceElementList.ToList();
				for(int i = 0; i < tmpList.Count; i ++){
					if ( tmpList[i] == null ) RemoveList.Add(tmpList[i]);
					else {
						if ( okList.Contains ( tmpList[i]) == false ){
							okList.Add (tmpList[i]);
						}
					}
				}
				tmpList = okList;

				foreach ( DKRaceData race in RemoveList ){
					if ( tmpList.Contains (race) ) tmpList.Remove (race);
				}

				Library.raceElementList = tmpList.ToArray();
				GameObject UMA = GameObject.Find("UMA_DCS");
				if ( UMA == null ) UMA = GameObject.Find("UMA");

				#if UMADCS
				if ( UMA!= null 
					&& FindObjectOfType<DynamicRaceLibrary>() != null ){
				}
				else Debug.LogError ("You need to install UMA_DCS to your scene ! Open the 'Elements Manager' and click on 'Install UMA'.");
				#else
				if ( UMA!= null 
					&& FindObjectOfType<RaceLibrary>() != null ){
				}
				else Debug.LogError ("You need to install UMA to your scene ! Open the 'Elements Manager' and click on 'Install UMA'.");
				#endif
			}
		}
	}

	public static void CleanUMARaceLibrary (){
		List<UMA.RaceData> tmpList = new List<UMA.RaceData>();
		List<UMA.RaceData> okList = new List<UMA.RaceData>();
		List<UMA.RaceData> RemoveList = new List<UMA.RaceData>();
		#if UMADCS
		DynamicRaceLibrary Library = FindObjectOfType<DynamicRaceLibrary>();

		// convert the libraries array to a list
		tmpList = Library.GetAllRaces().ToList();

		// search missing fields
		for(int i = 0; i < tmpList.Count; i ++){
			if ( tmpList[i] == null ) RemoveList.Add(tmpList[i]);
		}
		// clean missing fields
		foreach ( UMA.RaceData race in RemoveList ){
			if ( tmpList.Contains (race) ) tmpList.Remove (race);
		}
		// convert the tmp lists to a libraries array
		List<UMA.RaceData> existingList = Library.GetAllRacesBase ().ToList();
		foreach ( RaceData race in tmpList ){
			// verify if contains
			if ( existingList.Contains (race) == false )
				Library.AddRace(race);
		}
		#else
		RaceLibrary Library = FindObjectOfType<RaceLibrary>();

		// convert the libraries array to a list
		if ( Library != null ){
			tmpList = Library.GetAllRaces().ToList();

			// search missing fields
			for(int i = 0; i < tmpList.Count; i ++){
				if ( tmpList[i] == null ) RemoveList.Add(tmpList[i]);
			}
			// clean missing fields
			foreach ( UMA.RaceData race in RemoveList ){
				if ( tmpList.Contains (race) ) tmpList.Remove (race);
			}
			// convert the tmp lists to a libraries array
			List<UMA.RaceData> existingList = Library.GetAllRaces ().ToList();
			foreach ( RaceData race in tmpList ){
				// verify if contains
				if ( existingList.Contains (race) == false )
					Library.AddRace(race);
			}
		}
		#endif
	}

	public static void CleanLibWizard (){
		List<DKSlotData> DKSlotRemoveList = new List<DKSlotData>();
		List<DKOverlayData> DKOverlayRemoveList = new List<DKOverlayData>();
		List<UMA.SlotDataAsset> UMASlotRemoveList = new List<UMA.SlotDataAsset>();
		List<UMA.OverlayDataAsset> UMAOverlayRemoveList = new List<UMA.OverlayDataAsset>();

		GameObject DK_UMA = GameObject.Find("DK_UMA");
		if ( DK_UMA != null ) {
			// get DK UMA Game Settings
			DK_UMA_GameSettings GameSettings = DK_UMA.transform.GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;
			
			// clean Libraries Wizard
			if ( GameSettings != null ) {
				for(int i = 0; i < GameSettings._GameLibraries.DkSlotsLibrary.Count; i ++){
					if ( GameSettings._GameLibraries.DkSlotsLibrary[i] == null ) 
						DKSlotRemoveList.Add(GameSettings._GameLibraries.DkSlotsLibrary[i]);
				}
				for(int i = 0; i < GameSettings._GameLibraries.DkOverlaysLibrary.Count; i ++){
					if ( GameSettings._GameLibraries.DkOverlaysLibrary[i] == null ) 
						DKOverlayRemoveList.Add(GameSettings._GameLibraries.DkOverlaysLibrary[i]);
				}
				for(int i = 0; i < GameSettings._GameLibraries.UmaSlotsLibrary.Count; i ++){
					if ( GameSettings._GameLibraries.UmaSlotsLibrary[i] == null ) 
						UMASlotRemoveList.Add(GameSettings._GameLibraries.UmaSlotsLibrary[i]);
				}
				for(int i = 0; i < GameSettings._GameLibraries.UmaOverlaysLibrary.Count; i ++){
					if ( GameSettings._GameLibraries.UmaOverlaysLibrary[i] == null ) 
						UMAOverlayRemoveList.Add(GameSettings._GameLibraries.UmaOverlaysLibrary[i]);
				}
			}

			// clean missing fields
			foreach ( DKSlotData slot in DKSlotRemoveList ){
				if (  GameSettings._GameLibraries.DkSlotsLibrary.Contains (slot) )  
					GameSettings._GameLibraries.DkSlotsLibrary.Remove (slot);
			}
			foreach ( DKOverlayData overlay in DKOverlayRemoveList ){
				if ( GameSettings._GameLibraries.DkOverlaysLibrary.Contains (overlay) ) 
					GameSettings._GameLibraries.DkOverlaysLibrary.Remove (overlay);
			}
			foreach ( SlotDataAsset slot in UMASlotRemoveList ){
				if ( GameSettings._GameLibraries.UmaSlotsLibrary.Contains (slot) ) 
					GameSettings._GameLibraries.UmaSlotsLibrary.Remove (slot);
			}
			foreach ( OverlayDataAsset overlay in UMAOverlayRemoveList ){
				if ( GameSettings._GameLibraries.UmaOverlaysLibrary.Contains (overlay) ) 
					GameSettings._GameLibraries.UmaOverlaysLibrary.Remove (overlay);
			}
		}
	}

	#if UMADCS
	public static void CleanLibrariesUMA ( DynamicSlotLibrary UslotLibrary, DynamicOverlayLibrary UoverlayLibrary ){
		// create the temp lists
		List<UMA.SlotDataAsset> tmpList = new List<UMA.SlotDataAsset>();
		List<UMA.SlotDataAsset> RemoveList = new List<UMA.SlotDataAsset>();
		List<UMA.OverlayDataAsset> tmpList2 = new List<UMA.OverlayDataAsset>();
		List<UMA.OverlayDataAsset> removeList2 = new List<UMA.OverlayDataAsset>();
		
		// find the UMA libraries
		UslotLibrary = FindObjectOfType<DynamicSlotLibrary>();
		UoverlayLibrary = FindObjectOfType<DynamicOverlayLibrary>();
		
		// convert the libraries array to a list
		tmpList = UslotLibrary.GetAllSlotAssets().ToList();
		tmpList2 = UoverlayLibrary.GetAllOverlayAssets().ToList();
		
		// search missing fields
		for(int i = 0; i < tmpList.Count; i ++){
			if ( tmpList[i] == null ) RemoveList.Add(tmpList[i]);
		}
		for(int i = 0; i < tmpList2.Count; i ++){
			if ( tmpList2[i] == null ) removeList2.Add(tmpList2[i]);
		}
		
		// clean missing fields
		foreach ( SlotDataAsset slot in RemoveList ){
			if ( tmpList.Contains (slot) ) tmpList.Remove (slot);
		}
		foreach ( OverlayDataAsset overlay in removeList2 ){
			if ( tmpList2.Contains (overlay) ) tmpList2.Remove (overlay);
		}

		// convert the tmp lists to a libraries array
	//	UslotLibrary.slotElementList = tmpList.ToArray();
	//	UoverlayLibrary.overlayElementList = tmpList2.ToArray();

		List<UMA.SlotDataAsset> existingList = UslotLibrary.GetAllSlotAssets ().ToList();
		foreach ( UMA.SlotDataAsset element in tmpList ){
			if ( existingList.Contains (element) == false )
				UslotLibrary.AddSlotAsset(element);
		}

		List<UMA.OverlayDataAsset> existingList2 = UoverlayLibrary.GetAllOverlayAssets ().ToList();
		foreach ( UMA.OverlayDataAsset element in tmpList2 ){
			if ( existingList2.Contains (element) == false )
				UoverlayLibrary.AddOverlayAsset(element);
		}
	}
	#else
	public static void CleanLibrariesUMA ( SlotLibrary UslotLibrary, OverlayLibrary UoverlayLibrary ){
		// create the temp lists
		List<UMA.SlotDataAsset> tmpList = new List<UMA.SlotDataAsset>();
		List<UMA.SlotDataAsset> RemoveList = new List<UMA.SlotDataAsset>();
		List<UMA.OverlayDataAsset> tmpList2 = new List<UMA.OverlayDataAsset>();
		List<UMA.OverlayDataAsset> removeList2 = new List<UMA.OverlayDataAsset>();

		// find the UMA libraries
		UslotLibrary = FindObjectOfType<SlotLibrary>();
		UoverlayLibrary = FindObjectOfType<OverlayLibrary>();

		// convert the libraries array to a list
		tmpList = UslotLibrary.GetAllSlotAssets().ToList();
		tmpList2 = UoverlayLibrary.GetAllOverlayAssets().ToList();

		// search missing fields
		for(int i = 0; i < tmpList.Count; i ++){
			if ( tmpList[i] == null ) RemoveList.Add(tmpList[i]);
		}
		for(int i = 0; i < tmpList2.Count; i ++){
			if ( tmpList2[i] == null ) removeList2.Add(tmpList2[i]);
		}

		// clean missing fields
		foreach ( SlotDataAsset slot in RemoveList ){
			if ( tmpList.Contains (slot) ) tmpList.Remove (slot);
		}
		foreach ( OverlayDataAsset overlay in removeList2 ){
			if ( tmpList2.Contains (overlay) ) tmpList2.Remove (overlay);
		}

		// convert the tmp lists to a libraries array
		//	UslotLibrary.slotElementList = tmpList.ToArray();
		//	UoverlayLibrary.overlayElementList = tmpList2.ToArray();

		List<UMA.SlotDataAsset> existingList = UslotLibrary.GetAllSlotAssets ().ToList();
		foreach ( UMA.SlotDataAsset element in tmpList ){
			if ( existingList.Contains (element) == false )
				UslotLibrary.AddSlotAsset(element);
		}

		List<UMA.OverlayDataAsset> existingList2 = UoverlayLibrary.GetAllOverlayAssets ().ToList();
		foreach ( UMA.OverlayDataAsset element in tmpList2 ){
			if ( existingList2.Contains (element) == false )
				UoverlayLibrary.AddOverlayAsset(element);
		}
	}
	#endif

	public static void CleanEquipmentSets (){
		if (  GameObject.Find ("DK_UMA") != null ){
			// find game settings
			DKUMA_Variables gameSettings = GameObject.Find ("DK_UMA").GetComponent<DKUMA_Variables>();

			// get sets from game settings
			if ( gameSettings != null && gameSettings._EquipmentSets != null ) {
				DKEquipmentSetListData _EquipmentSetsData = gameSettings._EquipmentSets._EquipmentSetsData;

				// tmp list
				List<DKEquipmentSetData> tmpSetsList = new List<DKEquipmentSetData>();

				if ( _EquipmentSetsData && _EquipmentSetsData.Sets.SetsList.Count != 0 ){
					foreach ( DKEquipmentSetData set in _EquipmentSetsData.Sets.SetsList ){
						if ( set == null ) tmpSetsList.Add (set);
					}
					// remove nulls
					foreach ( DKEquipmentSetData set in tmpSetsList ){
						tmpSetsList.Remove (set);
					}
				}
			}
			else Debug.LogError ("DK UMA : No 'EquipmentSetsData' have been assigned to the 'DKUMA_Variables' of the 'DK_UMA' object of your scene. Assign it to be able to use the function.");
		}
	}
}
