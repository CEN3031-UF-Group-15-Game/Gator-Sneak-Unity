using System.IO;
using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UMA;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class DKUMA_Variables : MonoBehaviour {

	#region Variables
	[HideInInspector] public bool UseDkUMA = true;
	[HideInInspector] public bool UseUMA;

//	public float MinDnaValue = 0f;
//	public float MaxDnaValue = 1f;

	[HideInInspector] public DK_RPG_UMA SelectedAvatar;

	DK_UMACrowd _DK_UMACrowd;
//	DKUMAGenerator _DKUMAGenerator ;
	DKUMACustomization _DKUMACustomization;

	DK_GeneratorPresetLibrary ActivePresetLib;

	[HideInInspector] public DKSlotLibrary ActiveSlotLibrary;
	[HideInInspector] public DKSlotLibrary PrefabSlotLibrary;
//	public DKSlotLibrary BodySlotLibrary;
//	public DKSlotLibrary WearSlotLibrary;

	[HideInInspector] public DKOverlayLibrary ActiveOverlayLibrary;
	[HideInInspector] public DKOverlayLibrary PrefabOverlayLibrary;
//	public DKOverlayLibrary BodyOverlayLibrary;
//	public DKOverlayLibrary WearOverlayLibrary;

	[HideInInspector] public DKRaceLibrary ActiveRaceLibrary;
	[HideInInspector] public DKRaceLibrary DefaultRaceLibrary;
	[HideInInspector] public DKRaceLibrary RaceLibrary;

	[HideInInspector] public static RaceLibrary _RaceLibrary;
	[HideInInspector] public static SlotLibrary _SlotLibrary;
	[HideInInspector] 	public static OverlayLibrary _OverlayLibrary;

	public UMA.UMAMaterial DefaultUmaMaterial;

	public DK_UMA_GameSettings _DK_UMA_GameSettings;

	[System.Serializable]
	public class LinkedUMASlotData {
		public string Name = "";
		public int index = -1;
		public SlotDataAsset UMASlot;
		public DKSlotData DKSlot;

	}
	[HideInInspector] public List<LinkedUMASlotData> LinkedUMASlotDatasList = new List<LinkedUMASlotData>();
	[HideInInspector] public List<SlotDataAsset> LinkedUMASlotsList = new List<SlotDataAsset>();

//	[HideInInspector] 
	public List<DKSlotData> DKSlotsList = new List<DKSlotData>();
//	[HideInInspector] public List<string> DKSlotsNamesList = new List<string>();
	[HideInInspector] public List<UMA.SlotDataAsset> UMASlotsList = new List<UMA.SlotDataAsset>();
//	[HideInInspector] 
	public List<DKOverlayData> DKOverlaysList = new List<DKOverlayData>();
	[HideInInspector] public List<string> DKOverlaysNamesList = new List<string>();
	[HideInInspector] public List<UMA.OverlayDataAsset> UMAOverlaysList = new List<UMA.OverlayDataAsset>();


	[HideInInspector] public List<Texture2D> PreviewsList = new List<Texture2D>();
	[HideInInspector] public List<Texture2D> OvPreviewsList = new List<Texture2D>();
	

	[System.Serializable]
	public class EquipmentSetsData {
		public bool UseSets = true;
		public DKEquipmentSetListData _EquipmentSetsData; // = new DKEquipmentSetListData ();
	}
	public EquipmentSetsData _EquipmentSets = new EquipmentSetsData();

	public PhysicMaterial DK_UMA_Collider_Material;
	#endregion Variables

	void Init (){
		
	}

	void Awake()
	{
		// clear PlayerUMA (used for LOD order)
		if ( _DK_UMA_GameSettings != null )
			_DK_UMA_GameSettings.PlayerUMA = null;

		// Prepare the RPG lists
		PopulateLibraries ();

		if(_EquipmentSets._EquipmentSetsData == null) _EquipmentSets._EquipmentSetsData = ScriptableObject.CreateInstance<DKEquipmentSetListData> ();
	}

	void OnEnable (){
		_DK_UMACrowd = FindObjectOfType<DK_UMACrowd>();
		VerifyLibraries ();
	}

	public void VerifyLibraries (){
		_DK_UMACrowd = FindObjectOfType<DK_UMACrowd>();
		if ( _DK_UMACrowd.slotLibrary.slotElementList.Length == 0 
			|| _DK_UMACrowd.overlayLibrary.overlayElementList.Length == 0
			|| _DK_UMACrowd.raceLibrary.raceElementList.Length == 0 ){
			PopulateLibraries ();
		}
	}

	public void PopulateLibraries (){
		CleanLibraries ();
		AddToLib ();
		AddToDKRacesLib ();
		AddToUMALib ();	
	}

	public void VerifyDKSlotsLib (){		
		DKSlotLibrary SlotLibrary = GameObject.Find("DKSlotLibrary").GetComponent<DKSlotLibrary>();
		// Slots
		for (int i = 0; i < _DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary.Count; i++) {
			if ( SlotLibrary.slotDictionary.ContainsValue(_DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary[i] as DKSlotData) == false ){
				if ( _DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary[i] != null )
					SlotLibrary.AddSlot( _DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary[i].name, _DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary[i] as DKSlotData);
				else {
			//		SearchAll ();
				}
			}
		}
	}

	public void VerifyDKOverlaysLib (){
		DKOverlayLibrary OverlayLibrary = GameObject.Find("DKOverlayLibrary").GetComponent<DKOverlayLibrary>();

		// Overlays
		for (int i = 0; i < _DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary.Count; i++) {
			if ( OverlayLibrary.overlayDictionary.ContainsValue(_DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary[i] as DKOverlayData) == false ){
				if ( OverlayLibrary == null ) Debug.LogError ("OverlayLibrary == null");
				if ( _DK_UMA_GameSettings == null ) Debug.LogError ("_DK_UMA_GameSettings == null");
				if ( _DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary[i] == null ) Debug.LogError ("_DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary[i] == null");
				if ( _DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary[i] != null )
					OverlayLibrary.AddOverlay( _DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary[i].name, _DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary[i] as DKOverlayData);
				else {
			//		SearchAll ();
				}
			}
		}
	}

	void AddToLib (){
		VerifyDKSlotsLib ();
		VerifyDKOverlaysLib ();
	}

	public void AddToDKRacesLib (){
		RaceLibrary = GameObject.Find("DKRaceLibrary").GetComponent<DKRaceLibrary>();

		// Races
		for (int i = 0; i < _DK_UMA_GameSettings._GameLibraries.DkRacesLibrary.Count; i++) {
			if ( RaceLibrary.raceDictionary.ContainsValue(_DK_UMA_GameSettings._GameLibraries.DkRacesLibrary[i] as DKRaceData) == false ){
				DKRaceData race = _DK_UMA_GameSettings._GameLibraries.DkRacesLibrary[i];
				if ( race != null ){
					if ( race.name.Contains ( "Default" ) == false && race.Active )
					if ( race != null ) RaceLibrary.AddRace( race as DKRaceData);
				}
				else {
			//		SearchAll ();
				}
			}
		}
	}

	void AddToUMALib (){
//		GameObject DK_UMA = GameObject.Find("DK_UMA");

		// get DK UMA Game Settings
		#if UMADCS
		if ( _SlotLibrary == null ) _SlotLibrary =FindObjectOfType<DynamicSlotLibrary>();
		if ( _OverlayLibrary == null ) _OverlayLibrary = FindObjectOfType<DynamicOverlayLibrary>();
		#else
		if ( _SlotLibrary == null ) _SlotLibrary =FindObjectOfType<SlotLibrary>();
		if ( _OverlayLibrary == null ) _OverlayLibrary = FindObjectOfType<OverlayLibrary>();
		#endif

		List<UMA.SlotDataAsset> LibSlotsList = new List<UMA.SlotDataAsset>();
		if ( _SlotLibrary != null ) LibSlotsList = _SlotLibrary.GetAllSlotAssets ().ToList ();

		// Slots
		if ( _SlotLibrary != null ) for (int i = 0; i < _DK_UMA_GameSettings._GameLibraries.UmaSlotsLibrary.Count; i++) {
				if ( LibSlotsList.Contains( _DK_UMA_GameSettings._GameLibraries.UmaSlotsLibrary[i] as UMA.SlotDataAsset) == false)
					_SlotLibrary.AddSlotAsset( _DK_UMA_GameSettings._GameLibraries.UmaSlotsLibrary[i] as UMA.SlotDataAsset);
			}

		List<UMA.OverlayDataAsset> LibOvsList = new List<UMA.OverlayDataAsset>();
		if ( _OverlayLibrary != null ) LibOvsList = _OverlayLibrary.GetAllOverlayAssets ().ToList ();

		// Overlays

		if ( _OverlayLibrary != null )for (int i = 0; i < _DK_UMA_GameSettings._GameLibraries.UmaOverlaysLibrary.Count; i++) {
				if ( LibOvsList.Contains(_DK_UMA_GameSettings._GameLibraries.UmaOverlaysLibrary[i] as UMA.OverlayDataAsset) == false)
					_OverlayLibrary.AddOverlayAsset( _DK_UMA_GameSettings._GameLibraries.UmaOverlaysLibrary[i] as UMA.OverlayDataAsset);
			}
		AddToUMARaceLib ();
	}

	public void AddToUMARaceLib (){
		#if UMADCS
		if ( _RaceLibrary == null ) _RaceLibrary = FindObjectOfType<DynamicRaceLibrary>();
		#else
		if ( _RaceLibrary == null ) _RaceLibrary = FindObjectOfType<RaceLibrary>();
		#endif

		List<UMA.RaceData> LibRacesList = new List<UMA.RaceData>();
		if ( _RaceLibrary != null ) LibRacesList = _RaceLibrary.GetAllRaces ().ToList ();

		// Races
		if ( _RaceLibrary != null )for (int i = 0; i < _DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Count; i++) {
				if ( LibRacesList.Contains(_DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i] as UMA.RaceData) == false)
					_RaceLibrary.AddRace( _DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i] as UMA.RaceData);
			}
	}

	#region Search Voids
	public void SearchAll (){
		LinkedUMASlotDatasList.Clear();
		LinkedUMASlotsList.Clear();
	//	DKSlotsNamesList.Clear ();
		DKOverlaysNamesList.Clear ();

		#if UNITY_EDITOR
		if ( _DK_UMA_GameSettings != null ){
			SearchUMASlots ();
			SearchDKSlots ();
			SearchUMAOverlays ();
			SearchDKOverlays ();
			SearchEquipmentSets ();
			SearchUMARaces ();
			SearchDKRaces ();

			EditorUtility.SetDirty (_DK_UMA_GameSettings);
			AssetDatabase.SaveAssets ();
		}
		#endif
	}

	public void SearchDKUMA (){
		LinkedUMASlotDatasList.Clear();
		LinkedUMASlotsList.Clear();
	//	DKSlotsNamesList.Clear ();
		DKOverlaysNamesList.Clear ();
		
		SearchDKSlots ();
		SearchDKOverlays ();
	}

	public void SearchUMA (){
		SearchUMASlots ();
		SearchUMAOverlays ();
	}

	public void SearchEquipmentSets (){
		_DK_UMA_GameSettings.EquipmentSets.EquipmentSetsList.Clear ();

		#if UNITY_EDITOR
		FindAssets( typeof(DKEquipmentSetData) , ".asset" );
		#endif
	}

	public void SearchDKRaces (){
		_DK_UMA_GameSettings._GameLibraries.DkRacesLibrary.Clear ();

		#if UNITY_EDITOR
		FindAssets( typeof(DKRaceData) , ".asset" );
		#endif
	}

	public void SearchUMARaces (){
		_DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Clear ();

		#if UNITY_EDITOR
		FindAssets( typeof(UMA.RaceData) , ".asset" );
		#endif
	}

	void SearchUMASlots (){
		UMASlotsList.Clear ();
		GameObject UMACrowdObj;

		UMACrowdObj = GameObject.Find ("DKUMACrowd");
		if ( UMACrowdObj != null ){
			DK_UMACrowd _DK_UMACrowd =  UMACrowdObj.GetComponent<DK_UMACrowd>();
			if ( _DK_UMACrowd != null && _DK_UMACrowd.Default.ResearchDefault._SlotData != null ){
				System.Type type = _DK_UMACrowd.Default.ResearchDefault._SlotData.GetType() ;

				#if UNITY_EDITOR
				FindAssets( type , ".asset" );
				#endif
			}
			else Debug.LogError ( "The '_DK_UMACrowd.Default.ResearchDefault._SlotData' is not assigned to the '_DK_UMACrowd' component of the 'DKUMACrowd' object of your scene. " +
				"Select the 'DKUMACrowd' object and assign the 'DefaultUMASlot' object from your project to the 'Default.ResearchDefault._SlotData' variable." );
		}
		else Debug.LogError ( "The 'DKUMACrowd' object is not present in your scene. Install the DK UMA objects to your scene by opening the DK UMA Editor window, it is automated." );

	}
	void SearchDKSlots (){
		DKSlotsList.Clear ();
		LinkedUMASlotDatasList.Clear();
		LinkedUMASlotsList.Clear();

		_DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Clear ();
		_DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary.Clear ();
		_DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary.Clear ();

	//	DKSlotsNamesList.Clear ();
		PreviewsList.Clear ();
		GameObject UMACrowdObj;

		UMACrowdObj = GameObject.Find ("DKUMACrowd");
		if ( UMACrowdObj != null ){
			DK_UMACrowd _DK_UMACrowd =  UMACrowdObj.GetComponent<DK_UMACrowd>();
			if ( _DK_UMACrowd != null && _DK_UMACrowd.Default.ResearchDefault._DKSlotData != null ){
				System.Type type = _DK_UMACrowd.Default.ResearchDefault._DKSlotData.GetType() ;
				#if UNITY_EDITOR
				FindAssets( type , ".asset" );
				#endif
			}
			else Debug.LogError ( "The '_DK_UMACrowd.Default.ResearchDefault._DKSlotData' is not assigned to the '_DK_UMACrowd' component of the 'DKUMACrowd' object of your scene. " +
				"Select the 'DKUMACrowd' object and assign the 'DefaultDKSlot' object from your project to the 'Default.ResearchDefault._DKSlotData' variable." );
		}
		else Debug.LogError ( "The 'DKUMACrowd' object is not present in your scene. Install the DK UMA objects to your scene by opening the DK UMA Editor window, it is automated." );
	}

	void SearchUMAOverlays (){
		UMAOverlaysList.Clear ();
		GameObject UMACrowdObj;

		UMACrowdObj = GameObject.Find ("DKUMACrowd");
		if ( UMACrowdObj != null ){	
			DK_UMACrowd _DK_UMACrowd =  UMACrowdObj.GetComponent<DK_UMACrowd>();
			if ( _DK_UMACrowd != null && _DK_UMACrowd.Default.ResearchDefault._OverlayData != null ){
				System.Type type = _DK_UMACrowd.Default.ResearchDefault._OverlayData.GetType() ;
			//	GetAssetsOfType( type , ".asset" );
				#if UNITY_EDITOR
				FindAssets( type , ".asset" );
				#endif
			}
			else Debug.LogError ( "The '_DK_UMACrowd.Default.ResearchDefault._OverlayData' is not assigned to the '_DK_UMACrowd' component of the 'DKUMACrowd' object of your scene. " +
				"Select the 'DKUMACrowd' object and assign the 'DefaultUMAOverlay' object from your project to the 'Default.ResearchDefault._OverlayData' variable." );
		}
		else Debug.LogError ( "The 'DKUMACrowd' object is not present in your scene. Install the DK UMA objects to your scene by opening the DK UMA Editor window, it is automated." );

	}

	void SearchDKOverlays (){
		DKOverlaysList.Clear ();
		DKOverlaysNamesList.Clear ();
		OvPreviewsList.Clear ();

		GameObject UMACrowdObj;

		UMACrowdObj = GameObject.Find ("DKUMACrowd");
		if ( UMACrowdObj != null ){
			DK_UMACrowd _DK_UMACrowd =  UMACrowdObj.GetComponent<DK_UMACrowd>();
			if ( _DK_UMACrowd != null && _DK_UMACrowd.Default.ResearchDefault._DKOverlayData != null ){
				System.Type typeDK = _DK_UMACrowd.Default.ResearchDefault._DKOverlayData.GetType() ;
				#if UNITY_EDITOR
				FindAssets( typeDK , ".asset" );
				#endif
			}
			else Debug.LogError ( "The '_DK_UMACrowd.Default.ResearchDefault._DKOverlayData' is not assigned to the '_DK_UMACrowd' component of the 'DKUMACrowd' object of your scene. " +
				"Select the 'DKUMACrowd' object and assign the 'DefaultDKOverlay' object from your project to the 'Default.ResearchDefault._DKOverlayData' variable." );
		}
		else Debug.LogError ( "The 'DKUMACrowd' object is not present in your scene. Install the DK UMA objects to your scene by opening the DK UMA Editor window, it is automated." );

	}
	#endregion Search Voids
	
	#region Search Elements
	#if UNITY_EDITOR
	public void FindAssets(System.Type type, string fileExtension) {
		EditorUtility.SetDirty (_DK_UMA_GameSettings);

		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:"+type.Name, lookFor);

		// for DK Races
		if ( type.Name == "DKRaceData" ){
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				DKRaceData element = (DKRaceData)AssetDatabase.LoadAssetAtPath(path, typeof(DKRaceData));
				if ( element.UMA != null )
					element.UMAraceName = element.UMA.name;				
				_DK_UMA_GameSettings._GameLibraries.DkRacesLibrary.Add ( element );
			}	
		}

		// for DK Races
		else if ( type.Name == "UMA.RaceData" || type.Name == "RaceData" ){
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				UMA.RaceData element = (UMA.RaceData)AssetDatabase.LoadAssetAtPath(path, typeof(UMA.RaceData));

				_DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Add ( element );
			}	
		}

		// for EquipmentSets
		else if ( type.Name == "DKEquipmentSetData" ){
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				//	Debug.Log (path);
				DKEquipmentSetData element = (DKEquipmentSetData)AssetDatabase.LoadAssetAtPath(path, typeof(DKEquipmentSetData));

				_DK_UMA_GameSettings.EquipmentSets.EquipmentSetsList.Add ( element );
			}
		}

		// for DKSlotData elements
		else if ( type.Name == "DKSlotData" ){
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			//	Debug.Log (path);
				DKSlotData element = (DKSlotData)AssetDatabase.LoadAssetAtPath(path, typeof(DKSlotData));
				if ( element.name.Contains("DefaultDK") == false 
					&& element.name.Contains("DefaultSlotType") == false
					&& element.name.Contains("DefaultOverlayType") == false
					&& element.name.Contains("DefaultUMA") == false
					&& element.name.Contains("CapsuleCollider") == false
					&& element.name.Contains("ExpressionSlot") == false
					&& element.name.Contains("ForearmTwist") == false
					&& element.name.Contains("Locomotion") == false ){
					DKSlotsList.Add ( element );
					_DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary.Add ( element );
					if ( element._UMA != null ){
						element._UMAslotName = element._UMA.name;
						LinkedUMASlotData newData = new LinkedUMASlotData ();
						newData.Name = element.name;
						newData.DKSlot = element;
						newData.UMASlot = element._UMA;
						newData.index = LinkedUMASlotsList.Count-1;
						LinkedUMASlotDatasList.Add (newData);
						LinkedUMASlotsList.Add(element._UMA);
					}
				//	DKSlotsNamesList.Add (element.name);
				}
			}
	#if UNITY_EDITOR
			AssetDatabase.SaveAssets ();
	#endif			
		}

		// for DKOverlayData elements
		else if ( type.Name == "DKOverlayData" ){
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			//	Debug.Log (path);
				DKOverlayData element = (DKOverlayData)AssetDatabase.LoadAssetAtPath(path, typeof(DKOverlayData));
				if ( element.name.Contains("DefaultDK") == false 
					&& element.name.Contains("DefaultSlotType") == false
					&& element.name.Contains("DefaultOverlayType") == false
					&& element.name.Contains("DefaultUMA") == false
					&& element.name.Contains("CapsuleCollider") == false
					&& element.name.Contains("ExpressionSlot") == false
					&& element.name.Contains("ForearmTwist") == false
					&& element.name.Contains("Locomotion") == false ){
					if ( element._UMA != null )
						element._UMAoverlayName = element._UMA.name;
					DKOverlaysList.Add ( element );
					_DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary.Add ( element );
					DKOverlaysNamesList.Add (element.name);
				}
			}
		}

		// for UMA.OverlayDataAsset elements
		else if ( type.Name == "UMA.OverlayDataAsset" || type.Name == "OverlayDataAsset" ){
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			//	Debug.Log (path);
				UMA.OverlayDataAsset element = (UMA.OverlayDataAsset)AssetDatabase.LoadAssetAtPath(path, typeof(UMA.OverlayDataAsset));
				if ( element.name.Contains("DefaultDK") == false 
					&& element.name.Contains("DefaultSlotType") == false
					&& element.name.Contains("DefaultOverlayType") == false
					&& element.name.Contains("DefaultUMA") == false
					&& element.name.Contains("CapsuleCollider") == false
					&& element.name.Contains("ExpressionSlot") == false
					&& element.name.Contains("ForearmTwist") == false
					&& element.name.Contains("Locomotion") == false ){
					UMAOverlaysList.Add ( element );
				}
			}
		}

		// for UMA.SlotDataAsset elements
		else if ( type.Name == "UMA.SlotDataAsset" || type.Name == "SlotDataAsset" ){
			foreach (string guid in guids2) {
				string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			//	Debug.Log (path);
				UMA.SlotDataAsset element = (UMA.SlotDataAsset)AssetDatabase.LoadAssetAtPath(path, typeof(UMA.SlotDataAsset));
				if ( element.name.Contains("DefaultDK") == false 
					&& element.name.Contains("DefaultSlotType") == false
					&& element.name.Contains("DefaultOverlayType") == false
					&& element.name.Contains("DefaultUMA") == false
					&& element.name.Contains("CapsuleCollider") == false
					&& element.name.Contains("ExpressionSlot") == false
					&& element.name.Contains("ForearmTwist") == false
					&& element.name.Contains("Locomotion") == false ){
					UMASlotsList.Add ( element );
				}
			}
		}
	}

	#endif
	#endregion Search Elements

	public void CleanLibraries (){
		CleanDKLibraries ();
		CleanUMALibraries ();
	}

	public void CleanDKLibraries (){
		List<DKSlotData> tmpList = new List<DKSlotData>();
		List<DKSlotData> RemoveList = new List<DKSlotData>();
		List<DKOverlayData> tmpList2 = new List<DKOverlayData>();
		List<DKOverlayData> removeList2 = new List<DKOverlayData>();
		List<DKRaceData> tmpRaceList2 = new List<DKRaceData>();
		List<DKRaceData> removeRaceList2 = new List<DKRaceData>();

		if ( _DK_UMACrowd == null )
		try{
			_DK_UMACrowd = GameObject.Find ("DKUMACrowd").GetComponent<DK_UMACrowd>();
		}catch(NullReferenceException){ Debug.LogError ( "DK UMA is not installed in your scene, please install DK UMA." );}

		tmpList = _DK_UMACrowd.slotLibrary.slotElementList.ToList();
		for(int i = 0; i < tmpList.Count; i ++){
			if ( tmpList[i] == null ) RemoveList.Add(tmpList[i]);
		}
		
		tmpList2 = _DK_UMACrowd.overlayLibrary.overlayElementList.ToList();
		for(int i = 0; i < tmpList2.Count; i ++){
			if ( tmpList2[i] == null ) removeList2.Add(tmpList2[i]);
		}
		foreach ( DKSlotData slot in RemoveList ){
			if ( tmpList.Contains (slot) ) tmpList.Remove (slot);
		}
		foreach ( DKOverlayData overlay in removeList2 ){
			if ( tmpList2.Contains (overlay) ) tmpList2.Remove (overlay);
		}

		tmpRaceList2 = _DK_UMACrowd.raceLibrary.raceElementList.ToList();
		for(int i = 0; i < tmpRaceList2.Count; i ++){
			if ( tmpRaceList2[i] == null ) removeRaceList2.Add(tmpRaceList2[i]);
		}

		foreach ( DKRaceData race in removeRaceList2 ){
			if ( tmpRaceList2.Contains (race) ) tmpRaceList2.Remove (race);
		}

		_DK_UMACrowd.slotLibrary.slotElementList = tmpList.ToArray();
		_DK_UMACrowd.overlayLibrary.overlayElementList = tmpList2.ToArray();
		_DK_UMACrowd.raceLibrary.raceElementList = tmpRaceList2.ToArray();
	}
	#region UMA

	public void CleanUMALibraries (){
		CleanUMASlotsLibrary ();
		CleanUMAOverlaysLibrary ();
		CleanUMARacesLibrary ();
	}

	public void CleanUMASlotsLibrary (){
		if ( GameObject.Find ("UMA") != null ){
			UMAContext context = FindObjectOfType<UMAContext>();
			GameObject obj;

			List<SlotDataAsset> tmpSlotUMAList = new List<SlotDataAsset>();
			List<SlotDataAsset> RemoveSlotUMAList = new List<SlotDataAsset>();
			tmpSlotUMAList = FindObjectOfType<SlotLibrary>().GetAllSlotAssets ().ToList ();
			for(int i = 0; i < tmpSlotUMAList.Count; i ++){
				if ( tmpSlotUMAList[i] == null ) RemoveSlotUMAList.Add(tmpSlotUMAList[i]);
			}
			if ( RemoveSlotUMAList.Count > 0 ){
				foreach ( SlotDataAsset slot in RemoveSlotUMAList ){
					if ( tmpSlotUMAList.Contains (slot) ) tmpSlotUMAList.Remove (slot);
				}
				obj = FindObjectOfType<SlotLibrary>().gameObject;
				obj.gameObject.RemoveComponent(typeof (SlotLibrary));
				SlotLibrary lib = obj.gameObject.AddComponent<SlotLibrary>();
				foreach ( SlotDataAsset asset in tmpSlotUMAList ){
					lib.AddSlotAsset (asset);
				}
				context.slotLibrary = lib;
			}
		}
	}

	public void CleanUMAOverlaysLibrary (){
		if ( GameObject.Find ("UMA") != null ){
			UMAContext context = FindObjectOfType<UMAContext>();
			GameObject obj;

			List<OverlayDataAsset> tmpOvUMAList2 = new List<OverlayDataAsset>();
			List<OverlayDataAsset> removeOvUMAList2 = new List<OverlayDataAsset>();
			tmpOvUMAList2 = FindObjectOfType<OverlayLibrary>().GetAllOverlayAssets ().ToList ();
			for(int i = 0; i < tmpOvUMAList2.Count; i ++){
				if ( tmpOvUMAList2[i] == null ) removeOvUMAList2.Add(tmpOvUMAList2[i]);
			}
			if ( removeOvUMAList2.Count > 0 ){
				foreach ( OverlayDataAsset overlay in removeOvUMAList2 ){
					if ( tmpOvUMAList2.Contains (overlay) ) tmpOvUMAList2.Remove (overlay);
				}
				obj = FindObjectOfType<OverlayLibrary>().gameObject;
				obj.gameObject.RemoveComponent(typeof (OverlayLibrary));
				OverlayLibrary sllib = obj.gameObject.AddComponent<OverlayLibrary>();
				foreach ( OverlayDataAsset asset in tmpOvUMAList2 ){
					sllib.AddOverlayAsset (asset);
				}
				context.overlayLibrary = sllib;
			}
		}
	}

	public void CleanUMARacesLibrary (){
		if ( GameObject.Find ("UMA") != null ){
			UMAContext context = FindObjectOfType<UMAContext>();
			GameObject obj;

			List<RaceData> tmpRaceUMAList2 = new List<RaceData>();
			List<RaceData> removeRaceUMAList2 = new List<RaceData>();
			tmpRaceUMAList2 = FindObjectOfType<RaceLibrary>().GetAllRaces ().ToList ();
			for(int i = 0; i < tmpRaceUMAList2.Count; i ++){
				if ( tmpRaceUMAList2[i] == null ) removeRaceUMAList2.Add(tmpRaceUMAList2[i]);
			}
			if ( removeRaceUMAList2.Count > 0 ){
				foreach ( RaceData race in removeRaceUMAList2 ){
					if ( tmpRaceUMAList2.Contains (race) ) tmpRaceUMAList2.Remove (race);
				}
				obj = FindObjectOfType<RaceLibrary>().gameObject;
				obj.gameObject.RemoveComponent(typeof (RaceLibrary));
				RaceLibrary ralib = obj.gameObject.AddComponent<RaceLibrary>();
				foreach ( RaceData asset in tmpRaceUMAList2 ){
					ralib.AddRace (asset);
				}
				context.raceLibrary = ralib;
			}
		}
	}
	#endregion UMA
}
