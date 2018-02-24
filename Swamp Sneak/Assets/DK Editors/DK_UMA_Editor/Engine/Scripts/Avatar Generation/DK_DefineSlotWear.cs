using UnityEngine;
# if Editor
using UnityEditor;
# endif
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;


public class DK_DefineSlotWear : MonoBehaviour {

	public static bool Skipping = false;
	public static List<DKSlotData> AssignedSlotsList = new List<DKSlotData>();
	public static List<DKOverlayData> AssignedOverlayList = new List<DKOverlayData>();



	public static void DefineSlots (DK_UMACrowd Crowd){

		#region Creating
		Crowd._FaceOverlay = null;
		Crowd.Randomize.HairDone = false;
		List<DKOverlayData> TmpTorsoOverLayList = new List<DKOverlayData>();
		
		#region  DK UMA Editor
		if ( DK_UMACrowd.GeneratorMode == "Preset"  )
		{
		//	Debug.Log ("Test");
			Crowd.UMAGenerated = false;
			#region Find the Active Preset
			// find the Active Preset
			GameObject _GenPresets = GameObject.Find( "Generator Presets");
			foreach ( Transform Preset in _GenPresets.transform ) {
				DK_GeneratorPresetLibrary _DK_GeneratorPresetLibrary = Preset.GetComponent< DK_GeneratorPresetLibrary >();
				// Assign active Preset
				if ( _DK_GeneratorPresetLibrary.IsActivePreset == true ) Crowd.ActivePresetLib = _DK_GeneratorPresetLibrary;
			}
			#endregion Find the Active Preset
			
			#region Create lists
			// Create a tmp list for the slots to generate
		//	Crowd.tempSlotList = new List<DKSlotData>();Crowd.tempSlotList.Clear();
			
			// Create a tmp list for the slots to random
			List<DKSlotData> Tmplist = new List<DKSlotData>();
			List<DKSlotData> TmpWearlist = new List<DKSlotData>();
			List<DKSlotData> TmpMeshlist = new List<DKSlotData>();

			// Create a tmp list for the Overlay to random
			List<DKOverlayData> TmpOvLaylist = new List<DKOverlayData>();
			List<DKOverlayData> TmpOvLayUnderwearlist = new List<DKOverlayData>();
			List<DKOverlayData> TmpOvLayWearlist = new List<DKOverlayData>();
			List<DKOverlayData> TmpOvLayMeshlist = new List<DKOverlayData>();


			TmpTorsoOverLayList.Clear();
			AssignedSlotsList.Clear();
			#endregion Create lists
			
			Crowd.NewProcess = true;
			if ( Crowd.NewProcess ){
				
				#region Populate lists
				
				#endregion Populate lists
				
			}
			if ( Crowd.NewProcess || !Crowd.NewProcess ){
			//	Debug.Log ("Test");
				#region Torso Only and first
				for(int i0 = 0; i0 < Crowd.ActivePresetLib.dk_SlotsAnatomyElementList.Length; i0 ++){
					if ( Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] == null ){
						Debug.LogError ( "An Anatomy Part is missing from the current Generator preset '"+Crowd.ActivePresetLib.name
						                +"', You have to delete the missing field from the preset.");
					}
					else 
					{
						DK_Race TmpAnato = Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0].gameObject.GetComponent<DK_Race>();
						if ( TmpAnato.OverlayType == "Is Torso Part" ){
							Crowd.Wears.RanActivatewear = UnityEngine.Random.Range(0, Crowd.Wears.RanWearYesMax );
							Crowd.Wears.RanActivateMesh = UnityEngine.Random.Range(0, 3 ); 
							
							for(int i1 = 0; i1 < Crowd.slotLibrary.slotElementList.Length; i1 ++){
								DKSlotData _SlotData = Crowd.slotLibrary.slotElementList[i1];
								if ( Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0].dk_SlotsAnatomyElement != null && !_SlotData ) {
									Debug.LogError ( "Can not find the SlotData from the Library '"+Crowd.slotLibrary.name+"' : Verify your Library for missing Elements. Delete the field or drag and drop the corresponding missing Element. The missing Element is ignored and the generation continues." );
								}
								else
								if ( !_SlotData ) {
								}
								else
									if ( _SlotData.Place && _SlotData.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
									    && (_SlotData.Race.Contains(Crowd.RaceAndGender.Race) ==  true )
									    && _SlotData.OverlayType.Contains("Wear")
									    && _SlotData._LegacyData.IsLegacy == false
									    && _SlotData.Active == true
									    && Crowd.Wears.RanWearChoice
									    && ( Crowd.RaceAndGender.Gender == _SlotData.Gender )
									    && (_SlotData.Gender == TmpAnato.Gender || TmpAnato.Gender == "Both") )
								{	

									// WearWeight
									Debug.Log ("Test");
									if ((_SlotData.OverlayType.Contains("Head") && Crowd.Wears.WearWeightList[0].Weights.Contains(_SlotData.WearWeight))
									    ||(_SlotData.OverlayType.Contains("Torso") && Crowd.Wears.WearWeightList[1].Weights.Contains(_SlotData.WearWeight))
									    ||(_SlotData.OverlayType.Contains("Hands") && Crowd.Wears.WearWeightList[2].Weights.Contains(_SlotData.WearWeight))
									    ||(_SlotData.OverlayType.Contains("Legs") && Crowd.Wears.WearWeightList[3].Weights.Contains(_SlotData.WearWeight))
									    ||(_SlotData.OverlayType.Contains("Feet") && Crowd.Wears.WearWeightList[4].Weights.Contains(_SlotData.WearWeight))
									    ||(_SlotData.OverlayType.Contains("Shoulder") && Crowd.Wears.WearWeightList[5].Weights.Contains(_SlotData.WearWeight)))
									{
										TmpMeshlist.Add( Crowd.slotLibrary.slotElementList[i1] );
										TmpMeshlist[TmpMeshlist.Count-1].Place = Crowd.slotLibrary.slotElementList[i1].Place;
									}
								}
							}

							if ( Crowd.Wears.RanActivateMesh >= 1 && Crowd.Wears.RanWearChoice && TmpMeshlist.Count != 0 ) {
								int Ran = UnityEngine.Random.Range(0, TmpMeshlist.Count );
								// Assign the Slot
								if ( Crowd.Wears.RanActivateMesh >= 1 && AssignedSlotsList.Contains(TmpMeshlist[Ran]) == false )
								{
									AssignedSlotsList.Add (TmpMeshlist[Ran]);
									Crowd.tempSlotList.Add (Crowd.slotLibrary.InstantiateSlot(TmpMeshlist[Ran].slotName));
									# if Editor
											Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Preview = Tmplist[Ran].Preview;
											# endif
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Place = TmpMeshlist[Ran].Place;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Replace = TmpMeshlist[Ran].Replace;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType = TmpMeshlist[Ran].OverlayType;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.HasLegacy = TmpMeshlist[Ran]._LegacyData.HasLegacy;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.LegacyList = TmpMeshlist[Ran]._LegacyData.LegacyList;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.IsLegacy = TmpMeshlist[Ran]._LegacyData.IsLegacy;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.ElderList = TmpMeshlist[Ran]._LegacyData.ElderList;
									Debug.Log ("adding "+TmpMeshlist[Ran].slotName);
									Crowd.WearMeshIndex = Crowd.tempSlotList.Count - 1;
								}
								else Crowd.WearMeshIndex = 9999;
							}
							else Crowd.WearMeshIndex = 9999;
							
							#region Find Overlays for the chosen slot
							for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
								DKOverlayData _TmpOv = Crowd.overlayLibrary.overlayElementList[i2];
								if ( Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0].dk_SlotsAnatomyElement != null && !_TmpOv ) {
									Debug.LogError ( "Can not find the Overlay from the Library '"+Crowd.overlayLibrary.name+"' : Verify your Library for missing Elements. Delete the field or drag and drop the corresponding missing Element. The missing Element is ignored and the generation continues." );
								}
								else{

									#region Underwear list only
									if ( _TmpOv.OverlayType == ("Underwear")
									    && Crowd.Wears.RanUnderwearChoice
									    && ( _TmpOv.Gender == Crowd.RaceAndGender.Gender || _TmpOv.Gender == "Both" )
									    && _TmpOv.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0]
									    && _TmpOv.Active == true){
										TmpOvLayUnderwearlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
									}
									#endregion Underwear list only
									
									#region wear list only
									if ( _TmpOv.OverlayType != null
									    && _TmpOv.OverlayType.Contains("Wear")
									    && Crowd.Wears.RanWearChoice
									    && Crowd.Wears.RanActivatewear >= 1 
									    && ( _TmpOv.Gender == Crowd.RaceAndGender.Gender|| _TmpOv.Gender == "Both" )
									    && _TmpOv.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0]
									    && _TmpOv.Active == true){
										// WearWeight
										if ((Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Torso") && Crowd.Wears.WearWeightList[1].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
										    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Head") && Crowd.Wears.WearWeightList[0].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
										    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Hands") && Crowd.Wears.WearWeightList[2].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
										    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Legs") && Crowd.Wears.WearWeightList[3].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
										    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Feet") && Crowd.Wears.WearWeightList[4].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
										    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Shoulder") && Crowd.Wears.WearWeightList[5].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight)))
										{
											TmpOvLayWearlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
										}
									}
									#endregion wear list only
								}
							}

							#region Assign Underwear overlay only
							if ( Crowd.Wears.RanUnderwearChoice ){
								int Ran3 = UnityEngine.Random.Range(0, TmpOvLayUnderwearlist.Count );
								bool AlreadyIn;
								AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.TorsoIndex].overlayList[i].overlayName == TmpOvLayUnderwearlist[Ran3].overlayName ){
										AlreadyIn = true;
									}
								}
								if ( AlreadyIn != true ){
									Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLayUnderwearlist[Ran3].name,new Color(UnityEngine.Random.Range(0.1f,0.9f) ,UnityEngine.Random.Range(0.1f,0.9f),UnityEngine.Random.Range(0.1f,0.9f),1)));
								}
							}
							#endregion Assign Underwear overlay only
							
							#region Assign Torso wear overlay only_DK_Model.
							if ( Crowd.Wears.WearOverlays && Crowd.Wears.RanWearChoice && Crowd.Wears.RanActivatewear >= 1 && TmpOvLayWearlist.Count != 0){
								int Ran3 = UnityEngine.Random.Range(0, TmpOvLayWearlist.Count );
								bool AlreadyIn;
								AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.TorsoIndex].overlayList[i].overlayName == TmpOvLayWearlist[Ran3].overlayName ){
										AlreadyIn = true;
									}
								}
								if ( AlreadyIn != true ){
									int ran = UnityEngine.Random.Range(0,100);
									float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.WearAdjRanMaxi);
									Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
									if ( ran >= 50 ) Crowd.Colors.ColorToApply = Crowd.Colors.TorsoWearColor + _Adjust;
									else Crowd.Colors.ColorToApply = Crowd.Colors.LegsWearColor + _Adjust;
									//	if ( Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Count == 0 )
									Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLayWearlist[Ran3].name,Crowd.Colors.ColorToApply));
								}
							}
							#endregion Assign Torso wear overlay only

							#region Assign Torso wear Mesh overlay only
							if ( Crowd.Wears.WearMeshes && Crowd.Wears.RanWearChoice && Crowd.Wears.RanActivateMesh >= 1 && TmpOvLayMeshlist.Count != 0 && Crowd.WearMeshIndex != 9999  ){
								int Ran3 = UnityEngine.Random.Range(0, TmpOvLayMeshlist.Count );
								bool AlreadyIn;
								AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.WearMeshIndex].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.WearMeshIndex].overlayList[i].overlayName == TmpOvLayMeshlist[Ran3].overlayName ){
										AlreadyIn = true;
									}
								}
								if ( AlreadyIn != true ){
									float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.WearAdjRanMaxi);
									Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
									Crowd.Colors.TorsoWearColor = Crowd.Colors.TorsoWearColor + _Adjust;
									if ( Crowd.tempSlotList[Crowd.WearMeshIndex].overlayList.Count == 0 )
										Crowd.tempSlotList[Crowd.WearMeshIndex].overlayList.Add(new DKOverlayData(Crowd.overlayLibrary, TmpOvLayMeshlist[Ran3].name) { color = Crowd.Colors.TorsoWearColor });
									Debug.Log ( "adding "+TmpOvLayMeshlist[Ran3].name);
								}
							}
							#endregion Assign Torso wear Mesh overlay only
							
							#endregion Find Overlays for the chosen slot
							TmpTorsoOverLayList = Crowd.tempSlotList[Crowd.TorsoIndex].overlayList;
						}
					}
				}
				#endregion Torso Only and first

				/*-------------------*/
				
				for(int i0 = 0; i0 < Crowd.ActivePresetLib.dk_SlotsAnatomyElementList.Length; i0 ++){
					// assign Elements DK_Race
					if ( Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] == null ){}
					else {
						Crowd.Anato_DK_Race = Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0].gameObject.GetComponent<DK_Race>();
						Crowd.Race_DK_Race = Crowd.RaceAndGender.RaceToCreate;
						
						#region all Anatomy Part
						int RanSpawn = UnityEngine.Random.Range(0, 100 );
						if ( Crowd.Anato_DK_Race.OverlayType != "Is Torso Part" 
						    && Crowd.Anato_DK_Race.OverlayType != "blocked"
						    //    && Crowd.Anato_DK_Race.OverlayType != "Hair"
						    && ( Crowd.Anato_DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd.Anato_DK_Race.Gender ==  "Both" )
						    && ( Crowd.Anato_DK_Race.Gender == Crowd.Race_DK_Race.Gender || Crowd.Anato_DK_Race.Gender == "Both" )
						    && ( Crowd.Anato_DK_Race.Race.Contains(Crowd.RaceAndGender.Race) ==  true  )
						    && RanSpawn <= Crowd.Anato_DK_Race.SpawnPerct )
						{
							#region Find slots for the Anatomy part
							Tmplist.Clear();
							TmpMeshlist.Clear();
							for(int i1 = 0; i1 < Crowd.slotLibrary.slotElementList.Length; i1 ++){
								Crowd.Slot_DK_Race = Crowd.slotLibrary.slotElementList[i1];
								if ( !Crowd.Slot_DK_Race ) {}
								else
									if ( Crowd.Slot_DK_Race.Place
									    && Crowd.Slot_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
									    && ( Crowd.Slot_DK_Race.Race.Contains(Crowd.RaceAndGender.Race) ==  true  )
									    && Crowd.Slot_DK_Race.Active == true
									    &&  Crowd.Slot_DK_Race._LegacyData.IsLegacy == false
									    && ( Crowd.Slot_DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd.Slot_DK_Race.Gender == "Both" ) )
								{
									if ( Crowd.Wears.RanWearAct ) Crowd.Wears.RanWearYes = UnityEngine.Random.Range(0, Crowd.Wears.RanWearYesMax/2 );
									#region if wear Slot
									
									if (  Crowd.Wears.WearMeshes && Crowd.Wears.RanWearChoice && Crowd.Slot_DK_Race.OverlayType.Contains("Wear") ){
										if ( Crowd.Wears.RanWearYes >= 1 ){
											if ((Crowd.slotLibrary.slotElementList[i1].OverlayType.Contains("Head") && Crowd.Wears.WearWeightList[0].Weights.Contains(Crowd.slotLibrary.slotElementList[i1].WearWeight))
											    ||(Crowd.slotLibrary.slotElementList[i1].OverlayType.Contains("Torso") && Crowd.Wears.WearWeightList[2].Weights.Contains(Crowd.slotLibrary.slotElementList[i1].WearWeight))
											    ||(Crowd.slotLibrary.slotElementList[i1].OverlayType.Contains("Hands") && Crowd.Wears.WearWeightList[2].Weights.Contains(Crowd.slotLibrary.slotElementList[i1].WearWeight))
											    ||(Crowd.slotLibrary.slotElementList[i1].OverlayType.Contains("Legs") && Crowd.Wears.WearWeightList[3].Weights.Contains(Crowd.slotLibrary.slotElementList[i1].WearWeight))
											    ||(Crowd.slotLibrary.slotElementList[i1].OverlayType.Contains("Feet") && Crowd.Wears.WearWeightList[4].Weights.Contains(Crowd.slotLibrary.slotElementList[i1].WearWeight))
											    ||(Crowd.slotLibrary.slotElementList[i1].OverlayType.Contains("Shoulder") && Crowd.Wears.WearWeightList[5].Weights.Contains(Crowd.slotLibrary.slotElementList[i1].WearWeight)))
											{
												Tmplist.Add( Crowd.slotLibrary.slotElementList[i1] );
												Tmplist[Tmplist.Count-1].Place = Crowd.Slot_DK_Race.Place;
												
											}
										}
									}
									#endregion if wear Slot
								}

							}
						}
						#endregion Find slots for the Anatomy part
						
						#region Assign randomly to the Anatomy part
						if ( Tmplist.Count > 0 ) {
							int Ran = UnityEngine.Random.Range(0, Tmplist.Count );
							Crowd.Slot_DK_Race = Tmplist[Ran];
							// Assign the Slot
							if ( (Crowd.Anato_DK_Race.OverlayType != "Is Torso Part" 
							      || Tmplist[Ran].OverlayType.Contains("Wear"))
							    && Crowd.Slot_DK_Race.Place.name != "Head" 
							    //   && Crowd.Slot_DK_Race.Elem != true 
							    )
							{
								#region if NOT mesh Slot

								#region Other Slot
								bool AlreadyIn = false;
								bool placeOccupied = false;
								foreach ( DKSlotData slot in Crowd.tempSlotList ){
									if ( slot.name == Tmplist[Ran].name ) AlreadyIn = true;
									if ( slot.Place == Tmplist[Ran].Place ) placeOccupied = true;
								}
								if ( !placeOccupied && !AlreadyIn ){
									if ( AssignedSlotsList.Contains(Tmplist[Ran]) == false ){
										AssignedSlotsList.Add (Tmplist[Ran]);
										Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot(Tmplist[Ran].slotName ));
										# if Editor
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Preview = Tmplist[Ran].Preview;
										# endif
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Place = Tmplist[Ran].Place;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Replace = Tmplist[Ran].Replace;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType = Tmplist[Ran].OverlayType;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.HasLegacy = Tmplist[Ran]._LegacyData.HasLegacy;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.LegacyList = Tmplist[Ran]._LegacyData.LegacyList;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.IsLegacy = Tmplist[Ran]._LegacyData.IsLegacy;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.ElderList = Tmplist[Ran]._LegacyData.ElderList;
										
										Crowd.CurrentSlot = Tmplist[Ran];
										Crowd.CurrentSlotIndex = Crowd.tempSlotList.Count-1;
										//	Debug.Log (Crowd.Anato_DK_Race.gameObject.name+ " adding slot "+Tmplist[Ran].slotName );
									}
								}
								#endregion Other Slot

								if ( Crowd.CurrentSlot._HideData.HideShoulders ) {
									Crowd.Wears.HideShoulders = true;
									//	Debug.Log ("Hidding");
								}
								if ( Crowd.CurrentSlot._HideData.HideLegs ) {
									Crowd.Wears.HideLegs = true;
									//	Debug.Log ("Hidding");
								}

								for(int i1 = 0; i1 < Crowd.tempSlotList.Count; i1 ++){
									#region if Replace activated
									if ( Crowd.CurrentSlot.Replace == true ) {
										//	Debug.Log ( "Replace Active" );
										if ( Crowd.tempSlotList[i1].Place == Crowd.CurrentSlot.Place.dk_SlotsAnatomyElement.Place
										    ) {
											Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
											
										}
									}
									#endregion if Replace activated
									#region if Hide Hair
									if ( Crowd.CurrentSlot._HideData.HideHair == true ) {
										if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Hair" ) {
											Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
											
										}
									}
									#endregion if Hide Hair 
									#region if Hide Hair Module
									if ( Crowd.CurrentSlot._HideData.HideHairModule == true ) {
										if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Hair_Module" ) {
											Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
											
										}
									}
									#endregion if Hide Hair Module 
									#region if Hide Mouth
									if ( Crowd.CurrentSlot._HideData.HideMouth == true ) {
										if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Mouth" ) {
											Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
											
										}
									}
									#endregion if Hide Mouth 
									#region if Hide Beard
									if ( Crowd.CurrentSlot._HideData.HideBeard == true ) {
										if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Beard" ) {
											Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);

											
										}
									}
									#endregion if Hide Mouth 
									#region if Hide Ears
									if ( Crowd.CurrentSlot._HideData.HideEars == true ) {
										if ( Crowd.tempSlotList[i1].Place && Crowd.tempSlotList[i1].Place.name == "Ears" ) {
											Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
											
										}
									}
									#endregion if Hide Ears
								}
							}
							#endregion if NOT mesh Slot
							
							Tmplist.Clear();
							TmpWearlist.Clear();
							TmpMeshlist.Clear();
							#endregion Assign randomly to the Anatomy part
							
							#region Find Overlays for the chosen slot
							#region Linked Slots Only
							if ( Crowd.CurrentSlot 
							    && Crowd.UseLinkedOv 
							    && Crowd.CurrentSlot.overlayList.Count > 0 
							    && Crowd.CurrentSlot.OverlayType != "Face" )
							{
								
								#region Choose Color to apply
								if ( Crowd.CurrentSlot.OverlayType == "TorsoWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
								else if ( Crowd.CurrentSlot.OverlayType == "LegsWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.LegsWearColor;
								else if ( Crowd.CurrentSlot.OverlayType == "HandWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.HandWearColor;
								else if ( Crowd.CurrentSlot.OverlayType == "FeetWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.FeetWearColor;
								else if ( Crowd.CurrentSlot.OverlayType == "ShoulderWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
								#endregion Choose Color to apply
								
								//	Debug.Log ( Crowd.CurrentSlot.name+" / "+ Crowd.CurrentSlot.overlayList.Count.ToString());
								
								List<DKOverlayData> TmpLinkedToSlot = new List<DKOverlayData>();
								TmpLinkedToSlot = Crowd.CurrentSlot.overlayList;
								
								int Ran2 = UnityEngine.Random.Range(0, (TmpLinkedToSlot.Count) );
								bool AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList[i].overlayName == TmpLinkedToSlot[Ran2].overlayName ){
										AlreadyIn = true;
										Crowd.ToDelete = Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList[i];
									}
								}
								
								if ( Crowd.CurrentSlot.OverlayType.Contains("Wear") == true ){
									float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.WearAdjRanMaxi);
									Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
									Crowd.Colors.ColorToApply = Crowd.Colors.ColorToApply + _Adjust;
								}
								if ( AlreadyIn != true ){
									// add new
									//	if (// Crowd.CurrentSlot.OverlayType.Contains("Wear") == true && 
									//	    Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count == 0 ) 
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpLinkedToSlot[Ran2].overlayName,Crowd.Colors.ColorToApply));
									//	Debug.Log ( Ran2+" / " +tempSlotList[Crowd.tempSlotList.Count-1].slotName+" Linked to "+TmpLinkedToSlot[Ran2].overlayName+" Color("+Colors.ColorToApply.ToString()+")");
								}
								else/* if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count == 0 )*/{
									//	tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Remove(ToDelete);
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Clear();
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpLinkedToSlot[Ran2].overlayName,Crowd.Colors.ColorToApply));
									//	tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Remove(ToDelete);
									//	Debug.Log ( "Already In ! "+Ran2+"/ " +tempSlotList[Crowd.tempSlotList.Count-1].slotName+" Linked to "+TmpLinkedToSlot[Ran2].overlayName+", deleting "+ToDelete.overlayName+" Color("+Colors.ColorToApply.ToString()+")");
								}
							}
							#endregion Linked Slots Only
							
							// For not linked Slots
							else {
								for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
									Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
									if ( !Crowd.Ov_DK_Race || !Crowd.Slot_DK_Race ) {
									}else
									if ( ( Crowd.Slot_DK_Race.Gender == Crowd.Ov_DK_Race.Gender ||Crowd.Ov_DK_Race.Gender == "Both" )
									    && (Crowd.Ov_DK_Race.Race.Contains(Crowd.RaceAndGender.Race) ==  true )
									    &&Crowd.Ov_DK_Race.Active == true
									    && ( Crowd.Slot_DK_Race.Place ==Crowd.Ov_DK_Race.Place || Crowd.Anato_DK_Race.Place ==Crowd.Ov_DK_Race.Place ) )
									{

										if ( Crowd.Slot_DK_Race.LinkedOverlayList.Count == 0 ) {
											#region add overlays for all Slots to lists

											#region Wear only
											if ( Crowd.Wears.WearOverlays
											    && Crowd.Ov_DK_Race.Place == Crowd.Slot_DK_Race.Place 
											    && Crowd.Ov_DK_Race.OverlayType.Contains("Wear") 
											    && Crowd.Ov_DK_Race.OverlayType != ("Underwear")
											    && Crowd.Slot_DK_Race.Active == true
											    && Crowd.Wears.RanWearYes >= 1 
											    && Crowd.Wears.RanWearChoice )
											{
												if ( TmpOvLayWearlist.Contains(Crowd.overlayLibrary.overlayElementList[i2]) )
												{}
												else
													// WearWeight
													if ((Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Head") && Crowd.Wears.WearWeightList[0].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
													    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Torso") && Crowd.Wears.WearWeightList[2].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
													    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Hands") && Crowd.Wears.WearWeightList[2].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
													    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Legs") && Crowd.Wears.WearWeightList[3].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
													    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Feet") && Crowd.Wears.WearWeightList[4].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight))
													    ||(Crowd.overlayLibrary.overlayElementList[i2].OverlayType.Contains("Shoulder") && Crowd.Wears.WearWeightList[5].Weights.Contains(Crowd.overlayLibrary.overlayElementList[i2].WearWeight)))
												{
													TmpOvLayWearlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
												}
											}
											#endregion Wear only
											#region underwear only
											if ( Crowd.Ov_DK_Race.OverlayType == ("Underwear") 
											    && Crowd.Slot_DK_Race.Active == true )
											{
												if ( TmpOvLayUnderwearlist.Contains(Crowd.overlayLibrary.overlayElementList[i2]) )
												{}
												else
													TmpOvLayUnderwearlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
											}
											#endregion underwear only
											
											#endregion all Slot to list
										}
									}
								}
								#endregion Find Overlays for the chosen slot
								
								#region Assign Overlay randomly to the chosen slot

									
								#region add All	Overlays
									
								if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].slotName.Contains("Eyes") == false  )
								{
									#region Commun overlay assign( no wear or mesh )
									if ( Crowd.CurrentSlot.OverlayType.Contains("Wear") == false
									    && Crowd.CurrentSlot.OverlayType != "Face"
									    && Crowd.CurrentSlot.OverlayType != "Flesh" 
									    && Crowd.CurrentSlot.OverlayType != "Hair"
									    && TmpOvLaylist.Count > 0 )
									{
										int Ran2 = UnityEngine.Random.Range(0, TmpOvLaylist.Count );
										bool AlreadyIn;
										AlreadyIn = false;
										for(int i = 0; i <  Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count; i++){
											if ( Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList[i].overlayName == TmpOvLaylist[Ran2].overlayName ){
												AlreadyIn = true;
											}
										}
										if ( AlreadyIn != true ){
											Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLaylist[Ran2].name,Color.black));
											//	Debug.Log ( TmpOvLaylist[Ran2].name + " overlay assign Commun to "+tempSlotList[Crowd.CurrentSlotIndex].slotName);
										}
									}
									#endregion Commun overlay assign ( no wear or mesh )

									#region Wear overlay assign
									if ( Crowd.CurrentSlot.OverlayType == "Flesh"
									    && Crowd.Wears.RanWearYes >= 1 
									    && Crowd.Wears.RanWearChoice 
									    && TmpOvLayWearlist.Count > 0 )
									{
										int Ran2 = UnityEngine.Random.Range(0, TmpOvLayWearlist.Count );
										// colors
										if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Torso") )  Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Legs") )  Crowd.Colors.ColorToApply =Crowd.Colors.LegsWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Hand") )  Crowd.Colors.ColorToApply =Crowd.Colors.HandWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Feet") )  Crowd.Colors.ColorToApply =Crowd.Colors.FeetWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Head") )  Crowd.Colors.ColorToApply =Crowd.Colors.HeadWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Shoulder") )  Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
										
										// assign
										if ( Crowd.Wears.RanWearYes >= 1 )
										{
											if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Torso") == true
											    && TmpOvLayWearlist[Ran2].OverlayType.Contains("Torso") )
											{
												bool AlreadyIn;
												AlreadyIn = false;
												for(int i = 0; i <  Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Count; i++){
													if ( Crowd.tempSlotList[Crowd.TorsoIndex].overlayList[i].overlayName == TmpOvLayWearlist[Ran2].overlayName ){
														AlreadyIn = true;
													}
												}
												if ( AlreadyIn != true ){
													
													Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLayWearlist[Ran2].name,Crowd.Colors.ColorToApply));
													//	tempSlotList[Crowd.TorsoIndex].overlayList.Add(new DKOverlayData(Crowd.overlayLibrary, TmpOvLayWearlist[Ran2].name) { color = Crowd.Colors.ColorToApply });
													Debug.Log ( TmpOvLayWearlist[Ran2].name + " overlay assign Torso Wear to "+Crowd.CurrentSlot.name);
												}
											}
											else if (Crowd.Wears.RanWearYes >= 1 
											         && Crowd.CurrentSlot.Place.ToString().Contains("Torso") == false
											         && TmpOvLayWearlist[Ran2].OverlayType.Contains("Torso") == false)
											{
												bool AlreadyIn;
												AlreadyIn = false;
												for(int i = 0; i <  Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Count; i++){
													if ( Crowd.tempSlotList[Crowd.TorsoIndex].overlayList[i].overlayName == TmpOvLayWearlist[Ran2].overlayName ){
														//	AlreadyIn = true;
													}
												}
												if ( AlreadyIn != true ){
													float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.WearAdjRanMaxi);
													Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
													Crowd.Colors.ColorToApply = Crowd.Colors.ColorToApply + _Adjust;
													if (Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Count == 0 ){
														Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLayWearlist[Ran2].name,Crowd.Colors.ColorToApply));
														Debug.Log ( "adding "+TmpOvLayWearlist[Ran2].name);
													}
												}
											}
										}
									}
									#endregion Wear overlay assign
									
									#region for Wear slots only assign
									if ( Crowd.CurrentSlot.OverlayType.Contains("Wear") == true 
									    && Crowd.Wears.RanWearChoice 
									    && TmpOvLayWearlist.Count > 0 )
									{
										if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Torso") )  Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Legs") )  Crowd.Colors.ColorToApply =Crowd.Colors.LegsWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Hand") )  Crowd.Colors.ColorToApply =Crowd.Colors.HandWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Feet") )  Crowd.Colors.ColorToApply =Crowd.Colors.FeetWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Head") )  Crowd.Colors.ColorToApply =Crowd.Colors.HeadWearColor;
										else if ( Crowd.Wears.RanWearYes >= 1 && Crowd.CurrentSlot.Place.ToString().Contains("Shoulder") )  Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
										
										
										int Ran2 = UnityEngine.Random.Range(0, TmpOvLayWearlist.Count );
										if ( TmpOvLayWearlist[Ran2].OverlayType.Contains("Torso") )  Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
										else
											if ( TmpOvLayWearlist[Ran2].OverlayType.Contains("Legs") )  Crowd.Colors.ColorToApply =Crowd.Colors.LegsWearColor;
										
										if ( TmpOvLayWearlist[Ran2].Place == Crowd.Slot_DK_Race.Place )
										{
											if ( TmpOvLayWearlist[Ran2].OverlayType.Contains("Torso") ){
												bool AlreadyIn;
												AlreadyIn = false;
												for(int i = 0; i <  Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count; i++){
													if ( Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList[i].overlayName == TmpOvLayWearlist[Ran2].overlayName ){
														AlreadyIn = true;
													}
												}
												if ( AlreadyIn != true ){
													float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.WearAdjRanMaxi);
													Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
													Crowd.Colors.ColorToApply = Crowd.Colors.ColorToApply + _Adjust;
													if ( Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count == 0 )
														Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Add(new DKOverlayData(Crowd.overlayLibrary, TmpOvLayWearlist[Ran2].name) { color = Crowd.Colors.ColorToApply });
													//	Debug.Log ( TmpOvLayWearlist[Ran2].name + " overlay assign Torso Wear to "+CurrentSlot.name);
												}
											}
											else{
												bool AlreadyIn;
												AlreadyIn = false;
												for(int i = 0; i <  Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count; i++){
													if ( Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList[i].overlayName == TmpOvLayWearlist[Ran2].overlayName ){
														AlreadyIn = true;
													}
												}
												if ( AlreadyIn != true ){
													float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.WearAdjRanMaxi);
													Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
													Crowd.Colors.ColorToApply = Crowd.Colors.ColorToApply + _Adjust;
													if ( Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count == 0 )
														Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Add(new DKOverlayData(Crowd.overlayLibrary, TmpOvLayWearlist[Ran2].name) { color = Crowd.Colors.ColorToApply });
													//	Debug.Log ( TmpOvLayWearlist[Ran2].name + " overlay assign Leg Wear to "+CurrentSlot.name);
												}
											}
										}
									}
									#endregion for Wear slots only assign
									
									TmpOvLayWearlist.Clear();
									TmpOvLayMeshlist.Clear();
									TmpOvLaylist.Clear();
									Crowd.Wears.RanActivateMesh = 0;
									
								}
								#endregion add All Overlays
							}
							#endregion Assign Overlay randomly to the chosen slot
						}	
					}
					#endregion all Anatomy Part	
				}
			}
		}
		#endregion DK UMA Editor
		#endregion Creating
		
		#region Finishing
		List<DKSlotData> ToRemoveList = new List<DKSlotData>();
		#region Cleaning Avatar


		#region Legacy
		for(int i = 0; i < Crowd.tempSlotList.Count; i ++){
			#region if Replace activated
			if ( Crowd.tempSlotList[i].Replace == true ) {
				for(int i1 = 0; i1 < Crowd.tempSlotList.Count; i1 ++){
					if ( Crowd.tempSlotList[i].Place.dk_SlotsAnatomyElement.Place == Crowd.tempSlotList[i1].Place ) {
						ToRemoveList.Add ( Crowd.tempSlotList[i1] );
					}
				}
			}
			#endregion if Replace activated
			
			if ( Crowd.tempSlotList[i].Place != null ) {
				#region hide shoulders
				// detect 'hide shoulders'
				if ( Crowd.tempSlotList[i]._HideData.HideShoulders ){
					Crowd.Wears.HideShoulders = true;
				}
				// detect the shoulders
				if ( Crowd.tempSlotList[i].Place.name == "ShoulderWear" ){
					Crowd.Wears.Shoulders = Crowd.tempSlotList[i];
				}
				#endregion hide shoulders
				
				#region hide LegWear
				// detect 'hide Legs'
				if ( Crowd.tempSlotList[i]._HideData.HideLegs ){
					Crowd.Wears.HideLegs = true;
				}
				// detect the shoulders
				if ( Crowd.tempSlotList[i].Place.name == "LegsWear" ){
					Crowd.Wears.Legs = Crowd.tempSlotList[i];
				}
				#endregion hide LegWear
				
				#region hide Handled
				// detect 'hide Handled'
				if ( DK_UMACrowd.GenerateHandled == false ){
					if ( Crowd.tempSlotList[i].Place.name.Contains("Handled") == true ){
						//	Crowd.tempSlotList.Remove( Crowd.tempSlotList[i3]);
						ToRemoveList.Add ( Crowd.tempSlotList[i] );
						
					}
				}
				#endregion hide Handled
			}
			else{
				//	Debug.Log (Crowd.tempSlotList[i3].slotName+" place umpty");
			}

			// detect Legacy
			DKSlotData Slot = Crowd.tempSlotList[i];
			if ( Slot._LegacyData.HasLegacy == true ) {
				if ( Slot._LegacyData.LegacyList.Count > 0 ){
				//	int Ran = 0;
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
							else if ( LegacySlot.OverlayType == "TorsoWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
							else if ( LegacySlot.OverlayType == "LegsWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.LegsWearColor;
							else if ( LegacySlot.OverlayType == "HandWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.HandWearColor;
							else if ( LegacySlot.OverlayType == "FeetWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.FeetWearColor;
							else if ( LegacySlot.OverlayType == "ShoulderWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
							else if ( LegacySlot.OverlayType == "Eyes" ) Crowd.Colors.ColorToApply =Crowd.Colors.EyesColor;
							else if ( LegacySlot.OverlayType == "Face" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
							else if ( LegacySlot.OverlayType == "Flesh" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
							else if ( LegacySlot.Place && LegacySlot.Place.name == "InnerMouth" ) Crowd.Colors.ColorToApply =Crowd.Colors.InnerMouthColor;
							else {
								// assign Linked overlay color first 
								if ( LegacySlot.LinkedOverlayList.Count > 0 && LegacySlot.LinkedOverlayList[0].ColorPresets.Count > 0 ) 
									Crowd.Colors.ColorToApply = LegacySlot.LinkedOverlayList[0].ColorPresets[0].PresetColor;
								// or assign white color
								else Crowd.Colors.ColorToApply = Color.white;
							}
							
							
							#endregion Choose Color to apply
							bool AlreadyIn = false;
							DKSlotData placeHolder = ScriptableObject.CreateInstance("DKSlotData") as DKSlotData;
							foreach ( DKSlotData slot in Crowd.tempSlotList ){
								if ( slot.slotName == LegacySlot.slotName ) AlreadyIn = true;
								if ( slot.Place == LegacySlot.Place ) {
									//	placeOccupied = true;
									placeHolder = slot;
									
								}
							}
							// add
							// For flesh
							if ( LegacySlot.OverlayType != null && LegacySlot.OverlayType == "Flesh" ) {

							//	bool placeOccupied = false;
								if ( AssignedSlotsList.Contains(LegacySlot) == true ) AlreadyIn = true;
								if ( !AlreadyIn ){
									DKSlotData slot = Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName);
									Crowd.tempSlotList.Add(slot);
									slot._LegacyData.IsLegacy = true;
									slot._LegacyData.ElderList.Add(Crowd.tempSlotList[Crowd.tempSlotList.Count-1]);

								//	Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName));
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
								// assign Linked overlay color first 
								if ( LegacySlot.LinkedOverlayList.Count > 0 && LegacySlot.LinkedOverlayList[0].ColorPresets.Count > 0 ) 
									Crowd.Colors.ColorToApply = LegacySlot.LinkedOverlayList[0].ColorPresets[0].PresetColor;
								// or assign white color
								else Crowd.Colors.ColorToApply = Color.white;

								DKSlotData slot = Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName);
								Crowd.tempSlotList.Add(slot);
								slot._LegacyData.IsLegacy = true;
								slot._LegacyData.ElderList.Add(Crowd.tempSlotList[Crowd.tempSlotList.Count-1]);
							//	Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName));
								if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count == 0 )
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(LegacySlot.overlayList[0].name,Crowd.Colors.ColorToApply));
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
			if ( Slot._LegacyData.IsLegacy == true ) {
				foreach ( DKSlotData ElderSlot in Slot._LegacyData.ElderList ){
					if ( Crowd.tempSlotList.Contains(ElderSlot) == false 
					    || ToRemoveList.Contains(ElderSlot) == true ) ToRemoveList.Add (Slot);
				}
			}
		}
		// clear the list of place holders
		foreach (DKSlotData placeHolder in ToRemoveList ){
			Crowd.tempSlotList.Remove (placeHolder);
		//	Debug.Log ("removing "+placeHolder.slotName );
		}
		#endregion Legacy
		
		#region hide shoulders
		if ( Crowd.Wears.Shoulders && Crowd.Wears.HideShoulders == true ) {
			Crowd.tempSlotList.Remove(Crowd.Wears.Shoulders);
		}
		Crowd.Wears.Shoulders = null;
		Crowd.Wears.HideShoulders = false; 
		#endregion hide shoulders
		
		#region hide Legs
		if ( Crowd.Wears.Legs && Crowd.Wears.HideLegs == true ) {
			Crowd.tempSlotList.Remove(Crowd.Wears.Legs);
		}
		Crowd.Wears.Legs = null;
		Crowd.Wears.HideLegs = false; 
		#endregion hide shoulders
		
		// Clean the slots of the avatar if no overlay	
		//	for(int i = 0; i < Crowd.tempSlotList.Count; i ++){
		//		if ( Crowd.tempSlotList[i].overlayList.Count == 0 ) Crowd.tempSlotList.Remove(Crowd.tempSlotList[i]);
		//	}
		#endregion Cleaning Avatar
		
		DK_DefineSlotFinishing.Finish(Crowd);

		#endregion Finishing
	}

}
