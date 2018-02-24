using UnityEngine;
using System;
using System.Collections;
using UnityEditor;

public class AutoOverlayType : MonoBehaviour {


	public static void AutoAssignOverlayType ( DK_SlotsAnatomyElement Place, DKSlotData DKSlot, DKOverlayData DKOverlay ){
		if ( DKSlot == null && DKOverlay == null ){
			DKSlot =  Selection.activeObject as DKSlotData;
			DKOverlay =  Selection.activeObject as DKOverlayData;
		}
		#region for DK Slot
		if ( DKSlot != null ){
			try {
			DKSlot.OverlayType = Place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.OverlayType;
			DKSlot.Elem = Place.dk_SlotsAnatomyElement.DefaultSlotOverlayType.Elem;
			}
			catch (NullReferenceException){
				
			}
		}
		#endregion for DK Slot


		if ( DKSlot != null ) {
			EditorUtility.SetDirty(DKSlot);
			AssetDatabase.SaveAssets();
		}
		else if ( DKOverlay != null ) {
			EditorUtility.SetDirty(DKOverlay);
			AssetDatabase.SaveAssets();
		}
	}
}
