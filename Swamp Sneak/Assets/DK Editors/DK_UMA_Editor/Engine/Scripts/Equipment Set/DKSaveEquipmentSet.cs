using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif





public class DKSaveEquipmentSet : MonoBehaviour {
	
	public static void SavingSet ( string name, DKEquipmentSetData SelectedSet, DK_RPG_UMA _DK_RPG_UMA ) {
		bool UpdateSet = true;
		DKUMA_Variables _DKUMA_Variables = GameObject.Find("DK_UMA").GetComponentInChildren<DKUMA_Variables>();
		string _path;
		// create Set
		if ( SelectedSet == null ) {
			UpdateSet = false;
			SelectedSet = ScriptableObject.CreateInstance<DKEquipmentSetData>();
			SelectedSet.Name = name;
			SelectedSet.name = name;
		}
		// assign values
		#region Equipment
		// Head Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female.Active = true;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Slot = _DK_RPG_UMA._Equipment._HeadSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Overlay = _DK_RPG_UMA._Equipment._HeadSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.ColorPreset = _DK_RPG_UMA._Equipment._HeadSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Color = _DK_RPG_UMA._Equipment._HeadSub.Color;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Opt01Color = _DK_RPG_UMA._Equipment._HeadSub.Opt01Color;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Opt02Color = _DK_RPG_UMA._Equipment._HeadSub.Opt02Color;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Dirt01Color = _DK_RPG_UMA._Equipment._HeadSub.Dirt01Color;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Dirt02Color = _DK_RPG_UMA._Equipment._HeadSub.Dirt02Color;
		}
		else {
			SelectedSet.SetContent._Male.Active = true;
			SelectedSet.SetContent._Male._EquipmentData._HeadSub.Slot = _DK_RPG_UMA._Equipment._HeadSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._HeadSub.Overlay = _DK_RPG_UMA._Equipment._HeadSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._HeadSub.ColorPreset = _DK_RPG_UMA._Equipment._HeadSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._HeadSub.Color = _DK_RPG_UMA._Equipment._HeadSub.Color;
			SelectedSet.SetContent._Male._EquipmentData._HeadSub.Opt01Color = _DK_RPG_UMA._Equipment._HeadSub.Opt01Color;
			SelectedSet.SetContent._Male._EquipmentData._HeadSub.Opt02Color = _DK_RPG_UMA._Equipment._HeadSub.Opt02Color;
			SelectedSet.SetContent._Male._EquipmentData._HeadSub.Dirt01Color = _DK_RPG_UMA._Equipment._HeadSub.Dirt01Color;
			SelectedSet.SetContent._Male._EquipmentData._HeadSub.Dirt02Color = _DK_RPG_UMA._Equipment._HeadSub.Dirt02Color;
		}
		// Head
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Head.Slot = _DK_RPG_UMA._Equipment._Head.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Head.Overlay = _DK_RPG_UMA._Equipment._Head.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Head.ColorPreset = _DK_RPG_UMA._Equipment._Head.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Head.Color = _DK_RPG_UMA._Equipment._Head.Color;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Opt01Color = _DK_RPG_UMA._Equipment._HeadSub.Opt01Color;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Opt02Color = _DK_RPG_UMA._Equipment._HeadSub.Opt02Color;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Dirt01Color = _DK_RPG_UMA._Equipment._HeadSub.Dirt01Color;
			SelectedSet.SetContent._Female._EquipmentData._HeadSub.Dirt02Color = _DK_RPG_UMA._Equipment._HeadSub.Dirt02Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Head.Slot = _DK_RPG_UMA._Equipment._Head.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Head.Overlay = _DK_RPG_UMA._Equipment._Head.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Head.ColorPreset = _DK_RPG_UMA._Equipment._Head.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Head.Color = _DK_RPG_UMA._Equipment._Head.Color;
		}
		// Head Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._HeadCover.Slot = _DK_RPG_UMA._Equipment._HeadCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._HeadCover.Overlay = _DK_RPG_UMA._Equipment._HeadCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._HeadCover.ColorPreset = _DK_RPG_UMA._Equipment._HeadCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._HeadCover.Color = _DK_RPG_UMA._Equipment._HeadCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._HeadCover.Slot = _DK_RPG_UMA._Equipment._HeadCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._HeadCover.Overlay = _DK_RPG_UMA._Equipment._HeadCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._HeadCover.ColorPreset = _DK_RPG_UMA._Equipment._HeadCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._HeadCover.Color = _DK_RPG_UMA._Equipment._HeadCover.Color;
		}
		// Shoulder Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._ShoulderSub.Slot = _DK_RPG_UMA._Equipment._ShoulderSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._ShoulderSub.Overlay = _DK_RPG_UMA._Equipment._ShoulderSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._ShoulderSub.ColorPreset = _DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._ShoulderSub.Color = _DK_RPG_UMA._Equipment._ShoulderSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._ShoulderSub.Slot = _DK_RPG_UMA._Equipment._ShoulderSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._ShoulderSub.Overlay = _DK_RPG_UMA._Equipment._ShoulderSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._ShoulderSub.ColorPreset = _DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._ShoulderSub.Color = _DK_RPG_UMA._Equipment._ShoulderSub.Color;
		}
		// Shoulder
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Shoulder.Slot = _DK_RPG_UMA._Equipment._Shoulder.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Shoulder.Overlay = _DK_RPG_UMA._Equipment._Shoulder.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Shoulder.ColorPreset = _DK_RPG_UMA._Equipment._Shoulder.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Shoulder.Color = _DK_RPG_UMA._Equipment._Shoulder.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Shoulder.Slot = _DK_RPG_UMA._Equipment._Shoulder.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Shoulder.Overlay = _DK_RPG_UMA._Equipment._Shoulder.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Shoulder.ColorPreset = _DK_RPG_UMA._Equipment._Shoulder.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Shoulder.Color = _DK_RPG_UMA._Equipment._Shoulder.Color;
		}
		// Shoulder Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._ShoulderCover.Slot = _DK_RPG_UMA._Equipment._ShoulderCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._ShoulderCover.Overlay = _DK_RPG_UMA._Equipment._ShoulderCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._ShoulderCover.ColorPreset = _DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._ShoulderCover.Color = _DK_RPG_UMA._Equipment._ShoulderCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._ShoulderCover.Slot = _DK_RPG_UMA._Equipment._ShoulderCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._ShoulderCover.Overlay = _DK_RPG_UMA._Equipment._ShoulderCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._ShoulderCover.ColorPreset = _DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._ShoulderCover.Color = _DK_RPG_UMA._Equipment._ShoulderCover.Color;
		}
		// Torso Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._TorsoSub.Slot = _DK_RPG_UMA._Equipment._TorsoSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._TorsoSub.Overlay = _DK_RPG_UMA._Equipment._TorsoSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._TorsoSub.ColorPreset = _DK_RPG_UMA._Equipment._TorsoSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._TorsoSub.Color = _DK_RPG_UMA._Equipment._TorsoSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._TorsoSub.Slot = _DK_RPG_UMA._Equipment._TorsoSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._TorsoSub.Overlay = _DK_RPG_UMA._Equipment._TorsoSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._TorsoSub.ColorPreset = _DK_RPG_UMA._Equipment._TorsoSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._TorsoSub.Color = _DK_RPG_UMA._Equipment._TorsoSub.Color;
		}
		// Torso
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Torso.Slot = _DK_RPG_UMA._Equipment._Torso.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Torso.Overlay = _DK_RPG_UMA._Equipment._Torso.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Torso.ColorPreset = _DK_RPG_UMA._Equipment._Torso.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Torso.Color = _DK_RPG_UMA._Equipment._Torso.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Torso.Slot = _DK_RPG_UMA._Equipment._Torso.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Torso.Overlay = _DK_RPG_UMA._Equipment._Torso.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Torso.ColorPreset = _DK_RPG_UMA._Equipment._Torso.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Torso.Color = _DK_RPG_UMA._Equipment._Torso.Color;
		}
		// Torso Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._TorsoCover.Slot = _DK_RPG_UMA._Equipment._TorsoCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._TorsoCover.Overlay = _DK_RPG_UMA._Equipment._TorsoCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._TorsoCover.ColorPreset = _DK_RPG_UMA._Equipment._TorsoCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._TorsoCover.Color = _DK_RPG_UMA._Equipment._TorsoCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._TorsoCover.Slot = _DK_RPG_UMA._Equipment._TorsoCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._TorsoCover.Overlay = _DK_RPG_UMA._Equipment._TorsoCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._TorsoCover.ColorPreset = _DK_RPG_UMA._Equipment._TorsoCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._TorsoCover.Color = _DK_RPG_UMA._Equipment._TorsoCover.Color;
		}
		// ArmBand Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._ArmBandSub.Slot = _DK_RPG_UMA._Equipment._ArmBandSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._ArmBandSub.Overlay = _DK_RPG_UMA._Equipment._ArmBandSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._ArmBandSub.ColorPreset = _DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._ArmBandSub.Color = _DK_RPG_UMA._Equipment._ArmBandSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._ArmBandSub.Slot = _DK_RPG_UMA._Equipment._ArmBandSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._ArmBandSub.Overlay = _DK_RPG_UMA._Equipment._ArmBandSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._ArmBandSub.ColorPreset = _DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._ArmBandSub.Color = _DK_RPG_UMA._Equipment._ArmBandSub.Color;
		}
		// ArmBand
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._ArmBand.Slot = _DK_RPG_UMA._Equipment._ArmBand.Slot;
			SelectedSet.SetContent._Female._EquipmentData._ArmBand.Overlay = _DK_RPG_UMA._Equipment._ArmBand.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._ArmBand.ColorPreset = _DK_RPG_UMA._Equipment._ArmBand.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._ArmBand.Color = _DK_RPG_UMA._Equipment._ArmBand.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._ArmBand.Slot = _DK_RPG_UMA._Equipment._ArmBand.Slot;
			SelectedSet.SetContent._Male._EquipmentData._ArmBand.Overlay = _DK_RPG_UMA._Equipment._ArmBand.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._ArmBand.ColorPreset = _DK_RPG_UMA._Equipment._ArmBand.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._ArmBand.Color = _DK_RPG_UMA._Equipment._ArmBand.Color;
		}
		// ArmBand Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._ArmBandCover.Slot = _DK_RPG_UMA._Equipment._ArmBandCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._ArmBandCover.Overlay = _DK_RPG_UMA._Equipment._ArmBandCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._ArmBandCover.ColorPreset = _DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._ArmBandCover.Color = _DK_RPG_UMA._Equipment._ArmBandCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._ArmBandCover.Slot = _DK_RPG_UMA._Equipment._ArmBandCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._ArmBandCover.Overlay = _DK_RPG_UMA._Equipment._ArmBandCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._ArmBandCover.ColorPreset = _DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._ArmBandCover.Color = _DK_RPG_UMA._Equipment._ArmBandCover.Color;
		}
		// Wrist Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._WristSub.Slot = _DK_RPG_UMA._Equipment._WristSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._WristSub.Overlay = _DK_RPG_UMA._Equipment._WristSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._WristSub.ColorPreset = _DK_RPG_UMA._Equipment._WristSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._WristSub.Color = _DK_RPG_UMA._Equipment._WristSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._WristSub.Slot = _DK_RPG_UMA._Equipment._WristSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._WristSub.Overlay = _DK_RPG_UMA._Equipment._WristSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._WristSub.ColorPreset = _DK_RPG_UMA._Equipment._WristSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._WristSub.Color = _DK_RPG_UMA._Equipment._WristSub.Color;
		}
		// Wrist
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Wrist.Slot = _DK_RPG_UMA._Equipment._Wrist.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Wrist.Overlay = _DK_RPG_UMA._Equipment._Wrist.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Wrist.ColorPreset = _DK_RPG_UMA._Equipment._Wrist.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Wrist.Color = _DK_RPG_UMA._Equipment._Wrist.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Wrist.Slot = _DK_RPG_UMA._Equipment._Wrist.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Wrist.Overlay = _DK_RPG_UMA._Equipment._Wrist.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Wrist.ColorPreset = _DK_RPG_UMA._Equipment._Wrist.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Wrist.Color = _DK_RPG_UMA._Equipment._Wrist.Color;
		}
		// Wrist Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._WristCover.Slot = _DK_RPG_UMA._Equipment._WristCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._WristCover.Overlay = _DK_RPG_UMA._Equipment._WristCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._WristCover.ColorPreset = _DK_RPG_UMA._Equipment._WristCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._WristCover.Color = _DK_RPG_UMA._Equipment._WristCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._WristCover.Slot = _DK_RPG_UMA._Equipment._WristCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._WristCover.Overlay = _DK_RPG_UMA._Equipment._WristCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._WristCover.ColorPreset = _DK_RPG_UMA._Equipment._WristCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._WristCover.Color = _DK_RPG_UMA._Equipment._WristCover.Color;
		}
		// Hands Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._HandsSub.Slot = _DK_RPG_UMA._Equipment._HandsSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._HandsSub.Overlay = _DK_RPG_UMA._Equipment._HandsSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._HandsSub.ColorPreset = _DK_RPG_UMA._Equipment._HandsSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._HandsSub.Color = _DK_RPG_UMA._Equipment._HandsSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._HandsSub.Slot = _DK_RPG_UMA._Equipment._HandsSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._HandsSub.Overlay = _DK_RPG_UMA._Equipment._HandsSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._HandsSub.ColorPreset = _DK_RPG_UMA._Equipment._HandsSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._HandsSub.Color = _DK_RPG_UMA._Equipment._HandsSub.Color;
		}
		// Hands
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Hands.Slot = _DK_RPG_UMA._Equipment._Hands.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Hands.Overlay = _DK_RPG_UMA._Equipment._Hands.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Hands.ColorPreset = _DK_RPG_UMA._Equipment._Hands.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Hands.Color = _DK_RPG_UMA._Equipment._Hands.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Hands.Slot = _DK_RPG_UMA._Equipment._Hands.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Hands.Overlay = _DK_RPG_UMA._Equipment._Hands.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Hands.ColorPreset = _DK_RPG_UMA._Equipment._Hands.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Hands.Color = _DK_RPG_UMA._Equipment._Hands.Color;
		}
		// Hands Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._HandsCover.Slot = _DK_RPG_UMA._Equipment._HandsCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._HandsCover.Overlay = _DK_RPG_UMA._Equipment._HandsCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._HandsCover.ColorPreset = _DK_RPG_UMA._Equipment._HandsCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._HandsCover.Color = _DK_RPG_UMA._Equipment._HandsCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._HandsCover.Slot = _DK_RPG_UMA._Equipment._HandsCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._HandsCover.Overlay = _DK_RPG_UMA._Equipment._HandsCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._HandsCover.ColorPreset = _DK_RPG_UMA._Equipment._HandsCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._HandsCover.Color = _DK_RPG_UMA._Equipment._HandsCover.Color;
		}
		// Belt Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._BeltSub.Slot = _DK_RPG_UMA._Equipment._BeltSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._BeltSub.Overlay = _DK_RPG_UMA._Equipment._BeltSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._BeltSub.ColorPreset = _DK_RPG_UMA._Equipment._BeltSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._BeltSub.Color = _DK_RPG_UMA._Equipment._BeltSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._BeltSub.Slot = _DK_RPG_UMA._Equipment._BeltSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._BeltSub.Overlay = _DK_RPG_UMA._Equipment._BeltSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._BeltSub.ColorPreset = _DK_RPG_UMA._Equipment._BeltSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._BeltSub.Color = _DK_RPG_UMA._Equipment._BeltSub.Color;
		}
		// Belt
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Belt.Slot = _DK_RPG_UMA._Equipment._Belt.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Belt.Overlay = _DK_RPG_UMA._Equipment._Belt.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Belt.ColorPreset = _DK_RPG_UMA._Equipment._Belt.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Belt.Color = _DK_RPG_UMA._Equipment._Belt.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Belt.Slot = _DK_RPG_UMA._Equipment._Belt.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Belt.Overlay = _DK_RPG_UMA._Equipment._Belt.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Belt.ColorPreset = _DK_RPG_UMA._Equipment._Belt.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Belt.Color = _DK_RPG_UMA._Equipment._Belt.Color;
		}
		// Belt Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._BeltCover.Slot = _DK_RPG_UMA._Equipment._BeltCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._BeltCover.Overlay = _DK_RPG_UMA._Equipment._BeltCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._BeltCover.ColorPreset = _DK_RPG_UMA._Equipment._BeltCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._BeltCover.Color = _DK_RPG_UMA._Equipment._BeltCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._BeltCover.Slot = _DK_RPG_UMA._Equipment._BeltCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._BeltCover.Overlay = _DK_RPG_UMA._Equipment._BeltCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._BeltCover.ColorPreset = _DK_RPG_UMA._Equipment._BeltCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._BeltCover.Color = _DK_RPG_UMA._Equipment._BeltCover.Color;
		}
		// Legs Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._LegsSub.Slot = _DK_RPG_UMA._Equipment._LegsSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._LegsSub.Overlay = _DK_RPG_UMA._Equipment._LegsSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._LegsSub.ColorPreset = _DK_RPG_UMA._Equipment._LegsSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._LegsSub.Color = _DK_RPG_UMA._Equipment._LegsSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._LegsSub.Slot = _DK_RPG_UMA._Equipment._LegsSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._LegsSub.Overlay = _DK_RPG_UMA._Equipment._LegsSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._LegsSub.ColorPreset = _DK_RPG_UMA._Equipment._LegsSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._LegsSub.Color = _DK_RPG_UMA._Equipment._LegsSub.Color;
		}
		// Legs
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Legs.Slot = _DK_RPG_UMA._Equipment._Legs.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Legs.Overlay = _DK_RPG_UMA._Equipment._Legs.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Legs.ColorPreset = _DK_RPG_UMA._Equipment._Legs.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Legs.Color = _DK_RPG_UMA._Equipment._Legs.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Legs.Slot = _DK_RPG_UMA._Equipment._Legs.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Legs.Overlay = _DK_RPG_UMA._Equipment._Legs.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Legs.ColorPreset = _DK_RPG_UMA._Equipment._Legs.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Legs.Color = _DK_RPG_UMA._Equipment._Legs.Color;
		}
		// Legs Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._LegsCover.Slot = _DK_RPG_UMA._Equipment._LegsCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._LegsCover.Overlay = _DK_RPG_UMA._Equipment._LegsCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._LegsCover.ColorPreset = _DK_RPG_UMA._Equipment._LegsCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._LegsCover.Color = _DK_RPG_UMA._Equipment._LegsCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._LegsCover.Slot = _DK_RPG_UMA._Equipment._LegsCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._LegsCover.Overlay = _DK_RPG_UMA._Equipment._LegsCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._LegsCover.ColorPreset = _DK_RPG_UMA._Equipment._LegsCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._LegsCover.Color = _DK_RPG_UMA._Equipment._LegsCover.Color;
		}
		// LegBand Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._LegBandSub.Slot = _DK_RPG_UMA._Equipment._LegBandSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._LegBandSub.Overlay = _DK_RPG_UMA._Equipment._LegBandSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._LegBandSub.ColorPreset = _DK_RPG_UMA._Equipment._LegBandSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._LegBandSub.Color = _DK_RPG_UMA._Equipment._LegBandSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._LegBandSub.Slot = _DK_RPG_UMA._Equipment._LegBandSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._LegBandSub.Overlay = _DK_RPG_UMA._Equipment._LegBandSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._LegBandSub.ColorPreset = _DK_RPG_UMA._Equipment._LegBandSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._LegBandSub.Color = _DK_RPG_UMA._Equipment._LegBandSub.Color;
		}
		// LegBand
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._LegBand.Slot = _DK_RPG_UMA._Equipment._LegBand.Slot;
			SelectedSet.SetContent._Female._EquipmentData._LegBand.Overlay = _DK_RPG_UMA._Equipment._LegBand.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._LegBand.ColorPreset = _DK_RPG_UMA._Equipment._LegBand.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._LegBand.Color = _DK_RPG_UMA._Equipment._LegBand.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._LegBand.Slot = _DK_RPG_UMA._Equipment._LegBand.Slot;
			SelectedSet.SetContent._Male._EquipmentData._LegBand.Overlay = _DK_RPG_UMA._Equipment._LegBand.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._LegBand.ColorPreset = _DK_RPG_UMA._Equipment._LegBand.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._LegBand.Color = _DK_RPG_UMA._Equipment._LegBand.Color;
		}
		// LegBand Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._LegBandCover.Slot = _DK_RPG_UMA._Equipment._LegBandCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._LegBandCover.Overlay = _DK_RPG_UMA._Equipment._LegBandCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._LegBandCover.ColorPreset = _DK_RPG_UMA._Equipment._LegBandCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._LegBandCover.Color = _DK_RPG_UMA._Equipment._LegBandCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._LegBandCover.Slot = _DK_RPG_UMA._Equipment._LegBandCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._LegBandCover.Overlay = _DK_RPG_UMA._Equipment._LegBandCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._LegBandCover.ColorPreset = _DK_RPG_UMA._Equipment._LegBandCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._LegBandCover.Color = _DK_RPG_UMA._Equipment._LegBandCover.Color;
		}
		// Feet Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._FeetSub.Slot = _DK_RPG_UMA._Equipment._FeetSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._FeetSub.Overlay = _DK_RPG_UMA._Equipment._FeetSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._FeetSub.ColorPreset = _DK_RPG_UMA._Equipment._FeetSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._FeetSub.Color = _DK_RPG_UMA._Equipment._FeetSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._FeetSub.Slot = _DK_RPG_UMA._Equipment._FeetSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._FeetSub.Overlay = _DK_RPG_UMA._Equipment._FeetSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._FeetSub.ColorPreset = _DK_RPG_UMA._Equipment._FeetSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._FeetSub.Color = _DK_RPG_UMA._Equipment._FeetSub.Color;
		}
		// Feet
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Feet.Slot = _DK_RPG_UMA._Equipment._Feet.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Feet.Overlay = _DK_RPG_UMA._Equipment._Feet.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Feet.ColorPreset = _DK_RPG_UMA._Equipment._Feet.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Feet.Color = _DK_RPG_UMA._Equipment._Feet.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Feet.Slot = _DK_RPG_UMA._Equipment._Feet.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Feet.Overlay = _DK_RPG_UMA._Equipment._Feet.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Feet.ColorPreset = _DK_RPG_UMA._Equipment._Feet.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Feet.Color = _DK_RPG_UMA._Equipment._Feet.Color;
		}
		// Feet Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._FeetCover.Slot = _DK_RPG_UMA._Equipment._FeetCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._FeetCover.Overlay = _DK_RPG_UMA._Equipment._FeetCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._FeetCover.ColorPreset = _DK_RPG_UMA._Equipment._FeetCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._FeetCover.Color = _DK_RPG_UMA._Equipment._FeetCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._FeetCover.Slot = _DK_RPG_UMA._Equipment._FeetCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._FeetCover.Overlay = _DK_RPG_UMA._Equipment._FeetCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._FeetCover.ColorPreset = _DK_RPG_UMA._Equipment._FeetCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._FeetCover.Color = _DK_RPG_UMA._Equipment._FeetCover.Color;
		}


		// Collar Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._CollarSub.Slot = _DK_RPG_UMA._Equipment._CollarSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._CollarSub.Overlay = _DK_RPG_UMA._Equipment._CollarSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._CollarSub.ColorPreset = _DK_RPG_UMA._Equipment._CollarSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._CollarSub.Color = _DK_RPG_UMA._Equipment._CollarSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._CollarSub.Slot = _DK_RPG_UMA._Equipment._CollarSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._CollarSub.Overlay = _DK_RPG_UMA._Equipment._CollarSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._CollarSub.ColorPreset = _DK_RPG_UMA._Equipment._CollarSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._CollarSub.Color = _DK_RPG_UMA._Equipment._CollarSub.Color;
		}
		// Collar
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Collar.Slot = _DK_RPG_UMA._Equipment._Collar.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Collar.Overlay = _DK_RPG_UMA._Equipment._Collar.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Collar.ColorPreset = _DK_RPG_UMA._Equipment._Collar.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Collar.Color = _DK_RPG_UMA._Equipment._Collar.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Collar.Slot = _DK_RPG_UMA._Equipment._Collar.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Collar.Overlay = _DK_RPG_UMA._Equipment._Collar.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Collar.ColorPreset = _DK_RPG_UMA._Equipment._Collar.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Collar.Color = _DK_RPG_UMA._Equipment._Collar.Color;
		}
		// Collar Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._CollarCover.Slot = _DK_RPG_UMA._Equipment._CollarCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._CollarCover.Overlay = _DK_RPG_UMA._Equipment._CollarCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._CollarCover.ColorPreset = _DK_RPG_UMA._Equipment._CollarCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._CollarCover.Color = _DK_RPG_UMA._Equipment._CollarCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._CollarCover.Slot = _DK_RPG_UMA._Equipment._CollarCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._CollarCover.Overlay = _DK_RPG_UMA._Equipment._CollarCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._CollarCover.ColorPreset = _DK_RPG_UMA._Equipment._CollarCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._CollarCover.Color = _DK_RPG_UMA._Equipment._CollarCover.Color;
		}
		// RingLeft
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._RingLeft.Slot = _DK_RPG_UMA._Equipment._RingLeft.Slot;
			SelectedSet.SetContent._Female._EquipmentData._RingLeft.Overlay = _DK_RPG_UMA._Equipment._RingLeft.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._RingLeft.ColorPreset = _DK_RPG_UMA._Equipment._RingLeft.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._RingLeft.Color = _DK_RPG_UMA._Equipment._RingLeft.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._RingLeft.Slot = _DK_RPG_UMA._Equipment._RingLeft.Slot;
			SelectedSet.SetContent._Male._EquipmentData._RingLeft.Overlay = _DK_RPG_UMA._Equipment._RingLeft.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._RingLeft.ColorPreset = _DK_RPG_UMA._Equipment._RingLeft.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._RingLeft.Color = _DK_RPG_UMA._Equipment._RingLeft.Color;
		}
		// RingRight
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._RingRight.Slot = _DK_RPG_UMA._Equipment._RingRight.Slot;
			SelectedSet.SetContent._Female._EquipmentData._RingRight.Overlay = _DK_RPG_UMA._Equipment._RingRight.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._RingRight.ColorPreset = _DK_RPG_UMA._Equipment._RingRight.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._RingRight.Color = _DK_RPG_UMA._Equipment._RingRight.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._RingRight.Slot = _DK_RPG_UMA._Equipment._RingRight.Slot;
			SelectedSet.SetContent._Male._EquipmentData._RingRight.Overlay = _DK_RPG_UMA._Equipment._RingRight.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._RingRight.ColorPreset = _DK_RPG_UMA._Equipment._RingRight.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._RingRight.Color = _DK_RPG_UMA._Equipment._RingRight.Color;
		}
		// Cloak
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Cloak.Slot = _DK_RPG_UMA._Equipment._Cloak.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Cloak.Overlay = _DK_RPG_UMA._Equipment._Cloak.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Cloak.ColorPreset = _DK_RPG_UMA._Equipment._Cloak.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Cloak.Color = _DK_RPG_UMA._Equipment._Cloak.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Cloak.Slot = _DK_RPG_UMA._Equipment._Cloak.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Cloak.Overlay = _DK_RPG_UMA._Equipment._Cloak.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Cloak.ColorPreset = _DK_RPG_UMA._Equipment._Cloak.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Cloak.Color = _DK_RPG_UMA._Equipment._Cloak.Color;
		}
		// Backpack Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._BackpackSub.Slot = _DK_RPG_UMA._Equipment._BackpackSub.Slot;
			SelectedSet.SetContent._Female._EquipmentData._BackpackSub.Overlay = _DK_RPG_UMA._Equipment._BackpackSub.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._BackpackSub.ColorPreset = _DK_RPG_UMA._Equipment._BackpackSub.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._BackpackSub.Color = _DK_RPG_UMA._Equipment._BackpackSub.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._BackpackSub.Slot = _DK_RPG_UMA._Equipment._BackpackSub.Slot;
			SelectedSet.SetContent._Male._EquipmentData._BackpackSub.Overlay = _DK_RPG_UMA._Equipment._BackpackSub.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._BackpackSub.ColorPreset = _DK_RPG_UMA._Equipment._BackpackSub.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._BackpackSub.Color = _DK_RPG_UMA._Equipment._BackpackSub.Color;
		}
		// Backpack
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._Backpack.Slot = _DK_RPG_UMA._Equipment._Backpack.Slot;
			SelectedSet.SetContent._Female._EquipmentData._Backpack.Overlay = _DK_RPG_UMA._Equipment._Backpack.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._Backpack.ColorPreset = _DK_RPG_UMA._Equipment._Backpack.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._Backpack.Color = _DK_RPG_UMA._Equipment._Backpack.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._Backpack.Slot = _DK_RPG_UMA._Equipment._Backpack.Slot;
			SelectedSet.SetContent._Male._EquipmentData._Backpack.Overlay = _DK_RPG_UMA._Equipment._Backpack.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._Backpack.ColorPreset = _DK_RPG_UMA._Equipment._Backpack.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._Backpack.Color = _DK_RPG_UMA._Equipment._Backpack.Color;
		}
		// Backpack Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._BackpackCover.Slot = _DK_RPG_UMA._Equipment._BackpackCover.Slot;
			SelectedSet.SetContent._Female._EquipmentData._BackpackCover.Overlay = _DK_RPG_UMA._Equipment._BackpackCover.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._BackpackCover.ColorPreset = _DK_RPG_UMA._Equipment._BackpackCover.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._BackpackCover.Color = _DK_RPG_UMA._Equipment._BackpackCover.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._BackpackCover.Slot = _DK_RPG_UMA._Equipment._BackpackCover.Slot;
			SelectedSet.SetContent._Male._EquipmentData._BackpackCover.Overlay = _DK_RPG_UMA._Equipment._BackpackCover.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._BackpackCover.ColorPreset = _DK_RPG_UMA._Equipment._BackpackCover.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._BackpackCover.Color = _DK_RPG_UMA._Equipment._BackpackCover.Color;
		}
		// LeftHand
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._LeftHand.Slot = _DK_RPG_UMA._Equipment._LeftHand.Slot;
			SelectedSet.SetContent._Female._EquipmentData._LeftHand.Overlay = _DK_RPG_UMA._Equipment._LeftHand.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._LeftHand.ColorPreset = _DK_RPG_UMA._Equipment._LeftHand.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._LeftHand.Color = _DK_RPG_UMA._Equipment._LeftHand.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._LeftHand.Slot = _DK_RPG_UMA._Equipment._LeftHand.Slot;
			SelectedSet.SetContent._Male._EquipmentData._LeftHand.Overlay = _DK_RPG_UMA._Equipment._LeftHand.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._LeftHand.ColorPreset = _DK_RPG_UMA._Equipment._LeftHand.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._LeftHand.Color = _DK_RPG_UMA._Equipment._LeftHand.Color;
		}
		// RightHand
		if ( _DK_RPG_UMA.Gender == "Female" ){
			SelectedSet.SetContent._Female._EquipmentData._RightHand.Slot = _DK_RPG_UMA._Equipment._RightHand.Slot;
			SelectedSet.SetContent._Female._EquipmentData._RightHand.Overlay = _DK_RPG_UMA._Equipment._RightHand.Overlay;
			SelectedSet.SetContent._Female._EquipmentData._RightHand.ColorPreset = _DK_RPG_UMA._Equipment._RightHand.ColorPreset;
			SelectedSet.SetContent._Female._EquipmentData._RightHand.Color = _DK_RPG_UMA._Equipment._RightHand.Color;
		}
		else {
			SelectedSet.SetContent._Male._EquipmentData._RightHand.Slot = _DK_RPG_UMA._Equipment._RightHand.Slot;
			SelectedSet.SetContent._Male._EquipmentData._RightHand.Overlay = _DK_RPG_UMA._Equipment._RightHand.Overlay;
			SelectedSet.SetContent._Male._EquipmentData._RightHand.ColorPreset = _DK_RPG_UMA._Equipment._RightHand.ColorPreset;
			SelectedSet.SetContent._Male._EquipmentData._RightHand.Color = _DK_RPG_UMA._Equipment._RightHand.Color;
		}
		#endregion Equipment	

		// save to disk
		#if UNITY_EDITOR
		System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/EquipmentSets/Sets/");
		System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/EquipmentSets/Sets/");
		_path = ("Assets/DK Editors/DK_UMA_Content/EquipmentSets/Sets/"+SelectedSet.name+".asset");

		if ( UpdateSet == false ) {
			AssetDatabase.CreateAsset(SelectedSet, _path);
			AssetDatabase.Refresh ();
			Debug.Log ("Ingame Creator : EquipmentSet '"+SelectedSet.name+"' has been created at path '"+_path+"'.");
		}
		else {
			Debug.Log ("Ingame Creator : EquipmentSet '"+SelectedSet.name+"' has been Updated at path '"+_path+"'.");
		}

		// add to sets list
		_DKUMA_Variables._DK_UMA_GameSettings.EquipmentSets.EquipmentSetsList.Add ( SelectedSet );
		EditorUtility.SetDirty (_DKUMA_Variables._DK_UMA_GameSettings);
	//	if ( _DKUMA_Variables._EquipmentSets._EquipmentSetsData != null )
	//		_DKUMA_Variables._EquipmentSets._EquipmentSetsData.Sets.SetsList.Add ( SelectedSet );

		EditorUtility.SetDirty (SelectedSet);
		AssetDatabase.SaveAssets ();
		#endif
	}
}
