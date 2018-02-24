using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class ListsOfElements : MonoBehaviour {
	/*
	public static void PrepareLists ( DK_RPG_UMA _DK_RPG_UMA, string SelectedType, GameObject selection, List<DKSlotData> _slotList ) {
		DK_RPG_UMA_Generator _DK_RPG_UMA_Generator = GameObject.Find ("DK_UMA").GetComponent<DK_RPG_UMA_Generator>();
		if ( _DK_RPG_UMA == null ) {
			if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = selection.GetComponent<DK_RPG_UMA>();
			if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = selection.GetComponentInChildren<DK_RPG_UMA>();
			if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = selection.GetComponentInParent<DK_RPG_UMA>();
		}
		
		if ( _DK_RPG_UMA && _DK_RPG_UMA.RaceData == null ) {
			DKRaceLibrary races = GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>().raceLibrary;
			foreach ( DKRaceData race in races.raceElementList ) {
				if (race.Race == _DK_RPG_UMA.Race ) _DK_RPG_UMA.RaceData = race;
			}
		}

		List<DKSlotData> slotList = _slotList;

		if ( _DK_RPG_UMA.Gender == "Male" ) {
			if ( SelectedType == "Head" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Head.SlotList;
			if ( SelectedType == "Eyes" ) slotList =_DK_RPG_UMA.RaceData._Male._AvatarData._Face._Eyes.SlotList;
			if ( SelectedType == "Eyelash" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._EyeLash.SlotList;
			if ( SelectedType == "Eyelids" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._EyeLids.SlotList;
			if ( SelectedType == "Ears" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Ears.SlotList;
			if ( SelectedType == "Nose" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Nose.SlotList;
			if ( SelectedType == "Mouth" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.SlotList;
			//	if ( SelectedType == "Eyebrow" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.SlotList;
			//	if ( SelectedType == "Beard" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.SlotList;
			//	if ( SelectedType == "HeadTatoo" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.SlotList;
			//	if ( SelectedType == "HeadMakeup" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.SlotList;
			//	if ( SelectedType == "Hair" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.SlotList;
			//	if ( SelectedType == "HairModule" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.SlotList;
			//	if ( SelectedType == "Lips" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth.SlotList;
			if ( SelectedType == "Innermouth" ) slotList = _DK_RPG_UMA.RaceData._Male._AvatarData._Face._Mouth._InnerMouth.SlotList;
		}
	}*/
}
