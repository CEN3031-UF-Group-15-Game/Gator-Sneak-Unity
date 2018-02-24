
using System.Linq;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using UMA;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class TransposeDK2UMA : MonoBehaviour {

	// UMA Variables
	[HideInInspector]public UMA.UMAData umaData;
	[HideInInspector]public DKUMAData DKumaData;
	[HideInInspector]public UMA.RaceData _RaceData;

//	UMACrowd _UMACrowd;
	UMA.UMAGeneratorBase generator;
	Transform tempUMA;

	[HideInInspector] public bool UpdateOnly = false;
	public bool HideAtStart = false;
	[HideInInspector] public bool UpdateDNA = false;

	SlotLibrary slotLibrary;
	OverlayLibrary overlayLibrary;
	RaceLibrary raceLibrary;
	[HideInInspector] public GameObject DefaultUMA;

	[HideInInspector] public string _StreamedUMA;
	public LoadUMA _LoadUMA;
//	UMADynamicAvatar umaDynamicAvatar;

	// DK UMA Variables
	DK_UMACrowd _DK_UMACrowd;
//	DKUMAData _DKUMAData;

	// Avatar Variables
	DK_RPG_UMA _DK_RPG_UMA;
	string Gender;

	DK_UMA_GameSettings _DK_UMA_GameSettings;

	DKRaceLibrary DKRacelib;
	List<DKRaceData> raceList = new List<DKRaceData>();

//	DKUMA_Variables _DKUMA_Variables;
//	DK_UMA_GameSettings _DK_UMA_GameSettings;

/*	public void Launch (DK_RPG_UMA DKRPGUMA, DK_UMACrowd DKUMACrowd, string streamed, bool updateOnly, 
		bool _MeshDirty, bool _ShapeDirty, bool _TextureDirty, bool _AtlasDirty ){
		MeshDirty = _MeshDirty;
		ShapeDirty = _ShapeDirty;
		TextureDirty = _TextureDirty;
		AtlasDirty = _AtlasDirty;

		Launch (DKRPGUMA, DKUMACrowd, streamed, updateOnly );
	}*/

	public void Launch (DK_RPG_UMA DKRPGUMA, DK_UMACrowd DKUMACrowd, string streamed, bool updateOnly ){
		UpdateOnly = updateOnly;

		if ( _DK_UMA_GameSettings == null ) _DK_UMA_GameSettings = FindObjectOfType<DKUMA_Variables>()._DK_UMA_GameSettings;
		if ( _DK_UMA_GameSettings.Debugger ) Debug.Log ( "DK UMA : Launching Transpose");

		gameObject.SetActive (true);

		// verify if the UMA race is in the DK race library
		DKRacelib = FindObjectOfType<DKRaceLibrary>();
		raceList = DKRacelib.raceElementList.ToList();
			
		if ( DKRPGUMA.RaceData != null && DKRacelib.raceElementList.Contains(DKRPGUMA.RaceData) == false ){
			DKRacelib.AddRace (DKRPGUMA.RaceData);
		}

		// verify if the UMA race is in the UMA race library
		#if UMADCS
		raceLibrary = FindObjectOfType<DynamicRaceLibrary>();
		if ( DKRPGUMA.RaceData.UMA != null && lib.GetAllRaces().ToList().Contains(DKRPGUMA.RaceData.UMA) == false ){
			lib.AddRace (DKRPGUMA.RaceData.UMA);
		}
		#else
		raceLibrary = FindObjectOfType<RaceLibrary>();
		if ( DKRPGUMA.RaceData.UMA != null && raceLibrary.GetAllRaces().ToList().Contains(DKRPGUMA.RaceData.UMA) == false ){
			raceLibrary.AddRace (DKRPGUMA.RaceData.UMA);
		}
		#endif

		// find generator
		UMAGenerator _UMAGenerator = FindObjectOfType<UMAGenerator>();

		// verify the Mesh Combiner
		if ( _UMAGenerator.meshCombiner == null ){
			GameObject ObjmeshCombiner = GameObject.Find ("UMADefaultMeshCombiner");
			_UMAGenerator.meshCombiner = ObjmeshCombiner.transform.GetComponent<UMADefaultMeshCombiner>();

			if ( _UMAGenerator.meshCombiner == null ){
				_UMAGenerator.meshCombiner = ObjmeshCombiner.AddComponent<UMADefaultMeshCombiner>();
				Debug.Log ( "meshCombiner is missing : adding UMADefaultMeshCombiner");
			}
			else Debug.Log ( "meshCombiner is found");
		}

		// assign scripts
		_DK_UMACrowd = DKUMACrowd;
		_DK_RPG_UMA = DKRPGUMA;
		Gender = DKRPGUMA.Gender;

		// get libraries
		try {
			#if UMADCS
			slotLibrary = FindObjectOfType<DynamicSlotLibrary>();
			overlayLibrary = FindObjectOfType<DynamicOverlayLibrary>();
			#else
			slotLibrary = FindObjectOfType<SlotLibrary>();
			overlayLibrary = FindObjectOfType<OverlayLibrary>();
			#endif
		}catch ( NullReferenceException ) { 
			Debug.LogError ( "UMA_DCS is missing from your scene. UMA is required to generate a UMA avatar." ); 
		}
		// assign recipe
		_StreamedUMA = streamed;
		_DK_RPG_UMA.DkUmaStreaming = streamed;

		ConvertStreamedUMA (DKumaData.streamedUMA);

		if ( UpdateOnly ) {
	//		Debug.Log ( "DK UMA : Updating UMA avatar." );
			UpdateUMAData ();
			AssignRecipe ();
				
		#if UNITY_EDITOR
			SendRecipe ( _StreamedUMA );
		#endif
			SendDNAToUMA ();
		
		}
		else {
			CreateUMAData ();
			AssignRecipe ();
		#if UNITY_EDITOR
			SendRecipe ( _StreamedUMA );
		#endif
			SendDNAToUMA ();
		}

		#if UNITY_EDITOR
		if ( EditorApplication.isPlaying == false ){
			if ( transform.GetComponentInChildren<PreviewAvatar>() == false ) 
				GetComponentInChildren<LoadUMA>().gameObject.AddComponent<PreviewAvatar>();
			DestroyImmediate ( transform.GetComponentInChildren<PreviewAvatar>().NewPreview );
			transform.GetComponentInChildren<PreviewAvatar>().GeneratePreview ();
		}
		else {
			
		}
		#endif
	}

	void UpdateUMAData (){
		_DK_RPG_UMA = GetComponent<DK_RPG_UMA>();
	//	if ( _DK_RPG_UMA._UMA_Avatar == null )
		_DK_RPG_UMA._UMA_Avatar = _DK_RPG_UMA.GetComponentInChildren<UMAData>();
		if ( umaData == null )
		umaData = _DK_RPG_UMA._UMA_Avatar;
		if ( DefaultUMA == null )
			DefaultUMA = _DK_RPG_UMA._UMA_Avatar.gameObject;
		if ( DefaultUMA == null )
			DefaultUMA = _DK_RPG_UMA._UMA_Avatar.gameObject.GetComponentInChildren(typeof(UMAData)).gameObject;

	//	AssignRecipe ();
	}

	void SendRaceToUMA (){
		// assign the default human races if the UMA variable of the DK Race is blank
		if ( _DK_RPG_UMA.RaceData.UMA == null ){
			if ( Gender == "Male" ) _RaceData = raceLibrary.GetRace ("HumanMale");
			else if ( Gender == "Female" ) _RaceData = raceLibrary.GetRace ("HumanFemale");
		}
		// if the UMA variable of the DK Race is not blank, assign the correct race 
		else {
			_RaceData = raceLibrary.GetRace (_DK_RPG_UMA.RaceData.UMA.raceName);
		}
		Debug.Log ( _RaceData.raceName );
		DefaultUMA.GetComponent<UMA.UMAData>().umaRecipe.SetRace(_RaceData); 
	}

	void SendSlotsToUMA (){
		Debug.Log ( "Sending slots to UMA" );
		List<SlotData> tmpSlotList = new List<SlotData>();

		for (int i = 0; i <  _DK_UMACrowd.tempSlotList.Count; i ++) {
			DKSlotData dkSlot = _DK_UMACrowd.tempSlotList[i];
			if ( dkSlot._UMA != null ){
				// find overlays
				List<OverlayData> tmpOverlayList = new List<OverlayData>();
				for (int i1 = 0; i1 <  _DK_UMACrowd.tempSlotList[i].overlayList.Count; i1 ++) {
					DKOverlayData dkOverlay = _DK_UMACrowd.tempSlotList[i].overlayList[i1];
					if ( dkOverlay._UMA != null ){
						tmpOverlayList.Add ( overlayLibrary.InstantiateOverlay ( dkOverlay._UMA.overlayName, dkOverlay.color ) );
					}
					else Debug.LogError ( "Warning : The DKSlot '"+dkSlot.slotName+"' has the DKOverlay '"
					                     +dkOverlay.overlayName+"' and the UMA Link is missing from it. " +
					                     	"Fix it by selecting the concerned DKOverlay and adding the UMA Link.");
				}

				// add the UMA slot when its overlay tmplist
				tmpSlotList.Add (slotLibrary.InstantiateSlot( dkSlot._UMA.slotName, tmpOverlayList ) );

			}
			else Debug.LogError ( "Warning : The DKSlot '"+dkSlot.slotName+"' has a missing UMA Link for it. " +
				"Fix it by selecting the concerned DKSlot and add the UMA Link.");
		}
		// transfert to the UMAData
		DefaultUMA.GetComponent<UMA.UMAData>().umaRecipe.slotDataList = tmpSlotList.ToArray();
	}

	protected virtual void GenerateUMAShapes()
	{
	}

	void ConvertStreamedUMA (string streamed){
		_StreamedUMA = streamed;
		// Translate compressions
		_StreamedUMA = _StreamedUMA.Replace(@"sID","slotID");
		_StreamedUMA = _StreamedUMA.Replace ( @"oS" , "overlayScale" );
		_StreamedUMA = _StreamedUMA.Replace ( @"cOI" , "copyOverlayIndex" );
		_StreamedUMA = _StreamedUMA.Replace ( @"ODL" , "OverlayDataList" );
		_StreamedUMA = _StreamedUMA.Replace ( @"oID" , "overlayID" );
		_StreamedUMA = _StreamedUMA.Replace ( @"rL" , "rectList" );
		
		_StreamedUMA = _StreamedUMA.Replace ( @"cL" , "colorList" );
		_StreamedUMA = _StreamedUMA.Replace ( @"cML" , "channelMaskList" );
		_StreamedUMA = _StreamedUMA.Replace ( @"cAML" , "channelAdditiveMaskList" );
		_StreamedUMA = _StreamedUMA.Replace ( @"DKUMADnaHumanoid" , "UMADnaHumanoid" );
	
		// assign the default human races if the UMA variable of the DK Race is blank
		if ( DKRacelib == null ) DKRacelib = FindObjectOfType<DKRaceLibrary>();
		bool mofidiedDKRaceLib = false;
		if ( _DK_RPG_UMA.RaceData.UMA == null ){
			if ( transform.gameObject.GetComponent<DK_RPG_UMA>().Gender == "Female") {
				
				foreach ( DKRaceData race in DKRacelib.raceElementList ){
					// verify DK race lib
					if ( race == null ){
						mofidiedDKRaceLib = true;
					}
					else _StreamedUMA = _StreamedUMA.Replace ( @""+race.raceName+"" , "HumanFemale" );
				}
			}
			else {
				foreach ( DKRaceData race in DKRacelib.raceElementList )
					if ( race == null ){
						mofidiedDKRaceLib = true;
					}
					else {
						raceList.Add (race);
						_StreamedUMA = _StreamedUMA.Replace ( @""+race.raceName+"" , "HumanMale" );	
					}
			}
		}
		// if the UMA variable of the DK Race is not blank, assign the correct race
		else {
			foreach ( DKRaceData race in DKRacelib.raceElementList )
				if ( race == null ){
					mofidiedDKRaceLib = true;
				}
				else {					
					raceList.Add (race);
					
					if ( race.UMA != null && _StreamedUMA.Contains(race.UMA.raceName) == true )
						_StreamedUMA = _StreamedUMA.Replace ( @""+race.raceName+"" , race.UMA.raceName );	
				}
		}
		if ( mofidiedDKRaceLib ) {
			DKRacelib.raceElementList = raceList.ToArray ();
		}

		// Modify DNA names
		_StreamedUMA = _StreamedUMA.Replace ( @"N0\" , _DK_RPG_UMA.RaceData.DNAConverterDataList[0].Name+"\\" );
		_StreamedUMA = _StreamedUMA.Replace ( @"N10" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[12].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N11" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[11].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N12" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[12].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N13" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[13].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N14" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[14].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N15" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[15].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N16" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[16].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N17" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[17].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N18" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[18].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N19" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[19].Name );

		_StreamedUMA = _StreamedUMA.Replace ( @"N1\" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[1].Name+"\\" );

		_StreamedUMA = _StreamedUMA.Replace ( @"N20" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[20].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N21" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[21].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N22" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[22].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N23" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[23].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N24" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[24].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N25" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[25].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N26" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[26].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N27" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[27].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N28" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[28].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N29" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[29].Name );

		_StreamedUMA = _StreamedUMA.Replace ( @"N2\" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[2].Name+"\\" );

		_StreamedUMA = _StreamedUMA.Replace ( @"N30" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[30].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N31" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[31].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N32" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[32].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N33" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[33].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N34" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[34].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N35" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[35].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N36" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[36].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N37" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[37].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N38" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[38].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N39" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[39].Name );

		_StreamedUMA = _StreamedUMA.Replace ( @"N3\" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[3].Name+"\\" );

		_StreamedUMA = _StreamedUMA.Replace ( @"N40" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[40].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N41" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[41].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N42" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[42].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N43" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[43].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N44" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[44].Name );
		_StreamedUMA = _StreamedUMA.Replace ( @"N45" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[45].Name );			

		_StreamedUMA = _StreamedUMA.Replace ( @"N4\" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[4].Name+"\\" );
		_StreamedUMA = _StreamedUMA.Replace ( @"N5\" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[5].Name+"\\" );
		_StreamedUMA = _StreamedUMA.Replace ( @"N6\" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[6].Name+"\\" );
		_StreamedUMA = _StreamedUMA.Replace ( @"N7\" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[7].Name+"\\" );
		_StreamedUMA = _StreamedUMA.Replace ( @"N8\" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[8].Name+"\\" );
		_StreamedUMA = _StreamedUMA.Replace ( @"N9\" ,  _DK_RPG_UMA.RaceData.DNAConverterDataList[9].Name+"\\" );

		//		_StreamedUMA = _StreamedUMA.Replace ( "N46" , "" );
		//		_StreamedUMA = _StreamedUMA.Replace ( "N47" , "" );
		//		_StreamedUMA = _StreamedUMA.Replace ( "N48" , "" );
		//		_StreamedUMA = _StreamedUMA.Replace ( "N49" , "" );

		// Modify names for UMADnaHumanoid
	//	if ( _StreamedUMA.Contains("UMADnaHumanoid") == true ) {

			_StreamedUMA = _StreamedUMA.Replace ( @"Height" ,  "height" );
			_StreamedUMA = _StreamedUMA.Replace ( @"HeadSize" ,  "headSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"HeadWidth" ,  "headWidth" );
			_StreamedUMA = _StreamedUMA.Replace ( @"NeckThickness" ,  "neckThickness" );
			_StreamedUMA = _StreamedUMA.Replace ( @"ArmLength" ,  "armLength" );
			_StreamedUMA = _StreamedUMA.Replace ( @"ForearmLength" ,  "forearmLength" );
			_StreamedUMA = _StreamedUMA.Replace ( @"ArmWidth" ,  "armWidth" );
			_StreamedUMA = _StreamedUMA.Replace ( @"ForearmWidth" ,  "forearmWidth" );

			_StreamedUMA = _StreamedUMA.Replace ( @"HandsSize" ,  "handsSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"FeetSize" ,  "feetSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"LegSeparation" ,  "legSeparation" );
			_StreamedUMA = _StreamedUMA.Replace ( @"UpperMuscle" ,  "upperMuscle" );
			_StreamedUMA = _StreamedUMA.Replace ( @"LowerMuscle" ,  "lowerMuscle" );
			_StreamedUMA = _StreamedUMA.Replace ( @"UpperWeight" ,  "upperWeight" );
			_StreamedUMA = _StreamedUMA.Replace ( @"LowerWeight" ,  "lowerWeight" );
			_StreamedUMA = _StreamedUMA.Replace ( @"LegsSize" ,  "legsSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"Belly" ,  "belly" );
			_StreamedUMA = _StreamedUMA.Replace ( @"Waist" ,  "waist" );
			_StreamedUMA = _StreamedUMA.Replace ( @"GluteusSize" ,  "gluteusSize" );

			_StreamedUMA = _StreamedUMA.Replace ( @"EarsSize" ,  "earsSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"EarsPosition" ,  "earsPosition" );
			_StreamedUMA = _StreamedUMA.Replace ( @"EarsRotation" ,  "earsRotation" );
			_StreamedUMA = _StreamedUMA.Replace ( @"NoseSize" ,  "noseSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"NoseCurve" ,  "noseCurve" );
			_StreamedUMA = _StreamedUMA.Replace ( @"NoseWidth" ,  "noseWidth" );
			_StreamedUMA = _StreamedUMA.Replace ( @"NoseInclination" ,  "noseInclination" );
			_StreamedUMA = _StreamedUMA.Replace ( @"NosePosition" ,  "nosePosition" );
			_StreamedUMA = _StreamedUMA.Replace ( @"NosePronounced" ,  "nosePronounced" );
			_StreamedUMA = _StreamedUMA.Replace ( @"NoseFlatten" ,  "noseFlatten" );

			_StreamedUMA = _StreamedUMA.Replace ( @"ChinSize" ,  "chinSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"ChinPronounced" ,  "chinPronounced" );
			_StreamedUMA = _StreamedUMA.Replace ( @"ChinPosition" ,  "chinPosition" );

			_StreamedUMA = _StreamedUMA.Replace ( @"MandibleSize" ,  "mandibleSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"JawsSize" ,  "jawsSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"JawsPosition" ,  "jawsPosition" );

			_StreamedUMA = _StreamedUMA.Replace ( @"CheekSize" ,  "cheekSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"CheekPosition" ,  "cheekPosition" );
			_StreamedUMA = _StreamedUMA.Replace ( @"LowCheekPronounced" ,  "lowCheekPronounced" );
			_StreamedUMA = _StreamedUMA.Replace ( @"LowCheekPosition" ,  "lowCheekPosition" );

			_StreamedUMA = _StreamedUMA.Replace ( @"ForeheadSize" ,  "foreheadSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"ForeheadPosition" ,  "foreheadPosition" );

			_StreamedUMA = _StreamedUMA.Replace ( @"LipsSize" ,  "lipsSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"MouthSize" ,  "mouthSize" );
			_StreamedUMA = _StreamedUMA.Replace ( @"EyeRotation" ,  "eyeRotation" );
			_StreamedUMA = _StreamedUMA.Replace ( @"EyeSize" ,  "eyeSize" );

			_StreamedUMA = _StreamedUMA.Replace ( @"BreastSize" ,  "height" );

			_StreamedUMA = _StreamedUMA.Replace ( @"EyeSpacing" ,  "eyeSpacing" );					
	//	}

	//	Debug.Log (_StreamedUMA);
	//	if ( _DK_RPG_UMA.IsPlayer )
			
		DK_UMA_Save.SaveDNA ( _DK_RPG_UMA );

	//	System.IO.File.WriteAllText("d:/yourtextfile.txt", "This is text that goes into the text file");
	}	

	void CreateUMAData (){
		DefaultUMA = (GameObject) GameObject.Instantiate(Resources.Load("DefaultUMABase") as GameObject);
		GameObject ZeroPoint = GameObject.Find("ZeroPoint");
		if ( ZeroPoint == null ){
			ZeroPoint = (GameObject)Instantiate (Resources.Load ("ZeroPoint"));
			ZeroPoint.name = "ZeroPoint";
		}
		transform.position =  ZeroPoint.transform.position;
		DefaultUMA.transform.parent = transform;

		// set animator
		if ( _DK_UMACrowd.AnimatorController != null )
			DefaultUMA.GetComponentInChildren<UMADynamicAvatar>().animationController = _DK_UMACrowd.AnimatorController;

		if ( _DK_UMACrowd.zeroPoint) {
			DefaultUMA.transform.position = _DK_UMACrowd.zeroPoint.transform.position;
			DefaultUMA.transform.localPosition = new Vector3(0, 0, 0);
		}
		else {
			_DK_UMACrowd.zeroPoint = GameObject.Find ("ZeroPoint").transform;
			DefaultUMA.transform.position = _DK_UMACrowd.zeroPoint.transform.position;
			DefaultUMA.transform.localPosition = new Vector3(0, 0, 0);
		}	

		if ( DefaultUMA.GetComponent<UMADynamicAvatar>().context == null ) 
			DefaultUMA.GetComponent<UMADynamicAvatar>().context = GameObject.Find ("UMAContext").GetComponent<UMAContext>();

		// assign correct atlas resolution for the player avatar to the generator
		if ( _DK_RPG_UMA.IsPlayer ){
			DefaultUMA.GetComponent<UMADynamicAvatar>().umaGenerator = GameObject.Find ("DKUMAGenerator").GetComponent<UMAGeneratorBase>();
			DefaultUMA.GetComponent<UMADynamicAvatar>().umaGenerator.atlasResolution = _DK_RPG_UMA._LOD.LOD0Resolution;
			Debug.Log ("DK UMA : Player's Generator assigned : "+DefaultUMA.GetComponent<UMADynamicAvatar>().umaGenerator.transform/*.parent*/.name);

		}

		_DK_RPG_UMA._UMA_Avatar = umaData;
		if ( _DK_RPG_UMA._UMA_Avatar == null ) {
			_DK_RPG_UMA._UMA_Avatar = DefaultUMA.GetComponent<UMA.UMAData>();
		}
	
		// add the race to the UMA library if necessary
		if ( _DK_RPG_UMA.RaceData.UMA != null && raceLibrary.GetRace(_DK_RPG_UMA.RaceData.UMA.raceName) == null )
			raceLibrary.AddRace (_DK_RPG_UMA.RaceData.UMA);

		// assign the default human races if the UMA variable of the DK Race is blank
		if ( _DK_RPG_UMA.RaceData.UMA == null ){
			if ( _DK_RPG_UMA._UMA_Avatar && _DK_RPG_UMA.Gender == "Male" ){
				_DK_RPG_UMA._UMA_Avatar.umaRecipe.SetRace(raceLibrary.GetRace("HumanMale"));
			}
			else if ( _DK_RPG_UMA._UMA_Avatar && _DK_RPG_UMA.Gender == "Female" ){
				_DK_RPG_UMA._UMA_Avatar.umaRecipe.SetRace(raceLibrary.GetRace("HumanFemale"));
			}
		}
		// if the UMA variable of the DK Race is not blank, assign the correct race 
		else if ( _DK_RPG_UMA._UMA_Avatar != null ){			
			_DK_RPG_UMA._UMA_Avatar.umaRecipe.SetRace(raceLibrary.GetRace(_DK_RPG_UMA.RaceData.UMA.raceName));
		}

	/*	if ( _UMACrowd.animationController != null)
		{
			umaDynamicAvatar.animationController = _UMACrowd.animationController;
		}
		umaDynamicAvatar.UpdateNewRace();
		umaDynamicAvatar.umaData.myRenderer.enabled = false;
		tempUMA = newGO.transform;
		
		if (_DK_UMACrowd.zeroPoint)
		{
			tempUMA.position = new Vector3(_DK_UMACrowd.zeroPoint.position.x, _DK_UMACrowd.zeroPoint.position.y, _DK_UMACrowd.zeroPoint.position.z);
		}
		else
		{
			tempUMA.position = new Vector3(0, 0, 0);
		}
*/

	//	AssignRecipe ();
	}

	#region From original UMA

	void myColliderUpdateMethod(UMA.UMAData umaData)
	{
		CapsuleCollider tempCollider = umaData.umaRoot.gameObject.GetComponent("CapsuleCollider") as CapsuleCollider;
		if (tempCollider == null) tempCollider = umaData.umaRoot.gameObject.AddComponent<CapsuleCollider>();
		if (tempCollider)
		{
			UMA.UMADnaHumanoid umaDna = umaData.umaRecipe.GetDna<UMA.UMADnaHumanoid>();
			tempCollider.height = (umaDna.height + 0.5f) * 2 + 0.1f;
			tempCollider.center = new Vector3(0, tempCollider.height * 0.5f - 0.04f, 0);
		}
	}
	#endregion From original UMA

	
	void AssignRecipe (){
		// Prepare Packed DNA
		SaveDNAToMemoryStream();
		_LoadUMA = transform.GetComponentInChildren <LoadUMA>();
		// Add the new auto loader for the avatar. May be obsolete
		if ( _LoadUMA == null )
			_LoadUMA = DefaultUMA.AddComponent <LoadUMA>();
		_LoadUMA.HideAtStart = HideAtStart;
		_LoadUMA._StreamedUMA = _StreamedUMA;
	}

	public void SendRecipe ( string _StreamedUMA ){
		var asset = ScriptableObject.CreateInstance<UMATextRecipe>();
		asset.recipeString = _StreamedUMA;
		_LoadUMA._StreamedUMA = _StreamedUMA;
		transform.GetComponentInChildren<UMADynamicAvatar>().umaRecipe = asset;
		transform.GetComponentInChildren<UMADynamicAvatar>().umaRecipe.name = "Avatar";
	}

	public virtual void SaveDNAToMemoryStream() {	
		// DNA
		DK_RPG_UMA.DKUMAPackedDNA PackedDNA = new DK_RPG_UMA.DKUMAPackedDNA ();
		PackedDNA.packedDna.Clear();

		foreach(var dna in DKumaData.DNAList2 )
		{
			DK_RPG_UMA.DKUMAPackedDna packedDna = new DK_RPG_UMA.DKUMAPackedDna();

			packedDna.N = dna.Name;
			packedDna.V = dna.Value;

			PackedDNA.packedDna.Add(packedDna);
		}
	//	Byte[] V =  BitConverter.GetBytes((float)PackedDNA.packedDna);

	//	_DK_RPG_UMA.DkUmaDNAStream = JsonMapper.ToJson(V);
	}

	void LateUpdate (){
		if ( UpdateDNA ){
			// Add the new auto loader for the avatar. May be obsolete
			if ( _LoadUMA == null )
				_LoadUMA = DefaultUMA.AddComponent <LoadUMA>();
			_LoadUMA.HideAtStart = HideAtStart;
			_LoadUMA._StreamedUMA = _StreamedUMA;
			SendDNAToUMA ();
			UpdateDNA = false;
		}
	}

	public void SendDNAToUMA (){
		if ( _DK_RPG_UMA == null ) _DK_RPG_UMA = GetComponent<DK_RPG_UMA>();
		if ( DefaultUMA != null ){
			DefaultUMA.transform.parent = _DK_RPG_UMA.transform;
		}

		umaData = GetComponentInChildren<UMAData>();
		UMAAvatarBase avatar = GetComponentInChildren<UMAAvatarBase>();

		if ( DKumaData == null ) DKumaData = this.gameObject.GetComponent<DKUMAData>();
	
		if (avatar.context == null) {
			GameObject tmpObj = GameObject.Find("UMAContext");
			if ( tmpObj ) avatar.context = tmpObj.GetComponent<UMAContext>();
			else Debug.LogError ( "UMA is missing from your scene. UMA is required to generate a UMA avatar." );
		}
		try{
			if (  _LoadUMA.umaDna == null ) _LoadUMA.umaDna = new UMADnaHumanoid();

			if (avatar.context != null) 
				foreach ( DKRaceData.DNAConverterData dkDNA in DKumaData.DNAList2 )  {
					float DnaValue = 0;			
					DnaValue = dkDNA.Value;
				
					if ( DnaValue < 0 ) DnaValue = 0;

					if ( dkDNA.Name.ToLower() == "height" ) _LoadUMA.umaDna.height = DnaValue;
					else if ( dkDNA.Name.ToLower() == "headsize" ) _LoadUMA.umaDna.headSize = DnaValue;				
					else if ( dkDNA.Name.ToLower() == "headwidth" ) _LoadUMA.umaDna.headWidth = DnaValue;
					else if ( dkDNA.Name.ToLower() == "neckthickness" ) _LoadUMA.umaDna.neckThickness = DnaValue;
					else if ( dkDNA.Name.ToLower() == "handssize" ) _LoadUMA.umaDna.handsSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "feetsize" ) _LoadUMA.umaDna.feetSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "legsseparation" ) _LoadUMA.umaDna.legSeparation = DnaValue;
					else if ( dkDNA.Name.ToLower() == "waist" ) _LoadUMA.umaDna.waist = DnaValue;
					else if ( dkDNA.Name.ToLower() == "uppermuscle" ) _LoadUMA.umaDna.upperMuscle = DnaValue;
					else if ( dkDNA.Name.ToLower() == "upperweight" ) _LoadUMA.umaDna.upperWeight = DnaValue;
					else if ( dkDNA.Name.ToLower() == "lowermuscle" ) _LoadUMA.umaDna.lowerMuscle = DnaValue;
					else if ( dkDNA.Name.ToLower() == "lowerweight" ) _LoadUMA.umaDna.lowerWeight = DnaValue;
					else if ( dkDNA.Name.ToLower() == "belly" ) _LoadUMA.umaDna.belly = DnaValue;
					else if ( dkDNA.Name.ToLower() == "legssize" ) _LoadUMA.umaDna.legsSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "gluteussize" ) _LoadUMA.umaDna.gluteusSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "earssize" ) _LoadUMA.umaDna.earsSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "earsposition" ) _LoadUMA.umaDna.earsPosition = DnaValue;
					else if ( dkDNA.Name.ToLower() == "earsrotation" ) _LoadUMA.umaDna.earsRotation = DnaValue;
					else if ( dkDNA.Name.ToLower() == "nosesize" ) _LoadUMA.umaDna.noseSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "nosecurve" ) _LoadUMA.umaDna.noseCurve = DnaValue;
					else if ( dkDNA.Name.ToLower() == "nosewidth" ) _LoadUMA.umaDna.noseWidth = DnaValue;
					else if ( dkDNA.Name.ToLower() == "noseinclination" ) _LoadUMA.umaDna.noseInclination = DnaValue;
					else if ( dkDNA.Name.ToLower() == "noseposition" ) _LoadUMA.umaDna.nosePosition = DnaValue;
					else if ( dkDNA.Name.ToLower() == "nosepronounced" ) _LoadUMA.umaDna.nosePronounced = DnaValue;
					else if ( dkDNA.Name.ToLower() == "noseflatten" ) _LoadUMA.umaDna.noseFlatten = DnaValue;
					else if ( dkDNA.Name.ToLower() == "chinsize" ) _LoadUMA.umaDna.chinSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "chinpronounced" ) _LoadUMA.umaDna.chinPronounced = DnaValue;
					else if ( dkDNA.Name.ToLower() == "chinposition" ) _LoadUMA.umaDna.chinPosition = DnaValue;
					else if ( dkDNA.Name.ToLower() == "mandiblesize" ) _LoadUMA.umaDna.mandibleSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "jawssize" ) _LoadUMA.umaDna.jawsSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "jawsposition" ) _LoadUMA.umaDna.jawsPosition = DnaValue;
					else if ( dkDNA.Name.ToLower() == "cheeksize" ) _LoadUMA.umaDna.cheekSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "cheekposition" ) _LoadUMA.umaDna.cheekPosition = DnaValue;
					else if ( dkDNA.Name.ToLower() == "lowcheekpronounced" ) _LoadUMA.umaDna.lowCheekPronounced = DnaValue;
					else if ( dkDNA.Name.ToLower() == "lowcheekposition" ) _LoadUMA.umaDna.lowCheekPosition = DnaValue;
					else if ( dkDNA.Name.ToLower() == "foreheadsize" ) _LoadUMA.umaDna.foreheadSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "foreheadposition" ) _LoadUMA.umaDna.foreheadPosition = DnaValue;
					else if ( dkDNA.Name.ToLower() == "lipssize" ) _LoadUMA.umaDna.lipsSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "mouthsize" ) _LoadUMA.umaDna.mouthSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "eyesrotation" ) _LoadUMA.umaDna.eyeRotation = DnaValue;
					else if ( dkDNA.Name.ToLower() == "eyessize" ) _LoadUMA.umaDna.eyeSize = DnaValue;
					else if ( dkDNA.Name.ToLower() == "breastsize" ) _LoadUMA.umaDna.breastSize = DnaValue;
					if ( _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.CompleteAvatar 
						|| _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.NoHead ){
						if ( dkDNA.Name.ToLower() == "armlength" ) _LoadUMA.umaDna.armLength = DnaValue;
						else if ( dkDNA.Name.ToLower() == "forearmlength" ) _LoadUMA.umaDna.forearmLength = DnaValue;
						else if ( dkDNA.Name.ToLower() == "armwidth" ) _LoadUMA.umaDna.armWidth = DnaValue;
						else if ( dkDNA.Name.ToLower() == "forearmwidth" ) _LoadUMA.umaDna.forearmWidth = DnaValue;
					}
					else {
						if ( dkDNA.Name.ToLower() == "armlength" ) _LoadUMA.umaDna.armLength = -0.5f;
						else if ( dkDNA.Name.ToLower() == "forearmlength" ) _LoadUMA.umaDna.forearmLength = -0.5f;
						else if ( dkDNA.Name.ToLower() == "armwidth" ) _LoadUMA.umaDna.armWidth = -0.5f;
						else if ( dkDNA.Name.ToLower() == "forearmwidth" ) _LoadUMA.umaDna.forearmWidth = -0.5f;
					}

					// test to delete
					DnaValue = 0.5f;
			}
		}catch(NullReferenceException){}
		SendRPGToUMA();
	}

	void PrepareDNA (){
		
	}

	void SendRPGToUMA (){
		if ( !_DK_RPG_UMA.IsPlayer ) Invoke ( "Finish", 0.1f );
		else Finish ();
	}

	void Finish (){
		DefaultUMA.GetComponent<UMADynamicAvatar>().context = UMAContext.FindInstance();
		if (DefaultUMA.GetComponent<UMADynamicAvatar>().umaAdditionalRecipes == null || DefaultUMA.GetComponent<UMADynamicAvatar>().umaAdditionalRecipes.Length == 0)
		{
			//	DefaultUMA.GetComponent<UMADynamicAvatar>().Load(DefaultUMA.GetComponent<UMADynamicAvatar>().umaRecipe);
		}
		else
		{
			//	DefaultUMA.GetComponent<UMADynamicAvatar>().Load(DefaultUMA.GetComponent<UMADynamicAvatar>().umaRecipe, DefaultUMA.GetComponent<UMADynamicAvatar>().umaAdditionalRecipes);
		}
		//	Debug.Log ( "Convertion Finished");
		//	DestroyImmediate (this);
		// delete the DK Avatar
		//	gameObject.SetActive (false);

		// assign generator
		if (/* _DK_RPG_UMA.IsPlayer == false &&*/ DefaultUMA.GetComponent<UMADynamicAvatar>().umaGenerator == null )
			DefaultUMA.GetComponent<UMADynamicAvatar>().umaGenerator = GameObject.Find ("UMAGenerator").GetComponent<UMAGeneratorBase>();
		

		// assign correct atlas resolution for the player avatar to the generator
		if ( _DK_RPG_UMA.IsPlayer ){
			_DK_RPG_UMA.GetComponentInChildren<UMADynamicAvatar>().umaGenerator.atlasResolution = _DK_RPG_UMA._LOD.LOD0Resolution;
		}	
	
	}
}
