using UnityEngine;
using System.Collections;

public class DK_UMA_RPG_ChangeBody : MonoBehaviour {

	public static DKOverlayData _overlay;
	public static Color _color = Color.black;

	public static void PrepareChangeSlotElement (DKSlotData _slot, DKOverlayData overlay, DK_RPG_UMA _DK_RPG_UMA) {
		Debug.Log ("DK_UMA_RPG_ChangeBody");
		if ( _slot ){
			// choose overlay
			if ( overlay == null && _slot.LinkedOverlayList.Count > 0 ){
				int ran = Random.Range(0, _slot.LinkedOverlayList.Count);
				_overlay = _slot.LinkedOverlayList[ran];
			}
			// if no linked overlays in the list
			if ( overlay != null ) _overlay = overlay;
			// for face
			else  if (  _slot.Place.name == "Head" && _slot.OverlayType == "Face" ) {
				ChooseFaceOverlay ( _slot, _overlay, _DK_RPG_UMA );
			}
			// for face elements
			else if ( _slot.Place.name == "Ears" 
			         || _slot.Place.name == "Nose" 
			         || _slot.Place.name == "Mouth" 
			         || _slot.Place.name == "eyelid" ) {
				if ( _DK_RPG_UMA._Avatar._Face._Head.Overlay != null )  ChooseFaceOverlay ( _slot, _overlay, _DK_RPG_UMA );
				_overlay = _DK_RPG_UMA._Avatar._Face._Head.Overlay;
			}
			// for body
			else  if ( _slot.Place.name == "Torso" && _slot.OverlayType == "Flesh" ) {
				ChooseBodyOverlay ( _slot, _overlay, _DK_RPG_UMA );
			}
			// for face elements
			else if ( _slot.Place.name != "Torso" && _slot.OverlayType == "Flesh"  ) {
				if ( _DK_RPG_UMA._Avatar._Body._Torso.Overlay != null )  ChooseBodyOverlay ( _slot, _overlay, _DK_RPG_UMA );
				_overlay = _DK_RPG_UMA._Avatar._Body._Torso.Overlay;
			}
		}
		else if ( overlay ) {
			_overlay = overlay;
			// choose color
			if ( _overlay && _overlay.ColorPresets.Count > 0 ) {
				int ran = Random.Range(0, _overlay.ColorPresets.Count);
				_color = _overlay.ColorPresets[ran].PresetColor;
			}
			// if no color overlay, randomise color
			else {
				float ran1 = Random.Range(0f, 0.99f);
				float ran2 = Random.Range(0f, 0.99f);
				float ran3 = Random.Range(0f, 0.99f);
				_color = new Color (ran1, ran2, ran3 );
			}
		}
		if ( _overlay != null && _DK_RPG_UMA != null /*&& _color != Color.black*/ )
			EquipSlotElement ( _slot, _overlay, _DK_RPG_UMA, _color );
	}

	public static void EquipSlotElement ( DKSlotData _slot, DKOverlayData _overlay, DK_RPG_UMA _DK_RPG_UMA, Color color ){
		// for a slot element
		if ( _slot != null ){
			if ( _slot.Place.name == "Head" && _slot.OverlayType == "Face" ) {
				_DK_RPG_UMA._Avatar._Face._Head.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Face._Head.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Face._Head.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Face._Head.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			#region Face
			else if ( _slot.Place.name == "Beard" && _slot.OverlayType == "Beard" ) {
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Color = _DK_RPG_UMA._Avatar.HairColor;
			}
			else if ( _slot.Place.name == "Eyes" && _slot.slotName.Contains("Adjust") == false ) {
				_DK_RPG_UMA._Avatar._Face._Eyes.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Face._Eyes.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Face._Eyes.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Face._Eyes.Color = _DK_RPG_UMA._Avatar.EyeColor;
			}
			else if ( _slot.Place.name == "Eyelash" ) {
				Debug.Log ( "Test Eyelash");
				_DK_RPG_UMA._Avatar._Face._EyeLash.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Face._EyeLash.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Face._EyeLash.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Face._EyeLash.Color = _DK_RPG_UMA._Avatar.EyeColor;
			}
			else if ( _slot.Place.name == "eyelid" && _slot.OverlayType == "Face" ) {
				_DK_RPG_UMA._Avatar._Face._EyeLids.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Face._EyeLids.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Face._EyeLids.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Face._EyeLids.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			else if ( _slot.Place.name == "Ears" && _slot.OverlayType == "Face" ) {
				_DK_RPG_UMA._Avatar._Face._Ears.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Face._Ears.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Face._Ears.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Face._Ears.Color = color;
			}
			else if ( _slot.Place.name == "Nose" && _slot.OverlayType == "Face" ) {
				_DK_RPG_UMA._Avatar._Face._Nose.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Face._Nose.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Face._Nose.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Face._Nose.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			else if ( _slot.Place.name == "Mouth" && _slot.OverlayType == "Face" ) {
				_DK_RPG_UMA._Avatar._Face._Mouth.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Face._Mouth.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Face._Mouth.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Face._Mouth.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			else if ( _slot.Place.name == "InnerMouth" && _slot.OverlayType == "" ) {
				_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
			//	_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.color = color;
			}
			else if ( _slot.Place.name == "Hair" && _slot.OverlayType == "Hair" ) {
				_DK_RPG_UMA._Avatar._Hair._SlotOnly.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Hair._SlotOnly.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Hair._SlotOnly.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Hair._OverlayOnly.Overlay = null;
				_DK_RPG_UMA._Avatar._Hair._SlotOnly.Color = _DK_RPG_UMA._Avatar.HairColor;
			}
			else if ( _slot.Place.name == "Hair_Module" && _slot.OverlayType == "Hair" ) {
				_DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Color = _DK_RPG_UMA._Avatar.HairColor;
			}
			#endregion Face

			#region Body
			else if ( _slot.Place.name == "Torso" && _slot.OverlayType == "Flesh" ) {
				_DK_RPG_UMA._Avatar._Body._Torso.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Body._Torso.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Body._Torso.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Body._Torso.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			else if ( _slot.Place.name == "Hands" && _slot.OverlayType == "Flesh" ) {
				_DK_RPG_UMA._Avatar._Body._Hands.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Body._Hands.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Body._Hands.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Body._Hands.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			else if ( _slot.Place.name == "Legs" && _slot.OverlayType == "Flesh" ) {
				_DK_RPG_UMA._Avatar._Body._Legs.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Body._Legs.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Body._Legs.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Body._Legs.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			else if ( _slot.Place.name == "Feet" && _slot.OverlayType == "Flesh" ) {
				_DK_RPG_UMA._Avatar._Body._Feet.Slot = _slot;
				// if the overlay is already assigned
				if ( _overlay ) {
					_DK_RPG_UMA._Avatar._Body._Feet.Overlay = _overlay;
				}
				// if the overlay is not assigned, random one from the linked overlays of the slot
				else {
					if ( _slot.LinkedOverlayList.Count > 0 ) {
						int ran = Random.Range(0, _slot.LinkedOverlayList.Count-1);
						_DK_RPG_UMA._Avatar._Body._Feet.Overlay = _slot.LinkedOverlayList[ran];
					}
				}
				_DK_RPG_UMA._Avatar._Body._Feet.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			#endregion Body

		}
		// for an Overlay element
		else if ( _overlay ) {
			#region Face
			if ( _overlay.OverlayType == "Face" && _overlay.Place.name == "Head" ) {
				_DK_RPG_UMA._Avatar._Face._Head.Overlay = _overlay;
				if ( _DK_RPG_UMA._Avatar._Face._Head.Color == Color.black ) 
					_DK_RPG_UMA._Avatar._Face._Head.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			if ( _overlay.OverlayType == "Tatoo" && _overlay.Place.name == "Head" ) {
				_DK_RPG_UMA._Avatar._Face._Head.Tattoo = _overlay;
				if ( _DK_RPG_UMA._Avatar._Face._Head.TattooColor == Color.black ) 
					_DK_RPG_UMA._Avatar._Face._Head.TattooColor = color;
			}
			if ( _overlay.OverlayType == "Makeup" && _overlay.Place.name == "Head" ) {
				_DK_RPG_UMA._Avatar._Face._Head.Makeup = _overlay;
				if ( _DK_RPG_UMA._Avatar._Face._Head.MakeupColor == Color.black ) 
					_DK_RPG_UMA._Avatar._Face._Head.MakeupColor = color;
			}
			if ( _overlay.OverlayType == "Eyebrow" ) {
				_DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows = _overlay;
				if ( _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrowsColor == Color.black ) 
					_DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrowsColor = color;
			}
			else if ( _overlay.OverlayType == "Beard" ) {
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Slot = null;
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Overlay = null;
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1 = _overlay;
				if ( _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1Color == Color.black )
					_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1Color = color;
			}
			else if ( _overlay.Place.name == "Eyes" && _overlay.overlayName.Contains("Adjust") ) {
				_DK_RPG_UMA._Avatar._Face._Eyes.Adjust = _overlay;
				if ( _DK_RPG_UMA._Avatar._Face._Eyes.AdjustColor == Color.black )
					_DK_RPG_UMA._Avatar._Face._Eyes.AdjustColor = color;
			}
			else if ( _overlay.Place.name == "Hair" && _overlay.OverlayType == "Hair" ) {
				_DK_RPG_UMA._Avatar._Hair._SlotOnly.Slot = null;
				_DK_RPG_UMA._Avatar._Hair._SlotOnly.Overlay = null;
				_DK_RPG_UMA._Avatar._Hair._OverlayOnly.Overlay = _overlay;
				if ( _DK_RPG_UMA._Avatar._Hair._OverlayOnly.Color == Color.black )
					_DK_RPG_UMA._Avatar._Hair._OverlayOnly.Color = color;
			}
			#endregion Face

			#region Body
			else if ( _overlay.OverlayType == "Tatoo" && _overlay.Place.name == "Torso" ) {
				_DK_RPG_UMA._Avatar._Body._Torso.Tattoo = _overlay;
				if ( _DK_RPG_UMA._Avatar._Body._Torso.TattooColor == Color.black )
					_DK_RPG_UMA._Avatar._Body._Torso.TattooColor = color;
			}
			else if ( _overlay.OverlayType == "Tatoo" && _overlay.Place.name == "Hands" ) {
				_DK_RPG_UMA._Avatar._Body._Hands.Tattoo = _overlay;
				if ( _DK_RPG_UMA._Avatar._Body._Hands.TattooColor == Color.black )
					_DK_RPG_UMA._Avatar._Body._Hands.TattooColor = color;
			}
			else if ( _overlay.OverlayType == "Tatoo" && _overlay.Place.name == "Legs" ) {
				_DK_RPG_UMA._Avatar._Body._Legs.Tattoo = _overlay;
				if ( _DK_RPG_UMA._Avatar._Body._Hands.TattooColor == Color.black )
					_DK_RPG_UMA._Avatar._Body._Legs.TattooColor = color;
			}
			else if ( _overlay.OverlayType == "Tatoo" && _overlay.Place.name == "Feet" ) {
				_DK_RPG_UMA._Avatar._Body._Feet.Tattoo = _overlay;
				if ( _DK_RPG_UMA._Avatar._Body._Feet.TattooColor == Color.black )
					_DK_RPG_UMA._Avatar._Body._Feet.TattooColor = color;
			}
			else if ( _overlay.OverlayType == "Makeup" && _overlay.Place.name == "Torso" ) {
				_DK_RPG_UMA._Avatar._Body._Torso.Makeup = _overlay;
				if ( _DK_RPG_UMA._Avatar._Body._Torso.MakeupColor == Color.black )
					_DK_RPG_UMA._Avatar._Body._Torso.MakeupColor = color;
			}
			else if ( _overlay.OverlayType == "Makeup" && _overlay.Place.name == "Hands" ) {
				_DK_RPG_UMA._Avatar._Body._Hands.Makeup = _overlay;
				if ( _DK_RPG_UMA._Avatar._Body._Hands.MakeupColor == Color.black )
					_DK_RPG_UMA._Avatar._Body._Hands.MakeupColor = color;
			}
			else if ( _overlay.OverlayType == "Makeup" && _overlay.Place.name == "Legs" ) {
				_DK_RPG_UMA._Avatar._Body._Legs.Makeup = _overlay;
				if ( _DK_RPG_UMA._Avatar._Body._Legs.MakeupColor == Color.black )
					_DK_RPG_UMA._Avatar._Body._Legs.MakeupColor = color;
			}
			else if ( _overlay.OverlayType == "Makeup" && _overlay.Place.name == "Feet" ) {
				_DK_RPG_UMA._Avatar._Body._Feet.Makeup = _overlay;
				if ( _DK_RPG_UMA._Avatar._Body._Feet.MakeupColor == Color.black )
					_DK_RPG_UMA._Avatar._Body._Feet.MakeupColor = color;
			}
			#endregion Body

		}
		Rebuild ( _DK_RPG_UMA );

	}

	public static void ChooseFaceOverlay (DKSlotData _slot, DKOverlayData overlay, DK_RPG_UMA _DK_RPG_UMA) {
		DKRaceData _Race = _DK_RPG_UMA.RaceData;
		// _Head
		if ( _DK_RPG_UMA.Gender == "Male" ) 
		if ( overlay == null && _Race._Male._AvatarData._Face._Head.OverlayList.Count > 0 ){
			int ran = UnityEngine.Random.Range (0, _Race._Male._AvatarData._Face._Head.OverlayList.Count);
			overlay = _Race._Male._AvatarData._Face._Head.OverlayList[ran];
		}
		if ( _DK_RPG_UMA.Gender == "Female" ) 
		if (  overlay == null && _Race._Female._AvatarData._Face._Head.OverlayList.Count > 0 ){
			int ran = UnityEngine.Random.Range (0, _Race._Female._AvatarData._Face._Head.OverlayList.Count);
			overlay = _Race._Female._AvatarData._Face._Head.OverlayList[ran];
		}
		_DK_RPG_UMA._Avatar._Face._Head.Overlay = overlay;
		Rebuild ( _DK_RPG_UMA );
	}

	public static void ChooseBodyOverlay (DKSlotData _slot, DKOverlayData overlay, DK_RPG_UMA _DK_RPG_UMA) {
		DKRaceData _Race = _DK_RPG_UMA.RaceData;
		// _Head
		if ( _DK_RPG_UMA.Gender == "Male" ) 
		if ( overlay == null && _Race._Male._AvatarData._Body._Torso.OverlayList.Count > 0 ){
			int ran = UnityEngine.Random.Range (0, _Race._Male._AvatarData._Body._Torso.OverlayList.Count);
			overlay = _Race._Male._AvatarData._Body._Torso.OverlayList[ran];
			
		}
		if ( _DK_RPG_UMA.Gender == "Female" ) 
		if ( overlay == null && _Race._Female._AvatarData._Body._Torso.OverlayList.Count > 0 ){
			int ran = UnityEngine.Random.Range (0, _Race._Female._AvatarData._Body._Torso.OverlayList.Count);
			overlay = _Race._Female._AvatarData._Body._Torso.OverlayList[ran];
		}
		_DK_RPG_UMA._Avatar._Body._Torso.Overlay = overlay;
		Rebuild ( _DK_RPG_UMA );
	
	}

	public static void Rebuild ( DK_RPG_UMA _DK_RPG_UMA ) {
		DK_RPG_ReBuild _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.GetComponent<DK_RPG_ReBuild>();
		if ( _DK_RPG_ReBuild == null ) _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.AddComponent<DK_RPG_ReBuild>();
		DKUMAData _DKUMAData = _DK_RPG_UMA.gameObject.GetComponent<DKUMAData>();
		_DK_RPG_ReBuild.RefreshOnly = true;
		_DK_RPG_ReBuild.Launch (_DKUMAData);
	}
}
