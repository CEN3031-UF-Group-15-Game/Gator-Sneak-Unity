using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using UMA;

public class DK_AssignHeadWearStackedOverlays : MonoBehaviour {

	public static void VerifyUMAMaterial ( UMA.SlotDataAsset slot, UMA.OverlayDataAsset overlay ){
		if ( slot.material == null ) Debug.LogError ( "UMA Slot '"+slot.name+"' doesn't have a UMAMaterial assigned. Please fixe that issue by selecting the slot using the Element Manager and assign a UMAMaterial. " +
			"For a PBR element, the default material is 'UMA_Diffuse_Normal_Metallic'.");
		else if ( overlay.material == null ){
			overlay.material = slot.material;
			Debug.Log ( "UMA overlay '"+overlay.name+"' doesn't have a UMAMaterial assigned. Auto assigning the UMAMaterial from '"+slot.name+"' slot ("+slot.material.name+")." );
		}
	}

	public static void AssignStackedOverlays ( DK_UMACrowd Crowd, List<DKSlotData> TmpSlotDataList, List<UMA.SlotData> TmpUMASlotDataList, DK_RPG_UMA _DK_RPG_UMA, DKOverlayData Overlay, string Opt, string type, int index ){

		DKOverlayData stacked = ScriptableObject.CreateInstance<DKOverlayData>();
		Color ColorToApply = new Color ();


		// Head Wear
		if ( type.Contains("HeadSub") == true ) {
			VerifyUMAMaterial ( _DK_RPG_UMA._Equipment._HeadSub.Slot._UMA, Overlay._UMA );

			// set color
			if ( Opt == "Opt01" ){
				if ( Overlay.Opt01.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._HeadSub.Opt01Color == null ) 
					_DK_RPG_UMA._Equipment._HeadSub.Opt01Color 
					= Overlay.Opt01.ColorPresets [UnityEngine.Random.Range(0,Overlay.Opt01.ColorPresets.Count-1)];

				stacked = Overlay.Opt01;
				ColorToApply = _DK_RPG_UMA._Equipment._HeadSub.Opt01Color.PresetColor;
			}
			if ( Opt == "Opt02" ){
				if ( Overlay.Opt02.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._HeadSub.Opt02Color == null ) 
					_DK_RPG_UMA._Equipment._HeadSub.Opt02Color 
					= Overlay.Opt02.ColorPresets [UnityEngine.Random.Range(0,Overlay.Opt02.ColorPresets.Count-1)];

				stacked = Overlay.Opt02;
				ColorToApply = _DK_RPG_UMA._Equipment._HeadSub.Opt02Color.PresetColor;
			}
			// Dirt
			if ( Opt == "Dirt01" ){
				if ( Overlay.Dirt01.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._HeadSub.Dirt01Color == null ) 
					_DK_RPG_UMA._Equipment._HeadSub.Dirt01Color 
					= Overlay.Dirt01.ColorPresets [UnityEngine.Random.Range(0,Overlay.Dirt01.ColorPresets.Count-1)];

				stacked = Overlay.Dirt01;
				ColorToApply = _DK_RPG_UMA._Equipment._HeadSub.Dirt01Color.PresetColor;
			}
			if ( Opt == "Dirt02" ){
				if ( Overlay.Dirt02.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._HeadSub.Dirt02Color == null ) 
					_DK_RPG_UMA._Equipment._HeadSub.Dirt02Color 
					= Overlay.Dirt02.ColorPresets [UnityEngine.Random.Range(0,Overlay.Dirt02.ColorPresets.Count-1)];

				stacked = Overlay.Dirt02;
				ColorToApply = _DK_RPG_UMA._Equipment._HeadSub.Dirt02Color.PresetColor;
			}
		}
		else if ( type.Contains("HeadWear") == true ) {
			VerifyUMAMaterial ( _DK_RPG_UMA._Equipment._Head.Slot._UMA, Overlay._UMA );
			// set color
			if ( Opt == "Opt01" ){
				if ( Overlay.Opt01.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._Head.Opt01Color == null ) 
					_DK_RPG_UMA._Equipment._Head.Opt01Color 
					= Overlay.Opt01.ColorPresets [UnityEngine.Random.Range(0,Overlay.Opt01.ColorPresets.Count-1)];

				stacked = Overlay.Opt01;
				ColorToApply = _DK_RPG_UMA._Equipment._Head.Opt01Color.PresetColor;
			}
			if ( Opt == "Opt02" ){
				if ( Overlay.Opt02.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._Head.Opt02Color == null ) 
					_DK_RPG_UMA._Equipment._Head.Opt02Color 
					= Overlay.Opt02.ColorPresets [UnityEngine.Random.Range(0,Overlay.Opt02.ColorPresets.Count-1)];

				stacked = Overlay.Opt02;
				ColorToApply = _DK_RPG_UMA._Equipment._Head.Opt02Color.PresetColor;
			}
			// Dirt
			if ( Opt == "Dirt01" ){
				if ( Overlay.Dirt01.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._Head.Dirt01Color == null ) 
					_DK_RPG_UMA._Equipment._Head.Dirt01Color 
					= Overlay.Dirt01.ColorPresets [UnityEngine.Random.Range(0,Overlay.Dirt01.ColorPresets.Count-1)];

				stacked = Overlay.Dirt01;
				ColorToApply = _DK_RPG_UMA._Equipment._Head.Dirt01Color.PresetColor;
			}
			if ( Opt == "Dirt02" ){
				if ( Overlay.Dirt02.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._Head.Dirt02Color == null ) 
					_DK_RPG_UMA._Equipment._Head.Dirt02Color 
					= Overlay.Dirt02.ColorPresets [UnityEngine.Random.Range(0,Overlay.Dirt02.ColorPresets.Count-1)];

				stacked = Overlay.Dirt02;
				ColorToApply = _DK_RPG_UMA._Equipment._Head.Dirt02Color.PresetColor;
			}
		}
		else if ( type.Contains("HeadCover") == true ) {
			VerifyUMAMaterial ( _DK_RPG_UMA._Equipment._HeadCover.Slot._UMA, Overlay._UMA );
			// set color
			if ( Opt == "Opt01" ){
				if ( Overlay.Opt01.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._HeadCover.Opt01Color == null ) 
					_DK_RPG_UMA._Equipment._HeadCover.Opt01Color 
					= Overlay.Opt01.ColorPresets [UnityEngine.Random.Range(0,Overlay.Opt01.ColorPresets.Count-1)];

				stacked = Overlay.Opt01;
				ColorToApply = _DK_RPG_UMA._Equipment._HeadCover.Opt01Color.PresetColor;
			}
			if ( Opt == "Opt02" ){
				if ( Overlay.Opt02.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._HeadCover.Opt02Color == null ) 
					_DK_RPG_UMA._Equipment._HeadCover.Opt02Color 
					= Overlay.Opt02.ColorPresets [UnityEngine.Random.Range(0,Overlay.Opt02.ColorPresets.Count-1)];

				stacked = Overlay.Opt02;
				ColorToApply = _DK_RPG_UMA._Equipment._HeadCover.Opt02Color.PresetColor;
			}
			// Dirt
			if ( Opt == "Dirt01" ){
				if ( Overlay.Dirt01.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._HeadCover.Dirt01Color == null ) 
					_DK_RPG_UMA._Equipment._HeadCover.Dirt01Color 
					= Overlay.Dirt01.ColorPresets [UnityEngine.Random.Range(0,Overlay.Dirt01.ColorPresets.Count-1)];

				stacked = Overlay.Dirt01;
				ColorToApply = _DK_RPG_UMA._Equipment._HeadCover.Dirt01Color.PresetColor;
			}
			if ( Opt == "Dirt02" ){
				if ( Overlay.Dirt02.ColorPresets.Count > 0 && _DK_RPG_UMA._Equipment._HeadCover.Dirt02Color == null ) 
					_DK_RPG_UMA._Equipment._HeadCover.Dirt02Color 
					= Overlay.Dirt02.ColorPresets [UnityEngine.Random.Range(0,Overlay.Dirt02.ColorPresets.Count-1)];

				stacked = Overlay.Dirt02;
				ColorToApply = _DK_RPG_UMA._Equipment._HeadCover.Dirt02Color.PresetColor;
			}
		}

		// assign stacked overlay
		_DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();
		DK_UMA_GameSettings _DK_UMA_GameSettings = _DKUMA_Variables._DK_UMA_GameSettings;
		TmpSlotDataList[index].overlayList.Add( Crowd.overlayLibrary.InstantiateOverlay(stacked.overlayName, ColorToApply ));
		if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
			// directly to UMA Recipe
			TmpUMASlotDataList[index].AddOverlay(GetOverlayLibrary().InstantiateOverlay(stacked._UMA.overlayName,ColorToApply));
		
		TmpSlotDataList[index].overlayList[TmpSlotDataList[index].overlayList.Count-1].OverlayType = stacked.OverlayType;
		TmpSlotDataList[index].overlayList[TmpSlotDataList[index].overlayList.Count-1].ColorPresets = stacked.ColorPresets;
	}

	public static DKUMA_Variables _DKUMA_Variables;
	public static DK_UMA_GameSettings _DK_UMA_GameSettings;
	public static  UMAContext umaContext;
	public static  RaceLibraryBase GetRaceLibrary()
	{
		return umaContext.raceLibrary;
	}

	public static  SlotLibraryBase GetSlotLibrary()
	{
		return umaContext.slotLibrary;
	}

	public static  OverlayLibraryBase GetOverlayLibrary()
	{
		return umaContext.overlayLibrary;
	}
}
