using UnityEngine;
using System.Collections;

public class DK_UMA_RPG_EquipArmBand : MonoBehaviour {

	public static DKOverlayData _overlay;
	public static Color _color = Color.black;

	public static void EquipSlotElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction ){
		EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset, color, Layer, LayersAction, null,  null );
	}

	public static void EquipSlotElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction, ColorPresetData Opt1,  ColorPresetData Opt2 ){
		// for a slot element
		if ( _slot != null ){
			#region Equipment
			// for Sub Layer
			if ( Layer == "Sub" ){
				_DK_RPG_UMA._Equipment._ArmBandSub.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Equipment._ArmBandSub.Overlay = _overlay;

					// color preset
					if ( _overlay 
						&& _overlay.ColorPresets.Count > 0 ) {
						if (  ColorPreset == null ){
							int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
							color = _overlay.ColorPresets[ran2].PresetColor;
							_DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = _overlay.ColorPresets[ran2];
						}
						else {
							color = ColorPreset.PresetColor;
							_DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = ColorPreset;
						}
					}
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Equipment._ArmBandSub.Overlay = _slot.LinkedOverlayList[ran];
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
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._ArmBandSub.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._ArmBandSub.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._ArmBandSub.Opt02Color = Opt2;
			}
			// for Main Layer
			if ( Layer == "Main" ){
				_DK_RPG_UMA._Equipment._ArmBand.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Equipment._ArmBand.Overlay = _overlay;

					// color preset
					if ( _overlay 
					    && _overlay.ColorPresets.Count > 0 ) {
						if (  ColorPreset == null ){
							int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
							color = _overlay.ColorPresets[ran2].PresetColor;
							_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = _overlay.ColorPresets[ran2];
						}
						else {
							color = ColorPreset.PresetColor;
							_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = ColorPreset;
						}
					}
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Equipment._ArmBand.Overlay = _slot.LinkedOverlayList[ran];
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
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._ArmBand.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._ArmBand.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._ArmBand.Opt02Color = Opt2;
			}
			// for cover layer
			else if ( Layer == "Cover" ){
				_DK_RPG_UMA._Equipment._ArmBandCover.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Equipment._ArmBandCover.Overlay = _overlay;
					
					// color preset
					if ( _overlay 
					    && _overlay.ColorPresets.Count > 0 ) {
						if (  ColorPreset == null ){
							int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
							color = _overlay.ColorPresets[ran2].PresetColor;
							_DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = _overlay.ColorPresets[ran2];
						}
						else {
							color = ColorPreset.PresetColor;
							_DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = ColorPreset;
						}
					}
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Equipment._ArmBandCover.Overlay = _slot.LinkedOverlayList[ran];
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
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._ArmBandCover.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._ArmBandCover.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._ArmBandCover.Opt02Color = Opt2;
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
						_DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = ColorPreset;
					}
				}
				_DK_RPG_UMA._Equipment._ArmBandSub.Slot = null;
				_DK_RPG_UMA._Equipment._ArmBandSub.Overlay = _overlay;
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._ArmBandSub.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._ArmBandSub.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._ArmBandSub.Opt02Color = Opt2;
			}
			if ( Layer == "Main" ) {
				// color preset
				if ( _overlay 
				    && _overlay.ColorPresets.Count > 0 ) {
					if (  ColorPreset == null ){
						int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
						color = _overlay.ColorPresets[ran2].PresetColor;
						_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = ColorPreset;
					}
				}
				_DK_RPG_UMA._Equipment._ArmBand.Slot = null;
				_DK_RPG_UMA._Equipment._ArmBand.Overlay = _overlay;
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._ArmBand.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._ArmBand.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._ArmBand.Opt02Color = Opt2;
			}
			if ( Layer == "Cover" ) {
				// color preset
				if ( _overlay 
				    && _overlay.ColorPresets.Count > 0 ) {
					if (  ColorPreset == null ){
						int ran2 = Random.Range(0, _overlay.ColorPresets.Count-1);
						color = _overlay.ColorPresets[ran2].PresetColor;
						_DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = _overlay.ColorPresets[ran2];
					}
					else {
						color = ColorPreset.PresetColor;
						_DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = ColorPreset;
					}
				}
				_DK_RPG_UMA._Equipment._ArmBandCover.Slot = null;
				_DK_RPG_UMA._Equipment._ArmBandCover.Overlay = _overlay;
				if ( color != Color.black ) _DK_RPG_UMA._Equipment._ArmBandCover.Color = color;
				// opt 1 & 2 colors
				_DK_RPG_UMA._Equipment._ArmBandCover.Opt01Color = Opt1;
				_DK_RPG_UMA._Equipment._ArmBandCover.Opt02Color = Opt2;
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
		_DK_RPG_UMA._Equipment._ArmBandSub.Slot = null;
		_DK_RPG_UMA._Equipment._ArmBandSub.Overlay = null;
		_DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = null;
	}

	public static void RemoveMain ( DK_RPG_UMA _DK_RPG_UMA ){
		_DK_RPG_UMA._Equipment._ArmBand.Slot = null;
		_DK_RPG_UMA._Equipment._ArmBand.Overlay = null;
		_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = null;
	}

	public static void RemoveCover ( DK_RPG_UMA _DK_RPG_UMA ){
		_DK_RPG_UMA._Equipment._ArmBandCover.Slot = null;
		_DK_RPG_UMA._Equipment._ArmBandCover.Overlay = null;
		_DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = null;
	}
}
