using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UMA;

[System.Serializable]
public class DK_UMA_GameSettings : ScriptableObject {

//	public DK_UMA_RPG_Game_Libraries RPG_Library;

	#region Variables

	public bool Debugger = false;

	public Transform Player;
	public UMA.UMAData PlayerUMA;
	public bool SaveToFile = true;
	public enum UMAVersionEnum { version205, version25 }
	public UMAVersionEnum UMAVersion = UMAVersionEnum.version205;
	public List<UMA.UMAData> LODAvatarsInScene;

	public float MinDnaValueLimit = -0.5f;
	public float MaxDnaValueLimit = 2;
	/*
	[System.Serializable]
	public class AnatomyData {
		public bool Head = true;
		public bool Torso = true;
		public bool SeparatedArms = false;
		public bool Hands = true;
		public bool Legs = true;
		public bool Feet = true;
	}

	[System.Serializable]
	public class AnatomyCasesData {
		public enum AnatomyRuleChoice {EachAvatar, AllAvatars};
		public AnatomyRuleChoice AnatomyRule = new AnatomyRuleChoice();
		public enum AnatomyChoice {CompleteAvatar, NoHead, NoHeadNoArmsNoHands};
		public AnatomyChoice AnatomyToCreate = new AnatomyChoice();
		public AnatomyData CompleteAvatar = new AnatomyData();
		public AnatomyData NoHead = new AnatomyData();
		public AnatomyData NoHeadNoArmsNoHands = new AnatomyData();
		public AnatomyData AnatomyCase4 = new AnatomyData();
	}
	public AnatomyCasesData AnatomyCases = new AnatomyCasesData();
*/
	[System.Serializable]
	public class DefaultDatas {
		public UMA.UMAMaterial UMAMaterial;
		public ColorPresetData DefaultColorPreset;
	}
	public DefaultDatas MiscDefaultDatas = new DefaultDatas();

	[System.Serializable]
	public class ColorDatas {
		public float FleshVariation = 0.3f;
		public float HairVariation = 0.15f;
		public float WearVariation = 0.15f;
	}
	public ColorDatas Colors = new ColorDatas();

	[System.Serializable]
	public class RaceDefaultData {
		public DKRaceData FemaleRace;
		public DKRaceData MaleRace;
		public UMA.RaceData UMAFemaleRace;
		public UMA.RaceData UMAMaleRace;
	}
	public RaceDefaultData DefaultRaceData = new RaceDefaultData();

	[System.Serializable]
	public class DataBasesData {
		public DK_UMA_Avatars_Databases Avatars;
	}
	public DataBasesData Databases = new DataBasesData();

	[System.Serializable]
	public class ControllerData {
		public bool AddBasicLocomotion = true;
		public bool AddDKController = false;
	}
	public ControllerData _Controller = new ControllerData();

	#region PlugIns
	public class NaturalBehavData {
		public bool UseNaturalBehaviour;
		public bool PlayerOnly;
	}

	[System.Serializable]
	public class PlugInsData {
		NaturalBehavData NaturalBehaviour/* = new NaturalBehavData()*/;
	}
	public PlugInsData _PluginsSettings = new PlugInsData();
	#endregion PlugIns

	[System.Serializable]
	public class LODData {
		public bool UseLOD = false;
		[HideInInspector]
		public bool UseMeshLOD = false;
		public float Frequency = 0.5f;
		public int LOD0Resolution = 2048;
		public int LOD1Distance = 5;
		public int LOD1Resolution = 1024;
		public int LOD2Distance = 10;
		public int LOD2Resolution = 512;
		public int LOD3Distance = 15;
		public int LOD3Resolution = 256;
		public int LOD4Distance = 25;
		public int LOD4Resolution = 128;
	}
	public LODData _LOD = new LODData();

	[System.Serializable]
	public class EquipmentSetsData {
		public bool UseSets = true;
	//	public DKEquipmentSetListData SetsList/* = new DKEquipmentSetListData ()*/;
		public List<DKEquipmentSetData> EquipmentSetsList;
	}
	public EquipmentSetsData EquipmentSets = new EquipmentSetsData ();

	[System.Serializable]
	public class LibrariesData {
		public bool UseLibWizard = true;
		public List<DKRaceData> DkRacesLibrary/* = new List<DKSlotData>()*/;
		public List<UMA.RaceData> UmaRacesLibrary/* = new List<DKSlotData>()*/;

		public List<DKSlotData> DkSlotsLibrary/* = new List<DKSlotData>()*/;
		public List<DKOverlayData> DkOverlaysLibrary/* = new List<DKOverlayData>()*/;
		public List<UMA.SlotDataAsset> UmaSlotsLibrary/* = new List<UMA.SlotDataAsset>()*/;
		public List<UMA.OverlayDataAsset> UmaOverlaysLibrary/* = new List<UMA.OverlayDataAsset>()*/;
		public List<ColorPresetData> ColorPresetsList;
		public List<DK_SlotsAnatomyElement> PlacesList;
	}
	public LibrariesData _GameLibraries = new LibrariesData();

	[System.Serializable]
	public class AllElementsData {
		public List<DKSlotData> DKSlotsList = new List<DKSlotData>();
		public List<string> DKSlotsNamesList = new List<string>();
		public List<UMA.SlotDataAsset> UMASlotsList = new List<UMA.SlotDataAsset>();
		public List<DKOverlayData> DKOverlaysList = new List<DKOverlayData>();
		public List<string> DKOverlaysNamesList = new List<string>();
		public List<UMA.OverlayDataAsset> UMAOverlaysList = new List<UMA.OverlayDataAsset>();
	}
	public AllElementsData AllElements = new AllElementsData();

	[System.Serializable]
	public class PhysicsData {
		public UMA.SlotDataAsset CapsuleColliderSlot;
		public UMA.SlotDataAsset PhysicsStandardSlot;
		public UMA.SlotDataAsset PhysicsFemaleDefaultSlot;
		public UMA.SlotDataAsset PhysicsMaleDefaultSlot;
	}
	public PhysicsData Physics = new PhysicsData();
	#endregion Variables

	void Start (){
	//	if ( EquipmentSets.SetsList == null ) EquipmentSets.SetsList = new DKEquipmentSetListData ();
		if ( EquipmentSets.EquipmentSetsList == null ) EquipmentSets.EquipmentSetsList = new  List<DKEquipmentSetData>();
		if ( _GameLibraries.DkRacesLibrary == null ) _GameLibraries.DkRacesLibrary = new List<DKRaceData>();
		if ( _GameLibraries.UmaRacesLibrary == null ) _GameLibraries.UmaRacesLibrary = new List<UMA.RaceData>();
		if ( _GameLibraries.DkSlotsLibrary == null ) _GameLibraries.DkSlotsLibrary = new List<DKSlotData>();
		if ( _GameLibraries.DkOverlaysLibrary == null ) _GameLibraries.DkOverlaysLibrary = new List<DKOverlayData>();
		if ( _GameLibraries.UmaSlotsLibrary == null ) _GameLibraries.UmaSlotsLibrary = new List<UMA.SlotDataAsset>();
		if ( _GameLibraries.UmaOverlaysLibrary == null ) _GameLibraries.UmaOverlaysLibrary = new List<UMA.OverlayDataAsset>();
		if ( _GameLibraries.ColorPresetsList == null ) _GameLibraries.ColorPresetsList = new List<ColorPresetData>();
		if ( _GameLibraries.PlacesList == null ) _GameLibraries.PlacesList = new List<DK_SlotsAnatomyElement>();
		if ( Databases.Avatars == null ) Databases.Avatars = new DK_UMA_Avatars_Databases();
	}

	void CheckPlayerInLODList (){

	}

	public void SaveSettings (){
		#if UNITY_EDITOR
		EditorUtility.SetDirty (this);
		AssetDatabase.SaveAssets ();
		#endif

	}

	#region Special Rules
	public enum RulesOperation { None, SendColorFromChannel0To1, SendColorFromChannel0To1And2, SendColorFromChannel0To1And2And3 };

	public List<UMAMaterialRuleData> UMAMaterialRulesList;

	[System.Serializable]
	public class UMAMaterialRuleData
	{
		public string Name;
		public UMAMaterial umaMaterial;
		public RulesOperation Rule = RulesOperation.None;		
	}

	public void VerifyUMAMaterialRule ( OverlayData Overlay ){

		if ( Overlay && Overlay.asset.material ){
			foreach ( UMAMaterialRuleData UMAMatRule in UMAMaterialRulesList ){
				if ( UMAMatRule.umaMaterial == Overlay.asset.material ){
					ApplyUMAMaterialRule ( Overlay, UMAMatRule );
				}
			}
		}
	}

	public void ApplyUMAMaterialRule ( OverlayData Overlay, UMAMaterialRuleData UMAMatRule ){
		if ( UMAMatRule.Rule == RulesOperation.SendColorFromChannel0To1 ){
			if ( Overlay.asset.material.channels.Length >= 2 ){
				Overlay.SetColor(1, Overlay.GetColor(0));
			//	Debug.Log ("SendColorFromChannel applied for "+Overlay.overlayName+" ("+UMAMatRule.Rule.ToString()+")");
			}
		}
		if ( UMAMatRule.Rule == RulesOperation.SendColorFromChannel0To1And2 ){
			if ( Overlay.asset.material.channels.Length >= 3 ){
				Overlay.SetColor(1, Overlay.GetColor(0));
				Overlay.SetColor(2, Overlay.GetColor(0));
			//	Debug.Log ("SendColorFromChannel applied for "+Overlay.overlayName+" ("+UMAMatRule.Rule.ToString()+")");
			}
		}
		if ( UMAMatRule.Rule == RulesOperation.SendColorFromChannel0To1And2And3 ){
			if ( Overlay.asset.material.channels.Length >= 4 ){
				Overlay.SetColor(1, Overlay.GetColor(0));
				Overlay.SetColor(2, Overlay.GetColor(0));
				Overlay.SetColor(3, Overlay.GetColor(0));
			//	Debug.Log ("SendColorFromChannel applied for "+Overlay.overlayName+" ("+UMAMatRule.Rule.ToString()+")");
			}
		}
	}
	#endregion Special Rules

	#region Ensure Libraries
	public void EnsureAllLibraries (){
		EnsureDKSlotsLibrary ();
		EnsureDKOverlaysLibrary ();
	}

	public void EnsureDKSlotsLibrary (){
		DKSlotLibrary _SlotLibrary = FindObjectOfType<DKSlotLibrary>();
		if ( _SlotLibrary != null ){
			Debug.Log ("EnsureDKSlotsLibrary");
			List<DKSlotData> SlotsList = _SlotLibrary.slotElementList.ToList();
			if ( SlotsList.Count == 0 ){
				Debug.Log ("EnsureDKSlotsLibrary 2");
				foreach ( DKSlotData DKSlot in _GameLibraries.DkSlotsLibrary ){
					if ( DKSlot != null ) _SlotLibrary.AddSlot ( DKSlot.name, DKSlot );
				}
			}
		}
	}

	public void EnsureDKOverlaysLibrary (){
		DKOverlayLibrary _OverlayLibrary = FindObjectOfType<DKOverlayLibrary>();
		if ( _OverlayLibrary != null ){
			List<DKOverlayData> SlotsList = _OverlayLibrary.overlayElementList.ToList();
			if ( SlotsList.Count == 0 ){
				foreach ( DKOverlayData DKOverlay in _GameLibraries.DkOverlaysLibrary ){
					if ( DKOverlay != null ) _OverlayLibrary.AddOverlay ( DKOverlay.name, DKOverlay );
				}
			}
		}
	}
	#endregion Ensure Libraries
}
