using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class EditorVariables : MonoBehaviour {

	
	#region Variables

	public static Transform MFSelectedList;
	public static GameObject UMAModel;
	public static string AvatarName = "";

	public static DK_UMACrowd DK_UMACrowd;
	public static DKUMACustomization _DKUMACustomization;
	public static DKUMACustomization DK_DKUMACustomization;
	public static GameObject DKUMAGeneratorObj;
	public static GameObject UMACrowdObj;
	public static GameObject DKUMACustomizationObj;
	public static GameObject _DK_UMA_RPG_Data;
	public static DK_RPG_UMA_Generator _DK_RPG_UMA_Generator;

	public static bool ShowPrepare ;
	public static bool showCreate ;
	public static bool showModify ;
	public static bool showMorph ;
	public static bool showColors ;
	public static bool showColors2 ;
	public static bool showComp ;
	public static bool showList ;
	public static bool showSetup ;
	public static bool showPlugIn ;
	public static bool showAbout ;
	
	public static bool SingleORMulti = true;
	
	public static DKRaceData RaceToCreate;

	public static GameObject RaceLibraryObj;	
	public static DKOverlayLibrary _OverlayLibrary;
	public static GameObject OverlayLibraryObj;	
	public static DKRaceLibrary _RaceLibrary;
	public static GameObject DKSlotLibraryObj;	
	public static DKSlotLibrary _DKSlotLibrary;

	// web

	// Setup
	public static string AvatarSavePath = "";
	
	// Prepare
	public static GameObject UMAObj;
	public static string UMAObjDefault = "UMA";
	public static GameObject NPCModelsObj;

	public static string SelectedElementName = "";
	public static string SelectedElementRace = "";
	public static string SelectedElementType = "";
	public static string SelectedElementOverlayType ="";
	public static string SelectedElementWearWeight ="";
	public static string SelectedElementGender = "Both";
	public static GameObject SelectedElementObj;
	public static bool Replace = false;
	public static bool HideEars = false;
	public static bool HideMouth = false;
	public static bool HideHair = false;
	public static bool HideHairModule = false;
	public static DK_SlotsAnatomyElement SelectedElemPlace;

	public static DKOverlayData SelectedElemOvlay;
	public static DKOverlayData SelectedLinkedOvlay;
	public static DKSlotData SelectedElemSlot;
	public static DKSlotData SelectedLegacyElemSlot;
	public static  List<DKOverlayData> overlayList = new List<DKOverlayData>();

	// Detect Element Types
	public static bool AutoDetLib = false;
	public static bool DetRace;
	public static bool DetGender;
	public static bool DetPlace;
	public static bool DetOvType;
	public static bool DetWWeight;
	public static bool DetLink;
	public static string TmpName;
	public static bool DetSlots;
	public static bool DetOverlay;
	public static bool DetRaces;
	public static bool ApplyToEmpty;
	public static bool ApplyToSelection;
	public static bool ApplyToAll;
	public static string NewRaceName = "";
	public static bool RemoveRace = false;
	public static List<string> RaceToApplyList = new List<string>();
	public static bool SearchResultsOnly = false;
	
	// DK Libraries
	public static GameObject DK_UMA;
	public static GameObject SlotsAnatomyLibraryObj;	
	public static DK_SlotsAnatomyLibrary _SlotsAnatomyLibrary;
	public static DK_SlotsAnatomyLibrary TMP_SlotsAnatomyLibrary;
	public static GameObject SlotExpressionsLibraryObj;	
	public static DK_SlotExpressionsLibrary _SlotExpressionsLibrary;
	public static DK_SlotExpressionsLibrary TMP_SlotExpressionsLibrary;

	// Generator Presets

	public static GameObject GeneratorPresets;
	public static GameObject PresetToEdit;
	public static GameObject New_DK_GeneratorPresetLibraryObj;	

	
	// modify

	public static Transform EditedModel;

	public static string NewModelName = "";

	public static float ModifLimit = 2;
	
	// Colors
	// Presets
	public static string HeadWColorPresetName;
	public static string HandWColorPresetName;
	public static string LegsWColorPresetName;
	public static string TorsoWColorPresetName;
	public static string BeltWColorPresetName;
	public static string SkinColorPresetName;
	public static string HairColorPresetName;
	public static string EyesColorPresetName;
	public static string FeetWColorPresetName;
	

	// skin
	public static Color SkinTone;
	public static Color SkinColor;
	public static Color PickedColor;
	public static float SkinTone0 ;
	public static float SkinTone1 ;
	public static float SkinTone2 ;
	public static float SkinTone3 ;
	// Eyes
	public static Color EyesColor ;
	public static Color PickedEyesColor ;
	public static Color EyesTone ;
	public static float EyeOverlayAdjustColor ;
	// Hair
	public static Color HairColor ;
	public static Color PickedHairColor ;
	public static Color HairTone ;
	public static float HairColor1 ;
	public static float HairColor2 ;
	// Wear
	public static Color PickedTorsoWearColor;
	public static Color TorsoWearTone;
	public static Color TorsoWearColor;
	public static float TorsoWearColor1 ;
	public static float TorsoWearColor2 ;
	public static float TorsoWearColor3 ;
	// Legs
	public static Color PickedLegsWearColor;
	public static Color LegsWearTone;
	public static Color LegsWearColor ;
	public static float LegsWearColor1 ;
	public static float LegsWearColor2 ;
	public static float LegsWearColor3 ;
	// feet Color
	public static Color FeetWearColor = new Color(1,1,1,1);
	public static Color PickedFeetWearColor;
	public static Color FeetWearTone;
	public static float FeetWearColor1;
	public static float FeetWearColor2;
	public static float FeetWearColor3;
	// Hand Color
	public static Color HandWearColor = new Color(1,1,1,1);
	public static Color PickedHandWearColor;
	public static Color HandWearTone;
	public static float HandWearColor1;
	public static float HandWearColor2;
	public static float HandWearColor3;
	// Hand Color
	public static Color HeadWearColor = new Color(1,1,1,1);
	public static Color PickedHeadWearColor;
	public static Color HeadWearTone;
	public static float HeadWearColor1;
	public static float HeadWearColor2;
	public static float HeadWearColor3;
	// Belt Color
	public static Color BeltWearColor = new Color(1,1,1,1);
	public static Color PickedBeltWearColor;
	public static Color BeltWearTone;
	public static float BeltWearColor1;
	public static float BeltWearColor2;
	public static float BeltWearColor3;
	
	public static string DName;

	public static GameObject ColorPresetsObj;
	public static GameObject DK_UMAObj;
	public static GameObject GenPresetsObj;
	public static GameObject DNALibrariesObj;
	public static GameObject ZeroPoint;

	public static List<DKRaceData.DNAConverterData> tmpDNAList = new List<DKRaceData.DNAConverterData>();

	#endregion Variables
	


}
