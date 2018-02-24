using UnityEngine;
using System.Collections;



#pragma warning disable 472
public class DK_UMA_LoadData : MonoBehaviour {

	public static void LoadAvatar ( DK_RPG_UMA _DK_RPG_UMA, DK_UMACrowd _DK_UMACrowd, SaveData data ) {


		//Load the data we just saved

		// create new avatar

		// about race
		if ( _DK_RPG_UMA._LOD.OldRaceName != _DK_RPG_UMA.RaceData.raceName ){
			_DK_RPG_UMA._LOD.OldRaceName = _DK_RPG_UMA.RaceData.raceName;
		}
	
		if (data == null ) Debug.Log ("no data");

		_DK_RPG_UMA.Name = data.GetValue<string>("Name");
		_DK_RPG_UMA.Gender = data.GetValue<string>("Gender");
		_DK_RPG_UMA.Weight = data.GetValue<string>("Weight");

		// Race
		_DK_RPG_UMA.Race = data.GetValue<string>("Race");
		for(int i = 0; i < _DK_UMACrowd.raceLibrary.raceElementList.Length; i++)
		if(_DK_UMACrowd.raceLibrary.raceElementList[i].name == data.GetValue<string>("RaceData")){
			_DK_RPG_UMA.RaceData = _DK_UMACrowd.raceLibrary.raceElementList[i];
			if ( _DK_RPG_UMA.transform.GetComponent<DKUMAData>().umaRecipe.raceData != _DK_RPG_UMA.RaceData )
				_DK_RPG_UMA.transform.GetComponent<DKUMAData>().umaRecipe.raceData = _DK_RPG_UMA.RaceData;
		}

		// reset beard and hair only
		_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1 = null;
		_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2 = null;
		_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3 = null;
		_DK_RPG_UMA._Avatar._Hair._OverlayOnly.Overlay = null;

		#region Root
		int Index = 0;
		data.TryGetValue<int>("HeadIndex", out Index); _DK_RPG_UMA._Avatar.HeadIndex = Index;
		data.TryGetValue<int>("TorsoIndex", out Index); _DK_RPG_UMA._Avatar.TorsoIndex = Index;
		_DK_RPG_UMA._Avatar.SkinColor = data.GetValue<Color>("SkinColor");
		_DK_RPG_UMA._Avatar.HairColor = data.GetValue<Color>("HairColor");
		_DK_RPG_UMA._Avatar.EyeColor = data.GetValue<Color>("EyeColor");

		// ColorPresets from race
		foreach ( ColorPresetData ColorPreset in _DK_RPG_UMA.RaceData.ColorPresetDataList ){
			// skin
			if ( ColorPreset.ColorPresetName == data.GetValue<string>("SkinColorPreset") ) 
				_DK_RPG_UMA._Avatar.SkinColorPreset = ColorPreset;
			else _DK_RPG_UMA._Avatar.SkinColorPreset = null;
			// Hair
			if ( ColorPreset.ColorPresetName == data.GetValue<string>("HairColorPreset") ) 
				_DK_RPG_UMA._Avatar.HairColorPreset = ColorPreset;

			// Eye
			if ( ColorPreset.ColorPresetName == data.GetValue<string>("EyeColorPreset") ) 
				_DK_RPG_UMA._Avatar.EyeColorPreset = ColorPreset;
		}
		if ( data.GetValue<string>("SkinColorPreset") == "NA" ) _DK_RPG_UMA._Avatar.SkinColorPreset = null;
		if ( data.GetValue<string>("HairColorPreset") == "NA" ) _DK_RPG_UMA._Avatar.HairColorPreset = null;
		if ( data.GetValue<string>("EyeColorPreset") == "NA" ) _DK_RPG_UMA._Avatar.EyeColorPreset = null;
		#endregion Root

		#region Slots re assign
		foreach ( DKSlotData slot in _DK_UMACrowd.slotLibrary.slotElementList ){
			if ( slot == null ) Debug.LogError ( "A slot is missing from your library" );
			#region Head			
			if ( slot.name == data.GetValue<string>("FaceHeadSlot") )
				_DK_RPG_UMA._Avatar._Face._Head.Slot = slot;
			// Beard slot
			else if ( data.GetValue<string>("BeardSlotOnlySlot") != "NA" 
			         && slot.name == data.GetValue<string>("BeardSlotOnlySlot") )
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Slot = slot;
			// Eyes
			else if ( slot.name == data.GetValue<string>("FaceEyesSlot") )
				_DK_RPG_UMA._Avatar._Face._Eyes.Slot = slot;
			// EyeLash
			else if ( data.GetValue<string>("FaceEyeLashSlot") != "NA" 
			         && slot.name == data.GetValue<string>("FaceEyeLashSlot") )
				_DK_RPG_UMA._Avatar._Face._EyeLash.Slot = slot;
			// Eyelids
			else if ( slot.name == data.GetValue<string>("FaceEyeLidsSlot") )
				_DK_RPG_UMA._Avatar._Face._EyeLids.Slot = slot;
			// Ears
			else if ( slot.name == data.GetValue<string>("FaceEarsSlot") )
				_DK_RPG_UMA._Avatar._Face._Ears.Slot = slot;
			// Nose
			else if ( slot.name == data.GetValue<string>("FaceNoseSlot") )
				_DK_RPG_UMA._Avatar._Face._Nose.Slot = slot;
			// Mouth
			else if ( slot.name == data.GetValue<string>("FaceMouthSlot") )
				_DK_RPG_UMA._Avatar._Face._Mouth.Slot = slot;
			// Innermouth
			else if ( slot.name == data.GetValue<string>("FaceInnerMouthSlot") )
				_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Slot = slot;

			// Hair
			// Hair slot
			else if ( data.GetValue<string>("HairSlotOnlySlot") != "NA" 
			         && slot.name == data.GetValue<string>("HairSlotOnlySlot") )
				_DK_RPG_UMA._Avatar._Hair._SlotOnly.Slot = slot;
			// Hair module slot
			else if ( data.GetValue<string>("HairModuleSlot") != "NA" 
			         && slot.name == data.GetValue<string>("HairModuleSlot") )
				_DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Slot = slot;
			#endregion Head

			#region Body
			// Torso
			else if ( slot.name == data.GetValue<string>("BodyTorsoSlot") )
				_DK_RPG_UMA._Avatar._Body._Torso.Slot = slot;
			// Hands
			else if ( slot.name == data.GetValue<string>("BodyHandsSlot") )
				_DK_RPG_UMA._Avatar._Body._Hands.Slot = slot;
			// Legs
			else if ( slot.name == data.GetValue<string>("BodyLegsSlot") )
				_DK_RPG_UMA._Avatar._Body._Legs.Slot = slot;
			// Feet
			else if ( slot.name == data.GetValue<string>("BodyFeetSlot") )
				_DK_RPG_UMA._Avatar._Body._Feet.Slot = slot;
			// Wings
			else if ( data.GetValue<string>("BodyWingsSlot") != "NA" 
			         && slot.name == data.GetValue<string>("BodyWingsSlot") )
				_DK_RPG_UMA._Avatar._Body._Wings.Slot = slot;
			// Tail
			else if ( data.GetValue<string>("BodyTailSlot") != "NA" 
			         && slot.name == data.GetValue<string>("BodyTailSlot") )
				_DK_RPG_UMA._Avatar._Body._Tail.Slot = slot;
			// Underwear
			else if ( data.GetValue<string>("BodyUnderwearSlot") != "NA" 
			         && slot.name == data.GetValue<string>("BodyUnderwearSlot") ){
				_DK_RPG_UMA._Avatar._Body._Underwear.Slot = slot;
			}

			#endregion Body

			#region Equipment
			// Head Sub
			if ( data.GetValue<string>("EquipHeadSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipHeadSubSlot") )
				_DK_RPG_UMA._Equipment._HeadSub.Slot = slot;
			// Head
			if ( data.GetValue<string>("EquipHeadSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipHeadSlot") )
				_DK_RPG_UMA._Equipment._Head.Slot = slot;
			// Head Cover
			if ( data.GetValue<string>("EquipHeadCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipHeadCoverSlot") )
				_DK_RPG_UMA._Equipment._HeadCover.Slot = slot;

			// Shoulder Sub
			if ( data.GetValue<string>("EquipShoulderSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipShoulderSubSlot") )
				_DK_RPG_UMA._Equipment._ShoulderSub.Slot = slot;
			// Shoulder
			if ( data.GetValue<string>("EquipShoulderSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipShoulderSlot") )
				_DK_RPG_UMA._Equipment._Shoulder.Slot = slot;
			// Shoulder Cover
			if ( data.GetValue<string>("EquipShoulderCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipShoulderCoverSlot") )
				_DK_RPG_UMA._Equipment._ShoulderCover.Slot = slot;

			// Torso Sub
		//	if ( data.GetValue<string>("EquipTorsoSubSlot") != "NA" 
		//	         && slot.name == data.GetValue<string>("EquipTorsoSubSlot") ){
			//	_DK_RPG_UMA._Equipment._TorsoSub.Slot = slot;
		//	}

			// Torso
			if ( data.GetValue<string>("EquipTorsoSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipTorsoSlot") )
				_DK_RPG_UMA._Equipment._Torso.Slot = slot;
			// Torso Cover
			if ( data.GetValue<string>("EquipTorsoCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipTorsoCoverSlot") )
				_DK_RPG_UMA._Equipment._TorsoCover.Slot = slot;

			// ArmBand Sub
			if ( data.GetValue<string>("EquipArmBandSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipArmBandSubSlot") )
				_DK_RPG_UMA._Equipment._ArmBandSub.Slot = slot;
			// ArmBand
			if ( data.GetValue<string>("EquipArmBandSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipArmBandSlot") )
				_DK_RPG_UMA._Equipment._ArmBand.Slot = slot;
			// ArmBand Cover
			if ( data.GetValue<string>("EquipArmBandCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipArmBandCoverSlot") )
				_DK_RPG_UMA._Equipment._ArmBandCover.Slot = slot;

			// Wrist Sub
			if ( data.GetValue<string>("EquipWristSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipWristSubSlot") )
				_DK_RPG_UMA._Equipment._WristSub.Slot = slot;
			// Wrist
			if ( data.GetValue<string>("EquipWristSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipWristSlot") )
				_DK_RPG_UMA._Equipment._Wrist.Slot = slot;
			// Wrist Cover
			if ( data.GetValue<string>("EquipWristCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipWristCoverSlot") )
				_DK_RPG_UMA._Equipment._WristCover.Slot = slot;

			// Hands Sub
			if ( data.GetValue<string>("EquipHandsSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipHandsSubSlot") )
				_DK_RPG_UMA._Equipment._HandsSub.Slot = slot;
			// Hands
			if ( data.GetValue<string>("EquipHandsSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipHandsSlot") )
				_DK_RPG_UMA._Equipment._Hands.Slot = slot;
			// Hands Cover
			if ( data.GetValue<string>("EquipHandsCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipHandsCoverSlot") )
				_DK_RPG_UMA._Equipment._HandsCover.Slot = slot;

			// Belt Sub
			if ( data.GetValue<string>("EquipBeltSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipBeltSubSlot") )
				_DK_RPG_UMA._Equipment._BeltSub.Slot = slot;
			// Belt
			if ( data.GetValue<string>("EquipBeltSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipBeltSlot") )
				_DK_RPG_UMA._Equipment._Belt.Slot = slot;
			// Belt Cover
			if ( data.GetValue<string>("EquipBeltCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipBeltCoverSlot") )
				_DK_RPG_UMA._Equipment._BeltCover.Slot = slot;

			// Legs Sub
			if ( data.GetValue<string>("EquipLegsSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipLegsSubSlot") )
				_DK_RPG_UMA._Equipment._LegsSub.Slot = slot;
			// Legs
			if ( data.GetValue<string>("EquipLegsSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipLegsSlot") )
				_DK_RPG_UMA._Equipment._Legs.Slot = slot;
			// Legs Cover
			if ( data.GetValue<string>("EquipLegsCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipLegsCoverSlot") )
				_DK_RPG_UMA._Equipment._LegsCover.Slot = slot;

			// LegBand Sub
			if ( data.GetValue<string>("EquipLegBandSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipLegBandSubSlot") )
				_DK_RPG_UMA._Equipment._LegBandSub.Slot = slot;
			// LegBand
			if ( data.GetValue<string>("EquipLegBandSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipLegBandSlot") )
				_DK_RPG_UMA._Equipment._LegBand.Slot = slot;
			// LegBand Cover
			if ( data.GetValue<string>("EquipLegBandCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipLegBandCoverSlot") )
				_DK_RPG_UMA._Equipment._LegBandCover.Slot = slot;

			// Feet Sub
			if ( data.GetValue<string>("EquipFeetSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipFeetSubSlot") )
				_DK_RPG_UMA._Equipment._FeetSub.Slot = slot;
			// Feet
			if ( data.GetValue<string>("EquipFeetSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipFeetSlot") )
				_DK_RPG_UMA._Equipment._Feet.Slot = slot;
			// Feet Cover
			if ( data.GetValue<string>("EquipFeetCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipFeetCoverSlot") )
				_DK_RPG_UMA._Equipment._FeetCover.Slot = slot;


			// Collar Sub
			if ( data.GetValue<string>("EquipCollarSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipCollarSubSlot") )
				_DK_RPG_UMA._Equipment._CollarSub.Slot = slot;
			// Collar
			if ( data.GetValue<string>("EquipCollarSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipCollarSlot") )
				_DK_RPG_UMA._Equipment._Collar.Slot = slot;
			// Collar Cover
			if ( data.GetValue<string>("EquipCollarCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipCollarCoverSlot") )
				_DK_RPG_UMA._Equipment._CollarCover.Slot = slot;

			// RingLeft
			if ( data.GetValue<string>("EquipRingLeftSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipRingLeftSlot") )
				_DK_RPG_UMA._Equipment._RingLeft.Slot = slot;
			// RingRight
			if ( data.GetValue<string>("EquipRingRightSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipRingRightSlot") )
				_DK_RPG_UMA._Equipment._RingRight.Slot = slot;

			// Cloak
			if ( data.GetValue<string>("EquipCloakSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipCloakSlot") )
				_DK_RPG_UMA._Equipment._Cloak.Slot = slot;

			// Backpack Sub
			if ( data.GetValue<string>("EquipBackpackSubSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipBackpackSubSlot") )
				_DK_RPG_UMA._Equipment._BackpackSub.Slot = slot;
			// Backpack
			if ( data.GetValue<string>("EquipBackpackSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipBackpackSlot") )
				_DK_RPG_UMA._Equipment._Backpack.Slot = slot;
			// Backpack Cover
			if ( data.GetValue<string>("EquipBackpackCoverSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipBackpackCoverSlot") )
				_DK_RPG_UMA._Equipment._BackpackCover.Slot = slot;

			// LeftHand
			if ( data.GetValue<string>("EquipLeftHandSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipLeftHandSlot") )
				_DK_RPG_UMA._Equipment._LeftHand.Slot = slot;
			// RightHand
			if ( data.GetValue<string>("EquipRightHandSlot") != "NA" 
			         && slot.name == data.GetValue<string>("EquipRightHandSlot") )
				_DK_RPG_UMA._Equipment._RightHand.Slot = slot;
			#endregion Equipment
		}

		#region Head			
		if ( data.GetValue<string>("FaceHeadSlot") == "NA" ) _DK_RPG_UMA._Avatar._Face._Head.Slot = null;
		if ( data.GetValue<string>("BeardSlotOnlySlot") == "NA" ) _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Slot = null;
		if ( data.GetValue<string>("FaceEyesSlot") == "NA" ) _DK_RPG_UMA._Avatar._Face._Eyes.Slot = null;
		if ( data.GetValue<string>("FaceEyeLashSlot") == "NA" ) _DK_RPG_UMA._Avatar._Face._EyeLash.Slot = null;
		if ( data.GetValue<string>("FaceEyeLidsSlot") == "NA" ) _DK_RPG_UMA._Avatar._Face._EyeLids.Slot = null;
		if ( data.GetValue<string>("FaceEarsSlot") == "NA" ) _DK_RPG_UMA._Avatar._Face._Ears.Slot = null;
		if ( data.GetValue<string>("FaceNoseSlot") == "NA" ) _DK_RPG_UMA._Avatar._Face._Nose.Slot = null;
		if ( data.GetValue<string>("FaceInnerMouthSlot") == "NA" ) _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Slot = null;
		if ( data.GetValue<string>("FaceMouthSlot") == "NA" ) _DK_RPG_UMA._Avatar._Face._Mouth.Slot = null;
		if ( data.GetValue<string>("HairSlotOnlySlot") == "NA" ) _DK_RPG_UMA._Avatar._Hair._SlotOnly.Slot = null;
		if ( data.GetValue<string>("HairModuleSlot") == "NA" ) _DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Slot = null;
		#endregion Head
		
		#region Body
		if ( data.GetValue<string>("BodyTorsoSlot") == "NA" ) _DK_RPG_UMA._Avatar._Body._Torso.Slot = null;
		if ( data.GetValue<string>("BodyHandsSlot") == "NA" ) _DK_RPG_UMA._Avatar._Body._Hands.Slot = null;
		if ( data.GetValue<string>("BodyLegsSlot") == "NA" ) _DK_RPG_UMA._Avatar._Body._Legs.Slot = null;
		if ( data.GetValue<string>("BodyFeetSlot") == "NA" ) _DK_RPG_UMA._Avatar._Body._Feet.Slot = null;
		if ( data.GetValue<string>("BodyWingsSlot") == "NA" ) _DK_RPG_UMA._Avatar._Body._Wings.Slot = null;
		if ( data.GetValue<string>("BodyTailSlot") == "NA" ) _DK_RPG_UMA._Avatar._Body._Tail.Slot = null;
		if ( data.GetValue<string>("BodyUnderwearSlot") == "NA" ) _DK_RPG_UMA._Avatar._Body._Underwear.Slot = null;
		#endregion Body
		
		#region Equipment
		if ( data.GetValue<string>("EquipHeadSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._HeadSub.Slot = null;
		if ( data.GetValue<string>("EquipShoulderSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._ShoulderSub.Slot = null;
		//if ( data.GetValue<string>("EquipTorsoSubSlot") == "NA"  ) _DK_RPG_UMA._Equipment._TorsoSub.Slot = null;
		if ( data.GetValue<string>("EquipHandsSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._HandsSub.Slot = null;
		if ( data.GetValue<string>("EquipLegsSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._LegsSub.Slot = null;
		if ( data.GetValue<string>("EquipFeetSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._FeetSub.Slot = null;
		if ( data.GetValue<string>("EquipArmBandSubSlot") == "NA"  ) _DK_RPG_UMA._Equipment._ArmBandSub.Slot = null;
		if ( data.GetValue<string>("EquipWristSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._WristSub.Slot = null;
		if ( data.GetValue<string>("EquipLegBandSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._LegBandSub.Slot = null;
		if ( data.GetValue<string>("EquipCollarSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._CollarSub.Slot = null;
		if ( data.GetValue<string>("EquipBeltSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._BeltSub.Slot = null;
		if ( data.GetValue<string>("EquipBackpackSubSlot") == "NA" ) _DK_RPG_UMA._Equipment._BackpackSub.Slot = null;

		if ( data.GetValue<string>("EquipHeadSlot") == "NA" ) _DK_RPG_UMA._Equipment._Head.Slot = null;
		if ( data.GetValue<string>("EquipShoulderSlot") == "NA" ) _DK_RPG_UMA._Equipment._Shoulder.Slot = null;
		if ( data.GetValue<string>("EquipTorsoSlot") == "NA"  ) _DK_RPG_UMA._Equipment._Torso.Slot = null;
		if ( data.GetValue<string>("EquipHandsSlot") == "NA" ) _DK_RPG_UMA._Equipment._Hands.Slot = null;
		if ( data.GetValue<string>("EquipLegsSlot") == "NA" ) _DK_RPG_UMA._Equipment._Legs.Slot = null;
		if ( data.GetValue<string>("EquipFeetSlot") == "NA" ) _DK_RPG_UMA._Equipment._Feet.Slot = null;
		if ( data.GetValue<string>("EquipArmBandSlot") == "NA"  ) _DK_RPG_UMA._Equipment._ArmBand.Slot = null;
		if ( data.GetValue<string>("EquipWristSlot") == "NA" ) _DK_RPG_UMA._Equipment._Wrist.Slot = null;
		if ( data.GetValue<string>("EquipLegBandSlot") == "NA" ) _DK_RPG_UMA._Equipment._LegBand.Slot = null;
		if ( data.GetValue<string>("EquipCollarSlot") == "NA" ) _DK_RPG_UMA._Equipment._Collar.Slot = null;
		if ( data.GetValue<string>("EquipRingLeftSlot") == "NA" ) _DK_RPG_UMA._Equipment._RingLeft.Slot = null;
		if ( data.GetValue<string>("EquipRingRightSlot") == "NA" ) _DK_RPG_UMA._Equipment._RingRight.Slot = null;
		if ( data.GetValue<string>("EquipBeltSlot") == "NA" ) _DK_RPG_UMA._Equipment._Belt.Slot = null;
		if ( data.GetValue<string>("EquipCloakSlot") == "NA" ) _DK_RPG_UMA._Equipment._Cloak.Slot = null;
		if ( data.GetValue<string>("EquipBackpackSlot") == "NA" ) _DK_RPG_UMA._Equipment._Backpack.Slot = null;
		if ( data.GetValue<string>("EquipLeftHandSlot") == "NA" ) _DK_RPG_UMA._Equipment._LeftHand.Slot = null;
		if ( data.GetValue<string>("EquipRightHandSlot") == "NA" ) _DK_RPG_UMA._Equipment._RightHand.Slot = null;

		if ( data.GetValue<string>("EquipHeadCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._HeadCover.Slot = null;
		if ( data.GetValue<string>("EquipShoulderCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._ShoulderCover.Slot = null;
		if ( data.GetValue<string>("EquipTorsoCoverSlot") == "NA"  ) _DK_RPG_UMA._Equipment._TorsoCover.Slot = null;
		if ( data.GetValue<string>("EquipHandsCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._HandsCover.Slot = null;
		if ( data.GetValue<string>("EquipLegsCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._LegsCover.Slot = null;
		if ( data.GetValue<string>("EquipFeetCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._FeetCover.Slot = null;
		if ( data.GetValue<string>("EquipArmBandCoverSlot") == "NA"  ) _DK_RPG_UMA._Equipment._ArmBandCover.Slot = null;
		if ( data.GetValue<string>("EquipWristCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._WristCover.Slot = null;
		if ( data.GetValue<string>("EquipLegBandCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._LegBandCover.Slot = null;
		if ( data.GetValue<string>("EquipCollarCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._CollarCover.Slot = null;
		if ( data.GetValue<string>("EquipBeltCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._BeltCover.Slot = null;
		if ( data.GetValue<string>("EquipBackpackCoverSlot") == "NA" ) _DK_RPG_UMA._Equipment._BackpackCover.Slot = null;

		#endregion Equipment

		#endregion Slots re assign


		#region Overlays re assign
		foreach ( DKOverlayData Overlay in _DK_UMACrowd.overlayLibrary.overlayElementList ){
			#region Head			
			if ( Overlay.name == data.GetValue<string>("FaceHeadOverlay") ){
				_DK_RPG_UMA._Avatar._Face._Head.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Face._Head.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// head tatoo
			if ( data.GetValue<string>("FaceHeadTatoo") != "NA" 
			         && Overlay.name == data.GetValue<string>("FaceHeadTatoo") ){
				_DK_RPG_UMA._Avatar._Face._Head.Tattoo = Overlay;
				_DK_RPG_UMA._Avatar._Face._Head.TattooColor = data.GetValue<Color>("FaceHeadTatooColor");
				if ( data.GetValue<string>("FaceHeadTatooColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Avatar._Face._Head.Tattoo.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("FaceHeadTatooColorPreset") ) 
							_DK_RPG_UMA._Avatar._Face._Head.TatooColorPreset = Preset;
			}
			// head Makeup
			if ( data.GetValue<string>("FaceHeadMakeup") != "NA" 
			         && Overlay.name == data.GetValue<string>("FaceHeadMakeup") ){
				_DK_RPG_UMA._Avatar._Face._Head.Makeup = Overlay;
				_DK_RPG_UMA._Avatar._Face._Head.MakeupColor = data.GetValue<Color>("FaceHeadMakeupColor");
				if ( data.GetValue<string>("FaceHeadMakeupColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Avatar._Face._Head.Makeup.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("FaceHeadMakeupColorPreset") ) 
							_DK_RPG_UMA._Avatar._Face._Head.MakeupColorPreset = Preset;
			}
			// Beard SlotOnly
			if ( data.GetValue<string>("BeardSlotOnlyOverlay") != "NA" 
			         && Overlay.name == data.GetValue<string>("BeardSlotOnlyOverlay") ){
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Color = data.GetValue<Color>("BeardSlotOnlyColor");
			}
			// beard 1
			if ( data.GetValue<string>("Beard1OverlayOnly") != "NA" 
			         && Overlay.name == data.GetValue<string>("Beard1OverlayOnly") ){
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1 = Overlay;
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1Color = data.GetValue<Color>("Beard1OverlayOnlyColor");
			}
			// beard 2
			if ( data.GetValue<string>("Beard2OverlayOnly") != "NA" 
			         && Overlay.name == data.GetValue<string>("Beard2OverlayOnly") ){
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2 = Overlay;
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2Color = data.GetValue<Color>("Beard2OverlayOnlyColor");
			}
			// beard 3
			if ( data.GetValue<string>("Beard3OverlayOnly") != "NA" 
			         && Overlay.name == data.GetValue<string>("Beard3OverlayOnly") ){
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3 = Overlay;
				_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3Color = data.GetValue<Color>("Beard3OverlayOnlyColor");
			}
			// Eyes
			if ( Overlay.name == data.GetValue<string>("FaceEyesOverlay") ){
				_DK_RPG_UMA._Avatar._Face._Eyes.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Face._Eyes.Color = data.GetValue<Color>("FaceEyesColor");
			}
			// Eyes adjust
			if ( Overlay.name == data.GetValue<string>("FaceEyesAdjustOverlay") ){
				_DK_RPG_UMA._Avatar._Face._Eyes.Adjust = Overlay;
				_DK_RPG_UMA._Avatar._Face._Eyes.AdjustColor = data.GetValue<Color>("FaceEyesAdjustColor");
			}
			// EyeBrows
			if ( Overlay.name == data.GetValue<string>("EyeBrows") ){
				_DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows = Overlay;
				_DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrowsColor = data.GetValue<Color>("EyeBrowsColor");
			}
			// EyeLash
			if ( data.GetValue<string>("FaceEyeLashOverlay") != "NA" 
			         && Overlay.name == data.GetValue<string>("FaceEyeLashOverlay") ){
				_DK_RPG_UMA._Avatar._Face._EyeLash.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Face._EyeLash.Color = data.GetValue<Color>("FaceEyeLashColor");
			}
			// Eyelids
			if ( Overlay.name == data.GetValue<string>("FaceEyeLidsOverlay") ){
				_DK_RPG_UMA._Avatar._Face._EyeLids.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Face._EyeLids.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// Ears
			if ( Overlay.name == data.GetValue<string>("FaceEarsOverlay") ){
				_DK_RPG_UMA._Avatar._Face._Ears.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Face._Ears.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// Nose
			if ( Overlay.name == data.GetValue<string>("FaceNoseOverlay") ){
				_DK_RPG_UMA._Avatar._Face._Nose.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Face._Nose.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// Mouth
			if ( Overlay.name == data.GetValue<string>("FaceMouthOverlay") ){
				_DK_RPG_UMA._Avatar._Face._Mouth.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Face._Mouth.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// InnerMouth
			if ( Overlay.name == data.GetValue<string>("FaceInnerMouthOverlay") ){
				_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.color = data.GetValue<Color>("FaceInnerMouthColor");
			}
			// lips
			if ( data.GetValue<string>("FaceLipsOverlay") != "NA" 
			         && Overlay.name == data.GetValue<string>("FaceLipsOverlay") ){
				_DK_RPG_UMA._Avatar._Face._Mouth.Lips = Overlay;
				_DK_RPG_UMA._Avatar._Face._Mouth.LipsColor = data.GetValue<Color>("FaceLipsColor");
			}
			// Hair
			// Hair Overlay only
			if ( data.GetValue<string>("HairOverlayOnlyOverlay") != "NA" 
			         && Overlay.name == data.GetValue<string>("HairOverlayOnlyOverlay") ){
				_DK_RPG_UMA._Avatar._Hair._OverlayOnly.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Hair._OverlayOnly.Color = data.GetValue<Color>("HairOverlayOnlyColor");
			}
			// Hair slot only
			if ( data.GetValue<string>("HairSlotOnlyOverlay") != "NA" ){
			    if ( Overlay.name == data.GetValue<string>("HairSlotOnlyOverlay") ){
					_DK_RPG_UMA._Avatar._Hair._SlotOnly.Overlay = Overlay;
					_DK_RPG_UMA._Avatar._Hair._SlotOnly.Color = data.GetValue<Color>("HairSlotOnlyColor");
				}
			}
			// Hair module Overlay
			if ( data.GetValue<string>("HairModuleOverlay") != "NA" 
			         && Overlay.name == data.GetValue<string>("HairModuleOverlay") ){
				_DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Color = data.GetValue<Color>("HairModuleColor");
			}
			#endregion Head

			#region Body	
			// Torso
			if ( Overlay.name == data.GetValue<string>("BodyTorsoOverlay") ){
				_DK_RPG_UMA._Avatar._Body._Torso.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Body._Torso.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// Hands
			if ( Overlay.name == data.GetValue<string>("BodyHandsOverlay") ){
				_DK_RPG_UMA._Avatar._Body._Hands.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Body._Hands.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// Legs
			if ( Overlay.name == data.GetValue<string>("BodyLegsOverlay") ){
				_DK_RPG_UMA._Avatar._Body._Legs.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Body._Legs.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// Feet
			if ( Overlay.name == data.GetValue<string>("BodyFeetOverlay") ){
				_DK_RPG_UMA._Avatar._Body._Feet.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Body._Feet.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// Wings
			if ( data.GetValue<string>("BodyWingsOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("BodyWingsOverlay") ){
				_DK_RPG_UMA._Avatar._Body._Wings.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Body._Wings.Color = data.GetValue<Color>("BodyWingsColor");
			}
			// Tail
			if ( data.GetValue<string>("BodyTailOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("BodyTailOverlay") ){
				_DK_RPG_UMA._Avatar._Body._Tail.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Body._Tail.Color = _DK_RPG_UMA._Avatar.SkinColor;
			}
			// Underwear
			if ( data.GetValue<string>("BodyUnderwearOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("BodyUnderwearOverlay") ){
				_DK_RPG_UMA._Avatar._Body._Underwear.Overlay = Overlay;
				_DK_RPG_UMA._Avatar._Body._Underwear.Color = data.GetValue<Color>("BodyUnderwearColor");
			}
			#endregion Body			

			#region Equipment
			// Head Sub
			if ( data.GetValue<string>("EquipHeadSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipHeadSubOverlay") ){
				_DK_RPG_UMA._Equipment._HeadSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._HeadSub.Color = data.GetValue<Color>("EquipHeadSubColor");
				if ( data.GetValue<string>("EquipHeadSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._HeadSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipHeadSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._HeadSub.ColorPreset = Preset;
			}
			// Head
			if ( data.GetValue<string>("EquipHeadOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipHeadOverlay") ){
				_DK_RPG_UMA._Equipment._Head.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Head.Color = data.GetValue<Color>("EquipHeadColor");
				if ( data.GetValue<string>("EquipHeadColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Head.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipHeadColorPreset") ) 
							_DK_RPG_UMA._Equipment._Head.ColorPreset = Preset;
			}
			// Head Cover
			if ( data.GetValue<string>("EquipHeadCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipHeadCoverOverlay") ){
				_DK_RPG_UMA._Equipment._HeadCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._HeadCover.Color = data.GetValue<Color>("EquipHeadCoverColor");
				if ( data.GetValue<string>("EquipHeadCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._HeadCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipHeadCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._HeadCover.ColorPreset = Preset;
			}

			// Shoulder Sub
			if ( data.GetValue<string>("EquipShoulderSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipShoulderSubOverlay") ){
				_DK_RPG_UMA._Equipment._ShoulderSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._ShoulderSub.Color = data.GetValue<Color>("EquipShoulderSubColor");
				if ( data.GetValue<string>("EquipShoulderSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._ShoulderSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipShoulderSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset = Preset;
			}
			// Shoulder
			if ( data.GetValue<string>("EquipShoulderOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipShoulderOverlay") ){
				_DK_RPG_UMA._Equipment._Shoulder.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Shoulder.Color = data.GetValue<Color>("EquipShoulderColor");
				if ( data.GetValue<string>("EquipShoulderColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Shoulder.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipShoulderColorPreset") ) 
							_DK_RPG_UMA._Equipment._Shoulder.ColorPreset = Preset;
			}
			// Shoulder Cover
			if ( data.GetValue<string>("EquipShoulderCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipShoulderCoverOverlay") ){
				_DK_RPG_UMA._Equipment._ShoulderCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._ShoulderCover.Color = data.GetValue<Color>("EquipShoulderCoverColor");
				if ( data.GetValue<string>("EquipShoulderCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._ShoulderCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipShoulderCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset = Preset;
			}

			// Torso Sub
			if ( data.GetValue<string>("EquipTorsoSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipTorsoSubOverlay") ){
				_DK_RPG_UMA._Equipment._TorsoSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._TorsoSub.Color = data.GetValue<Color>("EquipTorsoSubColor");
				if ( data.GetValue<string>("EquipTorsoSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._TorsoSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipTorsoSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._TorsoSub.ColorPreset = Preset;
			}
			// Torso
			if ( data.GetValue<string>("EquipTorsoOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipTorsoOverlay") ){
				_DK_RPG_UMA._Equipment._Torso.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Torso.Color = data.GetValue<Color>("EquipTorsoColor");
				if ( data.GetValue<string>("EquipTorsoColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Torso.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipTorsoColorPreset") ) 
							_DK_RPG_UMA._Equipment._Torso.ColorPreset = Preset;
			}
			// Torso Cover
			if ( data.GetValue<string>("EquipTorsoCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipTorsoCoverOverlay") ){
				_DK_RPG_UMA._Equipment._TorsoCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._TorsoCover.Color = data.GetValue<Color>("EquipTorsoCoverColor");
				if ( data.GetValue<string>("EquipTorsoCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._TorsoCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipTorsoCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._TorsoCover.ColorPreset = Preset;
			}

			// ArmBand Sub
			if ( data.GetValue<string>("EquipArmBandSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipArmBandSubOverlay") ){
				_DK_RPG_UMA._Equipment._ArmBandSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._ArmBandSub.Color = data.GetValue<Color>("EquipArmBandSubColor");
				if ( data.GetValue<string>("EquipArmBandSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._ArmBandSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipArmBandSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = Preset;
			}
			// ArmBand
			if ( data.GetValue<string>("EquipArmBandOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipArmBandOverlay") ){
				_DK_RPG_UMA._Equipment._ArmBand.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._ArmBand.Color = data.GetValue<Color>("EquipArmBandColor");
				if ( data.GetValue<string>("EquipArmBandColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._ArmBand.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipArmBandColorPreset") ) 
							_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = Preset;
			}
			// ArmBand Cover
			if ( data.GetValue<string>("EquipArmBandCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipArmBandCoverOverlay") ){
				_DK_RPG_UMA._Equipment._ArmBandCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._ArmBandCover.Color = data.GetValue<Color>("EquipArmBandCoverColor");
				if ( data.GetValue<string>("EquipArmBandCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._ArmBandCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipArmBandCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = Preset;
			}

			// Wrist Sub
			if ( data.GetValue<string>("EquipWristSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipWristSubOverlay") ){
				_DK_RPG_UMA._Equipment._WristSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._WristSub.Color = data.GetValue<Color>("EquipWristSubColor");
				if ( data.GetValue<string>("EquipWristSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._WristSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipWristSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._WristSub.ColorPreset = Preset;
			}
			// Wrist
			if ( data.GetValue<string>("EquipWristOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipWristOverlay") ){
				_DK_RPG_UMA._Equipment._Wrist.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Wrist.Color = data.GetValue<Color>("EquipWristColor");
				if ( data.GetValue<string>("EquipWristColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Wrist.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipWristColorPreset") ) 
							_DK_RPG_UMA._Equipment._Wrist.ColorPreset = Preset;
			}
			// Wrist Cover
			if ( data.GetValue<string>("EquipWristCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipWristCoverOverlay") ){
				_DK_RPG_UMA._Equipment._WristCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._WristCover.Color = data.GetValue<Color>("EquipWristCoverColor");
				if ( data.GetValue<string>("EquipWristCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._WristCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipWristCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._WristCover.ColorPreset = Preset;
			}

			// Hands Sub
			if ( data.GetValue<string>("EquipHandsSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipHandsSubOverlay") ){
				_DK_RPG_UMA._Equipment._HandsSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._HandsSub.Color = data.GetValue<Color>("EquipHandsSubColor");
				if ( data.GetValue<string>("EquipHandsSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._HandsSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipHandsSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._HandsSub.ColorPreset = Preset;
			}
			// Hands
			if ( data.GetValue<string>("EquipHandsOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipHandsOverlay") ){
				_DK_RPG_UMA._Equipment._Hands.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Hands.Color = data.GetValue<Color>("EquipHandsColor");
				if ( data.GetValue<string>("EquipHandsColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Hands.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipHandsColorPreset") ) 
							_DK_RPG_UMA._Equipment._Hands.ColorPreset = Preset;
			}
			// Hands Cover
			if ( data.GetValue<string>("EquipHandsCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipHandsCoverOverlay") ){
				_DK_RPG_UMA._Equipment._HandsCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._HandsCover.Color = data.GetValue<Color>("EquipHandsCoverColor");
				if ( data.GetValue<string>("EquipHandsCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._HandsCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipHandsCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._HandsCover.ColorPreset = Preset;
			}

			// Legs Sub
			if ( data.GetValue<string>("EquipLegsSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipLegsSubOverlay") ){
				_DK_RPG_UMA._Equipment._LegsSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._LegsSub.Color = data.GetValue<Color>("EquipLegsSubColor");
				if ( data.GetValue<string>("EquipLegsSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._LegsSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipLegsSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._LegsSub.ColorPreset = Preset;
			}
			// Legs
			if ( data.GetValue<string>("EquipLegsOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipLegsOverlay") ){
				_DK_RPG_UMA._Equipment._Legs.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Legs.Color = data.GetValue<Color>("EquipLegsColor");
				if ( data.GetValue<string>("EquipLegsColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Legs.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipLegsColorPreset") ) 
							_DK_RPG_UMA._Equipment._Legs.ColorPreset = Preset;
			}
			// Legs Cover
			if ( data.GetValue<string>("EquipLegsCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipLegsCoverOverlay") ){
				_DK_RPG_UMA._Equipment._LegsCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._LegsCover.Color = data.GetValue<Color>("EquipLegsCoverColor");
				if ( data.GetValue<string>("EquipLegsCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._LegsCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipLegsCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._LegsCover.ColorPreset = Preset;
			}


			// LegBand Sub
			if ( data.GetValue<string>("EquipLegBandSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipLegBandSubOverlay") ){
				_DK_RPG_UMA._Equipment._LegBandSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._LegBandSub.Color = data.GetValue<Color>("EquipLegBandSubColor");
				if ( data.GetValue<string>("EquipLegBandSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._LegBandSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipLegBandSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = Preset;
			}
			// LegBand
			if ( data.GetValue<string>("EquipLegBandOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipLegBandOverlay") ){
				_DK_RPG_UMA._Equipment._LegBand.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._LegBand.Color = data.GetValue<Color>("EquipLegBandColor");
				if ( data.GetValue<string>("EquipLegBandColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._LegBand.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipLegBandColorPreset") ) 
							_DK_RPG_UMA._Equipment._LegBand.ColorPreset = Preset;
			}
			// LegBand Cover
			if ( data.GetValue<string>("EquipLegBandCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipLegBandCoverOverlay") ){
				_DK_RPG_UMA._Equipment._LegBandCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._LegBandCover.Color = data.GetValue<Color>("EquipLegBandCoverColor");
				if ( data.GetValue<string>("EquipLegBandCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._LegBandCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipLegBandCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = Preset;
			}

			// Feet Sub
			if ( data.GetValue<string>("EquipFeetSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipFeetSubOverlay") ){
				_DK_RPG_UMA._Equipment._FeetSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._FeetSub.Color = data.GetValue<Color>("EquipFeetSubColor");
				if ( data.GetValue<string>("EquipFeetSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._FeetSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipFeetSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._FeetSub.ColorPreset = Preset;
			}
			// Feet
			if ( data.GetValue<string>("EquipFeetOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipFeetOverlay") ){
				_DK_RPG_UMA._Equipment._Feet.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Feet.Color = data.GetValue<Color>("EquipFeetColor");
				if ( data.GetValue<string>("EquipFeetColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Feet.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipFeetColorPreset") ) 
							_DK_RPG_UMA._Equipment._Feet.ColorPreset = Preset;
			}
			// Feet Cover
			if ( data.GetValue<string>("EquipFeetCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipFeetCoverOverlay") ){
				_DK_RPG_UMA._Equipment._FeetCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._FeetCover.Color = data.GetValue<Color>("EquipFeetCoverColor");
				if ( data.GetValue<string>("EquipFeetCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._FeetCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipFeetCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._FeetCover.ColorPreset = Preset;
			}

			// Collar Sub
			if ( data.GetValue<string>("EquipCollarSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipCollarSubOverlay") ){
				_DK_RPG_UMA._Equipment._CollarSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._CollarSub.Color = data.GetValue<Color>("EquipCollarSubColor");
				if ( data.GetValue<string>("EquipCollarSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._CollarSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipCollarSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._CollarSub.ColorPreset = Preset;
			}
			// Collar
			if ( data.GetValue<string>("EquipCollarOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipCollarOverlay") ){
				_DK_RPG_UMA._Equipment._Collar.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Collar.Color = data.GetValue<Color>("EquipCollarColor");
				if ( data.GetValue<string>("EquipCollarColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Collar.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipCollarColorPreset") ) 
							_DK_RPG_UMA._Equipment._Collar.ColorPreset = Preset;
			}
			// Collar Cover
			if ( data.GetValue<string>("EquipCollarCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipCollarCoverOverlay") ){
				_DK_RPG_UMA._Equipment._CollarCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._CollarCover.Color = data.GetValue<Color>("EquipCollarCoverColor");
				if ( data.GetValue<string>("EquipCollarCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._CollarCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipCollarCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._CollarCover.ColorPreset = Preset;
			}

			// RingLeft
			if ( data.GetValue<string>("EquipRingLeftOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipRingLeftOverlay") ){
				_DK_RPG_UMA._Equipment._RingLeft.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._RingLeft.Color = data.GetValue<Color>("EquipRingLeftColor");
				if ( data.GetValue<string>("EquipRingLeftColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._RingLeft.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipRingLeftColorPreset") ) 
							_DK_RPG_UMA._Equipment._RingLeft.ColorPreset = Preset;
			}
			// RingRight
			if ( data.GetValue<string>("EquipRingRightOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipRingRightOverlay") ){
				_DK_RPG_UMA._Equipment._RingRight.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._RingRight.Color = data.GetValue<Color>("EquipRingRightColor");
				if ( data.GetValue<string>("EquipRingRightColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._RingRight.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipRingRightColorPreset") ) 
							_DK_RPG_UMA._Equipment._RingRight.ColorPreset = Preset;
			}

			// Belt Sub
			if ( data.GetValue<string>("EquipBeltSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipBeltSubOverlay") ){
				_DK_RPG_UMA._Equipment._BeltSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._BeltSub.Color = data.GetValue<Color>("EquipBeltSubColor");
				if ( data.GetValue<string>("EquipBeltSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._BeltSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipBeltSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._BeltSub.ColorPreset = Preset;
			}
			// Belt
			if ( data.GetValue<string>("EquipBeltOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipBeltOverlay") ){
				_DK_RPG_UMA._Equipment._Belt.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Belt.Color = data.GetValue<Color>("EquipBeltColor");
				if ( data.GetValue<string>("EquipBeltColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Belt.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipBeltColorPreset") ) 
							_DK_RPG_UMA._Equipment._Belt.ColorPreset = Preset;
			}
			// Belt Cover
			if ( data.GetValue<string>("EquipBeltCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipBeltCoverOverlay") ){
				_DK_RPG_UMA._Equipment._BeltCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._BeltCover.Color = data.GetValue<Color>("EquipBeltCoverColor");
				if ( data.GetValue<string>("EquipBeltCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._BeltCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipBeltCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._BeltCover.ColorPreset = Preset;
			}

			// Cloak
			if ( data.GetValue<string>("EquipCloakOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipCloakOverlay") ){
				_DK_RPG_UMA._Equipment._Cloak.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Cloak.Color = data.GetValue<Color>("EquipCloakColor");
				if ( data.GetValue<string>("EquipCloakColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Cloak.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipCloakColorPreset") ) 
							_DK_RPG_UMA._Equipment._Cloak.ColorPreset = Preset;
			}

			// Backpack Sub
			if ( data.GetValue<string>("EquipBackpackSubOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipBackpackSubOverlay") ){
				_DK_RPG_UMA._Equipment._BackpackSub.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._BackpackSub.Color = data.GetValue<Color>("EquipBackpackSubColor");
				if ( data.GetValue<string>("EquipBackpackSubColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._BackpackSub.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipBackpackSubColorPreset") ) 
							_DK_RPG_UMA._Equipment._BackpackSub.ColorPreset = Preset;
			}
			// Backpack
			if ( data.GetValue<string>("EquipBackpackOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipBackpackOverlay") ){
				_DK_RPG_UMA._Equipment._Backpack.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._Backpack.Color = data.GetValue<Color>("EquipBackpackColor");
				if ( data.GetValue<string>("EquipBackpackColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._Backpack.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipBackpackColorPreset") ) 
							_DK_RPG_UMA._Equipment._Backpack.ColorPreset = Preset;
			}
			// Backpack Cover
			if ( data.GetValue<string>("EquipBackpackCoverOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipBackpackCoverOverlay") ){
				_DK_RPG_UMA._Equipment._BackpackCover.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._BackpackCover.Color = data.GetValue<Color>("EquipBackpackCoverColor");
				if ( data.GetValue<string>("EquipBackpackCoverColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._BackpackCover.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipBackpackCoverColorPreset") ) 
							_DK_RPG_UMA._Equipment._BackpackCover.ColorPreset = Preset;
			}

			// LeftHand
			if ( data.GetValue<string>("EquipLeftHandOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipLeftHandOverlay") ){
				_DK_RPG_UMA._Equipment._LeftHand.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._LeftHand.Color = data.GetValue<Color>("EquipLeftHandColor");
				if ( data.GetValue<string>("EquipLeftHandColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._LeftHand.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipLeftHandColorPreset") ) 
							_DK_RPG_UMA._Equipment._LeftHand.ColorPreset = Preset;
			}
			// RightHand
			if ( data.GetValue<string>("EquipRightHandOverlay") != "NA"
			         && Overlay.name == data.GetValue<string>("EquipRightHandOverlay") ){
				_DK_RPG_UMA._Equipment._RightHand.Overlay = Overlay;
				_DK_RPG_UMA._Equipment._RightHand.Color = data.GetValue<Color>("EquipRightHandColor");
				if ( data.GetValue<string>("EquipRightHandColorPreset") != "NA" ) 
					foreach ( ColorPresetData Preset in _DK_RPG_UMA._Equipment._RightHand.Overlay.ColorPresets )
						if ( Preset.ColorPresetName == data.GetValue<string>("EquipRightHandColorPreset") ) 
							_DK_RPG_UMA._Equipment._RightHand.ColorPreset = Preset;
			}
			#endregion Equipment
		}

		#region Head			
		if ( data.GetValue<string>("FaceHeadOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._Head.Overlay = null;
		if ( data.GetValue<string>("FaceHeadTatoo") == "NA" ) _DK_RPG_UMA._Avatar._Face._Head.Tattoo = null;
		if ( data.GetValue<string>("FaceHeadTatooColorPreset") == "NA" ) _DK_RPG_UMA._Avatar._Face._Head.TatooColorPreset = null;
		if ( data.GetValue<string>("FaceHeadMakeup") == "NA" ) _DK_RPG_UMA._Avatar._Face._Head.Makeup = null;
		if ( data.GetValue<string>("FaceHeadMakeupColorPreset") == "NA" ) _DK_RPG_UMA._Avatar._Face._Head.MakeupColorPreset = null;
		if ( data.GetValue<string>("BeardSlotOnlyOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Overlay = null;
		if ( data.GetValue<string>("Beard1OverlayOnly") == "NA" ) _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1 = null;
		if ( data.GetValue<string>("Beard2OverlayOnly") == "NA" ) _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2 = null;
		if ( data.GetValue<string>("Beard3OverlayOnly") == "NA" ) _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3 = null;
		if ( data.GetValue<string>("FaceEyesOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._Eyes.Overlay = null;
		if ( data.GetValue<string>("FaceEyesAdjustOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._Eyes.Adjust = null;
		if ( data.GetValue<string>("EyeBrows") == "NA" ) _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows = null;
		if ( data.GetValue<string>("FaceEyeLashOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._EyeLash.Overlay = null;
		if ( data.GetValue<string>("FaceEyeLidsOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._EyeLids.Overlay = null;
		if ( data.GetValue<string>("FaceEarsOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._Ears.Overlay = null;
		if ( data.GetValue<string>("FaceNoseOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._Nose.Overlay = null;
		if ( data.GetValue<string>("FaceMouthOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._Mouth.Overlay = null;
		if ( data.GetValue<string>("FaceLipsOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._Mouth.Lips = null;
		if ( data.GetValue<string>("FaceLipsColorPreset") == "NA" ) _DK_RPG_UMA._Avatar._Face._Mouth.LipsColorPreset = null;
		if ( data.GetValue<string>("FaceInnerMouthOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Overlay = null;

		if ( data.GetValue<string>("HairOverlayOnlyOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Hair._OverlayOnly.Overlay = null;
		if ( data.GetValue<string>("HairSlotOnlyOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Hair._SlotOnly.Overlay = null;
		if ( data.GetValue<string>("HairModuleOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Overlay = null;

		#endregion Head
		
		#region Body	
		if ( data.GetValue<string>("BodyTorsoOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Body._Torso.Overlay = null;
		if ( data.GetValue<string>("BodyHandsOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Body._Hands.Overlay = null;
		if ( data.GetValue<string>("BodyLegsOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Body._Legs.Overlay = null;
		if ( data.GetValue<string>("BodyFeetOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Body._Feet.Overlay = null;
		if ( data.GetValue<string>("BodyWingsOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Body._Wings.Overlay = null;
		if ( data.GetValue<string>("BodyTailOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Body._Tail.Overlay = null;
		if ( data.GetValue<string>("BodyUnderwearOverlay") == "NA" ) _DK_RPG_UMA._Avatar._Body._Underwear.Overlay = null;
		#endregion Body			
		
		#region Equipment
		if ( data.GetValue<string>("EquipHeadSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._HeadSub.Overlay = null;
		if ( data.GetValue<string>("EquipHeadSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._HeadSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipShoulderSubOverlay") == "NA" )_DK_RPG_UMA._Equipment._ShoulderSub.Overlay = null;
		if ( data.GetValue<string>("EquipShoulderSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipTorsoSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._TorsoSub.Overlay = null;
		if ( data.GetValue<string>("EquipTorsoSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._TorsoSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipHandsSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._HandsSub.Overlay = null;
		if ( data.GetValue<string>("EquipHandsSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._HandsSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipLegsSubOverlay") == "NA" )_DK_RPG_UMA._Equipment._LegsSub.Overlay = null;
		if ( data.GetValue<string>("EquipLegsSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._LegsSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipFeetSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._FeetSub.Overlay = null;
		if ( data.GetValue<string>("EquipFeetSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._FeetSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipArmBandSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._ArmBandSub.Overlay = null;
		if ( data.GetValue<string>("EquipArmBandSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipWristSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._WristSub.Overlay = null;
		if ( data.GetValue<string>("EquipWristSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._WristSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipLegBandSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._LegBandSub.Overlay = null;
		if ( data.GetValue<string>("EquipLegBandSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipCollarSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._CollarSub.Overlay = null;
		if ( data.GetValue<string>("EquipCollarSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._CollarSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipBeltSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._BeltSub.Overlay = null;
		if ( data.GetValue<string>("EquipBeltSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._BeltSub.ColorPreset = null;
		if ( data.GetValue<string>("EquipBackpackSubOverlay") == "NA" ) _DK_RPG_UMA._Equipment._BackpackSub.Overlay = null;
		if ( data.GetValue<string>("EquipBackpackSubColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._BackpackSub.ColorPreset = null;

		if ( data.GetValue<string>("EquipHeadOverlay") == "NA" ) _DK_RPG_UMA._Equipment._Head.Overlay = null;
		if ( data.GetValue<string>("EquipHeadColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Head.ColorPreset = null;
		if ( data.GetValue<string>("EquipShoulderOverlay") == "NA" )_DK_RPG_UMA._Equipment._Shoulder.Overlay = null;
		if ( data.GetValue<string>("EquipShoulderColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Shoulder.ColorPreset = null;
		if ( data.GetValue<string>("EquipTorsoOverlay") == "NA" ) _DK_RPG_UMA._Equipment._Torso.Overlay = null;
		if ( data.GetValue<string>("EquipTorsoColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Torso.ColorPreset = null;
		if ( data.GetValue<string>("EquipHandsOverlay") == "NA" ) _DK_RPG_UMA._Equipment._Hands.Overlay = null;
		if ( data.GetValue<string>("EquipHandsColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Hands.ColorPreset = null;
		if ( data.GetValue<string>("EquipLegsOverlay") == "NA" )_DK_RPG_UMA._Equipment._Legs.Overlay = null;
		if ( data.GetValue<string>("EquipLegsColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Legs.ColorPreset = null;
		if ( data.GetValue<string>("EquipFeetOverlay") == "NA" ) _DK_RPG_UMA._Equipment._Feet.Overlay = null;
		if ( data.GetValue<string>("EquipFeetColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Feet.ColorPreset = null;
		if ( data.GetValue<string>("EquipArmBandOverlay") == "NA" ) _DK_RPG_UMA._Equipment._ArmBand.Overlay = null;
		if ( data.GetValue<string>("EquipArmBandColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._ArmBand.ColorPreset = null;
		if ( data.GetValue<string>("EquipWristOverlay") == "NA" ) _DK_RPG_UMA._Equipment._Wrist.Overlay = null;
		if ( data.GetValue<string>("EquipWristColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Wrist.ColorPreset = null;
		if ( data.GetValue<string>("EquipLegBandOverlay") == "NA" ) _DK_RPG_UMA._Equipment._LegBand.Overlay = null;
		if ( data.GetValue<string>("EquipLegBandColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._LegBand.ColorPreset = null;
		if ( data.GetValue<string>("EquipCollarOverlay") == "NA" ) _DK_RPG_UMA._Equipment._Collar.Overlay = null;
		if ( data.GetValue<string>("EquipCollarColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Collar.ColorPreset = null;
		if ( data.GetValue<string>("EquipRingLeftOverlay") == "NA" ) _DK_RPG_UMA._Equipment._RingLeft.Overlay = null;
		if ( data.GetValue<string>("EquipRingLeftColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._RingLeft.ColorPreset = null;
		if ( data.GetValue<string>("EquipRingRightOverlay") == "NA" ) _DK_RPG_UMA._Equipment._RingRight.Overlay = null;
		if ( data.GetValue<string>("EquipRingRightColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._RingRight.ColorPreset = null;
		if ( data.GetValue<string>("EquipBeltOverlay") == "NA" ) _DK_RPG_UMA._Equipment._Belt.Overlay = null;
		if ( data.GetValue<string>("EquipBeltColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Belt.ColorPreset = null;
		if ( data.GetValue<string>("EquipCloakOverlay") == "NA" ) _DK_RPG_UMA._Equipment._Cloak.Overlay = null;
		if ( data.GetValue<string>("EquipCloakColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Cloak.ColorPreset = null;
		if ( data.GetValue<string>("EquipBackpackOverlay") == "NA" ) _DK_RPG_UMA._Equipment._Backpack.Overlay = null;
		if ( data.GetValue<string>("EquipBackpackColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._Backpack.ColorPreset = null;
		if ( data.GetValue<string>("EquipLeftHandOverlay") == "NA" ) _DK_RPG_UMA._Equipment._LeftHand.Overlay = null;
		if ( data.GetValue<string>("EquipLeftHandColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._LeftHand.ColorPreset = null;
		if ( data.GetValue<string>("EquipRightHandOverlay") == "NA" ) _DK_RPG_UMA._Equipment._RightHand.Overlay = null;
		if ( data.GetValue<string>("EquipRightHandColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._RightHand.ColorPreset = null;

		if ( data.GetValue<string>("EquipHeadCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._HeadCover.Overlay = null;
		if ( data.GetValue<string>("EquipHeadCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._HeadCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipShoulderCoverOverlay") == "NA" )_DK_RPG_UMA._Equipment._ShoulderCover.Overlay = null;
		if ( data.GetValue<string>("EquipShoulderCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipTorsoCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._TorsoCover.Overlay = null;
		if ( data.GetValue<string>("EquipTorsoCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._TorsoCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipHandsCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._HandsCover.Overlay = null;
		if ( data.GetValue<string>("EquipHandsCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._HandsCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipLegsCoverOverlay") == "NA" )_DK_RPG_UMA._Equipment._LegsCover.Overlay = null;
		if ( data.GetValue<string>("EquipLegsCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._LegsCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipFeetCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._FeetCover.Overlay = null;
		if ( data.GetValue<string>("EquipFeetCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._FeetCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipArmBandCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._ArmBandCover.Overlay = null;
		if ( data.GetValue<string>("EquipArmBandCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipWristCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._WristCover.Overlay = null;
		if ( data.GetValue<string>("EquipWristCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._WristCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipLegBandCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._LegBandCover.Overlay = null;
		if ( data.GetValue<string>("EquipLegBandCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipCollarCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._CollarCover.Overlay = null;
		if ( data.GetValue<string>("EquipCollarCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._CollarCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipBeltCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._BeltCover.Overlay = null;
		if ( data.GetValue<string>("EquipBeltCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._BeltCover.ColorPreset = null;
		if ( data.GetValue<string>("EquipBackpackCoverOverlay") == "NA" ) _DK_RPG_UMA._Equipment._BackpackCover.Overlay = null;
		if ( data.GetValue<string>("EquipBackpackCoverColorPreset") == "NA" ) _DK_RPG_UMA._Equipment._BackpackCover.ColorPreset = null;

		#endregion Equipment

		#endregion Overlay re assign

		#region DNA
		// Add DNA
		foreach ( DKRaceData.DNAConverterData DNA in _DK_RPG_UMA.GetComponent<DKUMAData>().DNAList2 ) {
			try {
			if ( data.GetValue<float>(DNA.Name+"Value") != null )
				DNA.Value = data.GetValue<float>(DNA.Name+"Value");
			} catch ( System.Collections.Generic.KeyNotFoundException) {
			//	Debug.LogError ("Skipping DNA value '"+DNA.Name+"' : It is not present in your avatar ("+_DK_RPG_UMA.name+"). " + e);
			}
		}
		#endregion DNA

		// launch rebuild
		DK_RPG_ReBuild _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.GetComponent<DK_RPG_ReBuild>();
		if ( _DK_RPG_ReBuild == null ) _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.AddComponent<DK_RPG_ReBuild>();
		DKUMAData _DKUMAData = _DK_RPG_UMA.gameObject.GetComponent<DKUMAData>();
		//	_DK_RPG_ReBuild.RefreshOnly = false;
			_DK_RPG_ReBuild.RefreshOnly = true;
		_DK_RPG_ReBuild.Launch (_DKUMAData, false );


	}
}
