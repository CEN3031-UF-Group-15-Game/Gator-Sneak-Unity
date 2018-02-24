using UnityEngine;
using System.Collections;

public class DK_UMA_RPG_EquipRingLeft : MonoBehaviour {

	public static DKOverlayData _overlay;
	public static Color _color = Color.black;

	public static void EquipSlotElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction ){
		EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset, color, Layer, LayersAction, null,  null );
	}

	public static void EquipSlotElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction, ColorPresetData Opt1,  ColorPresetData Opt2 ){
		// for a slot element
		if ( _slot != null ){

			#region Equipment

			_DK_RPG_UMA._Equipment._RingLeft.Slot = _slot;
			// if the overlay is already assigned
			if ( _overlay ) {
				_DK_RPG_UMA._Equipment._RingLeft.Overlay = _overlay;

				// color preset
				if ( _overlay 
				    && _overlay.ColorPresets.Count > 0 ) {
					if (  ColorPreset == null ){
						int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
						color = _overlay.ColorPresets[ran2].PresetColor;
						_DK_RPG_UMA._Equipment._RingLeft.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._RingLeft.ColorPreset = ColorPreset;
					}
				}
			}
			// if the overlay is not assigned, random one from the linked overlays of the slot
			else {
				if ( _slot.LinkedOverlayList.Count > 0 ) {
					int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
					_DK_RPG_UMA._Equipment._RingLeft.Overlay = _slot.LinkedOverlayList[ran];
					if ( _slot.LinkedOverlayList[ran] 
					    && _slot.LinkedOverlayList[ran].ColorPresets.Count > 0 ) {
						if (  ColorPreset == null ){
							int ran2 = Random.Range(0, _slot.LinkedOverlayList[ran].ColorPresets.Count-1);
							color = _slot.LinkedOverlayList[ran].ColorPresets[ran2].PresetColor;
						}
						else color = ColorPreset.PresetColor;
					}
				}
			}
			if ( color != Color.black ) _DK_RPG_UMA._Equipment._RingLeft.Color = color;
			// opt 1 & 2 colors
			_DK_RPG_UMA._Equipment._RingLeft.Opt01Color = Opt1;
			_DK_RPG_UMA._Equipment._RingLeft.Opt02Color = Opt2;
			#endregion Equipment

			// Other layers action
						
			DK_RPG_ReBuild _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.GetComponent<DK_RPG_ReBuild>();
			if ( _DK_RPG_ReBuild == null ) _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.AddComponent<DK_RPG_ReBuild>();
			DKUMAData _DKUMAData = _DK_RPG_UMA.gameObject.GetComponent<DKUMAData>();
			_DK_RPG_ReBuild.RefreshOnly = true;
			_DK_RPG_ReBuild.Launch (_DKUMAData);
		}
	}
}
