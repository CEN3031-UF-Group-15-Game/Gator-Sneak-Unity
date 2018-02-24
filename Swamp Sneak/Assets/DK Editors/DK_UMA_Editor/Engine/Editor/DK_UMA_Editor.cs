using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0472 // Null is true.
#pragma warning disable 0649 // Never Assigned.

public class DK_UMA_Editor : EditorWindow {
	
	#region Variables
	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);
	
	public static GameObject ObjToFind;
	public static DKUMADnaHumanoid umaDna;

	public static bool CreatingUMA;
	public static bool Helper;
		
	public static bool Step0 = false;
	public static bool Step1 = false;
	public static bool Step2 = false;
	public static bool Step3 = false;
	public static bool Step4 = false;
	public static bool Step5 = false;
	public static bool Step6 = false;
	public static bool Step7 = false;
	public static bool MultipleUMASetup = false;
	
	public static int RanRace;
	public static DK_Race DK_Race;

	public static string Gender;
	public static string MultiRace;
	
	public static bool ShowPrepare ;
	public static bool showCreate ;
	public static bool showModify ;
	public static bool showMorph ;
	public static bool showColors ;
	public static bool showColors2 ;
	public static bool showComp ;
	public static bool showList ;
	public static bool showSetup ;
	public static bool showCorrector ;
	public static bool showPlugIn ;
	public static bool showAbout ;
	public static GameObject SlotExpressions;

	// Prepare
	public static string UMAObjName ;
	public static bool ShowLibraries = false;
	public static bool ShowDKLibraries = false;
	public static bool ShowDKLib = false;
	public static bool DisplayLists;

	public static bool choosePlace = false;
	public static bool chooseOverlay = false;
	public static bool chooseSlot = false;
	public static bool LinkedOverlayList = false;
	public static bool LinkedSlotList = false;
	public static Vector2 LinkedOverlayListScroll = new Vector2();
	public static Vector2 LinkedSlotListScroll = new Vector2();

	public static int TmpSlotIndex;
	public static string _SpawnPerct;
	public static string SearchString = "";
	
	// Detect Element Types
	public static  bool DetAll;
	
	// DK Libraries
	public static string myPath;

	public static bool ElemAlreadyIn = false;
	public static bool ShowDKLibSE = false;
	public static bool ShowDKLibSA = false;
	public static string NewExpressionName = "";
	public static string AnatomyPart = "";
	public static GameObject SelectedExpression;
	public static DK_SlotExpressionsElement dk_SlotExpressionsElement;
	public static bool ShowSelectAnatomy = false;
	public static DK_SlotsAnatomyElement SelectedAnaPart;
	public static GameObject SelectedAnaPartObj;
	public static string NewAnatomyName = "";
	public static bool ChooseLink;
	public static DK_SlotExpressionsElement AnaToLink;
	
	public static string SelectedRace = "Both";
	public static string SelectedPlace = "";
	public static string OverlayType =  "";
	public static string SelectedPrefabName = "";
	
	// Generator Presets
	public static bool ShowGenPreset = false;
	public static bool NewGenPreset = false;
	public static bool EditGenPreset = false;
	public static string NewPresetName =  "";
	public static string NewPresetGender =  "Both";
		
	public static DK_GeneratorPresetLibrary _GeneratorPresetLibrary;
	public static DK_GeneratorPresetLibrary _GeneratorPresetLibrary2;
	public static float SpawnFrequencySlider = 0.005F;
	
	// modify
	public static bool UMAEdit;
	public static DKUMAData SelectedDKUMAData;
	public static Transform EditedModel;
	public static bool Arms;
	public static bool Legs;
	public static bool Torso;
	public static bool Head;
	public static bool Face;
	public static bool Chin;
	public static bool Cheeks;
	public static bool Mouth;
	public static bool Nose;
	public static bool Eyes;
	public static bool ChangeOverlay;
	public static bool AddSlot;

	// Colors
	// Presets	
	public static bool ShowSkinTone = false;
	public static bool ShowEyesColor = false;
	public static bool ShowTorsoWColor = false;
	public static bool ShowHairColor = false;
	public static bool ShowLegWColor = false;

	
	public static string _Type;
	
	Vector2 scroll;
	public static Vector2 scroll2;
	public static Vector2 scroll3;
	public static Vector2 scroll4;
	public static Vector2 scroll5;

	public static string Limit ;
	public static string _Name = "";
	public static string tmpWearWeight = "";
	
	// Plug Ins
	public static string PlugInSelected;
	public List<PlugInData> PlugInDataList = new List<PlugInData>();
	public static PlugInData _PlugInData;
	public static string PlugInPath;
	public static string[] aFilePaths;
	public static string[] aWinPaths;
	public static string FileName;
	public static string SelectedFile;
	
	public static bool ApplyToLib = true;

	public static bool AddRaces;
	public static bool AddSlots;
	public static bool AddOverlays;
	
	public static  DKUMA_Variables _DKUMA_Variables;

	public static GameObject DK_UMA;
	public static GameObject _UMA;

	#endregion Variables
	

	[MenuItem("UMA/DK Editor/DK UMA Editor")]
	[MenuItem("Window/DK Editors/DK UMA/DK UMA Editor")]

	public static void Init()
    {
   		// Get existing open window or if none, make a new one:
		DK_UMA_Editor Editorwindow = EditorWindow.GetWindow<DK_UMA_Editor> (false, "DK UMA Editor");
		Editorwindow.autoRepaintOnSceneChange = true;
		Editorwindow.Show ();
	}

	#region Open Windows
	public static void OpenConvWin(){
	//	GetWindow(typeof(Select_Converter), false, "Limitations");
	}
	public static void OpenColorPresetWin()
	{
		GetWindow(typeof(ColorPreset_Editor), false, "Color Presets");
	}
	public static void OpenChooseOverlayWin()
	{
		GetWindow(typeof(ChooseLinked_Editor), false, "Elements");
	}
	public static void OpenChooseSlotWin()
	{
		GetWindow(typeof(ChooseSlot_Win), false, "Choose Slot");
	}
	public static void OpenPlaceWin()
	{
		GetWindow(typeof(ChooseAnatomy_Win), false, "Anatomy places");
		ChooseAnatomy_Win.Action = "ChoosePlace";
	}
	public static void OpenAnatomy_Editor()
	{
		GetWindow(typeof(Anatomy_Editor), false, "Anatomy Editor");
	//	ChooseAnatomy_Win.Action = "ChoosePlace";
	}
	public static void OpenRaceEditor()
    {
		GetWindow(typeof(DK_UMA_Race_Editor), false, "DK UMA Race");
		DK_UMA_Race_Editor._RaceData = Selection.activeObject as DKRaceData;
	}
	public static void OpenRaceSelectEditor()
    {
		GetWindow(typeof(DK_UMA_RaceSelect_Editor), false, "Races List");
	}
	public static void OpenProcessWindow()
	{
		GetWindow(typeof(DKUMAProcessWindow), false, "Processing...");
	}
	public static void OpenLibrariesWindow()
	{
		GetWindow(typeof(ChangeLibrary), false, "UMA Libraries");
	}

	public static void OpenDeleteAsset(){
		GetWindow(typeof(DeleteAsset), false, "Deleting");
	}

	public static void OpenPlugInCreator(){
		GetWindow(typeof(NewPlugInWin), false, "New Plug-In");
	}
	public static void OpenAutoDetectWin(){
		GetWindow(typeof(AutoDetect_Editor), false, "Manager");
	}
	public static void OpenExpressionsWin(){
		GetWindow(typeof(Expression_Editor), false, "Expressions");
	}

	public static void OpenDKConvWin(){
		GetWindow(typeof(UMAConvAvatarWin), false, "Convert 2 DK");
	}
	public static void OpenRPGCharacterWin(){
	//	GetWindow(typeof(DK_RPG_UMA_Avatar_Win), false, "RPG Avatar");
	}
	public static void OpenVersionWin(){
		GetWindow(typeof(DKUMAVersionWin), false, "DK UMA Vers");
	}
	public static void OpenPBRTuto(){
		GetWindow(typeof(PBRTutoWin), false, "PBR Tutorial");
	}
	public static void OpenChooseStakedOverlay01(){
		GetWindow(typeof(ChooseStacked_Editor), false, "Stacked Ov");
		ChooseStacked_Editor.Action = "Stacked01";
	}
	public static void OpenChooseStakedOverlay02(){
		GetWindow(typeof(ChooseStacked_Editor), false, "Stacked Ov");
		ChooseStacked_Editor.Action = "Stacked02";
	}
	public static void OpenChooseDirtOverlay(){
		GetWindow(typeof(ChooseStacked_Editor), false, "Dirt Ov");
		ChooseStacked_Editor.Action = "Dirt";
	}
	public static void OpenForumTuto(){
		Application.OpenURL ("http://unity3d-dk-tools.boards.net/thread/85/documentation-table-content");
	}
	public static void OpenDocumentationLink(){
		Application.OpenURL ("http://alteredreality.wix.com/dk-uma#!/c4nz");
	}

	public static void OnEnable(){
		DetectAndAddDK.DetectAll();
	}
	public static void Awake(){
		DetectAndAddDK.DetectAll();
		// verify if the Editor is ready
	//	VerifyEditorInstallation ();
	}
	#endregion Open Windows


	UMA.OverlayDataAsset EyelashOverlay = null;
	public static void VerifyEditorInstallation (){
		Debug.Log ("Opening DK UMA Editor...");
		List<UMA.OverlayDataAsset> OverlaysList = UnityEngine.Object.FindObjectsOfType<UMA.OverlayDataAsset>().ToList();
		foreach ( UMA.OverlayDataAsset Ov in OverlaysList ){
			if ( Ov.name == "FemaleEyelash" && Ov.textureList.Contains (null) ){
				Debug.LogError ("UMA have to be fixed to work properly, open the DK UMA 'Elements Manager'...");
				OpenAutoDetectWin();
			//	EyelashOverlay = Ov;
			}
		}
	}

	void OnGUI () {
		#region fonts variables
		var bold = new GUIStyle ("label");
		var boldFold = new GUIStyle ("foldout");
		bold.fontStyle = FontStyle.Bold;
		bold.fontSize = 14;
		boldFold.fontStyle = FontStyle.Bold;
		var Slim = new GUIStyle ("label");
		Slim.fontStyle = FontStyle.Normal;
		Slim.fontSize = 10;	
		var style = new GUIStyle ("label");
		style.wordWrap = true;
		#endregion fonts variables

		GUILayout.Label ( "Dynamic Kit U.M.A. Editor", "toolbarbutton", GUILayout.ExpandWidth (true));
		if ( EditorVariables.DK_UMACrowd == null )
			EditorVariables.DK_UMACrowd = FindObjectOfType<DK_UMACrowd>();

		DK_UMA = GameObject.Find("DK_UMA");
		_UMA =  GameObject.Find("UMA");

		if ( DK_UMA != null ) {
			if ( _DKUMA_Variables == null && DK_UMA != null ) 
				_DKUMA_Variables = DK_UMA.GetComponent<DKUMA_Variables>();
			if ( _DKUMA_Variables == null ) _DKUMA_Variables =  DK_UMA.AddComponent<DKUMA_Variables>() as DKUMA_Variables;
			if (EditorVariables.DK_UMACrowd ){
				_DKUMA_Variables = DK_UMA.GetComponent<DKUMA_Variables>();
				if ( _DKUMA_Variables == null ) _DKUMA_Variables =  DK_UMA.GetComponent("DKUMA_Variables") as DKUMA_Variables;
				if ( _DKUMA_Variables == null ) _DKUMA_Variables =  DK_UMA.AddComponent<DKUMA_Variables>() as DKUMA_Variables;
				if ( EditorVariables.DK_UMACrowd.raceLibrary == null ) EditorVariables.DK_UMACrowd.raceLibrary = GameObject.Find( "DKRaceLibrary").GetComponent<DKRaceLibrary>();
				if ( EditorVariables.DK_UMACrowd.slotLibrary == null ) EditorVariables.DK_UMACrowd.slotLibrary = GameObject.Find( "DKSlotLibrary").GetComponent<DKSlotLibrary>();
				if ( EditorVariables.DK_UMACrowd.overlayLibrary == null ) EditorVariables.DK_UMACrowd.overlayLibrary = GameObject.Find( "DKOverlayLibrary").GetComponent<DKOverlayLibrary>();
			}

			_DKUMA_Variables.VerifyLibraries ();

			this.minSize = new Vector2(490, 610);

			if ( !EditorVariables.DK_UMACrowd  ) {
			//	DetectAndAddDK.DetectAll();
			}

			ObjToFind = GameObject.Find("NPC Models");
			if ( ObjToFind == null ) 
			{	var go = new GameObject();	go.name = "NPC Models";	ObjToFind = GameObject.Find("NPC Models");		}
			if ( ObjToFind != null )
			{
				EditorVariables.MFSelectedList = ObjToFind.transform;
				if ( EditorVariables.DKUMACustomizationObj != null ) EditorVariables._DKUMACustomization =  EditorVariables.DKUMACustomizationObj.GetComponent<DKUMACustomization>();			
			}
		}
		#region Menu

		#region Main Menu
		using (new Horizontal()) {
			GUI.color = Color.yellow;
			if (GUILayout.Button ( "Welcome", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				EditorOptions.DisplayWelcome();
			}
			if ( ShowPrepare == true ) GUI.color = Green;
			else GUI.color = Color.white;
			if (EditorVariables.DK_UMACrowd 
			    && GUILayout.Button ( "Prepare", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				DKUMACleanLibraries.CleanLibraries ();
				EditorOptions.DisplayPrepare ();
			}

			if ( DK_UMA != null && _UMA != null ){
				if ( showCreate == true ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( EditorVariables.DK_UMACrowd ){
				//   if ( EditorVariables.SlotsAnatomyLibraryObj && EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length != 0 ){
					   if ( GUILayout.Button ( "Create", "toolbarbutton", GUILayout.ExpandWidth (false))) 
						{
							DKUMACleanLibraries.CleanLibraries ();
							_DKUMA_Variables.PopulateLibraries ();
							EditorOptions.DisplayCreate ();
						}
				//	}
				}
				try {
					if ( showSetup == true ) GUI.color = Green;
					else GUI.color = Color.white;
					if (EditorVariables.DK_UMACrowd  
					    && GUILayout.Button ("Setup", "toolbarbutton", GUILayout.ExpandWidth (false))) {
						EditorOptions.DisplaySetup ();
					}
					if ( showCorrector == true ) GUI.color = Green;
					else GUI.color = Color.white;
					if (EditorVariables.DK_UMACrowd  
						&& GUILayout.Button ("Corrector", "toolbarbutton", GUILayout.ExpandWidth (false))) {
						EditorOptions.DisplayCorrector ();
					}
				} catch (System.ArgumentException ) {}
			}
			if ( showAbout == true ) GUI.color = Green;
			else GUI.color = Color.white;
			if (GUILayout.Button ( "About", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				EditorOptions.DisplayAbout ();

			}
			GUI.color = Color.white;
			GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
			if ( !Helper )  GUI.color = Color.white;
			else GUI.color = Green;
			if (GUILayout.Button ( "?", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				if ( Helper ) Helper = false;
				else Helper = true;
			}
		}
		#endregion Main Menu


		if ( DK_UMA != null && _UMA != null ){
			#region Add Scripts

			#endregion Add Scripts
		}
		#region Starting welcome
		if ( !showPlugIn && !showAbout && !ShowPrepare && !showCorrector 
			&& !showCreate && !showModify && !showSetup && !ShowLibraries && !ShowDKLibraries && !ShowGenPreset) 
		{
			DK_UMA_WelcomeTab.OnGUI ();
		}
		#endregion Starting welcome

		#region Install to scene
		if ( AssetDatabase.IsValidFolder ("Assets/DK Editors/DK_UMA_Content") == true ){
			if (( DK_UMA == null || _UMA == null ) && !showAbout ) {
			using (new HorizontalCentered()) {
				GUILayout.Label ( "Demo Scene", bold, GUILayout.ExpandWidth (true));
			}
			GUI.color = Color.white;
			using (new Horizontal()) {
				EditorGUILayout.HelpBox("Open directly the example scene to try the tool.", UnityEditor.MessageType.None);
				if (  GUILayout.Button("Open the Example scene", GUILayout.Width (170))){
					UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
					UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/DK Editors/DK_UMA_Editor/Scenes/Example Scene.unity");
				}
			}
			using (new HorizontalCentered()) {
				GUILayout.Label ( "Install DK UMA to the Scene", bold, GUILayout.ExpandWidth (true));
			}
			GUILayout.Space(15);
			if ( DK_UMA == null ) {
				EditorGUILayout.HelpBox("DK UMA is not installed to the current scene. Do you want to install it ?", UnityEditor.MessageType.Info);

				using (new Horizontal()) {
					GUILayout.Space(20);
					if (  GUILayout.Button ( "Yes, Install DK UMA.", GUILayout.ExpandWidth (true))){
						InstallDKUMA ();
					}
					if ( GUILayout.Button ( "No, Cancel", GUILayout.ExpandWidth (true))){
						CloseSelf();
					}
					GUILayout.Space(20);
				}
			}
			else {
				EditorGUILayout.HelpBox("DK UMA is now installed to the scene but UMA is not installed to the current scene. Do you want to install it ?", UnityEditor.MessageType.Info);
				using (new Horizontal()) {
					GUILayout.Space(20);
					if (  GUILayout.Button ( "Yes, Install UMA.", GUILayout.ExpandWidth (true))){
						InstallUMA ();
					}
					if ( GUILayout.Button ( "No, Cancel", GUILayout.ExpandWidth (true))){
						CloseSelf();
					}
					GUILayout.Space(20);
				}
			}
		}
		}
		else {
			GUI.color = Color.yellow ;
			EditorGUILayout.HelpBox("No DK UMA Content detected ! Import the Default Content or your own Packed DK UMA Content.", UnityEditor.MessageType.Warning);

			GUI.color = Color.white ;
			using (new Horizontal()) {
				if ( GUILayout.Button ( "Import DK UMA Default Content", GUILayout.ExpandWidth (true))) {
					if ( AssetDatabase.IsValidFolder ("Assets/UMA/Core/Extensions/DynamicCharacterSystem") == false )
						InstallDKUMAForUMA205Content ();
					else InstallDKUMAForUMA25Content ();
				}					
			}
		}
		#endregion Install to scene

		if ( DK_UMA != null && _UMA != null ){
			#region Prepare
			if (ShowPrepare) {
				OnSelectionChange();
				if (!ShowLibraries && !ShowDKLibraries && !ShowGenPreset) {

					DK_UMA_PrepEngineTab.OnGUI ();

					DK_UMA_PrepElemTab.OnGUI ();
				}
			
				#region DK Libraries
				if ( ShowDKLibraries ) DK_UMA_DKLibTab.OnGUI ();				
				#endregion DK Libraries
			
				#region Generator Presets
				if ( ShowGenPreset ) 
				{
					#region Edit Generator Preset
					if ( EditGenPreset )
					{
						// variables
						 DK_UMA = GameObject.Find("DK_UMA");
						if ( DK_UMA == null ) {
							var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
							DK_UMA = GameObject.Find("DK_UMA");
						}
						EditorVariables.GeneratorPresets = GameObject.Find("Generator Presets");
						if ( EditorVariables.GeneratorPresets == null ) 
						{
							EditorVariables.GeneratorPresets = (GameObject) Instantiate(Resources.Load("Generator Presets"), Vector3.zero, Quaternion.identity);
							EditorVariables.GeneratorPresets.name = "Generator Presets";
							EditorVariables.GeneratorPresets = GameObject.Find("Generator Presets");
							EditorVariables.GeneratorPresets.transform.parent = DK_UMA.transform;
						}
						
						#region Menu
						// Title
						GUILayout.Space(5);
						using (new Horizontal()) {
							GUI.color = Color.white ;
							GUILayout.Label("Edit Generator Presets", "toolbarbutton", GUILayout.ExpandWidth (true));
							GUI.color = Red;
							// actions
							if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
								ShowGenPreset = false;
								EditGenPreset = false;
								EditorVariables.PresetToEdit = null;
								
							}
						}
						#endregion Menu
						
						#region Presets List
						if ( !EditorVariables.PresetToEdit )
						{
							GUI.color = Color.yellow;
							if ( Helper ) GUILayout.TextField("Select a Generator Preset in the List to edit it. You also can define a Preset to be the Active one for the Generator process to use it during the Model Creation, click on a 'Active' button." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							GUILayout.Space(5);
							// list
							GUI.color = Color.white ;
							GUILayout.Label("Presets List", "toolbarbutton", GUILayout.ExpandWidth (true));
							GUILayout.Space(5);
							using (new Horizontal()) {
								GUI.color = Color.white ;
								GUILayout.Label("Preset Name", "toolbarbutton", GUILayout.Width (165));
								GUILayout.Label("Gender", "toolbarbutton", GUILayout.Width (60));
								GUILayout.Label("Activated", "toolbarbutton", GUILayout.Width (60));
								GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
							}
							using (new ScrollView(ref scroll)) 	{
								foreach ( Transform Preset in EditorVariables.GeneratorPresets.transform )
								{
									if ( Preset != null ) using (new Horizontal()) {
										// destroy it
										GUI.color = Red;
										if ( Preset != null && GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) {
											DestroyImmediate ( Preset.gameObject ); 
										}
										// Select it
										GUI.color = Color.white ;
										if ( Preset != null && GUILayout.Button ( Preset.name, "Label",GUILayout.Width (140))) {
											EditorVariables.PresetToEdit = Preset.gameObject;
											_GeneratorPresetLibrary =  EditorVariables.PresetToEdit.GetComponent<DK_GeneratorPresetLibrary>();
											NewPresetGender = _GeneratorPresetLibrary.ToGender;
											NewPresetName = _GeneratorPresetLibrary.PresetName;
											// merge lists
											EditorVariables.SlotsAnatomyLibraryObj = GameObject.Find("DK_SlotsAnatomyLibrary");
											EditorVariables._SlotsAnatomyLibrary =  EditorVariables.SlotsAnatomyLibraryObj.GetComponent<DK_SlotsAnatomyLibrary>();
											for(int i = 0; i < EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
												if (EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i] )
												{
													EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = false;
													for(int i2 = 0; i2 < _GeneratorPresetLibrary.dk_SlotsAnatomyElementList.Length; i2 ++){
														if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i] != null
															&& EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement != null 
														    && _GeneratorPresetLibrary.dk_SlotsAnatomyElementList[i2] != null
															&& _GeneratorPresetLibrary.dk_SlotsAnatomyElementList[i2].dk_SlotsAnatomyElement != null
															&& EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement 
															== _GeneratorPresetLibrary.dk_SlotsAnatomyElementList[i2].dk_SlotsAnatomyElement )
														{
															EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = true;
														}
													}
												}
											}
										}
										if ( Preset ) { _GeneratorPresetLibrary2 = Preset.GetComponent<DK_GeneratorPresetLibrary>();}
										if ( _GeneratorPresetLibrary2 != null )
										{
											// Gender
											GUI.color = Color.yellow ;
											GUILayout.Label(_GeneratorPresetLibrary2.ToGender, Slim, GUILayout.Width (55));
											// Active
											if ( _GeneratorPresetLibrary2.IsActivePreset ) GUI.color = Green;
											else GUI.color = Color.gray ;
											if ( GUILayout.Button ( "Active", GUILayout.Width (60))) {
												foreach ( Transform Presets in EditorVariables.GeneratorPresets.transform )
												{
													DK_GeneratorPresetLibrary _GeneratorPresetLibrarys = Presets.GetComponent<DK_GeneratorPresetLibrary>();
													_GeneratorPresetLibrarys.IsActivePreset = false;
												}
												if ( _GeneratorPresetLibrary2.IsActivePreset ) _GeneratorPresetLibrary2.IsActivePreset = false;
												else _GeneratorPresetLibrary2.IsActivePreset = true;
												Selection.activeGameObject = Preset.gameObject;
											}
										}
									}
								}
							}
						}
						#endregion Presets List
						
						#region Edit Preset
						if ( EditorVariables._SlotsAnatomyLibrary && EditorVariables.PresetToEdit != null ) {
							GUILayout.Space(5);
							using (new Horizontal()) {
								GUI.color = Color.white ;
								GUILayout.Label("Editing Preset '" +EditorVariables.PresetToEdit.name+"'", "toolbarbutton", GUILayout.ExpandWidth (true));
								GUI.color = Red;
								// actions
								if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
									EditorVariables.PresetToEdit = null;
									for(int i = 0; i < EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
										if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i] != null)
										EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = false;
									}
								}
							}
								Selection.activeGameObject = EditorVariables.PresetToEdit;	
							// general help	
							if ( Helper )	
							{
								GUI.color = Color.white;
								GUILayout.TextField("Here you can Edit the selected Generator Preset and manage its Slot Elements." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
								GUI.color = Color.yellow;
								GUILayout.TextField("Change the Preset's Name in the field bellow." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							}
							// Preset Name
							using (new Horizontal()) {
								GUI.color = Color.white;
								GUILayout.Label("Preset Name :", GUILayout.Width (100));
									if ( EditorVariables.UMAObj == null ) 
								{
										UMAObjName = EditorVariables.UMAObjDefault;
										EditorVariables.UMAObj =  GameObject.Find(UMAObjName);
								}
								if ( NewPresetName != "" ) GUI.color = Green;
								else GUI.color = Red;
								NewPresetName = GUILayout.TextField(NewPresetName, 50, GUILayout.ExpandWidth (true));
							}
							// Gender
							GUI.color = Color.yellow;
							if ( Helper ) GUILayout.TextField("You can specify a Gender or let it be usable by both (Both is recommended)." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							using (new Horizontal()) {
								if ( NewPresetGender == "Both" ) GUI.color = Green;
								else GUI.color = Color.gray;
								if ( GUILayout.Button ( "Both", GUILayout.ExpandWidth (true))) {
									NewPresetGender = "Both";
								}
								if ( NewPresetGender == "Female" ) GUI.color = Green;
								else GUI.color = Color.gray;
								if ( GUILayout.Button ( "Female", GUILayout.ExpandWidth (true))) {
									NewPresetGender = "Female";
								}
								if ( NewPresetGender == "Male" ) GUI.color = Green;
								else GUI.color = Color.gray;
								if ( GUILayout.Button ( "Male", GUILayout.ExpandWidth (true))) {
									NewPresetGender = "Male";
								}
							}
							if ( Helper )	
							{
								GUI.color = Color.yellow;
								GUILayout.TextField("Make your selections then click on the 'Apply to Preset' button." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							}
							// Apply to generator Preset	
							using (new Horizontal()) {
								GUI.color = Color.white ;
								if ( GUILayout.Button ( "Apply to Preset", GUILayout.ExpandWidth (true))) {
										EditorOptions.ApplyToGeneratorPreset ();
								}
							}
							if ( Helper )	
							{
								GUI.color = Color.white;
								GUILayout.TextField("The list is displaying both Elements of the Preset to Edit and from the Slots Anatomy Library. You can select or unselect any Anatomy Element." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							}
								#region List
							GUILayout.Space(5);
							using (new Horizontal()) {
								GUI.color = Color.white ;
								GUILayout.Label("Anatomy Slot Name", "toolbarbutton", GUILayout.Width (140));
								GUILayout.Label("Race", "toolbarbutton", GUILayout.Width (60));
								GUILayout.Label("Gender", "toolbarbutton", GUILayout.Width (55));
								GUILayout.Label("Select", "toolbarbutton", GUILayout.Width (55));
								GUILayout.Label("Overlay type", "toolbarbutton", GUILayout.Width (75));
								GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
							}
							using (new Horizontal()) {
								if ( EditorVariables._SlotsAnatomyLibrary != null ) using (new ScrollView(ref scroll)) 	{
									for(int i = 0; i < EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
										if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i] != null) using (new Horizontal(GUILayout.Width (80))) {
											// Element
											GUI.color = Color.white ;
											if ( GUILayout.Button ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName , Slim, GUILayout.Width (140))) {
											}
											// Race
											if ( EditorVariables._SlotsAnatomyLibrary && EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
												EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
											}
											DK_Race DK_Race;
											DK_Race = EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
											if ( DK_Race.Race.Count == 0) GUI.color = Red;
											if ( DK_Race.Race.Count == 0 && GUILayout.Button ( "No Race" , Slim, GUILayout.Width (50))) {
												
											}
											GUI.color = Green;
											if ( DK_Race.Race.Count == 1 && GUILayout.Button ( DK_Race.Race[0] , Slim, GUILayout.Width (50))) {
												
											}
											if ( DK_Race.Race.Count > 1 && GUILayout.Button ( "Multi" , Slim, GUILayout.Width (50))) {
												
											}	
											// Gender
											if ( DK_Race.Gender == "" ) GUI.color = Red;
											if ( DK_Race.Gender == "" ) GUILayout.Label ( "N" , "Button") ;
											GUI.color = Green;
											if ( DK_Race.Gender != "" && DK_Race.Gender == "Female" ) GUILayout.Label ( "Female" , Slim, GUILayout.Width (50) );
											if ( DK_Race.Gender != "" && DK_Race.Gender == "Male" ) GUILayout.Label ( "Male" , Slim, GUILayout.Width (50) );
											if ( DK_Race.Gender != "" && DK_Race.Gender == "Both" ) GUILayout.Label ( "Both" , Slim, GUILayout.Width (50) );
											// selected
											if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected == true )GUI.color = Green;
											else GUI.color = Color.gray;
											if ( GUILayout.Button ( "Select" , GUILayout.Width (50)) ) {
												if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected == false ) EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = true ;
												else EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = false;
											}
											// OverlayType
											if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
												EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
											}
											DK_Race = EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
											if ( DK_Race.OverlayType == "" ) GUI.color = Color.yellow;
											if ( DK_Race.OverlayType == "" && GUILayout.Button ( "No Overlay" , Slim, GUILayout.Width (70))) {
												
											}
											GUI.color = Green;
											if ( DK_Race.OverlayType != "" && GUILayout.Button ( DK_Race.OverlayType , Slim, GUILayout.Width (70))) {
												
											}	
										}
									}
								}
								Selection.activeGameObject = EditorVariables.PresetToEdit;
							}
							#endregion List
						}
						#endregion Edit Preset
					}
					#endregion Edit Generator Preset
					
					#region New Generator Preset
					if ( NewGenPreset )
					{
						#region Menu
						// title	
						GUILayout.Space(5);
						using (new Horizontal()) {
							GUI.color = Color.white ;
							GUILayout.Label("New Generator Preset", "toolbarbutton", GUILayout.ExpandWidth (true));
							GUI.color = Red;
							if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
								ShowGenPreset = false;
								NewGenPreset = false;
							}
						}
						
						// general help	
						GUI.color = Color.white;
						if ( Helper )	
						{
							GUILayout.TextField("Here you can Create a new Generator Preset and add the required Slot Elements to it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							GUI.color = Color.yellow;
							GUILayout.TextField("Write the new Preset's Name in the field bellow." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
						}
						// Preset Name
						using (new Horizontal()) {
							GUI.color = Color.white;
							GUILayout.Label("Preset Name :", GUILayout.Width (100));
								if ( EditorVariables.UMAObj == null ) 
							{
									UMAObjName = EditorVariables.UMAObjDefault;
									EditorVariables.UMAObj =  GameObject.Find(UMAObjName);
							}
							if ( NewPresetName != "" ) GUI.color = Green;
							else GUI.color = Red;
							NewPresetName = GUILayout.TextField(NewPresetName, 50, GUILayout.ExpandWidth (true));
						}
						// Gender
						GUI.color = Color.yellow;
						if ( Helper ) GUILayout.TextField("You can specify a Gender or let it be usable by both (Both is recommended)." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
						using (new Horizontal()) {
							if ( NewPresetGender == "Both" ) GUI.color = Green;
							else GUI.color = Color.gray;
							if ( GUILayout.Button ( "Both", GUILayout.ExpandWidth (true))) {
								NewPresetGender = "Both";
							}
							if ( NewPresetGender == "Female" ) GUI.color = Green;
							else GUI.color = Color.gray;
							if ( GUILayout.Button ( "Female", GUILayout.ExpandWidth (true))) {
								NewPresetGender = "Female";
							}
							if ( NewPresetGender == "Male" ) GUI.color = Green;
							else GUI.color = Color.gray;
							if ( GUILayout.Button ( "Male", GUILayout.ExpandWidth (true))) {
								NewPresetGender = "Male";
							}
						}
						GUI.color = Color.yellow;
						if ( Helper ) GUILayout.TextField("Select the desired Slots in the following List, the click on the 'Create Preset' button." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
						using (new Horizontal()) {
							// Create
							GUI.color = Color.white;
							if ( GUILayout.Button ( "Create Preset", GUILayout.ExpandWidth (true))) {
								EditorOptions.CreateGeneratorPreset ();
								NewGenPreset = false;
							}
							// Reset
							GUI.color = Color.yellow;
							if ( GUILayout.Button ( "Reset Preset", GUILayout.ExpandWidth (true))) {
									for(int i = 0; i < EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
									EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = false;
								}
							}
						}
						#endregion Menu
						
						#region List
						GUILayout.Space(5);
						using (new Horizontal()) {
							GUI.color = Color.white ;
							GUILayout.Label("Anatomy Slot Name", "toolbarbutton", GUILayout.Width (140));
							GUILayout.Label("Race", "toolbarbutton", GUILayout.Width (60));
							GUILayout.Label("Gender", "toolbarbutton", GUILayout.Width (55));
							GUILayout.Label("Select", "toolbarbutton", GUILayout.Width (55));
							GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
						}
						using (new Horizontal()) {
							using (new ScrollView(ref scroll)) 	{
								for(int i = 0; i < EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
									using (new Horizontal(GUILayout.Width (80))) {
										// Element
										GUI.color = Color.white ;
										if (GUILayout.Button ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName , Slim, GUILayout.Width (140))) {
										}
										// Race
										if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
											EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
										}
										DK_Race DK_Race;
										DK_Race = EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
										if ( DK_Race.Race.Count == 0 ) GUI.color = Red;
										if ( DK_Race.Race.Count == 0 && GUILayout.Button ( "No Race" , Slim, GUILayout.Width (50))) {
											
										}
										GUI.color = Green;
										if ( DK_Race.Race.Count == 1 && GUILayout.Button ( DK_Race.Race[0] , Slim, GUILayout.Width (50))) {
											
										}
										if ( DK_Race.Race.Count > 1 && GUILayout.Button ( "Multi" , Slim, GUILayout.Width (50))) {
											
										}
										// Gender
										if ( DK_Race.Gender == "" ) GUI.color = Red;
										if ( DK_Race.Gender == "" ) GUILayout.Label ( "N" , "Button") ;
										GUI.color = Green;
										if ( DK_Race.Gender != "" && DK_Race.Gender == "Female" ) GUILayout.Label ( "Female" , Slim, GUILayout.Width (50) );
										if ( DK_Race.Gender != "" && DK_Race.Gender == "Male" ) GUILayout.Label ( "Male" , Slim, GUILayout.Width (50) );
										if ( DK_Race.Gender != "" && DK_Race.Gender == "Both" ) GUILayout.Label ( "Both" , Slim, GUILayout.Width (50) );
										
										if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected == true )GUI.color = Green;
										else GUI.color = Color.gray;
										if ( GUILayout.Button ( "Select" , GUILayout.Width (50)) ) {
											if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected == false ) EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = true ;
											else EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = false;
										}
										// OverlayType
										if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
											EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
										}
										DK_Race = EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
										if ( DK_Race.OverlayType == "" ) GUI.color = Red;
										if ( DK_Race.OverlayType == "" && GUILayout.Button ( "No Race" , Slim, GUILayout.Width (50))) {
											
										}
										GUI.color = Green;
										if ( DK_Race.OverlayType != "" && GUILayout.Button ( DK_Race.OverlayType , Slim, GUILayout.Width (50))) {
											
										}
									}
								}
							}
							Selection.activeGameObject = EditorVariables.New_DK_GeneratorPresetLibraryObj;
						}
						#endregion List
					}
					#endregion New Generator Preset
				}
				#endregion Generator Presets
			}		
			#endregion Prepare

			#region Create
			if ( showCreate )  DK_UMA_CreateTab.OnGUI();
			#endregion Create
			 
			#region Create
			if ( showCorrector )  DK_UMA_CorrectorTab.OnGUI();
			#endregion Create
		
			#region UMA
			if ( Selection.activeGameObject
			    && (( Selection.activeGameObject.GetComponent("DKUMAData") as DKUMAData == true 
			    || (Selection.activeGameObject.transform.parent && Selection.activeGameObject.transform.parent.GetComponent("DKUMAData") as DKUMAData == true )
			    || (Selection.activeGameObject.transform.parent && Selection.activeGameObject.transform.parent.parent && Selection.activeGameObject.transform.parent.parent.GetComponent("DKUMAData") as DKUMAData == true)
			    || ( Selection.activeGameObject.GetComponentInChildren<DKUMAData>() as DKUMAData == true ) )
				     )
				&& showModify == true )
			{			
		
				#region Menu
				if ( EditedModel == null || Selection.activeGameObject == null ) using (new HorizontalCentered()) {
					GUI.color = Color.yellow ;
					GUILayout.Label("Please select a DK Element.", GUILayout.ExpandWidth (false));
				}
				GUI.color = new Color (0.8f, 1f, 0.8f, 1) ;

					if ( !ShowDKLibraries && EditedModel && Selection.activeGameObject  ){ 
						GUI.color = Color.white;
						GUILayout.Label ("Avatar Information", "toolbarbutton", GUILayout.ExpandWidth (true));
						using (new Horizontal()) {

					
							if ( !ChangeOverlay )using (new Horizontal()) {

								if (!AddSlot &&  GUILayout.Button("Rebuild", GUILayout.ExpandWidth (true))){
									DKUMAData _DKUMAData = EditedModel.GetComponentInChildren<DKUMAData>();
									DK_RPG_ReBuild _DK_RPG_ReBuild = _DKUMAData.transform.gameObject.AddComponent<DK_RPG_ReBuild>();
									_DK_RPG_ReBuild.RefreshOnly = true;
									_DK_RPG_ReBuild.Launch(_DKUMAData);
								}
							}
						}
					}

				
					if ( EditedModel && Selection.activeGameObject && EditedModel != Selection.activeGameObject.transform ){ 
				
					}
					if ( EditedModel && Selection.activeGameObject ) using (new Horizontal()) 
					{
						GUI.color = Color.white ;
						GUILayout.Label("Name :", GUILayout.ExpandWidth (false));
						if (EditorVariables.NewModelName != "" ) GUI.color = Green;
						else GUI.color = Red;
						if ( (EditorVariables.NewModelName == null || EditorVariables.NewModelName == "") ){
								EditorVariables.NewModelName = EditedModel.name;
						}
							EditorVariables.NewModelName = GUILayout.TextField(EditorVariables.NewModelName, 50, GUILayout.Width (200));
						if ( EditedModel != null & GUILayout.Button ( "Change", GUILayout.ExpandWidth (false))) {
							EditedModel.name = EditorVariables.NewModelName;
						}
					}
					if ( !ShowDKLibraries && EditedModel && Selection.activeGameObject /*&& EditedModel == Selection.activeGameObject.transform*/ ) using (new Horizontal()) {
					GUI.color = Color.yellow;
					using (new HorizontalCentered()) {
							GUILayout.Label( EditedModel.transform.GetComponent<DK_Model>().Race, GUILayout.ExpandWidth (true));
							GUILayout.Label( " - "+EditedModel.transform.GetComponent<DK_Model>().Gender, GUILayout.ExpandWidth (true));
					}
				}

				#endregion Menu

					if ( !ShowDKLibraries 
					    && EditedModel 
					    && Selection.activeGameObject 
					    ) {
					
					#region Components
					if ( showComp && Selection.activeGameObject ) {
						DKUMAData umaData = Selection.activeGameObject.GetComponentInChildren<DKUMAData>();
						
							GUI.color = Color.yellow;
							GUILayout.TextField("Beware : Modifying the elements from this window is not effective on a RPG Avatar. To Modify the elements of a RPG Avatar, use the RPG Avatar window.", 300, style, GUILayout.ExpandHeight (true),  GUILayout.ExpandWidth (true));
							GUI.color = Green;
						
							GUILayout.Space (5);
							if ( !ChangeOverlay && !AddSlot ) using (new Horizontal()) {
								GUI.color = Color.white;
								GUILayout.Label("Model's Elements List", "toolbarbutton", GUILayout.ExpandWidth (true));
								
							}
							#region Search
							using (new Horizontal()) {
								GUI.color = Color.white;
								GUILayout.Label("Search for :", GUILayout.Width (75));
								if (SearchString == null ) SearchString = "";
								SearchString = GUILayout.TextField(SearchString, 100, GUILayout.ExpandWidth (true));
							}
							#endregion Search
							if ( !AddSlot 
							    && !ChangeOverlay 
							    && Selection.activeGameObject != null ) using (new ScrollView(ref scroll)) 
						{
							GUILayout.Space (10);
							//	# if Editor
								try{
									if (umaData) for(int i = 0; i <  umaData.umaRecipe.slotDataList.Length; i++){
										if ( umaData.umaRecipe.slotDataList[i] != null && ( SearchString == "" || umaData.umaRecipe.slotDataList[i].slotName.ToLower().Contains(SearchString.ToLower()) == true )) {

											using (new Horizontal()) {
												GUI.color = Color.white;
												if (GUILayout.Button("["+i+"] "+umaData.umaRecipe.slotDataList[i].slotName, "toolbarbutton", GUILayout.Width (195))){

												}									
											}
										
											using (new Horizontal()) {										

												using (new Vertical()) {
												
													if ( i < umaData.umaRecipe.slotDataList.Length ) 
													for(int i1 = 0; i1 <  umaData.umaRecipe.slotDataList[i].overlayList.Count; i1++){
														using (new Horizontal()) {
															GUI.color = Color.white;
															if ( GUILayout.Button(umaData.umaRecipe.slotDataList[i].overlayList[i1].overlayName, Slim, GUILayout.Width (110))){
															
															}
															GUI.color = Red;
															if ( GUILayout.Button("X", "toolbarbutton", GUILayout.ExpandWidth (false))){
																umaData.umaRecipe.slotDataList[i].overlayList.Remove(umaData.umaRecipe.slotDataList[i].overlayList[i1]);
															}
															if ( i1 < umaData.umaRecipe.slotDataList[i].overlayList.Count ) 
																umaData.umaRecipe.slotDataList[i].overlayList[i1].color = EditorGUILayout.ColorField("", umaData.umaRecipe.slotDataList[i].overlayList[i1].color, GUILayout.Width (60));
														}
													}
												}
											}
										}
									}
								}catch(NullReferenceException){
									Debug.Log ( "umaRecipe.slotDataList is umpty" );
								}
							}
					}
					#endregion Components
				
					}
			}
			#endregion UMA
			
			#region Setup menu
			
		if ( showSetup ) {
			DK_UMA_SetupTab.OnGUI ();
		}
		#endregion Setup
		}
		#region About					
			
		if ( showAbout ) DK_UMA_AboutTab.OnGUI ();
		#endregion About
		#endregion Menu

	}

	public static DKDnaConverterBehaviour _DnaConverterBehaviour;
	public static void ResetSteps () {
		Step1 = false;Step2 = false;Step3 = false;Step4 = false;Step5 = false;Step6 = false;Step7 = false;

	}

	#region Add Element
	public static void AddToDK (System.Type type, UnityEngine.Object Element){

		#region Slot
		// verify the type
		if ( type.ToString() == "UMA.SlotDataAsset" ){
			// create the new DK element
			DKSlotData newSlot = new DKSlotData() ;
			//copy the values from the UMA element
			newSlot.name = (Element as UMA.SlotDataAsset).name ;
			newSlot.overlayScale = (Element as UMA.SlotDataAsset).overlayScale ;
			newSlot.slotName = (Element as UMA.SlotDataAsset).slotName ;

			// Add the correct slotDNA TO VERIFY IF IT'S NECESSARY

			// add the DK element to the DK...List
			_DKUMA_Variables.DKSlotsList.Add (newSlot);

			// Create the prefab
			System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/");
			// Gender select and create the asset
			// Male
			if ( newSlot.name.Contains("Male") == true && newSlot.name.Contains("Female") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Male/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Male/"+newSlot.name+".asset");
				newSlot._UMA = Element as  UMA.SlotDataAsset;
				// add the name to the DK...
				_DKUMA_Variables.LinkedUMASlotsList.Add (newSlot._UMA);
				if ( newSlot._UMA != null ){
					DKUMA_Variables.LinkedUMASlotData newData = new DKUMA_Variables.LinkedUMASlotData ();
					newData.Name = newSlot.name;
					newData.DKSlot = newSlot;
					newData.UMASlot = newSlot._UMA;
					newData.index = _DKUMA_Variables.LinkedUMASlotsList.Count-1;
					_DKUMA_Variables.LinkedUMASlotsList.Add(newSlot._UMA);
				}

				AssetDatabase.CreateAsset(newSlot, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newSlot;

			}
			// Female
			if ( newSlot.name.Contains("Female") && newSlot.name.Contains("Male") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Female/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Female/"+newSlot.name+".asset");
				newSlot._UMA = Element as  UMA.SlotDataAsset;
				AssetDatabase.CreateAsset(newSlot, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newSlot;
			}
			// Shared
			if ( newSlot.name.Contains("Female") == false && newSlot.name.Contains("Male") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Shared/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Shared/"+newSlot.name+".asset");
				newSlot._UMA = Element as  UMA.SlotDataAsset;
				AssetDatabase.CreateAsset(newSlot, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newSlot;
			}
		}
		#endregion Slot

		#region Overlay
		// verify the type
		if ( type.ToString() == "UMA.OverlayDataAsset" ){
			// test TODELETE

			// create the new DK element
			DKOverlayData newOverlay = ScriptableObject.CreateInstance<DKOverlayData>();
			//copy the values from the UMA element
			newOverlay.name = (Element as UMA.OverlayDataAsset).name ;

			foreach ( Texture texture in (Element as UMA.OverlayDataAsset).textureList ){
				Texture2D newTexture = texture as Texture2D;
				if ( newTexture != null ){
					try {
						newOverlay.textureList.ToList().Add (newTexture);
					}
					catch (ArgumentNullException ){
					}
				}
			}
			newOverlay.overlayName = (Element as UMA.OverlayDataAsset).overlayName ;
			newOverlay.rect = (Element as UMA.OverlayDataAsset).rect ;
			newOverlay.tags = (Element as UMA.OverlayDataAsset).tags ;

			// Add the correct slotDNA TO VERIFY IF IT'S NECESSARY

			// add the DK element to the DK...List
			_DKUMA_Variables.DKOverlaysList.Add (newOverlay);

			// add the name to the DK...NamesList
			_DKUMA_Variables.DKOverlaysNamesList.Add (newOverlay.name);

			// Create the prefab
			System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/");
			// Gender select and create the asset
			// Male
			if ( newOverlay.name.Contains("Male") == true && newOverlay.name.Contains("Female") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Male/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Male/"+newOverlay.name+".asset");
				newOverlay._UMA = Element as  UMA.OverlayDataAsset;
				AssetDatabase.CreateAsset(newOverlay, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newOverlay;
			}
			// Female
			if ( newOverlay.name.Contains("Female") && newOverlay.name.Contains("Male") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Female/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Female/"+newOverlay.name+".asset");
				newOverlay._UMA = Element as  UMA.OverlayDataAsset;
				AssetDatabase.CreateAsset(newOverlay, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newOverlay;
			}
			// Shared
			if ( newOverlay.name.Contains("Female") == false && newOverlay.name.Contains("Male") == false ){
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Shared/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Shared/"+newOverlay.name+".asset");
				newOverlay._UMA = Element as  UMA.OverlayDataAsset;
				AssetDatabase.CreateAsset(newOverlay, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newOverlay;

			}
			// TODO
			// Optionnal :
			// Move textures and other datas to the DK Elements Folder
			CorrectElements ();

		}
		#endregion Slot
	}
	#endregion Add Element

	public static void CorrectElements () {
		List<DKSlotData> tempList = new List<DKSlotData>();
		List<DKOverlayData> RemoveNullOvList = new List<DKOverlayData>();

	//	Debug.Log ("Verify Elements : The Linked Overlays, the Textures and the not required slots." );
		foreach ( DKSlotData slot in _DKUMA_Variables.DKSlotsList ){

			// verify if the slot is a real mesh slot
			if ( slot.name.Contains("CapsuleCollider") 
				|| slot.name.Contains("ExpressionSlot")
				|| slot.name.Contains("ForearmTwist")
				|| slot.name.Contains("Locomotion")) tempList.Add (slot as DKSlotData);

			// assign linked overlays
			if ( slot.overlayList.Count > 0 ){
				slot.LinkedOverlayList.AddRange(slot.overlayList);
				slot.overlayList.Clear();
				if ( slot.materialSample == null ) slot.materialSample = _DKUMA_Variables.DKSlotsList[0].materialSample;
				if ( slot != null ) EditorUtility.SetDirty (slot);
			}
			if ( slot.LinkedOverlayList.Count > 0 ){
				foreach ( DKOverlayData overlay in slot.LinkedOverlayList ){
					if ( overlay == null ) RemoveNullOvList.Add(overlay);
					if ( overlay != null && overlay.LinkedToSlot.Contains(slot) == false ) {
						overlay.LinkedToSlot.Add (slot);
						EditorUtility.SetDirty (overlay);
					}
				}
				foreach ( DKOverlayData overlay in RemoveNullOvList ){
					if ( slot.LinkedOverlayList.Contains (overlay) ) slot.LinkedOverlayList.Remove (overlay);
				}
			}

			// verify material
			if ( slot && slot._UMA && slot._UMA.material == null ) {
				// verify PBR Material
				if ( slot._UMA != null && slot._UMA.material == null ) {
					if ( _DKUMA_Variables._DK_UMA_GameSettings.MiscDefaultDatas.UMAMaterial != null ) {
						if ( slot.LinkedOverlayList.Count > 0 ) {
							// verify the first linked overlay UMA Mat
							if ( slot.LinkedOverlayList[0]._UMA.material != null ){
								// if ok, assign the UMA Mat to the slot
								EditorUtility.SetDirty (slot._UMA);
								slot._UMA.material = slot.LinkedOverlayList[0]._UMA.material;
								Debug.Log ("DK UMA : "+slot._UMA.name+" does not have a UMA Material Assigned, assigning  the one from its Overlay "+slot.LinkedOverlayList[0]._UMA.name+"." );
							}
							// assign default UMA Mat PBR
							else {
								EditorUtility.SetDirty (slot._UMA);
								EditorUtility.SetDirty (slot.LinkedOverlayList[0]._UMA);
								slot._UMA.material = _DKUMA_Variables._DK_UMA_GameSettings.MiscDefaultDatas.UMAMaterial;
								slot.LinkedOverlayList[0]._UMA.material = _DKUMA_Variables._DK_UMA_GameSettings.MiscDefaultDatas.UMAMaterial;
								Debug.Log ("DK UMA : "+slot._UMA.name+" and "+slot.LinkedOverlayList[0]._UMA.name+" do not have a UMA Material Assigned, assigning default ("+_DKUMA_Variables._DK_UMA_GameSettings.MiscDefaultDatas.UMAMaterial.name+")." );
							}
						}
						// if no Linked Overlay
						else {
							EditorUtility.SetDirty (slot._UMA);
							slot._UMA.material = _DKUMA_Variables._DK_UMA_GameSettings.MiscDefaultDatas.UMAMaterial;
							Debug.Log ("DK UMA : "+slot._UMA.name+" does not have a UMA Material Assigned and no Linked Overlay, assignind default ("+_DKUMA_Variables._DK_UMA_GameSettings.MiscDefaultDatas.UMAMaterial.name+")." );
						}
					}
					else Debug.LogError ( "DK UMA : The Default UMAMaterial of your project is not set. Search for the 'DK UMA Game Settings' in your project and assign a valid UMAMaterial to the variable : MiscDefaultDatas/UMAMaterial" ); 
				}
			}
		}

		// delete the not mesh slot
		foreach ( DKSlotData slot in tempList ){
			_DKUMA_Variables.DKSlotsList.Remove (slot);
		}

		// about Materials and textures
		foreach ( DKOverlayData _Overlay in _DKUMA_Variables.DKOverlaysList ){
			// verify PBR Material
			if (  _Overlay != null && _Overlay._UMA != null && _Overlay._UMA.material == null ) {
				_Overlay._UMA.material = _DKUMA_Variables.DefaultUmaMaterial;
			}


			List<Texture2D> tmpList = new List<Texture2D>();

			// reassign textures
			try {
				if ( _Overlay != null && _Overlay._UMA != null 
					&& _Overlay._UMA.textureList != null 
					&& _Overlay._UMA.textureList.Length > 0 ) {

					for (int i1 = 0; i1 < (_Overlay._UMA as UMA.OverlayDataAsset).textureList.Length; i1 ++) {
						Texture texture = (_Overlay._UMA as UMA.OverlayDataAsset).textureList[i1];

						if ( _Overlay != null && _Overlay.textureList != null && _Overlay.textureList.Length > 0 && _Overlay.textureList[0] == null  )
							EditorUtility.SetDirty (_Overlay);

						// find and add the texture
						if ( _Overlay != null && _Overlay.overlayName == "FemaleEyelash" ) {
							string[] Eyelashs = AssetDatabase.FindAssets ("Eyelash_diffuse", null);
							Texture _texture =  
								AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(Eyelashs[0])
									, typeof(Texture)) as Texture;
							(_Overlay._UMA as UMA.OverlayDataAsset).textureList[i1] = _texture;
							EditorUtility.SetDirty(_Overlay);
							if ( _Overlay._UMA != null ) EditorUtility.SetDirty(_Overlay._UMA);

						}
						Texture2D newTexture = texture as Texture2D;
						tmpList.Add (newTexture);

						_Overlay.textureList = tmpList.ToArray();

					}
				}
			}catch (NullReferenceException e){
				Debug.LogError ( _Overlay+" / "+_Overlay._UMA+" () : "+e );
			}
			// fix textures readable
			for (int i1 = 0; i1 < _Overlay.textureList.Length; i1 ++) {
				var obj = _Overlay.textureList [i1];
				if ( obj != null ){
					EditorUtility.SetDirty (obj);
					string file = AssetDatabase.GetAssetPath (obj);
					var importer = TextureImporter.GetAtPath (file) as TextureImporter;
					if (!importer.isReadable) {
						importer.isReadable = true;
						AssetDatabase.ImportAsset (file);
					}
				}
			}
		}
		AssetDatabase.SaveAssets ();
		Debug.Log ("Element is fixed" );
	}

	void CreateDNASliders( DKUMAData _DKUMAData ){
		EditorVariables.tmpDNAList.Clear();
		for (int i = 0; i <_DKUMAData.DNAList2.Count ; i++)
		{
			DKRaceData.DNAConverterData DNA = new DKRaceData.DNAConverterData();
			DNA.Name = _DKUMAData.DNAList2[i].Name;
			DNA.Value = _DKUMAData.DNAList2[i].Value;
			DNA.Part = _DKUMAData.DNAList2[i].Part;
			DNA.Part2 = _DKUMAData.DNAList2[i].Part2;
			EditorVariables.tmpDNAList.Add(DNA);
		}
	}

	void InstallDKUMA (){
		DKUMACorrectors.InstallDKUMA ();
	}

	void InstallUMA (){
		DKUMACorrectors.InstallUMA ();
	}

	/*
	void InstallDKUMA (){
		DK_UMA =  GameObject.Find("DK_UMA");
		GameObject ZeroPoint = GameObject.Find("ZeroPoint");
		if ( ZeroPoint == null )
		//	ZeroPoint = (GameObject)PrefabUtility.InstantiatePrefab (Resources.Load ("ZeroPoint"));
			ZeroPoint = (GameObject)Instantiate (Resources.Load ("ZeroPoint"));
		ZeroPoint.name = "ZeroPoint";
		if ( DK_UMA == null ) {
		//	DK_UMA = (GameObject)PrefabUtility.InstantiatePrefab (Resources.Load ("DK_UMA"));
			DK_UMA = (GameObject)Instantiate (Resources.Load ("DK_UMA"));
			#if UNITY_EDITOR
			PrefabUtility.DisconnectPrefabInstance (DK_UMA);
			#endif
			DK_UMA.name = "DK_UMA";

			Debug.Log ("DK UMA : DK_UMA object installed to the scene.");
		}
	}

	void InstallUMA (){
		_UMA =  GameObject.Find("UMA");
		if ( _UMA == null ) {
		//	_UMA = (GameObject)PrefabUtility.InstantiatePrefab (Resources.Load ("UMA205"));
			_UMA = (GameObject)Instantiate (Resources.Load ("UMA205"));
			#if UNITY_EDITOR
			PrefabUtility.DisconnectPrefabInstance (_UMA);
			#endif
			_UMA.name = "UMA";

			DKUMACleanLibraries.CleanLibraries ();
			_DKUMA_Variables.PopulateLibraries ();

			Debug.Log ("DK UMA : UMA object installed to the scene.");
		}
	}
*/

	void CloseSelf(){
		this.Close();
	}

	void InstallDKUMAForUMA205Content (){
		AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
			"Packages/Contents/DK UMA for UMA 2.0.5 Default Content.unitypackage", true);
	}

	void InstallDKUMAForUMA25Content (){
		AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
			"Packages/Contents/DK UMA for UMA 2.6 Default Content.unitypackage", true);
	}

	public static void OnSelectionChange() {
		EditorSelectionChange.OnSelectionChange ();
	}

	void OnInspectorUpdate (){
		Repaint ();
	}
}
