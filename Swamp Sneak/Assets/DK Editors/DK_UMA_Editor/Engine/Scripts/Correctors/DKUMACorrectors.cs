using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DKUMACorrectors : MonoBehaviour {
	public static DKUMA_Variables _DKUMA_Variables;

	#region Link Elements
	public static void LinkToUMAslot ( DKSlotData slot, DKUMA_Variables _DKUMA_Variables ){
		if ( slot._UMA == null ) {
			foreach ( UMA.SlotDataAsset data in _DKUMA_Variables.UMASlotsList ){
				if ( data.slotName == slot.slotName ) slot._UMA = data;
			}
		} 
		if ( slot._UMA == null ) LinkSlotByBackupName ( slot, _DKUMA_Variables );
		else {
			#if UNITY_EDITOR
			EditorUtility.SetDirty (slot);
			AssetDatabase.SaveAssets ();
			#endif
		}
	}

	public static void LinkToUMAoverlay ( DKOverlayData overlay, DKUMA_Variables _DKUMA_Variables ){
		if ( overlay._UMA == null ) {
			foreach ( UMA.OverlayDataAsset data in _DKUMA_Variables.UMAOverlaysList ){
				if ( data.overlayName == overlay.overlayName ) overlay._UMA = data;
			}
		} 
		if ( overlay._UMA == null ) LinkOverlayByBackupName ( overlay, _DKUMA_Variables );
		else {
			#if UNITY_EDITOR
			EditorUtility.SetDirty (overlay);
			AssetDatabase.SaveAssets ();
			#endif
		}
	}

	public static void LinkSlotByBackupName ( DKSlotData slot, DKUMA_Variables _DKUMA_Variables ){
		if ( slot._UMA == null ) {
			foreach ( UMA.SlotDataAsset data in _DKUMA_Variables.UMASlotsList ){
				if ( data.name == slot._UMAslotName ) {
					slot._UMA = data;
				}
			}
			if ( slot._UMA == null ) Debug.LogError ( slot.name+" has no link to the original UMA SlotData. " +
				"The engine tried to fix it using 2 methods but it seams that this linked UMA element is not present into your project. " +
				"To fix it manually, reimport the UMA element, then select "
				+slot.name+" and assign the UMA SlotData to the corresponding field called 'UMA'." );
			else {
				#if UNITY_EDITOR
				EditorUtility.SetDirty (slot);
				AssetDatabase.SaveAssets ();
				#endif
			}
		} 
	}

	public static void LinkOverlayByBackupName ( DKOverlayData overlay, DKUMA_Variables _DKUMA_Variables ){
		if ( overlay._UMA == null ) {
			foreach ( UMA.OverlayDataAsset data in _DKUMA_Variables.UMAOverlaysList ){
				if ( data.name == overlay._UMAoverlayName ) {
					overlay._UMA = data;
				}
			}
		} 

		if ( overlay._UMA == null ) Debug.LogError ( overlay.name+" has no link to the original UMA OverlayData. " +
			"The engined tried to fix it using 2 methods but it seams that this linked UMA element is not present into your project. " +
			"To fix it manually, reimport the UMA element, then select "
			+overlay.name+" and assign the UMA OverlayData to the corresponding field called 'UMA'." );
		else {
			#if UNITY_EDITOR
			EditorUtility.SetDirty (overlay);
			AssetDatabase.SaveAssets ();
			#endif
		}
	}
	#endregion Link Elements

	#region Elements Places

	public static void VerifyDKSlotPlace ( DKSlotData slot, DKUMA_Variables _DKUMA_Variables ){

		if ( _DKUMA_Variables == null )
			_DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();

		if ( slot.Place == null && slot.PlaceName != "" ){
			if ( _DKUMA_Variables._DK_UMA_GameSettings != null
				&& _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Count == 0 ) {
				#if UNITY_EDITOR
				DetectPlaces ();
				#endif
			}
			if ( _DKUMA_Variables._DK_UMA_GameSettings != null
				&& _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Count != 0 ){
				foreach ( DK_SlotsAnatomyElement place in _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList ) {
					if ( place.gameObject.name == slot.PlaceName ) {
						slot.Place = place;
						slot.PlaceName = place.gameObject.name;
						#if UNITY_EDITOR
						EditorUtility.SetDirty (slot);
						#endif
					}
				}
			}
		}
		if ( slot.Place != null && slot.PlaceName == "" ) {
			slot.PlaceName = slot.Place.name;
			#if UNITY_EDITOR
			EditorUtility.SetDirty (slot);
			#endif
		}
		if ( slot.Place == null && slot.PlaceName != "" ) Debug.LogError ("DK UMA Corrector : the Element '"+slot.name+"' does not have a Place assigned ! " +
			"You have to Select a Place for it using the 'Prepare' tab of the DK UMA Editor.");
	}

	public static void VerifyDKOverlayPlace ( DKOverlayData Overlay, DKUMA_Variables _DKUMA_Variables ){

		if ( _DKUMA_Variables == null )
			_DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();

		if ( Overlay.Place == null && Overlay.PlaceName != "" ){
			if ( _DKUMA_Variables._DK_UMA_GameSettings != null
				&& _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Count == 0 ) {
				#if UNITY_EDITOR
				DetectPlaces ();
				#endif
			}
			if ( _DKUMA_Variables._DK_UMA_GameSettings != null
				&& _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Count != 0 ){
				foreach ( DK_SlotsAnatomyElement place in _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList ) {
					if ( place.gameObject.name == Overlay.PlaceName ) {
						Overlay.Place = place;
						Overlay.PlaceName = place.gameObject.name;
						#if UNITY_EDITOR
						EditorUtility.SetDirty (Overlay);
						#endif
					}
				}
			}
		}
		if ( Overlay.Place != null && Overlay.PlaceName == "" ) {
			Overlay.PlaceName = Overlay.Place.name;
			#if UNITY_EDITOR
			EditorUtility.SetDirty (Overlay);
			#endif
		}
		if ( Overlay.Place == null && Overlay.PlaceName != "" ) Debug.LogError ("DK UMA Corrector : the Element '"+Overlay.name+"' does not have a Place assigned ! " +
			"You have to Select a Place for it using the 'Prepare' tab of the DK UMA Editor.");
	}


	#if UNITY_EDITOR
	public static void DetectPlaces (){		
		DKUMA_Variables _DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();

		// clear list
		_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Clear();

		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:GameObject", lookFor);
		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			GameObject element = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
			if ( element.GetComponent<DK_SlotsAnatomyElement>() != null ){
				if ( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Contains ( element.GetComponent<DK_SlotsAnatomyElement>() ) == false )
					_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Add ( element.GetComponent<DK_SlotsAnatomyElement>() );
			}
		}
		EditorUtility.SetDirty (_DKUMA_Variables._DK_UMA_GameSettings);
		AssetDatabase.SaveAssets ();
	}
	#endif
	#endregion Elements Places

	#region Correct Libraries
	public static void VerifyLibraries ( string _Type, DK_RPG_UMA _DK_RPG_UMA, DKSlotData slot, DKOverlayData overlay, DKSlotLibrary slotLib, DKOverlayLibrary ovLib, UMA.SlotLibrary uslotLib, UMA.OverlayLibrary uovLib ){
		if ( _DKUMA_Variables == null ) _DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();

		// verify libraries
		if ( slot != null ){
			if ( slot.PhysicsUMASlot != null && uslotLib.HasSlot (slot.PhysicsUMASlot.slotName) == false ) 
			if ( uslotLib.HasSlot (slot.PhysicsUMASlot.slotName) == false ){
				Debug.Log ("DK UMA Corrector : Adding Physics '"+slot.PhysicsUMASlot.slotName+"' to the UMA Slots Library.");
				uslotLib.AddSlotAsset (slot.PhysicsUMASlot);
			}	
			if ( slotLib.slotElementList.ToList().Contains (slot) == false )
			slotLib.AddSlot (slot.slotName, slot);
			if ( slot._UMA == null ) LinkToUMAslot ( slot, _DKUMA_Variables );
			if ( uslotLib == null ) uslotLib = FindObjectOfType<UMA.SlotLibrary>();
			if ( uslotLib.HasSlot (slot._UMA.slotName) == false ){
				Debug.Log ("DK UMA Corrector : Adding '"+slot._UMA.slotName+"' to the UMA Slots Library.");
				uslotLib.AddSlotAsset (slot._UMA);
			}
		}
		if ( overlay  != null ){
			if ( ovLib.overlayElementList.ToList().Contains (overlay) == false )
				ovLib.AddOverlay (overlay.overlayName, overlay);
			if ( uovLib == null )  uovLib = FindObjectOfType<UMA.OverlayLibrary>();
			if ( uovLib.HasOverlay (overlay._UMA.overlayName) == false ){
				Debug.Log ("DK UMA Corrector : Adding '"+overlay._UMA.overlayName+"' to the UMA Overlays Library.");
				uovLib.AddOverlayAsset (overlay._UMA);
			}
		}
	}
	#endregion Correct Libraries

	#region Install engine to the scene
	public static void InstallDKUMA (){
		GameObject DK_UMA =  GameObject.Find("DK_UMA");
		GameObject ZeroPoint = GameObject.Find("ZeroPoint");
		if ( ZeroPoint == null )
			//	ZeroPoint = (GameObject)PrefabUtility.InstantiatePrefab (Resources.Load ("ZeroPoint"));
			ZeroPoint = (GameObject)Instantiate (Resources.Load ("ZeroPoint"));
		ZeroPoint.name = "ZeroPoint";
		if ( DK_UMA == null ) {
			//	DK_UMA = (GameObject)PrefabUtility.InstantiatePrefab (Resources.Load ("DK_UMA"));
			DK_UMA = (GameObject)Instantiate (Resources.Load ("DK_UMA"));
			#if UNITY_EDITOR
			PrefabUtility.DisconnectPrefabInstance (DK_UMA);
			#endif
			DK_UMA.name = "DK_UMA";

			Debug.Log ("DK UMA : DK_UMA object installed to the scene.");
		}
	}

	public static void InstallUMA (){
		GameObject _UMA =  GameObject.Find("UMA");
		if ( _UMA == null ) {
			//	_UMA = (GameObject)PrefabUtility.InstantiatePrefab (Resources.Load ("UMA205"));
			_UMA = (GameObject)Instantiate (Resources.Load ("UMA205"));
			#if UNITY_EDITOR
			PrefabUtility.DisconnectPrefabInstance (_UMA);
			#endif
			_UMA.name = "UMA";

			DKUMACleanLibraries.CleanLibraries ();
			if ( _DKUMA_Variables == null ) _DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();
			_DKUMA_Variables.PopulateLibraries ();
			Debug.Log ("DK UMA : UMA object installed to the scene.");
		}
	}
	#endregion Install engine to the scene
}
