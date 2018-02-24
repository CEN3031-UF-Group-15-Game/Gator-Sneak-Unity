using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
// using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using UMA;

public class DK_RPG_UMA_Generator : MonoBehaviour {
	#region Variables

	[HideInInspector] public bool RPGPrepared = false;

	// Libraries
	[HideInInspector] public DKUMA_Variables _DKUMA_Variables;

	public DKSlotLibrary _DKSlotLibrary;
	public DKOverlayLibrary _DKOverlayLibrary;
	public DKRaceLibrary _DKRaceLibrary;

	[HideInInspector] public Transform _DK_UMA;

	public static bool AddToRPG = true;
	public static bool DirectToUMA2 = true;
	[HideInInspector] public bool Done = false;
	public bool RefreshOnStart = true;

	[System.Serializable]
	public class ConverterToUMA2 {
		public UMARecipeBase DefaultMaleRecipe;
		public UMARecipeBase DefaultFemaleRecipe;
	}
	public ConverterToUMA2 _ConverterToUMA2 = new ConverterToUMA2();

	#region Lists
	[System.Serializable]
	public class HornsData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();

		public void ClearAll(){
			SlotList.Clear();
			OverlayList.Clear();
		}

		public void Select(){
		}

		public void Assign(){
		}
	}


	[System.Serializable]
	public class HeadData {

		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> TattooList = new List<DKOverlayData>();
		public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
		public List<DKOverlayData> LipsList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();


		public void ClearAll(){
			SlotList.Clear();
			OverlayList.Clear();
			TattooList.Clear();
			MakeupList.Clear();
			OptionalList.Clear();

			Debug.Log ("Head Lists Cleared");
		}

		public void Select(){
			Debug.Log ("Test Select");
		}

		public void Assign(){
			Debug.Log ("Test Assign");
		}
	}
	[System.Serializable]
	public class BeardOvOnlyData {
		public List<DKOverlayData> BeardList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class BeardSlotOnlyData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class FaceHairData {
		public List<DKOverlayData> EyeBrowsList = new List<DKOverlayData>();
		public BeardOvOnlyData _BeardOverlayOnly = new BeardOvOnlyData();
		public BeardSlotOnlyData _BeardSlotOnly = new BeardSlotOnlyData();

	}
	[System.Serializable]
	public class EyesData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> AdjustList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class EyesIrisData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> AdjustList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class EyebrowsData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> AdjustList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class EyeLashData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class EyeLidsData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> TattooList = new List<DKOverlayData>();
		public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class EarsData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> TattooList = new List<DKOverlayData>();
		public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();

	}
	[System.Serializable]
	public class NoseData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> TattooList = new List<DKOverlayData>();
		public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class InnerMouthData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class MouthData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> TattooList = new List<DKOverlayData>();
		public List<DKOverlayData> LipsList = new List<DKOverlayData>();
		public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
		public InnerMouthData _InnerMouth = new InnerMouthData();
	}
	[System.Serializable]
	public class FaceData {
		public HeadData _Head = new HeadData();
		public HornsData _Horns = new HornsData();
		public FaceHairData _FaceHair = new FaceHairData();
		public EyesData _Eyes = new EyesData();
		public EyesIrisData _EyesIris = new EyesIrisData();
		public EyebrowsData _Eyebrows = new EyebrowsData();
		public EyeLashData _EyeLash = new EyeLashData();
		public EyeLidsData _EyeLids = new EyeLidsData();
		public EarsData _Ears = new EarsData();
		public NoseData _Nose = new NoseData();
		public MouthData _Mouth = new MouthData();
	} 
	[System.Serializable]
	public class OvOnlyData {
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class HairModuleData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class SlotOnlyData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public HairModuleData _HairModule = new HairModuleData();
	}
	[System.Serializable]
	public class HairData {
		public OvOnlyData _OverlayOnly = new OvOnlyData();
		public SlotOnlyData _SlotOnly = new SlotOnlyData();
	} 
	[System.Serializable]
	public class TorsoData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> TattooList = new List<DKOverlayData>();
		public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	} 
	[System.Serializable]
	public class HandsData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> TattooList = new List<DKOverlayData>();
		public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	} 
	[System.Serializable]
	public class LegsData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> TattooList = new List<DKOverlayData>();
		public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	} 
	[System.Serializable]
	public class FeetData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> TattooList = new List<DKOverlayData>();
		public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	}
	[System.Serializable]
	public class UnderwearData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
	} 
	[System.Serializable]
	public class BodyData {		
		public TorsoData _Torso = new TorsoData();
		public HandsData _Hands = new HandsData();
		public LegsData _Legs = new LegsData();		 
		public FeetData _Feet = new FeetData();
		public PartData _Wings = new PartData();
		public PartData _Tail = new PartData();
		public UnderwearData _Underwear = new UnderwearData();
	} 
	[System.Serializable]
	public class AvatarData {
		public FaceData _Face = new FaceData();
		public HairData _Hair = new HairData();
		public BodyData _Body = new BodyData();
	} 

	#region avatar Meshs and Textures
	public AvatarData _MaleAvatar = new AvatarData();
	public AvatarData _FemaleAvatar = new AvatarData();
	#endregion avatar Meshs and Textures

	[System.Serializable]
	public class PartData {
		public List<DKSlotData> SlotList = new List<DKSlotData>();
		public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
		public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
		public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
	} 

	#region Equipment
	[System.Serializable]
	public class EquipmentData {
		public PartData _Glasses = new PartData();
		public PartData _Mask = new PartData();

		public PartData _Head = new PartData();
		public PartData _Shoulder = new PartData();
		public PartData _Torso = new PartData();		 
		public PartData _Armband = new PartData();
		public PartData _LegBand = new PartData();
		public PartData _Wrist = new PartData();
		public PartData _Hands = new PartData();
		public PartData _Legs = new PartData();
		public PartData _Feet = new PartData();
		public PartData _Belt = new PartData();
		public PartData _Cloak = new PartData();
		public PartData _Collar = new PartData();
		public PartData _Backpack = new PartData();
		public PartData _LeftHand = new PartData();
		public PartData _RightHand = new PartData();
		public PartData _LeftRing = new PartData();
		public PartData _RightRing = new PartData();
	} 
	public EquipmentData _MaleEquipment = new EquipmentData();
	public EquipmentData _FemaleEquipment = new EquipmentData();

	#endregion Equipment
	#endregion Lists

	DK_UMA_GameSettings GameSettings;

	DK_UMACrowd _DK_UMACrowd;

	[HideInInspector] public bool ForcedPrepare = false;
	#endregion Variables

	void Awake (){
		if ( _MaleAvatar._Face._Head.LipsList == null )  _MaleAvatar._Face._Head.LipsList = new List<DKOverlayData>();
		if ( _MaleAvatar._Face._Head.SlotList == null )  _MaleAvatar._Face._Head.SlotList = new List<DKSlotData>();
		if ( _MaleAvatar._Face._Head.OverlayList == null )  _MaleAvatar._Face._Head.OverlayList = new List<DKOverlayData>();
		if ( _MaleAvatar._Face._Head.TattooList == null )  _MaleAvatar._Face._Head.TattooList = new List<DKOverlayData>();
		if ( _MaleAvatar._Face._Head.MakeupList == null )  _MaleAvatar._Face._Head.MakeupList = new List<DKOverlayData>();
		if ( _MaleAvatar._Face._Head.OptionalList == null )  _MaleAvatar._Face._Head.OptionalList = new List<DKOverlayData>();
		if ( _FemaleAvatar._Face._Head.LipsList == null )  _FemaleAvatar._Face._Head.LipsList = new List<DKOverlayData>();
		if ( _FemaleAvatar._Face._Head.SlotList == null )  _FemaleAvatar._Face._Head.SlotList = new List<DKSlotData>();
		if ( _FemaleAvatar._Face._Head.OverlayList == null )  _FemaleAvatar._Face._Head.OverlayList = new List<DKOverlayData>();
		if ( _FemaleAvatar._Face._Head.TattooList == null )  _FemaleAvatar._Face._Head.TattooList = new List<DKOverlayData>();
		if ( _FemaleAvatar._Face._Head.MakeupList == null )  _FemaleAvatar._Face._Head.MakeupList = new List<DKOverlayData>();
		if ( _FemaleAvatar._Face._Head.OptionalList == null )  _FemaleAvatar._Face._Head.OptionalList = new List<DKOverlayData>();
		PopulateAllLists();
	//	Debug.Log ( "Awake");
	}

	void Start () {
		RPGPrepared = false;
	//	if ( RefreshOnStart /*&& !Application.isPlaying*/ && !RPGPrepared )
		//	PopulateAllLists();
	//	Debug.Log ( "Starting");
	}

	public void PopulateAllLists(){
		GameObject DK_UMA =  GameObject.Find ("DK_UMA");
		if (_DK_UMA != null){	
		//	_DK_UMA = DK_UMA.transform;

			_DK_UMACrowd = GameObject.Find ("DKUMACrowd").GetComponent<DK_UMACrowd>();
			
			// Find the DKUMA_Variables
			_DKUMA_Variables = _DK_UMA.GetComponent<DKUMA_Variables>();
			if ( _DKUMA_Variables == null ) _DKUMA_Variables = _DK_UMA.gameObject.AddComponent<DKUMA_Variables>() as DKUMA_Variables;
			GameSettings = _DKUMA_Variables._DK_UMA_GameSettings;
			if ( GameSettings == null ) {
				Debug.LogError ( "The variable 'GameSettings'is missing from the 'DKUMA_Variables' component of the 'DK_UMA' object of your scene. Please assign itt using the inspector" );
			}
			else{
				_DK_UMACrowd.CleanLibraries ();
				PopulateLists();
			}
		}
	}

	public void PopulateLists(){		

		if ( !RPGPrepared || ForcedPrepare ) {
			// test variables
		//	Test ();

			RPGPrepared = true;
			_DKRaceLibrary = _DK_UMACrowd.raceLibrary;
			_DKSlotLibrary = _DK_UMACrowd.slotLibrary;
			_DKOverlayLibrary = _DK_UMACrowd.overlayLibrary;

			Debug.Log ("DK UMA : Preparing RPG Lists");
			if ( _DKUMA_Variables == null )
				_DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();
			if ( _DKUMA_Variables != null ){
				foreach ( DKRaceData race in _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary ){
					PopulateRace ( race );
				}
			}
			#if UNITY_EDITOR
			//	Debug.Log ("DK UMA RPG Helper : All the DK UMA elements have been declared to the DK UMA races of your current project. They are sorted by gender, take the time to take a look at a DK UMA Race in the inspector window to find the classes where the elements are displayed.");
			AssetDatabase.SaveAssets ();
			#endif
			Done = true;
			if ( Application.isPlaying ) RPGPrepared = true;
		}
	}

	public void VerifyRaces (){
		
	}

	public void PopulateRace ( DKRaceData race ){
		if ( race.name == "MangaMale2" )
			Debug.Log ( "PopulateRace MangaMale2.");

		_MaleAvatar = new AvatarData();
		_FemaleAvatar = new AvatarData();
		_MaleEquipment = new EquipmentData();
		_FemaleEquipment = new EquipmentData();

		GameSettings = _DKUMA_Variables._DK_UMA_GameSettings;

		// Find all the slots in the library
		foreach ( DKSlotData Slot in GameSettings._GameLibraries.DkSlotsLibrary ){
			if ( Slot == null ) {
					Debug.LogError ( "The SlotsLibrary reports a missing element, please use the 'Clean Lib' button of the 'Elements Manager' window.");
			}
			else if ( Slot.Active && Slot._UMA != null && Slot._LOD.MasterLOD == null ){

				#region Populate Face Lists
				// Add the Head slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "Head" && Slot.OverlayType == "Face" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race)  )
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Face._Head.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._Head.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Face._Head.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._Head.SlotList.Add (Slot);
				}
				// Add the _Horns slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "Horns" && Slot.OverlayType == "Horns" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race)  )
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleAvatar._Face._Horns.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._Horns.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleAvatar._Face._Horns.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._Horns.SlotList.Add (Slot);
				}
				// Add the Beard slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "Beard" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race) )
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Face._FaceHair._BeardSlotOnly.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._FaceHair._BeardSlotOnly.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Face._FaceHair._BeardSlotOnly.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._FaceHair._BeardSlotOnly.SlotList.Add (Slot);

				}
				// Add the Eyes slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Eyes" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Face._Eyes.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._Eyes.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Face._Eyes.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._Eyes.SlotList.Add (Slot);

				}
				// Add the Eyes Iris slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "EyesIris" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleAvatar._Face._EyesIris.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._EyesIris.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleAvatar._Face._EyesIris.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._EyesIris.SlotList.Add (Slot);

				}
				// Add the Eyebrows slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "Eyebrows" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleAvatar._Face._Eyebrows.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._Eyebrows.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleAvatar._Face._Eyebrows.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._Eyebrows.SlotList.Add (Slot);

				}

				// Add the Eyelash slots to the list
				if (Slot.Place != null
				    && Slot.Place.name ==  "Eyelash" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
				//	Debug.Log ("Eyelash test");
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Face._EyeLash.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._EyeLash.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Face._EyeLash.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._EyeLash.SlotList.Add (Slot);
					
				}
				// Add the eyelid slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "eyelid" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Face._EyeLids.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._EyeLids.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Face._EyeLids.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._EyeLids.SlotList.Add (Slot);
				}

				// Add the Ears slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Ears" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Face._Ears.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._Ears.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Face._Ears.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._Ears.SlotList.Add (Slot);
					
				}
				// Add the Nose slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Nose" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Face._Nose.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._Nose.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Face._Nose.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._Nose.SlotList.Add (Slot);
					
				}
				// Add the Mouth slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Mouth" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Face._Mouth.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._Mouth.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Face._Mouth.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._Mouth.SlotList.Add (Slot);
					
				}
				// Add the InnerMouth slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "InnerMouth" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Face._Mouth._InnerMouth.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Face._Mouth._InnerMouth.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Face._Mouth._InnerMouth.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Face._Mouth._InnerMouth.SlotList.Add (Slot);
					
				}
				#endregion Populate Face Lists

				#region Populate Hair Lists
				// Add the Hair slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Hair" && Slot.Elem == false  && Slot.OverlayType == "Hair"
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Hair._SlotOnly.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Hair._SlotOnly.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Hair._SlotOnly.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Hair._SlotOnly.SlotList.Add (Slot);
					
				}
				// Add the Hair Module slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Hair_Module" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Hair._SlotOnly._HairModule.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Hair._SlotOnly._HairModule.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Hair._SlotOnly._HairModule.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Hair._SlotOnly._HairModule.SlotList.Add (Slot);
					
				}
				#endregion Populate Hair Lists

				#region Populate Body Lists
				// Add the Torso slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Torso" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Body._Torso.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Body._Torso.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Body._Torso.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Body._Torso.SlotList.Add (Slot);
					
				}
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "Wings" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleAvatar._Body._Wings.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Body._Wings.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleAvatar._Body._Wings.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Body._Wings.SlotList.Add (Slot);

				}
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "Tail" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleAvatar._Body._Tail.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Body._Tail.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleAvatar._Body._Tail.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Body._Tail.SlotList.Add (Slot);

				}
				// Add the Hands slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name == "Hands" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Body._Hands.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Body._Hands.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Body._Hands.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Body._Hands.SlotList.Add (Slot);
					
				}
				// Add the Legs slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Legs" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Body._Legs.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Body._Legs.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Body._Legs.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Body._Legs.SlotList.Add (Slot);
					
				}
				// Add the Feet slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Feet" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Body._Feet.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Body._Feet.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Body._Feet.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Body._Feet.SlotList.Add (Slot);
					
				}
				// Add the Underwear slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "Underwear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleAvatar._Body._Underwear.SlotList.Contains(Slot) == false  ) 
						_MaleAvatar._Body._Underwear.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleAvatar._Body._Underwear.SlotList.Contains(Slot) == false  ) 
						_FemaleAvatar._Body._Underwear.SlotList.Add (Slot);
				}
				#endregion Populate Body Lists

				#region Populate Equipment Lists
				// Add the Glasses slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "GlassesWear" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleEquipment._Glasses.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Glasses.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleEquipment._Glasses.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Glasses.SlotList.Add (Slot);
				}
				// Add the Mask slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "MaskWear" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleEquipment._Mask.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Mask.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleEquipment._Mask.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Mask.SlotList.Add (Slot);
				}

				// Add the Head slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "HeadWear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._Head.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Head.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._Head.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Head.SlotList.Add (Slot);
				}

				// Add the Shoulder slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "ShoulderWear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._Shoulder.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Shoulder.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._Shoulder.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Shoulder.SlotList.Add (Slot);
				}

				// Add the WristWear slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "WristWear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._Wrist.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Wrist.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._Wrist.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Wrist.SlotList.Add (Slot);
				}

				// Add the ArmbandWear slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "ArmbandWear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._Armband.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Armband.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._Armband.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Armband.SlotList.Add (Slot);
				}

				// Add the Torso slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "TorsoWear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._Torso.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Torso.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._Torso.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Torso.SlotList.Add (Slot);
					
				}

				// Add the Hands slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "HandWear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._Hands.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Hands.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._Hands.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Hands.SlotList.Add (Slot);
					
				}
				// Add the Legs slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "LegsWear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._Legs.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Legs.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._Legs.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Legs.SlotList.Add (Slot);
					
				}
				// Add the Legband slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "LegBandWear" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleEquipment._LegBand.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._LegBand.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleEquipment._LegBand.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._LegBand.SlotList.Add (Slot);

				}
				// Add the Feet slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "FeetWear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
				//	if ( Slot.slotName.ToLower().Contains("dark") == true ) Debug.Log ( Slot.slotName+" / "+race.raceName);

					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._Feet.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Feet.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._Feet.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Feet.SlotList.Add (Slot);
					
				}
				// Add the Belt slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "BeltWear" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._Belt.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Belt.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._Belt.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Belt.SlotList.Add (Slot);
					
				}
				// Add the Collar slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
							&& Slot.Place.name ==  "Collar" 
				    && Slot._LegacyData.IsLegacy == false
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
								&& _MaleEquipment._Collar.SlotList.Contains(Slot) == false  ) 
								_MaleEquipment._Collar.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
								&& _FemaleEquipment._Collar.SlotList.Contains(Slot) == false  ) 
								_FemaleEquipment._Collar.SlotList.Add (Slot);
					
				}
				// Add the Cloak slots to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "CloakWear" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleEquipment._Cloak.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Cloak.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleEquipment._Cloak.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Cloak.SlotList.Add (Slot);

				}
				// Add Backpack to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "Backpack" 
					&& Slot._LegacyData.IsLegacy == false
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleEquipment._Backpack.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._Backpack.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleEquipment._Backpack.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._Backpack.SlotList.Add (Slot);

				}
				// Add the HandledLeft Slot to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "HandledLeft" 
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._LeftHand.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._LeftHand.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._LeftHand.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._LeftHand.SlotList.Add (Slot);
					
				}
				// Add the HandledRight Overlay to the list
				if (Slot.Place != null && Slot.OverlayType != null
				    && Slot.Place.name ==  "HandledRight" 
				    && Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
					    && _MaleEquipment._RightHand.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._RightHand.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
					    && _FemaleEquipment._RightHand.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._RightHand.SlotList.Add (Slot);
					
				}
				// Add the RingLeft Slot to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "RingLeft" 
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleEquipment._LeftRing.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._LeftRing.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleEquipment._LeftRing.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._LeftRing.SlotList.Add (Slot);
				}
				// Add the RingRight Overlay to the list
				if (Slot.Place != null && Slot.OverlayType != null
					&& Slot.Place.name ==  "RingRight" 
					&& Slot.Race.Contains(race.Race))
				{
					if ((Slot.Gender == "Both" || Slot.Gender == "Male" )
						&& _MaleEquipment._RightRing.SlotList.Contains(Slot) == false  ) 
						_MaleEquipment._RightRing.SlotList.Add (Slot);
					if ((Slot.Gender == "Both" || Slot.Gender == "Female" ) 
						&& _FemaleEquipment._RightRing.SlotList.Contains(Slot) == false  ) 
						_FemaleEquipment._RightRing.SlotList.Add (Slot);
				}
				#endregion Populate Equipment Lists
			}
		}

		// Find all the Overlays in the library
		foreach ( DKOverlayData Overlay in GameSettings._GameLibraries.DkOverlaysLibrary ){
			try { 
				if ( Overlay == null ) {
						Debug.LogError ( "The librarie reports a missing element, please use the 'Clean Lib' button of the 'Prepare DK Elements' window.");
				}
				else if ( Overlay && Overlay.Active && Overlay._UMA && Overlay._UMA.textureList[0] != null ){
					
					#region Populate Face Lists
					// Add the Head Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Head" && Overlay.OverlayType == "Face"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._Head.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._Head.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._Head.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._Head.OverlayList.Add (Overlay);
							
					}	
					// Add the Tatoo Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Head" && Overlay.OverlayType == "Tatoo"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._Head.TattooList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._Head.TattooList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._Head.TattooList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._Head.TattooList.Add (Overlay);
						
					}	
					// Add the Makeup Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Head" && Overlay.OverlayType == "Makeup"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._Head.MakeupList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._Head.MakeupList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._Head.MakeupList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._Head.MakeupList.Add (Overlay);
						
					}
					// Add the Lips Overlay to the list
					if (Overlay.OverlayType == "Lips"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._Mouth.LipsList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._Mouth.LipsList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._Mouth.LipsList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._Mouth.LipsList.Add (Overlay);
					}
					// Add the Beard slot Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Beard" && Overlay.OverlayType == "Beard"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._FaceHair._BeardSlotOnly.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._FaceHair._BeardSlotOnly.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._FaceHair._BeardSlotOnly.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._FaceHair._BeardSlotOnly.OverlayList.Add (Overlay);
						
					}
					// Add the Beard Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Head" && Overlay.OverlayType == "Beard"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._FaceHair._BeardOverlayOnly.BeardList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._FaceHair._BeardOverlayOnly.BeardList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._FaceHair._BeardOverlayOnly.BeardList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._FaceHair._BeardOverlayOnly.BeardList.Add (Overlay);
						
					}
					// Add the Eye brow Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Head" && Overlay.OverlayType == "Eyebrow"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._FaceHair.EyeBrowsList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._FaceHair.EyeBrowsList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._FaceHair.EyeBrowsList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._FaceHair.EyeBrowsList.Add (Overlay);
						
					}
					// Add the Eyes Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null && Overlay.name != null
					    && Overlay.Place.name ==  "Eyes" && Overlay.OverlayType == "Eyes" 
					    && Overlay.name.Contains("Adjust") == false
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._Eyes.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._Eyes.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._Eyes.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._Eyes.OverlayList.Add (Overlay);
						
					}
					// Add the Eyes Overlay Adjust to the list
					if (Overlay.Place != null && Overlay.OverlayType != null && Overlay.name != null
					    && Overlay.Place.name ==  "Eyes" && Overlay.OverlayType == "Eyes" 
					    && Overlay.name.Contains("Adjust") == true
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._Eyes.AdjustList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._Eyes.AdjustList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._Eyes.AdjustList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._Eyes.AdjustList.Add (Overlay);
						
					}

					// Add the Eyelash Overlay Adjust to the list
					if (Overlay.Place != null && Overlay.OverlayType == null
					    && Overlay.Place.name ==  "Eyelash"
					    && Overlay.Race.Contains(race.Race) )
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._EyeLash.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._EyeLash.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._EyeLash.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._EyeLash.OverlayList.Add (Overlay);
						
					}
					// Add the InnerMouth Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "InnerMouth"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Face._Mouth._InnerMouth.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Face._Mouth._InnerMouth.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Face._Mouth._InnerMouth.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Face._Mouth._InnerMouth.OverlayList.Add (Overlay);
						
					}

					#endregion Populate Face Lists

					#region Populate Hair Lists
					// Add the Hair Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Hair" && Overlay.OverlayType == "Hair"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Hair._OverlayOnly.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Hair._OverlayOnly.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Hair._OverlayOnly.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Hair._OverlayOnly.OverlayList.Add (Overlay);
						
					}
					/*
					// Add the Hair slot Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Hair" && Overlay.OverlayType == "Hair" && Overlay.Elem == false
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Hair._SlotOnly.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Hair._SlotOnly.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Hair._SlotOnly.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Hair._SlotOnly.OverlayList.Add (Overlay);
						
					}
					// Add the Hair module slot Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Hair_Module" && Overlay.OverlayType == "Hair" && Overlay.Elem == true
						&& Overlay.Race.Contains(race.Race)){
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Hair._SlotOnly._HairModule.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Hair._SlotOnly._HairModule.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Hair._SlotOnly._HairModule.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Hair._SlotOnly._HairModule.OverlayList.Add (Overlay);
						
					}*/
					#endregion Populate Hair Lists

					#region Populate Body Lists
					// Add the Torso Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Torso" && Overlay.OverlayType == "Flesh"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Torso.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Torso.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Torso.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Torso.OverlayList.Add (Overlay);
						
					}
					// Add the Torso Tatoo Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Torso" && Overlay.OverlayType == "Tatoo"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Torso.TattooList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Torso.TattooList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Torso.TattooList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Torso.TattooList.Add (Overlay);
						
					}	
					// Add the Torso Makeup Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Torso" && Overlay.OverlayType == "Makeup"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Torso.MakeupList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Torso.MakeupList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Torso.MakeupList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Torso.MakeupList.Add (Overlay);
						
					}
					// Add the Hands Tatoo Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Hands" && Overlay.OverlayType == "Tatoo"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Hands.TattooList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Hands.TattooList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Hands.TattooList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Hands.TattooList.Add (Overlay);
						
					}	
					// Add the Hands Makeup Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Hands" && Overlay.OverlayType == "Makeup"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Hands.MakeupList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Hands.MakeupList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Hands.MakeupList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Hands.MakeupList.Add (Overlay);
						
					}
					// Add the Legs Tatoo Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Legs" && Overlay.OverlayType == "Tatoo"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Legs.TattooList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Legs.TattooList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Legs.TattooList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Legs.TattooList.Add (Overlay);
						
					}	
					// Add the Legs Makeup Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Hands" && Overlay.OverlayType == "Makeup"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Legs.MakeupList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Legs.MakeupList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Legs.MakeupList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Legs.MakeupList.Add (Overlay);
						
					}
					// Add the Feet Tatoo Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Feet" && Overlay.OverlayType == "Tatoo"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Feet.TattooList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Feet.TattooList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Feet.TattooList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Feet.TattooList.Add (Overlay);
						
					}	
					// Add the Feet Makeup Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Feet" && Overlay.OverlayType == "Makeup"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Feet.MakeupList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Feet.MakeupList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Feet.MakeupList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Feet.MakeupList.Add (Overlay);
						
					}
					// Add the Underwear slots to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.OverlayType ==  "Underwear" 
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleAvatar._Body._Underwear.OverlayList.Contains(Overlay) == false  ) 
							_MaleAvatar._Body._Underwear.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleAvatar._Body._Underwear.OverlayList.Contains(Overlay) == false  ) 
							_FemaleAvatar._Body._Underwear.OverlayList.Add (Overlay);
					}
					#endregion Populate Body Lists

					#region Populate Wear Lists
					// Add the Glasses Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
						&& Overlay.Place.name ==  "GlassesWear" && Overlay.OverlayType == "GlassesWear"
						&& Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
							&& _MaleEquipment._Glasses.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Glasses.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
							&& _FemaleEquipment._Glasses.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Glasses.OverlayList.Add (Overlay);
					}
					// Add the Mask Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
						&& Overlay.Place.name ==  "MaskWear" && Overlay.OverlayType == "MaskWear"
						&& Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
							&& _MaleEquipment._Mask.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Mask.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
							&& _FemaleEquipment._Mask.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Mask.OverlayList.Add (Overlay);
					}
					// Add the Glasses Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
						&& ( Overlay.Place.name ==  "Head" 
						&& Overlay.Race.Contains(race.Race))
						&& Overlay.OverlayType == "GlassesWear")
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
							&& _MaleEquipment._Glasses.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Glasses.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
							&& _FemaleEquipment._Glasses.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Glasses.OverlayOnlyList.Add (Overlay);
					}
					// Add the Mask Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
						&& ( Overlay.Place.name ==  "Head" 
						&& Overlay.Race.Contains(race.Race))
						&& Overlay.OverlayType == "MaskWear")
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
							&& _MaleEquipment._Mask.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Mask.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
							&& _FemaleEquipment._Mask.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Mask.OverlayOnlyList.Add (Overlay);
					}

					// Add the Head Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "HeadWear" && Overlay.OverlayType == "HeadWear"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Head.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Head.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Head.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Head.OverlayList.Add (Overlay);
						
					}
					// Add the Head Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Head" 
					    && Overlay.OverlayType == "HeadWear"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Head.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Head.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Head.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Head.OverlayOnlyList.Add (Overlay);
						
					}
					// Add the Shoulder Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "ShoulderWear" && Overlay.OverlayType == "ShoulderWear"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Shoulder.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Shoulder.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Shoulder.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Shoulder.OverlayList.Add (Overlay);
						
					}
					// Add the Shoulder Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && (Overlay.Place.name ==  "Torso" || Overlay.Place.name ==  "Shoulder"
					    && Overlay.Race.Contains(race.Race)) 
					    && Overlay.OverlayType == "ShoulderWear")
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Shoulder.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Shoulder.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Shoulder.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Shoulder.OverlayOnlyList.Add (Overlay);
						
					}
					// Add the Torso Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "TorsoWear" && Overlay.OverlayType == "TorsoWear"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Torso.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Torso.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Torso.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Torso.OverlayList.Add (Overlay);
						
					}
					// Add the Torso Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Torso" 
					    && Overlay.OverlayType == "TorsoWear"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Torso.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Torso.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Torso.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Torso.OverlayOnlyList.Add (Overlay);
						
					}
					// Add the Hands Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "HandsWear" && Overlay.OverlayType == "HandsWear"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Hands.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Hands.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Hands.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Hands.OverlayList.Add (Overlay);
						
					}
					// Add the Hands Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && (Overlay.Place.name ==  "Torso" || Overlay.Place.name ==  "Hands"
					    && Overlay.Race.Contains(race.Race)) 
					    && Overlay.OverlayType == "HandsWear")
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Hands.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Hands.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Hands.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Hands.OverlayOnlyList.Add (Overlay);
						
					}
					// Add the Legs Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "LegsWear" && Overlay.OverlayType == "LegsWear"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Legs.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Legs.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Legs.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Legs.OverlayList.Add (Overlay);
						
					}
					// Add the Legs Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && ( Overlay.Place.name ==  "Torso" || Overlay.Place.name ==  "Legs" 
					    && Overlay.Race.Contains(race.Race))
					    && Overlay.OverlayType == "LegsWear")
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Legs.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Legs.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Legs.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Legs.OverlayOnlyList.Add (Overlay);
						
					}
					// Add the Feet Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "FeetWear" && Overlay.OverlayType == "FeetWear"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Feet.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Feet.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Feet.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Feet.OverlayList.Add (Overlay);
						
					}
				/*	// Add the Legs Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && ( Overlay.Place.name ==  "Torso" || Overlay.Place.name ==  "Feet" 
					    && Overlay.Race.Contains(race.Race))
					    && Overlay.OverlayType == "FeetWear")
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Feet.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Feet.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Feet.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Feet.OverlayOnlyList.Add (Overlay);
						
					}
					// Add the Feet Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "FeetWear" && Overlay.OverlayType == "FeetWear"
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Feet.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Feet.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Feet.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Feet.OverlayList.Add (Overlay);
						
					}*/
					// Add the Feet Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && ( Overlay.Place.name ==  "Torso" || Overlay.Place.name ==  "Feet" 
					    && Overlay.Race.Contains(race.Race))
					    && Overlay.OverlayType == "FeetWear")
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Feet.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Feet.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Feet.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Feet.OverlayOnlyList.Add (Overlay);
						
					}
					// Add the Belt Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "BeltWear" 
					    && ( Overlay.OverlayType == "BeltWear" || Overlay.OverlayType == "" 
					    && Overlay.Race.Contains(race.Race)))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Belt.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Belt.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Belt.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Belt.OverlayList.Add (Overlay);
						
					}
					// Add the Belt Wear Overlay Only to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && ( Overlay.Place.name ==  "Torso" || Overlay.Place.name ==  "Belt" 
					    && Overlay.Race.Contains(race.Race))
					    && Overlay.OverlayType == "BeltWear")
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Belt.OverlayOnlyList.Contains(Overlay) == false  ) 
							_MaleEquipment._Belt.OverlayOnlyList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Belt.OverlayOnlyList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Belt.OverlayOnlyList.Add (Overlay);
						
					}
					// Add the Collar Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "Collar" 
									&& ( Overlay.OverlayType == "Collar" || Overlay.OverlayType == "" 
					    && Overlay.Race.Contains(race.Race)))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._Collar.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Collar.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._Collar.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Collar.OverlayList.Add (Overlay);
						
					}
					// Add the Backpack Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
						&& Overlay.Place.name ==  "Backpack" 
						&& ( Overlay.OverlayType == "Backpack" || Overlay.OverlayType == "" 
							&& Overlay.Race.Contains(race.Race)))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
							&& _MaleEquipment._Backpack.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Backpack.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
							&& _FemaleEquipment._Backpack.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Backpack.OverlayList.Add (Overlay);

					}
					// Add the Cloak Wear Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
						&& Overlay.Place.name ==  "CloakWear" 
						&& ( Overlay.OverlayType == "CloakWear" || Overlay.OverlayType == "" 
						&& Overlay.Race.Contains(race.Race)))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
							&& _MaleEquipment._Cloak.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._Cloak.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
							&& _FemaleEquipment._Cloak.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._Cloak.OverlayList.Add (Overlay);

					}
					// Add the HandledLeft Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "HandledLeft" 
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._LeftHand.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._LeftHand.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._LeftHand.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._LeftHand.OverlayList.Add (Overlay);
						
					}
					// Add the HandledRight Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
					    && Overlay.Place.name ==  "HandledRight" 
					    && Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
						    && _MaleEquipment._RightHand.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._RightHand.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
						    && _FemaleEquipment._RightHand.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._RightHand.OverlayList.Add (Overlay);
					}
					// Add the RingLeft Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
						&& Overlay.Place.name ==  "RingLeft" 
						&& Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
							&& _MaleEquipment._LeftRing.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._LeftRing.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
							&& _FemaleEquipment._LeftRing.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._LeftRing.OverlayList.Add (Overlay);

					}
					// Add the RingRight Overlay to the list
					if (Overlay.Place != null && Overlay.OverlayType != null
						&& Overlay.Place.name ==  "RingRight" 
						&& Overlay.Race.Contains(race.Race))
					{
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Male" )
							&& _MaleEquipment._RightRing.OverlayList.Contains(Overlay) == false  ) 
							_MaleEquipment._RightRing.OverlayList.Add (Overlay);
						if ((Overlay.Gender == "Both" || Overlay.Gender == "Female" ) 
							&& _FemaleEquipment._RightRing.OverlayList.Contains(Overlay) == false  ) 
							_FemaleEquipment._RightRing.OverlayList.Add (Overlay);

					}
					#endregion Populate Wear Lists
				}
			}
			catch (System.IndexOutOfRangeException e) { Debug.LogError ("DK UMA : Some texture is missing from your Overlay. fix it by using the 'Fix Elements' option in the 'Elements Manager'. "+e); }
		}
		race._Male._AvatarData = _MaleAvatar;
		race._Male._EquipmentData = _MaleEquipment;
		race._Female._AvatarData = _FemaleAvatar;
		race._Female._EquipmentData = _FemaleEquipment;
		#if UNITY_EDITOR
		EditorUtility.SetDirty (race);
		#endif
	}
}
