using UnityEngine;
using System.Collections;

public class DK_UMA_RPG_Equip : MonoBehaviour {

	public static DKOverlayData _overlay;
	public static Color _color = Color.black;
	public static DK_UMA_GameSettings _DK_UMA_GameSettings;

	public static void PrepareEquipSlotElement (DKSlotData _slot, DKOverlayData overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction) {
		PrepareEquipSlotElement (_slot, overlay, _DK_RPG_UMA, ColorPreset, color, Layer, LayersAction, null,  null);
			
	}

	public static void PrepareEquipSlotElement (DKSlotData _slot, DKOverlayData overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction, ColorPresetData Opt1,  ColorPresetData Opt2) {
		

		if ( _slot && Layer != "SubOverlayOnly" ){
			// choose overlay
			if ( overlay == null && _slot.LinkedOverlayList.Count > 0 ){
				int ran = Random.Range(0, _slot.LinkedOverlayList.Count);
				_overlay = _slot.LinkedOverlayList[ran];
			}
			else _overlay = overlay;
		}
		else if ( overlay ) {
			_slot = null;
			_overlay = overlay;
			// choose color

			// if color preset is present
			if ( ColorPreset != null ){

			}
			// or choose a color preset
			else if ( _overlay 
			         && _overlay.ColorPresets.Count > 0 ) {
				if ( ColorPreset == null ){
					int ran = Random.Range(0, _overlay.ColorPresets.Count-1);
					_color = _overlay.ColorPresets[ran].PresetColor;
					ColorPreset = _overlay.ColorPresets[ran];
				}
				else _color = ColorPreset.PresetColor;
			}
			// if no color preset, randomise color
			else if ( color == new Color (0,0,0) ) {
				float ran1 = Random.Range(0f, 0.99f);
				float ran2 = Random.Range(0f, 0.99f);
				float ran3 = Random.Range(0f, 0.99f);
				_color = new Color (ran1, ran2, ran3 );
			}
		}
		if ( _DK_RPG_UMA != null )
			EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
	}

	public static void EquipSlotElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction ){
		EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset, color, Layer, LayersAction, null, null );
	}

	public static void EquipSlotElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, ColorPresetData ColorPreset, Color color, string Layer, string LayersAction, ColorPresetData Opt1,  ColorPresetData Opt2 ){

		if ( _slot != null ){
			if ( _DK_UMA_GameSettings == null ) _DK_UMA_GameSettings = FindObjectOfType<DKUMA_Variables>()._DK_UMA_GameSettings;
			if ( _DK_UMA_GameSettings.Debugger ) Debug.Log ("DK UMA : Equiping DK UMA Slot : "+_slot.name);

			if ( _overlay != null ){/* Debug.Log ("DK UMA : Equiping slot "+_slot.name+"."); */}
			else Debug.LogError ("DK UMA : Can not Equip Element "+_slot.name+" because its overlay is missing.");
		}
		else if ( _overlay != null ) {/* Debug.Log ("DK UMA : Equiping overlay "+_overlay.name+".");*/ }
		else Debug.LogError ("DK UMA : Can not Equip Element because slot and overlay are missing.");

		if ( _slot != null || _overlay != null ){
			// Head
			if ( ( _slot != null && _slot.OverlayType == "HeadWear" )
			    || ( _overlay != null && _overlay.OverlayType == "HeadWear" ) ) {
				DK_UMA_RPG_EquipHead.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Cloak
			else if ( ( _slot != null && _slot.Place.name == "CloakWear" ) ) {
				DK_UMA_RPG_EquipCloak.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Backpack
			else if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "Backpack" ) ) {
				DK_UMA_RPG_EquipBackpack.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// RingRight
			else if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "RingRight" ) ) {
				DK_UMA_RPG_EquipRingRight.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// RingLeft
			else if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "RingLeft" ) ) {
				DK_UMA_RPG_EquipRingLeft.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Collar
			else if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "Collar" )
				   || ( _overlay != null && _overlay.Place != null && _overlay.Place.name == "Collar" ) ) {
				DK_UMA_RPG_EquipCollar.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Shoulder
			else if ( ( _slot != null && _slot.OverlayType == "ShoulderWear" )
			    || ( _overlay != null && _overlay.OverlayType == "ShoulderWear" ) ) {
				DK_UMA_RPG_EquipShoulder.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// ArmBand
			else if ( ( _slot != null && _slot.OverlayType == "ArmbandWear" )
			    || ( _overlay != null && _overlay.OverlayType == "ArmbandWear" ) ) {
				DK_UMA_RPG_EquipArmBand.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Wrist
			else if ( ( _slot != null && _slot.OverlayType == "WristWear" )
				   || ( _overlay != null && _overlay.OverlayType == "WristWear" ) ) {
				DK_UMA_RPG_EquipWrist.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Hands
			else if ( ( _slot != null && _slot.OverlayType == "HandsWear" )
			    || ( _overlay != null && _overlay.OverlayType == "HandsWear" ) ) {
				DK_UMA_RPG_EquipHands.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// HandledRight
			if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "HandledRight" ) ) {
				DK_UMA_RPG_EquipHandledRight.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// HandledLeft
			else if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "HandledLeft" ) ) {
				DK_UMA_RPG_EquipHandledLeft.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Torso
			else if ( ( _slot != null && _slot.OverlayType == "TorsoWear" )
			    || ( _overlay != null && _overlay.OverlayType == "TorsoWear" ) ) {
				DK_UMA_RPG_EquipTorso.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Belt
			else if ( ( _slot != null && _slot.OverlayType == "BeltWear" )
			    || ( _overlay != null && _overlay.OverlayType == "BeltWear" ) ) {
				DK_UMA_RPG_EquipBelt.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Legs
			else if ( ( _slot != null && _slot.OverlayType == "LegsWear" )
			    || ( _overlay != null && _overlay.OverlayType == "LegsWear" ) ) {
				DK_UMA_RPG_EquipLegs.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// LegsBand
			else if ( ( _slot != null && _slot.OverlayType == "LegBandWear" )
			    || ( _overlay != null && _overlay.OverlayType == "LegBandWear" ) ) {
				DK_UMA_RPG_EquipLegsBand.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// feet
			else if ( ( _slot != null && _slot.OverlayType == "FeetWear" )
			    || ( _overlay != null && _overlay.OverlayType == "FeetWear" ) ) {
				DK_UMA_RPG_EquipFeet.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Glasses
			else if ( ( _slot != null && _slot.Place.name == "GlassesWear" ) ) {
				DK_UMA_RPG_EquipGlasses.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}
			// Mask
			else if ( ( _slot != null && _slot.Place.name == "MaskWear" ) ) {
				DK_UMA_RPG_EquipMask.EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, ColorPreset , _color, Layer, LayersAction, Opt1, Opt2 );
			}

			// save
			SaveAvatar ( _DK_RPG_UMA );
		}
	}


	public static void RemoveElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, string Layer, string LayersAction ){
		// for a slot element
		if ( _slot != null || _overlay != null ){
			// find the correct place

			// Glasses
			if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "GlassesWear" ) ) {
				_DK_RPG_UMA._Equipment._Glasses.Slot = null;
				_DK_RPG_UMA._Equipment._Glasses.Overlay = null;
				_DK_RPG_UMA._Equipment._Glasses.ColorPreset = null;
			}
			// Mask
			if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "MaskWear" ) ) {
				_DK_RPG_UMA._Equipment._Mask.Slot = null;
				_DK_RPG_UMA._Equipment._Mask.Overlay = null;
				_DK_RPG_UMA._Equipment._Mask.ColorPreset = null;
			}

			// Headwear
			if ( ( _slot != null && _slot.OverlayType == "HeadWear" )
			    || ( _overlay != null && _overlay.OverlayType == "HeadWear" ) ) {
				if ( Layer == "Sub" ){
					_DK_RPG_UMA._Equipment._HeadSub.Slot = null;
					_DK_RPG_UMA._Equipment._HeadSub.Overlay = null;
					_DK_RPG_UMA._Equipment._HeadSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._Head.Slot = null;
					_DK_RPG_UMA._Equipment._Head.Overlay = null;
					_DK_RPG_UMA._Equipment._Head.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._HeadCover.Slot = null;
					_DK_RPG_UMA._Equipment._HeadCover.Overlay = null;
					_DK_RPG_UMA._Equipment._HeadCover.ColorPreset = null;
				}
			}
			// Cloak
			else if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "Cloak" )
				   || ( _overlay != null && _overlay.Place != null && _overlay.Place.name == "Cloak" ) ) {
				_DK_RPG_UMA._Equipment._Cloak.Slot = null;
				_DK_RPG_UMA._Equipment._Cloak.Overlay = null;
				_DK_RPG_UMA._Equipment._Cloak.ColorPreset = null;
			}
			// Collar
			else if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "Collar" )
				   || ( _overlay != null && _overlay.Place != null && _overlay.Place.name == "Collar" ) ) {
				if ( Layer == "Sub" ){
					_DK_RPG_UMA._Equipment._CollarSub.Slot = null;
					_DK_RPG_UMA._Equipment._CollarSub.Overlay = null;
					_DK_RPG_UMA._Equipment._CollarSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._Collar.Slot = null;
					_DK_RPG_UMA._Equipment._Collar.Overlay = null;
					_DK_RPG_UMA._Equipment._Collar.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._CollarCover.Slot = null;
					_DK_RPG_UMA._Equipment._CollarCover.Overlay = null;
					_DK_RPG_UMA._Equipment._CollarCover.ColorPreset = null;
				}
			}
			// Shoulderwear
			else if ( ( _slot != null && _slot.OverlayType == "ShoulderWear" )
			    || ( _overlay != null && _overlay.OverlayType == "ShoulderWear" ) ) {
				if ( Layer == "Sub" ){
					_DK_RPG_UMA._Equipment._ShoulderSub.Slot = null;
					_DK_RPG_UMA._Equipment._ShoulderSub.Overlay = null;
					_DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._Shoulder.Slot = null;
					_DK_RPG_UMA._Equipment._Shoulder.Overlay = null;
					_DK_RPG_UMA._Equipment._Shoulder.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._ShoulderCover.Slot = null;
					_DK_RPG_UMA._Equipment._ShoulderCover.Overlay = null;
					_DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset = null;
				}
			}
			// ArmBandwear
			else if ( ( _slot != null && _slot.OverlayType == "ArmbandWear" )
				   || ( _overlay != null && _overlay.OverlayType == "ArmbandWear" ) ) {
				if ( Layer == "Sub" ){
					_DK_RPG_UMA._Equipment._ArmBandSub.Slot = null;
					_DK_RPG_UMA._Equipment._ArmBandSub.Overlay = null;
					_DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._ArmBand.Slot = null;
					_DK_RPG_UMA._Equipment._ArmBand.Overlay = null;
					_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._ArmBandCover.Slot = null;
					_DK_RPG_UMA._Equipment._ArmBandCover.Overlay = null;
					_DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = null;
				}
			}
			// Handswear
			else if ( ( _slot != null && _slot.OverlayType == "HandsWear" )
				   || ( _overlay != null && _overlay.OverlayType == "HandsWear" ) ) {
				if ( Layer == "Sub" ){
					_DK_RPG_UMA._Equipment._HandsSub.Slot = null;
					_DK_RPG_UMA._Equipment._HandsSub.Overlay = null;
					_DK_RPG_UMA._Equipment._HandsSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._Hands.Slot = null;
					_DK_RPG_UMA._Equipment._Hands.Overlay = null;
					_DK_RPG_UMA._Equipment._Hands.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._HandsCover.Slot = null;
					_DK_RPG_UMA._Equipment._HandsCover.Overlay = null;
					_DK_RPG_UMA._Equipment._HandsCover.ColorPreset = null;
				}
			}
			// HandledRight
			else if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "HandledRight" )
				   || ( _overlay != null && _overlay.Place != null && _overlay.Place.name == "HandledRight" ) ) {
				_DK_RPG_UMA._Equipment._RightHand.Slot = null;
				_DK_RPG_UMA._Equipment._RightHand.Overlay = null;
				_DK_RPG_UMA._Equipment._RightHand.ColorPreset = null;
			}
			// HandledLeft
			else if ( ( _slot != null && _slot.Place != null && _slot.Place.name == "HandledLeft" )
				|| ( _overlay != null && _overlay.Place != null && _overlay.Place.name == "HandledLeft" ) ) {
				_DK_RPG_UMA._Equipment._LeftHand.Slot = null;
				_DK_RPG_UMA._Equipment._LeftHand.Overlay = null;
				_DK_RPG_UMA._Equipment._LeftHand.ColorPreset = null;
				
			}
			// Torsowear
			else if ( ( _slot != null && _slot.OverlayType == "TorsoWear" )
			    || ( _overlay != null && _overlay.OverlayType == "TorsoWear" ) ) {
				if ( Layer == "Sub" ){
				//	_DK_RPG_UMA._Equipment._TorsoSub.Slot = null;
					_DK_RPG_UMA._Equipment._TorsoSub.Overlay = null;
					_DK_RPG_UMA._Equipment._TorsoSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._Torso.Slot = null;
					_DK_RPG_UMA._Equipment._Torso.Overlay = null;
					_DK_RPG_UMA._Equipment._Torso.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._TorsoCover.Slot = null;
					_DK_RPG_UMA._Equipment._TorsoCover.Overlay = null;
					_DK_RPG_UMA._Equipment._TorsoCover.ColorPreset = null;
				}
			}
			// Beltwear
			else if ( ( _slot != null && _slot.OverlayType == "BeltWear" )
			    || ( _overlay != null && _overlay.OverlayType == "BeltWear" ) ) {
				if ( Layer == "Sub" ){
					_DK_RPG_UMA._Equipment._BeltSub.Slot = null;
					_DK_RPG_UMA._Equipment._BeltSub.Overlay = null;
					_DK_RPG_UMA._Equipment._BeltSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._Belt.Slot = null;
					_DK_RPG_UMA._Equipment._Belt.Overlay = null;
					_DK_RPG_UMA._Equipment._Belt.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._BeltCover.Slot = null;
					_DK_RPG_UMA._Equipment._BeltCover.Overlay = null;
					_DK_RPG_UMA._Equipment._BeltCover.ColorPreset = null;
				}
			}
			// Legswear
			else if ( ( _slot != null && _slot.OverlayType == "LegsWear" )
			    || ( _overlay != null && _overlay.OverlayType == "LegsWear" ) ) {
				if ( Layer == "Sub" ){
					_DK_RPG_UMA._Equipment._LegsSub.Slot = null;
					_DK_RPG_UMA._Equipment._LegsSub.Overlay = null;
					_DK_RPG_UMA._Equipment._LegsSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._Legs.Slot = null;
					_DK_RPG_UMA._Equipment._Legs.Overlay = null;
					_DK_RPG_UMA._Equipment._Legs.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._LegsCover.Slot = null;
					_DK_RPG_UMA._Equipment._LegsCover.Overlay = null;
					_DK_RPG_UMA._Equipment._LegsCover.ColorPreset = null;
				}
			}
			// LegBandwear
			else if ( ( _slot != null && _slot.OverlayType == "LegBandWear" )
			    || ( _overlay != null && _overlay.OverlayType == "LegBandWear" ) ) {
				if ( Layer == "Sub" ){
					_DK_RPG_UMA._Equipment._LegBandSub.Slot = null;
					_DK_RPG_UMA._Equipment._LegBandSub.Overlay = null;
					_DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._LegBand.Slot = null;
					_DK_RPG_UMA._Equipment._LegBand.Overlay = null;
					_DK_RPG_UMA._Equipment._LegBand.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._LegBandCover.Slot = null;
					_DK_RPG_UMA._Equipment._LegBandCover.Overlay = null;
					_DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = null;
				}
			}
			// Feetwear
			else if ( ( _slot != null && _slot.OverlayType == "FeetWear" )
			    || ( _overlay != null && _overlay.OverlayType == "FeetWear" ) ) {
				if ( Layer == "Sub" ){
					_DK_RPG_UMA._Equipment._FeetSub.Slot = null;
					_DK_RPG_UMA._Equipment._FeetSub.Overlay = null;
					_DK_RPG_UMA._Equipment._FeetSub.ColorPreset = null;
				}
				if ( Layer == "Main" ){
					_DK_RPG_UMA._Equipment._Feet.Slot = null;
					_DK_RPG_UMA._Equipment._Feet.Overlay = null;
					_DK_RPG_UMA._Equipment._Feet.ColorPreset = null;
				}
				if ( Layer == "Cover" ){
					_DK_RPG_UMA._Equipment._FeetCover.Slot = null;
					_DK_RPG_UMA._Equipment._FeetCover.Overlay = null;
					_DK_RPG_UMA._Equipment._FeetCover.ColorPreset = null;
				}
			}

			// save
			SaveAvatar ( _DK_RPG_UMA );
		}

		// rebuild

		DK_RPG_ReBuild _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.GetComponent<DK_RPG_ReBuild>();
		if ( _DK_RPG_ReBuild == null ) _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.AddComponent<DK_RPG_ReBuild>();
		DKUMAData _DKUMAData = _DK_RPG_UMA.gameObject.GetComponent<DKUMAData>();
		if ( _DK_RPG_ReBuild != null ){
			_DK_RPG_ReBuild.RefreshOnly = true;
			_DK_RPG_ReBuild.Launch (_DKUMAData);
		}
	}

	public static void SaveAvatar ( DK_RPG_UMA _DK_RPG_UMA ){
		// save to file ?
		// to do so, the player avatar (DK_RPG_UMA component) have to be IsPlayer, LoadFromFile and a file name have to be written
		if ( _DK_RPG_UMA.IsPlayer && _DK_RPG_UMA.LoadFromFile && _DK_RPG_UMA.FileName != "" ){
			DK_UMA_Save.SaveCompleteAvatar ( _DK_RPG_UMA, _DK_RPG_UMA.FileName, true, false );
		}
	}
}
