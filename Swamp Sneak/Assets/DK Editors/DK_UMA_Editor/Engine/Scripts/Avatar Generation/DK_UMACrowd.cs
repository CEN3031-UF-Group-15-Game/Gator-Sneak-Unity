using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UMA;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class DK_UMACrowd : MonoBehaviour {	
	#region Variables
	#region UMA
	[HideInInspector] public RuntimeAnimatorController AnimatorController;
	[HideInInspector] public DKOverlayData _FaceOverlay;
	[HideInInspector] public DK_Model _DK_Model;
	[HideInInspector] public DKOverlayData ToDelete;
	[HideInInspector] public DK_GeneratorPresetLibrary ActivePresetLib;
	[HideInInspector] public DK_Race Anato_DK_Race ;
	[HideInInspector] public DKRaceData Race_DK_Race ;
	[HideInInspector] public DKSlotData Slot_DK_Race;
	[HideInInspector] public DKOverlayData Ov_DK_Race;
	[HideInInspector] public DKSlotData CurrentSlot;
	[HideInInspector] public DKSlotData CurrentMeshSlot;
	[HideInInspector] public DKOverlayData FleshOverlay;
	[HideInInspector] public DKUMADnaHumanoid umaDna;
	[HideInInspector] public DKSlotData _DK_Race;
//	DKUMAGenerator DK_DKUMAGenerator ;

	[HideInInspector] public DKUMAData umaData;
	public DKSlotLibrary slotLibrary;
	public DKOverlayLibrary overlayLibrary;
	public DKRaceLibrary raceLibrary;

	#if UMADCS
	[HideInInspector] public DynamicSlotLibrary UslotLibrary;
	[HideInInspector] public DynamicOverlayLibrary UoverlayLibrary;
	#else
//	[HideInInspector] 
	public SlotLibrary UslotLibrary;
//	[HideInInspector] 
	public OverlayLibrary UoverlayLibrary;
	#endif

	[HideInInspector] public bool UpdateProcess;

	[HideInInspector] GameObject DNAConvObj;

	public static bool GenerateWear = true;
	public static bool GenerateHandled = false;

	[System.Serializable]
	public class RaceDefaultData{
		public GameObject MaleRacePrefab;
		public GameObject FemaleRacePrefab;
		public UMA.UmaTPose MaleTPose;
		public UMA.UmaTPose FemaleTPose;
		public DKDnaConverterBehaviour DnaConverterBehaviour;
	}

	[System.Serializable]
	public class ResearchDefaultData{
		public DKSlotData _DKSlotData;
		public UMA.SlotDataAsset _SlotData;
		public DKOverlayData _DKOverlayData;
		public UMA.OverlayDataAsset _OverlayData;
		public DK_ExpressionData DK_ExpressionData;
	}

	[System.Serializable]
	public class DefaultData{
		public ResearchDefaultData ResearchDefault = new ResearchDefaultData ();
		public RaceDefaultData  Race =  new RaceDefaultData();
	}
	public DefaultData Default = new DefaultData();

	[HideInInspector] public static string GeneratorMode = "RPG";
	[HideInInspector] public bool generateUMA;
	[HideInInspector] public bool NewProcess;
	[HideInInspector] public bool generateLotsUMA;
	[HideInInspector] public bool UMAGenerated;
	[HideInInspector] public float AvatarMaxi = 1;
	[HideInInspector] public float AvatarCurrent = 0;

	[HideInInspector] public bool UseLinkedSlots;
	public bool UseLinkedOv;
	public bool UseNaturalBehaviour = true;
	[HideInInspector] public bool ToUMA = true;

	[HideInInspector] public Transform tempUMA;

	[HideInInspector] public Vector2 umaCrowdSize;
	public Transform zeroPoint;
	[HideInInspector] public float atlasResolutionScale;
	[HideInInspector] private float X = 0;
	[HideInInspector] private float Y = 0;
	[HideInInspector] public Vector2 Spacing;
	public float SpawnFrequency;
	private float umaTimer;
	[HideInInspector] public float umaTimerEnd;

	[System.Serializable]
	public class Race_Data{
		public string Gender;
		public string GenderType;
		public string MultiRace = "";
		public string  Race =  "";
		public bool SingleORMulti;
		public bool RaceDone;
		public DKRaceData RaceToCreate;
	}
	[HideInInspector] public Race_Data RaceAndGender = new Race_Data();
	int randomRace;

	[System.Serializable]
	public class RandomData{
		public int randomResult = 0;
		public bool RanShape = true;
		public bool RanElements = true;
		public int PiloAmount = 0;
		public int PiloMaxi = 0;
		public bool HairDone = false;
		public string Pilosity = "Random";
		public string Hair = "Random";
		public string Height = "Random";
		public string _Height = "Random";
		public string Weight = "Random";
		public string _Weight = "Random";
		public string Muscles = "Random";
		public string _Muscles = "Random";
		public bool Tatoo = true;
		public int TatooChance = 50;
		public bool Lips = true;
		public int LipsChance = 80;
		public bool Makeup = true;
		public int MakeupChance = 50;
	}
	[HideInInspector] public RandomData Randomize = new RandomData();

	[HideInInspector] public int  HeadIndex;
	[HideInInspector] public int  TorsoIndex;
	[HideInInspector] public int  WearMeshIndex;
	[HideInInspector] public int FaceSlot;
	[HideInInspector] public DKOverlayData HairOverlay;
	[HideInInspector] public int HairOverlayIndex;
	[HideInInspector] public int CurrentSlotIndex;

	// lists
	[HideInInspector] public List<DKSlotData> tempSlotList;
	[System.Serializable]
	public class WearsData{
		public int RanWearYes;
		public int RanWearYesMax;
		public bool RanWearChoice = true;
		public bool RanWearAct = true;
		public bool RanUnderwearChoice = true;
		public int RanActivatewear;
		public int RanActivateMesh;
		public bool WearOverlays = true;
		public bool WearMeshes = true;
		public bool HideShoulders = false;
		public DKSlotData Shoulders;
		public bool HideLegs = false;
		public DKSlotData Legs;
		public bool HideBelt = false;
		public DKSlotData Belt;
		public bool HideArmBand = false;
		public DKSlotData ArmBand;
		public bool HideWrist = false;
		public DKSlotData Wrist;
		public bool HideLegBand = false;
		public DKSlotData LegBand;
		public bool HideCollar = false;
		public DKSlotData Collar;
		public bool HideRingLeft = false;
		public DKSlotData RingLeft;
		public bool HideRingRight = false;
		public DKSlotData RingRight;
		public bool HideBackpack = false;
		public DKSlotData Backpack;
		public bool HideCloak = false;
		public DKSlotData Cloak;
		public bool HideUnderwear = false;

		// wear weight
		public List<DKUMAData.WearWeightData> WearWeightList = new List<DKUMAData.WearWeightData>();
	}
	[HideInInspector] public WearsData Wears = new WearsData();

	# endregion UMA
	[HideInInspector] public List<DKSlotData> AssignedSlotsList = new List<DKSlotData>();
	[HideInInspector] public List<DKOverlayData> AssignedOverlayList = new List<DKOverlayData>();

	public bool MediumDNA = false;

	[System.Serializable]
	public class ColorsData{
		public bool UsePresets;
		public bool UseOverlayPresets;
		public bool RanColors ;
		public float AdjRanMaxi;
		public float WearAdjRanMaxi;
		public float HairAdjRanMaxi;
		public Color  ColorToApply = new Color(1,1,1,1);
		public Color InnerMouthColor;
		public ColorPresetData InnerMouthColorPreset;

		//Skin color
		public bool RanSkin = false;
		public Color skinColor = new Color(1,1,1,1);
		public ColorPresetData skinColorPreset;
		public float skinTone;
		public float skinTone1;
		public float skinTone2;
		public float skinTone3;
		// Eyes color
		public bool RanEyes = false;
		public Color EyesColor = new Color(1,1,1,1);
		public ColorPresetData EyesColorPreset;
		public Color EyeOverlayAdjustColor;
		public float EyeOverlayAdjustColor1;
		public float EyeOverlayAdjustColor2;
		public float EyeOverlayAdjustColor3;
		// Hair Color
		public bool RanHair = false;
		public Color HairColor = new Color(1,1,1,1);
		public ColorPresetData HairColorPreset;
		public float HairColor1;
		public float HairColor2;

		//Wears
		//Torso Color
		public bool RanWear = true;
		public Color TorsoWearColor;
		public float TorsoWearColor1;
		public float TorsoWearColor2;
		public float TorsoWearColor3;
		// Legs Color
		public Color LegsWearColor= new Color(1,1,1,1);
		public float LegsWearColor1;
		public float LegsWearColor2;
		public float LegsWearColor3;
		// feet Color
		public Color FeetWearColor = new Color(1,1,1,1);
		public float FeetWearColor1;
		public float FeetWearColor2;
		public float FeetWearColor3;
		// Hand Color
		public Color HandWearColor = new Color(1,1,1,1);
		public float HandWearColor1;
		public float HandWearColor2;
		public float HandWearColor3;
		// Hand Color
		public Color HeadWearColor = new Color(1,1,1,1);
		public float HeadWearColor1;
		public float HeadWearColor2;
		public float HeadWearColor3;
		// Belt Color
		public Color BeltWearColor = new Color(1,1,1,1);
		public float BeltWearColor1;
		public float BeltWearColor2;
		public float BeltWearColor3;
	}
//	[HideInInspector] 
	public ColorsData Colors = new ColorsData();
		

	public static string DName;
	public static bool PlusID;

	public static GameObject DKSlotLibraryObj;
	public static GameObject RaceLibraryObj;
	public static GameObject OverlayLibraryObj;
	float AdjRan;
	bool Skipping = false;

	DK_UMA_GameSettings _DK_UMA_GameSettings;
	# endregion Variables

	void Init () {	
		GameObject DKUMAGeneratorObj = GameObject.Find("DKUMAGenerator");

		if ( RaceLibraryObj == null ) RaceLibraryObj = GameObject.Find("DKRaceLibrary");
		raceLibrary =  RaceLibraryObj.GetComponent<DKRaceLibrary>();
		raceLibrary.UpdateDictionary();
		
		if ( DKSlotLibraryObj == null ) DKSlotLibraryObj = GameObject.Find("DKSlotLibrary");
		slotLibrary =  DKSlotLibraryObj.GetComponent<DKSlotLibrary>();
		slotLibrary.UpdateDictionary();
		
		if ( OverlayLibraryObj == null ) OverlayLibraryObj = GameObject.Find("DKOverlayLibrary");
		overlayLibrary = OverlayLibraryObj.GetComponent<DKOverlayLibrary>();
		overlayLibrary.UpdateDictionary();
	
	}

	void OnEnable (){
		if ( _DKUMA_Variables == null ) _DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();
		if ( _DKUMA_Variables != null && _DKUMA_Variables._DK_UMA_GameSettings != null ) _DKUMA_Variables._DK_UMA_GameSettings.EnsureAllLibraries ();
		if ( UoverlayLibrary == null ) UoverlayLibrary = FindObjectOfType<OverlayLibrary>();
		if ( UslotLibrary == null ) UslotLibrary = FindObjectOfType<SlotLibrary>();

	}

	void Start () {
		DK_UMA_GameSettings _DK_UMA_GameSettings = FindObjectOfType<DKUMA_Variables>()._DK_UMA_GameSettings;

		// get Color variations
		if ( _DK_UMA_GameSettings != null ){
			Colors.AdjRanMaxi = _DK_UMA_GameSettings.Colors.FleshVariation;
			Colors.HairAdjRanMaxi = _DK_UMA_GameSettings.Colors.HairVariation;
			Colors.WearAdjRanMaxi = _DK_UMA_GameSettings.Colors.WearVariation;
		}

		GameObject DKUMAGeneratorObj = GameObject.Find("DKUMAGenerator");
		if ( RaceLibraryObj == null ) RaceLibraryObj = GameObject.Find("DKRaceLibrary");
		raceLibrary =  RaceLibraryObj.GetComponent<DKRaceLibrary>();
		raceLibrary.UpdateDictionary();
		
		if ( DKSlotLibraryObj == null ) DKSlotLibraryObj = GameObject.Find("DKSlotLibrary");
		slotLibrary =  DKSlotLibraryObj.GetComponent<DKSlotLibrary>();
		slotLibrary.UpdateDictionary();
		
		if ( OverlayLibraryObj == null ) OverlayLibraryObj = GameObject.Find("DKOverlayLibrary");
		overlayLibrary = OverlayLibraryObj.GetComponent<DKOverlayLibrary>();
		overlayLibrary.UpdateDictionary();

	//	DK_RPG_UMA_Generator _DK_RPG_UMA_Generator;
	//	_DK_RPG_UMA_Generator = GameObject.Find("DK_UMA").GetComponent<DK_RPG_UMA_Generator>();
	//	if ( !_DK_RPG_UMA_Generator ) _DK_RPG_UMA_Generator = GameObject.Find("DK_UMA").AddComponent<DK_RPG_UMA_Generator>();
	}
	
	public DK_UMACrowd ()
	{
		#if UNITY_EDITOR
		EditorApplication.update += Update;
		#endif
	}

	public void CreateWeights (){
		if (Wears.WearWeightList.Count == 0 ){
			DKUMAData.WearWeightData tmpData1 = new DKUMAData.WearWeightData();
			DKUMAData.WearWeightData tmpData2 = new DKUMAData.WearWeightData();
			DKUMAData.WearWeightData tmpData3 = new DKUMAData.WearWeightData();
			DKUMAData.WearWeightData tmpData4 = new DKUMAData.WearWeightData();
			DKUMAData.WearWeightData tmpData5 = new DKUMAData.WearWeightData();
			DKUMAData.WearWeightData tmpData6 = new DKUMAData.WearWeightData();
			tmpData1.Name = "Head Weight";AddWeights( tmpData1 );
			tmpData2.Name = "Torso Weight";AddWeights( tmpData2 );
			tmpData3.Name = "Hands Weight";AddWeights( tmpData3 );
			tmpData4.Name = "Legs Weight";AddWeights( tmpData4 );
			tmpData5.Name = "Feet Weight";AddWeights( tmpData5 );
			tmpData5.Name = "Shoulder Weight";AddWeights( tmpData5 );
		}
	}

	void AddWeights( DKUMAData.WearWeightData tmpData ){
		tmpData.Weights.Add("Light");
		tmpData.Weights.Add("Medium");
		tmpData.Weights.Add("High");
		tmpData.Weights.Add("Heavy");
		
		Wears.WearWeightList.Add(tmpData);
	}
	public void RemoveWeights (){
		if (Wears.WearWeightList.Count != 0 ){
			RemoveWeight( Wears.WearWeightList[0] );
			RemoveWeight( Wears.WearWeightList[1] );
			RemoveWeight( Wears.WearWeightList[2] );
			RemoveWeight( Wears.WearWeightList[3] );
			RemoveWeight( Wears.WearWeightList[4] );
			RemoveWeight( Wears.WearWeightList[5] );
		}
	}
	void RemoveWeight( DKUMAData.WearWeightData tmpData ){
		tmpData.Weights.Remove("Light");
		tmpData.Weights.Remove("Medium");
		tmpData.Weights.Remove("High");
		tmpData.Weights.Remove("Heavy");
	}

	void Update () {
		#if UNITY_EDITOR
	//	if ( FindObjectOfType<DKUMA_Variables>() != null )
	//		_DK_UMA_GameSettings = FindObjectOfType<DKUMA_Variables>()._DK_UMA_GameSettings;
		DK_UMA_GameSettings _DK_UMA_GameSettings = null;
		GameObject DK_UMA = GameObject.Find ("DK_UMA");
		if ( DK_UMA != null ) _DK_UMA_GameSettings = DK_UMA.GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;

		// get Color variations
		if ( _DK_UMA_GameSettings != null ){
			Colors.AdjRanMaxi = _DK_UMA_GameSettings.Colors.FleshVariation;
			Colors.HairAdjRanMaxi = _DK_UMA_GameSettings.Colors.HairVariation;
			Colors.WearAdjRanMaxi = _DK_UMA_GameSettings.Colors.WearVariation;
		}
		#endif

		if (Wears != null && Wears.WearWeightList != null && Wears.WearWeightList.Count != 5 ){ 
			CreateWeights(); 
		}

		if ( zeroPoint == null ) {
			GameObject tmpObj = GameObject.Find("ZeroPoint");
			if ( tmpObj != null ) zeroPoint = tmpObj.transform;
		}

		// new system
		if ( generateLotsUMA
			&& AvatarCurrent != 0
			&& AvatarCurrent <= AvatarMaxi ){

			GenerateOneUMA();

			AvatarCurrent = AvatarCurrent + 1;

			// place
			zeroPoint =  GameObject.Find("ZeroPoint").transform;
			if(zeroPoint){
				tempUMA.transform.position = new Vector3(X+zeroPoint.position.x - umaCrowdSize.x*0.5f + 0.5f,zeroPoint.position.y,Y+zeroPoint.position.z - umaCrowdSize.y*0.5f + 0.5f);
			}else{
				tempUMA.transform.position = new Vector3(zeroPoint.position.x + X - umaCrowdSize.x*0.5f + 0.5f,0,zeroPoint.position.y + Y - umaCrowdSize.y*0.5f + 0.5f);	
			}

			X = X + Spacing.x;
			if(X >= umaCrowdSize.x*Spacing.x){
				X = 0;
				Y = Y + Spacing.y;
				//	generateUMA = true;
			}
			if(Y >= umaCrowdSize.y*Spacing.x){
				Debug.Log ("DK UMA create crowd : Generating last avatar.");
				generateLotsUMA = false;
				X = 0;
				Y = 0;
			}

			// assign to avatars parent
			if ( tempUMA.transform.parent != GameObject.Find ("NPC Models").transform )  tempUMA.transform.parent = GameObject.Find ("NPC Models").transform;
		}
		else if ( generateLotsUMA
			&& AvatarCurrent == AvatarMaxi + 1 ){
			generateLotsUMA = false;
			AvatarCurrent = 0;
			AvatarMaxi = 1;
		}
		# region generate One UMA
		if(generateUMA
		   && overlayLibrary.overlayElementList.Length > 0 
		   && slotLibrary.slotElementList.Length > 0)
		{
		//	Debug.Log ("Generating one avatar");

			GenerateOneUMA();
			generateUMA = false;
		}
		# endregion generate One UMA
	}

	public void CleanLibraries (){
		DKUMACleanLibraries.CleanLibraries ( slotLibrary, overlayLibrary, UslotLibrary, UoverlayLibrary );
	
	}

	public void CleanLibrariesUMA (){
		DKUMACleanLibraries.CleanLibrariesUMA ( UslotLibrary, UoverlayLibrary );

	}

	public void LaunchGenerateUMA(){
		CleanLibraries ();

		// launch the generation
		if ( RaceAndGender.SingleORMulti ) {
			GenerateOneUMA();
			UMAGenerated = true;
		//	Debug.Log ( "Launch Generate UMA : one" );
		}
		else {
			AvatarMaxi = umaCrowdSize.y * umaCrowdSize.x;
			AvatarCurrent = 1;
			generateLotsUMA = true;
			UMAGenerated = true;
		//	Debug.Log ( "Launch Generate UMA : multi" );
		}
	}


	DKUMA_Variables _DKUMA_Variables;
	public void GenerateOneUMA(){
		#region Prepare Libraries
		var umaRecipe = new DKUMAData.UMARecipe();
		if ( RaceLibraryObj == null ) RaceLibraryObj = GameObject.Find("DKRaceLibrary");
		raceLibrary =  RaceLibraryObj.GetComponent<DKRaceLibrary>();
		raceLibrary.UpdateDictionary();
				
		if ( DKSlotLibraryObj == null ) DKSlotLibraryObj = GameObject.Find("DKSlotLibrary");
		slotLibrary =  DKSlotLibraryObj.GetComponent<DKSlotLibrary>();
		slotLibrary.UpdateDictionary();
		
		if ( OverlayLibraryObj == null ) OverlayLibraryObj = GameObject.Find("DKOverlayLibrary");
		overlayLibrary = OverlayLibraryObj.GetComponent<DKOverlayLibrary>();
		overlayLibrary.UpdateDictionary();
		#endregion Prepare Libraries

		#region Randomization
		int randomResult = 0;

		#region Gender
		if ( RaceAndGender.GenderType == "Random" ) {
			randomResult = Random.Range(1,50);
			if ( randomResult <= 25 ) 
			{
				RaceAndGender.Gender = "Male";
			}
			if ( randomResult > 25 ) 
			{
				RaceAndGender.Gender = "Female";
			}
		}
		#endregion

		#region Race
		if ( !RaceAndGender.RaceDone
			|| ( generateLotsUMA && ( RaceAndGender.MultiRace == "One Race" && RaceAndGender.RaceDone == false )
				|| RaceAndGender.MultiRace == "Random for One" ) ) {
			// corresponding Races lists creation
			List<DKRaceData> Tmplist = new List<DKRaceData>();
			List<string> RaceNamelist = new List<string>();
			for(int i = 0; i < raceLibrary.raceElementList.Length; i ++){
				// verify gender
				if ( raceLibrary.raceElementList[i] == null ) {}
				else
				if ( raceLibrary.raceElementList[i].Gender == RaceAndGender.Gender ){
					if ( RaceNamelist.Contains(raceLibrary.raceElementList[i].Race) == false ) RaceNamelist.Add( raceLibrary.raceElementList[i].Race );
				}
			}
			if ( RaceNamelist.Count > 0 ) {
				randomRace = Random.Range(0, RaceNamelist.Count-1 ); 
			}
			if (RaceAndGender.Race == "" ) RaceAndGender.Race = "Random";
			if (RaceAndGender.Race == "Random"
				|| ( generateLotsUMA && ( RaceAndGender.MultiRace == "One Race" && RaceAndGender.RaceDone == false )
					|| RaceAndGender.MultiRace == "Random for One" ) ) {
				RaceAndGender.Race = RaceNamelist[randomRace];
				for(int i = 0; i < raceLibrary.raceElementList.Length; i ++){
					// verify gender
					if ( raceLibrary.raceElementList[i] == null ) {}
					else
					if ( raceLibrary.raceElementList[i].Gender == RaceAndGender.Gender 
						    && raceLibrary.raceElementList[i].Race == RaceAndGender.Race
						)
					{
						if ( Tmplist.Contains(raceLibrary.raceElementList[i]) == false ) Tmplist.Add( raceLibrary.raceElementList[i] );
					}
				}
				if ( Tmplist.Count > 0 ) {
					randomRace = Random.Range(0, Tmplist.Count-1 ); 
					RaceAndGender.RaceToCreate = Tmplist[randomRace];
				}
			}
			else {
				for(int i = 0; i < raceLibrary.raceElementList.Length; i ++){
					// verify gender
					if ( raceLibrary.raceElementList[i] == null ) {}
					else
					if ( raceLibrary.raceElementList[i].Gender == RaceAndGender.Gender 
						    && raceLibrary.raceElementList[i].Race == RaceAndGender.Race
						)
					{
						if ( Tmplist.Contains(raceLibrary.raceElementList[i]) == false ) Tmplist.Add( raceLibrary.raceElementList[i] );
					}
				}
				if ( Tmplist.Count > 0 ) {
					randomRace = Random.Range(0, Tmplist.Count-1 ); 
					RaceAndGender.RaceToCreate = Tmplist[randomRace];
				}
			}
			if ( RaceAndGender.MultiRace == "One Race" && RaceAndGender.RaceDone == false ) RaceAndGender.RaceDone = true;
		}
		if ( RaceAndGender.MultiRace == "Random for All" && RaceAndGender.RaceDone == false ) RaceAndGender.RaceDone = true;


		#endregion

		#region Colors
		#region Prepare ColorPresets
		if ( Colors.UsePresets == true ){
			List<ColorPresetData> skinColorPresetList = new List<ColorPresetData>();
			List<ColorPresetData> EyesColorPresetList = new List<ColorPresetData>();
			List<ColorPresetData> HairColorPresetList = new List<ColorPresetData>();
			List<ColorPresetData> InnerMouthColorPresetList = new List<ColorPresetData>();

			for(int i1 = 0; i1 <  RaceAndGender.RaceToCreate.ColorPresetDataList.Count; i1 ++){
				ColorPresetData ColorPreset = RaceAndGender.RaceToCreate.ColorPresetDataList[i1];
				if ( ColorPreset == null ) Debug.LogError ("The color Preset List of the race "+RaceAndGender.RaceToCreate.name+"  contains some null entry, correct it.");
				else if ( Colors != null ) {
					if ( ColorPreset.OverlayType == "Flesh" || ColorPreset.OverlayType == "Face" ){
						Colors.RanSkin = false;
						Colors.RanColors = false;
						skinColorPresetList.Add(ColorPreset);
					}
					if ( ColorPreset.OverlayType == "Eyes" ){
						Colors.RanEyes = false;
						Colors.RanColors = false;
						EyesColorPresetList.Add(ColorPreset);
					}
					if ( ColorPreset.OverlayType == "Hair" 
					    || ColorPreset.OverlayType == "Eyebrow" 
					    || ColorPreset.OverlayType == "Beard" ){
						Colors.RanHair = false;
						Colors.RanColors = false;
						HairColorPresetList.Add(ColorPreset);
					}
					if ( ColorPreset.OverlayType == "InnerMouth"){
						InnerMouthColorPresetList.Add(ColorPreset);
					}
				}
			}

			// ran in lists and apply to color
			if ( skinColorPresetList.Count !=0 ){
				int Ran = Random.Range(0, skinColorPresetList.Count-1);
			//	float ranColorChange = Random.Range( 0, Colors.AdjRanMaxi );
				Colors.skinColor = skinColorPresetList[Ran].PresetColor 
			//		+ new Color (ranColorChange,ranColorChange,ranColorChange,1)
					;
				Colors.skinColorPreset = skinColorPresetList[Ran];
			}
			else Colors.RanSkin = true;
			if ( EyesColorPresetList.Count !=0 ){
				int Ran = Random.Range(0, EyesColorPresetList.Count-1);
				Colors.EyesColor = EyesColorPresetList[Ran].PresetColor;
				Colors.EyesColorPreset = EyesColorPresetList[Ran];
			}
			else Colors.RanEyes = true;
			if ( HairColorPresetList.Count !=0 ){
				int Ran = Random.Range(0, HairColorPresetList.Count-1);
				Colors.HairColor = HairColorPresetList[Ran].PresetColor;
				Colors.HairColor = new Color(HairColorPresetList[Ran].PresetColor.r,
				                             HairColorPresetList[Ran].PresetColor.g,
				                             HairColorPresetList[Ran].PresetColor.b,1.0f);
				Colors.HairColorPreset = HairColorPresetList[Ran];
			}
			else Colors.RanHair = true;
			if ( InnerMouthColorPresetList.Count !=0 ){
				int Ran = Random.Range(0, InnerMouthColorPresetList.Count-1);
				Colors.InnerMouthColor = InnerMouthColorPresetList[Ran].PresetColor;
				Colors.InnerMouthColorPreset = InnerMouthColorPresetList[Ran];
			}
		}
		#endregion Prepare ColorPresets

		#region Randomize Colors
		if ( Colors.RanColors || Colors.RanSkin )
		{
			// Skin
			Colors.skinTone = Random.Range(0.1f, 0.6f);
			Colors.skinTone1 = Colors.skinTone + Random.Range(0.35f,0.4f);	
			Colors.skinTone2 = Colors.skinTone + Random.Range(0.25f,0.4f);
			Colors.skinTone3 = Colors.skinTone + Random.Range(0.35f,0.4f);
			Colors.skinColor = new Color(Colors.skinTone1, Colors.skinTone2, Colors.skinTone3, 1);
		}
		if ( Colors.RanColors || Colors.RanEyes )
		{
			// Eyes
			Colors.EyeOverlayAdjustColor1 = Random.Range(0.1f,0.9f);
			Colors.EyeOverlayAdjustColor2 = Random.Range(0.1f,0.9f);
			Colors.EyeOverlayAdjustColor3 = Random.Range(0.1f,0.9f);
			Colors.EyeOverlayAdjustColor = new Color(Colors.EyeOverlayAdjustColor1 , Colors.EyeOverlayAdjustColor2 , Colors.EyeOverlayAdjustColor3,1);
			Colors.EyesColor = new Color(Colors.EyeOverlayAdjustColor1 ,Colors.EyeOverlayAdjustColor2,Colors.EyeOverlayAdjustColor3,1);
			Colors.EyeOverlayAdjustColor = new Color(Colors.EyeOverlayAdjustColor1 ,Colors.EyeOverlayAdjustColor2,Colors.EyeOverlayAdjustColor3,1);
		}

		if ( Colors.RanColors || Colors.RanHair )
		{
			// Hair	
			Colors.HairColor1 = Random.Range(0.1f,0.9f);
			Colors.HairColor2 = Random.Range(0.01f,0.9f);
			Colors.HairColor = new Color(Colors.HairColor1,Colors.HairColor2,Colors.HairColor2,1.0f);
		}
		if ( Colors.RanColors || Colors.RanWear )
		{
			// Torso
			Colors.TorsoWearColor1 = Random.Range(0.01f,0.9f);
			Colors.TorsoWearColor2 = Random.Range(0.01f,0.9f);
			Colors.TorsoWearColor3 = Random.Range(0.01f,0.9f);
			Colors.TorsoWearColor = new Color(Colors.TorsoWearColor1 ,Colors.TorsoWearColor2,Colors.TorsoWearColor3,1);
			// Legs
			Colors.LegsWearColor1 = Random.Range(0.01f,0.9f);
			Colors.LegsWearColor2 = Random.Range(0.01f,0.9f);
			Colors.LegsWearColor3 = Random.Range(0.01f,0.9f);
			Colors.LegsWearColor= new Color(Colors.LegsWearColor1 ,Colors.LegsWearColor2,Colors.LegsWearColor3,1);
			// Belt
			Colors.BeltWearColor1 = Random.Range(0.01f,0.9f);
			Colors.BeltWearColor2 = Random.Range(0.01f,0.9f);
			Colors.BeltWearColor3 = Random.Range(0.01f,0.9f);
			Colors.BeltWearColor = new Color(Colors.BeltWearColor1 ,Colors.BeltWearColor2,Colors.BeltWearColor3,1);
			// Head
			Colors.HeadWearColor1 = Random.Range(0.01f,0.9f);
			Colors.HeadWearColor2 = Random.Range(0.01f,0.9f);
			Colors.HeadWearColor3 = Random.Range(0.01f,0.9f);
			Colors.HeadWearColor = new Color(Colors.HeadWearColor1 ,Colors.HeadWearColor2,Colors.HeadWearColor3,1);
			// Hand
			Colors.HandWearColor1 = Random.Range(0.01f,0.9f);
			Colors.HandWearColor2 = Random.Range(0.01f,0.9f);
			Colors.HandWearColor3 = Random.Range(0.01f,0.9f);
			Colors.HandWearColor = new Color(Colors.HandWearColor1 ,Colors.HandWearColor2,Colors.HandWearColor3,1);
			// Feet
			Colors.FeetWearColor1 = Random.Range(0.01f,0.9f);
			Colors.FeetWearColor2 = Random.Range(0.01f,0.9f);
			Colors.FeetWearColor3 = Random.Range(0.01f,0.9f);
			Colors.FeetWearColor = new Color(Colors.FeetWearColor1 ,Colors.FeetWearColor2,Colors.FeetWearColor3,1);
		}
		#endregion Randomize Colors
		#endregion  Colors
		#endregion Randomization

		// verify if race is ready
		if ( RaceAndGender.RaceToCreate._Male._AvatarData._Face._Head.SlotList.Count == 0
			&& RaceAndGender.RaceToCreate._Male._AvatarData._Face._Head.SlotList.Count == 0 ) 
			Debug.LogError ("DK UMA : It seems like your Races are not properly prepared. " +
				"Open the 'Elements Manager' window and click twice on the 'Add to Libraries' button. " +
				"First time is to populate the Database, second time is to populate the races.");
		else {
			#region Create the UMA gameobject
			umaRecipe.SetRace(raceLibrary.GetRace(RaceAndGender.RaceToCreate.raceName));
			GameObject racePrefab;

	    	tempUMA = (Instantiate(umaRecipe.raceData.racePrefab ,Vector3.zero,Quaternion.identity) as GameObject).transform;
			if ( DName == "" || DName == null ) {
				tempUMA.name = "New UMA Model";

				if ( PlusID ) tempUMA.name = tempUMA.name+" ("+tempUMA.GetInstanceID()+")";
			}
			else {
				tempUMA.name = DName;
				if ( PlusID ) tempUMA.name = tempUMA.name+" ("+tempUMA.GetInstanceID()+")";
			}
			umaData = tempUMA.gameObject.GetComponent<DKUMAData>();

			try{
				umaData.umaRecipe = umaRecipe;
			}catch(System.NullReferenceException){ Debug.Log ("No UMA Recipe");}
			if ( tempUMA.gameObject.GetComponent("DK_Model") as DK_Model == null ) 
			{
				tempUMA.gameObject.AddComponent<DK_Model>();
			}
			_DK_Model = tempUMA.GetComponent<DK_Model>();

			// assign to avatars parent
			if ( tempUMA.transform.parent != GameObject.Find ("NPC Models").transform )  tempUMA.transform.parent = GameObject.Find ("NPC Models").transform;

			#endregion Create the UMA gameobject

			SetDKUMAData();
		}
	}
		
	void SetDKUMAData(){
	//	Debug.Log ("SetDKUMAData");
		umaData.Creating = true;

		if ( GeneratorMode == "" ){
			umaData.atlasResolutionScale = atlasResolutionScale;
			umaData.Dirty(true,true,true);
			umaData.OnUpdated += MyOnUpdateMethod;
		}
		_DK_Model.OverlayLibraryObj = OverlayLibraryObj.name;
		_DK_Model.DKSlotLibraryObj = DKSlotLibraryObj.name;
		_DK_Model.RaceLibraryObj = RaceLibraryObj.name;

		umaData.OverlayLibraryObj = OverlayLibraryObj.name;
		umaData.overlayLibrary = OverlayLibraryObj.GetComponent<DKOverlayLibrary>();;

		umaData.DKSlotLibraryObj = DKSlotLibraryObj.name;
		umaData.slotLibrary = DKSlotLibraryObj.GetComponent<DKSlotLibrary>();;

		umaData.RaceLibraryObj = RaceLibraryObj.name;
		umaData.raceLibrary = RaceLibraryObj.GetComponent<DKRaceLibrary>();

		#if UNITY_EDITOR
		Selection.activeGameObject = umaData.transform.gameObject ;
		#endif
		///////////////////////
		GenerateUMAShapes();
	}
	
	void GenerateUMAShapes (){	
	//	Debug.Log ("GenerateUMAShapes");

		#region DNA Converter
		umaDna = new DKUMADnaHumanoid();
	//	umaData.umaRecipe.umaDna.Add(umaDna.GetType(),umaDna);
		for(int i = 0; i < umaData.umaRecipe.raceData.dnaConverterList.Length; i ++){
			if ( umaData.umaRecipe.raceData.dnaConverterList[i].GetComponent<DK_DNAConverterBehaviour>() != null)
			{
				DNAConvObj = umaData.umaRecipe.raceData.dnaConverterList[i].gameObject;
			}
		}
		# endregion DNA Converter

		#region Randomization DNA values
		if ( Randomize.RanShape 
			&&  umaDna != null) 
		{
				if ( Randomize.RanShape || Randomize.Height == "Random" ) {
					int Ran = Random.Range( 1, 3 );
					if ( Ran == 1 ) Randomize._Height = "Low";
					if ( Ran == 2 ) Randomize._Height = "Medium";
					if ( Ran == 3 ) Randomize._Height = "High";
				}

				if ( Randomize.RanShape || Randomize.Muscles == "Random" ){ 
					int Ran = Random.Range( 1, 3 );
					if ( Ran == 1 ) Randomize._Muscles = "Low";
					if ( Ran == 2 ) Randomize._Muscles = "Medium";
					if ( Ran == 3 ) Randomize._Muscles = "High";
				}

				if ( Randomize.RanShape || Randomize.Weight == "Random" ) {
					int Ran = Random.Range( 1, 3 );
					if ( Ran == 1 ) Randomize._Weight = "Low";
					if ( Ran == 2 ) Randomize._Weight = "Medium";
					if ( Ran == 3 ) Randomize._Weight = "High";
				}

			for (int i = 0; i <  RaceAndGender.RaceToCreate.DNAConverterDataList.Count; i ++) {

				if ( MediumDNA ) RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = 0.5f;
				else {
					float ValueMini = RaceAndGender.RaceToCreate.ConverterDataList[i].ValueMini;
					float ValueMaxi = RaceAndGender.RaceToCreate.ConverterDataList[i].ValueMaxi;

					// apply DNA limits
					DKUMA_Variables _DKUMA_Variables = GameObject.Find ("DK_UMA" ).GetComponent<DKUMA_Variables>();
					if ( ValueMini < _DKUMA_Variables._DK_UMA_GameSettings.MinDnaValueLimit ) ValueMini = _DKUMA_Variables._DK_UMA_GameSettings.MinDnaValueLimit;
					if ( ValueMaxi > _DKUMA_Variables._DK_UMA_GameSettings.MaxDnaValueLimit ) ValueMaxi = _DKUMA_Variables._DK_UMA_GameSettings.MaxDnaValueLimit;




					if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Type == "Height" ){
						if ( Randomize.Height == "Random" ) {
							if ( Randomize._Height == "Low" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMini, ValueMaxi/3);
							if ( Randomize._Height == "Medium" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi/3, ValueMaxi-(ValueMaxi/3));
							if ( Randomize._Height == "High" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi-(ValueMaxi/3),ValueMaxi);
							if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value < ValueMini )
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = ValueMini;
							if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value < ValueMaxi )
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = ValueMaxi;

						}
						else {
							if ( Randomize.Height == "Low" ) 
									RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMini, ValueMaxi/3);
							if ( Randomize.Height == "Medium" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi/3, ValueMaxi-(ValueMaxi/3));
							if ( Randomize.Height == "High" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi-(ValueMaxi/3),ValueMaxi);
							if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value < ValueMini )
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = ValueMini;
							if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value > ValueMaxi )
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = ValueMaxi;
						}
					}
					else 
					if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Type == "Muscles" ){
						if ( Randomize.Muscles == "Random" ){ 
							if ( Randomize._Muscles == "Low" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMini, ValueMaxi/3);
							if ( Randomize._Muscles == "Medium" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi/3, ValueMaxi-(ValueMaxi/3));
							if ( Randomize._Muscles == "High" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi-(ValueMaxi/3),ValueMaxi);
							if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value < ValueMini )
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = ValueMini;
							if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value > ValueMaxi )
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = ValueMaxi;
						}
						else {
							if ( Randomize.Muscles == "Low" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMini, ValueMaxi/3);
							if ( Randomize.Muscles == "Medium" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi/3, ValueMaxi-(ValueMaxi/3));
							if ( Randomize.Muscles == "High" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi-(ValueMaxi/3),ValueMaxi);
							if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value < ValueMini )
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = ValueMini;
							if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value > ValueMaxi )
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = ValueMaxi;
						}
					}
					else
					if ( RaceAndGender.RaceToCreate.DNAConverterDataList[i].Type == "Weight" ){
						if ( Randomize.Weight == "Random" ) {
							if ( Randomize._Weight == "Low" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMini, ValueMaxi/3);
							if ( Randomize._Weight == "Medium" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi/3, ValueMaxi-(ValueMaxi/3));
							if ( Randomize._Weight == "High" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi-(ValueMaxi/3),ValueMaxi);

						}
						else {
							if ( Randomize.Weight == "Low" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMini, ValueMaxi/3);
							if ( Randomize.Weight == "Medium" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi/3, ValueMaxi-(ValueMaxi/3));
							if ( Randomize.Weight == "High" ) 
								RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(ValueMaxi-(ValueMaxi/3),ValueMaxi);
						}
					}
					else{
						RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value = Random.Range(RaceAndGender.RaceToCreate.ConverterDataList[i].ValueMini, RaceAndGender.RaceToCreate.ConverterDataList[i].ValueMaxi);
					}
				}

				DKRaceData.DNAConverterData _newDNA = new DKRaceData.DNAConverterData();
				_newDNA.Name = RaceAndGender.RaceToCreate.DNAConverterDataList[i].Name;
				_newDNA.Value = RaceAndGender.RaceToCreate.DNAConverterDataList[i].Value;
				_newDNA.Part = RaceAndGender.RaceToCreate.DNAConverterDataList[i].Part;
				_newDNA.Part2 = RaceAndGender.RaceToCreate.DNAConverterDataList[i].Part2;
				umaData.DNAList2.Add(_newDNA);

				// add to DK_UMAdnaHumanoid
					float tmpValue = _newDNA.Value;
				if ( MediumDNA ) tmpValue = 0.5f;
									
				if ( i == 0 ) umaDna.N0 = tmpValue;if ( i == 1 ) umaDna.N1 = tmpValue;if ( i == 2 ) umaDna.N2 = tmpValue;
				if ( i == 3 ) umaDna.N3 = tmpValue;if ( i == 4 ) umaDna.N4 = tmpValue;if ( i == 5 ) umaDna.N5 = tmpValue;
				if ( i == 6 ) umaDna.N6 = tmpValue;if ( i == 7 ) umaDna.N7 = tmpValue;if ( i == 8 ) umaDna.N8 = tmpValue;
				if ( i == 9 ) umaDna.N9 = tmpValue;if ( i == 10 ) umaDna.N10 = tmpValue;if ( i == 11 ) umaDna.N11 = tmpValue;
				if ( i == 12 ) umaDna.N12 = tmpValue;if ( i == 13 ) umaDna.N13 = tmpValue;if ( i == 14 ) umaDna.N14 = tmpValue;
				if ( i == 15 ) umaDna.N15 = tmpValue;if ( i == 16 ) umaDna.N16 = tmpValue;if ( i == 17 ) umaDna.N17 = tmpValue;
				if ( i == 18 ) umaDna.N18 = tmpValue;if ( i == 19 ) umaDna.N19 = tmpValue;if ( i == 20 ) umaDna.N20 = tmpValue;
				if ( i == 21 ) umaDna.N21 = tmpValue;if ( i == 22 ) umaDna.N22 = tmpValue;if ( i == 23 ) umaDna.N23 = tmpValue;
				if ( i == 24 ) umaDna.N24 = tmpValue;if ( i == 25 ) umaDna.N25 = tmpValue;if ( i == 26 ) umaDna.N26 = tmpValue;
				if ( i == 27 ) umaDna.N27 = tmpValue;if ( i == 28 ) umaDna.N28 = tmpValue;if ( i == 29 ) umaDna.N29 = tmpValue;
				if ( i == 30 ) umaDna.N30 = tmpValue;if ( i == 31 ) umaDna.N31 = tmpValue;if ( i == 32 ) umaDna.N32 = tmpValue;
				if ( i == 33 ) umaDna.N33 = tmpValue;if ( i == 34 ) umaDna.N34 = tmpValue;if ( i == 35 ) umaDna.N35 = tmpValue;
				if ( i == 36 ) umaDna.N36 = tmpValue;if ( i == 37 ) umaDna.N37 = tmpValue;if ( i == 38 ) umaDna.N38 = tmpValue;
				if ( i == 39 ) umaDna.N39 = tmpValue;if ( i == 40 ) umaDna.N40 = tmpValue;if ( i == 41 ) umaDna.N41 = tmpValue;
				if ( i == 42 ) umaDna.N42 = tmpValue;if ( i == 43 ) umaDna.N43 = tmpValue;if ( i == 44 ) umaDna.N44 = tmpValue;
				if ( i == 45 ) umaDna.N45 = tmpValue;if ( i == 46 ) umaDna.N46 = tmpValue;if ( i == 47 ) umaDna.N47 = tmpValue;
				if ( i == 48 ) umaDna.N48 = tmpValue;if ( i == 49 ) umaDna.N49 = tmpValue;
				if ( i == 50 ) umaDna.N50 = tmpValue;if ( i == 51 ) umaDna.N51 = tmpValue;if ( i == 52 ) umaDna.N52 = tmpValue;
				if ( i == 53 ) umaDna.N53 = tmpValue;if ( i == 54 ) umaDna.N54 = tmpValue;if ( i == 55 ) umaDna.N55 = tmpValue;
				if ( i == 56 ) umaDna.N56 = tmpValue;if ( i == 57 ) umaDna.N57 = tmpValue;if ( i == 58 ) umaDna.N58 = tmpValue;
				if ( i == 59 ) umaDna.N59 = tmpValue;
				if ( i == 60 ) umaDna.N60 = tmpValue;if ( i == 61 ) umaDna.N61 = tmpValue;if ( i == 62 ) umaDna.N62 = tmpValue;
				if ( i == 63 ) umaDna.N63 = tmpValue;if ( i == 64 ) umaDna.N64 = tmpValue;if ( i == 65 ) umaDna.N65 = tmpValue;
				if ( i == 66 ) umaDna.N66 = tmpValue;if ( i == 67 ) umaDna.N67 = tmpValue;if ( i == 68 ) umaDna.N68 = tmpValue;
				if ( i == 69 ) umaDna.N69 = tmpValue;
				if ( i == 70 ) umaDna.N70 = tmpValue;
				}
			}
			#endregion Randomization DNA values
		//////
		 
		umaData.umaRecipe.umaDna.Add(umaDna.GetType(),umaDna);


		DK_Model ();
		DefineSlots();
	}


	void DefineSlots (){
		DK_RPG_DefineSlotBody.DefineSlots(this);
	}

	public void MyOnUpdateMethod(bool firstTime){}

	void ResetOverlay(DKSlotData[] slotElementList){
		for(int i = 0; i < slotElementList.Length; i++){
			if(slotElementList[i].overlayList.Count > 1){
				DKOverlayData tempOverlayList = slotElementList[i].overlayList[0];
				slotElementList[i].overlayList = new List<DKOverlayData>();
				slotElementList[i].overlayList.Add(tempOverlayList);
			}
		}
	}

	void DK_Model () {
		// Copy variables to DK_Model
		_DK_Model.Gender = RaceAndGender.Gender;
		_DK_Model.Race = RaceAndGender.Race;
		_DK_Model.IsUmaModel = true;
	}
}