using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UMA;
using UMA.CharacterSystem;


public class AutoDetect_Editor : EditorWindow {
	#region Variables
	public static string Action = "";
	public DK_UMACrowd _DK_UMACrowd;

	public string SearchString = "";
	bool InResults = true;
//	string Done = "Not done";
//	string Done2 = "Not done";
//	string Done3 = "Not done";
//	string Done4 = "Not done";
	bool ShowUMA = true;
	bool ShowDK = true;
	bool ShowDefault = true;
	bool ShowNew = false;
	bool ShowLOD = true;
	bool ShowLinked = true;
	bool ShowLegacy = true;

	bool ShowListOptions = false;
	bool DetailedDKSlots = true;
	bool DetailedIcons = true;

	public bool ShowDKslot = true;
	public bool ShowDKoverlay = true;

	public bool Showslot = true;
	public bool Showoverlay = true;

	public bool ShowRaces = true;
	public bool ShowDKRaces = true;

	bool ShowLibs = true;

	bool UsePBR = true;

	bool QuickSetup = true;

	UMAContext _UMAContext;

	SlotLibrary _SlotLibrary;
	OverlayLibrary _OverlayLibrary;
	DKSlotLibrary _DKSlotLibrary;
	DKOverlayLibrary _DKOverlayLibrary;

	DKUMA_Variables _DKUMA_Variables;
	GameObject DK_UMA;
	GameObject _UMA;

	
	Vector2 scroll;
	Vector2 scroll2;
	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);

	bool Helper = false;

	DK_RPG_UMA _DK_RPG_UMA;

	DK_UMA_GameSettings GameSettings;
	#endregion Variables


	[MenuItem("UMA/DK Editor/Elements Manager")]
	[MenuItem("Window/DK Editors/DK UMA/Elements Manager")]
	public static void Init()
	{
		// Get existing open window or if none, make a new one:
		AutoDetect_Editor window = EditorWindow.GetWindow<AutoDetect_Editor> (false, "Elements Manager");
		window.autoRepaintOnSceneChange = true;
		window.Show ();
	}

	#region Open windows
	public static void OpenChooseLibWin()
	{
		GetWindow(typeof(ChangeLibrary), false, "UMA Libs");
	}
	public static void OpenRaceWin()
	{
		GetWindow(typeof(DK_UMA_RaceSelect_Editor), false, "Select Race");
	}
	public static void OpenEditorPrepareWin()
	{
		GetWindow(typeof(DK_UMA_Editor), false, "DK UMA Editor");
		// open prepare tab
		EditorOptions.DisplayPrepare ();
	}
	public static void OpenSelectSlotWin()
	{
		GetWindow(typeof(ChooseSlot_Win), false, "Choose Slot");
	}
	#endregion Open windows

	void OnEnable (){
		if ( DK_UMA == null )
			DK_UMA = GameObject.Find("DK_UMA");
		if ( _UMA == null )
			_UMA = GameObject.Find("_UMA");

		if ( DK_UMA != null ) {	
			if ( _DKUMA_Variables == null )
				_DKUMA_Variables = DK_UMA.GetComponent<DKUMA_Variables>();
			if ( GameSettings == null )
			GameSettings = DK_UMA.GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;
			if ( GameSettings != null ){
				_DKUMA_Variables.DefaultUmaMaterial = _DKUMA_Variables._DK_UMA_GameSettings.MiscDefaultDatas.UMAMaterial;
				_DKUMA_Variables.CleanLibraries ();
				_DKUMA_Variables.SearchAll ();
			}
		}
	}

	void OnGUI () {
		if ( DK_UMA == null )
			DK_UMA = GameObject.Find("DK_UMA");
		if ( _UMA == null )
			_UMA = GameObject.Find("_UMA");
		
		this.minSize = new Vector2(390, 500);

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

		using (new Horizontal()) {
			GUILayout.Label("DK Elements Manager", "toolbarbutton", GUILayout.ExpandWidth (true));
			if ( Helper ) GUI.color = Green;
			else GUI.color = Color.yellow;
			if ( GUILayout.Button ( "?", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				if ( Helper ) Helper = false;
				else Helper = true;
			}
		}
		if ( GameSettings != null ){
		try{	
		
		if ( _DK_UMACrowd == null )
			try{
			_DK_UMACrowd = GameObject.Find ("DKUMACrowd").GetComponent<DK_UMACrowd>();
			}catch(NullReferenceException){ Debug.LogError ( "DK UMA is not installed in your scene, please install DK UMA." ); this.Close(); }
		if ( _DKUMA_Variables == null )
			_DKUMA_Variables = DK_UMA.GetComponent<DKUMA_Variables>();

		#region Menu

				GUI.color = Color.white;
				if ( Helper ) EditorGUILayout.HelpBox("You must convert a UMA Element for DK UMA to be able to use it. " +
					"To do so you have to click on the 'Add' button of the UMA element in the lists of the UMA elements.", UnityEditor.MessageType.Warning);
				if ( Helper ) EditorGUILayout.HelpBox("DK UMA Premium only : You can use the ArchMage's 'Import UMA Content Editor' plug-in to import and set the UMA elements.", UnityEditor.MessageType.Info);
				
		//	if ( Helper ) GUILayout.TextField(" the Converter will search all your elements for UMA or DK, then it will be possible to Convert all the UMA elements to DK." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			// Libraries
			GUILayout.Space(5);
		if ( !Helper )using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label("UMA installation", "toolbarbutton", GUILayout.ExpandWidth (true));
				if ( ShowLibs ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
					if ( ShowLibs ) ShowLibs = false;
					else ShowLibs = true;
				}
			}

		if ( !Helper && ShowLibs ){
				// libraries variables
				_DKSlotLibrary = _DK_UMACrowd.slotLibrary;
				_DKOverlayLibrary = _DK_UMACrowd.overlayLibrary;

				_SlotLibrary = DKUMA_Variables._SlotLibrary;
				if ( DKUMA_Variables._SlotLibrary == null ){
					try{
						DKUMA_Variables._SlotLibrary = FindObjectOfType<DynamicSlotLibrary>();
					}catch(NullReferenceException){ /*Debug.LogError ( "UMA is not installed in your scene, please install UMA." );*/ }
				}

				_OverlayLibrary = DKUMA_Variables._OverlayLibrary;
				_DKUMA_Variables.ActiveOverlayLibrary = EditorVariables._OverlayLibrary;
				if ( DKUMA_Variables._OverlayLibrary == null ){
					try{
						DKUMA_Variables._OverlayLibrary = FindObjectOfType<DynamicOverlayLibrary>();
					}catch(NullReferenceException){/* Debug.LogError ( "UMA is not installed in your scene, please install UMA." );*/ }
				}

			if (!Helper ) using (new Horizontal()) {
				_DKUMA_Variables.ActiveSlotLibrary = EditorVariables._DKSlotLibrary;

				if ( _DKSlotLibrary == null ){
					_DKSlotLibrary = EditorVariables._DKSlotLibrary;
				}
				if ( _DKSlotLibrary ){
			/*	if ( ShowDK ) using (new Horizontal()) {
						GUI.color = Color.white;
						GUILayout.TextField( _DKSlotLibrary.name , 256, style, GUILayout.Width (110));
						if ( GUILayout.Button ( "Change", GUILayout.ExpandWidth (false))) {
							OpenChooseLibWin();
							ChangeLibrary.CurrentLibrary = _DKSlotLibrary.gameObject;

						}
					}*/
				}
				else {
				if ( ShowDK ) using (new Horizontal()) {
						GUILayout.Label("No DK Slots Library detected", GUILayout.ExpandWidth (true));
						if ( GUILayout.Button ( "Install Default", GUILayout.ExpandWidth (false))) {
							
						}
					}
				}
				if ( _DKOverlayLibrary == null ){
					_DKOverlayLibrary = EditorVariables._OverlayLibrary;
				}
				if ( !_DKOverlayLibrary ) {
					if ( ShowDK ) using (new Horizontal()) {
						GUILayout.Label("No DK Overlay Library detected", GUILayout.ExpandWidth (true));
						if ( GUILayout.Button ( "Install Default", GUILayout.ExpandWidth (false))) {
							
						}
					}
				}
			}				
				if (! Helper && FindObjectOfType<UMAGenerator>() != null && _DKSlotLibrary && _DKOverlayLibrary ) {
					using (new Horizontal()) {
						GUILayout.Label("UMA and DK UMA are installed.", GUILayout.ExpandWidth (true));
					}
				}
				if (! Helper ){	
					if ( FindObjectOfType<UMAGenerator>() == null ) {

						using (new Horizontal()) {
							GUI.color = Color.white ;
							GUILayout.Label("UMA not detected in the scene", GUILayout.ExpandWidth (true));
							GUI.color = Green ;
							if ( GUILayout.Button ( "Install UMA", GUILayout.ExpandWidth (true))) {
								InstallUMA();
							}
							if ( GUILayout.Button ( "Install UMA DCS (2.5)", GUILayout.ExpandWidth (true))) {
								InstallUMADCS();
							}
						}
					}
				}
			}
			// Options
		try {
				GUILayout.Space(5);
				using (new Horizontal()) {
					GUI.color = Color.white ;
					GUILayout.Label("Options to Detect assets", "toolbarbutton", GUILayout.ExpandWidth (true));

					if ( QuickSetup ) GUI.color = Green ;
					else GUI.color = Color.gray ;
					if ( GUILayout.Button ( "Quick", "toolbarbutton", GUILayout.ExpandWidth (false))) {
						QuickSetup = true;
					}
					if ( !QuickSetup ) GUI.color = Green ;
					else GUI.color = Color.gray ;
					if ( GUILayout.Button ( "Detailled", "toolbarbutton", GUILayout.ExpandWidth (false))) {
						QuickSetup = false;
					}
				}
			#region Actions
					GUI.color = Color.white ;
			if ( Helper ) GUILayout.TextField("1- Detect all the UMA and DK UMA assets using the Detect button." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			if ( Helper ) GUILayout.TextField("2- You can convert all the UMA assets for DK UMA to use them." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		}catch (ArgumentException){}
		
			if ( !QuickSetup ) {
				using (new Horizontal()) {
					GUI.color = Color.white ;
						if ( GUILayout.Button ( "1-Detect all Elements", GUILayout.ExpandWidth (true))) {
							Action = "Detecting";
						/*	SearchUMASlots ();
							SearchDKSlots ();
							SearchUMAOverlays ();
							SearchDKOverlays ();
							*/
						CorrectElements ();
						_DKUMA_Variables.SearchAll ();
						}
						GUI.color = Color.white ;
						if ( GUILayout.Button ( "2-Convert all", GUILayout.ExpandWidth (true))) {
							ImportAll ();
						}
						if ( UsePBR ) GUI.color = Green ;
						else GUI.color = Color.white ;
					/*	if ( GUILayout.Button ( "Use PBR", GUILayout.ExpandWidth (true))) {
						if ( UsePBR ) UsePBR = false;
						else UsePBR = true;
					}*/
				}
				if ( Helper ) GUILayout.TextField("3- You Can add all the new DK UMA elements to you current libraries, all of them or just the search result ones." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				if ( Helper ) GUILayout.TextField("4- You Can add all of them or just the search result ones." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				if ( Helper ) GUILayout.TextField("5- Clean your Libraries of any 'null' reference." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

				using (new Horizontal()) {
					GUI.color = Color.white ;
					GUILayout.Label("Add to Libraries", GUILayout.ExpandWidth (false));
					if ( GUILayout.Button ( "DK", GUILayout.Width (50))) {
						AddToLib ();
					}
					if ( _SlotLibrary && _OverlayLibrary && GUILayout.Button ( "UMA", GUILayout.Width (50))) {
						AddToUMALib ();
					}
					if ( InResults ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Result Only", GUILayout.ExpandWidth (true))) {
						if ( InResults ) InResults = false;
						else InResults = true ;
					}
					GUI.color = Color.white ;
					if ( GUILayout.Button ( "Clean", GUILayout.ExpandWidth (true))) {
						_DK_UMACrowd.CleanLibraries ();
					}
				}

				if ( Helper ) GUILayout.TextField("6- The original UMA element need to be linked to the DK UMA element for the incoming new version of DK UMA to be able to directly generate the basic UMA Avatars. It is automated but i let the button, just in case." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				if ( Helper ) GUILayout.TextField("7- DK UMA is now working with a brand new generator to store and find the DK elements. It is designed to be the root of the DK UMA RPG asset. It is automated but i let the button, just in case." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				if ( Helper ) GUILayout.TextField("8- An Auto Fix to move the linked Overlays of your DK Slots to the new correct lists." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

				using (new Horizontal()) {
					GUI.color = Color.white ;
					if ( GUILayout.Button ( "Link to UMA", GUILayout.ExpandWidth (true))) {
						LinkAll ();
					}
					if ( GUILayout.Button ( "Create RPG lists", GUILayout.ExpandWidth (true))) {
						DK_RPG_UMA_Generator _DK_RPG_UMA_Generator = DK_UMA.GetComponent<DK_RPG_UMA_Generator>();
						_DK_RPG_UMA_Generator.RPGPrepared = false;
						_DK_RPG_UMA_Generator.PopulateAllLists();
					}
					if ( GUILayout.Button ( "Fix Elements", GUILayout.ExpandWidth (true))) {
						CorrectElements ();
					}
				}
			}
			// quick setup
			else {
				GUI.color = Color.white ;
				try {
			//	GUILayout.TextField("Use this options to prepare your project." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				}catch (ArgumentException){}
				using (new Horizontal()) {
					if ( GUILayout.Button ( "1-Detect Elements", GUILayout.ExpandWidth (true))) {
						Action = "Detecting";
						CorrectElements ();
						_DKUMA_Variables.SearchAll ();
					}
					if ( GUILayout.Button ( "2-Fix Elements", GUILayout.ExpandWidth (true))) {
						_DKUMA_Variables.SearchAll ();
						CorrectElements ();
					}
					if ( GUILayout.Button ( "3-Add to Libraries", GUILayout.ExpandWidth (true))) {
						DK_RPG_UMA_Generator _DK_RPG_UMA_Generator = DK_UMA.GetComponent<DK_RPG_UMA_Generator>();
						_DK_RPG_UMA_Generator.RPGPrepared = false;
						_DK_RPG_UMA_Generator.PopulateAllLists();
						_DK_UMACrowd.CleanLibraries ();
						AddToLib ();
						AddToUMALib ();

						_DKUMA_Variables.PopulateLibraries ();
					}
				}
				//	EditorGUILayout.HelpBox("'Click on 'Add to Libraries' to add all the elements to the Databases of the project and to the races able to use them.", UnityEditor.MessageType.Info);
				//	EditorGUILayout.HelpBox("'Remember to click another time on 'Add to Libraries' when all your elements are setup and ready to add all the elements to the races able to use them.", UnityEditor.MessageType.Warning);

			}
				EditorGUILayout.HelpBox("'Click on 'Add to Libraries' to add all the elements to the Databases of the project and to the races able to use them.", UnityEditor.MessageType.Info);
				EditorGUILayout.HelpBox("'Remember to click another time on 'Add to Libraries' when all your elements are setup and ready to add all the elements to the races able to use them.", UnityEditor.MessageType.Warning);

			if ( Helper ) GUILayout.TextField("Close the helper to access to the lists of elements." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			GUI.color = Green ;
			if ( Helper && GUILayout.Button ( "Close Helper", GUILayout.ExpandWidth (true))) {
				Helper = false;
			}
	//	}
		#endregion Actions
		#endregion Menu

		#region Lists
		GUILayout.Space(5);


				if ( !Helper ) {
						//This draws a Line to seperate the Controls
						GUI.color = Color.white;
						GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

						using (new Horizontal()) {
							if ( GUILayout.Button( "Options displayed in the lists", "toolbarbutton", GUILayout.ExpandWidth (true))){
								ShowListOptions = !ShowListOptions;
							}
							if ( ShowListOptions ) GUI.color = Green;
							else GUI.color = Color.gray;
							if ( GUILayout.Button( "Show Options", "toolbarbutton", GUILayout.ExpandWidth (false))){
								ShowListOptions = !ShowListOptions;
							}
						}
						
					if ( ShowListOptions ) {
						using (new Horizontal()) {
							GUI.color = Color.white ;
							GUILayout.Label("Elements :", GUILayout.ExpandWidth (true));

							if (ShowLOD) GUI.color = Green ;
							else GUI.color = Color.gray ;
							if ( GUILayout.Button( "LOD", GUILayout.Width (60))){
								if (ShowLOD) ShowLOD = false;
								else ShowLOD = true;
							}
							if (ShowLinked) GUI.color = Green ;
							else GUI.color = Color.gray ;
							if ( GUILayout.Button( "Linked", GUILayout.Width (60))){
								if (ShowLinked) ShowLinked = false;
								else ShowLinked = true;
							}
							if (ShowLegacy) GUI.color = Green ;
							else GUI.color = Color.gray ;
							if ( GUILayout.Button( "Legacy", GUILayout.Width (60))){
								if ( ShowLegacy ) ShowLegacy = false;
								else ShowLegacy = true;
							}
							if (ShowNew) GUI.color = Green ;
							else GUI.color = Color.gray ;
							if ( GUILayout.Button( "New Only", GUILayout.Width (60))){
								if (ShowNew) ShowNew = false;
								else ShowNew = true;
							}
							if (ShowDefault) GUI.color = Green ;
							else GUI.color = Color.gray ;
							if ( GUILayout.Button( "Default", GUILayout.Width (60))){
								if ( ShowDefault ) ShowDefault = false;
								else ShowDefault = true;
							}
						}
						using (new Horizontal()) {
							GUI.color = Color.white ;
							GUILayout.Label("DK SLots :", GUILayout.ExpandWidth (false));
							if ( DetailedDKSlots ) GUI.color = Green ;
							else GUI.color = Color.gray ;
							if ( GUILayout.Button( "Detailed Info", GUILayout.ExpandWidth (true))){
								DetailedDKSlots = !DetailedDKSlots;
							}

							if ( DetailedIcons ) GUI.color = Green ;
							else GUI.color = Color.gray ;
							if ( GUILayout.Button( "Show Icons", GUILayout.ExpandWidth (true))){
								DetailedIcons = !DetailedIcons;
							}
						}
						GUI.color = Color.white ;
						GUILayout.Label("Legend of the Detailed Info :", GUILayout.ExpandWidth (true));
						GUI.color = Color.white ;
						EditorGUILayout.HelpBox("'Is Active' : Is the element active in the project ?.", UnityEditor.MessageType.Info);
						EditorGUILayout.HelpBox("'At Start' : This option is used by the 'Ingame Creator' plug-in. It is destined to be used for the Wear elements. " +
							"if it is unabled, this element will be listed, available and used in the 'Ingame Creator' scene by the user in the Gear panel.", UnityEditor.MessageType.Info);
						EditorGUILayout.HelpBox("'In Library' : Is the element stored in the DK UMA database of the project " +
							"and in the libraries of the scene ?", UnityEditor.MessageType.Info);
					}
		}
				if (!Helper ) {
					//This draws a Line to seperate the Controls
					GUI.color = Color.white;
				GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));
					GUI.color = Color.white ;
					GUILayout.Label("List of the UMA and DK UMA Elements", "toolbarbutton", GUILayout.ExpandWidth (true));
					using (new Horizontal()) {
			if (ShowDKslot) GUI.color = Color.cyan ;
			else GUI.color = Color.gray ;
				if ( GUILayout.Button( "DK slots", "toolbarbutton", GUILayout.ExpandWidth (true))){
				if (ShowDKslot){
				//	ShowDK = false;
					ShowDKslot = false;
				}
				else {
				//	ShowDK = true;
					ShowDKslot = true;
				}
			}
			if (Showslot) GUI.color = Color.cyan ;
			else GUI.color = Color.gray ;
				if ( GUILayout.Button( "UMA slots", "toolbarbutton", GUILayout.ExpandWidth (true))){
				if (Showslot){ 
				//	ShowUMA = false; 
					Showslot = false;
				}
				else {
				//	ShowUMA = true;
					Showslot = true;
				}
			}
			if (ShowDKoverlay) GUI.color = Color.cyan ;
			else GUI.color = Color.gray ;
				if ( GUILayout.Button( "DK overlays", "toolbarbutton", GUILayout.ExpandWidth (true))){
				if (ShowDKoverlay ){ ShowDKoverlay = false; }
				else { ShowDKoverlay = true; }
			}
			if (Showoverlay) GUI.color = Color.cyan ;
			else GUI.color = Color.gray ;
				if ( GUILayout.Button( "UMA overlays", "toolbarbutton", GUILayout.ExpandWidth (true))){
				if (Showoverlay){ 
				//	ShowUMA = false; 
					Showoverlay = false;
				}
				else {
				//	ShowUMA = true;
					Showoverlay = true;
				}
			}
					if (ShowDKRaces) GUI.color = Color.cyan ;
					else GUI.color = Color.gray ;
					if ( GUILayout.Button( "DK Races", "toolbarbutton", GUILayout.ExpandWidth (true))){
						if (ShowDKRaces ){ ShowDKRaces = false; }
						else { ShowDKRaces = true; }
					}
					if (ShowRaces) GUI.color = Color.cyan ;
					else GUI.color = Color.gray ;
					if ( GUILayout.Button( "UMA Races", "toolbarbutton", GUILayout.ExpandWidth (true))){
						if (ShowRaces){ 
							ShowRaces = false;
						}
						else {
							ShowRaces = true;
						}
					}
					}
		}

		/*	#region Helper
		// Helper
		GUI.color = Color.white ;
		if ( Helper ) GUILayout.TextField("Click on the name to select an Element." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		GUI.color = Green ;
		if ( Helper ) GUILayout.TextField("The Green elements are installed in DK UMA, click on the 'X' button at the end of the line to delete it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

		GUI.color = Red ;
		if ( Helper ) GUILayout.TextField("The Red elements are not installed in DK UMA, click on the 'Add' button at the end of the line to correct it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

		#endregion Helper
*/
				#region Search
				if ( !Helper ) using (new Horizontal()) {
					GUI.color = Color.white;
					GUILayout.Label("Search for :", GUILayout.ExpandWidth (false));
					SearchString = GUILayout.TextField(SearchString, 100, GUILayout.ExpandWidth (true));
					
				}
				if ( ShowRaces || ShowDKRaces ){
					GUI.color = Color.white ;
					EditorGUILayout.HelpBox("Premium or Race Editor only : Use the Race Editor tool to modify the UMA or DK UMA races.", UnityEditor.MessageType.None);
				}
				#endregion Search

		if ( !Helper ) using (new Horizontal()) {
			using (new ScrollView(ref scroll)) 	{
				if (Showslot) 
				using (new Horizontal()) {
					// UMA Slots
					GUI.color = Color.cyan ;
					GUILayout.Label("UMA Slots ("+_DKUMA_Variables.UMASlotsList.Count.ToString()+")", "toolbarbutton", GUILayout.ExpandWidth (true));
					if (Showslot) GUI.color = Green ;
					else GUI.color = Color.gray ;
					if ( GUILayout.Button( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))){
						if (Showslot) Showslot = false;
						else Showslot = true;
					}
				}
				if ( ShowUMA && Showslot && _DKUMA_Variables.UMASlotsList.Count > 0 )
				{
					#region Helper
					// Helper
					GUI.color = Color.white ;
					if ( Helper ) GUILayout.TextField("Click on the name to select an Element." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					GUI.color = Green ;
					if ( Helper ) GUILayout.TextField("The Green elements are installed in DK UMA." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					
					GUI.color = Red ;
					if ( Helper ) GUILayout.TextField("The Red elements are not installed in DK UMA, click on the 'Add' button to install it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					
					#endregion Helper
					for (int i = 0; i < _DKUMA_Variables.UMASlotsList.Count ; i++)
					{
						// preview image
						
								if ( _DKUMA_Variables.UMASlotsList[i] != null
										&& ( !ShowNew ||( ShowNew && _DKUMA_Variables.LinkedUMASlotsList.Contains(_DKUMA_Variables.UMASlotsList[i]) == false ))
						   			&& _DKUMA_Variables.UMASlotsList[i].name != "DefaultSlotType"
						    		&& ( SearchString == "" 
						    		|| _DKUMA_Variables.UMASlotsList[i].name.ToLower().Contains(SearchString.ToLower()) ))
							
									using (new Horizontal()) {
						/*	using (new Vertical()) {
								Texture2D Preview;
								string path = AssetDatabase.GetAssetPath (_DKUMA_Variables.UMASlotsList[i]);
								path = path.Replace (_DKUMA_Variables.UMASlotsList[i].name+".asset", "");
								Preview = AssetDatabase.LoadAssetAtPath(path+"Preview-"+_DKUMA_Variables.UMASlotsList[i].name+".asset", typeof(Texture2D) ) as Texture2D;
								
									// trying to modify hideflags
								if ( Preview )  Preview.hideFlags = HideFlags.None;
							//	if ( i == 1 ) Debug.Log (Preview.hideFlags.ToString());
								if (Preview == null ){
									path = AssetDatabase.GetAssetPath (_DKUMA_Variables.UMASlotsList[i]);
									path = path.Replace (_DKUMA_Variables.UMASlotsList[i].name+".asset", "");
									Preview = AssetDatabase.LoadAssetAtPath(path+"Preview-"+_DKUMA_Variables.UMASlotsList[i].name+".asset", typeof(Texture2D) ) as Texture2D;
								}
								if ( Preview != null ) GUI.color = Color.white ;
											else GUI.color = Color.black;
								if ( GUILayout.Button( Preview ,GUILayout.Width (75), GUILayout.Height (75))){
									Selection.activeObject = _DKUMA_Variables.UMASlotsList[i];
								}
							}*/

									using (new Vertical()) {
										GUILayout.Space(5);
										using (new Horizontal()) {
														GUILayout.Space(25);
														bool Linked = false;

													if (_DKUMA_Variables.LinkedUMASlotsList.Contains(_DKUMA_Variables.UMASlotsList[i])) Linked = true;
														
													if ( Linked ) GUI.color = Green ;
													else GUI.color = Red ;
													if ( Selection.activeObject == _DKUMA_Variables.UMASlotsList[i] ) GUI.color = Color.yellow ;
													if ( GUILayout.Button( _DKUMA_Variables.UMASlotsList[i].name, "toolbarbutton", GUILayout.Width (225))){
														Selection.activeObject = _DKUMA_Variables.UMASlotsList[i];
													}
													GUI.color = Green ;
													if ( Linked == false
														&& GUILayout.Button( "Add", "toolbarbutton", GUILayout.Width (80))){
														AddToDK(_DKUMA_Variables.UMASlotsList[i].GetType(), _DKUMA_Variables.UMASlotsList[i] as UnityEngine.Object);
													}

													if ( Linked  == true
														&& GUILayout.Button( "Select DK", "toolbarbutton", GUILayout.Width (80)))
													{
														for (int i1 = 0; i1 < _DKUMA_Variables.DKSlotsList.Count ; i1++)
														{
															if ( _DKUMA_Variables.DKSlotsList[i1]._UMA == _DKUMA_Variables.UMASlotsList[i] ){
																Selection.activeObject = _DKUMA_Variables.DKSlotsList[i1];
															}
														}
													}
													GUI.color = Color.gray ;
													if ( GUILayout.Button( "X", "toolbarbutton", GUILayout.ExpandWidth (false))){
														
													}
												}

									if ( _DKUMA_Variables.UMASlotsList[i] != null 
									    && _DKUMA_Variables.UMASlotsList[i].name != "DefaultSlotType"
									    && ( SearchString == "" 
									    || _DKUMA_Variables.UMASlotsList[i].name.ToLower().Contains(SearchString.ToLower()) ))
																	
									try{
										using (new Horizontal()) {
														GUILayout.Space(25);
												if ( GameSettings._GameLibraries.UmaSlotsLibrary.Contains (_DKUMA_Variables.UMASlotsList[i]) ) GUI.color = Green ;
										else GUI.color = Color.gray ;
										if ( GUILayout.Button( "In UMA Library", "toolbarbutton", GUILayout.Width (320)))
										{

													if (GameSettings._GameLibraries.UmaSlotsLibrary.Contains (_DKUMA_Variables.UMASlotsList[i]) == false ) {
														GameSettings._GameLibraries.UmaSlotsLibrary.Add ( _DKUMA_Variables.UMASlotsList[i]);
														EditorUtility.SetDirty (GameSettings);
													AssetDatabase.SaveAssets ();
											}
										}
									}
								}catch(ArgumentException){}
												//This draws a Line to seperate the Controls
												GUI.color = Color.white;
												GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));
							}
						}
									if ( _DKUMA_Variables.UMASlotsList[i] == null ) _DKUMA_Variables.UMASlotsList.Remove (_DKUMA_Variables.UMASlotsList[i]);
								/*	try {
										if ( _DKUMA_Variables.UMASlotsList[i] != null && ( SearchString == "" 
						                                 || _DKUMA_Variables.UMASlotsList[i].name.ToLower().Contains(SearchString.ToLower()) )
										&& ( !ShowNew ||( ShowNew && _DKUMA_Variables.DKSlotsNamesList.Contains(_DKUMA_Variables.UMASlotsList[i].name) == false )))
											GUILayout.Space(10);
									}catch(ArgumentOutOfRangeException){}
*/

					}
				}
				
						// DK Slots
				if (ShowDKslot)
				using (new Horizontal()) {
					GUI.color = Color.cyan ;
					GUILayout.Label("DK Slots ("+_DKUMA_Variables.DKSlotsList.Count.ToString()+")", "toolbarbutton", GUILayout.ExpandWidth (true));
					if (ShowDKslot) GUI.color = Green ;
					else GUI.color = Color.gray ;
					if ( GUILayout.Button( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))){
						if (ShowDKslot) ShowDKslot = false;
						else ShowDKslot = true;
					}
				}

				#region Helper
				// Helper
				GUI.color = Color.white ;
				if ( Helper ) GUILayout.TextField("Click on the name to select an Element and modify it using the Prepare tab of the DK Editor, or using the Auto Detect (in the Prepare tab)." , 500, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUI.color = Green ;
				if ( Helper ) GUILayout.TextField("The Green elements has been setup for DK UMA." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUI.color = Red ;
				if ( Helper ) GUILayout.TextField("The Red elements are not ready for DK UMA, click on the 'Setup' button at the end of the line to ..." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				
				#endregion Helper
				if ( ShowDK && ShowDKslot && _DKUMA_Variables.DKSlotsList.Count > 0 ){
					for (int i = 0; i < _DKUMA_Variables.DKSlotsList.Count ; i++)
					{
						DKSlotData slot = _DKUMA_Variables.DKSlotsList[i];
						if ( slot != null 
						    && ( SearchString == "" || slot.name.ToLower().Contains(SearchString.ToLower() ) )
							    && ( ( ShowLOD == false && slot._LOD.MasterLOD == false ) || ShowLOD == true )
							    && ( ( ShowLegacy == false && slot._LegacyData.HasLegacy == false ) || ShowLegacy == true )
							    && ( ( ShowDefault == false && slot.Default == false ) || ShowDefault == true )
						    && ( ( ShowNew && ( slot.Place == null && slot._LegacyData.IsLegacy == false )) || !ShowNew )
								)
									
						using (new Horizontal()) {
											if ( DetailedIcons ) { 
												// Slot Preview
												using (new Vertical()) {
													Texture2D Preview = null;
													Sprite _Sprite = null;
													DKSlotData DKSlot;
													if (Preview == null ){
														if ( _DKUMA_Variables.DKSlotsList[i]._LOD.IsLOD0 == true  ) DKSlot = _DKUMA_Variables.DKSlotsList[i];
														else DKSlot = _DKUMA_Variables.DKSlotsList[i]._LOD.MasterLOD;

														string path = AssetDatabase.GetAssetPath (DKSlot);
														path = path.Replace (DKSlot.name+".asset", "");
														Preview = AssetDatabase.LoadAssetAtPath(path+"Preview-"+DKSlot.name+".asset", typeof(Texture2D) ) as Texture2D;
														// load sprite
														if ( Preview == null ){
															_Sprite = AssetDatabase.LoadAssetAtPath(path+"Sprite-"+DKSlot.name+".png", typeof(Sprite) ) as Sprite;
														}
													}
													if ( Preview != null ){
														GUI.color = Color.white ;
														if ( GUILayout.Button( Preview ,GUILayout.Width (70), GUILayout.Height (70))){								
														}
													}
													else if ( _Sprite != null ){
														GUI.color = Color.white ;
														if ( GUILayout.Button( _Sprite.texture ,GUILayout.Width (70), GUILayout.Height (70))){								
														}
													}
													else {
														GUI.color = Color.black ;
														if ( GUILayout.Button( Preview ,GUILayout.Width (70), GUILayout.Height (70))){								
														}
													}
													GUILayout.Space(10);
												}									
											}

									//	GUILayout.Space(10);
							using (new Vertical()) {
								GUILayout.Space(3);
								using (new Horizontal()) {
									if ( _DKUMA_Variables.DKSlotsList[i].Place != null ) GUI.color = Green ;
									else GUI.color = Red ;
										if ( _DKUMA_Variables.DKSlotsList[i]._LOD.MasterLOD != null ) GUI.color = Color.gray ;
									if ( Selection.activeObject == _DKUMA_Variables.DKSlotsList[i] ) GUI.color = Color.yellow ;
									if ( _DKUMA_Variables.DKSlotsList[i] != null && GUILayout.Button( _DKUMA_Variables.DKSlotsList[i].name, "toolbarbutton", GUILayout.Width (235))){
										Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
									}
										GUI.color = Color.cyan;
										if ( GUILayout.Button( "Edit", "toolbarbutton", GUILayout.ExpandWidth (false))){
											Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
											OpenEditorPrepareWin();										
										}
									GUI.color = Red ;
									if ( GUILayout.Button( "X", "toolbarbutton", GUILayout.ExpandWidth (false))){
										RemoveFromDK(_DKUMA_Variables.DKSlotsList[i].GetType(), _DKUMA_Variables.DKSlotsList[i] as UnityEngine.Object);
									}
								}
								using (new Horizontal()) {
									if ( _DKUMA_Variables.DKSlotsList[i].Active == true ) GUI.color = Green ;
									else GUI.color = Color.gray ;
									if ( GUILayout.Button( "Is Active", "toolbarbutton", GUILayout.Width (80))){
										if ( _DKUMA_Variables.DKSlotsList[i].Active == true ) _DKUMA_Variables.DKSlotsList[i].Active = false ;
										else _DKUMA_Variables.DKSlotsList[i].Active = true ;
										EditorUtility.SetDirty (_DKUMA_Variables.DKSlotsList[i]);
										AssetDatabase.SaveAssets ();
									}
									if ( _DKUMA_Variables.DKSlotsList[i].AvailableAtStart == true ) GUI.color = Green ;
									else GUI.color = Color.gray ;
									if ( GUILayout.Button( "At Start", "toolbarbutton", GUILayout.Width (80))){
										if ( _DKUMA_Variables.DKSlotsList[i].AvailableAtStart == true ) _DKUMA_Variables.DKSlotsList[i].AvailableAtStart = false ;
										else _DKUMA_Variables.DKSlotsList[i].AvailableAtStart = true ;
										EditorUtility.SetDirty (_DKUMA_Variables.DKSlotsList[i]);
										AssetDatabase.SaveAssets ();
									}
										if ( GameSettings._GameLibraries.DkSlotsLibrary.Contains (_DKUMA_Variables.DKSlotsList[i]) ) GUI.color = Green ;
									else GUI.color = Color.gray ;
									if ( GUILayout.Button( "In Library", "toolbarbutton", GUILayout.Width (120))){
											if (GameSettings._GameLibraries.DkSlotsLibrary.Contains (_DKUMA_Variables.DKSlotsList[i]) == false ) {
												GameSettings._GameLibraries.DkSlotsLibrary.Add (_DKUMA_Variables.DKSlotsList[i]);
												EditorUtility.SetDirty (GameSettings);
											AssetDatabase.SaveAssets ();
										}
										else {

										}
									}
								}
												if ( DetailedDKSlots ) {
								if ( _DKUMA_Variables.DKSlotsList[i]._UMA == null  ) {
									GUI.color = Red ;
									if ( GUILayout.Button( "The UMA Element is not in your project", "toolbarbutton", GUILayout.Width (240))){
															DKUMACorrectors.LinkToUMAslot ( _DKUMA_Variables.DKSlotsList[i], _DKUMA_Variables );
									}
								//	GUILayout.Label("The DK Element is skipped.", Slim, GUILayout.ExpandWidth (true));

									}										
								//	if ( _DKUMA_Variables.DKSlotsList[i]._LOD.MasterLOD == null 
								//	    && ( _DKUMA_Variables.DKSlotsList[i].Race.Count == 0
								//	    || _DKUMA_Variables.DKSlotsList[i].Gender == "" ) )
									
											using (new Horizontal()) {
												if ( _DKUMA_Variables.DKSlotsList[i]._LOD.IsLOD0 == true  ) {
													if ( _DKUMA_Variables.DKSlotsList[i].Race.Count == 0 ) GUI.color = Red ;
													else GUI.color = Color.white;
													if ( _DKUMA_Variables.DKSlotsList[i].Race.Count == 0 ){
														if ( GUILayout.Button( "No Race", "toolbarbutton", GUILayout.Width (75))){
															Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
															OpenRaceWin ();
														}
													}
												/*	else if ( GUILayout.Button( "Race(s)", "toolbarbutton", GUILayout.Width (75))){
														Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
														OpenRaceWin ();
													}*/
													if ( _DKUMA_Variables.DKSlotsList[i].Gender == "" ) GUI.color = Red ;
													else GUI.color = Color.white;
													if ( _DKUMA_Variables.DKSlotsList[i].Gender == "" ){
														if ( GUILayout.Button( "No Gender", "toolbarbutton", GUILayout.Width (60))){
															Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
															OpenEditorPrepareWin ();
														}
													}
													else if ( GUILayout.Button( _DKUMA_Variables.DKSlotsList[i].Gender, "toolbarbutton", GUILayout.Width (60))){
														Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
														OpenEditorPrepareWin ();
													}
													if ( _DKUMA_Variables.DKSlotsList[i].Place == null ) GUI.color = Red ;
													else GUI.color = Color.white;
													if ( _DKUMA_Variables.DKSlotsList[i].Place == null ){
														if ( GUILayout.Button( "No Place", "toolbarbutton", GUILayout.Width (75))){
															Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
															OpenEditorPrepareWin ();
														}
													}
													else if ( GUILayout.Button( _DKUMA_Variables.DKSlotsList[i].Place.name, "toolbarbutton", GUILayout.Width (75))){
														Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
														OpenEditorPrepareWin ();
													}
													if ( _DKUMA_Variables.DKSlotsList[i].OverlayType != "" ){
														if ( GUILayout.Button( _DKUMA_Variables.DKSlotsList[i].OverlayType, "toolbarbutton", GUILayout.Width (75))){
															Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
															OpenEditorPrepareWin ();
														}
													}
													if ( _DKUMA_Variables.DKSlotsList[i].LinkedOverlayList.Count > 0 ){
														if ( GUILayout.Button( "Overlay(s)", "toolbarbutton", GUILayout.Width (70))){
															Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
															OpenEditorPrepareWin ();
														}
													}
													else{
														if ( _DKUMA_Variables.DKSlotsList[i].OverlayType != "Flesh"
															&& _DKUMA_Variables.DKSlotsList[i].OverlayType != "Face"
															&& _DKUMA_Variables.DKSlotsList[i].OverlayType != "Eyes"
																&& _DKUMA_Variables.DKSlotsList[i].Place != null
															&& _DKUMA_Variables.DKSlotsList[i].Place.name != "InnerMouth" )  GUI.color = Red ;
														if ( GUILayout.Button( "No Overlays", "toolbarbutton", GUILayout.Width (70))){
															Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
															OpenEditorPrepareWin ();
														}
													}
												}
											}
									if ( _DKUMA_Variables.DKSlotsList[i]._LOD.IsLOD0 == true  ) {
										using (new Horizontal()) {
											GUI.color = Green ;
											GUILayout.Label("Is LOD 0", Slim, GUILayout.ExpandWidth (false));
											// LOD1
											if ( Selection.activeObject == _DKUMA_Variables.DKSlotsList[i]._LOD.LOD1 ) GUI.color = Color.yellow;
											else GUI.color = Color.white;
											// if LOD1 is present
											if ( _DKUMA_Variables.DKSlotsList[i]._LOD.LOD1 != null ){
												if ( GUILayout.Button( _DKUMA_Variables.DKSlotsList[i]._LOD.LOD1.slotName, "toolbarbutton", GUILayout.Width (110))){
													Selection.activeObject = _DKUMA_Variables.DKSlotsList[i]._LOD.LOD1;
												}
											}
											// if no LOD1
											else {
												GUI.color = Color.white;
												if ( GUILayout.Button( "Choose LOD1", "toolbarbutton", GUILayout.Width (110))){
													Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
													ChooseSlot_Win.Action = "LOD1";
													OpenSelectSlotWin ();
												}
											}
											// LOD2
											if ( Selection.activeObject == _DKUMA_Variables.DKSlotsList[i]._LOD.LOD2 ) GUI.color = Color.yellow;
											else GUI.color = Color.white;
											// if LOD2 is present
											if ( _DKUMA_Variables.DKSlotsList[i]._LOD.LOD2 != null ){
												if ( GUILayout.Button( _DKUMA_Variables.DKSlotsList[i]._LOD.LOD2.slotName, "toolbarbutton", GUILayout.Width (110))){
													Selection.activeObject = _DKUMA_Variables.DKSlotsList[i]._LOD.LOD2;
												}
											}
											// if no LOD2
											else {
												GUI.color = Color.white;
												if ( GUILayout.Button( "Choose LOD2", "toolbarbutton", GUILayout.Width (110))){
													Selection.activeObject = _DKUMA_Variables.DKSlotsList[i];
													ChooseSlot_Win.Action = "LOD2";
													OpenSelectSlotWin ();
												}
											}
										}
									}
									// is LOD 1 or 2
									else {
										using (new Horizontal()) {
											GUI.color = Color.white;
											GUILayout.Label("Is LOD child of ", Slim, GUILayout.ExpandWidth (false));
											if ( _DKUMA_Variables.DKSlotsList[i]._LOD.MasterLOD != null )
											if ( GUILayout.Button( _DKUMA_Variables.DKSlotsList[i]._LOD.MasterLOD.slotName, "toolbarbutton", GUILayout.Width (180))){
												Selection.activeObject = _DKUMA_Variables.DKSlotsList[i]._LOD.MasterLOD;
											}
										}
									}

							/*	if ( Preview == null ) using (new Horizontal()) {
									GUI.color = Color.gray ;
									if ( GUILayout.Button( "preview (Double click)", "toolbarbutton", GUILayout.Width (240))){
										if ( _DKUMA_Variables.DKSlotsList[i].meshRenderer.gameObject != null ){
											path = AssetDatabase.GetAssetPath (_DKUMA_Variables.DKSlotsList[i]);
											path = path.Replace (_DKUMA_Variables.DKSlotsList[i].name+".asset", "");
											Preview = AssetPreview.GetAssetPreview( _DKUMA_Variables.DKSlotsList[i].meshRenderer.gameObject);
											if ( Preview ){
												Preview.hideFlags = HideFlags.DontSaveInBuild;
												AssetDatabase.CreateAsset (Preview, path+"Preview-"+_DKUMA_Variables.DKSlotsList[i].name+".asset");
											}
										}
									}
								}*/
								// Add to Avatar
							//	else 
										if ( _DKUMA_Variables.DKSlotsList[i].Place != null
								         && _DKUMA_Variables.DKSlotsList[i].OverlayType != null
								         && _DKUMA_Variables.DKSlotsList[i].Race.Count > 0
								         && _DKUMA_Variables.DKSlotsList[i].Gender != null ) 
								{
									// find RPG component

									if ( _DK_RPG_UMA == null && Selection.activeGameObject ) _DK_RPG_UMA = Selection.activeGameObject.GetComponent<DK_RPG_UMA>();
									if ( _DK_RPG_UMA == null && Selection.activeGameObject &&Selection.activeGameObject.transform && Selection.activeGameObject.transform.parent ) _DK_RPG_UMA = Selection.activeGameObject.transform.parent.GetComponent<DK_RPG_UMA>();
									
									// Verify the Race And gender
									if ( _DK_RPG_UMA 
									    &&  _DKUMA_Variables.DKSlotsList[i].Race.Contains(_DK_RPG_UMA.Race)
									    && ( _DKUMA_Variables.DKSlotsList[i].Gender == _DK_RPG_UMA.Gender ||_DKUMA_Variables.DKSlotsList[i].Gender == "Both" )
									    ){
										using (new Horizontal()) {
											GUI.color = Color.yellow;
											if ( GUILayout.Button( "Add Slot to selected avatar", "toolbarbutton", GUILayout.Width (240))){
												if ( _DKUMA_Variables.DKSlotsList[i].Place.name.Contains("Wear")
												    || _DKUMA_Variables.DKSlotsList[i].Place.name.Contains("Handled") ) 
														DK_UMA_RPG_Equip.PrepareEquipSlotElement ( _DKUMA_Variables.DKSlotsList[i], null, _DK_RPG_UMA, null, new Color (0,0,0), "Main", "Yes" );
												else {
													DK_UMA_RPG_ChangeBody.PrepareChangeSlotElement ( _DKUMA_Variables.DKSlotsList[i], null, _DK_RPG_UMA );
												}
											}
										}
									}
									else if ( _DK_RPG_UMA 
									         && ( _DKUMA_Variables.DKSlotsList[i].Race.Contains(_DK_RPG_UMA.Race) == false
									         || _DKUMA_Variables.DKSlotsList[i].Gender != _DK_RPG_UMA.Gender ) ) {
										GUILayout.Label("not same gender or race", Slim, GUILayout.ExpandWidth (true));

									//	GUI.color = Color.gray;
									//	if ( GUILayout.Button( "Selection is not of the same race or gender", "toolbarbutton", GUILayout.Width (240))){
									//	}
									}
								}
												}
										//	GUILayout.Space(10);
												//This draws a Line to seperate the Controls
												GUI.color = Color.white;
												GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-100), GUILayout.Height(3));
							}
						}
						if ( _DKUMA_Variables.DKSlotsList[i] == null ) {
							_DKUMA_Variables.DKSlotsList.Remove(_DKUMA_Variables.DKSlotsList[i]);
						}
					}
				}


			// UMA Overlays
				if (Showoverlay)
				using (new Horizontal()) {
					GUI.color = Color.cyan ;
					GUILayout.Label("UMA Overlays ("+_DKUMA_Variables.UMAOverlaysList.Count.ToString()+")", "toolbarbutton", GUILayout.ExpandWidth (true));
					if (Showoverlay) GUI.color = Green ;
					else GUI.color = Color.gray ;
					if ( GUILayout.Button( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))){
						if (Showoverlay) Showoverlay = false;
						else Showoverlay = true;
					}
				}
				if ( ShowUMA && Showoverlay && _DKUMA_Variables.UMAOverlaysList.Count > 0 ){
					#region Helper
					// Helper
					GUI.color = Color.white ;
					if ( Helper ) GUILayout.TextField("Click on the name to select an Element." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					GUI.color = Green ;
					if ( Helper ) GUILayout.TextField("The Green elements are installed in DK UMA, click on the 'X' button at the end of the line to delete it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					
					GUI.color = Red ;
					if ( Helper ) GUILayout.TextField("The Red elements are not installed in DK UMA, click on the 'Add' button at the end of the line to correct it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					
					#endregion Helper
					if ( ShowUMA && Showoverlay && _DKUMA_Variables.UMAOverlaysList.Count > 0 ){
						for (int i = 0; i < _DKUMA_Variables.UMAOverlaysList.Count ; i++)
						{
									if (  _DKUMA_Variables.UMAOverlaysList[i] != null 
										&& ( !ShowNew ||( ShowNew && _DKUMA_Variables.DKOverlaysNamesList.Contains(_DKUMA_Variables.UMAOverlaysList[i].name) == false ))
							    && _DKUMA_Variables.UMAOverlaysList[i].name != "DefaultOverlayType"
							    && ( SearchString == "" 
							    || _DKUMA_Variables.UMAOverlaysList[i].name.ToLower().Contains(SearchString.ToLower()) )){
								using (new Horizontal()) {
									if (_DKUMA_Variables.DKOverlaysNamesList.Contains(_DKUMA_Variables.UMAOverlaysList[i].name)) GUI.color = Green ;
									else GUI.color = Red ;
									if ( Selection.activeObject == _DKUMA_Variables.UMAOverlaysList[i] ) GUI.color = Color.yellow ;
									if ( GUILayout.Button( _DKUMA_Variables.UMAOverlaysList[i].name, "toolbarbutton", GUILayout.Width (310))){
										Selection.activeObject = _DKUMA_Variables.UMAOverlaysList[i];
									}
									GUI.color = Red ;
									if ( GUILayout.Button( "X", "toolbarbutton", GUILayout.ExpandWidth (false))){
										
									}
								}
								if ( _DKUMA_Variables.UMAOverlaysList[i] != null && ( SearchString == "" 
								                                                     || _DKUMA_Variables.UMAOverlaysList[i].name.ToLower().Contains(SearchString.ToLower()) ))
								using (new Horizontal()) {
									foreach ( Texture2D _Texture2D in _DKUMA_Variables.UMAOverlaysList[i].textureList)
									using (new Vertical()) {
										if ( _Texture2D != null ) GUI.color = Color.white ;
										else GUI.color = Red;
										if ( GUILayout.Button( _Texture2D ,GUILayout.Width (75), GUILayout.Height (75))){
											Selection.activeObject = _DKUMA_Variables.UMAOverlaysList[i];
										}
									}
									using (new Vertical()) {
										using (new Horizontal()) {
											GUI.color = Green ;
											if ( _DKUMA_Variables.DKOverlaysNamesList.Contains(_DKUMA_Variables.UMAOverlaysList[i].name) == false
											    && GUILayout.Button( "Add", "toolbarbutton", GUILayout.Width (65))){
												AddToDK(_DKUMA_Variables.UMAOverlaysList[i].GetType(), _DKUMA_Variables.UMAOverlaysList[i] as UnityEngine.Object);
											}
										}
										using (new Horizontal()) {
										GUI.color = Green ;
											if ( _DKUMA_Variables.DKOverlaysNamesList.Contains(_DKUMA_Variables.UMAOverlaysList[i].name) == true
											    && GUILayout.Button( "Select DK", "toolbarbutton", GUILayout.Width (65)))
										{
												for (int i1 = 0; i1 < _DKUMA_Variables.DKOverlaysList.Count ; i1++)
											{
													if (_DKUMA_Variables.DKOverlaysList[i1].name == _DKUMA_Variables.UMAOverlaysList[i].name ){
														Selection.activeObject = _DKUMA_Variables.DKOverlaysList[i1];
														
													}
												}
											}
										}
											if ( GameSettings ) using (new Horizontal()) {
												if ( GameSettings._GameLibraries.UmaOverlaysLibrary.Contains (_DKUMA_Variables.UMAOverlaysList[i]) == true ) GUI.color = Green ;
											else GUI.color = Color.gray ;
											if ( GUILayout.Button( "In Library", "toolbarbutton", GUILayout.Width (65))){
													if (GameSettings._GameLibraries.UmaOverlaysLibrary.Contains (_DKUMA_Variables.UMAOverlaysList[i]) == false ) {
														GameSettings._GameLibraries.UmaOverlaysLibrary.Add (_DKUMA_Variables.UMAOverlaysList[i]);

												}
												else{

												}
												EditorUtility.SetDirty (_OverlayLibrary);
												AssetDatabase.SaveAssets ();
											}
										}
									}
									
									if ( _DKUMA_Variables.UMAOverlaysList[i] == null ) {
										_DKUMA_Variables.UMAOverlaysList.Remove(_DKUMA_Variables.UMAOverlaysList[i]);
									}
								}
								if ( _DKUMA_Variables.UMAOverlaysList[i] != null && ( SearchString == "" 
							     || _DKUMA_Variables.UMAOverlaysList[i].name.ToLower().Contains(SearchString.ToLower()) )
											&& ( !ShowNew ||( ShowNew && _DKUMA_Variables.DKOverlaysNamesList.Contains(_DKUMA_Variables.UMAOverlaysList[i].name) == false )))
								GUILayout.Space(5);
											//This draws a Line to seperate the Controls
											GUI.color = Color.white;
											GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));		
							}
						}
					}
				}
			
				// DK Overlays
				if (ShowDKoverlay)
				using (new Horizontal()) {
					GUI.color = Color.cyan ;
					GUILayout.Label("DK Overlays ("+_DKUMA_Variables.DKOverlaysList.Count.ToString()+")", "toolbarbutton", GUILayout.ExpandWidth (true));
					if (ShowDKoverlay) GUI.color = Green ;
					else GUI.color = Color.gray ;
					if ( GUILayout.Button( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))){
						if (ShowDKoverlay) ShowDKoverlay = false;
						else ShowDKoverlay = true;
					}
				}
				#region Helper
				// Helper
				GUI.color = Color.white ;
				if ( Helper ) GUILayout.TextField("Click on the name to select an Element and modify it using the Prepare tab of the DK Editor, or using the Auto Detect (in the Prepare tab)." , 500, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUI.color = Green ;
				if ( Helper ) GUILayout.TextField("The Green elements has been setup for DK UMA." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUI.color = Red ;
				if ( Helper ) GUILayout.TextField("The Red elements are not ready for DK UMA, click on the 'Setup' button at the end of the line to ..." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				#endregion Helper

				
				if ( ShowDK && ShowDKoverlay && _DKUMA_Variables.DKOverlaysList.Count > 0 ){
					for (int i = 0; i < _DKUMA_Variables.DKOverlaysList.Count ; i++)
					{
						DKOverlayData overlay = _DKUMA_Variables.DKOverlaysList[i];
						if ( overlay != null && ( SearchString == "" || overlay.name.ToLower().Contains(SearchString.ToLower()) )
							    && ( ( ShowLinked == false && overlay.LinkedToSlot.Count == 0 ) || ShowLinked == true )
							    && (( ShowDefault == false && overlay.Default == false ) || ShowDefault )
						    && (( ShowNew && (overlay.Place == null && overlay.LinkedToSlot.Count == 0 )) || !ShowNew )
						    ){
							using (new Horizontal()) {
								if ( _DKUMA_Variables.DKOverlaysList[i].Place != null ) GUI.color = Green ;
								else GUI.color = Red ;
									if ( _DKUMA_Variables.DKOverlaysList[i].LinkedToSlot.Count > 0 ) GUI.color = Color.gray ;
								if ( Selection.activeObject == _DKUMA_Variables.DKOverlaysList[i] ) GUI.color = Color.yellow ;
								if ( _DKUMA_Variables.DKOverlaysList[i] != null && GUILayout.Button( _DKUMA_Variables.DKOverlaysList[i].name, "toolbarbutton", GUILayout.Width (310))){
									Selection.activeObject = _DKUMA_Variables.DKOverlaysList[i];
								}
									GUI.color = Color.cyan;
									if ( GUILayout.Button( "Edit", "toolbarbutton", GUILayout.ExpandWidth (false))){
										Selection.activeObject = _DKUMA_Variables.DKOverlaysList[i];
										OpenEditorPrepareWin();										
									}
									GUI.color = Red ;
									if ( GUILayout.Button( "X", "toolbarbutton", GUILayout.ExpandWidth (false))){
									RemoveFromDK(_DKUMA_Variables.DKOverlaysList[i].GetType(), _DKUMA_Variables.DKOverlaysList[i] as UnityEngine.Object);
								}
							}
							if ( _DKUMA_Variables.DKOverlaysList[i] != null && ( SearchString == "" 
							                                                    || _DKUMA_Variables.DKOverlaysList[i].name.ToLower().Contains(SearchString.ToLower()) ))
							
								if ( _DKUMA_Variables )
								using (new Horizontal()) {
										try {
								foreach ( Texture2D _Texture2D in _DKUMA_Variables.DKOverlaysList[i].textureList)
								using (new Vertical()) {
									if ( _Texture2D != null ) GUI.color = Color.white ;
									else GUI.color = Red;
									if ( GUILayout.Button( _Texture2D ,GUILayout.Width (50), GUILayout.Height (50))){
										Selection.activeObject = _DKUMA_Variables.DKOverlaysList[i];
									}
								}
										}catch (NullReferenceException ) {}

								using (new Vertical()) {
									using (new Horizontal()) {
										if ( _DKUMA_Variables.DKOverlaysList[i].Active == true ) GUI.color = Green ;
										else GUI.color = Color.gray ;
										if ( GUILayout.Button( "Is Active", "toolbarbutton", GUILayout.Width (60))){
											if (_DKUMA_Variables.DKOverlaysList[i].Active == true ) _DKUMA_Variables.DKOverlaysList[i].Active = false ;
											else _DKUMA_Variables.DKOverlaysList[i].Active = true ;
											EditorUtility.SetDirty (_DKUMA_Variables.DKOverlaysList[i]);
											AssetDatabase.SaveAssets ();
										}
									/*	if ( _DKUMA_Variables.DKOverlaysList[i].AvailableAtStart == true ) GUI.color = Green ;
										else GUI.color = Color.gray ;
										if ( GUILayout.Button( "At Start", "toolbarbutton", GUILayout.Width (60))){
											if ( _DKUMA_Variables.DKOverlaysList[i].AvailableAtStart == true ) _DKUMA_Variables.DKOverlaysList[i].AvailableAtStart = false ;
											else _DKUMA_Variables.DKOverlaysList[i].AvailableAtStart = true ;
											EditorUtility.SetDirty (_DKUMA_Variables.DKOverlaysList[i]);
											AssetDatabase.SaveAssets ();
										}*/
										
												if ( GameSettings != null 
												    && GameSettings._GameLibraries.DkOverlaysLibrary.Contains (_DKUMA_Variables.DKOverlaysList[i]) ) GUI.color = Green ;
										else GUI.color = Color.gray ;
										if ( GUILayout.Button( "In Library", "toolbarbutton", GUILayout.Width (65))){
													if (GameSettings._GameLibraries.DkOverlaysLibrary.Contains (_DKUMA_Variables.DKOverlaysList[i]) == false ) {
														GameSettings._GameLibraries.DkOverlaysLibrary.Add (_DKUMA_Variables.DKOverlaysList[i] as DKOverlayData);
														EditorUtility.SetDirty (GameSettings);
												AssetDatabase.SaveAssets ();
											}
										}
									}
									// Add to Avatar
												if ( _DKUMA_Variables.DKOverlaysList[i]._UMA == null  ) {
													GUI.color = Red ;
													if ( GUILayout.Button( "The UMA Element is not in your project", "toolbarbutton", GUILayout.Width (240))){
															DKUMACorrectors.LinkToUMAoverlay ( _DKUMA_Variables.DKOverlaysList[i], _DKUMA_Variables );

													}
													//	GUILayout.Label("The DK Element is skipped.", Slim, GUILayout.ExpandWidth (true));

												}	
									if ( _DKUMA_Variables.DKOverlaysList[i].LinkedToSlot.Count > 0 ){
										GUILayout.Label("Is Linked to a slot", GUILayout.ExpandWidth (true));
									}
									else if ( _DKUMA_Variables.DKOverlaysList[i].Place != null
									    && _DKUMA_Variables.DKOverlaysList[i].OverlayType != null
									    && _DKUMA_Variables.DKOverlaysList[i].Race.Count > 0
									    && _DKUMA_Variables.DKOverlaysList[i].Gender != null
									    && _DKUMA_Variables.DKOverlaysList[i].LinkedToSlot.Count == 0 ) 
									{
										if ( _DK_RPG_UMA == null && Selection.activeGameObject ) _DK_RPG_UMA = Selection.activeGameObject.GetComponent<DK_RPG_UMA>();
										if ( _DK_RPG_UMA == null && Selection.activeGameObject 
										    && Selection.activeGameObject.transform 
										    && Selection.activeGameObject.transform.parent ) _DK_RPG_UMA = Selection.activeGameObject.transform.parent.GetComponent<DK_RPG_UMA>();
										
										// Verify the Race And gender
										if ( _DK_RPG_UMA 
										    &&  _DKUMA_Variables.DKOverlaysList[i].Race.Contains(_DK_RPG_UMA.Race)
										    && ( _DKUMA_Variables.DKOverlaysList[i].Gender == _DK_RPG_UMA.Gender ||_DKUMA_Variables.DKOverlaysList[i].Gender == "Both" ))
										{
											GUI.color = Color.yellow ;
											using (new Horizontal()) {
												if ( GUILayout.Button( "Add to the avatar", "toolbarbutton", GUILayout.Width (130))){
													if ( _DKUMA_Variables.DKOverlaysList[i].OverlayType.Contains("Wear") == true ) 
																DK_UMA_RPG_Equip.PrepareEquipSlotElement ( null, _DKUMA_Variables.DKOverlaysList[i], _DK_RPG_UMA, null, new Color (0,0,0), "Main", "Yes" );
													else {
														DK_UMA_RPG_ChangeBody.PrepareChangeSlotElement ( null, _DKUMA_Variables.DKOverlaysList[i], _DK_RPG_UMA );

													}
												}
											}
										}
										else if ( _DK_RPG_UMA ) {
											GUILayout.Label("not same gender or race", Slim, GUILayout.ExpandWidth (true));
										//	GUI.color = Color.gray ;
										//	if ( GUILayout.Button( "not same gender or race", "toolbarbutton", GUILayout.Width (130))){
										//	}
										}
									}
								}

								if ( _DKUMA_Variables.DKOverlaysList[i] == null ) {
									_DKUMA_Variables.DKOverlaysList.Remove(_DKUMA_Variables.DKOverlaysList[i]);
								}

							}
							if ( _DKUMA_Variables.DKOverlaysList[i] != null && ( SearchString == "" 
							                                                    || _DKUMA_Variables.DKOverlaysList[i].name.ToLower().Contains(SearchString.ToLower()) ))
								GUILayout.Space(5);
										//This draws a Line to seperate the Controls
										GUI.color = Color.white;
										GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));
						}
					}
				}
			
						if (ShowDKRaces) 							
							using (new Horizontal()) {
								// UMA Slots
								GUI.color = Color.cyan ;
								GUILayout.Label("DK Races ("+_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary.Count+")", "toolbarbutton", GUILayout.ExpandWidth (true));
								if (ShowDKRaces) GUI.color = Green ;
								else GUI.color = Color.gray ;
								if ( GUILayout.Button( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))){
									if (ShowDKRaces) ShowDKRaces = false;
									else ShowDKRaces = true;
								}
							}
						if ( ShowDKRaces && _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary.Count > 0 )
						{
							#region Helper
							// Helper
							GUI.color = Color.white ;
							if ( Helper ) GUILayout.TextField("Click on the name to select an Element." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							GUI.color = Green ;
							if ( Helper ) GUILayout.TextField("The Green elements are installed in DK Race Library of the scene." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

						//	GUI.color = Red ;
						//	if ( Helper ) GUILayout.TextField("The Red elements are not installed in DK UMA, click on the 'Add' button to install it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

							#endregion Helper
							for (int i = 0; i < _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary.Count ; i++)
							{
								// preview image

								if ( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary[i] != null
									&& ( SearchString == "" 
											|| _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary[i].name.ToLower().Contains(SearchString.ToLower()) )){

									using (new Horizontal()) {					
										using (new Vertical()) {
											GUILayout.Space(5);
											using (new Horizontal()) {
												GUILayout.Space(25);
											//	if (_DKUMA_Variables.DKSlotsNamesList.Contains(_DKUMA_Variables.UMASlotsList[i].name)) GUI.color = Green ;
											//	else GUI.color = Red ;
												if ( Selection.activeObject == _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary[i] ) GUI.color = Color.yellow ;
												else GUI.color = Color.white ;
												if ( GUILayout.Button( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary[i].name, "toolbarbutton", GUILayout.Width (303))){
													Selection.activeObject = _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary[i];
												}

												GUI.color = Red ;
												if ( GUILayout.Button( "X", "toolbarbutton", GUILayout.ExpandWidth (false))){

												}
											}

											if (_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary[i] != null 
												&& ( SearchString == "" 
													|| _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkRacesLibrary[i].name.ToLower().Contains(SearchString.ToLower()) ))


												try{
												using (new Horizontal()) {
													GUILayout.Space(25);
													if ( GameSettings._GameLibraries.UmaSlotsLibrary.Contains (_DKUMA_Variables.UMASlotsList[i]) ) GUI.color = Green ;
													else GUI.color = Color.gray ;
													if ( GUILayout.Button( "In Game Library", "toolbarbutton", GUILayout.Width (320)))
													{

														if (GameSettings._GameLibraries.UmaSlotsLibrary.Contains (_DKUMA_Variables.UMASlotsList[i]) == false ) {
															GameSettings._GameLibraries.UmaSlotsLibrary.Add ( _DKUMA_Variables.UMASlotsList[i]);
															EditorUtility.SetDirty (GameSettings);
															AssetDatabase.SaveAssets ();
														}
													}
												}
											
											}catch(ArgumentException){}
										}
									}
										//This draws a Line to seperate the Controls
										GUI.color = Color.white;
										GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));
									}
							}
						}

						if (ShowRaces) 
							using (new Horizontal()) {
								// UMA Slots
								GUI.color = Color.cyan ;
								GUILayout.Label("UMA Races ("+_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Count+")", "toolbarbutton", GUILayout.ExpandWidth (true));
								if (ShowRaces) GUI.color = Green ;
								else GUI.color = Color.gray ;
								if ( GUILayout.Button( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))){
									if (ShowRaces) ShowRaces = false;
									else ShowRaces = true;
								}
							}
						if ( ShowRaces && _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Count > 0 )
						{
							#region Helper
							// Helper
							GUI.color = Color.white ;
							if ( Helper ) GUILayout.TextField("Click on the name to select an Element." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
							GUI.color = Green ;
							if ( Helper ) GUILayout.TextField("The Green elements are installed in DK Race Library of the scene." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

							//	GUI.color = Red ;
							//	if ( Helper ) GUILayout.TextField("The Red elements are not installed in DK UMA, click on the 'Add' button to install it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

							#endregion Helper
							for (int i = 0; i < _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Count ; i++)
							{
								// preview image

								if ( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i] != null
									&& ( SearchString == "" 
											|| _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i].name.ToLower().Contains(SearchString.ToLower()) )){

									using (new Horizontal()) {					
										using (new Vertical()) {
											GUILayout.Space(5);
											using (new Horizontal()) {
												GUILayout.Space(25);
												//	if (_DKUMA_Variables.DKSlotsNamesList.Contains(_DKUMA_Variables.UMASlotsList[i].name)) GUI.color = Green ;
												//	else GUI.color = Red ;
												if ( Selection.activeObject == _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i] ) GUI.color = Color.yellow ;
												else GUI.color = Color.white ;
												if ( GUILayout.Button( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i].name, "toolbarbutton", GUILayout.Width (303))){
													Selection.activeObject = _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i];
												}

												GUI.color = Red ;
												if ( GUILayout.Button( "X", "toolbarbutton", GUILayout.ExpandWidth (false))){

												}
											}

											if (_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i] != null 
												&& ( SearchString == "" 
													|| _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i].name.ToLower().Contains(SearchString.ToLower()) ))


												try{
												using (new Horizontal()) {
													GUILayout.Space(25);
													if ( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Contains (_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i]) ) GUI.color = Green ;
													else GUI.color = Color.gray ;
													if ( GUILayout.Button( "In Game Library", "toolbarbutton", GUILayout.Width (320)))
													{

														if (_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Contains (_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i]) == false ) {
															_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary.Add ( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.UmaRacesLibrary[i]);
															EditorUtility.SetDirty (GameSettings);
															AssetDatabase.SaveAssets ();
														}
													}
												}

											}catch(ArgumentException){}
										}
									}
										//This draws a Line to seperate the Controls
										GUI.color = Color.white;
										GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));
									}
							}
						}
					}
		}
		#endregion Lists
		}catch(InvalidOperationException){}
	
		}
		else {
			if ( DK_UMA == null ){
				GUI.color = Red ;
				EditorGUILayout.HelpBox("DK UMA is not installed to your scene, open the 'DK UMA Editor' to install DK UMA to the current scene.", UnityEditor.MessageType.Warning);
			}
			if ( _UMA == null ){
				GUI.color = Red ;
				EditorGUILayout.HelpBox("UMA is not installed to your scene, open the 'DK UMA Editor' to install UMA to the current scene.", UnityEditor.MessageType.Warning);
			}
			else {
				GUI.color = Red ;
				EditorGUILayout.HelpBox("No DK UMA Game Settings is assigned. " 
					+"Open the DK UMA Editor window and its Setup Tab, " 
					+"then assign a DK UMA Game Settings to the corresponding field.", UnityEditor.MessageType.Warning);
			}
			GUI.color = Color.white ;
			if ( GUILayout.Button ( "Open the 'DK UMA Editor'", GUILayout.ExpandWidth (true))) {
				GetWindow(typeof(DK_UMA_Editor), false, "DK UMA Editor");
				Close();
			}
		}
	}

	#region Search Voids

	public void PrepareForPBR (){ }

	void CorrectElements () {
		if ( _DKUMA_Variables._DK_UMA_GameSettings != null
			&& _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Count == 0 )
			DKUMACorrectors.DetectPlaces ();

		List<DKSlotData> tempList = new List<DKSlotData>();
		List<DKOverlayData> RemoveNullOvList = new List<DKOverlayData>();

	//	string[] materials = AssetDatabase.FindAssets ("UMA_Diffuse_Normal_Metallic", null);
	//	string[] Legacymaterials = AssetDatabase.FindAssets ("UMALegacy", null);

	//	Debug.Log ("Verify Elements : The Linked Overlays, the Textures and the not required slots." );
		foreach ( DKSlotData slot in _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary ){
			if ( slot != null ) {
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
				// verify place
				DKUMACorrectors.VerifyDKSlotPlace (slot, _DKUMA_Variables);

				// verify linked UMA element
				if ( slot._UMA == null ) {
					DKUMACorrectors.LinkToUMAslot (slot, _DKUMA_Variables);
				}

				// verify Material
				else if ( slot._UMA != null && slot._UMA.material == null ) {
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
		foreach ( DKOverlayData _Overlay in _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary ){

			// verify place
			if (  _Overlay != null ) DKUMACorrectors.VerifyDKOverlayPlace ( _Overlay, _DKUMA_Variables);

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

				//	foreach ( Texture texture in (_Overlay._UMA as UMA.OverlayDataAsset).textureList ){
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

			if ( _Overlay == null ) Debug.LogError ( "_Overlay == null" );
			else if ( _Overlay._UMA == null ) {
				DKUMACorrectors.LinkToUMAoverlay (_Overlay, _DKUMA_Variables);
			}
			if ( _Overlay._UMA != null ) {
				// fix textures readable
				for (int i1 = 0; i1 < _Overlay._UMA.textureList.Length; i1 ++) {
					var obj = _Overlay._UMA.textureList [i1];
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
		}
		AssetDatabase.SaveAssets ();
		Debug.Log ("Elements are fixed" );
	}

	void MakePreviews (){
	//	PreviewsList.Clear ();

		for (int i = 0; i < _DKUMA_Variables.DKSlotsList.Count; i ++) {
			MakePreview ( _DKUMA_Variables.DKSlotsList[i] );
		}
	}

	void MakePreview ( DKSlotData DKslot ){
		// prepare path
		string path = AssetDatabase.GetAssetPath (DKslot._UMA);
		path = path.Replace (DKslot._UMA.name+".asset", "");
		string DKpath = AssetDatabase.GetAssetPath (DKslot);
		DKpath = DKpath.Replace (DKslot._UMA.name+".asset", "");
		Debug.Log ("Preview : Source path = "+path );
		DirectoryInfo directory = new DirectoryInfo(path);
		
		List<FileInfo> goFileInfo = new List<FileInfo>();
		goFileInfo.AddRange(directory.GetFiles("*.prefab", SearchOption.AllDirectories));
		
		int i = 0; int goFileInfoLength = goFileInfo.Count;
		FileInfo tempGoFileInfo; string tempFilePath;
		UnityEngine.Object tempGO = null;

		Debug.Log ("Preview : Prefab(s) found with source "+goFileInfo.Count+" (0 is "+goFileInfo[0]+")" );

		for (; i < goFileInfoLength; i++)
		{
			tempGoFileInfo = goFileInfo[i];
			if (tempGoFileInfo == null)
				continue;
			
			tempFilePath = tempGoFileInfo.FullName;
			tempFilePath = tempFilePath.Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			try{
				#if UNITY_EDITOR
				tempGO = UnityEditor.AssetDatabase.LoadAssetAtPath(tempFilePath, typeof(GameObject)) as GameObject;
				#endif
				if (tempGO == null)
				{
					continue;
				}
			
				foreach ( Transform child in (tempGO as GameObject).transform ){
					if ( child.name != "global" && child.GetComponent<SkinnedMeshRenderer>() ){ 
						DKslot.meshRenderer = child.GetComponent<SkinnedMeshRenderer>(); 
					//	Debug.Log (tempGO.name+" "+tempGO.GetType().ToString());

					}
				}
			}
			catch(Exception e){
				Debug.Log ( "test"+e.ToString());
			}
		}

		if ( DKslot.meshRenderer != null ){
			List<Material> List = DKslot.meshRenderer.sharedMaterials.ToList();
			List.Clear();

			List.Add (DKslot.LinkedOverlayList[0]._UMA.material.material);


			DKslot.meshRenderer.sharedMaterials = List.ToArray();

			// prepare material
			DKslot.meshRenderer.sharedMaterials[0] =  DKslot.LinkedOverlayList[0]._UMA.material.material;

			// prepare texture of the mesh
			if ( DKslot.meshRenderer.sharedMaterials[0].mainTexture == null ){
				if ( DKslot.LinkedOverlayList.Count > 0 ){
					DKslot.meshRenderer.sharedMaterial.mainTexture = DKslot.LinkedOverlayList[0].textureList[0];
				}
			}
			/*
			// create preview
			Texture2D Preview;
			Preview = AssetPreview.GetAssetPreview( tempGO);
		//	Preview = AssetPreview.GetAssetPreview( DKslot.meshRenderer.transform.parent.gameObject as UnityEngine.Object);

			Texture2D Preview2;
			Preview2 = AssetPreview.GetAssetPreview( tempGO);

			if ( Preview2 ){
				SavePreview ( Preview2, DKpath, DKslot, DKslot.name );
			//	AssetDatabase.CreateAsset (Preview, path+"Preview-"+DKslot._UMA.name+".asset");
			}
*/
		}
	}

	void SavePreview ( Texture2D Preview, string DKpath, DKSlotData slot, string name ) {
		AssetDatabase.Refresh ();

		Preview.hideFlags = HideFlags.None;

		Texture2D oldPreview;
		string path = AssetDatabase.GetAssetPath (slot);
		path = path.Replace (name+".asset", "");
		oldPreview = AssetDatabase.LoadAssetAtPath(DKpath+"Preview-"+name+".asset", typeof(Texture2D) ) as Texture2D;

		if ( oldPreview != null ) {
		//	AssetDatabase.DeleteAsset (DKpath+"Preview-"+name+".asset");
			oldPreview = Preview;
			AssetDatabase.Refresh ();
			EditorUtility.SetDirty (oldPreview);
			AssetDatabase.SaveAssets ();
			Debug.Log ( "Preview updated");
		}
		else {
			AssetDatabase.CreateAsset (Preview, path+"Preview-"+name+".asset");
			AssetDatabase.Refresh ();

			Debug.Log ( "Preview Created");
		}
	}
	
	void ImportAll (){
		// Slots
		for (int i = 0; i < _DKUMA_Variables.UMASlotsList.Count; i++) {
			if (_DKUMA_Variables.LinkedUMASlotsList.Contains(_DKUMA_Variables.UMASlotsList[i]) == false ){

				AddToDK(_DKUMA_Variables.UMASlotsList[i].GetType(), _DKUMA_Variables.UMASlotsList[i] as UnityEngine.Object);
			}
		}
		// Overlays
		for (int i = 0; i < _DKUMA_Variables.UMAOverlaysList.Count; i++) {
			if (_DKUMA_Variables.DKOverlaysNamesList.Contains(_DKUMA_Variables.UMAOverlaysList[i].name) == false ){
				
				AddToDK(_DKUMA_Variables.UMAOverlaysList[i].GetType(), _DKUMA_Variables.UMAOverlaysList[i] as UnityEngine.Object);
			}
		}
		Debug.Log ("All the UMA elements have been converted to DK UMA. You can select and set them up by using the lists of the current window.");
	}
	#endregion Import all Element

	#region Add Element
	void AddToDK (System.Type type, UnityEngine.Object Element){

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
				newSlot._UMAslotName = newSlot._UMA.name;
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
				newSlot._UMAslotName = newSlot._UMA.name;
				AssetDatabase.CreateAsset(newSlot, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newSlot;
			}
			// Shared
			if ( newSlot.name.Contains("Female") == false && newSlot.name.Contains("Male") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Shared/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Shared/"+newSlot.name+".asset");
				newSlot._UMA = Element as  UMA.SlotDataAsset;
				newSlot._UMAslotName = newSlot._UMA.name;
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
					try {
						newOverlay.textureList.ToList().Add (newTexture);
					}
					catch (ArgumentNullException ){}
					}
					catch (NullReferenceException ){}
				}
			}
		//	newOverlay.textureList = (Element as UMA.OverlayDataAsset).textureList ;
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
				newOverlay._UMAoverlayName = newOverlay._UMA.name;
				AssetDatabase.CreateAsset(newOverlay, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newOverlay;
			}
			// Female
			if ( newOverlay.name.Contains("Female") && newOverlay.name.Contains("Male") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Female/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Female/"+newOverlay.name+".asset");
				newOverlay._UMA = Element as  UMA.OverlayDataAsset;
				newOverlay._UMAoverlayName = newOverlay._UMA.name;
				AssetDatabase.CreateAsset(newOverlay, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newOverlay;
			}
			// Shared
			if ( newOverlay.name.Contains("Female") == false && newOverlay.name.Contains("Male") == false ){
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Shared/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Shared/"+newOverlay.name+".asset");
				newOverlay._UMA = Element as  UMA.OverlayDataAsset;
				newOverlay._UMAoverlayName = newOverlay._UMA.name;
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

	void LinkAll (){
		#region link slots
		foreach ( DKSlotData slot in _DKUMA_Variables.DKSlotsList ){
			if ( slot._UMA == null ) {
				DKUMACorrectors.LinkToUMAslot (slot, _DKUMA_Variables);
			} 
		}
		#endregion link slots

		#region link overlays
		foreach ( DKOverlayData overlay in _DKUMA_Variables.DKOverlaysList ){
			DKUMACorrectors.LinkToUMAoverlay (overlay, _DKUMA_Variables);
		}
		#endregion link overlays
	}

	#region Add To Lib
	void AddToLib (){
		_DK_UMACrowd = GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>(); 

		GameObject DK_UMA = GameObject.Find("DK_UMA");

		// get DK UMA Game Settings
		 GameSettings = DK_UMA.transform.GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;

		// Slots
		for (int i = 0; i < _DKUMA_Variables.DKSlotsList.Count; i++) {
			if ( InResults ) {
				if (( ((_DKUMA_Variables.DKSlotsList[i] as DKSlotData).slotName.ToLower().Contains(SearchString.ToLower())) 
					|| SearchString == "" )
				    && _DK_UMACrowd.slotLibrary.slotDictionary.ContainsValue(_DKUMA_Variables.DKSlotsList[i] as DKSlotData) == false
				 ){
					_DK_UMACrowd.slotLibrary.AddSlot( _DKUMA_Variables.DKSlotsList[i].name, _DKUMA_Variables.DKSlotsList[i] as DKSlotData);

				}
			}else if ( _DK_UMACrowd.slotLibrary.slotDictionary.ContainsValue(_DKUMA_Variables.DKSlotsList[i] as DKSlotData) == false
			          ){
				_DK_UMACrowd.slotLibrary.AddSlot( _DKUMA_Variables.DKSlotsList[i].name, _DKUMA_Variables.DKSlotsList[i] as DKSlotData);
			}
			// lib wizard
			if ( GameSettings != null 
			    && GameSettings._GameLibraries.UseLibWizard
			    && ( ((_DKUMA_Variables.DKSlotsList[i] as DKSlotData).slotName.ToLower().Contains(SearchString.ToLower())) 
			    || SearchString == "" )
			    && GameSettings._GameLibraries.DkSlotsLibrary.Contains(_DKUMA_Variables.DKSlotsList[i] as DKSlotData) == false )
			{
				GameSettings._GameLibraries.DkSlotsLibrary.Add ( _DKUMA_Variables.DKSlotsList[i] as DKSlotData );
			}
		}
		// Overlays
		for (int i = 0; i < _DKUMA_Variables.DKOverlaysList.Count; i++) {
			if ( InResults ) {
				if (((InResults && (_DKUMA_Variables.DKOverlaysList[i] as DKOverlayData).overlayName.ToLower().Contains(SearchString.ToLower())) 
				    || SearchString == "" )
					&& _DK_UMACrowd.overlayLibrary.overlayDictionary.ContainsValue(_DKUMA_Variables.DKOverlaysList[i] as DKOverlayData) == false){
					_DK_UMACrowd.overlayLibrary.AddOverlay( _DKUMA_Variables.DKOverlaysList[i].overlayName, _DKUMA_Variables.DKOverlaysList[i] as DKOverlayData);

				}
			}else if ( _DK_UMACrowd.overlayLibrary.overlayDictionary.ContainsValue(_DKUMA_Variables.DKOverlaysList[i] as DKOverlayData) == false){
				_DK_UMACrowd.overlayLibrary.AddOverlay( _DKUMA_Variables.DKOverlaysList[i].overlayName, _DKUMA_Variables.DKOverlaysList[i] as DKOverlayData);
			}

			// lib wizard
			if ( GameSettings != null 
			    && GameSettings._GameLibraries.UseLibWizard
			    && ( ((_DKUMA_Variables.DKOverlaysList[i] as DKOverlayData).overlayName.ToLower().Contains(SearchString.ToLower())) 
			    || SearchString == "" )
			    && GameSettings._GameLibraries.DkOverlaysLibrary.Contains(_DKUMA_Variables.DKOverlaysList[i] as DKOverlayData) == false )
			{
				GameSettings._GameLibraries.DkOverlaysLibrary.Add ( _DKUMA_Variables.DKOverlaysList[i] as DKOverlayData );
			}
		}
		// Races
		for (int i = 0; i < GameSettings._GameLibraries.DkRacesLibrary.Count; i++) {
			if ( InResults ) {
				if (((InResults && (GameSettings._GameLibraries.DkRacesLibrary[i] as DKRaceData).raceName.ToLower().Contains(SearchString.ToLower())) 
					|| SearchString == "" )
					&& _DK_UMACrowd.raceLibrary.raceDictionary.ContainsValue(GameSettings._GameLibraries.DkRacesLibrary[i] as DKRaceData) == false
					&& GameSettings._GameLibraries.DkRacesLibrary[i].name.Contains ("Default") == false
					&& GameSettings._GameLibraries.DkRacesLibrary[i].Active == true ){
					_DK_UMACrowd.raceLibrary.AddRace( GameSettings._GameLibraries.DkRacesLibrary[i] as DKRaceData);

				}
			}else if ( _DK_UMACrowd.raceLibrary.raceDictionary.ContainsValue(GameSettings._GameLibraries.DkRacesLibrary[i] as DKRaceData) == false
				&& GameSettings._GameLibraries.DkRacesLibrary[i].name.Contains ("Default") == false
				&& GameSettings._GameLibraries.DkRacesLibrary[i].Active == true ){
				_DK_UMACrowd.raceLibrary.AddRace( GameSettings._GameLibraries.DkRacesLibrary[i] as DKRaceData);
			}
		}
	//	Debug.Log ("All the DK elements added scene libraries.");
	//	Debug.Log ("DK Helper : You are now able to setup your new elements for DK to use them properly.");

		EditorUtility.SetDirty(GameSettings);
		EditorUtility.SetDirty(_DK_UMACrowd.overlayLibrary);
		EditorUtility.SetDirty(_DK_UMACrowd.slotLibrary);
		EditorUtility.SetDirty(_DK_UMACrowd.raceLibrary);
		AssetDatabase.SaveAssets();
	}

	void AddToUMALib (){
		GameObject DK_UMA = GameObject.Find("DK_UMA");

		// get DK UMA Game Settings
		GameSettings = DK_UMA.transform.GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;
		if ( FindObjectOfType<UMAGenerator>() != null ){
			if ( _SlotLibrary == null ) _SlotLibrary = FindObjectOfType<DynamicSlotLibrary>();
			if ( _OverlayLibrary == null ) _OverlayLibrary = FindObjectOfType<DynamicOverlayLibrary>();

			List<UMA.SlotDataAsset> LibSlotsList = new List<UMA.SlotDataAsset>();
			if ( _SlotLibrary != null ) LibSlotsList = _SlotLibrary.GetAllSlotAssets ().ToList ();

			// Slots
			if ( _SlotLibrary != null ) for (int i = 0; i < _DKUMA_Variables.UMASlotsList.Count; i++) {
				if ( LibSlotsList.Contains(_DKUMA_Variables.UMASlotsList[i] as UMA.SlotDataAsset) == false)
					_SlotLibrary.AddSlotAsset( _DKUMA_Variables.UMASlotsList[i] as UMA.SlotDataAsset);

				// lib wizard
				if ( GameSettings != null 
				    && GameSettings._GameLibraries.UseLibWizard
				    && ( ((_DKUMA_Variables.UMASlotsList[i] as UMA.SlotDataAsset).slotName.ToLower().Contains(SearchString.ToLower())) 
				    || SearchString == "" )
				    && GameSettings._GameLibraries.UmaSlotsLibrary.Contains(_DKUMA_Variables.UMASlotsList[i] as UMA.SlotDataAsset) == false )
				{
					GameSettings._GameLibraries.UmaSlotsLibrary.Add ( _DKUMA_Variables.UMASlotsList[i] as UMA.SlotDataAsset );
				}
			}

			List<UMA.OverlayDataAsset> LibOvsList = new List<UMA.OverlayDataAsset>();
			if ( _OverlayLibrary != null ) LibOvsList = _OverlayLibrary.GetAllOverlayAssets ().ToList ();

			// Overlays
			if ( _OverlayLibrary != null )for (int i = 0; i < _DKUMA_Variables.UMAOverlaysList.Count; i++) {
				if ( LibOvsList.Contains(_DKUMA_Variables.UMAOverlaysList[i] as UMA.OverlayDataAsset) == false)
					_OverlayLibrary.AddOverlayAsset( _DKUMA_Variables.UMAOverlaysList[i] as UMA.OverlayDataAsset);

				// lib wizard
				if ( GameSettings != null 
				    && GameSettings._GameLibraries.UseLibWizard
				    && ( ((_DKUMA_Variables.UMAOverlaysList[i] as UMA.OverlayDataAsset).overlayName.ToLower().Contains(SearchString.ToLower())) 
				    || SearchString == "" )
				    && GameSettings._GameLibraries.UmaOverlaysLibrary.Contains(_DKUMA_Variables.UMAOverlaysList[i] as UMA.OverlayDataAsset) == false )
				{
					GameSettings._GameLibraries.UmaOverlaysLibrary.Add ( _DKUMA_Variables.UMAOverlaysList[i] as UMA.OverlayDataAsset );
				}
			}

			// Races
			RaceLibrary UMARaceLibrary = FindObjectOfType<DynamicRaceLibrary>();
			List<UMA.RaceData> LibRacesList = new List<UMA.RaceData>();
			if ( UMARaceLibrary != null ) LibRacesList = UMARaceLibrary.GetAllRaces ().ToList ();

			if ( UMARaceLibrary != null ) for (int i = 0; i <GameSettings._GameLibraries.UmaRacesLibrary.Count; i++) {
					if ( LibRacesList.Contains(GameSettings._GameLibraries.UmaRacesLibrary[i] as UMA.RaceData) == false)
						UMARaceLibrary.AddRace(GameSettings._GameLibraries.UmaRacesLibrary[i] as UMA.RaceData);
				}	
			Debug.Log ("All the UMA elements added to the UMA libraries.");
		//	Debug.Log ("DK Helper : You are now able to generate a UMA clone of every new DK avatar.");
			EditorUtility.SetDirty(GameSettings);
			AssetDatabase.SaveAssets();
		}
		else Debug.LogError ( "DK UMA : the UMA object is not present in your scene. Install it and retry to prepare the libraries." );
	}
	#endregion Add to Lib

	#region Remove Element
	void RemoveFromDK (System.Type type, UnityEngine.Object Element){
		// slot
		if ( type.ToString() == "DKSlotData" ){
			if ( _DKUMA_Variables.DKSlotsList.Contains ( Element as DKSlotData ) ){
				_DKUMA_Variables.DKSlotsList.Remove (Element as DKSlotData);
				}
			if ( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary.Contains ( Element as DKSlotData ) ){
				_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkSlotsLibrary.Remove ( Element as DKSlotData );
			}
			DestroyImmediate (Element, true);
		}
		// Overlay
		if ( type.ToString() == "DKOverlayData" ){
			if ( _DKUMA_Variables.DKOverlaysList.Contains ( Element as DKOverlayData ) ){
				_DKUMA_Variables.DKOverlaysList.Remove (Element as DKOverlayData);
			}
			if ( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary.Contains ( Element as DKOverlayData ) ){
				_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.DkOverlaysLibrary.Remove ( Element as DKOverlayData );
			}		
			DestroyImmediate (Element, true);
		}
		EditorUtility.SetDirty(GameSettings);
		AssetDatabase.SaveAssets();
	}
	#endregion Remove Element

	void InstallUMADCS (){
		Debug.Log ( "Installing UMA_DCS prefab to the current scene." );
		GameObject go = Instantiate(Resources.Load("UMA_DCS")) as GameObject;
		go.name = "UMA_DCS";
	//	Debug.Log ( go.name );

		GameObject _UMA = GameObject.Find("UMA_DCS");
		List<Transform> _List =  go.transform.GetComponentsInChildren<Transform>().ToList();

		for (int i1 = 0; i1 < _List.Count ; i1++)
		{
			_List[i1].parent = _UMA.transform;
		}
	//	DestroyImmediate (go);
	}

	void InstallUMA (){
		Debug.Log ( "Installing UMA prefab to the current scene." );
		GameObject go = Instantiate(Resources.Load("UMA205")) as GameObject;
		go.name = "UMA";
		//	Debug.Log ( go.name );

		GameObject _UMA = GameObject.Find("UMA");
		List<Transform> _List =  go.transform.GetComponentsInChildren<Transform>().ToList();

		for (int i1 = 0; i1 < _List.Count ; i1++)
		{
			_List[i1].parent = _UMA.transform;
		}
		//	DestroyImmediate (go);
	}

	void OnProjectChange() {
		if ( Action == "Detecting" ){
		}
	}

	void OnSelectionChange() {
		_DK_RPG_UMA = null;
		Repaint();
	}
}
