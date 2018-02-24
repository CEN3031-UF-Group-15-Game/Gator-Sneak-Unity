using UnityEngine;
# if Editor
using UnityEditor;
# endif
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;


public class DK_DefineSlotBody : MonoBehaviour {

	public static bool Skipping = false;
	public static List<DKSlotData> AssignedSlotsList = new List<DKSlotData>();
	public static List<DKOverlayData> AssignedOverlayList = new List<DKOverlayData>();



	public static void DefineSlots (DK_UMACrowd Crowd){
		#region Creating
		//		int randomResult = 0;
		Crowd._FaceOverlay = null;
		Crowd.Randomize.HairDone = false;
		List<DKOverlayData> TmpTorsoOverLayList = new List<DKOverlayData>();
		
		#region  DK UMA Editor
		if ( DK_UMACrowd.GeneratorMode == "Preset"  && Crowd.UMAGenerated  )
		{
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
			Crowd.tempSlotList = new List<DKSlotData>();Crowd.tempSlotList.Clear();
			
			// Create a tmp list for the slots to random
			List<DKSlotData> Tmplist = new List<DKSlotData>();
			List<DKSlotData> TmpTorsolist = new List<DKSlotData>();
			List<DKSlotData> TmpHairlist = new List<DKSlotData>();
			
			// Create a tmp list for the Overlay to random
			List<DKOverlayData> TmpOvLaylist = new List<DKOverlayData>();
			List<DKOverlayData> TmpOvLayHairlist = new List<DKOverlayData>();
			List<DKOverlayData> TmpTatoolist = new List<DKOverlayData>();
			List<DKOverlayData> TmpOvLayFleshlist = new List<DKOverlayData>();
		//	List<DKOverlayData> TmpOvLayUnderwearlist = new List<DKOverlayData>();
			List<DKOverlayData> TmpOvLayAdjustlist = new List<DKOverlayData>();


			TmpTorsoOverLayList.Clear();
			AssignedSlotsList.Clear();
			#endregion Create lists
			
			Crowd.NewProcess = true;
			if ( Crowd.NewProcess ){
				
				#region Populate lists
				
				#endregion Populate lists
				
			}
			
			if ( Crowd.NewProcess ){
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
									if ( _SlotData.Place && _SlotData.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
									    && (_SlotData.Race.Contains(Crowd.RaceAndGender.Race) ==  true )
									    && _SlotData._LegacyData.IsLegacy == false
									    && _SlotData.Active == true
									    && _SlotData.OverlayType == "Flesh" 
									    && ( Crowd.RaceAndGender.Gender == _SlotData.Gender )
									    && (_SlotData.Gender == TmpAnato.Gender || TmpAnato.Gender == "Both") )
								{
									TmpTorsolist.Add( Crowd.slotLibrary.slotElementList[i1] );
								}
							}
							// ran in the slots list
							if ( TmpTorsolist.Count != 0 ) {
								int Ran = UnityEngine.Random.Range(0, TmpTorsolist.Count );
								// Assign the Slot
								
								if ( AssignedSlotsList.Contains(TmpTorsolist[Ran]) == false ){
									AssignedSlotsList.Add (TmpTorsolist[Ran]);
									Crowd.tempSlotList.Add (Crowd.slotLibrary.InstantiateSlot(TmpTorsolist[Ran].slotName));
									# if Editor
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Preview = Tmplist[Ran].Preview;
									# endif
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Place = TmpTorsolist[Ran].Place;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Replace = TmpTorsolist[Ran].Replace;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType = TmpTorsolist[Ran].OverlayType;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].LinkedOverlayList = TmpTorsolist[Ran].LinkedOverlayList;
									Crowd.TorsoIndex = Crowd.tempSlotList.Count - 1;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.HasLegacy = TmpTorsolist[Ran]._LegacyData.HasLegacy;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.LegacyList = TmpTorsolist[Ran]._LegacyData.LegacyList;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.IsLegacy = TmpTorsolist[Ran]._LegacyData.IsLegacy;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.ElderList = TmpTorsolist[Ran]._LegacyData.ElderList;
									//	Debug.Log ("adding "+TmpTorsolist[Ran].slotName);
								}
							}

							#region Find Overlays for the chosen slot
							for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
								DKOverlayData _TmpOv = Crowd.overlayLibrary.overlayElementList[i2];
								if ( Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0].dk_SlotsAnatomyElement != null && !_TmpOv ) {
									Debug.LogError ( "Can not find the Overlay from the Library '"+Crowd.overlayLibrary.name+"' : Verify your Library for missing Elements. Delete the field or drag and drop the corresponding missing Element. The missing Element is ignored and the generation continues." );
								}
								else{
									#region Flesh list only
									if ( _TmpOv.OverlayType == ("Flesh")
									    && ( _TmpOv.Gender == Crowd.RaceAndGender.Gender || _TmpOv.Gender == "Both" )
									    && _TmpOv.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0]
									    && _TmpOv.Active == true){
										TmpOvLayFleshlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
									}
									
									#endregion Flesh list only
									
									#region Tatoo list only
									if ( _TmpOv.OverlayType == ("Tatoo")
									    && Crowd.Wears.RanUnderwearChoice
									    && ( _TmpOv.Gender == Crowd.RaceAndGender.Gender || _TmpOv.Gender == "Both" )
									    && _TmpOv.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0]
									    && _TmpOv.Active == true){
										TmpTatoolist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
									}
									#endregion Tatoo list only

								}
							}
							
							#region Assign flesh overlay only
							// verify if the slot has a linked overlay
							if ( Crowd.tempSlotList[Crowd.TorsoIndex].LinkedOverlayList.Count > 0 ){
								float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.AdjRanMaxi);
								Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
								Crowd.Colors.skinColor = Crowd.Colors.skinColor + _Adjust;

								int Ran3 = UnityEngine.Random.Range(0, Crowd.tempSlotList[Crowd.TorsoIndex].LinkedOverlayList.Count-1 );
								Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(Crowd.tempSlotList[Crowd.TorsoIndex].LinkedOverlayList[Ran3].overlayName,Crowd.Colors.skinColor));
								Crowd.FleshOverlay = Crowd.tempSlotList[Crowd.TorsoIndex].LinkedOverlayList[Ran3];
							}
							// if no linked overlay
							else {
								int Ran2 = UnityEngine.Random.Range(0, TmpOvLayFleshlist.Count );
								if ( Crowd.FleshOverlay == null )
								{
									bool AlreadyIn;
									AlreadyIn = false;
									for(int i = 0; i <  Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Count; i++){
										if ( Crowd.tempSlotList[Crowd.TorsoIndex].overlayList[i].overlayName == TmpOvLayFleshlist[Ran2].overlayName ){
											AlreadyIn = true;
										}
									}
									if ( AlreadyIn != true ){
										float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.AdjRanMaxi);
										Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
										Crowd.Colors.skinColor = Crowd.Colors.skinColor + _Adjust;
																			
										Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLayFleshlist[Ran2].overlayName,Crowd.Colors.skinColor));
										Crowd.FleshOverlay = TmpOvLayFleshlist[Ran2];
									}
								}
							}
							#endregion Assign flesh overlay only
							
							#region Assign Tatoo overlay only
							int tmpran = 0;
							if ( Crowd.Randomize.Tatoo == true ) tmpran = UnityEngine.Random.Range(0, 100 );
							if ( TmpTatoolist.Count > 0 && tmpran <= Crowd.Randomize.TatooChance ){
								int Ran3 = UnityEngine.Random.Range(0, TmpTatoolist.Count );
								bool AlreadyIn;
								AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.TorsoIndex].overlayList[i].overlayName == TmpTatoolist[Ran3].overlayName ){
										AlreadyIn = true;
									}
								}
								if ( AlreadyIn != true ){
									Crowd.tempSlotList[Crowd.TorsoIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpTatoolist[Ran3].name,new Color(UnityEngine.Random.Range(0.1f,0.9f) ,UnityEngine.Random.Range(0.1f,0.9f),UnityEngine.Random.Range(0.1f,0.9f),1)));
								}
							}
							#endregion Assign Tatoo overlay only

							#endregion Find Overlays for the chosen slot
							TmpTorsoOverLayList = Crowd.tempSlotList[Crowd.TorsoIndex].overlayList;
						}
					}
				}
				#endregion Torso Only and first
				
				#region to do for Face Preset Slot
				for(int i0 = 0; i0 < Crowd.ActivePresetLib.dk_SlotsAnatomyElementList.Length; i0 ++){
					// assign Elements DK_Race
					if ( Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] != null ){
						Crowd.Anato_DK_Race = Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0].gameObject.GetComponent<DK_Race>();
						Crowd.Race_DK_Race = Crowd.RaceAndGender.RaceToCreate;
						
						#region Find Face slot ONLY
						if ( !Crowd.Anato_DK_Race ){
							Debug.LogError ( "An Anatomy Part is missing from the current Generator preset '"+Crowd.ActivePresetLib.name
							                +"', You have to delete the missing field from the preset.");
						}
						else
						if (Crowd.Anato_DK_Race.OverlayType == "Is Head Part" ){
							if (( Crowd.Anato_DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd.Anato_DK_Race.Gender ==  "Both" )
							    && ( Crowd.Anato_DK_Race.Gender == Crowd.Race_DK_Race.Gender || Crowd.Anato_DK_Race.Gender == "Both" )
							    && (Crowd.Anato_DK_Race.Race.Contains(Crowd.RaceAndGender.Race) ==  true ) )
							{
								Tmplist.Clear();

								#region Find Slots Elements
								for(int i2 = 0; i2 < Crowd.slotLibrary.slotElementList.Length; i2 ++){
									Crowd._DK_Race = Crowd.slotLibrary.slotElementList[i2];
									
									// Add Slot Element to a list
									if ( !Crowd._DK_Race ) {
									}
									else
										if ( (Crowd._DK_Race.Race.Contains(Crowd.RaceAndGender.Race) ==  true )
										    &&  ( Crowd._DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd._DK_Race.Gender == "Both" )
										    &&  Crowd._DK_Race.OverlayType == "Face"
										    && Crowd._DK_Race._LegacyData.IsLegacy == false
										    &&  Crowd._DK_Race.Elem == false
										    && Crowd._DK_Race.Active == true
										    && Crowd._DK_Race.Place 
										    && Crowd._DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] )
									{
										Tmplist.Add( Crowd.slotLibrary.slotElementList[i2] );
									}
								}
								
								// ran in the slots list
								if ( Tmplist.Count != 0 ) {
									int Ran = UnityEngine.Random.Range(0, Tmplist.Count );
									
									// Assign the Slot
									if ( AssignedSlotsList.Contains(Tmplist[Ran]) == false ){
										AssignedSlotsList.Add (Tmplist[Ran]);
										Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot(Tmplist[Ran].slotName ));
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType = Tmplist[Ran].OverlayType;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Place = Tmplist[Ran].Place;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Replace = Tmplist[Ran].Replace;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].LinkedOverlayList = Tmplist[Ran].LinkedOverlayList;

										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.HasLegacy = Tmplist[Ran]._LegacyData.HasLegacy;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.LegacyList = Tmplist[Ran]._LegacyData.LegacyList;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.IsLegacy = Tmplist[Ran]._LegacyData.IsLegacy;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.ElderList = Tmplist[Ran]._LegacyData.ElderList;
										
										Crowd.FaceSlot = Crowd.tempSlotList.Count-1;
									}
								}
								#endregion Find Slots Elements
								
								#region find the face Overlay
									// verify if the slot has a linked overlay
									if ( Crowd.tempSlotList[Crowd.FaceSlot].LinkedOverlayList.Count > 0 ){								
										int Ran3 = UnityEngine.Random.Range(0, Crowd.tempSlotList[Crowd.FaceSlot].LinkedOverlayList.Count-1 );
										Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(Crowd.tempSlotList[Crowd.FaceSlot].LinkedOverlayList[Ran3].overlayName,Crowd.Colors.skinColor));
										Crowd._FaceOverlay = Crowd.tempSlotList[Crowd.FaceSlot].LinkedOverlayList[Ran3];
									}
									// if no linked overlay
									else {
										TmpOvLaylist.Clear();
										for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
											Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
											if ( !Crowd.Ov_DK_Race ) {}
											else
												// face overlay
												if ( Crowd.Ov_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
												    && Crowd.Ov_DK_Race.OverlayType == "Face"
												    && Crowd.Ov_DK_Race.Elem == false
												    && Crowd.Ov_DK_Race.Active == true
												    && (Crowd.Ov_DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd.Ov_DK_Race.Gender ==  "Both") )
											{
												TmpOvLaylist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
												//	Debug.Log ( Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0].name+" Overlay "+Crowd.overlayLibrary.overlayElementList[i2].overlayName+" Color("+Crowd.Colors.skinColor.ToString()+")");
											}
										}
										
										// ran in the Overlay list
										if ( TmpOvLaylist.Count != 0 ) {
											int Ran = UnityEngine.Random.Range(0, TmpOvLaylist.Count );
											// Assign the overlay
											Skipping = false;
											for(int i1 = 0; i1 < Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i1 ++){
												if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i1].OverlayType == "Face"){
													Skipping = true;
													Debug.Log ("skipping");
												}
											}
											if ( !Skipping && Crowd._FaceOverlay == null ){
												Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLaylist[Ran].name,Crowd.Colors.skinColor));
												//	Debug.Log ( Crowd.tempSlotList[Crowd.FaceSlot].name+" Overlay "+TmpOvLaylist[Ran].overlayName+" Color("+Colors.skinColor.ToString()+")");
												Crowd._FaceOverlay = TmpOvLaylist[Ran];
											}
										}									
									}
								#endregion
							}
							#endregion to do for Face Preset Slot
							
							#region find the Beard Overlays
							TmpOvLaylist.Clear();
							for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
								Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
								if ( !Crowd.Ov_DK_Race ) {
								}
								else
									if ( Crowd.Ov_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
									    && Crowd.Ov_DK_Race.OverlayType == "Beard" 
									    && Crowd.Ov_DK_Race.Active == true
									    && ( Crowd.Ov_DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd.Ov_DK_Race.Gender == "Both" ) )
								{
									TmpOvLaylist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
								}
							}
							
							// ran in the Overlay list
							if ( Crowd.Randomize.Pilosity != "None" && TmpOvLaylist.Count != 0 ) {
								for(int i2 = 0; i2 < TmpOvLaylist.Count; i2 ++){
									
									// Reset and prepare Pilosity
									if (i2 == 0){
										Crowd.Randomize.PiloAmount = 0;
										if ( Crowd.Randomize.Pilosity == "Random" ) Crowd.Randomize.PiloMaxi = UnityEngine.Random.Range(0, 3);
										if ( Crowd.Randomize.Pilosity == "Low" ) Crowd.Randomize.PiloMaxi = 1;
										if ( Crowd.Randomize.Pilosity == "Medium" ) Crowd.Randomize.PiloMaxi = 2;
										if ( Crowd.Randomize.Pilosity == "High" ) Crowd.Randomize.PiloMaxi = 3;
									}
									
									// Assign the overlay
									int Ran = UnityEngine.Random.Range(0, 10 );
									if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Contains(TmpOvLaylist[i2]) == false
									    && Crowd.Randomize.PiloAmount < Crowd.Randomize.PiloMaxi 
									    && Ran >= 5 )
									{ 
										float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.HairAdjRanMaxi);
										Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
										Crowd.Colors.ColorToApply = Crowd.Colors.HairColor + _Adjust;
										// if not already assigned
										//	if ( AssignedOverlayList.Contains ( TmpOvLaylist[i2] ) == false ){
										Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLaylist[i2].name,Crowd.Colors.ColorToApply));
										Crowd.Randomize.PiloAmount = Crowd.Randomize.PiloAmount +1;
										//		AssignedOverlayList.Add (TmpOvLaylist[i2]);
										//	}
									}
								}
							}
							#endregion
							
							#region find the hair Overlays
							if ( Crowd.Randomize.Hair != "None")
							{
								// reset
								Crowd.HairOverlay = null;
								TmpOvLayHairlist.Clear();
								// list overlay
								for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
									Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
									if ( !Crowd.Ov_DK_Race ) {}
									else
										if (  Crowd.Ov_DK_Race.OverlayType == "Hair" 
										    && Crowd.Ov_DK_Race.Place.name.Contains("Hair") == true  
										    &&Crowd.Ov_DK_Race.Active == true
										    && Crowd.Ov_DK_Race.Elem == false
										    && ( Crowd.Ov_DK_Race.Gender == Crowd.RaceAndGender.Gender 
										    || Crowd.Ov_DK_Race.Gender == "Both" ) )
									{
										TmpOvLayHairlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
										//		Debug.Log ( "hair listed : "+overlayLibrary.overlayElementList[i2].overlayName );
									}
								}
								
								// Has Hair or No ?
								int HairRan;
								int HairRan2;
								int Max = 100;
								HairRan = UnityEngine.Random.Range(0, Max );
								// very if hair slots are present in the library
								bool HairSlotsPresent = false;
								foreach ( DKSlotData slot in Crowd.slotLibrary.slotElementList ){
									try {
										if ( slot.Place && slot.Place.name == "Hair" 
										    && ( Crowd.RaceAndGender.Gender == slot.Gender || Crowd.RaceAndGender.Gender == "Both" )
										    && slot.Race.Contains(Crowd.RaceAndGender.Race) )  HairSlotsPresent = true;
									}
									catch (System.NullReferenceException){ 
										Debug.LogError ( slot.slotName+" Can't be generated" );
										Debug.LogError ( slot.slotName+" Gender "+slot.Gender );
										Debug.LogError ( slot.slotName+" Race "+slot.Race );
									}
								}
								
								if ( HairRan < 95 ){
									// mesh or ov ?
									HairRan2 = UnityEngine.Random.Range(0, 100 );
									int HairOvOnlyChance = 0;
									if ( HairSlotsPresent ) HairOvOnlyChance = 25;
									else HairOvOnlyChance = 101;
									
									// assign hair overlay Only
									if ( (( HairRan2 < HairOvOnlyChance && Crowd.RaceAndGender.Gender == "Male" )
									      || ( HairRan2 < HairOvOnlyChance && Crowd.RaceAndGender.Gender == "Female") )
									    && ( Crowd.Randomize.Hair.Contains("Simple") 
									    || Crowd.Randomize.Hair == "Random" )
									    && TmpOvLayHairlist.Count != 0 ) 
									{
										// Assign the overlay
										HairRan2 = UnityEngine.Random.Range(0, TmpOvLayHairlist.Count  );
										if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Contains(TmpOvLayHairlist[HairRan2]) == false){
											float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.HairAdjRanMaxi);
											Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
											Crowd.Colors.ColorToApply = Crowd.Colors.HairColor + _Adjust;
											Skipping = false;
											for(int i1 = 0; i1 < Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i1 ++){
												if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i1].OverlayType == "Hair"){
													Skipping = true;
													//	Debug.Log ("skipping");
												}
											}
											if ( !Skipping && Crowd.Randomize.HairDone == false ){
												Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLayHairlist[HairRan2].name,Crowd.Colors.ColorToApply));
												Crowd.HairOverlay = TmpOvLayHairlist[HairRan2];
												Crowd.HairOverlayIndex = HairRan2;
												Crowd.Randomize.HairDone = true;
												//	Debug.Log ( "hair assigned /"+HairRan2 );
											}
										}
									}
								}
							}	
							#endregion
							
							#region find the eyebrow Overlays
							TmpOvLaylist = new List<DKOverlayData>();
							for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
								Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
								if ( !Crowd.Ov_DK_Race ) {
								}
								else
									if ( Crowd.Ov_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
									    && Crowd.Ov_DK_Race.OverlayType == "Eyebrow" 
									    && ( Crowd.Ov_DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd.Ov_DK_Race.Gender == "Both" ) )
								{
									TmpOvLaylist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
								}
							}
							
							// ran in the Overlay list
							if ( TmpOvLaylist.Count != 0 ) {
								int Ran = UnityEngine.Random.Range(0, TmpOvLaylist.Count );
								// Assign the overlay
								bool AlreadyIn;
								AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i].overlayName == TmpOvLaylist[Ran].overlayName ){
										AlreadyIn = true;
									}
								}
								if ( AlreadyIn != true ){
									float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.HairAdjRanMaxi);
									Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
									Crowd.Colors.ColorToApply = Crowd.Colors.HairColor + _Adjust;
									Skipping = false;
									for(int i1 = 0; i1 < Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i1 ++){
										if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i1].name == TmpOvLaylist[Ran].name ){
											Skipping = true;
											Debug.Log ("skipping");
										}
									}
									if ( !Skipping )
										Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLaylist[Ran].name,Crowd.Colors.ColorToApply));
									TmpOvLaylist.Clear();
								}
							}
							#endregion find the eyebrow Overlays
							
							#region find the lips Overlays
							int tmpran = 0;
							if ( Crowd.Randomize.Lips == true ) tmpran = UnityEngine.Random.Range(0, 100 );
							if ( Crowd.Randomize.Lips == true && tmpran <= Crowd.Randomize.LipsChance ){
								TmpOvLaylist = new List<DKOverlayData>();
								for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
									Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
									if ( !Crowd.Ov_DK_Race ) {
									}
									else
										if (Crowd.Ov_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
										    &&Crowd.Ov_DK_Race.OverlayType == "Lips" 
										    && (Crowd.Ov_DK_Race.Gender == Crowd.RaceAndGender.Gender ||Crowd.Ov_DK_Race.Gender == "Both" ) )
									{
										TmpOvLaylist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
									}
								}
							}

							// ran in the Overlay list
							if ( TmpOvLaylist.Count != 0 ) {
								int Ran = UnityEngine.Random.Range(0, TmpOvLaylist.Count );
								// Assign the overlay
								bool AlreadyIn;
								AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i].overlayName == TmpOvLaylist[Ran].overlayName ){
										AlreadyIn = true;
									}
								}
								if ( AlreadyIn != true ){
									Color Red = new Color (0.86f, 0.21f, 0.18f);
									float AdjRan = UnityEngine.Random.Range(-1.0f,1f);
									Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
									Crowd.Colors.ColorToApply = Red - _Adjust;
									Skipping = false;
									for(int i1 = 0; i1 < Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i1 ++){
										if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i1].name == TmpOvLaylist[Ran].name ){
											Skipping = true;
											Debug.Log ("skipping");
										}
									}
									if ( !Skipping )
										Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLaylist[Ran].name,Crowd.Colors.ColorToApply));
									TmpOvLaylist.Clear();
								}
							}
							#endregion find the lips Overlays
							
							#region find the Makeup Overlays
							if ( Crowd.Randomize.Makeup == true ) tmpran = UnityEngine.Random.Range(0, 100 );
							if ( Crowd.Randomize.Makeup == true && tmpran <= Crowd.Randomize.MakeupChance ){
								TmpOvLaylist = new List<DKOverlayData>();
								for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
									Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
									if ( !Crowd.Ov_DK_Race ) {
									}
									else
										if (Crowd.Ov_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
										    &&Crowd.Ov_DK_Race.OverlayType == "Makeup" 
										    && (Crowd.Ov_DK_Race.Gender == Crowd.RaceAndGender.Gender ||Crowd.Ov_DK_Race.Gender == "Both" ) )
									{
										TmpOvLaylist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
									}
								}
							}

							// ran in the Overlay list
							if ( TmpOvLaylist.Count != 0 ) {
								int Ran = UnityEngine.Random.Range(0, TmpOvLaylist.Count );

								// Assign the overlay
								bool AlreadyIn;
								AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i].overlayName == TmpOvLaylist[Ran].overlayName ){
										AlreadyIn = true;
									}
								}
								if ( AlreadyIn != true ){
									float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.HairAdjRanMaxi);
									Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
									Crowd.Colors.ColorToApply =Crowd.Colors.HairColor + _Adjust;
									Skipping = false;
									for(int i1 = 0; i1 < Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i1 ++){
										if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i1].name == TmpOvLaylist[Ran].name ){
											Skipping = true;
											Debug.Log ("skipping");
										}
									}
									if ( !Skipping )
										Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLaylist[Ran].name,Crowd.Colors.ColorToApply));
									TmpOvLaylist.Clear();
								}
							}
							#endregion find the Makeup Overlays
							
							#region find the Tatoo Overlays
							if ( Crowd.Randomize.Tatoo == true ) tmpran = UnityEngine.Random.Range(0, 100 );
							if ( Crowd.Randomize.Tatoo == true && tmpran <= Crowd.Randomize.TatooChance ){
								TmpOvLaylist = new List<DKOverlayData>();
								for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
									Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
									if ( !Crowd.Ov_DK_Race ) {
									}
									else
										if (Crowd.Ov_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
										    &&Crowd.Ov_DK_Race.OverlayType == "Tatoo" 
										    && (Crowd.Ov_DK_Race.Gender == Crowd.RaceAndGender.Gender ||Crowd.Ov_DK_Race.Gender == "Both" ) )
									{
										TmpOvLaylist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
									}
								}
								// ran in the Overlay list
								if ( TmpOvLaylist.Count != 0 ) {
									int Ran = UnityEngine.Random.Range(0, TmpOvLaylist.Count );
									// Assign the overlay
									bool AlreadyIn;
									AlreadyIn = false;
									for(int i = 0; i <  Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i++){
										if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i].overlayName == TmpOvLaylist[Ran].overlayName ){
											AlreadyIn = true;
										}
									}
									if ( AlreadyIn != true ){
										float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.HairAdjRanMaxi);
										Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
										Crowd.Colors.ColorToApply =Crowd.Colors.HairColor + _Adjust;
										Skipping = false;
										for(int i1 = 0; i1 < Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i1 ++){
											if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i1].name == TmpOvLaylist[Ran].name ){
												Skipping = true;
												Debug.Log ("skipping");
											}
										}
										if ( !Skipping )
											Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLaylist[Ran].name,Crowd.Colors.ColorToApply));
										TmpOvLaylist.Clear();
									}
								}
							}
							#endregion find the Tatoo Overlays
							
							#region find the other head Overlays
							TmpOvLaylist = new List<DKOverlayData>();
							for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
								Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
								if ( !Crowd.Ov_DK_Race ) {
								}
								else
									if (Crowd.Ov_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
									    &&Crowd.Ov_DK_Race.OverlayType != "Beard" 
									    &&Crowd.Ov_DK_Race.OverlayType != "Hair"
									    &&Crowd.Ov_DK_Race.OverlayType != "Eyebrow"
									    &&Crowd.Ov_DK_Race.OverlayType != "Face"
									    &&Crowd.Ov_DK_Race.OverlayType != "Flesh"
									    &&Crowd.Ov_DK_Race.OverlayType != "Eyes"
									    &&Crowd.Ov_DK_Race.OverlayType != "Underwear"
									    && (Crowd.Ov_DK_Race.Gender == Crowd.RaceAndGender.Gender ||Crowd.Ov_DK_Race.Gender == "Both" ) )
								{
									TmpOvLaylist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
								}
							}
							
							// ran in the Overlay list
							if ( TmpOvLaylist.Count != 0 ) {
								int Ran = UnityEngine.Random.Range(0, TmpOvLaylist.Count );
								// Assign the overlay
								bool AlreadyIn;
								AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i].overlayName == TmpOvLaylist[Ran].overlayName ){
										AlreadyIn = true;
									}
								}
								if ( AlreadyIn != true ){
									float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.HairAdjRanMaxi);
									Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
									Crowd.Colors.ColorToApply =Crowd.Colors.HairColor + _Adjust;
									Skipping = false;
									for(int i1 = 0; i1 < Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i1 ++){
										if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i1].name == TmpOvLaylist[Ran].name ){
											Skipping = true;
											Debug.Log ("skipping");
										}
									}
									if ( !Skipping )
										Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLaylist[Ran].name,Crowd.Colors.ColorToApply));
									//	Debug.Log ( Crowd.tempSlotList[Crowd.FaceSlot].slotName+" Overlay "+TmpOvLaylist[Ran].overlayName+" Color("+Colors.skinColor.ToString()+")");
									TmpOvLaylist.Clear();
									
								}
							}
							#endregion
							TmpOvLaylist.Clear();
							Tmplist.Clear();
						}
					}
					#endregion
				}
				
				#region for Head Elem only
				
				#region Find Slots Elements
				for(int i0 = 0; i0 < Crowd.ActivePresetLib.dk_SlotsAnatomyElementList.Length; i0 ++){
					for(int i2 = 0; i2 < Crowd.slotLibrary.slotElementList.Length; i2 ++){
						DKSlotData tmpSlot = Crowd.slotLibrary.slotElementList[i2];
						// Add Slot Element to a list
						if ( !tmpSlot ) {}
						else if ( (tmpSlot.Race.Contains(Crowd.RaceAndGender.Race) ==  true )
						         && ( tmpSlot.Gender == Crowd.RaceAndGender.Gender || tmpSlot.Gender == "Both" )
						         && tmpSlot.OverlayType == "Face"
						         && tmpSlot._LegacyData.IsLegacy == false
						         && tmpSlot.Elem == true
						         && tmpSlot.Active == true
						         && tmpSlot.Place
						         && tmpSlot.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] )
						{
							Tmplist.Add( Crowd.slotLibrary.slotElementList[i2] );
						}
					}
					// ran in the slots list
					if ( Tmplist.Count != 0 ) {
						int Ran = UnityEngine.Random.Range(0, Tmplist.Count );
						// Assign the Slot
						bool AlreadyIn = false;
						bool placeOccupied = false;
						foreach ( DKSlotData slot in Crowd.tempSlotList ){
							if ( slot.name == Tmplist[Ran].name ) AlreadyIn = true;
							if ( slot.Place == Tmplist[Ran].Place ) placeOccupied = true;
						}
						if ( !placeOccupied && !AlreadyIn ){
							if ( Tmplist[Ran].overlayList.Count > 0 ){
								int Ran2 = UnityEngine.Random.Range(0, Tmplist[Ran].overlayList.Count );
								if ( AssignedSlotsList.Contains(Tmplist[Ran]) ==  false ) {
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
									
									// clean previous overlay if not of the flesh color
									if (Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList[0].color !=Crowd.Colors.skinColor ){
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.RemoveAt(0);
										
									}
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(Tmplist[Ran].overlayList[Ran2].overlayName,Crowd.Colors.skinColor));
									//	Debug.Log ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].slotName+" Overlay ("+tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count.ToString()+") "+Tmplist[Ran].overlayList[Ran2].overlayName+" Color("+Colors.skinColor.ToString()+")");
								}
							}
							else{
								if ( AssignedSlotsList.Contains(Tmplist[Ran]) ==  false ) {
									AssignedSlotsList.Add (Tmplist[Ran]);
									Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot(Tmplist[Ran].slotName,Crowd.tempSlotList[Crowd.FaceSlot].overlayList));
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
									
									//	Debug.Log ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].slotName+" Color("+Colors.skinColor.ToString()+")");
									
								}
							}
						}
					}
					TmpOvLaylist.Clear();
					Tmplist.Clear();
					#endregion Find Slots Elements
				}
				#endregion for Head Elem only
				
				bool HasHairOverlays = false;
				#region Hair Anatomy Part
				for(int i0 = 0; i0 < Crowd.ActivePresetLib.dk_SlotsAnatomyElementList.Length; i0 ++){
					for(int i1 = 0; i1 < Crowd.slotLibrary.slotElementList.Length; i1 ++){
						Crowd.Slot_DK_Race = Crowd.slotLibrary.slotElementList[i1];
						if ( Crowd.Slot_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
						    && ( Crowd.Slot_DK_Race.Race.Contains(Crowd.RaceAndGender.Race) ==  true  )
						    && Crowd.Slot_DK_Race.Active == true
						    && ( Crowd.Slot_DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd.Slot_DK_Race.Gender == "Both" )
						    && Crowd.Slot_DK_Race.OverlayType == "Hair" 
						    && Crowd.Slot_DK_Race.Elem == false 
						    && TmpHairlist.Contains(Crowd.slotLibrary.slotElementList[i1]) == false 
						    && Crowd.Randomize.HairDone == false
						    && ( Crowd.Randomize.Hair == "Random"
						    || Crowd.Randomize.Hair.Contains("Simple") ))
						{
							HasHairOverlays = true;
						}
					}
				}
				Skipping = false;
				for(int i1 = 0; i1 < Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i1 ++){
					if ( Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i1].OverlayType == "Hair" ){
						Skipping = true;
						Debug.Log ("skipping");
					}
				}
				int RanHairOverlay;
				if ( Crowd.HairOverlay != null ) RanHairOverlay = UnityEngine.Random.Range( 0, 100 );
				else RanHairOverlay = 100;
				if ( Skipping == false && HasHairOverlays == true && RanHairOverlay >= 50 ){
					//	tempSlotList[Crowd.FaceSlot].overlayList.Remove(Crowd.tempSlotList[Crowd.FaceSlot].overlayList[HairOverlayIndex]);
					if ( Crowd.HairOverlay != null ) {
						for(int i0 = 0; i0 < Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Count; i0 ++){
							if ( Crowd.HairOverlay && Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i0].overlayName == Crowd.HairOverlay.overlayName ) {
								Crowd.tempSlotList[Crowd.FaceSlot].overlayList.Remove(Crowd.tempSlotList[Crowd.FaceSlot].overlayList[i0]);
								//	Debug.Log ( "Hair slot, previous ov removed /"+tempSlotList[Crowd.FaceSlot].overlayList[i0].overlayNameLinked );
								Crowd.HairOverlay = null;
							}
						}
					}
					for(int i0 = 0; i0 < Crowd.ActivePresetLib.dk_SlotsAnatomyElementList.Length; i0 ++){
						Crowd.Anato_DK_Race = Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0].gameObject.GetComponent<DK_Race>();
						Crowd.Race_DK_Race = Crowd.RaceAndGender.RaceToCreate;
						
						if ( Crowd.Anato_DK_Race.OverlayType == "Hair"
						    && Crowd.Randomize.HairDone == false
						    && ( Crowd.Anato_DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd.Anato_DK_Race.Gender ==  "Both" )
						    && ( Crowd.Anato_DK_Race.Gender == Crowd.Race_DK_Race.Gender || Crowd.Anato_DK_Race.Gender == "Both" )
						    && ( Crowd.Anato_DK_Race.Race.Contains(Crowd.RaceAndGender.Race) ==  true  )
						    )
						{
							//	Debug.Log ( "prepare list Hair slot" );
							#region Find slots for Hair the Anatomy part
							TmpHairlist.Clear();
							for(int i1 = 0; i1 < Crowd.slotLibrary.slotElementList.Length; i1 ++){
								Crowd.Slot_DK_Race = Crowd.slotLibrary.slotElementList[i1];
								if ( Crowd.Slot_DK_Race.Place
								    && Crowd.Slot_DK_Race.Place == Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0] 
								    && Crowd.Slot_DK_Race._LegacyData.IsLegacy == false
								    && ( Crowd.Slot_DK_Race.Race.Contains(Crowd.RaceAndGender.Race) ==  true  )
								    && Crowd.Slot_DK_Race.Active == true
								    && ( Crowd.Slot_DK_Race.Gender == Crowd.RaceAndGender.Gender || Crowd.Slot_DK_Race.Gender == "Both" )
								    && Crowd.Slot_DK_Race.OverlayType == "Hair" 
								    && Crowd.Slot_DK_Race.Elem == false 
								    && TmpHairlist.Contains(Crowd.slotLibrary.slotElementList[i1]) == false 
								    && Crowd.Randomize.HairDone == false
								    && ( Crowd.Randomize.Hair == "Random"
								    || Crowd.Randomize.Hair.Contains("Simple") ))
								{
								//	int ran = UnityEngine.Random.Range(0, 100 );
									//	if ( ran < 95 ) {
									TmpHairlist.Add( Crowd.slotLibrary.slotElementList[i1] );
									TmpHairlist[TmpHairlist.Count-1].Place = Crowd.Slot_DK_Race.Place;
									//	Debug.Log ( "List Hair slot "+slotLibrary.slotElementList[i1] );
									//	}
									//	else {
									// Debug.Log ( "Hair slot Skipped (model with no hair)" );
									//	}
								}
							}

							#region Assign randomly to Hair the Anatomy part
							if ( TmpHairlist.Count > 0 ) {
								int Ran = UnityEngine.Random.Range(0, TmpHairlist.Count );
								Crowd.Slot_DK_Race = TmpHairlist[Ran];
								// Assign the Slot
								if ( (Crowd.Anato_DK_Race.OverlayType != "Is Torso Part" 
								      || TmpHairlist[Ran].OverlayType.Contains("Hair"))
								    && Crowd.Slot_DK_Race.Place.name != "Head" 
								    //   && Crowd.Slot_DK_Race.Elem != true 
								    )
								{
									#region Assign Hair Slot
									if ( TmpHairlist[Ran].OverlayType == "Hair"
									    && TmpHairlist[Ran].Elem == false
									    && Crowd.Randomize.HairDone == false
									    && AssignedSlotsList.Contains(TmpHairlist[Ran]) ==  false ) 
									{
										AssignedSlotsList.Add (TmpHairlist[Ran]);
										Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot(TmpHairlist[Ran].slotName ));
										# if Editor
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Preview = Tmplist[Ran].Preview;
										# endif
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Place = TmpHairlist[Ran].Place;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Replace = TmpHairlist[Ran].Replace;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType = TmpHairlist[Ran].OverlayType;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.HasLegacy = TmpHairlist[Ran]._LegacyData.HasLegacy;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.LegacyList = TmpHairlist[Ran]._LegacyData.LegacyList;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.IsLegacy = TmpHairlist[Ran]._LegacyData.IsLegacy;
										Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.ElderList = TmpHairlist[Ran]._LegacyData.ElderList;
										
										
										Crowd.CurrentSlot = TmpHairlist[Ran];
										Crowd.CurrentSlotIndex = Crowd.tempSlotList.Count-1;
										//	Debug.Log ( "Assign Hair slot - "+TmpHairlist[Ran] );
										Crowd.Randomize.HairDone = true;
									}
									#endregion Assign Hair Slot
								}
							}
							#endregion Assign randomly to Hair the Anatomy part
							//	}
							#endregion Find slots for the Hair Anatomy part
							
							#region Find Overlays for the chosen Hair slot
							#region Linked Hair Slots Only
							List<DKOverlayData> TmpLinkedToSlot = new List<DKOverlayData>();
							if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType == "Hair"
							    && Crowd.tempSlotList[Crowd.tempSlotList.Count-1].LinkedOverlayList.Count != 0 ){
								TmpLinkedToSlot = Crowd.tempSlotList[Crowd.tempSlotList.Count-1].LinkedOverlayList;
								
								int Ran2 = UnityEngine.Random.Range(0, (TmpLinkedToSlot.Count-1) );
								bool AlreadyIn = false;
								//	Debug.Log ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].slotName+" Use LinkedToSlot ("+tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count+")" );
								
								// verify if TmpLinkedToSlot present in overlayList 
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count; i++){
									if (Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType == "Hair"
									    && Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList[i].overlayName == TmpLinkedToSlot[Ran2].overlayName )
									{
										AlreadyIn = true;
										Crowd.ToDelete = Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList[i];
										//	Debug.Log ( "To Del - "+ToDelete.overlayName
										//	           +"("+TmpLinkedToSlot[Ran2].OverlayType+"/"
										//	           +tempSlotList[Crowd.tempSlotList.Count-1].OverlayType);
									}
								}
								float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.HairAdjRanMaxi);
								Crowd.Colors.ColorToApply = Crowd.Colors.HairColor;
								Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
								Crowd.Colors.ColorToApply = Crowd.Colors.ColorToApply + _Adjust;
								
								if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType == "Hair"
								    && AlreadyIn != true ){
									// add new
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpLinkedToSlot[Ran2].overlayName,Crowd.Colors.ColorToApply));
									//	Debug.Log ( Ran2+" / " +tempSlotList[Crowd.tempSlotList.Count-1].slotName+" Linked to "+TmpLinkedToSlot[Ran2].overlayName);
									//	Debug.Log ( "Assign Hair Overlay - "+TmpLinkedToSlot[Ran2].overlayName );
								}
								else if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType == "Hair" ) {
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpLinkedToSlot[Ran2].overlayName,Crowd.Colors.ColorToApply));
									//	Debug.Log ( "Already in Hair Overlay - "+TmpLinkedToSlot[Ran2].overlayName );
									//	Debug.Log ( "Removing - "+ToDelete.overlayName);
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Remove(Crowd.ToDelete);
								}
							}
							#endregion Linked HairSlots Only 
							#endregion Other Crowd.HairOverlays Only 
							
							else {
								for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
									Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
									if ( !Crowd.Ov_DK_Race || !Crowd.Slot_DK_Race ) {}
									else
										#region Hair only
										if (Crowd.Ov_DK_Race.Place == Crowd.Slot_DK_Race.Place 
										    &&Crowd.Ov_DK_Race.OverlayType == ("Hair")
										    &&Crowd.Ov_DK_Race.Active == true )
									{
										if ( TmpOvLayHairlist.Contains(Crowd.overlayLibrary.overlayElementList[i2]) )
										{}
										else{
											TmpOvLayHairlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
											//	Randomize.HairDone = true;
										}
									}
									#endregion Hair only
								}
								#region Hair overlay assign
								if ( Crowd.CurrentSlot.OverlayType == "Hair" 
								    && TmpOvLayHairlist.Count > 0 )
								{
									Skipping = false;
									for(int i1 = 0; i1 < Crowd.tempSlotList.Count; i1 ++){
										if ( Crowd.tempSlotList[i1].OverlayType == "Hair" ){
											Skipping = true;
											Debug.Log ("skipping");
										}
									}
									if ( !Skipping && Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count == 0 ){
										int Ran2 = UnityEngine.Random.Range(0, TmpOvLayHairlist.Count );
										bool AlreadyIn;
										AlreadyIn = false;
										for(int i = 0; i <  Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count; i++){
											if ( Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList[i].overlayName == TmpOvLayHairlist[Ran2].overlayName ){
												AlreadyIn = true;
											}
										}
										if ( AlreadyIn != true ){
											Crowd.Colors.ColorToApply =Crowd.Colors.HairColor;
											float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.HairAdjRanMaxi);
											Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
											Crowd.Colors.ColorToApply =Crowd.Colors.ColorToApply + _Adjust;
											Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLayHairlist[Ran2].name,Crowd.Colors.ColorToApply));
											Debug.Log ( TmpOvLayHairlist[Ran2].name + " overlay assign Hair to "+Crowd.tempSlotList[Crowd.CurrentSlotIndex].slotName);
											Crowd.Randomize.HairDone = true;
										}
									}
								}
								#endregion Hair overlay assign
							}
						}
						//	}
					}
				}
				#endregion Hair Anatomy Part
				//	}
				
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
									if ( Crowd.Wears.RanWearAct ) Crowd.Wears.RanWearYes = UnityEngine.Random.Range(0, Crowd.Wears.RanWearYesMax );
									#region NOT wear Slot
									if ( Crowd.Slot_DK_Race.OverlayType.Contains("Wear") == false ) {
										#region Hair_module Slot
										if (( Crowd.Randomize.Hair == "Simple+Modules" || Crowd.Randomize.Hair == "Random" )
										    && Crowd.Slot_DK_Race.OverlayType == "Hair"
										    //    && Crowd.Slot_DK_Race.OverlayType == "Face"
										    && Crowd.Slot_DK_Race._LegacyData.IsLegacy == false
										    && Crowd.Slot_DK_Race.Elem == true
										    && Crowd.Slot_DK_Race.Place
										    && Tmplist.Contains(Crowd.slotLibrary.slotElementList[i1]) == false )
										{
											Tmplist.Add( Crowd.slotLibrary.slotElementList[i1] );
											Tmplist[Tmplist.Count-1].Place = Crowd.Slot_DK_Race.Place;
											//	Debug.Log ( Crowd.slotLibrary.slotElementList[i1].slotName );
										}
										#endregion Hair_module Slot
										#region Other Slot
										else if ( Crowd.Slot_DK_Race.OverlayType != "Hair" 
										         && Crowd.Slot_DK_Race.OverlayType != "Face"
										         && Crowd.Slot_DK_Race._LegacyData.IsLegacy == false
										         && Crowd.Slot_DK_Race.Place) {
											Tmplist.Add( Crowd.slotLibrary.slotElementList[i1] );
											Tmplist[Tmplist.Count-1].Place = Crowd.Slot_DK_Race.Place;
											#endregion Other Slot
										}
									}
									#endregion NOT wear Slot
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

								#region Hair_module Slot
								if ( Tmplist[Ran].OverlayType == "Hair"
								    && Tmplist[Ran].Elem == true
								    && AssignedSlotsList.Contains(Tmplist[Ran]) ==  false ) 
								{
									AssignedSlotsList.Add (Tmplist[Ran]);
									Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot(Tmplist[Ran].slotName ));
									# if Editor
											Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Preview = Tmplist[Ran].Preview;
											# endif
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Place = Tmplist[Ran].Place;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].Replace = Tmplist[Ran].Replace;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].OverlayType = Tmplist[Ran].OverlayType;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].LinkedOverlayList = Tmplist[Ran].LinkedOverlayList;

									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.HasLegacy = Tmplist[Ran]._LegacyData.HasLegacy;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.LegacyList = Tmplist[Ran]._LegacyData.LegacyList;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.IsLegacy = Tmplist[Ran]._LegacyData.IsLegacy;
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.ElderList = Tmplist[Ran]._LegacyData.ElderList;
									
									Crowd.CurrentSlot = Tmplist[Ran];
									Crowd.CurrentSlotIndex = Crowd.tempSlotList.Count-1;
									Crowd.Randomize.HairDone = true;
									//		Debug.Log (Crowd.Anato_DK_Race.gameObject.name+ " adding NO mesh slot "+Tmplist[Ran].slotName );
								}
								#endregion Hair_module Slot

								#region Other Slot
								else {
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
											Crowd.tempSlotList[Crowd.tempSlotList.Count-1].LinkedOverlayList = Tmplist[Ran].LinkedOverlayList;
											Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.HasLegacy = Tmplist[Ran]._LegacyData.HasLegacy;
											Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.LegacyList = Tmplist[Ran]._LegacyData.LegacyList;
											Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.IsLegacy = Tmplist[Ran]._LegacyData.IsLegacy;
											Crowd.tempSlotList[Crowd.tempSlotList.Count-1]._LegacyData.ElderList = Tmplist[Ran]._LegacyData.ElderList;
											
											Crowd.CurrentSlot = Tmplist[Ran];
											Crowd.CurrentSlotIndex = Crowd.tempSlotList.Count-1;
											//	Debug.Log (Crowd.Anato_DK_Race.gameObject.name+ " adding slot "+Tmplist[Ran].slotName );
										}
									}
								}
								#endregion Other Slot
								
								#region if Replace activated
								if ( Crowd.CurrentSlot.Replace == true ) {
									//	Debug.Log ( "Replace Active" );
									for(int i1 = 0; i1 < Crowd.tempSlotList.Count; i1 ++){
										if ( Crowd.tempSlotList[i1].Place == Crowd.CurrentSlot.Place.dk_SlotsAnatomyElement.Place
										    //   && Crowd.ActivePresetLib.dk_SlotsAnatomyElementList[i0].dk_SlotsAnatomyElement.Place 
										    ) {
											//	Debug.Log ( "Removing "+tempSlotList[i1].name );
											Crowd.tempSlotList.Remove(Crowd.tempSlotList[i1]);
											
										}
									}
								}
								#endregion if Replace activated
							}
							#endregion if NOT mesh Slot
							
							Tmplist.Clear();
							#endregion Assign randomly to the Anatomy part
							
							#region Find Overlays for the chosen slot
							#region Linked Slots Only
							
							if ( Crowd.CurrentSlot 
							    && Crowd.UseLinkedOv 
							    && Crowd.CurrentSlot.LinkedOverlayList.Count > 0 
							    && Crowd.CurrentSlot.OverlayType != "Face" )
							{
								
								#region Choose Color to apply
								if ( Crowd.CurrentSlot.OverlayType == "Hair" ) Crowd.Colors.ColorToApply =Crowd.Colors.HairColor;
								if ( Crowd.CurrentSlot.OverlayType == "Beard" ) Crowd.Colors.ColorToApply =Crowd.Colors.HairColor;
								if ( Crowd.CurrentSlot.OverlayType == "Eyes" ) Crowd.Colors.ColorToApply =Crowd.Colors.EyesColor;
								if ( Crowd.CurrentSlot.OverlayType == "Face" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
								if ( Crowd.CurrentSlot.OverlayType == "Flesh" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
								if ( Crowd.CurrentSlot.Place.name == "InnerMouth" ) Crowd.Colors.ColorToApply =Crowd.Colors.InnerMouthColor;
								if ( Crowd.Anato_DK_Race.gameObject.name.ToLower().Contains("eyelash")) Crowd.Colors.ColorToApply = GUI.color = Color.black;
								#endregion Choose Color to apply
								
								//	Debug.Log ( Crowd.CurrentSlot.name+" / "+ Crowd.CurrentSlot.overlayList.Count.ToString());
								
								List<DKOverlayData> TmpLinkedToSlot = new List<DKOverlayData>();
								TmpLinkedToSlot = Crowd.CurrentSlot.LinkedOverlayList;
								
								int Ran2 = UnityEngine.Random.Range(0, (TmpLinkedToSlot.Count) );
								bool AlreadyIn = false;
								for(int i = 0; i <  Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count; i++){
									if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList[i].overlayName == TmpLinkedToSlot[Ran2].overlayName ){
										AlreadyIn = true;
										Crowd.ToDelete = Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList[i];
									}
								}
								
								if ( Crowd.CurrentSlot.OverlayType.Contains("Hair") == true ){
									float AdjRan = UnityEngine.Random.Range(0,Crowd.Colors.HairAdjRanMaxi);
									Crowd.Colors.ColorToApply = Crowd.Colors.HairColor;
									Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
									Crowd.Colors.ColorToApply = Crowd.Colors.ColorToApply + _Adjust;
								}
								if ( AlreadyIn != true ){
									
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpLinkedToSlot[Ran2].overlayName,Crowd.Colors.ColorToApply));
								}
								else {
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Clear();
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpLinkedToSlot[Ran2].overlayName,Crowd.Colors.ColorToApply));
								}
							}
							#endregion Linked Slots Only
							
							// For not linked Slots
							else {
								for(int i2 = 0; i2 < Crowd.overlayLibrary.overlayElementList.Length; i2 ++){
									Crowd.Ov_DK_Race = Crowd.overlayLibrary.overlayElementList[i2];
									if ( !Crowd.Ov_DK_Race || !Crowd.Slot_DK_Race ) {
									}
									else
										if ( ( Crowd.Slot_DK_Race.Gender == Crowd.Ov_DK_Race.Gender ||Crowd.Ov_DK_Race.Gender == "Both" )
										    && (Crowd.Ov_DK_Race.Race.Contains(Crowd.RaceAndGender.Race) ==  true )
										    &&Crowd.Ov_DK_Race.Active == true
										    && ( Crowd.Slot_DK_Race.Place ==Crowd.Ov_DK_Race.Place || Crowd.Anato_DK_Race.Place ==Crowd.Ov_DK_Race.Place ) )
									{
										#region add overlays Eyes Only Slot to list
										// add Eyes overlay Only
										//	try{
										if (//Crowd.Ov_DK_Race.Place &&
										    //   Crowd.Ov_DK_Race.Place.name.Contains("Eye") && 
										   Crowd.Ov_DK_Race.OverlayType == "Eyes" 
										    && Crowd.Slot_DK_Race.OverlayType == "Eyes")
										{
											// Adjust
											if ( Crowd.overlayLibrary.overlayElementList[i2].name.Contains("Adjust") )
											{
												if ( TmpOvLaylist.Contains(Crowd.overlayLibrary.overlayElementList[i2]) )
												{}
												else
													TmpOvLayAdjustlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
												//									Debug.Log ( Crowd.overlayLibrary.overlayElementList[i2]+" adding to Eyes Adjust" );
											}
											// Overlay
											else {
												TmpOvLaylist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
												//									Debug.Log ( Crowd.overlayLibrary.overlayElementList[i2]+" adding to Eyes Overlay" );
											}
										}
										//	}catch(){}
										#endregion Eyes Only Slot to list
										else
										if ( Crowd.Slot_DK_Race.LinkedOverlayList.Count == 0 ) {
											#region add overlays for all Slots to lists
											
											#region Commun 
											if (Crowd.Ov_DK_Race.OverlayType.Contains("Wear") == false 
											    &&Crowd.Ov_DK_Race.OverlayType != ("Underwear")
											    &&Crowd.Ov_DK_Race.OverlayType != ("Face")
											    &&Crowd.Ov_DK_Race.OverlayType != ("Eyes") 
											    &&Crowd.Ov_DK_Race.OverlayType != ("Flesh")	
											    &&Crowd.Ov_DK_Race.OverlayType != ("Hair")
											    &&Crowd.Ov_DK_Race.OverlayType != ("Eyebrow")
											    && Crowd.Slot_DK_Race.OverlayType != ("Hair")
											    &&Crowd.Ov_DK_Race.Active == true
											    )
											{
												if (Crowd.Ov_DK_Race.Place != Crowd.Slot_DK_Race.Place && TmpOvLaylist.Contains(Crowd.overlayLibrary.overlayElementList[i2]) )
												{}
												TmpOvLaylist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
												//	Debug.Log ( Crowd.Slot_DK_Race.name +" : "+ Crowd.overlayLibrary.overlayElementList[i2]+" added to Commun Overlays List"+" Color("+Colors.ColorToApply.ToString()+")" );
											}
											#endregion Commun
											#region Flesh only
											if (Crowd.Ov_DK_Race.OverlayType == ("Flesh")  
											    &&Crowd.Ov_DK_Race.Active == true){
												TmpOvLayFleshlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
												//	Debug.Log ( Crowd.Slot_DK_Race.name +" : "+ Crowd.overlayLibrary.overlayElementList[i2]+" added to Flesh Overlays List"+" Color("+Colors.ColorToApply.ToString()+")" );
											}
											#endregion Flesh only
											#region Hair only
											if (Crowd.Ov_DK_Race.Place == Crowd.Slot_DK_Race.Place 
											    &&Crowd.Ov_DK_Race.OverlayType.Contains("Wear") == false
											    && Crowd.Slot_DK_Race.OverlayType.Contains("Wear") == false
											    &&Crowd.Ov_DK_Race.OverlayType == ("Hair")
											    &&Crowd.Ov_DK_Race.Elem == true
											    && Crowd.Slot_DK_Race.Elem == true
											    && Crowd.Slot_DK_Race.OverlayType == ("Hair")
											    && Crowd.Slot_DK_Race.Active == true	)
											{
												if ( TmpOvLayHairlist.Contains(Crowd.overlayLibrary.overlayElementList[i2]) )
												{}
												else
													TmpOvLayHairlist.Add( Crowd.overlayLibrary.overlayElementList[i2] );
											}
											#endregion Hair only

											#endregion all Slot to list
										}
									}
								}
								#endregion Find Overlays for the chosen slot
								
								#region Assign Overlay randomly to the chosen slot
								
								#region add Eyes overlay Only
								if ( Crowd.Anato_DK_Race.OverlayType == "Eyes" )
								{
									// eyes
									int Ran2 = UnityEngine.Random.Range(0, TmpOvLaylist.Count );
									Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLaylist[Ran2].name));
									
									// Adjust
									int Ran3 = UnityEngine.Random.Range(0, TmpOvLayAdjustlist.Count );
									bool AlreadyIn;
									AlreadyIn = false;
									for(int i = 0; i <  Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count; i++){
										if ( Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList[i].overlayName == TmpOvLayAdjustlist[Ran3].overlayName ){
											AlreadyIn = true;
										}
									}
									if ( AlreadyIn != true ){
										float AdjRan = UnityEngine.Random.Range(0.0f,0.6f);
										Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
										Crowd.Colors.EyesColor =Crowd.Colors.EyesColor + _Adjust;
										Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLayAdjustlist[Ran3].name,Crowd.Colors.EyesColor));
									}
									TmpOvLayAdjustlist.Clear();
									TmpOvLaylist.Clear();
								}
								#endregion add Eyes overlay Only
								
								else {
									#region add All	Overlays
									if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].slotName.Contains("Eyes") == false  )
									{
										#region flesh overlay assign
										if ( Crowd.CurrentSlot.OverlayType.Contains("Wear") == false 
										    && Crowd.CurrentSlot.OverlayType == "Flesh"
											&& ( TmpOvLayFleshlist.Count > 0 || Crowd.CurrentSlot.LinkedOverlayList.Count > 0 ) )
										{
											// verify if the slot has a linked overlay
											if ( Crowd.CurrentSlot.LinkedOverlayList.Count > 0 ){								
												int Ran3 = UnityEngine.Random.Range(0, Crowd.CurrentSlot.LinkedOverlayList.Count-1 );
												Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(Crowd.CurrentSlot.LinkedOverlayList[Ran3].overlayName,Crowd.Colors.skinColor));
												Debug.Log ( "assigning overlay "+Crowd.CurrentSlot.LinkedOverlayList[Ran3].overlayName+" to slot "+ Crowd.tempSlotList[Crowd.tempSlotList.Count-1].slotName );
											}
											// if no linked overlay
											else {
												if ( Crowd.FleshOverlay == null )
												{								
													Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(Crowd.FleshOverlay.name,Crowd.Colors.skinColor));
												}
												else{
													bool AlreadyIn;
													AlreadyIn = false;
													for(int i = 0; i <  Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count; i++){
														if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList[i].overlayName == Crowd.FleshOverlay.overlayName ){
															AlreadyIn = true;
														}
													}
													if ( AlreadyIn != true ){
														Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(Crowd.FleshOverlay.name,Crowd.Colors.skinColor));
														Debug.Log ( "assigning overlay "+Crowd.FleshOverlay.overlayName+" to slot "+ Crowd.tempSlotList[Crowd.tempSlotList.Count-1].slotName );
													}
												}
											}
										}
										#endregion flesh overlay assign
										
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
												Debug.Log ( TmpOvLaylist[Ran2].name + " overlay assign Common to "+Crowd.tempSlotList[Crowd.CurrentSlotIndex].slotName);
											}
										}
										#endregion Commun overlay assign ( no wear or mesh )
										
										#region Hair overlay assign
										if ( Crowd.CurrentSlot.OverlayType.Contains("Wear") == false 
										    && Crowd.CurrentSlot.OverlayType == "Hair" 
										    && TmpOvLayHairlist.Count > 0 )
										{
											if ( Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count == 0 ){
												int Ran2 = UnityEngine.Random.Range(0, TmpOvLayHairlist.Count );
												bool AlreadyIn;
												AlreadyIn = false;
												for(int i = 0; i <  Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Count; i++){
													if ( Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList[i].overlayName == TmpOvLayHairlist[Ran2].overlayName ){
														AlreadyIn = true;
													}
												}
												if ( AlreadyIn != true ){
													Crowd.Colors.ColorToApply =Crowd.Colors.HairColor;
													float AdjRan = UnityEngine.Random.Range(0.0f,Crowd.Colors.HairAdjRanMaxi);
													Color _Adjust = new Color(AdjRan ,AdjRan,AdjRan,1);
													Crowd.Colors.ColorToApply =Crowd.Colors.ColorToApply + _Adjust;
													Crowd.tempSlotList[Crowd.CurrentSlotIndex].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(TmpOvLayHairlist[Ran2].name,Crowd.Colors.ColorToApply));
													Debug.Log ( TmpOvLayHairlist[Ran2].name + " overlay assign Hair to "+Crowd.tempSlotList[Crowd.CurrentSlotIndex].slotName);
												}
											}
										}
										#endregion Commun overlay assign ( no wear or mesh )

										TmpOvLaylist.Clear();
										TmpOvLayFleshlist.Clear();
										TmpOvLayHairlist.Clear();

									}
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
		foreach( DKSlotData slot in Crowd.tempSlotList){
			#region if Replace activated
			if ( slot.Replace == true ) {
				for(int i1 = 0; i1 < Crowd.tempSlotList.Count; i1 ++){
					if ( slot.Place.dk_SlotsAnatomyElement.Place == Crowd.tempSlotList[i1].Place ) {
						ToRemoveList.Add(Crowd.tempSlotList[i1]);
					}
				}
			}
			#endregion if Replace activated

			#region hide Handled
			// detect 'hide Handled'
			if (  DK_UMACrowd.GenerateWear == false && DK_UMACrowd.GenerateHandled == false ){
				if ( slot.Place.name.Contains("Handled") == true ){
					ToRemoveList.Add( slot );
				}
			}
			#endregion hide Handled
		}
		
		#region Legacy
		for(int i = 0; i < Crowd.tempSlotList.Count; i ++){
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
							else if ( LegacySlot.OverlayType == "Eyes" ) Crowd.Colors.ColorToApply =Crowd.Colors.EyesColor;
							else if ( LegacySlot.OverlayType == "Face" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
							else if ( LegacySlot.OverlayType == "Flesh" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
							else if ( LegacySlot.Place && LegacySlot.Place.name == "InnerMouth" ) Crowd.Colors.ColorToApply =Crowd.Colors.InnerMouthColor;
							else {
								Crowd.Colors.ColorToApply = new Color (UnityEngine.Random.Range(0.01f,0.9f),UnityEngine.Random.Range(0.01f,0.9f),UnityEngine.Random.Range(0.01f,0.9f));
								//	Colors.BeltWearColor1 = UnityEngine.Random.Range(0.01f,0.9f);
							}
							#endregion Choose Color to apply
							
							// add
							// For flesh
							if ( LegacySlot.OverlayType != null && LegacySlot.OverlayType == "Flesh" ) {
								bool AlreadyIn = false;
							//	bool placeOccupied = false;
								DKSlotData placeHolder = ScriptableObject.CreateInstance("DKSlotData") as DKSlotData;
								foreach ( DKSlotData slot in Crowd.tempSlotList ){
									if ( slot.slotName == LegacySlot.slotName ) AlreadyIn = true;
									if ( slot.Place == LegacySlot.Place ) {
									//	placeOccupied = true;
										placeHolder = slot;
										
									}
								}
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
								float c1 = UnityEngine.Random.Range(0.01f,0.9f);
								float c2 = UnityEngine.Random.Range(0.01f,0.9f);
								float c3 = UnityEngine.Random.Range(0.01f,0.9f);
								Crowd.Colors.ColorToApply = new Color (c1,c2,c3);
								DKSlotData slot = Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName);
								Crowd.tempSlotList.Add(slot);
								slot._LegacyData.IsLegacy = true;
								slot._LegacyData.ElderList.Add(Crowd.tempSlotList[Crowd.tempSlotList.Count-1]);
							//	Crowd.tempSlotList.Add(Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName));
								if ( Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Count == 0 ){
									Crowd.tempSlotList[Crowd.tempSlotList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(LegacySlot.overlayList[0].name,Crowd.Colors.ColorToApply));
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
		

		// Clean the slots of the avatar if no overlay	
		//	for(int i = 0; i < Crowd.tempSlotList.Count; i ++){
		//		if ( Crowd.tempSlotList[i].overlayList.Count == 0 ) Crowd.tempSlotList.Remove(Crowd.tempSlotList[i]);
		//	}
		#endregion Cleaning Avatar

		if ( DK_UMACrowd.GenerateWear ) DK_DefineSlotWear.DefineSlots(Crowd);
		else DK_DefineSlotFinishing.Finish(Crowd);


		#endregion Finishing
	}

}
