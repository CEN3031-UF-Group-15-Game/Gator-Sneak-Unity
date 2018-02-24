using UnityEngine;
// using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

public class DK_RPG_Avatar_PrepareLists : MonoBehaviour {
	public static List<DKSlotData> SlotList = new List<DKSlotData>();
	public static List<DKOverlayData> OverlayList = new List<DKOverlayData>();
	public static List<DKOverlayData> LinkedOverlayList = new List<DKOverlayData>();


	public static void PrepareLists (DK_RPG_UMA_Generator _DK_RPG_UMA_Generator, 
	                                 DK_RPG_UMA _DK_RPG_UMA, 
	                                 GameObject _Selection, 
	                                 string SelectedType, 
	                                 DKSlotData SelectedSlot,
	                                 DKOverlayData SelectedOverlay ) {
	//	Debug.Log ("Launch prepare lists");
		_DK_RPG_UMA_Generator = GameObject.Find ("DK_UMA").GetComponent<DK_RPG_UMA_Generator>();
		if ( _DK_RPG_UMA == null ) {
			if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = _Selection.GetComponent<DK_RPG_UMA>();
			if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = _Selection.GetComponentInChildren<DK_RPG_UMA>();
			if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = _Selection.GetComponentInParent<DK_RPG_UMA>();
		}
		
		if ( _DK_RPG_UMA && _DK_RPG_UMA.RaceData == null ) {
			DKRaceLibrary races = GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>().raceLibrary;
			foreach ( DKRaceData race in races.raceElementList ) {
				if (race.Race == _DK_RPG_UMA.Race ) _DK_RPG_UMA.RaceData = race;
			}
		}
	//	Debug.Log (_DK_RPG_UMA);
	//	Debug.Log (_DK_RPG_UMA.RaceData);
	//	 Debug.Log (_DK_RPG_UMA.RaceData.raceName);
	//	_DK_RPG_UMA_Generator.PopulateAllLists();
		if ( _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Head.SlotList.Count == 0 ) {
			_DK_RPG_UMA_Generator.PopulateAllLists();
			Debug.Log ( "test list count :"+_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Head.SlotList.Count);
		}

		#region Face Lists
		if ( SelectedType == "Head" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Head.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Head.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "Eyes" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Eyes.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Eyes.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "Eyelash" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._EyeLash.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._EyeLash.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "Eyelids" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._EyeLids.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._EyeLids.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "Ears" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Ears.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Ears.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "Nose" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Nose.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Nose.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "Mouth" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Mouth.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "Eyebrow" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._FaceHair.EyeBrowsList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._FaceHair.EyeBrowsList);
			SlotList.Clear();
		}
		else if ( SelectedType == "Beard" && SelectedSlot ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._FaceHair._BeardSlotOnly.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._FaceHair._BeardSlotOnly.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "Beard" && SelectedSlot == null && SelectedOverlay ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._FaceHair._BeardOverlayOnly.BeardList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._FaceHair._BeardOverlayOnly.BeardList);
			SlotList.Clear();
		}
		else if ( SelectedType == "Beard" && SelectedSlot == null && SelectedOverlay == null ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._FaceHair._BeardOverlayOnly.BeardList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._FaceHair._BeardOverlayOnly.BeardList);
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._FaceHair._BeardSlotOnly.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._FaceHair._BeardSlotOnly.SlotList);
		}
		else if ( SelectedType == "HeadTatoo" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Head.TattooList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Head.TattooList);
			SlotList.Clear();
		}
		else if ( SelectedType == "HeadMakeup" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Head.MakeupList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Head.MakeupList);
			SlotList.Clear();
		}
		else if ( SelectedType == "Hair" /*&& SelectedSlot*/ ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._SlotOnly.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._OverlayOnly.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._SlotOnly.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._OverlayOnly.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._OverlayOnly.OverlayList);
				if (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._SlotOnly.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._OverlayOnly.OverlayList);
				if (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._SlotOnly.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
	/*	else if ( SelectedType == "Hair" && SelectedSlot == null && SelectedOverlay ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._SlotOnly.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._OverlayOnly.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._SlotOnly.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._OverlayOnly.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._OverlayOnly.OverlayList);
				if (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._SlotOnly.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._OverlayOnly.OverlayList);
				if (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._SlotOnly.SlotList.Count == 0 ) SlotList.Clear();
			}
		}*/
		/*	else if ( SelectedType == "Hair" && SelectedSlot == null && SelectedOverlay == null ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._OverlayOnly.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._OverlayOnly.OverlayList);
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._SlotOnly.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._SlotOnly.SlotList);
			Debug.Log (OverlayList.Count);
		}*/
		
		else if ( SelectedType == "HairModule" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Hair._SlotOnly._HairModule.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Hair._SlotOnly._HairModule.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "Lips" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.LipsList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Mouth.LipsList);
			SlotList.Clear();
		}
		else if ( SelectedType == "Innermouth" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth._InnerMouth.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Mouth._InnerMouth.SlotList);
			OverlayList.Clear();
		}
		#endregion Face Lists
		
		#region Body Lists
		else if ( SelectedType == "Torso" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Torso.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Torso.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "TorsoTatoo" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Torso.TattooList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Torso.TattooList);
			SlotList.Clear();
		}
		else if ( SelectedType == "TorsoMakeup" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Torso.MakeupList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Torso.MakeupList);
			SlotList.Clear();
		}
		else if ( SelectedType == "Hands" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Hands.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Hands.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "HandsTatoo" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Hands.TattooList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Hands.TattooList);
			SlotList.Clear();
		}
		else if ( SelectedType == "HandsMakeup" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Hands.MakeupList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Hands.MakeupList);
			SlotList.Clear();
		}
		else if ( SelectedType == "Legs" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Legs.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Legs.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "LegsTatoo" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Legs.TattooList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Legs.TattooList);
			SlotList.Clear();
		}
		else if ( SelectedType == "LegsMakeup" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Legs.MakeupList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Legs.MakeupList);
			SlotList.Clear();
		}
		else if ( SelectedType == "Feet" ) {
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Feet.SlotList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Feet.SlotList);
			OverlayList.Clear();
		}
		else if ( SelectedType == "FeetTatoo" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Feet.TattooList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Feet.TattooList);
			SlotList.Clear();
		}
		else if ( SelectedType == "FeetMakeup" ){
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Feet.MakeupList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Feet.MakeupList);
			SlotList.Clear();
		}
		#endregion Body Lists
		
		#region Wear Lists

		else if ( SelectedType.Contains("HeadWear") /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Head.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Head.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Head.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Head.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Head.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Head.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Head.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Head.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType.Contains("ShoulderWear") /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Shoulder.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Shoulder.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Shoulder.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Shoulder.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Shoulder.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Shoulder.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Shoulder.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Shoulder.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType.Contains("ArmBandWear") /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Armband.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Armband.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Armband.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Armband.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Armband.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Armband.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Armband.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Armband.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType.Contains("WristWear") /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Wrist.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Wrist.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Wrist.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Wrist.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Wrist.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Wrist.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Wrist.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Wrist.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType == "CloakWear" /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Cloak.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Cloak.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Cloak.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Cloak.OverlayList.Count == 0 ) OverlayList.Clear();
			}
		}

		else if ( SelectedType.Contains("TorsoWear") /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Torso.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Torso.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Torso.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Torso.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Torso.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Torso.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Torso.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Torso.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType.Contains("HandsWear")  ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Hands.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Hands.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Hands.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Hands.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Hands.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Hands.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Hands.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Hands.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType.Contains("LegsWear")  ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Legs.SlotList);
			//	if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Legs.OverlayList.Count == 0 ) OverlayList.Clear();
				Debug.Log ("legs wear count"+_DK_RPG_UMA.RaceData._Male._EquipmentData._Legs.OverlayList.Count);
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Legs.SlotList);
			//	if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Legs.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Legs.OverlayOnlyList);
			//	if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Legs.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Legs.OverlayOnlyList);
			//	if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Legs.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType.Contains("LegBandWear")  ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._LegBand.SlotList);
				Debug.Log ("legs wear count"+_DK_RPG_UMA.RaceData._Male._EquipmentData._LegBand.OverlayList.Count);
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._LegBand.SlotList);
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._LegBand.OverlayOnlyList);
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._LegBand.OverlayOnlyList);
			}
		}
		else if ( SelectedType.Contains("FeetWear")  ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Feet.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Feet.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Feet.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Feet.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Feet.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Feet.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Feet.OverlayOnlyList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Feet.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType == "Underwear"  ){
			SlotList.Clear();
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Underwear.OverlayList);
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Underwear.OverlayList);
			}
		}
		else if ( SelectedType == "HandledLeft" /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._LeftHand.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._LeftHand.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._LeftHand.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._LeftHand.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._LeftHand.OverlayList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._LeftHand.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._LeftHand.OverlayList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._LeftHand.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType == "HandledRight" /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._RightHand.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._RightHand.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._RightHand.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._RightHand.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._RightHand.OverlayList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._RightHand.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._RightHand.OverlayList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._RightHand.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType.Contains("BeltWear") /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Belt.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Belt.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Belt.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Belt.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Belt.OverlayList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Belt.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Belt.OverlayList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Belt.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType == "Cloak" /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Cloak.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Cloak.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Cloak.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Cloak.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Cloak.OverlayList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Cloak.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Cloak.OverlayList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Cloak.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType.Contains("BackPack") /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Backpack.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Backpack.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Backpack.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Backpack.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Backpack.OverlayList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Backpack.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Backpack.OverlayList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Backpack.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
		else if ( SelectedType.Contains("Collar") /*&& SelectedSlot == null && SelectedOverlay*/ ){
			SlotList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Collar.SlotList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Collar.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Collar.SlotList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Collar.OverlayList.Count == 0 ) OverlayList.Clear();
			}
			OverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Collar.OverlayList);
				if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Collar.SlotList.Count == 0 ) SlotList.Clear();
			}
			else if ( _DK_RPG_UMA.Gender == "Female" ) {
				OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Collar.OverlayList);
				if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Collar.SlotList.Count == 0 ) SlotList.Clear();
			}
		}
	else if ( SelectedType.Contains("Glasses") ){
		SlotList.Clear();
		if ( _DK_RPG_UMA.Gender == "Male" ) {
			SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Glasses.SlotList);
			if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Glasses.OverlayList.Count == 0 ) OverlayList.Clear();
		}
		else if ( _DK_RPG_UMA.Gender == "Female" ) {
			SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Glasses.SlotList);
			if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Glasses.OverlayList.Count == 0 ) OverlayList.Clear();
		}
		OverlayList.Clear();
		if ( _DK_RPG_UMA.Gender == "Male" ) {
			OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Glasses.OverlayList);
			if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Glasses.SlotList.Count == 0 ) SlotList.Clear();
		}
		else if ( _DK_RPG_UMA.Gender == "Female" ) {
			OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Glasses.OverlayList);
			if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Glasses.SlotList.Count == 0 ) SlotList.Clear();
		}
	}
	else if ( SelectedType.Contains("Mask") ){
		SlotList.Clear();
		if ( _DK_RPG_UMA.Gender == "Male" ) {
			SlotList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Mask.SlotList);
			if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Mask.OverlayList.Count == 0 ) OverlayList.Clear();
		}
		else if ( _DK_RPG_UMA.Gender == "Female" ) {
			SlotList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Mask.SlotList);
			if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Mask.OverlayList.Count == 0 ) OverlayList.Clear();
		}
		OverlayList.Clear();
		if ( _DK_RPG_UMA.Gender == "Male" ) {
			OverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._EquipmentData._Mask.OverlayList);
			if (_DK_RPG_UMA.RaceData._Male._EquipmentData._Mask.SlotList.Count == 0 ) SlotList.Clear();
		}
		else if ( _DK_RPG_UMA.Gender == "Female" ) {
			OverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._EquipmentData._Mask.OverlayList);
			if (_DK_RPG_UMA.RaceData._Female._EquipmentData._Mask.SlotList.Count == 0 ) SlotList.Clear();
		}
	}

		#endregion Wear Lists
		PrepareOverlaysLists ( _DK_RPG_UMA_Generator, 
		                      _DK_RPG_UMA, 
		                      _Selection, 
		                      SelectedType, 
		                      SelectedSlot,
		                      SelectedOverlay );
	}

	public static void PrepareOverlaysLists (DK_RPG_UMA_Generator _DK_RPG_UMA_Generator, 
	                                 DK_RPG_UMA _DK_RPG_UMA, 
	                                 GameObject _Selection, 
	                                 string SelectedType, 
	                                 DKSlotData SelectedSlot,
	                                 DKOverlayData SelectedOverlay ) {
		_DK_RPG_UMA_Generator = GameObject.Find ("DK_UMA").GetComponent<DK_RPG_UMA_Generator>();
		if ( _DK_RPG_UMA == null ) {
			if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = _Selection.GetComponent<DK_RPG_UMA>();
			if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = _Selection.GetComponentInChildren<DK_RPG_UMA>();
			if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = _Selection.GetComponentInParent<DK_RPG_UMA>();
		}
		
		if ( _DK_RPG_UMA && _DK_RPG_UMA.RaceData == null ) {
			DKRaceLibrary races = GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>().raceLibrary;
			foreach ( DKRaceData race in races.raceElementList ) {
				if (race.Race == _DK_RPG_UMA.Race ) _DK_RPG_UMA.RaceData = race;
			}
		}
		#region Face Lists
		LinkedOverlayList.Clear();
		if ( SelectedType == "Head" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Head.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Head.OverlayList);
		}
		else if ( SelectedType == "Eyes" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Eyes.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Eyes.OverlayList);
		}
		else if ( SelectedType == "Eyelash" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._EyeLash.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._EyeLash.OverlayList);
		}
		else if ( SelectedType == "Eyelids" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._EyeLids.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._EyeLids.OverlayList);
		}
		else if ( SelectedType == "Ears" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Ears.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Ears.OverlayList);
		}
		else if ( SelectedType == "Nose" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Nose.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Nose.OverlayList);
		}
		else if ( SelectedType == "Mouth" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Mouth.OverlayList);
		}
		else if ( SelectedType == "Innermouth" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth._InnerMouth.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Face._Mouth._InnerMouth.OverlayList);
		}
		#endregion Face Lists
		
		#region Body Lists
		else if ( SelectedType == "Torso" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Torso.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Torso.OverlayList);
		}
		else if ( SelectedType == "Hands" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Hands.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Hands.OverlayList);
		}
		else if ( SelectedType == "Legs" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Legs.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Legs.OverlayList);
		}
		else if ( SelectedType == "Feet" ) {
			LinkedOverlayList.Clear();
			if ( _DK_RPG_UMA.Gender == "Male" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Male._AvatarData._Body._Feet.OverlayList);
			else if ( _DK_RPG_UMA.Gender == "Female" ) LinkedOverlayList.AddRange (_DK_RPG_UMA.RaceData._Female._AvatarData._Body._Feet.OverlayList);
		}
		if ( SelectedSlot ) LinkedOverlayList.AddRange ( SelectedSlot.LinkedOverlayList );
		#endregion Body Lists

	}
}
