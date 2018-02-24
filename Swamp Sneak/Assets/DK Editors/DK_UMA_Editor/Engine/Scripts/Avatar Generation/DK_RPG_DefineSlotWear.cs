using UnityEngine;
# if Editor
using UnityEditor;
# endif
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UMA;

public class DK_RPG_DefineSlotWear : MonoBehaviour {

	public static bool Skipping = false;
	public static List<DKSlotData> AssignedSlotsList = new List<DKSlotData>();
	public static List<DKSlotData> tmpSlotsList = new List<DKSlotData>();
	public static List<DKOverlayData> AssignedOverlayList = new List<DKOverlayData>();
	public static List<DKOverlayData> TmpTorsoOverLayList = new List<DKOverlayData>();
	public static DK_RPG_UMA tmp_DK_RPG_UMA;
	public static DK_RPG_UMA _DK_RPG_UMA;

	public static bool ErrorDetected = false;

	public static DKUMA_Variables _DKUMA_Variables;

	public static void DefineSlots (DK_UMACrowd Crowd){

		ErrorDetected = false;

	//	Debug.Log ("DefineWears");
		// create the RPG script in the UMA avatar
		if ( Crowd.umaData ) DK_RPG_DefineSlotWear.tmp_DK_RPG_UMA = Crowd.umaData.transform.gameObject.GetComponent("DK_RPG_UMA") as DK_RPG_UMA;
		if ( tmp_DK_RPG_UMA ) _DK_RPG_UMA = tmp_DK_RPG_UMA;

		Crowd._FaceOverlay = null;
		Crowd.Randomize.HairDone = false;

		AssignedSlotsList.Clear();
		AssignedOverlayList.Clear();

		if ( DK_UMACrowd.GeneratorMode == "RPG" || DK_UMACrowd.GeneratorMode == "Preset" )
		{
			Crowd.UMAGenerated = false;

			#region Create lists
			AssignedSlotsList.Clear();
			#endregion Create lists

			DKRaceData _Race = Crowd.RaceAndGender.RaceToCreate;
			string _Gender = Crowd.RaceAndGender.Gender;
			List<DKOverlayData> TmpOvlist = new List<DKOverlayData>();

			#region Male
			if ( _Gender == "Male" ){
				#region _Belt
				if ( _Race._Male._EquipmentData._Belt.SlotList.Count > 0 ){
					int ran = UnityEngine.Random.Range (0, _Race._Male._EquipmentData._Belt.SlotList.Count);
					DKSlotData slot = _Race._Male._EquipmentData._Belt.SlotList[ran];
					int ran2 = UnityEngine.Random.Range (0, 100);
					if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
						// assign the overlays list
						TmpOvlist = _Race._Male._EquipmentData._Belt.OverlayList;
						// find color
						Color color =  Crowd.Colors.BeltWearColor;
						// assign the slot and its overlays and color
						DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_Belt", color);	
					}
				}
				#endregion _Belt
				#region _Cloak
				if ( _Race._Male._EquipmentData._Cloak.SlotList.Count > 0 ){
					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.HairAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.BeltWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					if ( _Race._Male._EquipmentData._Cloak.SlotList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, _Race._Male._EquipmentData._Cloak.SlotList.Count);
						DKSlotData slot = _Race._Male._EquipmentData._Cloak.SlotList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Male._EquipmentData._Cloak.OverlayList;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_Cloak", color);	
						}
					}
				}
				#endregion _Cloak

				#region _Feet
				#region _SlotOnly
				if (_Race._Male._EquipmentData._Feet.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Male._EquipmentData._Feet.SlotList )
						if (_SlotData.OverlayType == "FeetWear" 
						    && Crowd.Wears.WearWeightList[4].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						if ( tmpSlotsList.Count == 1 ) ran = 0;
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Male._EquipmentData._Feet.OverlayList;
							// find color
							Color color =  Crowd.Colors.FeetWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_FeetWear", color);	
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( _Race._Male._EquipmentData._Feet.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Male._EquipmentData._Feet.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[4].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.FeetWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_FeetWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly 
				#endregion _Feet
				
				#region _Hands
				#region _SlotOnly
				if (  _Race._Male._EquipmentData._Hands.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Male._EquipmentData._Hands.SlotList )
						if (_SlotData.OverlayType.Contains("Hands") && Crowd.Wears.WearWeightList[2].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Male._EquipmentData._Hands.OverlayList;
							// find color
							Color color =  Crowd.Colors.HandWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_HandsWear", color);
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( _Race._Male._EquipmentData._Hands.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Male._EquipmentData._Hands.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[2].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.HandWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_HandsWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly 
				#endregion _Hands
				
				#region _Head
				#region _SlotOnly
				if ( _Race._Male._EquipmentData._Head.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Male._EquipmentData._Head.SlotList )
						if ((_SlotData.OverlayType.Contains("Head") && Crowd.Wears.WearWeightList[0].Weights.Contains(_SlotData.WearWeight))
					){
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Male._EquipmentData._Head.OverlayList;
							// find color
							Color color =  Crowd.Colors.HeadWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_HeadWear", color);
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( _Race._Male._EquipmentData._Head.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Male._EquipmentData._Head.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[0].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.HeadWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_HeadWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly 
				#endregion _Head

				#region _Torso
				#region _SlotOnly
				if ( _Race._Male._EquipmentData._Torso.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Male._EquipmentData._Torso.SlotList )
						if ((_SlotData.OverlayType.Contains("Torso") && Crowd.Wears.WearWeightList[1].Weights.Contains(_SlotData.WearWeight))
						   ){
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Male._EquipmentData._Torso.OverlayList;
							// find color
							Color color =  Crowd.Colors.HeadWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_TorsoWear", color);
						}
					}
				}
				#endregion _SlotOnly

				#region _OverlayOnly
				else if ( _Race._Male._EquipmentData._Torso.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Male._EquipmentData._Torso.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[1].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.TorsoWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_TorsoWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Torso

				#region _LeftHand
				if ( DK_UMACrowd.GenerateHandled == true && _Race._Male._EquipmentData._LeftHand.SlotList.Count > 0 ){
					int ran = UnityEngine.Random.Range (0, _Race._Male._EquipmentData._LeftHand.SlotList.Count);
					DKSlotData slot = _Race._Male._EquipmentData._LeftHand.SlotList[ran];
					int ran2 = UnityEngine.Random.Range (0, 100);
					if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
						// assign the overlays list
						TmpOvlist = _Race._Male._EquipmentData._LeftHand.OverlayList;
						// find color
						Color color = new Color(1,1,1,1);					// assign the slot and its overlays and color
						DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_LeftHand", color);	
					}
				}
				#endregion _LeftHand
				
				#region _Legs
				#region _SlotOnly
				if ( Crowd.Wears.HideLegs == false && _Race._Male._EquipmentData._Legs.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Male._EquipmentData._Legs.SlotList )
						if (_SlotData.OverlayType.Contains("Legs") && Crowd.Wears.WearWeightList[3].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Male._EquipmentData._Legs.OverlayList;
							// find color
							Color color =  Crowd.Colors.LegsWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_LegsWear", color);
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( Crowd.Wears.HideLegs == false && _Race._Male._EquipmentData._Legs.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Male._EquipmentData._Legs.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[3].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.LegsWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_LegsWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Legs
				
				#region _RightHand
				if ( DK_UMACrowd.GenerateHandled == true && _Race._Male._EquipmentData._RightHand.SlotList.Count > 0 ){
					int ran = UnityEngine.Random.Range (0, _Race._Male._EquipmentData._RightHand.SlotList.Count);
					DKSlotData slot = _Race._Male._EquipmentData._RightHand.SlotList[ran];
					int ran2 = UnityEngine.Random.Range (0, 100);
					if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
						// assign the overlays list
						TmpOvlist = _Race._Male._EquipmentData._RightHand.OverlayList;
						// find color
						Color color = new Color(1,1,1,1);					// assign the slot and its overlays and color
						// assign the slot and its overlays and color
						DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_RightHand", color);	
					}
				}
				#endregion _RightHand
				
				#region _Shoulder
				#region _SlotOnly
				if ( Crowd.Wears.HideShoulders == false && _Race._Male._EquipmentData._Shoulder.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Male._EquipmentData._Shoulder.SlotList )
						if (_SlotData.OverlayType.Contains("Shoulder") && Crowd.Wears.WearWeightList[5].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Male._EquipmentData._Shoulder.OverlayList;
							// find color
							Color color =  Crowd.Colors.TorsoWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_ShoulderWear", color);
						}
					}
				}
				#endregion _SlotOnly

				#region _OverlayOnly
				else if ( _Race._Male._EquipmentData._Shoulder.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Male._EquipmentData._Shoulder.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[5].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.TorsoWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_ShoulderWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Shoulder

				#region _Armband
				#region _SlotOnly
				if ( Crowd.Wears.HideArmBand == false && _Race._Male._EquipmentData._Armband.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Male._EquipmentData._Armband.SlotList )
						if (_SlotData.OverlayType.Contains("Armband") && Crowd.Wears.WearWeightList[5].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Male._EquipmentData._Armband.OverlayList;
							// find color
							Color color =  Crowd.Colors.TorsoWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_Armband", color);	
						}
					}
				}
				#endregion _SlotOnly
				
				#region _OverlayOnly
				else if ( _Race._Male._EquipmentData._Armband.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Male._EquipmentData._Armband.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[5].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}
					
					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.TorsoWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_Armband", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Armband

				#region _Wrist
				#region _SlotOnly
				if ( Crowd.Wears.HideArmBand == false && _Race._Male._EquipmentData._Wrist.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Male._EquipmentData._Wrist.SlotList )
						if (_SlotData.OverlayType.Contains("Wrist") && Crowd.Wears.WearWeightList[5].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Male._EquipmentData._Wrist.OverlayList;
							// find color
							Color color =  Crowd.Colors.TorsoWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_Wrist", color);
						}
					}
				}
				#endregion _SlotOnly
				
				#region _OverlayOnly
				else if ( _Race._Male._EquipmentData._Wrist.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Male._EquipmentData._Wrist.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[5].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}
					
					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.TorsoWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_Wrist", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Wrist

			}
			#endregion Male
		

			#region Female
			if ( _Gender == "Female" ){
				#region _Belt
				if ( _Race._Female._EquipmentData._Belt.SlotList.Count > 0 ){
					int ran = UnityEngine.Random.Range (0, _Race._Female._EquipmentData._Belt.SlotList.Count);
					DKSlotData slot = _Race._Female._EquipmentData._Belt.SlotList[ran];
					int ran2 = UnityEngine.Random.Range (0, 100);
					if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
						// assign the overlays list
						TmpOvlist = _Race._Female._EquipmentData._Belt.OverlayList;
						// find color
						Color color =  Crowd.Colors.BeltWearColor;
						// assign the slot and its overlays and color
						DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_Belt", color);	
					}
				}
				#endregion _Belt
				#region _Cloak
				if ( _Race._Female._EquipmentData._Cloak.SlotList.Count > 0 ){
					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.HairAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.BeltWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					if ( _Race._Female._EquipmentData._Cloak.SlotList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, _Race._Female._EquipmentData._Cloak.SlotList.Count);
						DKSlotData slot = _Race._Female._EquipmentData._Cloak.SlotList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Female._EquipmentData._Cloak.OverlayList;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_Cloak", color);	
						}
					}
				}
				#endregion _Cloak
				#region _Feet
				#region _SlotOnly
				if ( _Race._Female._EquipmentData._Feet.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Female._EquipmentData._Feet.SlotList ){
						try {
						if (_SlotData.OverlayType =="FeetWear" && Crowd.Wears.WearWeightList[4].Weights.Contains(_SlotData.WearWeight))
						{
							tmpSlotsList.Add( _SlotData );
						}	
						}catch ( NullReferenceException e ){ Debug.LogError ( "Race "+_Race.name+" have a missing slot for feet. Use the Element Manager to re prepare the RPG lists by clicking on Add to Lists. ("+e+")" ); }
					}
					if ( tmpSlotsList.Count > 0 ){

						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						if ( tmpSlotsList.Count == 1 ) ran = 0;
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Female._EquipmentData._Feet.OverlayList;
							// find color
							Color color =  Crowd.Colors.FeetWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_FeetWear", color);
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( _Race._Female._EquipmentData._Feet.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear();
					foreach ( DKOverlayData _OverlayData in _Race._Female._EquipmentData._Feet.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[4].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.FeetWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_FeetWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly 
				#endregion _Feet

				#region _Hands
				#region _SlotOnly
				if ( _Race._Female._EquipmentData._Hands.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Female._EquipmentData._Hands.SlotList )
						if (_SlotData.OverlayType.Contains("Hands") && Crowd.Wears.WearWeightList[2].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Female._EquipmentData._Hands.OverlayList;
							// find color
							Color color =  Crowd.Colors.HandWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_HandsWear", color);
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( _Race._Female._EquipmentData._Hands.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear();
					foreach ( DKOverlayData _OverlayData in _Race._Female._EquipmentData._Hands.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[2].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.HandWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_HandsWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly 
				#endregion _Hands

				#region _Head
				#region _SlotOnly
				if ( _Race._Female._EquipmentData._Head.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();

					foreach ( DKSlotData _SlotData in _Race._Female._EquipmentData._Head.SlotList )
						if ((_SlotData.OverlayType.Contains("Head") && Crowd.Wears.WearWeightList[0].Weights.Contains(_SlotData.WearWeight))
						    ){
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							try{
							}catch ( ArgumentOutOfRangeException e ){ Debug.LogError ("Index : "+ran+" Out of range. List count is "
								+tmpSlotsList.Count.ToString()+". "+e); }

							// assign the overlays list
							TmpOvlist = _Race._Female._EquipmentData._Head.OverlayList;
							// find color
							Color color =  Crowd.Colors.HeadWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_HeadWear", color);
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( _Race._Female._EquipmentData._Head.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear();
					foreach ( DKOverlayData _OverlayData in _Race._Female._EquipmentData._Head.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[0].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.HeadWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					TmpOvlist = _Race._Female._EquipmentData._Head.OverlayOnlyList;
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_HeadWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly 
				#endregion _Head

				#region _Torso
				#region _SlotOnly
				if ( _Race._Female._EquipmentData._Torso.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Female._EquipmentData._Torso.SlotList )
						if (_SlotData.OverlayType.Contains("Torso") && Crowd.Wears.WearWeightList[1].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Female._EquipmentData._Torso.OverlayList;
							// find color
							Color color =  Crowd.Colors.TorsoWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_TorsoWear", color);
							if ( slot._HideData.HideShoulders ) Crowd.Wears.HideShoulders = true;
							if ( slot._HideData.HideLegs ) Crowd.Wears.HideLegs = true;
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( _Race._Female._EquipmentData._Torso.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear();
					foreach ( DKOverlayData _OverlayData in _Race._Female._EquipmentData._Torso.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[1].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.TorsoWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_TorsoWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Torso

				#region _LeftHand
				if ( DK_UMACrowd.GenerateHandled == true && _Race._Female._EquipmentData._LeftHand.SlotList.Count > 0 ){
					int ran = UnityEngine.Random.Range (0, _Race._Female._EquipmentData._LeftHand.SlotList.Count);
					DKSlotData slot = _Race._Female._EquipmentData._LeftHand.SlotList[ran];
					// assign the overlays list
					TmpOvlist = _Race._Female._EquipmentData._LeftHand.OverlayList;
					// find color
					Color color = new Color(1,1,1,1);					// assign the slot and its overlays and color
					DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_LeftHand", color);	
					
				}
				#endregion _LeftHand
			
				#region _Legs
				#region _SlotOnly
				if ( Crowd.Wears.HideLegs == false && _Race._Female._EquipmentData._Legs.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Female._EquipmentData._Legs.SlotList )
						if (_SlotData.OverlayType.Contains("Legs") && Crowd.Wears.WearWeightList[3].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Female._EquipmentData._Legs.OverlayList;
							// find color
							Color color =  Crowd.Colors.LegsWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_LegsWear", color);
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( Crowd.Wears.HideLegs == false && _Race._Female._EquipmentData._Legs.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear();
					foreach ( DKOverlayData _OverlayData in _Race._Female._EquipmentData._Legs.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[3].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.LegsWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_LegsWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Legs

				#region _RightHand
				if ( DK_UMACrowd.GenerateHandled == true && _Race._Female._EquipmentData._RightHand.SlotList.Count > 0 ){
					int ran = UnityEngine.Random.Range (0, _Race._Female._EquipmentData._RightHand.SlotList.Count);
					DKSlotData slot = _Race._Female._EquipmentData._RightHand.SlotList[ran];
					// assign the overlays list
					TmpOvlist = _Race._Female._EquipmentData._RightHand.OverlayList;
					// find color
					Color color = new Color(1,1,1,1);					// assign the slot and its overlays and color
					// assign the slot and its overlays and color
					DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_RightHand", color);	
					
				}
				#endregion _RightHand

				#region _Shoulder
				#region _SlotOnly
				if ( _Race._Female._EquipmentData._Shoulder.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Female._EquipmentData._Shoulder.SlotList )
						if (_SlotData.OverlayType.Contains("Shoulder") && Crowd.Wears.WearWeightList[5].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Female._EquipmentData._Shoulder.OverlayList;
							// find color
							Color color =  Crowd.Colors.TorsoWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_ShoulderWear", color);
						}
					}
				}
				#endregion _SlotOnly
				#region _OverlayOnly
				else if ( _Race._Female._EquipmentData._Shoulder.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear();
					foreach ( DKOverlayData _OverlayData in _Race._Female._EquipmentData._Shoulder.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[5].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}

					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.TorsoWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;

					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_ShoulderWear", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Shoulder

				#region _Armband
				#region _SlotOnly
				if ( Crowd.Wears.HideArmBand == false && _Race._Female._EquipmentData._Armband.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Female._EquipmentData._Armband.SlotList )
						if (_SlotData.OverlayType.Contains("Armband") && Crowd.Wears.WearWeightList[5].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Female._EquipmentData._Armband.OverlayList;
							// find color
							Color color =  Crowd.Colors.TorsoWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_Armband", color);	
						}
					}
				}
				#endregion _SlotOnly
				
				#region _OverlayOnly
				else if ( _Race._Female._EquipmentData._Armband.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Female._EquipmentData._Armband.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[5].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}
					
					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.TorsoWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_Armband", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Armband
				
				#region _Wrist
				#region _SlotOnly
				if ( Crowd.Wears.HideArmBand == false && _Race._Female._EquipmentData._Wrist.SlotList.Count > 0 ){
					// WearWeight
					tmpSlotsList.Clear ();
					foreach ( DKSlotData _SlotData in _Race._Female._EquipmentData._Wrist.SlotList )
						if (_SlotData.OverlayType.Contains("Wrist") && Crowd.Wears.WearWeightList[5].Weights.Contains(_SlotData.WearWeight))
					{
						tmpSlotsList.Add( _SlotData );
					}	
					if ( tmpSlotsList.Count > 0 ){
						int ran = UnityEngine.Random.Range (0, tmpSlotsList.Count);
						DKSlotData slot = tmpSlotsList[ran];
						int ran2 = UnityEngine.Random.Range (0, 100);
						if ( ran2 <= slot.Place.dk_SlotsAnatomyElement.SpawnPerct ) {
							// assign the overlays list
							TmpOvlist = _Race._Female._EquipmentData._Wrist.OverlayList;
							// find color
							Color color =  Crowd.Colors.TorsoWearColor;
							// assign the slot and its overlays and color
							DK_RPG_DefineSlotWear.AssigningSlot (Crowd, slot, TmpOvlist, _Race, "_Wrist", color);	
						}
					}
				}
				#endregion _SlotOnly
				
				#region _OverlayOnly
				else if ( _Race._Female._EquipmentData._Wrist.OverlayOnlyList.Count > 0 ){
					TmpOvlist.Clear ();
					foreach ( DKOverlayData _OverlayData in _Race._Female._EquipmentData._Wrist.OverlayOnlyList )
						if ( Crowd.Wears.WearWeightList[5].Weights.Contains(_OverlayData.WearWeight))
					{
						TmpOvlist.Add( _OverlayData );
					}
					
					// color preparation
					float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
					Crowd.Colors.ColorToApply = Crowd.Colors.TorsoWearColor;
					Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
					Color color = Crowd.Colors.ColorToApply + _Adjust;
					
					DKSlotData slot = Crowd.tempSlotList[ Crowd.TorsoIndex ];
					DK_RPG_DefineSlotWear.AssigningOverlay (Crowd, slot, Crowd.TorsoIndex, TmpOvlist, _Race, "_Wrist", true, color);	// assign the slot and its overlays				
				}
				#endregion _OverlayOnly
				#endregion _Wrist
			}
		#endregion Female
		}
		if ( ErrorDetected == false ) Cleaning ( Crowd );
	}

	public static void AssigningSlot (DK_UMACrowd Crowd, DKSlotData slot, List<DKOverlayData> list, DKRaceData _Race, string type, Color color){
		// Verify if not already in the list
		if ( AssignedSlotsList.Contains(slot) == false ){
			// Assign the Slot
			AssignedSlotsList.Add (slot);

			int index = Crowd.tempSlotList.Count;

			// verify if the element is in the library
			if ( Crowd.slotLibrary.slotElementList.Contains (slot) == false ){
				GameObject.Find ("DK_UMA").GetComponent<DKUMA_Variables>().PopulateLibraries ();
				/*
				// add the element if missing from library
				Crowd.slotLibrary.AddSlot (slot.slotName, slot);
				// for UMA
				SlotLibrary _SlotLibrary = GameObject.Find ("SlotLibrary").GetComponent<SlotLibrary>();
				if ( _SlotLibrary.GetAllSlotAssets().ToList().Contains (slot._UMA) == false ){
					Debug.Log ( "Adding "+slot._UMA.name+" to UMA library." );
					_SlotLibrary.AddSlotAsset (slot._UMA);
				}*/
			}
			// add slot to the avatar recipe
			Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot( slot.slotName ));

			// Copy the values
			if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1] == null ) {
				Debug.LogError ( "DK UMA : missing element in the libraries. Aborting the generation of the avatar. The libraries have been verified and corrected. Try to create another avatar." );

				ErrorDetected = true;
				// delete avatar
				try {
				DestroyImmediate (Crowd._DK_Model.gameObject);
				}
				catch (MissingReferenceException){}

			}
			else {
				AssigningOverlay (Crowd, slot, index, list, _Race, type, false, color);
				CopyValues (Crowd, slot, Crowd.tempSlotList.Count-1);
			}
		}
		if ( ErrorDetected == false ) {
			if ( slot.Place.name == "HeadWear" )
			for (int i1 = 0; i1 < Crowd.tempSlotList.Count; i1 ++){
			
				#region if Hide Hair
				if ( slot._HideData.HideHair == true ) {
					if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Hair" ) {
						Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
					}
				}
				#endregion if Hide Hair 


				#region if Hide Hair Module
				if ( slot._HideData.HideHairModule == true ) {
					if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Hair_Module" ) {
						Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
					}
				}
				#endregion if Hide Hair Module 

				#region if Hide Mouth
				if ( slot._HideData.HideMouth == true ) {
					if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Mouth" ) {
						Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
					}
				}
				#endregion if Hide Mouth 

				#region if Hide Beard
				if ( slot._HideData.HideBeard == true ) {
					if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Beard" ) {
						Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
						// TODO hide the legacy
						//	Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]._LegacyData);
					}
				}
				#endregion if Hide Mouth 

				#region if Hide Ears
				if ( slot._HideData.HideEars == true ) {
					if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Ears" ) {
						Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
					}
				}
				#endregion if Hide Ears
			}
		}
		EnsureSlot (slot);
	}

	public static void EnsureSlot (DKSlotData slot){
		if ( slot != null && slot._UMA != null ){
			UMA.SlotLibrary lib = FindObjectOfType<UMA.SlotLibrary>();
			// Cleaning library
			if ( lib.GetAllSlotAssets().ToList().Contains(null) == true ) {
				if ( _DKUMA_Variables == null )
					_DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();
				_DKUMA_Variables.CleanUMASlotsLibrary ();
			}
			if ( lib.HasSlot(slot._UMA.name) == false ) {
				lib.AddSlotAsset (slot._UMA);
				Debug.Log ("DK UMA Fix lib : Adding slot "+slot._UMA.name+" to UMA library.");
			}
		}
	}

	public static DKOverlayData overlay;
	public static void AssigningOverlay (DK_UMACrowd Crowd, DKSlotData slot, int index, List<DKOverlayData> list, DKRaceData _Race, string type, bool OverlayOnly, Color color){
		Color ColorToApply = color;
		ColorPresetData _ColorPreset = null;


		// choose a linked overlay only
		if ( !OverlayOnly && slot.LinkedOverlayList.Count > 0 ){
			int ranOv = UnityEngine.Random.Range (0, slot.LinkedOverlayList.Count);
			overlay = slot.LinkedOverlayList[ranOv];

			// verify if the element is missing from library
			if ( Crowd.overlayLibrary.overlayElementList.Contains (slot.LinkedOverlayList[ranOv]) == false ){
				// add to library if missing
				Crowd.overlayLibrary.AddOverlay (slot.LinkedOverlayList[ranOv].overlayName, slot.LinkedOverlayList[ranOv]);

				// for UMA
				#if UMADCS
				OverlayLibrary _OverlayLibrary = FindObjectOfType<DynamicOverlayLibrary>();
				if ( _OverlayLibrary.GetAllOverlayAssets().ToList().Contains (overlay._UMA) == false ){
					_OverlayLibrary.AddOverlayAsset (overlay._UMA);
				}
				#else
				OverlayLibrary _OverlayLibrary = FindObjectOfType<OverlayLibrary>();
				if ( _OverlayLibrary.GetAllOverlayAssets().ToList().Contains (overlay._UMA) == false ){
					_OverlayLibrary.AddOverlayAsset (overlay._UMA);
				}
				#endif
			}

			// find color presets if available
			if ( overlay.ColorPresets.Count > 0 ){
				int ranColorPreset = UnityEngine.Random.Range (0, overlay.ColorPresets.Count);
				ColorToApply = overlay.ColorPresets[ranColorPreset].PresetColor;
				_ColorPreset = overlay.ColorPresets[ranColorPreset];

				// adjust the color
				float ranAdj = 0;
				ranAdj = UnityEngine.Random.Range (0.001f, Crowd.Colors.WearAdjRanMaxi);
				Color adj = new Color (ranAdj,ranAdj,ranAdj);
				ColorToApply = ColorToApply + adj;
			}

			// Verify if not already in the list
			if ( type.Contains("Wear") && Crowd.tempSlotList[index] != null && Crowd.tempSlotList[index].overlayList.Count > 0 ) {
				Crowd.tempSlotList[index].overlayList.Clear();
			}

			// Assign the Slot
			//	AssignedOverlayList.Add (overlay);
			if ( Crowd.tempSlotList[index] != null && overlay != null ) {

				Crowd.tempSlotList[index].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(overlay.overlayName,ColorToApply));
				Crowd.tempSlotList[index].overlayList[Crowd.tempSlotList[index].overlayList.Count-1].ColorPresets = overlay.ColorPresets;

				if ( DK_RPG_UMA_Generator.AddToRPG == true ) AddToRPG (Crowd, slot, overlay, type, false, ColorToApply, _ColorPreset );
			}
			else {
				Debug.LogError ( "DK UMA : missing element in the libraries. Aborting the generation of the avatar. The libraries have been verified and corrected. Try to create another avatar." );
		
			}
		}
		// if no linked overlay, choose a random overlay
		else {
			if ( list.Count > 0 ) {
				int ranOv = UnityEngine.Random.Range (0, list.Count);
				overlay = list[ranOv];

				if ( overlay.ColorPresets.Count > 0 ){
					int ranColorPreset = UnityEngine.Random.Range (0, overlay.ColorPresets.Count);
					ColorToApply = overlay.ColorPresets[ranColorPreset].PresetColor;
					_ColorPreset = overlay.ColorPresets[ranColorPreset];

					// adjust the color
					float ranAdj = 0;
					ranAdj = UnityEngine.Random.Range (0.001f, Crowd.Colors.WearAdjRanMaxi);
					Color adj = new Color (ranAdj,ranAdj,ranAdj);
					ColorToApply = ColorToApply + adj;
				}

				// Verify if not already in the list
			//	if ( type.Contains("Wear") && Crowd.tempSlotList[index].overlayList.Count > 0 ) {
			//		Crowd.tempSlotList[index].overlayList.Clear();
					//	Debug.Log ( "Clearing list" );
			//	}
					// Assign to Slot
				//	AssignedOverlayList.Add (overlay);
					Crowd.tempSlotList[index].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(overlay.overlayName,ColorToApply));
				if ( DK_RPG_UMA_Generator.AddToRPG == true ) AddToRPG (Crowd, slot, overlay, type, true, ColorToApply, _ColorPreset );
			}
		}
		EnsureOverlay (overlay);
	}

	public static void EnsureOverlay (DKOverlayData ov){
		if ( ov != null && ov._UMA != null ){
			UMA.OverlayLibrary lib = FindObjectOfType<UMA.OverlayLibrary>();
			// Cleaning library
			if ( lib.GetAllOverlayAssets().ToList().Contains(null) == true ) {
				if ( _DKUMA_Variables == null )
					_DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();
				_DKUMA_Variables.CleanUMAOverlaysLibrary ();
			}
			if ( lib.HasOverlay(ov._UMA.name) == false ) {
				lib.AddOverlayAsset (ov._UMA);
				Debug.Log ("DK UMA Fix lib : Adding Overlay "+ov._UMA.name+" to UMA library.");
			}
		}
	}

	public static void AddToRPG ( DK_UMACrowd Crowd, DKSlotData slot, DKOverlayData overlay, string type, bool OverlayOnly, Color color, ColorPresetData _ColorPreset){
		// Add the slot to the RPG values of the Avatar
		// assigning the element to the Character
		if ( type == "_Belt" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._Belt.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._Belt.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Belt.Color = color;
				tmp_DK_RPG_UMA._Equipment._Belt.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._Belt.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Belt.Color = color;
				tmp_DK_RPG_UMA._Equipment._Belt.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_HeadWear" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._Head.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._Head.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Head.Color = color;
				tmp_DK_RPG_UMA._Equipment._Head.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._Head.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Head.Color = color;
				tmp_DK_RPG_UMA._Equipment._Head.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_ShoulderWear" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._Shoulder.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._Shoulder.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Shoulder.Color = color;
				tmp_DK_RPG_UMA._Equipment._Shoulder.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._Shoulder.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Shoulder.Color = color;
				tmp_DK_RPG_UMA._Equipment._Shoulder.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_Armband" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._ArmBand.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._ArmBand.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._ArmBand.Color = color;
				tmp_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._ArmBand.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._ArmBand.Color = color;
				tmp_DK_RPG_UMA._Equipment._ArmBand.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_Wrist" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._Wrist.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._Wrist.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Wrist.Color = color;
				tmp_DK_RPG_UMA._Equipment._Wrist.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._Wrist.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Wrist.Color = color;
				tmp_DK_RPG_UMA._Equipment._Wrist.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_TorsoWear" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._Torso.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._Torso.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Torso.Color = color;
				tmp_DK_RPG_UMA._Equipment._Torso.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._Torso.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Torso.Color = color;
				tmp_DK_RPG_UMA._Equipment._Torso.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_HandsWear" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._Hands.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._Hands.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Hands.Color = color;
				tmp_DK_RPG_UMA._Equipment._Hands.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._Hands.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Hands.Color = color;
				tmp_DK_RPG_UMA._Equipment._Hands.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_LegsWear" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._Legs.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._Legs.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Legs.Color = color;
				tmp_DK_RPG_UMA._Equipment._Legs.ColorPreset = _ColorPreset;

				if ( slot._HideData.HideUnderwear == true ){
				//	foreach( DKOverlayData _overlay in slot.overlayList){
				//	for(int i1 = 0; i1 < tmpSlotsList[Crowd.TorsoIndex].overlayList.Count; i1 ++){
				//		if ( _overlay.OverlayType == "Underwear" ){
					//		tmpSlotsList[Crowd.TorsoIndex].overlayList.Remove(tmpSlotsList[Crowd.TorsoIndex].overlayList[i1]);
				//		}
				//	}
				}
			}
			else
			{ 
				tmp_DK_RPG_UMA._Equipment._Legs.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Legs.Color = color;
				tmp_DK_RPG_UMA._Equipment._Legs.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_FeetWear" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._Feet.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._Feet.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Feet.Color = color;
				tmp_DK_RPG_UMA._Equipment._Feet.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._Feet.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Feet.Color = color;
				tmp_DK_RPG_UMA._Equipment._Feet.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_Cloak" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._Cloak.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._Cloak.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Cloak.Color = color;
				tmp_DK_RPG_UMA._Equipment._Cloak.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._Cloak.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._Cloak.Color = color;
				tmp_DK_RPG_UMA._Equipment._Cloak.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_LeftHand" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._LeftHand.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._LeftHand.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._LeftHand.Color = color;
				tmp_DK_RPG_UMA._Equipment._LeftHand.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._LeftHand.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._LeftHand.Color = color;
				tmp_DK_RPG_UMA._Equipment._LeftHand.ColorPreset = _ColorPreset;
			}
		}
		if ( type == "_RightHand" ){
			if ( OverlayOnly == false ){
				tmp_DK_RPG_UMA._Equipment._RightHand.Slot = slot;
				tmp_DK_RPG_UMA._Equipment._RightHand.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._RightHand.Color = color;
				tmp_DK_RPG_UMA._Equipment._RightHand.ColorPreset = _ColorPreset;
			}
			else{ 
				tmp_DK_RPG_UMA._Equipment._RightHand.Overlay = overlay;
				tmp_DK_RPG_UMA._Equipment._RightHand.Color = color;
				tmp_DK_RPG_UMA._Equipment._RightHand.ColorPreset = _ColorPreset;
			}
		}
	}

	public static void CopyValues (DK_UMACrowd Crowd, DKSlotData slot, int index){
		if ( Crowd.tempSlotList[index] == null ) Debug.LogError ( "DK UMA : missing element in the libraries. Aborting the generation of the avatar. The libraries have been verified and corrected. Try to create another avatar." );
		else {
			Crowd.tempSlotList[index].OverlayType = slot.OverlayType;
			Crowd.tempSlotList[index]._UMA = slot._UMA;
			Crowd.tempSlotList[index].Place = slot.Place;
			Crowd.tempSlotList[index].Replace = slot.Replace;
			Crowd.tempSlotList[index]._HideData.HideHair = slot._HideData.HideHair;
			Crowd.tempSlotList[index]._HideData.HideHairModule = slot._HideData.HideHairModule;
			Crowd.tempSlotList[index]._HideData.HideLegs = slot._HideData.HideLegs;
			Crowd.tempSlotList[index]._HideData.HideMouth = slot._HideData.HideMouth;
			Crowd.tempSlotList[index]._HideData.HideShoulders = slot._HideData.HideShoulders;
			Crowd.tempSlotList[index]._HideData.HideBeard = slot._HideData.HideBeard;
			Crowd.tempSlotList[index]._HideData.HideEars = slot._HideData.HideEars;
			Crowd.tempSlotList[index]._LegacyData.HasLegacy = slot._LegacyData.HasLegacy;
			Crowd.tempSlotList[index]._LegacyData.LegacyList = slot._LegacyData.LegacyList;
			Crowd.tempSlotList[index]._LegacyData.IsLegacy = slot._LegacyData.IsLegacy;
			Crowd.tempSlotList[index]._LegacyData.ElderList = slot._LegacyData.ElderList;
			Crowd.tempSlotList[index]._LOD.IsLOD0 = slot._LOD.IsLOD0;
			Crowd.tempSlotList[index]._LOD.LOD1 = slot._LOD.LOD1;
			Crowd.tempSlotList[index]._LOD.LOD2 = slot._LOD.LOD2;
			Crowd.tempSlotList[index]._LOD.MasterLOD = slot._LOD.MasterLOD;
		}
	}

	public static void Cleaning( DK_UMACrowd Crowd ){
		#region Finishing
		List<DKSlotData> ToRemoveList = new List<DKSlotData>();
		#region Cleaning Avatar
		foreach( DKSlotData slot in Crowd.tempSlotList){
			#region if Replace activated
			if ( slot == null ) Debug.LogError ( "DK UMA : missing element in the libraries. Aborting the generation of the avatar. The libraries have been verified and corrected. Try to create another avatar." );
			else {
				if ( slot.Replace == true ) {
					for(int i1 = 0; i1 < Crowd.tempSlotList.Count; i1 ++){
						if ( slot.Place.dk_SlotsAnatomyElement.Place == Crowd.tempSlotList[i1].Place ) {
							ToRemoveList.Add(Crowd.tempSlotList[i1]);
						}
					}
				}
				#endregion if Replace activated

				#region hide shoulders
				// detect 'hide shoulders'
				if ( slot._HideData.HideShoulders ){
					Crowd.Wears.HideShoulders = true;
				}
				// detect the shoulders
				if ( !slot.Place ) Debug.LogError ( "Slot "+slot.slotName+" has no place, you have to fix it." );
				else
				if ( slot.Place.name == "ShoulderWear" ){
					Crowd.Wears.Shoulders = slot;
				//	ToRemoveList.Add(slot);
				}
				#endregion hide shoulders

				#region hide Armband
				// detect 'hide shoulders'
				if ( slot._HideData.HideArmBand ){
					Crowd.Wears.HideShoulders = true;
				}
				// detect the shoulders
				if ( !slot.Place ) Debug.LogError ( "Slot "+slot.slotName+" has no place, you have to fix it." );
				else
				if ( slot.Place.name == "ShoulderWear" ){
					Crowd.Wears.Shoulders = slot;
					//	ToRemoveList.Add(slot);
				}
				#endregion hide shoulders

				#region hide LegWear
				// detect 'hide Legs'
				if ( slot._HideData.HideLegs ){
					Crowd.Wears.HideLegs = true;
				}
				// detect the shoulders
				if ( slot.Place.name == "LegsWear" ){
					Crowd.Wears.Legs = slot;
				}
				#endregion hide LegWear

				#region hide Handled
				// detect 'hide Handled'
				if (  DK_UMACrowd.GenerateWear == false && DK_UMACrowd.GenerateHandled == false ){
					if ( slot.Place.name.Contains("Handled") == true ){
						ToRemoveList.Add( slot );
					}
				}
				#endregion hide Handled
				#region hide underwear
				// detect 'hide underwear'
				if ( slot._HideData.HideUnderwear == true ){
					Debug.Log ("test _HideData.HideUnderwear");
					Crowd.Wears.HideUnderwear = true;
				}
				#endregion hide underwear
			}
		}


	/*	// detect the underwear
		if ( Crowd.Wears.HideUnderwear){
			Debug.Log ("test hide underwear 1");
			for(int i1 = 0; i1 < tmpSlotsList[Crowd.TorsoIndex].overlayList.Count; i1 ++){
				if ( tmpSlotsList[Crowd.TorsoIndex].overlayList[i1].OverlayType == "Underwear" ){
					Debug.Log ("test hide underwear 2");

					tmpSlotsList[Crowd.TorsoIndex].overlayList.Remove(tmpSlotsList[Crowd.TorsoIndex].overlayList[i1]);

				}
			}
		}*/

		#region Legacy
		for(int i = 0; i < Crowd.tempSlotList.Count; i ++){
			// detect Legacy
			DKSlotData Slot = Crowd.tempSlotList[i];

			if (// Slot != null && 
				Slot._LegacyData.HasLegacy == true ) {
				if ( Slot._LegacyData.LegacyList.Count > 0 ){
					foreach ( DKSlotData LegacySlot in Slot._LegacyData.LegacyList ){
						// select the overlay
						try {
							if ( LegacySlot.overlayList.Count > 0
							    || (LegacySlot.OverlayType != null && LegacySlot.OverlayType == "Flesh") )
							{
								//				Ran = UnityEngine.Random.Range(0, LegacySlot.overlayList.Count-1);
								//	LegacySlot = Slot._LegacyData.LegacyList[i4];
							}

							#region Choose Color to apply
							if ( LegacySlot.OverlayType == "Hair" ) Crowd.Colors.ColorToApply =Crowd.Colors.HairColor;
							else if ( LegacySlot.OverlayType == "Beard" ) Crowd.Colors.ColorToApply =Crowd.Colors.HairColor;
							else if ( LegacySlot.OverlayType == "Eyes" ) Crowd.Colors.ColorToApply =Crowd.Colors.EyesColor;
							else if ( LegacySlot.OverlayType == "Face" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
							else if ( LegacySlot.OverlayType == "Flesh" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
							else if ( LegacySlot.Place && LegacySlot.Place.name == "InnerMouth" ) Crowd.Colors.ColorToApply =Crowd.Colors.InnerMouthColor;
							else {
								Crowd.Colors.ColorToApply = new Color (UnityEngine.Random.Range(0.01f,0.9f),UnityEngine.Random.Range(0.01f,0.9f),UnityEngine.Random.Range(0.01f,0.9f));
							}
							#endregion Choose Color to apply
							
							// add
							// For flesh
							if ( LegacySlot.OverlayType != null && LegacySlot.OverlayType == "Flesh" ) {
								bool AlreadyIn = false;
								DKSlotData placeHolder = ScriptableObject.CreateInstance("DKSlotData") as DKSlotData;
								foreach ( DKSlotData slot in Crowd.tempSlotList ){
									if ( slot.slotName == LegacySlot.slotName ) AlreadyIn = true;
									if ( slot.Place == LegacySlot.Place ) {
										placeHolder = slot;
									}
								}

								if ( AssignedSlotsList.Contains(LegacySlot) == true ) AlreadyIn = true;
								if ( !AlreadyIn ){
									DKSlotData slot = Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName);
									Crowd.tempSlotList.Add(slot);
									slot._LegacyData.IsLegacy = true;
									slot._LegacyData.ElderList.Add(Crowd.tempSlotList[Crowd.tempSlotList.Count-1]);

									Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName));

									TmpTorsoOverLayList = Crowd.tempSlotList[Crowd.TorsoIndex].overlayList;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList = TmpTorsoOverLayList;
									AssignedSlotsList.Add (LegacySlot);
									Crowd.umaData.tmpRecipeList = Crowd.tempSlotList;
								}
								// del placeHolder if necessary
								if ( LegacySlot._LegacyData.Replace ) {
									ToRemoveList.Add (placeHolder);
								}
							}

							// for Wear and hair
							else {
								if ( Slot.OverlayType == "Hair" ) Debug.Log ("Hair legacy");
								float c1 = UnityEngine.Random.Range(0.01f,0.9f);
								float c2 = UnityEngine.Random.Range(0.01f,0.9f);
								float c3 = UnityEngine.Random.Range(0.01f,0.9f);
								Crowd.Colors.ColorToApply = new Color (c1,c2,c3);
								DKSlotData slot = Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName);
								Crowd.tempSlotList.Add(slot);
								slot._LegacyData.IsLegacy = true;
								slot._LegacyData.ElderList.Add(Crowd.tempSlotList[Crowd.tempSlotList.Count-1]);

								if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count == 0 ){

									// define legacy slot's overlay
									if ( LegacySlot.LinkedOverlayList.Count > 0 ) {
										int ran = UnityEngine.Random.Range(0, LegacySlot.LinkedOverlayList.Count-1);

										// define color preset
										if (LegacySlot.LinkedOverlayList[ran].ColorPresets.Count > 0) {
											int ran2 = UnityEngine.Random.Range(0, LegacySlot.LinkedOverlayList[ran].ColorPresets.Count-1);

											// get elder color for metal
											if ( Slot.overlayList.Count > 0 && Slot.overlayList[0].ColorPresets.Count > 0
											    && Slot.overlayList[0].ColorPresets[0].OverlayType == "Metal" ){

												float colorModif = 0.05f;
												Crowd.Colors.ColorToApply = Slot.overlayList[0].color + (new Color ( colorModif, colorModif, colorModif ));
											}
											else
												// ran own color presets
												Crowd.Colors.ColorToApply = LegacySlot.LinkedOverlayList[ran].ColorPresets[ran2].PresetColor;
										
										}
										AssignedOverlayList.Add (LegacySlot.LinkedOverlayList[ran]);
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(LegacySlot.LinkedOverlayList[ran].name,Crowd.Colors.ColorToApply));
									}
								}
							}
						}
						catch (System.NullReferenceException) {
							if ( LegacySlot == null ){
								Debug.LogError ( "slot '"+Slot.slotName+"' Legacy can't be generated. The legacy slot is missing. Verify the setting of '"+Slot.slotName+"' about the legacy. Skipping the legacy for "+Slot.slotName);
								#if UNITY_EDITOR
								//	EditorWindow.GetWindow(typeof(DK_UMA_Error_Win), false, "DK Errors");
								//	DK_UMA_Error_Win.AddError_LegacyNotFound();
								#endif
							}
						}
					}
				}
			}
		}
		
		for(int i = 0; i < Crowd.tempSlotList.Count; i ++){
			// detect Legacy
			DKSlotData Slot = Crowd.tempSlotList[i];
			// remove the legacy item if the elder is missing / deleted
			if ( Slot != null && Slot._LegacyData.IsLegacy == true ) {
				foreach ( DKSlotData ElderSlot in Slot._LegacyData.ElderList ){
					if ( Crowd.tempSlotList.Contains(ElderSlot) == false 
					    || ToRemoveList.Contains(ElderSlot) == true ) ToRemoveList.Add (Slot);
				}
			}
		}
		#endregion Legacy

		if ( Crowd.Wears.HideShoulders == true ) Crowd.tempSlotList.Remove (Crowd.Wears.Shoulders);
		if ( Crowd.Wears.HideLegs == true ) Crowd.tempSlotList.Remove (Crowd.Wears.Legs);
		Crowd.Wears.HideShoulders = false;
		Crowd.Wears.HideLegs = false;

		// clear the list of place holders
		foreach (DKSlotData placeHolder in ToRemoveList ){
			Crowd.tempSlotList.Remove (placeHolder);
			//	Debug.Log ("removing "+placeHolder.slotName );
		}

		// Clean the slots of the avatar if no overlay	
		//	for(int i = 0; i < Crowd.tempSlotList.Count; i ++){
		//		if ( Crowd.tempSlotList[i].overlayList.Count == 0 ) Crowd.tempSlotList.Remove(Crowd.tempSlotList[i]);
		//	}
		#endregion Cleaning Avatar

		Finishing ( Crowd );
		#endregion Finishing
	}

	public static void Finishing ( DK_UMACrowd Crowd ){
	//	Debug.Log ( "Finishing RPG wear" );
		DK_DefineSlotFinishing.Finish(Crowd);
	
	}
}
