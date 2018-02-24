using UnityEngine;
using System.Collections;

public class DKLoadEquipmentSet : MonoBehaviour {

	public static void LoadingSet ( DKEquipmentSetData SetToLoad, DK_RPG_UMA _DK_RPG_UMA ) {
		#region Equipment
		// Head Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._HeadSub.Slot = SetToLoad.SetContent._Female._EquipmentData._HeadSub.Slot;
			_DK_RPG_UMA._Equipment._HeadSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._HeadSub.Overlay;
			_DK_RPG_UMA._Equipment._HeadSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._HeadSub.ColorPreset;
			_DK_RPG_UMA._Equipment._HeadSub.Color = SetToLoad.SetContent._Female._EquipmentData._HeadSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._HeadSub.Slot = SetToLoad.SetContent._Male._EquipmentData._HeadSub.Slot;
			_DK_RPG_UMA._Equipment._HeadSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._HeadSub.Overlay;
			_DK_RPG_UMA._Equipment._HeadSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._HeadSub.ColorPreset;
			_DK_RPG_UMA._Equipment._HeadSub.Color = SetToLoad.SetContent._Male._EquipmentData._HeadSub.Color;
		}
		// Head
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Head.Slot = SetToLoad.SetContent._Female._EquipmentData._Head.Slot;
			_DK_RPG_UMA._Equipment._Head.Overlay = SetToLoad.SetContent._Female._EquipmentData._Head.Overlay;
			_DK_RPG_UMA._Equipment._Head.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Head.ColorPreset;
			_DK_RPG_UMA._Equipment._Head.Color = SetToLoad.SetContent._Female._EquipmentData._Head.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Head.Slot = SetToLoad.SetContent._Male._EquipmentData._Head.Slot;
			_DK_RPG_UMA._Equipment._Head.Overlay = SetToLoad.SetContent._Male._EquipmentData._Head.Overlay;
			_DK_RPG_UMA._Equipment._Head.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Head.ColorPreset;
			_DK_RPG_UMA._Equipment._Head.Color = SetToLoad.SetContent._Male._EquipmentData._Head.Color;
		}
		// Head Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._HeadCover.Slot = SetToLoad.SetContent._Female._EquipmentData._HeadCover.Slot;
			_DK_RPG_UMA._Equipment._HeadCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._HeadCover.Overlay;
			_DK_RPG_UMA._Equipment._HeadCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._HeadCover.ColorPreset;
			_DK_RPG_UMA._Equipment._HeadCover.Color = SetToLoad.SetContent._Female._EquipmentData._HeadCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._HeadCover.Slot = SetToLoad.SetContent._Male._EquipmentData._HeadCover.Slot;
			_DK_RPG_UMA._Equipment._HeadCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._HeadCover.Overlay;
			_DK_RPG_UMA._Equipment._HeadCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._HeadCover.ColorPreset;
			_DK_RPG_UMA._Equipment._HeadCover.Color = SetToLoad.SetContent._Male._EquipmentData._HeadCover.Color;
		}
		// Shoulder Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._ShoulderSub.Slot = SetToLoad.SetContent._Female._EquipmentData._ShoulderSub.Slot;
			_DK_RPG_UMA._Equipment._ShoulderSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._ShoulderSub.Overlay;
			_DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._ShoulderSub.ColorPreset;
			_DK_RPG_UMA._Equipment._ShoulderSub.Color = SetToLoad.SetContent._Female._EquipmentData._ShoulderSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._ShoulderSub.Slot = SetToLoad.SetContent._Male._EquipmentData._ShoulderSub.Slot;
			_DK_RPG_UMA._Equipment._ShoulderSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._ShoulderSub.Overlay;
			_DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._ShoulderSub.ColorPreset;
			_DK_RPG_UMA._Equipment._ShoulderSub.Color = SetToLoad.SetContent._Male._EquipmentData._ShoulderSub.Color;
		}
		// Shoulder
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Shoulder.Slot = SetToLoad.SetContent._Female._EquipmentData._Shoulder.Slot;
			_DK_RPG_UMA._Equipment._Shoulder.Overlay = SetToLoad.SetContent._Female._EquipmentData._Shoulder.Overlay;
			_DK_RPG_UMA._Equipment._Shoulder.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Shoulder.ColorPreset;
			_DK_RPG_UMA._Equipment._Shoulder.Color = SetToLoad.SetContent._Female._EquipmentData._Shoulder.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Shoulder.Slot = SetToLoad.SetContent._Male._EquipmentData._Shoulder.Slot;
			_DK_RPG_UMA._Equipment._Shoulder.Overlay = SetToLoad.SetContent._Male._EquipmentData._Shoulder.Overlay;
			_DK_RPG_UMA._Equipment._Shoulder.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Shoulder.ColorPreset;
			_DK_RPG_UMA._Equipment._Shoulder.Color = SetToLoad.SetContent._Male._EquipmentData._Shoulder.Color;
		}
		// Shoulder Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._ShoulderCover.Slot = SetToLoad.SetContent._Female._EquipmentData._ShoulderCover.Slot;
			_DK_RPG_UMA._Equipment._ShoulderCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._ShoulderCover.Overlay;
			_DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._ShoulderCover.ColorPreset;
			_DK_RPG_UMA._Equipment._ShoulderCover.Color = SetToLoad.SetContent._Female._EquipmentData._ShoulderCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._ShoulderCover.Slot = SetToLoad.SetContent._Male._EquipmentData._ShoulderCover.Slot;
			_DK_RPG_UMA._Equipment._ShoulderCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._ShoulderCover.Overlay;
			_DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._ShoulderCover.ColorPreset;
			_DK_RPG_UMA._Equipment._ShoulderCover.Color = SetToLoad.SetContent._Male._EquipmentData._ShoulderCover.Color;
		}
		// Torso Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._TorsoSub.Slot = SetToLoad.SetContent._Female._EquipmentData._TorsoSub.Slot;
			_DK_RPG_UMA._Equipment._TorsoSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._TorsoSub.Overlay;
			_DK_RPG_UMA._Equipment._TorsoSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._TorsoSub.ColorPreset;
			_DK_RPG_UMA._Equipment._TorsoSub.Color = SetToLoad.SetContent._Female._EquipmentData._TorsoSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._TorsoSub.Slot = SetToLoad.SetContent._Male._EquipmentData._TorsoSub.Slot;
			_DK_RPG_UMA._Equipment._TorsoSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._TorsoSub.Overlay;
			_DK_RPG_UMA._Equipment._TorsoSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._TorsoSub.ColorPreset;
			_DK_RPG_UMA._Equipment._TorsoSub.Color = SetToLoad.SetContent._Male._EquipmentData._TorsoSub.Color;
		}
		// Torso
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Torso.Slot = SetToLoad.SetContent._Female._EquipmentData._Torso.Slot;
			_DK_RPG_UMA._Equipment._Torso.Overlay = SetToLoad.SetContent._Female._EquipmentData._Torso.Overlay;
			_DK_RPG_UMA._Equipment._Torso.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Torso.ColorPreset;
			_DK_RPG_UMA._Equipment._Torso.Color = SetToLoad.SetContent._Female._EquipmentData._Torso.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Torso.Slot = SetToLoad.SetContent._Male._EquipmentData._Torso.Slot;
			_DK_RPG_UMA._Equipment._Torso.Overlay = SetToLoad.SetContent._Male._EquipmentData._Torso.Overlay;
			_DK_RPG_UMA._Equipment._Torso.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Torso.ColorPreset;
			_DK_RPG_UMA._Equipment._Torso.Color = SetToLoad.SetContent._Male._EquipmentData._Torso.Color;
		}
		// Torso Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._TorsoCover.Slot = SetToLoad.SetContent._Female._EquipmentData._TorsoCover.Slot;
			_DK_RPG_UMA._Equipment._TorsoCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._TorsoCover.Overlay;
			_DK_RPG_UMA._Equipment._TorsoCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._TorsoCover.ColorPreset;
			_DK_RPG_UMA._Equipment._TorsoCover.Color = SetToLoad.SetContent._Female._EquipmentData._TorsoCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._TorsoCover.Slot = SetToLoad.SetContent._Male._EquipmentData._TorsoCover.Slot;
			_DK_RPG_UMA._Equipment._TorsoCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._TorsoCover.Overlay;
			_DK_RPG_UMA._Equipment._TorsoCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._TorsoCover.ColorPreset;
			_DK_RPG_UMA._Equipment._TorsoCover.Color = SetToLoad.SetContent._Male._EquipmentData._TorsoCover.Color;
		}
		// ArmBand Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._ArmBandSub.Slot = SetToLoad.SetContent._Female._EquipmentData._ArmBandSub.Slot;
			_DK_RPG_UMA._Equipment._ArmBandSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._ArmBandSub.Overlay;
			_DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._ArmBandSub.ColorPreset;
			_DK_RPG_UMA._Equipment._ArmBandSub.Color = SetToLoad.SetContent._Female._EquipmentData._ArmBandSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._ArmBandSub.Slot = SetToLoad.SetContent._Male._EquipmentData._ArmBandSub.Slot;
			_DK_RPG_UMA._Equipment._ArmBandSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._ArmBandSub.Overlay;
			_DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._ArmBandSub.ColorPreset;
			_DK_RPG_UMA._Equipment._ArmBandSub.Color = SetToLoad.SetContent._Male._EquipmentData._ArmBandSub.Color;
		}
		// ArmBand
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._ArmBand.Slot = SetToLoad.SetContent._Female._EquipmentData._ArmBand.Slot;
			_DK_RPG_UMA._Equipment._ArmBand.Overlay = SetToLoad.SetContent._Female._EquipmentData._ArmBand.Overlay;
			_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._ArmBand.ColorPreset;
			_DK_RPG_UMA._Equipment._ArmBand.Color = SetToLoad.SetContent._Female._EquipmentData._ArmBand.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._ArmBand.Slot = SetToLoad.SetContent._Male._EquipmentData._ArmBand.Slot;
			_DK_RPG_UMA._Equipment._ArmBand.Overlay = SetToLoad.SetContent._Male._EquipmentData._ArmBand.Overlay;
			_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._ArmBand.ColorPreset;
			_DK_RPG_UMA._Equipment._ArmBand.Color = SetToLoad.SetContent._Male._EquipmentData._ArmBand.Color;
		}
		// ArmBand Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._ArmBandCover.Slot = SetToLoad.SetContent._Female._EquipmentData._ArmBandCover.Slot;
			_DK_RPG_UMA._Equipment._ArmBandCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._ArmBandCover.Overlay;
			_DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._ArmBandCover.ColorPreset;
			_DK_RPG_UMA._Equipment._ArmBandCover.Color = SetToLoad.SetContent._Female._EquipmentData._ArmBandCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._ArmBandCover.Slot = SetToLoad.SetContent._Male._EquipmentData._ArmBandCover.Slot;
			_DK_RPG_UMA._Equipment._ArmBandCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._ArmBandCover.Overlay;
			_DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._ArmBandCover.ColorPreset;
			_DK_RPG_UMA._Equipment._ArmBandCover.Color = SetToLoad.SetContent._Male._EquipmentData._ArmBandCover.Color;
		}
		// Wrist Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._WristSub.Slot = SetToLoad.SetContent._Female._EquipmentData._WristSub.Slot;
			_DK_RPG_UMA._Equipment._WristSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._WristSub.Overlay;
			_DK_RPG_UMA._Equipment._WristSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._WristSub.ColorPreset;
			_DK_RPG_UMA._Equipment._WristSub.Color = SetToLoad.SetContent._Female._EquipmentData._WristSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._WristSub.Slot = SetToLoad.SetContent._Male._EquipmentData._WristSub.Slot;
			_DK_RPG_UMA._Equipment._WristSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._WristSub.Overlay;
			_DK_RPG_UMA._Equipment._WristSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._WristSub.ColorPreset;
			_DK_RPG_UMA._Equipment._WristSub.Color = SetToLoad.SetContent._Male._EquipmentData._WristSub.Color;
		}
		// Wrist
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Wrist.Slot = SetToLoad.SetContent._Female._EquipmentData._Wrist.Slot;
			_DK_RPG_UMA._Equipment._Wrist.Overlay = SetToLoad.SetContent._Female._EquipmentData._Wrist.Overlay;
			_DK_RPG_UMA._Equipment._Wrist.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Wrist.ColorPreset;
			_DK_RPG_UMA._Equipment._Wrist.Color = SetToLoad.SetContent._Female._EquipmentData._Wrist.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Wrist.Slot = SetToLoad.SetContent._Male._EquipmentData._Wrist.Slot;
			_DK_RPG_UMA._Equipment._Wrist.Overlay = SetToLoad.SetContent._Male._EquipmentData._Wrist.Overlay;
			_DK_RPG_UMA._Equipment._Wrist.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Wrist.ColorPreset;
			_DK_RPG_UMA._Equipment._Wrist.Color = SetToLoad.SetContent._Male._EquipmentData._Wrist.Color;
		}
		// Wrist Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._WristCover.Slot = SetToLoad.SetContent._Female._EquipmentData._WristCover.Slot;
			_DK_RPG_UMA._Equipment._WristCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._WristCover.Overlay;
			_DK_RPG_UMA._Equipment._WristCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._WristCover.ColorPreset;
			_DK_RPG_UMA._Equipment._WristCover.Color = SetToLoad.SetContent._Female._EquipmentData._WristCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._WristCover.Slot = SetToLoad.SetContent._Male._EquipmentData._WristCover.Slot;
			_DK_RPG_UMA._Equipment._WristCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._WristCover.Overlay;
			_DK_RPG_UMA._Equipment._WristCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._WristCover.ColorPreset;
			_DK_RPG_UMA._Equipment._WristCover.Color = SetToLoad.SetContent._Male._EquipmentData._WristCover.Color;
		}
		// Hands Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._HandsSub.Slot = SetToLoad.SetContent._Female._EquipmentData._HandsSub.Slot;
			_DK_RPG_UMA._Equipment._HandsSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._HandsSub.Overlay;
			_DK_RPG_UMA._Equipment._HandsSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._HandsSub.ColorPreset;
			_DK_RPG_UMA._Equipment._HandsSub.Color = SetToLoad.SetContent._Female._EquipmentData._HandsSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._HandsSub.Slot = SetToLoad.SetContent._Male._EquipmentData._HandsSub.Slot;
			_DK_RPG_UMA._Equipment._HandsSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._HandsSub.Overlay;
			_DK_RPG_UMA._Equipment._HandsSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._HandsSub.ColorPreset;
			_DK_RPG_UMA._Equipment._HandsSub.Color = SetToLoad.SetContent._Male._EquipmentData._HandsSub.Color;
		}
		// Hands
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Hands.Slot = SetToLoad.SetContent._Female._EquipmentData._Hands.Slot;
			_DK_RPG_UMA._Equipment._Hands.Overlay = SetToLoad.SetContent._Female._EquipmentData._Hands.Overlay;
			_DK_RPG_UMA._Equipment._Hands.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Hands.ColorPreset;
			_DK_RPG_UMA._Equipment._Hands.Color = SetToLoad.SetContent._Female._EquipmentData._Hands.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Hands.Slot = SetToLoad.SetContent._Male._EquipmentData._Hands.Slot;
			_DK_RPG_UMA._Equipment._Hands.Overlay = SetToLoad.SetContent._Male._EquipmentData._Hands.Overlay;
			_DK_RPG_UMA._Equipment._Hands.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Hands.ColorPreset;
			_DK_RPG_UMA._Equipment._Hands.Color = SetToLoad.SetContent._Male._EquipmentData._Hands.Color;
		}
		// Hands Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._HandsCover.Slot = SetToLoad.SetContent._Female._EquipmentData._HandsCover.Slot;
			_DK_RPG_UMA._Equipment._HandsCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._HandsCover.Overlay;
			_DK_RPG_UMA._Equipment._HandsCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._HandsCover.ColorPreset;
			_DK_RPG_UMA._Equipment._HandsCover.Color = SetToLoad.SetContent._Female._EquipmentData._HandsCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._HandsCover.Slot = SetToLoad.SetContent._Male._EquipmentData._HandsCover.Slot;
			_DK_RPG_UMA._Equipment._HandsCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._HandsCover.Overlay;
			_DK_RPG_UMA._Equipment._HandsCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._HandsCover.ColorPreset;
			_DK_RPG_UMA._Equipment._HandsCover.Color = SetToLoad.SetContent._Male._EquipmentData._HandsCover.Color;
		}
		// Belt Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._BeltSub.Slot = SetToLoad.SetContent._Female._EquipmentData._BeltSub.Slot;
			_DK_RPG_UMA._Equipment._BeltSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._BeltSub.Overlay;
			_DK_RPG_UMA._Equipment._BeltSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._BeltSub.ColorPreset;
			_DK_RPG_UMA._Equipment._BeltSub.Color = SetToLoad.SetContent._Female._EquipmentData._BeltSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._BeltSub.Slot = SetToLoad.SetContent._Male._EquipmentData._BeltSub.Slot;
			_DK_RPG_UMA._Equipment._BeltSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._BeltSub.Overlay;
			_DK_RPG_UMA._Equipment._BeltSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._BeltSub.ColorPreset;
			_DK_RPG_UMA._Equipment._BeltSub.Color = SetToLoad.SetContent._Male._EquipmentData._BeltSub.Color;
		}
		// Belt
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Belt.Slot = SetToLoad.SetContent._Female._EquipmentData._Belt.Slot;
			_DK_RPG_UMA._Equipment._Belt.Overlay = SetToLoad.SetContent._Female._EquipmentData._Belt.Overlay;
			_DK_RPG_UMA._Equipment._Belt.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Belt.ColorPreset;
			_DK_RPG_UMA._Equipment._Belt.Color = SetToLoad.SetContent._Female._EquipmentData._Belt.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Belt.Slot = SetToLoad.SetContent._Male._EquipmentData._Belt.Slot;
			_DK_RPG_UMA._Equipment._Belt.Overlay = SetToLoad.SetContent._Male._EquipmentData._Belt.Overlay;
			_DK_RPG_UMA._Equipment._Belt.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Belt.ColorPreset;
			_DK_RPG_UMA._Equipment._Belt.Color = SetToLoad.SetContent._Male._EquipmentData._Belt.Color;
		}
		// Belt Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._BeltCover.Slot = SetToLoad.SetContent._Female._EquipmentData._BeltCover.Slot;
			_DK_RPG_UMA._Equipment._BeltCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._BeltCover.Overlay;
			_DK_RPG_UMA._Equipment._BeltCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._BeltCover.ColorPreset;
			_DK_RPG_UMA._Equipment._BeltCover.Color = SetToLoad.SetContent._Female._EquipmentData._BeltCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._BeltCover.Slot = SetToLoad.SetContent._Male._EquipmentData._BeltCover.Slot;
			_DK_RPG_UMA._Equipment._BeltCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._BeltCover.Overlay;
			_DK_RPG_UMA._Equipment._BeltCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._BeltCover.ColorPreset;
			_DK_RPG_UMA._Equipment._BeltCover.Color = SetToLoad.SetContent._Male._EquipmentData._BeltCover.Color;
		}
		// Legs Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._LegsSub.Slot = SetToLoad.SetContent._Female._EquipmentData._LegsSub.Slot;
			_DK_RPG_UMA._Equipment._LegsSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._LegsSub.Overlay;
			_DK_RPG_UMA._Equipment._LegsSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._LegsSub.ColorPreset;
			_DK_RPG_UMA._Equipment._LegsSub.Color = SetToLoad.SetContent._Female._EquipmentData._LegsSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._LegsSub.Slot = SetToLoad.SetContent._Male._EquipmentData._LegsSub.Slot;
			_DK_RPG_UMA._Equipment._LegsSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._LegsSub.Overlay;
			_DK_RPG_UMA._Equipment._LegsSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._LegsSub.ColorPreset;
			_DK_RPG_UMA._Equipment._LegsSub.Color = SetToLoad.SetContent._Male._EquipmentData._LegsSub.Color;
		}
		// Legs
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Legs.Slot = SetToLoad.SetContent._Female._EquipmentData._Legs.Slot;
			_DK_RPG_UMA._Equipment._Legs.Overlay = SetToLoad.SetContent._Female._EquipmentData._Legs.Overlay;
			_DK_RPG_UMA._Equipment._Legs.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Legs.ColorPreset;
			_DK_RPG_UMA._Equipment._Legs.Color = SetToLoad.SetContent._Female._EquipmentData._Legs.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Legs.Slot = SetToLoad.SetContent._Male._EquipmentData._Legs.Slot;
			_DK_RPG_UMA._Equipment._Legs.Overlay = SetToLoad.SetContent._Male._EquipmentData._Legs.Overlay;
			_DK_RPG_UMA._Equipment._Legs.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Legs.ColorPreset;
			_DK_RPG_UMA._Equipment._Legs.Color = SetToLoad.SetContent._Male._EquipmentData._Legs.Color;
		}
		// Legs Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._LegsCover.Slot = SetToLoad.SetContent._Female._EquipmentData._LegsCover.Slot;
			_DK_RPG_UMA._Equipment._LegsCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._LegsCover.Overlay;
			_DK_RPG_UMA._Equipment._LegsCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._LegsCover.ColorPreset;
			_DK_RPG_UMA._Equipment._LegsCover.Color = SetToLoad.SetContent._Female._EquipmentData._LegsCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._LegsCover.Slot = SetToLoad.SetContent._Male._EquipmentData._LegsCover.Slot;
			_DK_RPG_UMA._Equipment._LegsCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._LegsCover.Overlay;
			_DK_RPG_UMA._Equipment._LegsCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._LegsCover.ColorPreset;
			_DK_RPG_UMA._Equipment._LegsCover.Color = SetToLoad.SetContent._Male._EquipmentData._LegsCover.Color;
		}
		// LegBand Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._LegBandSub.Slot = SetToLoad.SetContent._Female._EquipmentData._LegBandSub.Slot;
			_DK_RPG_UMA._Equipment._LegBandSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._LegBandSub.Overlay;
			_DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._LegBandSub.ColorPreset;
			_DK_RPG_UMA._Equipment._LegBandSub.Color = SetToLoad.SetContent._Female._EquipmentData._LegBandSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._LegBandSub.Slot = SetToLoad.SetContent._Male._EquipmentData._LegBandSub.Slot;
			_DK_RPG_UMA._Equipment._LegBandSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._LegBandSub.Overlay;
			_DK_RPG_UMA._Equipment._LegBandSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._LegBandSub.ColorPreset;
			_DK_RPG_UMA._Equipment._LegBandSub.Color = SetToLoad.SetContent._Male._EquipmentData._LegBandSub.Color;
		}
		// LegBand
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._LegBand.Slot = SetToLoad.SetContent._Female._EquipmentData._LegBand.Slot;
			_DK_RPG_UMA._Equipment._LegBand.Overlay = SetToLoad.SetContent._Female._EquipmentData._LegBand.Overlay;
			_DK_RPG_UMA._Equipment._LegBand.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._LegBand.ColorPreset;
			_DK_RPG_UMA._Equipment._LegBand.Color = SetToLoad.SetContent._Female._EquipmentData._LegBand.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._LegBand.Slot = SetToLoad.SetContent._Male._EquipmentData._LegBand.Slot;
			_DK_RPG_UMA._Equipment._LegBand.Overlay = SetToLoad.SetContent._Male._EquipmentData._LegBand.Overlay;
			_DK_RPG_UMA._Equipment._LegBand.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._LegBand.ColorPreset;
			_DK_RPG_UMA._Equipment._LegBand.Color = SetToLoad.SetContent._Male._EquipmentData._LegBand.Color;
		}
		// LegBand Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._LegBandCover.Slot = SetToLoad.SetContent._Female._EquipmentData._LegBandCover.Slot;
			_DK_RPG_UMA._Equipment._LegBandCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._LegBandCover.Overlay;
			_DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._LegBandCover.ColorPreset;
			_DK_RPG_UMA._Equipment._LegBandCover.Color = SetToLoad.SetContent._Female._EquipmentData._LegBandCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._LegBandCover.Slot = SetToLoad.SetContent._Male._EquipmentData._LegBandCover.Slot;
			_DK_RPG_UMA._Equipment._LegBandCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._LegBandCover.Overlay;
			_DK_RPG_UMA._Equipment._LegBandCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._LegBandCover.ColorPreset;
			_DK_RPG_UMA._Equipment._LegBandCover.Color = SetToLoad.SetContent._Male._EquipmentData._LegBandCover.Color;
		}
		// Feet Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._FeetSub.Slot = SetToLoad.SetContent._Female._EquipmentData._FeetSub.Slot;
			_DK_RPG_UMA._Equipment._FeetSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._FeetSub.Overlay;
			_DK_RPG_UMA._Equipment._FeetSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._FeetSub.ColorPreset;
			_DK_RPG_UMA._Equipment._FeetSub.Color = SetToLoad.SetContent._Female._EquipmentData._FeetSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._FeetSub.Slot = SetToLoad.SetContent._Male._EquipmentData._FeetSub.Slot;
			_DK_RPG_UMA._Equipment._FeetSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._FeetSub.Overlay;
			_DK_RPG_UMA._Equipment._FeetSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._FeetSub.ColorPreset;
			_DK_RPG_UMA._Equipment._FeetSub.Color = SetToLoad.SetContent._Male._EquipmentData._FeetSub.Color;
		}
		// Feet
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Feet.Slot = SetToLoad.SetContent._Female._EquipmentData._Feet.Slot;
			_DK_RPG_UMA._Equipment._Feet.Overlay = SetToLoad.SetContent._Female._EquipmentData._Feet.Overlay;
			_DK_RPG_UMA._Equipment._Feet.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Feet.ColorPreset;
			_DK_RPG_UMA._Equipment._Feet.Color = SetToLoad.SetContent._Female._EquipmentData._Feet.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Feet.Slot = SetToLoad.SetContent._Male._EquipmentData._Feet.Slot;
			_DK_RPG_UMA._Equipment._Feet.Overlay = SetToLoad.SetContent._Male._EquipmentData._Feet.Overlay;
			_DK_RPG_UMA._Equipment._Feet.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Feet.ColorPreset;
			_DK_RPG_UMA._Equipment._Feet.Color = SetToLoad.SetContent._Male._EquipmentData._Feet.Color;
		}
		// Feet Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._FeetCover.Slot = SetToLoad.SetContent._Female._EquipmentData._FeetCover.Slot;
			_DK_RPG_UMA._Equipment._FeetCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._FeetCover.Overlay;
			_DK_RPG_UMA._Equipment._FeetCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._FeetCover.ColorPreset;
			_DK_RPG_UMA._Equipment._FeetCover.Color = SetToLoad.SetContent._Female._EquipmentData._FeetCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._FeetCover.Slot = SetToLoad.SetContent._Male._EquipmentData._FeetCover.Slot;
			_DK_RPG_UMA._Equipment._FeetCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._FeetCover.Overlay;
			_DK_RPG_UMA._Equipment._FeetCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._FeetCover.ColorPreset;
			_DK_RPG_UMA._Equipment._FeetCover.Color = SetToLoad.SetContent._Male._EquipmentData._FeetCover.Color;
		}


		// Collar Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._CollarSub.Slot = SetToLoad.SetContent._Female._EquipmentData._CollarSub.Slot;
			_DK_RPG_UMA._Equipment._CollarSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._CollarSub.Overlay;
			_DK_RPG_UMA._Equipment._CollarSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._CollarSub.ColorPreset;
			_DK_RPG_UMA._Equipment._CollarSub.Color = SetToLoad.SetContent._Female._EquipmentData._CollarSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._CollarSub.Slot = SetToLoad.SetContent._Male._EquipmentData._CollarSub.Slot;
			_DK_RPG_UMA._Equipment._CollarSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._CollarSub.Overlay;
			_DK_RPG_UMA._Equipment._CollarSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._CollarSub.ColorPreset;
			_DK_RPG_UMA._Equipment._CollarSub.Color = SetToLoad.SetContent._Male._EquipmentData._CollarSub.Color;
		}
		// Collar
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Collar.Slot = SetToLoad.SetContent._Female._EquipmentData._Collar.Slot;
			_DK_RPG_UMA._Equipment._Collar.Overlay = SetToLoad.SetContent._Female._EquipmentData._Collar.Overlay;
			_DK_RPG_UMA._Equipment._Collar.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Collar.ColorPreset;
			_DK_RPG_UMA._Equipment._Collar.Color = SetToLoad.SetContent._Female._EquipmentData._Collar.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Collar.Slot = SetToLoad.SetContent._Male._EquipmentData._Collar.Slot;
			_DK_RPG_UMA._Equipment._Collar.Overlay = SetToLoad.SetContent._Male._EquipmentData._Collar.Overlay;
			_DK_RPG_UMA._Equipment._Collar.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Collar.ColorPreset;
			_DK_RPG_UMA._Equipment._Collar.Color = SetToLoad.SetContent._Male._EquipmentData._Collar.Color;
		}
		// Collar Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._CollarCover.Slot = SetToLoad.SetContent._Female._EquipmentData._CollarCover.Slot;
			_DK_RPG_UMA._Equipment._CollarCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._CollarCover.Overlay;
			_DK_RPG_UMA._Equipment._CollarCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._CollarCover.ColorPreset;
			_DK_RPG_UMA._Equipment._CollarCover.Color = SetToLoad.SetContent._Female._EquipmentData._CollarCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._CollarCover.Slot = SetToLoad.SetContent._Male._EquipmentData._CollarCover.Slot;
			_DK_RPG_UMA._Equipment._CollarCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._CollarCover.Overlay;
			_DK_RPG_UMA._Equipment._CollarCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._CollarCover.ColorPreset;
			_DK_RPG_UMA._Equipment._CollarCover.Color = SetToLoad.SetContent._Male._EquipmentData._CollarCover.Color;
		}
		// RingLeft
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._RingLeft.Slot = SetToLoad.SetContent._Female._EquipmentData._RingLeft.Slot;
			_DK_RPG_UMA._Equipment._RingLeft.Overlay = SetToLoad.SetContent._Female._EquipmentData._RingLeft.Overlay;
			_DK_RPG_UMA._Equipment._RingLeft.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._RingLeft.ColorPreset;
			_DK_RPG_UMA._Equipment._RingLeft.Color = SetToLoad.SetContent._Female._EquipmentData._RingLeft.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._RingLeft.Slot = SetToLoad.SetContent._Male._EquipmentData._RingLeft.Slot;
			_DK_RPG_UMA._Equipment._RingLeft.Overlay = SetToLoad.SetContent._Male._EquipmentData._RingLeft.Overlay;
			_DK_RPG_UMA._Equipment._RingLeft.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._RingLeft.ColorPreset;
			_DK_RPG_UMA._Equipment._RingLeft.Color = SetToLoad.SetContent._Male._EquipmentData._RingLeft.Color;
		}
		// RingRight
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._RingRight.Slot = SetToLoad.SetContent._Female._EquipmentData._RingRight.Slot;
			_DK_RPG_UMA._Equipment._RingRight.Overlay = SetToLoad.SetContent._Female._EquipmentData._RingRight.Overlay;
			_DK_RPG_UMA._Equipment._RingRight.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._RingRight.ColorPreset;
			_DK_RPG_UMA._Equipment._RingRight.Color = SetToLoad.SetContent._Female._EquipmentData._RingRight.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._RingRight.Slot = SetToLoad.SetContent._Male._EquipmentData._RingRight.Slot;
			_DK_RPG_UMA._Equipment._RingRight.Overlay = SetToLoad.SetContent._Male._EquipmentData._RingRight.Overlay;
			_DK_RPG_UMA._Equipment._RingRight.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._RingRight.ColorPreset;
			_DK_RPG_UMA._Equipment._RingRight.Color = SetToLoad.SetContent._Male._EquipmentData._RingRight.Color;
		}
		// Cloak
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Cloak.Slot = SetToLoad.SetContent._Female._EquipmentData._Cloak.Slot;
			_DK_RPG_UMA._Equipment._Cloak.Overlay = SetToLoad.SetContent._Female._EquipmentData._Cloak.Overlay;
			_DK_RPG_UMA._Equipment._Cloak.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Cloak.ColorPreset;
			_DK_RPG_UMA._Equipment._Cloak.Color = SetToLoad.SetContent._Female._EquipmentData._Cloak.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Cloak.Slot = SetToLoad.SetContent._Male._EquipmentData._Cloak.Slot;
			_DK_RPG_UMA._Equipment._Cloak.Overlay = SetToLoad.SetContent._Male._EquipmentData._Cloak.Overlay;
			_DK_RPG_UMA._Equipment._Cloak.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Cloak.ColorPreset;
			_DK_RPG_UMA._Equipment._Cloak.Color = SetToLoad.SetContent._Male._EquipmentData._Cloak.Color;
		}
		// Backpack Sub
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._BackpackSub.Slot = SetToLoad.SetContent._Female._EquipmentData._BackpackSub.Slot;
			_DK_RPG_UMA._Equipment._BackpackSub.Overlay = SetToLoad.SetContent._Female._EquipmentData._BackpackSub.Overlay;
			_DK_RPG_UMA._Equipment._BackpackSub.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._BackpackSub.ColorPreset;
			_DK_RPG_UMA._Equipment._BackpackSub.Color = SetToLoad.SetContent._Female._EquipmentData._BackpackSub.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._BackpackSub.Slot = SetToLoad.SetContent._Male._EquipmentData._BackpackSub.Slot;
			_DK_RPG_UMA._Equipment._BackpackSub.Overlay = SetToLoad.SetContent._Male._EquipmentData._BackpackSub.Overlay;
			_DK_RPG_UMA._Equipment._BackpackSub.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._BackpackSub.ColorPreset;
			_DK_RPG_UMA._Equipment._BackpackSub.Color = SetToLoad.SetContent._Male._EquipmentData._BackpackSub.Color;
		}
		// Backpack
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._Backpack.Slot = SetToLoad.SetContent._Female._EquipmentData._Backpack.Slot;
			_DK_RPG_UMA._Equipment._Backpack.Overlay = SetToLoad.SetContent._Female._EquipmentData._Backpack.Overlay;
			_DK_RPG_UMA._Equipment._Backpack.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._Backpack.ColorPreset;
			_DK_RPG_UMA._Equipment._Backpack.Color = SetToLoad.SetContent._Female._EquipmentData._Backpack.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._Backpack.Slot = SetToLoad.SetContent._Male._EquipmentData._Backpack.Slot;
			_DK_RPG_UMA._Equipment._Backpack.Overlay = SetToLoad.SetContent._Male._EquipmentData._Backpack.Overlay;
			_DK_RPG_UMA._Equipment._Backpack.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._Backpack.ColorPreset;
			_DK_RPG_UMA._Equipment._Backpack.Color = SetToLoad.SetContent._Male._EquipmentData._Backpack.Color;
		}
		// Backpack Cover
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._BackpackCover.Slot = SetToLoad.SetContent._Female._EquipmentData._BackpackCover.Slot;
			_DK_RPG_UMA._Equipment._BackpackCover.Overlay = SetToLoad.SetContent._Female._EquipmentData._BackpackCover.Overlay;
			_DK_RPG_UMA._Equipment._BackpackCover.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._BackpackCover.ColorPreset;
			_DK_RPG_UMA._Equipment._BackpackCover.Color = SetToLoad.SetContent._Female._EquipmentData._BackpackCover.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._BackpackCover.Slot = SetToLoad.SetContent._Male._EquipmentData._BackpackCover.Slot;
			_DK_RPG_UMA._Equipment._BackpackCover.Overlay = SetToLoad.SetContent._Male._EquipmentData._BackpackCover.Overlay;
			_DK_RPG_UMA._Equipment._BackpackCover.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._BackpackCover.ColorPreset;
			_DK_RPG_UMA._Equipment._BackpackCover.Color = SetToLoad.SetContent._Male._EquipmentData._BackpackCover.Color;
		}
		// LeftHand
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._LeftHand.Slot = SetToLoad.SetContent._Female._EquipmentData._LeftHand.Slot;
			_DK_RPG_UMA._Equipment._LeftHand.Overlay = SetToLoad.SetContent._Female._EquipmentData._LeftHand.Overlay;
			_DK_RPG_UMA._Equipment._LeftHand.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._LeftHand.ColorPreset;
			_DK_RPG_UMA._Equipment._LeftHand.Color = SetToLoad.SetContent._Female._EquipmentData._LeftHand.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._LeftHand.Slot = SetToLoad.SetContent._Male._EquipmentData._LeftHand.Slot;
			_DK_RPG_UMA._Equipment._LeftHand.Overlay = SetToLoad.SetContent._Male._EquipmentData._LeftHand.Overlay;
			_DK_RPG_UMA._Equipment._LeftHand.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._LeftHand.ColorPreset;
			_DK_RPG_UMA._Equipment._LeftHand.Color = SetToLoad.SetContent._Male._EquipmentData._LeftHand.Color;
		}
		// RightHand
		if ( _DK_RPG_UMA.Gender == "Female" ){
			_DK_RPG_UMA._Equipment._RightHand.Slot = SetToLoad.SetContent._Female._EquipmentData._RightHand.Slot;
			_DK_RPG_UMA._Equipment._RightHand.Overlay = SetToLoad.SetContent._Female._EquipmentData._RightHand.Overlay;
			_DK_RPG_UMA._Equipment._RightHand.ColorPreset = SetToLoad.SetContent._Female._EquipmentData._RightHand.ColorPreset;
			_DK_RPG_UMA._Equipment._RightHand.Color = SetToLoad.SetContent._Female._EquipmentData._RightHand.Color;
		}
		else {
			_DK_RPG_UMA._Equipment._RightHand.Slot = SetToLoad.SetContent._Male._EquipmentData._RightHand.Slot;
			_DK_RPG_UMA._Equipment._RightHand.Overlay = SetToLoad.SetContent._Male._EquipmentData._RightHand.Overlay;
			_DK_RPG_UMA._Equipment._RightHand.ColorPreset = SetToLoad.SetContent._Male._EquipmentData._RightHand.ColorPreset;
			_DK_RPG_UMA._Equipment._RightHand.Color = SetToLoad.SetContent._Male._EquipmentData._RightHand.Color;
		}
		#endregion Equipment	
	}

}
