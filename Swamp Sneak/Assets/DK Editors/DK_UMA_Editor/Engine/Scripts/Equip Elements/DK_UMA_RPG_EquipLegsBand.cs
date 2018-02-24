using UnityEngine;
using System.Collections;

public class DK_UMA_RPG_EquipLegsBand : MonoBehaviour {

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
				_DK_RPG_UMA._Equipment._LegBand.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Equipment._LegBand.Overlay = _overlay;

					// color preset
					if ( _overlay 
					    && _overlay.ColorPresets.Count > 0 ) {
						if (  ColorPreset == null ){
							int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
							color = _overlay.ColorPresets[ran2].PresetColor;
							_DK_RPG_UMA._Equipment._LegBand.ColorPreset = _overlay.ColorPresets[ran2];
						}
						else {
							color = ColorPreset.PresetColor;
							_DK_RPG_UMA._Equipment._LegBand.ColorPreset = ColorPreset;
						}
					}
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Equipment._LegBand.Overlay = _slot.LinkedOverlayList[ran];
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
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._LegBand.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._LegBand.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._LegBand.Opt02Color = Opt2;
			}
			// for cover layer
			else if ( Layer == "Cover" ){
				_DK_RPG_UMA._Equipment._LegBandCover.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Equipment._LegBandCover.Overlay = _overlay;
					
					// color preset
					if ( _overlay 
					    && _overlay.ColorPresets.Count > 0 ) {
						if (  ColorPreset == null ){
							int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
							color = _overlay.ColorPresets[ran2].PresetColor;
							_DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = _overlay.ColorPresets[ran2];
						}
						else {
							color = ColorPreset.PresetColor;
							_DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = ColorPreset;
						}
					}
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Equipment._LegBandCover.Overlay = _slot.LinkedOverlayList[ran];
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
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._LegBandCover.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._LegBandCover.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._LegBandCover.Opt02Color = Opt2;
			}
			// for Sub layer
			else if ( Layer == "Sub" ){
				_DK_RPG_UMA._Equipment._LegBandSub.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Equipment._LegBandSub.Overlay = _overlay;

					// color preset
					if ( _overlay 
						&& _overlay.ColorPresets.Count > 0 ) {
						if (  ColorPreset == null ){
							int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
							color = _overlay.ColorPresets[ran2].PresetColor;
							_DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = _overlay.ColorPresets[ran2];
						}
						else {
							color = ColorPreset.PresetColor;
							_DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = ColorPreset;
						}
					}
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Equipment._LegBandSub.Overlay = _slot.LinkedOverlayList[ran];
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
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._LegBandSub.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._LegBandSub.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._LegBandSub.Opt02Color = Opt2;
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
						_DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = ColorPreset;
					}
				}
				_DK_RPG_UMA._Equipment._LegBandSub.Slot = null;
				_DK_RPG_UMA._Equipment._LegBandSub.Overlay = _overlay;
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._LegBandSub.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._LegBandSub.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._LegBandSub.Opt02Color = Opt2;
			}
			if ( Layer == "Main" ) {
				// color preset
				if ( _overlay 
				    && _overlay.ColorPresets.Count > 0 ) {
					if (  ColorPreset == null ){
						int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
						color = _overlay.ColorPresets[ran2].PresetColor;
						_DK_RPG_UMA._Equipment._LegBand.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._LegBand.ColorPreset = ColorPreset;
					}
				}
				_DK_RPG_UMA._Equipment._LegBand.Slot = null;
				_DK_RPG_UMA._Equipment._LegBand.Overlay = _overlay;
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._LegBand.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._LegBand.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._LegBand.Opt02Color = Opt2;
			}
			if ( Layer == "Cover" ) {
				// color preset
				if ( _overlay 
				    && _overlay.ColorPresets.Count > 0 ) {
					if (  ColorPreset == null ){
						int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
						color = _overlay.ColorPresets[ran2].PresetColor;
						_DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = ColorPreset;
					}
				}
				_DK_RPG_UMA._Equipment._LegBandCover.Slot = null;
				_DK_RPG_UMA._Equipment._LegBandCover.Overlay = _overlay;
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._LegBandCover.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._LegBandCover.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._LegBandCover.Opt02Color = Opt2;
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
		_DK_RPG_UMA._Equipment._LegBandSub.Slot = null;
		_DK_RPG_UMA._Equipment._LegBandSub.Overlay = null;
		_DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = null;
	}

	public static void RemoveMain ( DK_RPG_UMA _DK_RPG_UMA ){
		_DK_RPG_UMA._Equipment._LegBand.Slot = null;
		_DK_RPG_UMA._Equipment._LegBand.Overlay = null;
		_DK_RPG_UMA._Equipment._LegBand.ColorPreset = null;
	}

	public static void RemoveCover ( DK_RPG_UMA _DK_RPG_UMA ){
		_DK_RPG_UMA._Equipment._LegBandCover.Slot = null;
		_DK_RPG_UMA._Equipment._LegBandCover.Overlay = null;
		_DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = null;
	}
}
