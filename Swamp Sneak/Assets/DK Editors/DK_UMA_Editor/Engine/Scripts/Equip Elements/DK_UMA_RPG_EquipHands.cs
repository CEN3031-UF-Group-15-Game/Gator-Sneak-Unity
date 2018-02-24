using UnityEngine;
using System.Collections;

public class DK_UMA_RPG_EquipHands : MonoBehaviour {

	public static DKOverlayData _overlay;
	public static Color _color = Color.black;

	public static void EquipSlotElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction ){
		EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset, color, Layer, LayersAction, null,  null );
	}

	public static void EquipSlotElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction, ColorPresetData Opt1,  ColorPresetData Opt2 ){
		// for a slot element
		if ( _slot != null ){
			#region Equipment

			// for Main Layer
			if ( Layer == "Main" ){
				_DK_RPG_UMA._Equipment._Hands.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Equipment._Hands.Overlay = _overlay;

					// color preset
					if ( _overlay 
					    && _overlay.ColorPresets.Count > 0 ) {
						if (  ColorPreset == null ){
							int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
							color = _overlay.ColorPresets[ran2].PresetColor;
							_DK_RPG_UMA._Equipment._Hands.ColorPreset = _overlay.ColorPresets[ran2];
						}
						else {
							color = ColorPreset.PresetColor;
							_DK_RPG_UMA._Equipment._Hands.ColorPreset = ColorPreset;
						}
					}
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Equipment._Hands.Overlay = _slot.LinkedOverlayList[ran];
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
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._Hands.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._Hands.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._Hands.Opt02Color = Opt2;
			}
			// for cover layer
			else if ( Layer == "Cover" ){
				_DK_RPG_UMA._Equipment._HandsCover.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Equipment._HandsCover.Overlay = _overlay;
					
					// color preset
					if ( _overlay 
					    && _overlay.ColorPresets.Count > 0 ) {
						if (  ColorPreset == null ){
							int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
							color = _overlay.ColorPresets[ran2].PresetColor;
							_DK_RPG_UMA._Equipment._HandsCover.ColorPreset = _overlay.ColorPresets[ran2];
						}
						else {
							color = ColorPreset.PresetColor;
							_DK_RPG_UMA._Equipment._HandsCover.ColorPreset = ColorPreset;
						}
					}
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Equipment._HandsCover.Overlay = _slot.LinkedOverlayList[ran];
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
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._HandsCover.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._HandsCover.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._HandsCover.Opt02Color = Opt2;
			}
			#endregion Equipment
		}
		// for an Overlay element
		else if ( _overlay ) {
			#region Equipment
			if ( Layer == "SubOverlayOnly" ) {
				// color preset
				if ( _overlay 
				    && _overlay.ColorPresets.Count > 0 ) {
					if (  ColorPreset == null ){
						int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
						color = _overlay.ColorPresets[ran2].PresetColor;
						_DK_RPG_UMA._Equipment._HandsSub.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._HandsSub.ColorPreset = ColorPreset;
					}
				}
				_DK_RPG_UMA._Equipment._HandsSub.Slot = null;
				_DK_RPG_UMA._Equipment._HandsSub.Overlay = _overlay;
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._HandsSub.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._HandsSub.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._HandsSub.Opt02Color = Opt2;
			}
			if ( Layer == "Main" ) {
				// color preset
				if ( _overlay 
				    && _overlay.ColorPresets.Count > 0 ) {
					if (  ColorPreset == null ){
						int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
						color = _overlay.ColorPresets[ran2].PresetColor;
						_DK_RPG_UMA._Equipment._Hands.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._Hands.ColorPreset = ColorPreset;
					}
				}
				_DK_RPG_UMA._Equipment._Hands.Slot = null;
				_DK_RPG_UMA._Equipment._Hands.Overlay = _overlay;
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._Hands.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._Hands.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._Hands.Opt02Color = Opt2;
			}
			if ( Layer == "Cover" ) {
				// color preset
				if ( _overlay 
				    && _overlay.ColorPresets.Count > 0 ) {
					if (  ColorPreset == null ){
						int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
						color = _overlay.ColorPresets[ran2].PresetColor;
						_DK_RPG_UMA._Equipment._HandsCover.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._HandsCover.ColorPreset = ColorPreset;
					}
				}
				_DK_RPG_UMA._Equipment._HandsCover.Slot = null;
				_DK_RPG_UMA._Equipment._HandsCover.Overlay = _overlay;
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._HandsCover.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._HandsCover.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._HandsCover.Opt02Color = Opt2;
			}
			#endregion Equipment

		}

		// Other layers action

		// remove all other layers
		if ( LayersAction == "Yes" ){
			if ( Layer == "SubOverlayOnly" ) {
				RemoveMain ( _DK_RPG_UMA );
				RemoveCover ( _DK_RPG_UMA );
			}
			if ( Layer == "Main" ) {
				RemoveSub ( _DK_RPG_UMA );
				RemoveCover ( _DK_RPG_UMA );
			}
			if ( Layer == "Cover" ) {
				RemoveSub ( _DK_RPG_UMA );
				RemoveMain ( _DK_RPG_UMA );
			}
		}
		else if ( LayersAction == "OnlySub" ){
			RemoveSub ( _DK_RPG_UMA );
		}
		else if ( LayersAction == "OnlyMain" ){
			RemoveMain ( _DK_RPG_UMA );
		}
		else if ( LayersAction == "OnlyCover" ){
			RemoveCover ( _DK_RPG_UMA );
		}

		DK_RPG_ReBuild _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.GetComponent<DK_RPG_ReBuild>();
		if ( _DK_RPG_ReBuild == null ) _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.AddComponent<DK_RPG_ReBuild>();
		DKUMAData _DKUMAData = _DK_RPG_UMA.gameObject.GetComponent<DKUMAData>();
		_DK_RPG_ReBuild.RefreshOnly = true;
		_DK_RPG_ReBuild.Launch (_DKUMAData);
	}

	public static void RemoveSub ( DK_RPG_UMA _DK_RPG_UMA ){
		_DK_RPG_UMA._Equipment._HandsSub.Slot = null;
		_DK_RPG_UMA._Equipment._HandsSub.Overlay = null;
		_DK_RPG_UMA._Equipment._HandsSub.ColorPreset = null;
	}

	public static void RemoveMain ( DK_RPG_UMA _DK_RPG_UMA ){
		_DK_RPG_UMA._Equipment._Hands.Slot = null;
		_DK_RPG_UMA._Equipment._Hands.Overlay = null;
		_DK_RPG_UMA._Equipment._Hands.ColorPreset = null;
	}

	public static void RemoveCover ( DK_RPG_UMA _DK_RPG_UMA ){
		_DK_RPG_UMA._Equipment._HandsCover.Slot = null;
		_DK_RPG_UMA._Equipment._HandsCover.Overlay = null;
		_DK_RPG_UMA._Equipment._HandsCover.ColorPreset = null;
	}
}
