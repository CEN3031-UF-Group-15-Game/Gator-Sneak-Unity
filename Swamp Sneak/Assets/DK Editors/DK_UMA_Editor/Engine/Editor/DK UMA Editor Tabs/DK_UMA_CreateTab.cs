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

public class DK_UMA_CreateTab : EditorWindow {
	public static Color Green = new Color (0.8f, 1f, 0.8f, 1);
	public static Color Red = new Color (0.9f, 0.5f, 0.5f);

	public static Vector2 scroll;


	public static void OnGUI () {
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

		if ( (GameObject.Find ("DK_UMA") == null && GameObject.Find ("UMA") == null)
			|| (GameObject.Find ("DK_UMA") == null && GameObject.Find ("UMA_DCS") == null) )
			EditorOptions.DisplayWelcome ();

		DK_UMACrowd _DK_UMACrowd = GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>();
		EditorVariables.DK_UMACrowd = _DK_UMACrowd;
		if ( !EditorVariables._RaceLibrary ) EditorVariables._RaceLibrary =  _DK_UMACrowd.raceLibrary;
		if ( !EditorVariables._DKSlotLibrary ) EditorVariables._DKSlotLibrary = _DK_UMACrowd.slotLibrary;
		if ( !EditorVariables._OverlayLibrary ) EditorVariables._OverlayLibrary =  _DK_UMACrowd.overlayLibrary;
		#region If not ready

		else if ( GameObject.Find ("UMA") == null ) {
			GUI.color = Color.yellow;
			EditorGUILayout.HelpBox("You need to install UMA to your scene ! Open the 'Elements Manager' and Install UMA to the scene.", UnityEditor.MessageType.Warning);
			GUI.color = Color.white;
			if ( GUILayout.Button ( "Use the Elements Manager to prepare the scene", GUILayout.ExpandWidth (true))) {
				DK_UMA_Editor.OpenAutoDetectWin ();
			}
		}
		else
		try {
			if ( EditorVariables._RaceLibrary.raceElementList.Length == 0 || EditorVariables._DKSlotLibrary.slotElementList.Length == 0 || EditorVariables._OverlayLibrary.overlayElementList.Length == 0 ) {

				GameObject.Find ("DK_UMA").GetComponent<DKUMA_Variables>().PopulateLibraries();

				// overlay library
				if ( GameObject.Find ("UMA") != null && EditorVariables._OverlayLibrary.overlayElementList.Length == 0 ){
					GUI.color = Color.yellow;
					EditorGUILayout.HelpBox("DK UMA : The scene's Libraries seems umpty. To be able to create an avatar in Editor mode you have to populate the libraries. " +
						"Open the 'Elements Manager' by clicking on the next button, then click twice on the 'Add to Libraries' button. " +
						"First time is to populate the Database, second time is to populate the races.", UnityEditor.MessageType.Warning);
					using (new Horizontal()) {
						GUI.color = Color.white;
						if ( GUILayout.Button ( "Use the Elements Manager to prepare the scene", GUILayout.ExpandWidth (true))) {
							DK_UMA_Editor.OpenAutoDetectWin ();
						}
					}
				}
			}		

			if ( EditorVariables._RaceLibrary.raceElementList.Length > 0 && EditorVariables._DKSlotLibrary.slotElementList.Length > 0 && EditorVariables._OverlayLibrary.overlayElementList.Length > 0  )
				using (new Horizontal()) {
					GUI.color = Color.white;
					GUILayout.Label("Default Name :", GUILayout.ExpandWidth (false));
					if ( EditorVariables.DName == null ) EditorVariables.DName = "";
					EditorVariables.DName = GUILayout.TextField(EditorVariables.DName, 100, GUILayout.ExpandWidth (true));
					if ( EditorVariables.DName != DK_UMACrowd.DName && GUILayout.Button("Apply", GUILayout.ExpandWidth (false))) {
						DK_UMACrowd.DName = EditorVariables.DName;
					}
					if ( DK_UMACrowd.PlusID == true ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button("+ID", GUILayout.ExpandWidth (false))) {
						if ( DK_UMACrowd.PlusID == true ) DK_UMACrowd.PlusID = false;
						else DK_UMACrowd.PlusID = true;
					}
				}
		}catch(System.NullReferenceException){}

		if ( EditorVariables.UMACrowdObj && EditorVariables.DK_UMACrowd == null ){
			DetectAndAddDK.DetectAll();
			Debug.Log ( "debug");
		}
		//	if ( EditorVariables.DK_UMACrowd && EditorVariables.DK_UMACrowd.GeneratorMode != "Preset" )EditorVariables.DK_UMACrowd.GeneratorMode = "Preset"; 
		#region Create buttons list

		#endregion If not ready
		else if ( GameObject.Find ("UMA") != null
			&& EditorVariables._OverlayLibrary.overlayElementList.Length != 0 ){
			GUI.color = Color.white;
			EditorGUILayout.HelpBox("Do you want to add some clothes and handled items to the generated avatar ? " +
				"The equipments elements are declared and managed by the races (because it is generally depending on the morphology). " +
				"Use the 'Prepare' Tab to add/remove the races able to use the element. When done, " +
				"use the 'Elements Manager' to apply the modifications to the races by clicking on 'Add to Libraries' button.", UnityEditor.MessageType.Info);


			using (new Horizontal()) {
				if ( DK_RPG_UMA_Generator.AddToRPG == true ) GUI.color = Green;
				else GUI.color = Color.gray;
				/*	if ( GUILayout.Button("Is character", GUILayout.ExpandWidth (true))) {
						if ( DK_RPG_UMA_Generator.AddToRPG == true ) DK_RPG_UMA_Generator.AddToRPG = false;
						else DK_RPG_UMA_Generator.AddToRPG = true;
					}*/
				GUI.color = Color.white;
				GUILayout.Label("Generate", GUILayout.ExpandWidth (false));
				if ( DK_UMACrowd.GenerateWear == true ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button("wears equipment", GUILayout.ExpandWidth (true))) {
					if ( DK_UMACrowd.GenerateWear == true ) DK_UMACrowd.GenerateWear = false;
					else DK_UMACrowd.GenerateWear = true;
				}
				if ( DK_UMACrowd.GenerateHandled == true ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button("Handled item", GUILayout.ExpandWidth (true))) {
					if ( DK_UMACrowd.GenerateHandled == true ) DK_UMACrowd.GenerateHandled = false;
					else DK_UMACrowd.GenerateHandled = true;
				}
			}
		}

		#region Step0
		try {
			if ( DK_UMA_Editor.Step0 &&  !DK_UMA_Editor.MultipleUMASetup 
				&& EditorVariables._RaceLibrary.raceElementList.Length > 0 && EditorVariables._DKSlotLibrary.slotElementList.Length > 0 && EditorVariables._OverlayLibrary.overlayElementList.Length > 0 
			) {
				DK_UMA_Editor.ResetSteps ();
				GUI.color = Color.white;
				EditorGUILayout.HelpBox("Create a single Avatar using some automated process or defining it by yourself.", UnityEditor.MessageType.Info);

				if (  !DK_UMA_Editor.MultipleUMASetup && GUILayout.Button ( "Create a single Avatar", GUILayout.ExpandWidth (true))) {
					EditorVariables.DK_UMACrowd.RaceAndGender.SingleORMulti = true;
					EditorVariables.DK_UMACrowd.RaceAndGender.MultiRace = "One Race";
					DK_UMA_Editor.MultiRace = "";
					DK_UMA_Editor.MultipleUMASetup = false;
					DK_UMA_Editor.Step0 = false ;
					DK_UMA_Editor.Step1 = true ;
					if ( EditorVariables.RaceLibraryObj == null ) EditorVariables.RaceLibraryObj = GameObject.Find("DKRaceLibrary");	
					if ( EditorVariables.RaceLibraryObj != null ) EditorVariables._RaceLibrary =  EditorVariables.RaceLibraryObj.GetComponent<DKRaceLibrary>();
					if ( EditorVariables.DKSlotLibraryObj == null ) EditorVariables.DKSlotLibraryObj = GameObject.Find("DKSlotLibrary");	
					if ( EditorVariables.DKSlotLibraryObj != null ) EditorVariables._DKSlotLibrary =  EditorVariables.DKSlotLibraryObj.GetComponent<DKSlotLibrary>();
					if ( EditorVariables.OverlayLibraryObj == null ) EditorVariables.OverlayLibraryObj = GameObject.Find("DKOverlayLibrary");	
					if ( EditorVariables.OverlayLibraryObj != null ) EditorVariables._OverlayLibrary =  EditorVariables.OverlayLibraryObj.GetComponent<DKOverlayLibrary>();
					//			EditorVariables.DKUMACustomizationObj = GameObject.Find("DKUMACustomization");	
					//			if ( EditorVariables.DKUMACustomizationObj != null ) EditorVariables.DK_DKUMACustomization =  EditorVariables.DKUMACustomizationObj.GetComponent<DKUMACustomization>();

					DK_UMA_Editor.DK_UMA = GameObject.Find("DK_UMA");
					if ( DK_UMA_Editor.DK_UMA == null ) {
						var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
						DK_UMA_Editor.DK_UMA = GameObject.Find("DK_UMA");
					}
					EditorVariables.GeneratorPresets = GameObject.Find("Generator Presets");
					if ( EditorVariables.GeneratorPresets == null ) 
					{
						EditorVariables.GeneratorPresets = (GameObject) Instantiate(Resources.Load("Generator Presets"), Vector3.zero, Quaternion.identity);
						EditorVariables.GeneratorPresets.name = "Generator Presets";
						EditorVariables.GeneratorPresets = GameObject.Find("Generator Presets");
						EditorVariables.GeneratorPresets.transform.parent = DK_UMA_Editor.DK_UMA.transform;
						EditorVariables.PresetToEdit = null;
					}
				}
				EditorGUILayout.HelpBox("Create a crowd of multiple Avatars using some automated process or defining the crowd about the races and the gender of the avatars.", UnityEditor.MessageType.Info);

				if (!DK_UMA_Editor.MultipleUMASetup && GUILayout.Button ( "Create Multiple Avatars", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.MultipleUMASetup = true;
					EditorVariables.DK_UMACrowd.RaceAndGender.SingleORMulti = false;
					if ( EditorVariables.RaceLibraryObj == null ) EditorVariables.RaceLibraryObj = GameObject.Find("DKRaceLibrary");	
					if ( EditorVariables.RaceLibraryObj != null ) EditorVariables._RaceLibrary =  EditorVariables.RaceLibraryObj.GetComponent<DKRaceLibrary>();
					if ( EditorVariables.DKSlotLibraryObj == null ) EditorVariables.DKSlotLibraryObj = GameObject.Find("DKSlotLibrary");	
					if ( EditorVariables.DKSlotLibraryObj != null ) EditorVariables._DKSlotLibrary =  EditorVariables.DKSlotLibraryObj.GetComponent<DKSlotLibrary>();
					if ( EditorVariables.OverlayLibraryObj == null ) EditorVariables.OverlayLibraryObj = GameObject.Find("DKOverlayLibrary");	
					if ( EditorVariables.OverlayLibraryObj != null ) EditorVariables._OverlayLibrary =  EditorVariables.OverlayLibraryObj.GetComponent<DKOverlayLibrary>();
					//			EditorVariables.DKUMACustomizationObj = GameObject.Find("DKUMACustomization");	
					//		if ( EditorVariables.DKUMACustomizationObj != null ) EditorVariables.DK_DKUMACustomization =  EditorVariables.DKUMACustomizationObj.GetComponent<DKUMACustomization>();

					DK_UMA_Editor.DK_UMA = GameObject.Find("DK_UMA");
					if ( DK_UMA_Editor.DK_UMA == null ) {
						var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
						DK_UMA_Editor.DK_UMA = GameObject.Find("DK_UMA");
					}
					EditorVariables.GeneratorPresets = GameObject.Find("Generator Presets");
					if ( EditorVariables.GeneratorPresets == null ) 
					{
						EditorVariables.GeneratorPresets = (GameObject) Instantiate(Resources.Load("Generator Presets"), Vector3.zero, Quaternion.identity);
						EditorVariables.GeneratorPresets.name = "Generator Presets";
						EditorVariables.GeneratorPresets = GameObject.Find("Generator Presets");
						EditorVariables.GeneratorPresets.transform.parent = DK_UMA_Editor.DK_UMA.transform;
						EditorVariables.PresetToEdit = null;
					}
				}
			}
		}catch(System.NullReferenceException){Debug.LogError ("DK UMA is not installed in the current scene. Please open the Welcome tab of the DK UMA Editor and setup the Editor.");}
		if ( !DK_UMA_Editor.MultipleUMASetup && !DK_UMA_Editor.Step0 ) {
			GUI.color = Color.white;
			GUILayout.Label("Create Single", "toolbarbutton", GUILayout.ExpandWidth (true));
		}
		if ( DK_UMA_Editor.MultipleUMASetup )
		{
			GUI.color = Color.white;
			GUILayout.Label("Create Multiple", "toolbarbutton", GUILayout.ExpandWidth (true));
			if ( DK_UMA_Editor.MultiRace == "" )DK_UMA_Editor.MultiRace = "One Race";
			// help
			GUI.color = Color.white;
			GUILayout.Space(5);

			// Crowd
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ( "Crowd", GUILayout.ExpandWidth (false)); 
				string Xsize = EditorVariables.DK_UMACrowd.umaCrowdSize.x.ToString();
				GUI.color = Color.white;
				GUILayout.Label ( "X", GUILayout.ExpandWidth (false)); 
				Xsize= GUILayout.TextField(Xsize, 3, GUILayout.Width (30));
				Xsize= Regex.Replace(Xsize, "[^0-9]", "");
				if ( Xsize == "" ) Xsize = "1";
				EditorVariables.DK_UMACrowd.umaCrowdSize.x = Convert.ToInt32(Xsize);
				string Ysize = EditorVariables.DK_UMACrowd.umaCrowdSize.y.ToString();
				GUILayout.Label ( "Y", GUILayout.ExpandWidth (false));
				Ysize= GUILayout.TextField(Ysize, 3, GUILayout.Width (30));
				Ysize= Regex.Replace(Ysize, "[^0-9]", "");
				if ( Ysize == "" ) Ysize = "1";
				EditorVariables.DK_UMACrowd.umaCrowdSize.y = Convert.ToInt32(Ysize);
				if ( EditorVariables.DK_UMACrowd.umaCrowdSize.x < 1 ) EditorVariables.DK_UMACrowd.umaCrowdSize.x = 1;
				if ( EditorVariables.DK_UMACrowd.umaCrowdSize.y < 1 ) EditorVariables.DK_UMACrowd.umaCrowdSize.y = 1;

				GUI.color = Color.yellow;
				GUILayout.Label ( "Spacing", GUILayout.ExpandWidth (false)); 
				string XSpacing = EditorVariables.DK_UMACrowd.Spacing.x.ToString();
				GUI.color = Color.white;
				GUILayout.Label ( "X", GUILayout.ExpandWidth (false)); 
				XSpacing= GUILayout.TextField(XSpacing, 3, GUILayout.Width (30));
				XSpacing= Regex.Replace(XSpacing, "[^0-9]", "");
				if ( XSpacing == "" ) XSpacing = "1";
				EditorVariables.DK_UMACrowd.Spacing.x = Convert.ToInt32(XSpacing);
				string YSpacing = EditorVariables.DK_UMACrowd.Spacing.y.ToString();
				GUILayout.Label ( "Y", GUILayout.ExpandWidth (false));
				YSpacing= GUILayout.TextField(YSpacing, 3, GUILayout.Width (30));
				YSpacing= Regex.Replace(YSpacing, "[^0-9]", "");
				if ( YSpacing == "" ) YSpacing = "1";
				EditorVariables.DK_UMACrowd.Spacing.y = Convert.ToInt32(YSpacing);
				if ( EditorVariables.DK_UMACrowd.Spacing.x < 1 ) EditorVariables.DK_UMACrowd.Spacing.x = 1;
				if ( EditorVariables.DK_UMACrowd.Spacing.y < 1 ) EditorVariables.DK_UMACrowd.Spacing.y = 1;

			}
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ( "Spawn Frequency :", GUILayout.ExpandWidth (false)); 
				GUI.color = Color.white;

				DK_UMA_Editor.SpawnFrequencySlider = GUILayout.HorizontalSlider(DK_UMA_Editor.SpawnFrequencySlider, 0.005f, 0.5F);
				EditorVariables.DK_UMACrowd.SpawnFrequency = DK_UMA_Editor.SpawnFrequencySlider;
				GUILayout.Label ( EditorVariables.DK_UMACrowd.SpawnFrequency.ToString(), GUILayout.ExpandWidth (false));
			}

			// Race
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label ( "Race :", GUILayout.ExpandWidth (true)); 
				if ( DK_UMA_Editor.MultiRace == "One Race" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if (GUILayout.Button ( "One Race", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.MultiRace = "One Race";
					EditorVariables.DK_UMACrowd.RaceAndGender.MultiRace =DK_UMA_Editor.MultiRace;
				}

				if ( DK_UMA_Editor.MultiRace == "Random for One" ) GUI.color = Green;
				else GUI.color = Color.gray;	
				if (GUILayout.Button ( "Random for One", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.MultiRace = "Random for One";
					EditorVariables.DK_UMACrowd.RaceAndGender.MultiRace = DK_UMA_Editor.MultiRace;
				}	
			}
			GUILayout.Space(15);
			using (new Horizontal()) {
				GUI.color = Color.white;
				if ( DK_UMA_Editor.Step0 && GUILayout.Button ( "Generate now", GUILayout.ExpandWidth (true))) {
					EditorVariables.DK_UMACrowd.RaceAndGender.GenderType = "Random";
					EditorVariables.DK_UMACrowd.RaceAndGender.Gender = "Random";
					if ( EditorVariables.DK_UMACrowd.Colors != null ) EditorVariables.DK_UMACrowd.Colors.RanColors = true;
					EditorVariables.DK_UMACrowd.Randomize.RanShape = true;
					EditorVariables.DK_UMACrowd.Randomize.RanElements = true;
					if ( EditorVariables.DK_UMACrowd.Wears != null ) EditorVariables.DK_UMACrowd.Wears.RanWearAct = true;
					if ( EditorVariables.DK_UMACrowd.Wears != null ) EditorVariables.DK_UMACrowd.Wears.RanWearChoice = true;
					DK_UMA_Editor.Step0 = true ;
					DK_UMA_Editor.Step1 = false ;
					DK_UMA_Editor.MultipleUMASetup = false ;
					EditorVariables.SingleORMulti = true ;
					//	DK_UMACrowd.GeneratorMode = "Preset";
					DK_UMACrowd.OverlayLibraryObj = EditorVariables.OverlayLibraryObj;
					DK_UMACrowd.DKSlotLibraryObj = EditorVariables.DKSlotLibraryObj;
					DK_UMACrowd.RaceLibraryObj = EditorVariables.RaceLibraryObj;
					EditorVariables.DK_UMACrowd.LaunchGenerateUMA();
					EditorVariables.DK_UMACrowd.generateLotsUMA = true;
					EditorVariables.DK_UMACrowd.umaTimerEnd = 0;
					EditorVariables.DK_UMACrowd.RaceAndGender.RaceDone = false;
				}
				if ( DK_UMA_Editor.Step0 && GUILayout.Button ( "Continue to Customize", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.Step0 = false ;
					DK_UMA_Editor.Step1 = true ;
				}
				GUI.color = Red;
				if (GUILayout.Button ( "Cancel", GUILayout.ExpandWidth (true))) {
					EditorVariables.SingleORMulti = true;
					EditorVariables.DK_UMACrowd.RaceAndGender.SingleORMulti = false;
					DK_UMA_Editor.Step0 = true ;
					DK_UMA_Editor.MultipleUMASetup = false;
				}
			}
		}
		#endregion Step0

		#region Step1
		if ( DK_UMA_Editor.Step1 ) {
			// Navigate
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if (GUILayout.Button ( "<", GUILayout.ExpandWidth (false))) 
				{		DK_UMA_Editor.Step0 = true ;	 EditorVariables.SingleORMulti = true; }
				GUI.color = Red;
				if (GUILayout.Button ( "Reset All", GUILayout.ExpandWidth (true))) 
				{		DK_UMA_Editor.Step0 = true ;	 EditorVariables.SingleORMulti = true;	}
				GUI.color = Color.yellow;
			}
			// help
			if ( DK_UMA_Editor.MultiRace == "" ){
				GUI.color = Color.yellow;
				GUILayout.TextField("Select a single Race from the list or let it be randomized" , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.Space(5);
			}
			if ( DK_UMA_Editor.MultiRace == "One Race" ){
				GUI.color = Color.yellow;
				GUILayout.TextField("Select a single Race from the list." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.Space(5);
			}	
			if ( DK_UMA_Editor.MultiRace == "Random for All" )
			{
				GUI.color = Color.yellow;
				GUILayout.TextField("You are creating a crowd with a single Race for all the models, click on the Random Race Button." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.Space(5);
			}
			if ( DK_UMA_Editor.MultiRace == "Random for One" )
			{
				if ( DK_UMA_Editor.Helper )
				{
					GUI.color = Color.yellow;
					GUILayout.TextField("You are creating a crowd with a different Race for every Model. You just have to click on the 'Next Step' button." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					GUILayout.Space(5);
				}
				using (new Horizontal()) {
					GUI.color = Color.white;
					if (GUILayout.Button ( "Next Step" )) {
						DK_UMA_Editor.Step1 = false ;
						DK_UMA_Editor.Step2 = true ;
					}
				}
			}
			GUI.color = Color.white;
			if ( DK_UMA_Editor.MultiRace == "Random for All" || DK_UMA_Editor.MultiRace == "One Race"|| DK_UMA_Editor.MultiRace == ""  ) {
				if ( GUILayout.Button ( "Random Race", GUILayout.ExpandWidth (true))) {

					EditorVariables.DK_UMACrowd.RaceAndGender.RaceToCreate = null;
					EditorVariables.DK_UMACrowd.RaceAndGender.Race = "Random";
					EditorVariables.DK_UMACrowd.RaceAndGender.GenderType = "Random";

					DK_UMA_Editor.Step1 = false ;
					DK_UMA_Editor.Step2 = true ;
				}
			}
			GUILayout.Space(5);
			if ( EditorVariables._RaceLibrary && DK_UMA_Editor.MultiRace == "One Race" || DK_UMA_Editor.MultiRace == "" ) using (new ScrollView(ref scroll)) 
			{
				List<string> _RacesList = new List<string>();
				for(int i = 0; i < EditorVariables._RaceLibrary.raceElementList.Length; i ++){
					try{
						if ( _RacesList.Contains(EditorVariables._RaceLibrary.raceElementList[i].Race) == false 
							&& EditorVariables._RaceLibrary.raceElementList[i].Active ){
							_RacesList.Add(EditorVariables._RaceLibrary.raceElementList[i].Race);
						}
					}catch(NullReferenceException){}
				}
				for(int i = 0; i < _RacesList.Count; i ++){
					using (new Horizontal()) {

						if (GUILayout.Button ( _RacesList[i], GUILayout.ExpandWidth (true))) {
							for(int i2 = 0; i2 < EditorVariables._RaceLibrary.raceElementList.Length; i2 ++){
								if ( EditorVariables._RaceLibrary.raceElementList[i2].Race == _RacesList[i] ) 
								{
									EditorVariables.RaceToCreate = EditorVariables._RaceLibrary.raceElementList[i2];
									EditorVariables.DK_UMACrowd.RaceAndGender.Race = EditorVariables._RaceLibrary.raceElementList[i2].Race;	
								}
							}
							DK_UMA_Editor.Step1 = false ;
							DK_UMA_Editor.Step2 = true ;
						}
					}
				}
			}
		}
		#endregion Step1

		#region Step2
		if ( DK_UMA_Editor.Step2 ) {
			// Navigate
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if (GUILayout.Button ( "<", GUILayout.ExpandWidth (false))) 
				{		DK_UMA_Editor.Step1 = true ;	DK_UMA_Editor.Step2 = false ;	}
				GUI.color = Red;
				if (GUILayout.Button ( "Reset All", GUILayout.ExpandWidth (true))) 
				{		DK_UMA_Editor.Step0 = true ;	}
				GUI.color = Color.yellow;
			}
			// help
			if ( DK_UMA_Editor.Helper )
			{
				GUI.color = Color.yellow;
				GUILayout.TextField("Do you want to create a Completely Random model OR do you want to customize the creation ?" , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.Space(5);
			}
			GUI.color = Color.white;
			if ( EditorVariables.DK_UMACrowd.RaceAndGender.SingleORMulti == true && GUILayout.Button ( "Completely Random model", GUILayout.ExpandWidth (true))) {
				EditorVariables.DK_UMACrowd.RaceAndGender.RaceDone = false;
				DK_UMA_Editor.Step0 = true ;
				EditorVariables.DK_UMACrowd.RaceAndGender.GenderType = "Random";
				EditorVariables.DK_UMACrowd.RaceAndGender.Gender = "Random";
				EditorVariables.DK_UMACrowd.Colors.RanColors = true;
				EditorVariables.DK_UMACrowd.Randomize.RanShape = true;
				EditorVariables.DK_UMACrowd.Wears.RanWearAct = true;
				EditorVariables.DK_UMACrowd.Wears.RanWearChoice = true;
				GameObject DKUMAGeneratorObj = GameObject.Find("DKUMAGenerator");


				DK_UMACrowd.OverlayLibraryObj = EditorVariables.OverlayLibraryObj;
				DK_UMACrowd.DKSlotLibraryObj = EditorVariables.DKSlotLibraryObj;
				DK_UMACrowd.RaceLibraryObj = EditorVariables.RaceLibraryObj;
				EditorVariables.DK_UMACrowd.LaunchGenerateUMA();

				DK_UMA_Editor.CreatingUMA = true;
				if ( DK_UMA_Editor.CreatingUMA == true ) 
				{
					EditorVariables.UMAModel = GameObject.Find("New UMA Model");
					if (EditorVariables.UMAModel != null ) 
					{
						EditorVariables.UMAModel.name = ("New UMA Model (Rename it)");
						EditorVariables.UMAModel.transform.parent = EditorVariables.MFSelectedList.transform;
						DK_UMA_Editor.CreatingUMA = false;
						Selection.activeGameObject = EditorVariables.UMAModel;
					//	EditorVariables.UMAModel.name = EditorVariables.AvatarName;
					}
				}
			}
			if (EditorVariables.DK_UMACrowd.RaceAndGender.SingleORMulti == false && GUILayout.Button ( "Generate Crowd now", GUILayout.ExpandWidth (true))) {
				GameObject DKUMAGeneratorObj = GameObject.Find("DKUMAGenerator");

				EditorVariables.DK_UMACrowd.RaceAndGender.GenderType = "Random";
				EditorVariables.DK_UMACrowd.RaceAndGender.Gender = "Random";
				EditorVariables.DK_UMACrowd.Colors.RanColors = true;
				EditorVariables.DK_UMACrowd.Randomize.RanShape = true;
				EditorVariables.DK_UMACrowd.Randomize.RanElements = true;
				EditorVariables.DK_UMACrowd.Wears.RanWearAct = true;
				EditorVariables.DK_UMACrowd.Wears.RanWearChoice = true;
				DK_UMA_Editor.Step0 = true ;
				DK_UMA_Editor.Step1 = false ;
				DK_UMA_Editor.MultipleUMASetup = false ;
				EditorVariables.SingleORMulti = true ;
				//	DK_UMACrowd.GeneratorMode = "Preset";
				DK_UMACrowd.OverlayLibraryObj = EditorVariables.OverlayLibraryObj;
				DK_UMACrowd.DKSlotLibraryObj = EditorVariables.DKSlotLibraryObj;
				DK_UMACrowd.RaceLibraryObj = EditorVariables.RaceLibraryObj;
				EditorVariables.DK_UMACrowd.generateLotsUMA = true;
				EditorVariables.DK_UMACrowd.umaTimerEnd = 0;

				EditorVariables.DK_UMACrowd.LaunchGenerateUMA();
			}
			if (GUILayout.Button ( "Customize the creation", GUILayout.ExpandWidth (true))) {
				DK_UMA_Editor.Step3 = true ;
				DK_UMA_Editor.Step2 = false ;
			}
		}
		#endregion Step2

		#region Step3
		if ( DK_UMA_Editor.Step3 ) {
			// Navigate
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if (GUILayout.Button ( "<", GUILayout.ExpandWidth (false))) 
				{		DK_UMA_Editor.Step2 = true ;	DK_UMA_Editor.Step3 = false ;	}
				GUI.color = Red;
				if (GUILayout.Button ( "Reset All", GUILayout.ExpandWidth (true))) 
				{		DK_UMA_Editor.Step0 = true ;	}
				GUI.color = Color.yellow;
			}
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label ( "Gender :", GUILayout.ExpandWidth (true)); 
				if ( DK_UMA_Editor.Gender == "Male" ) GUI.color = Green;
				else GUI.color = Color.white;
				if (GUILayout.Button ( "Male", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.Gender = "Male";
					EditorVariables.DK_UMACrowd.RaceAndGender.Gender = DK_UMA_Editor.Gender; EditorVariables.DK_UMACrowd.RaceAndGender.GenderType = "Modified";
					DK_UMA_Editor.Step4 = true ;	DK_UMA_Editor.Step3 = false ;

				}
				if ( DK_UMA_Editor.Gender == "Female" ) GUI.color = Green;
				else GUI.color = Color.white;	
				if (GUILayout.Button ( "Female", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.Gender = "Female";
					EditorVariables.DK_UMACrowd.RaceAndGender.Gender =DK_UMA_Editor. Gender; EditorVariables.DK_UMACrowd.RaceAndGender.GenderType = "Modified";
					DK_UMA_Editor.Step4 = true ;	DK_UMA_Editor.Step3 = false ;

				}
				if ( DK_UMA_Editor.Gender == "" || DK_UMA_Editor.Gender == null ) EditorVariables.DK_UMACrowd.RaceAndGender.GenderType = "Random";
				if ( EditorVariables.DK_UMACrowd.RaceAndGender.Gender == "Random" ) GUI.color = Green;
				else GUI.color = Color.white;	
				if (GUILayout.Button ( "Random", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.Gender = "Random"; 
					EditorVariables.DK_UMACrowd.RaceAndGender.GenderType = "Random";
					EditorVariables.DK_UMACrowd.RaceAndGender.Gender = DK_UMA_Editor.Gender;
					DK_UMA_Editor.Step4 = true ;	DK_UMA_Editor.Step3 = false ;

				}
			}
		}
		#endregion Step3

		#region Step4
		if ( DK_UMA_Editor.Step4 ) {
			#region Step4 Actions
			// Navigate
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if (GUILayout.Button ( "<", GUILayout.ExpandWidth (false))) 
				{		DK_UMA_Editor.Step3 = true ;	DK_UMA_Editor.Step4 = false ;	}
				GUI.color = Red;
				if (GUILayout.Button ( "Reset All", GUILayout.ExpandWidth (true))) 
				{		DK_UMA_Editor.Step0 = true ;	}
				GUI.color = Color.yellow;
			}
			GUILayout.Space(5);
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if ( GUILayout.Button ( "Randomize and go Next", GUILayout.ExpandWidth (true))) 
				{
					EditorVariables.DK_UMACrowd.Colors.RanColors = true;
					DK_UMA_Editor.Step5 = true ;	DK_UMA_Editor.Step4 = false ;
					// TO COMPLET
				}
				GUI.color = Green;
				if ( GUILayout.Button ( "Apply and go Next", GUILayout.ExpandWidth (true))) 
				{
					EditorVariables.DK_UMACrowd.Colors.RanColors = false;
					EditorVariables.DK_UMACrowd.Colors.skinColor = EditorVariables.SkinColor;
					EditorVariables.DK_UMACrowd.Colors.EyesColor = EditorVariables.EyesColor;
					EditorVariables.DK_UMACrowd.Colors.HairColor = EditorVariables.HairColor;
					EditorVariables.DK_UMACrowd.Colors.TorsoWearColor = EditorVariables.TorsoWearColor;
					EditorVariables.DK_UMACrowd.Colors.LegsWearColor = EditorVariables.LegsWearColor;
					EditorVariables.DK_UMACrowd.Colors.FeetWearColor = EditorVariables.FeetWearColor;
					EditorVariables.DK_UMACrowd.Colors.HandWearColor = EditorVariables.HandWearColor;
					EditorVariables.DK_UMACrowd.Colors.HeadWearColor = EditorVariables.HeadWearColor;
					EditorVariables.DK_UMACrowd.Colors.BeltWearColor = EditorVariables.BeltWearColor;

					EditorVariables.DK_UMACrowd.Colors.skinTone = EditorVariables.SkinTone0;
					EditorVariables.DK_UMACrowd.Colors.skinTone1 = EditorVariables.SkinTone1;
					EditorVariables.DK_UMACrowd.Colors.skinTone2 = EditorVariables.SkinTone2;
					EditorVariables.DK_UMACrowd.Colors.skinTone3 = EditorVariables.SkinTone3;

					EditorVariables.DK_UMACrowd.Colors.HairColor1 = EditorVariables.HairColor1;
					EditorVariables.DK_UMACrowd.Colors.HairColor2 = EditorVariables.HairColor2;

					EditorVariables.DK_UMACrowd.Colors.TorsoWearColor1 = EditorVariables.TorsoWearColor1;
					EditorVariables.DK_UMACrowd.Colors.TorsoWearColor2 = EditorVariables.TorsoWearColor2;
					EditorVariables.DK_UMACrowd.Colors.TorsoWearColor3 = EditorVariables.TorsoWearColor3;

					EditorVariables.DK_UMACrowd.Colors.LegsWearColor1 = EditorVariables.LegsWearColor1;
					EditorVariables.DK_UMACrowd.Colors.LegsWearColor2 = EditorVariables.LegsWearColor2;
					EditorVariables.DK_UMACrowd.Colors.LegsWearColor3 = EditorVariables.LegsWearColor3;

					EditorVariables.DK_UMACrowd.Colors.HeadWearColor1 = EditorVariables.HeadWearColor1;
					EditorVariables.DK_UMACrowd.Colors.HeadWearColor2 = EditorVariables.HeadWearColor2;
					EditorVariables.DK_UMACrowd.Colors.HeadWearColor3 = EditorVariables.HeadWearColor3;

					EditorVariables.DK_UMACrowd.Colors.HandWearColor1 = EditorVariables.HandWearColor1;
					EditorVariables.DK_UMACrowd.Colors.HandWearColor2 = EditorVariables.HandWearColor2;
					EditorVariables.DK_UMACrowd.Colors.HandWearColor3 = EditorVariables.HandWearColor3;

					EditorVariables.DK_UMACrowd.Colors.BeltWearColor1 = EditorVariables.BeltWearColor1;
					EditorVariables.DK_UMACrowd.Colors.BeltWearColor2 = EditorVariables.BeltWearColor2;
					EditorVariables.DK_UMACrowd.Colors.BeltWearColor3 = EditorVariables.BeltWearColor3;

					EditorVariables.DK_UMACrowd.Colors.FeetWearColor1 = EditorVariables.FeetWearColor1;
					EditorVariables.DK_UMACrowd.Colors.FeetWearColor2 = EditorVariables.FeetWearColor2;
					EditorVariables.DK_UMACrowd.Colors.FeetWearColor3 = EditorVariables.FeetWearColor3;

					DK_UMA_Editor.Step5 = true ;	DK_UMA_Editor.Step4 = false ;	
					// TO COMPLET

				}

			}
			GUI.color = Color.yellow;
			if ( DK_UMA_Editor.Helper ) GUILayout.TextField("You can randomize only the selected colors category and customize the desired ones. Setup your colors and click on the colors types you want to let it be randomized. " +
				"Don't forget to uncheck the colors type you are customizing." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			using (new Horizontal()) {
				if ( EditorVariables.DK_UMACrowd.Colors.UsePresets == true ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Use Race Presets", GUILayout.ExpandWidth (true))){
					if ( EditorVariables.DK_UMACrowd.Colors.UsePresets == true ) EditorVariables.DK_UMACrowd.Colors.UsePresets = false;
					else EditorVariables.DK_UMACrowd.Colors.UsePresets = true;
				} 
				if ( EditorVariables.DK_UMACrowd.Colors.UseOverlayPresets == true ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Use Overlay Presets", GUILayout.ExpandWidth (true))){
					if ( EditorVariables.DK_UMACrowd.Colors.UseOverlayPresets == true ) EditorVariables.DK_UMACrowd.Colors.UseOverlayPresets = false;
					else EditorVariables.DK_UMACrowd.Colors.UseOverlayPresets = true;
				} 
			}
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label("Randomize", GUILayout.Width (70));
				if ( EditorVariables.DK_UMACrowd.Colors.RanSkin == true ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Skin", GUILayout.ExpandWidth (true))){
					if ( EditorVariables.DK_UMACrowd.Colors.RanSkin == true ) EditorVariables.DK_UMACrowd.Colors.RanSkin = false;
					else EditorVariables.DK_UMACrowd.Colors.RanSkin = true;
				} 
				if ( EditorVariables.DK_UMACrowd.Colors.RanEyes == true ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Eyes", GUILayout.ExpandWidth (true))){
					if ( EditorVariables.DK_UMACrowd.Colors.RanEyes == true ) EditorVariables.DK_UMACrowd.Colors.RanEyes = false;
					else EditorVariables.DK_UMACrowd.Colors.RanEyes = true;
				} 
				if ( EditorVariables.DK_UMACrowd.Colors.RanHair == true ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Hair", GUILayout.ExpandWidth (true))){
					if ( EditorVariables.DK_UMACrowd.Colors.RanHair == true ) EditorVariables.DK_UMACrowd.Colors.RanHair = false;
					else EditorVariables.DK_UMACrowd.Colors.RanHair = true;
				} 
				if ( EditorVariables.DK_UMACrowd.Colors.RanWear == true ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Wear", GUILayout.ExpandWidth (true))){
					if ( EditorVariables.DK_UMACrowd.Colors.RanWear == true ) EditorVariables.DK_UMACrowd.Colors.RanWear = false;
					else EditorVariables.DK_UMACrowd.Colors.RanWear = true;
				} 

			}
			#endregion Step4 Actions

			#region Step4 Help
			GUILayout.Space(5);
			// help
			GUI.color = Color.yellow;
			GUILayout.TextField("Select color of the different parts or let it be random." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			GUILayout.Space(5);
			#endregion Step4 Help

			#region Step4 Menu
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
				if ( DK_UMA_Editor.ShowSkinTone ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Skin Color", "toolbarbutton", GUILayout.ExpandWidth (true))) 
				{	
					DK_UMA_Editor.ShowSkinTone = true;
					DK_UMA_Editor.ShowEyesColor = false;
					DK_UMA_Editor.ShowHairColor = false;
					DK_UMA_Editor.ShowTorsoWColor = false;
				}
				if ( DK_UMA_Editor.ShowEyesColor ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Eyes Color", "toolbarbutton", GUILayout.ExpandWidth (true))) 
				{	
					DK_UMA_Editor.ShowEyesColor = true;
					DK_UMA_Editor.ShowSkinTone = false;
					DK_UMA_Editor.ShowHairColor = false;
					DK_UMA_Editor.ShowTorsoWColor = false;
				}
				if ( DK_UMA_Editor.ShowHairColor ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Hair Color", "toolbarbutton", GUILayout.ExpandWidth (true))) 
				{	
					DK_UMA_Editor.ShowHairColor = true;
					DK_UMA_Editor.ShowSkinTone = false;
					DK_UMA_Editor.ShowEyesColor = false;
					DK_UMA_Editor.ShowTorsoWColor = false;
				}
				if ( DK_UMA_Editor.ShowTorsoWColor ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Wear / Cloth", "toolbarbutton", GUILayout.ExpandWidth (true))) 
				{	
					DK_UMA_Editor.ShowTorsoWColor = true;
					DK_UMA_Editor.ShowSkinTone = false;
					DK_UMA_Editor.ShowEyesColor = false;
					DK_UMA_Editor.ShowHairColor = false;
				}
				GUI.color = Color.white;
				GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
			}
			#endregion Step4 Menu

			#region Step4 Skin Color
			if ( DK_UMA_Editor.ShowSkinTone )
			{
				EditorVariables.SkinTone = new Color ( (EditorVariables.SkinTone0 + EditorVariables.SkinTone1),(EditorVariables.SkinTone0 + EditorVariables.SkinTone2),(EditorVariables.SkinTone0 + EditorVariables.SkinTone3), 1 );
				if ( EditorVariables.SkinColorPresetName == "" || EditorVariables.SkinColorPresetName == null ) EditorVariables.SkinColor = EditorVariables.PickedColor + EditorVariables.SkinTone;
				using (new Horizontal()) {
					GUILayout.Label ( "Final Skin Color", GUILayout.Width (100));
					EditorVariables.SkinColor = EditorGUILayout.ColorField("", EditorVariables.SkinColor, GUILayout.Width (100));
				}
				// title
				GUILayout.Space(5);
				using (new Horizontal()) {
					GUI.color = Color.yellow;
					GUILayout.Label("Skin Color", "toolbarbutton", GUILayout.ExpandWidth (true));
				}

				if ( EditorVariables.SkinColorPresetName == "" || EditorVariables.SkinColorPresetName == null ) {
					using (new Horizontal()) {	
						GUI.color = Color.white;
						if (GUILayout.Button ( "Random", GUILayout.Width (100))) 
						{
							EditorVariables.SkinTone0 = UnityEngine.Random.Range(0.1f, 0.6f);
							EditorVariables.SkinTone1 = UnityEngine.Random.Range(0.35f,0.4f);
							EditorVariables.SkinTone2 = UnityEngine.Random.Range(0.25f,0.4f);
							EditorVariables.SkinTone3 = UnityEngine.Random.Range(0.35f,0.4f);	
						}
						EditorVariables.SkinTone = EditorGUILayout.ColorField("", EditorVariables.SkinTone, GUILayout.Width (100));
					}
					using (new Horizontal()) {
						GUILayout.Label("SkinTone0", GUILayout.ExpandWidth (false));
						EditorVariables.SkinTone0 = GUILayout.HorizontalSlider(EditorVariables.SkinTone0 ,0.05f, 0.6f );
						GUILayout.Label(EditorVariables.SkinTone0.ToString(), GUILayout.Width (50));
					}
					using (new Horizontal()) {
						GUILayout.Label("SkinTone1", GUILayout.ExpandWidth (false));
						EditorVariables.SkinTone1 = GUILayout.HorizontalSlider(EditorVariables.SkinTone1 ,0.35f,0.4f );
						GUILayout.Label(EditorVariables.SkinTone1.ToString(), GUILayout.Width (50));
					}
					using (new Horizontal()) {
						GUILayout.Label("SkinTone2", GUILayout.ExpandWidth (false));
						EditorVariables.SkinTone2 = GUILayout.HorizontalSlider(EditorVariables.SkinTone2 ,0.25f,0.4f );
						GUILayout.Label(EditorVariables.SkinTone2.ToString(), GUILayout.Width (50));
					}	
					using (new Horizontal()) {
						GUILayout.Label("SkinTone3", GUILayout.ExpandWidth (false));
						EditorVariables.SkinTone3 = GUILayout.HorizontalSlider(EditorVariables.SkinTone3 ,0.35f,0.4f );
						GUILayout.Label(EditorVariables.SkinTone3.ToString(), GUILayout.Width (50));
					}
				}
				using (new Horizontal()) {
					if ( EditorVariables.SkinColorPresetName != "" && EditorVariables.SkinColorPresetName != null ) GUI.color = Green;
					else GUI.color = Color.gray;
					if (GUILayout.Button ( "Color Preset", GUILayout.Width (100))) 
					{
						DK_UMA_Editor.OpenColorPresetWin();
						ColorPreset_Editor.Statut = "ApplyTo";
						ColorPreset_Editor.SelectedElement = "Skin";
						ColorPreset_Editor.CurrentElementColor = EditorVariables.SkinColor;

					}
					if ( EditorVariables.SkinColorPresetName != "" && EditorVariables.SkinColorPresetName != null ){
						GUILayout.Label(EditorVariables.SkinColorPresetName, GUILayout.Width (125));
					}
					GUI.color = Red;
					if ( EditorVariables.SkinColorPresetName != "" && EditorVariables.SkinColorPresetName != null 
						&& GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) 
					{
						EditorVariables.SkinColorPresetName = "";
					}
				}
			}
			#endregion Step4 Skin Color

			#region Step4 Eyes Color
			if ( DK_UMA_Editor.ShowEyesColor )
			{
				EditorVariables.EyesTone = new Color ( EditorVariables.EyeOverlayAdjustColor,EditorVariables.EyeOverlayAdjustColor,EditorVariables.EyeOverlayAdjustColor, 1 );
				if ( EditorVariables.EyesColorPresetName == "" || EditorVariables.EyesColorPresetName == null )EditorVariables.EyesColor = EditorVariables.PickedEyesColor  +  EditorVariables.EyesTone;
				using (new Horizontal()) {
					GUILayout.Label ( "Final Eyes Color", GUILayout.Width (100));
					EditorVariables.EyesColor = EditorGUILayout.ColorField("", EditorVariables.EyesColor, GUILayout.Width (100));
				}
				// title
				GUILayout.Space(5);
				using (new Horizontal()) {
					GUI.color = Color.yellow;
					GUILayout.Label("Eyes Color", "toolbarbutton", GUILayout.ExpandWidth (true));
				}

				if ( EditorVariables.EyesColorPresetName == "" || EditorVariables.EyesColorPresetName == null ){
					GUILayout.Space(5);
					// options
					using (new Horizontal()) {	
						GUI.color = Color.white;
						if (GUILayout.Button ( "Random", GUILayout.Width (100))) 
						{
							EditorVariables.EyeOverlayAdjustColor = UnityEngine.Random.Range(0.05f, 0.5f);
						}
						EditorVariables.EyesTone = EditorGUILayout.ColorField("", EditorVariables.EyesTone, GUILayout.Width (100));
					}
					using (new Horizontal()) {
						GUILayout.Label("Eye Adjust Color", GUILayout.ExpandWidth (false));
						EditorVariables.EyeOverlayAdjustColor = GUILayout.HorizontalSlider(EditorVariables.EyeOverlayAdjustColor ,0.05f, 0.5f );
						GUILayout.Label(EditorVariables.EyeOverlayAdjustColor.ToString(), GUILayout.Width (50));
					}
				}
				using (new Horizontal()) {
					if ( EditorVariables.EyesColorPresetName != "" && EditorVariables.EyesColorPresetName != null ) GUI.color = Green;
					else GUI.color = Color.gray;
					if (GUILayout.Button ( "Color Preset", GUILayout.Width (100))) 
					{
						DK_UMA_Editor.OpenColorPresetWin();
						ColorPreset_Editor.Statut = "ApplyTo";
						ColorPreset_Editor.SelectedElement = "Eyes";
						ColorPreset_Editor.CurrentElementColor = EditorVariables.EyesColor;

					}
					if ( EditorVariables.EyesColorPresetName != "" && EditorVariables.EyesColorPresetName != null ){
						GUILayout.Label(EditorVariables.EyesColorPresetName, GUILayout.Width (125));
					}
					GUI.color = Red;
					if ( EditorVariables.EyesColorPresetName != "" && EditorVariables.EyesColorPresetName != null 
						&& GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) 
					{
						EditorVariables.EyesColorPresetName = "";
					}
				}
			}
			#endregion Step4 Eyes Color

			#region Step4 Hair Color
			if ( DK_UMA_Editor.ShowHairColor )
			{
				EditorVariables.HairTone = new Color(EditorVariables.HairColor1,EditorVariables.HairColor2,EditorVariables.HairColor2,1.0f);
				if ( EditorVariables.HairColorPresetName == "" || EditorVariables.HairColorPresetName == null ) EditorVariables.HairColor = EditorVariables.PickedHairColor + EditorVariables.HairTone;
				using (new Horizontal()) {
					GUILayout.Label ( "Final Hair Color", GUILayout.Width (100));
					EditorVariables.HairColor = EditorGUILayout.ColorField("", EditorVariables.HairColor, GUILayout.Width (100));
				}
				// title
				GUILayout.Space(5);
				using (new Horizontal()) {
					GUI.color = Color.yellow;
					GUILayout.Label("Hair Color", "toolbarbutton", GUILayout.ExpandWidth (true));
				}

				if ( EditorVariables.HairColorPresetName == "" || EditorVariables.HairColorPresetName == null ){

					// options
					using (new Horizontal()) {	
						GUI.color = Color.white;
						if (GUILayout.Button ( "Random", GUILayout.Width (100))) 
						{
							EditorVariables.HairColor1 = UnityEngine.Random.Range(0.05f, 0.9f);
							EditorVariables.HairColor2 = UnityEngine.Random.Range(0.05f, 0.9f);
						}
						EditorVariables.HairTone = EditorGUILayout.ColorField("", EditorVariables.HairTone, GUILayout.Width (100));
					}
					using (new Horizontal()) {
						GUILayout.Label("Hair Tone 1", GUILayout.ExpandWidth (false));
						EditorVariables.HairColor1 = GUILayout.HorizontalSlider(EditorVariables.HairColor1 ,0.05f, 0.9f );
						GUILayout.Label(EditorVariables.HairColor1.ToString(), GUILayout.Width (50));
					}
					using (new Horizontal()) {
						GUILayout.Label("Hair Tone 2", GUILayout.ExpandWidth (false));
						EditorVariables.HairColor2 = GUILayout.HorizontalSlider(EditorVariables.HairColor2 ,0.05f, 0.9f );
						GUILayout.Label(EditorVariables.HairColor2.ToString(), GUILayout.Width (50));
					}
				}
				using (new Horizontal()) {
					if ( EditorVariables.HairColorPresetName != "" && EditorVariables.HairColorPresetName != null ) GUI.color = Green;
					else GUI.color = Color.gray;
					if (GUILayout.Button ( "Color Preset", GUILayout.Width (100))) 
					{
						DK_UMA_Editor.OpenColorPresetWin();
						ColorPreset_Editor.Statut = "ApplyTo";
						ColorPreset_Editor.SelectedElement = "Hair";
						ColorPreset_Editor.CurrentElementColor = EditorVariables.HairColor;

					}
					if ( EditorVariables.HairColorPresetName != "" && EditorVariables.HairColorPresetName != null ){
						GUILayout.Label( EditorVariables.HairColorPresetName, GUILayout.Width (125));
					}
					GUI.color = Red;
					if ( EditorVariables.HairColorPresetName != "" && EditorVariables.HairColorPresetName != null 
						&& GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) 
					{
						EditorVariables.HairColorPresetName = "";
					}
				}
			}
			#endregion Step4 Hair Color

			#region Step4 Wear Color
			if ( DK_UMA_Editor.ShowTorsoWColor )
			{
				EditorVariables.TorsoWearTone = new Color( EditorVariables.TorsoWearColor1, EditorVariables.TorsoWearColor2, EditorVariables.TorsoWearColor3,1);
				if (  EditorVariables.TorsoWColorPresetName == "" ||  EditorVariables.TorsoWColorPresetName == null ) EditorVariables.TorsoWearColor =EditorVariables.PickedTorsoWearColor +  EditorVariables.TorsoWearTone;
				EditorVariables.LegsWearTone = new Color(EditorVariables.LegsWearColor1,EditorVariables.LegsWearColor2,EditorVariables.LegsWearColor3,1);
				if ( EditorVariables.LegsWColorPresetName == "" || EditorVariables.LegsWColorPresetName == null  ) EditorVariables.LegsWearColor =EditorVariables.PickedLegsWearColor + EditorVariables.LegsWearTone;
				EditorVariables.FeetWearTone = new Color(EditorVariables.FeetWearColor1,EditorVariables.FeetWearColor2,EditorVariables.FeetWearColor3,1);
				if ( EditorVariables.FeetWColorPresetName == "" || EditorVariables.FeetWColorPresetName == null  ) EditorVariables.FeetWearColor = EditorVariables.PickedFeetWearColor + EditorVariables.FeetWearTone;
				EditorVariables.HeadWearTone = new Color(EditorVariables.HeadWearColor1,EditorVariables.HeadWearColor2,EditorVariables.HeadWearColor3,1);
				if ( EditorVariables.HeadWColorPresetName == "" || EditorVariables.HeadWColorPresetName == null ) EditorVariables.HeadWearColor = EditorVariables.PickedHeadWearColor + EditorVariables.HeadWearTone;
				EditorVariables.HandWearTone = new Color(EditorVariables.HandWearColor1,EditorVariables.HandWearColor2,EditorVariables.HandWearColor3,1);
				if ( EditorVariables.HandWColorPresetName == "" || EditorVariables.HandWColorPresetName == null  ) EditorVariables.HandWearColor = EditorVariables.PickedHandWearColor + EditorVariables.HandWearTone;
				EditorVariables.BeltWearTone = new Color(EditorVariables.BeltWearColor1,EditorVariables.BeltWearColor2,EditorVariables.BeltWearColor3,1);
				if ( EditorVariables.BeltWColorPresetName == "" || EditorVariables.BeltWColorPresetName == null  ) EditorVariables.BeltWearColor = EditorVariables.PickedBeltWearColor + EditorVariables.BeltWearTone;

				using (new Horizontal()) {
					GUILayout.Label ( "Head", GUILayout.Width (50));
					EditorVariables.HeadWearColor = EditorGUILayout.ColorField("", EditorVariables.HeadWearColor, GUILayout.Width (50));
					GUILayout.Label ( "Torso", GUILayout.Width (50));
					EditorVariables.TorsoWearColor = EditorGUILayout.ColorField("", EditorVariables.TorsoWearColor, GUILayout.Width (50));
					GUILayout.Label ( "Leg", GUILayout.Width (50));
					EditorVariables.LegsWearColor = EditorGUILayout.ColorField("", EditorVariables.LegsWearColor, GUILayout.Width (50));
				}

				using (new Horizontal()) {
					GUILayout.Label ( "Belt", GUILayout.Width (50));
					EditorVariables.BeltWearColor = EditorGUILayout.ColorField("", EditorVariables.BeltWearColor, GUILayout.Width (50));
					GUILayout.Label ( "Hand", GUILayout.Width (50));
					EditorVariables.HandWearColor = EditorGUILayout.ColorField("", EditorVariables.HandWearColor, GUILayout.Width (50));
					GUILayout.Label ( "Feet", GUILayout.Width (50));
					EditorVariables.FeetWearColor = EditorGUILayout.ColorField("", EditorVariables.FeetWearColor, GUILayout.Width (50));
				}

				using (new ScrollView(ref scroll)) 
				{
					#region Step4 Head Color
					// title
					GUILayout.Space(5);
					using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label("HeadWear Color", "toolbarbutton", GUILayout.ExpandWidth (true));
					}

					if (EditorVariables.HeadWColorPresetName == "" ||EditorVariables.HeadWColorPresetName == null ){		
						using (new Horizontal()) {	
							GUI.color = Color.white;
							if (GUILayout.Button ( "Random", GUILayout.Width (100))) 
							{
								EditorVariables.HeadWearColor1 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.HeadWearColor2 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.HeadWearColor3 = UnityEngine.Random.Range(0.05f, 0.9f);
							}
							EditorVariables.HeadWearTone = EditorGUILayout.ColorField("",EditorVariables.HeadWearTone, GUILayout.Width (100));
						}
						using (new Horizontal()) {
							GUILayout.Label("HeadWear Tone 1", GUILayout.ExpandWidth (false));
							EditorVariables.HeadWearColor1 = GUILayout.HorizontalSlider(EditorVariables.HeadWearColor1 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.HeadWearColor1.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("HeadWear Tone 2", GUILayout.ExpandWidth (false));
							EditorVariables.HeadWearColor2 = GUILayout.HorizontalSlider(EditorVariables.HeadWearColor2 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.HeadWearColor2.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("HeadWear Tone 3", GUILayout.ExpandWidth (false));
							EditorVariables.HeadWearColor3 = GUILayout.HorizontalSlider(EditorVariables.HeadWearColor3 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.HeadWearColor3.ToString(), GUILayout.Width (50));
						}
					}		
					using (new Horizontal()) {
						if (EditorVariables.HeadWColorPresetName != "" &&EditorVariables.HeadWColorPresetName != null ) GUI.color = Green;
						else GUI.color = Color.gray;
						if (GUILayout.Button ( "Color Preset", GUILayout.Width (100))) 
						{
							DK_UMA_Editor.OpenColorPresetWin();
							ColorPreset_Editor.Statut = "ApplyTo";
							ColorPreset_Editor.SelectedElement = "HeadWear";
							ColorPreset_Editor.CurrentElementColor = EditorVariables.HeadWearColor;

						}
						if (EditorVariables.HeadWColorPresetName != "" &&EditorVariables.HeadWColorPresetName != null ){
							GUILayout.Label(EditorVariables.HeadWColorPresetName, GUILayout.Width (125));
						}
						GUI.color = Red;
						if (EditorVariables.HeadWColorPresetName != "" &&EditorVariables.HeadWColorPresetName != null 
							&& GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) 
						{
							EditorVariables.HeadWColorPresetName = "";
						}
					}

					#endregion Step4 Head Color

					#region Step4 Torso Color
					// title
					GUILayout.Space(5);

					using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label("TorsoWear Color", "toolbarbutton", GUILayout.ExpandWidth (true));
					}
					if (  EditorVariables.TorsoWColorPresetName == "" ||  EditorVariables.TorsoWColorPresetName == null  ){	

						using (new Horizontal()) {	
							GUI.color = Color.white;
							if (GUILayout.Button ( "Random", GUILayout.Width (100))) 
							{
								EditorVariables.TorsoWearColor1 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.TorsoWearColor2 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.TorsoWearColor3 = UnityEngine.Random.Range(0.05f, 0.9f);
							}
							EditorVariables.TorsoWearTone = EditorGUILayout.ColorField("",  EditorVariables.TorsoWearTone, GUILayout.Width (100));
						}
						using (new Horizontal()) {
							GUILayout.Label("TorsoWear Tone 1", GUILayout.ExpandWidth (false));
							EditorVariables.TorsoWearColor1 = GUILayout.HorizontalSlider( EditorVariables.TorsoWearColor1 ,0.05f, 0.9f );
							GUILayout.Label( EditorVariables.TorsoWearColor1.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("TorsoWear Tone 2", GUILayout.ExpandWidth (false));
							EditorVariables.TorsoWearColor2 = GUILayout.HorizontalSlider( EditorVariables.TorsoWearColor2 ,0.05f, 0.9f );
							GUILayout.Label( EditorVariables.TorsoWearColor2.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("TorsoWear Tone 3", GUILayout.ExpandWidth (false));
							EditorVariables.TorsoWearColor3 = GUILayout.HorizontalSlider( EditorVariables.TorsoWearColor3 ,0.05f, 0.9f );
							GUILayout.Label( EditorVariables.TorsoWearColor3.ToString(), GUILayout.Width (50));
						}
					}
					using (new Horizontal()) {
						if (  EditorVariables.TorsoWColorPresetName != "" &&  EditorVariables.TorsoWColorPresetName != null ) GUI.color = Green;
						else GUI.color = Color.gray;
						if (GUILayout.Button ( "Color Preset", GUILayout.Width (100))) 
						{
							DK_UMA_Editor.OpenColorPresetWin();
							ColorPreset_Editor.Statut = "ApplyTo";
							ColorPreset_Editor.SelectedElement = "TorsoWear";
							ColorPreset_Editor.CurrentElementColor = EditorVariables.TorsoWearColor;

						}
						if (  EditorVariables.TorsoWColorPresetName != "" &&  EditorVariables.TorsoWColorPresetName != null ){
							GUILayout.Label( EditorVariables.TorsoWColorPresetName, GUILayout.Width (125));
						}
						GUI.color = Red;
						if (  EditorVariables.TorsoWColorPresetName != "" &&  EditorVariables.TorsoWColorPresetName != null 
							&& GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) 
						{
							EditorVariables.TorsoWColorPresetName = "";
						}
					}
					#endregion Step4 Torso Color

					#region Step4 Hand Color
					// title
					GUILayout.Space(5);

					using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label("HandWear Color", "toolbarbutton", GUILayout.ExpandWidth (true));
					}


					if ( EditorVariables.HandWColorPresetName == "" || EditorVariables.HandWColorPresetName == null  ){
						using (new Horizontal()) {	
							GUI.color = Color.white;
							if (GUILayout.Button ( "Random", GUILayout.Width (100))) 
							{
								EditorVariables.HandWearColor1 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.HandWearColor2 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.HandWearColor3 = UnityEngine.Random.Range(0.05f, 0.9f);
							}
							EditorVariables.HandWearTone = EditorGUILayout.ColorField("", EditorVariables.HandWearTone, GUILayout.Width (100));
						}
						using (new Horizontal()) {
							GUILayout.Label("HandWear Tone 1", GUILayout.ExpandWidth (false));
							EditorVariables.HandWearColor1 = GUILayout.HorizontalSlider(EditorVariables.HandWearColor1 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.HandWearColor1.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("HandWear Tone 2", GUILayout.ExpandWidth (false));
							EditorVariables.HandWearColor2 = GUILayout.HorizontalSlider(EditorVariables.HandWearColor2 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.HandWearColor2.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("HandWear Tone 3", GUILayout.ExpandWidth (false));
							EditorVariables.HandWearColor3 = GUILayout.HorizontalSlider(EditorVariables.HandWearColor3 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.HandWearColor3.ToString(), GUILayout.Width (50));
						}
					}
					using (new Horizontal()) {
						if ( EditorVariables.HandWColorPresetName != "" && EditorVariables.HandWColorPresetName != null ) GUI.color = Green;
						else GUI.color = Color.gray;
						if (GUILayout.Button ( "Color Preset", GUILayout.Width (100))) 
						{
							DK_UMA_Editor.OpenColorPresetWin();
							ColorPreset_Editor.Statut = "ApplyTo";
							ColorPreset_Editor.SelectedElement = "HandWear";
							ColorPreset_Editor.CurrentElementColor = EditorVariables.HandWearColor;
						}
						if ( EditorVariables.HandWColorPresetName != "" && EditorVariables.HandWColorPresetName != null ){
							GUILayout.Label(EditorVariables.HandWColorPresetName, GUILayout.Width (125));
						}
						GUI.color = Red;
						if ( EditorVariables.HandWColorPresetName != "" && EditorVariables.HandWColorPresetName != null 
							&& GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) 
						{
							EditorVariables.HandWColorPresetName = "";
						}
					}	
					#endregion Step4 Hand Color

					#region Step4 Belt Color
					// title
					GUILayout.Space(5);
					using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label("BeltWear Color", "toolbarbutton", GUILayout.ExpandWidth (true));
					}

					if ( EditorVariables.BeltWColorPresetName == "" || EditorVariables.BeltWColorPresetName == null  ){

						using (new Horizontal()) {	
							GUI.color = Color.white;
							if (GUILayout.Button ( "Random", GUILayout.Width (100))) 
							{
								EditorVariables.BeltWearColor1 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.BeltWearColor2 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.BeltWearColor3 = UnityEngine.Random.Range(0.05f, 0.9f);
							}
							EditorVariables.BeltWearTone = EditorGUILayout.ColorField("", EditorVariables.BeltWearTone, GUILayout.Width (100));
						}
						using (new Horizontal()) {
							GUILayout.Label("BeltWear Tone 1", GUILayout.ExpandWidth (false));
							EditorVariables.BeltWearColor1 = GUILayout.HorizontalSlider(EditorVariables.BeltWearColor1 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.BeltWearColor1.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("BeltWear Tone 2", GUILayout.ExpandWidth (false));
							EditorVariables.BeltWearColor2 = GUILayout.HorizontalSlider(EditorVariables.BeltWearColor2 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.BeltWearColor2.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("BeltWear Tone 3", GUILayout.ExpandWidth (false));
							EditorVariables.BeltWearColor3 = GUILayout.HorizontalSlider(EditorVariables.BeltWearColor3 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.BeltWearColor3.ToString(), GUILayout.Width (50));
						}
					}
					using (new Horizontal()) {
						if ( EditorVariables.BeltWColorPresetName != "" && EditorVariables.BeltWColorPresetName != null ) GUI.color = Green;
						else GUI.color = Color.gray;
						if (GUILayout.Button ( "Color Preset", GUILayout.Width (100))) 
						{
							DK_UMA_Editor.OpenColorPresetWin();
							ColorPreset_Editor.Statut = "ApplyTo";
							ColorPreset_Editor.SelectedElement = "BeltWear";
							ColorPreset_Editor.CurrentElementColor = EditorVariables.BeltWearColor;

						}
						if ( EditorVariables.BeltWColorPresetName != "" && EditorVariables.BeltWColorPresetName != null ){
							GUILayout.Label(EditorVariables.BeltWColorPresetName, GUILayout.Width (125));
						}
						GUI.color = Red;
						if ( EditorVariables.BeltWColorPresetName != "" && EditorVariables.BeltWColorPresetName != null 
							&& GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) 
						{
							EditorVariables.BeltWColorPresetName = "";
						}
					}	
					#endregion Step4 Belt Color

					#region Step4 Legs Color
					// title
					GUILayout.Space(5);
					using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label("LegsWear Color", "toolbarbutton", GUILayout.ExpandWidth (true));
					}

					if ( EditorVariables.LegsWColorPresetName == "" || EditorVariables.LegsWColorPresetName == null  ){	

						using (new Horizontal()) {	
							GUI.color = Color.white;
							if (GUILayout.Button ( "Random", GUILayout.Width (100))) 
							{
								EditorVariables.LegsWearColor1 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.LegsWearColor2 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.LegsWearColor3 = UnityEngine.Random.Range(0.05f, 0.9f);
							}
							EditorVariables.LegsWearTone = EditorGUILayout.ColorField("", EditorVariables.LegsWearTone, GUILayout.Width (100));
						}
						using (new Horizontal()) {
							GUILayout.Label("LegsWear Tone 1", GUILayout.ExpandWidth (false));
							EditorVariables.LegsWearColor1 = GUILayout.HorizontalSlider(EditorVariables.LegsWearColor1 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.LegsWearColor1.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("LegsWear Tone 2", GUILayout.ExpandWidth (false));
							EditorVariables.LegsWearColor2 = GUILayout.HorizontalSlider(EditorVariables.LegsWearColor2 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.LegsWearColor2.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("LegsWear Tone 3", GUILayout.ExpandWidth (false));
							EditorVariables.LegsWearColor3 = GUILayout.HorizontalSlider(EditorVariables.LegsWearColor3 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.LegsWearColor3.ToString(), GUILayout.Width (50));
						}
					}
					using (new Horizontal()) {
						if ( EditorVariables.LegsWColorPresetName != "" && EditorVariables.LegsWColorPresetName != null ) GUI.color = Green;
						else GUI.color = Color.gray;
						if (GUILayout.Button ( "Color Preset", GUILayout.Width (100))) 
						{
							DK_UMA_Editor.OpenColorPresetWin();
							ColorPreset_Editor.Statut = "ApplyTo";
							ColorPreset_Editor.SelectedElement = "LegsWear";
							ColorPreset_Editor.CurrentElementColor = EditorVariables.LegsWearColor;

						}
						if ( EditorVariables.LegsWColorPresetName != "" && EditorVariables.LegsWColorPresetName != null ){
							GUILayout.Label(EditorVariables.LegsWColorPresetName, GUILayout.Width (125));
						}
						GUI.color = Red;
						if ( EditorVariables.LegsWColorPresetName != "" && EditorVariables.LegsWColorPresetName != null 
							&& GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) 
						{
							EditorVariables.LegsWColorPresetName = "";
						}
					}
					#endregion Step4 Legs Color

					#region Step4 Feet Color
					// title
					GUILayout.Space(5);
					using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label("FeetWear Color", "toolbarbutton", GUILayout.ExpandWidth (true));
					}

					GUILayout.Space(5);
					if ( EditorVariables.FeetWColorPresetName == "" || EditorVariables.FeetWColorPresetName == null  ){	

						using (new Horizontal()) {	
							GUI.color = Color.white;
							if (GUILayout.Button ( "Random", GUILayout.Width (100))) 
							{
								EditorVariables.FeetWearColor1 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.FeetWearColor2 = UnityEngine.Random.Range(0.05f, 0.9f);
								EditorVariables.FeetWearColor3 = UnityEngine.Random.Range(0.05f, 0.9f);
							}
							EditorVariables.FeetWearTone = EditorGUILayout.ColorField("", EditorVariables.FeetWearTone, GUILayout.Width (100));
						}
						using (new Horizontal()) {
							GUILayout.Label("FeetWear Tone 1", GUILayout.ExpandWidth (false));
							EditorVariables.FeetWearColor1 = GUILayout.HorizontalSlider(EditorVariables.FeetWearColor1 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.FeetWearColor1.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("FeetWear Tone 2", GUILayout.ExpandWidth (false));
							EditorVariables.FeetWearColor2 = GUILayout.HorizontalSlider(EditorVariables.FeetWearColor2 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.FeetWearColor2.ToString(), GUILayout.Width (50));
						}
						using (new Horizontal()) {
							GUILayout.Label("FeetWear Tone 3", GUILayout.ExpandWidth (false));
							EditorVariables.FeetWearColor3 = GUILayout.HorizontalSlider(EditorVariables.FeetWearColor3 ,0.05f, 0.9f );
							GUILayout.Label(EditorVariables.FeetWearColor3.ToString(), GUILayout.Width (50));
						}
					}
					using (new Horizontal()) {
						if ( EditorVariables.FeetWColorPresetName != "" && EditorVariables.FeetWColorPresetName != null ) GUI.color = Green;
						else GUI.color = Color.gray;
						if (GUILayout.Button ( "Color Preset", GUILayout.Width (100))) 
						{
							DK_UMA_Editor.OpenColorPresetWin();
							ColorPreset_Editor.Statut = "ApplyTo";
							ColorPreset_Editor.SelectedElement = "FeetWear";
							ColorPreset_Editor.CurrentElementColor = EditorVariables.FeetWearColor;

						}
						if ( EditorVariables.FeetWColorPresetName != "" && EditorVariables.FeetWColorPresetName != null ){
							GUILayout.Label(EditorVariables.FeetWColorPresetName, GUILayout.Width (125));
						}
						GUI.color = Red;
						if ( EditorVariables.FeetWColorPresetName != "" && EditorVariables.FeetWColorPresetName != null 
							&& GUILayout.Button ( "X", GUILayout.ExpandWidth (false))) 
						{
							EditorVariables.FeetWColorPresetName = "";
						}
					}	
					#endregion Step4 Feet Color
				}
			}
			#endregion Step4 Wear Color
		}
		#endregion Step4

		#region Step5 Shape
		if ( DK_UMA_Editor.Step5 ) {
			// Navigate
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if (GUILayout.Button ( "<", GUILayout.ExpandWidth (false))) 
				{		DK_UMA_Editor.Step4 = true ;	DK_UMA_Editor.Step5 = false ;}
				GUI.color = Red;
				if (GUILayout.Button ( "Reset All", GUILayout.ExpandWidth (true))) 
				{		DK_UMA_Editor.Step0 = true ;	}
				GUI.color = Color.yellow;
			}
			GUILayout.Space(5);
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if ( GUILayout.Button ( "Randomize and go Next", GUILayout.ExpandWidth (true))) 
				{
					EditorVariables.DK_UMACrowd.Randomize.RanShape = true;
					DK_UMA_Editor.Step6 = true ;	DK_UMA_Editor.Step5 = false ;	
				}
				GUI.color = Green;
				if ( GUILayout.Button ( "Apply and go Next", GUILayout.ExpandWidth (true))) 
				{
					DK_UMA_Editor.Step6 = true ;	DK_UMA_Editor.Step5 = false ;	
				}
			}

			GUI.color = Color.white;
			if (DK_UMA_Editor.Helper ) GUILayout.TextField("Setup the shape of your model." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			// Height
			using (new Horizontal()) {
				GUILayout.Label ( "Height :", GUILayout.Width (45));
				if ( EditorVariables.DK_UMACrowd.Randomize.Height == "Random" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Random", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Height = "Random";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Height == "Low" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Low", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Height = "Low";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Height == "Medium" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Height = "Medium";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Height == "High" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Height = "High";
				}

			}
			// Weight
			using (new Horizontal()) {
				GUILayout.Label ( "Weight", GUILayout.Width (45));
				if ( EditorVariables.DK_UMACrowd.Randomize.Weight == "Random" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Random", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Weight = "Random";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Weight == "Low" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Low", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Weight = "Low";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Weight == "Medium" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Weight = "Medium";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Weight == "High" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Weight = "High";
				}

			}
			// Muscles
			using (new Horizontal()) {
				GUILayout.Label ( "Muscles :", GUILayout.Width (45));
				if ( EditorVariables.DK_UMACrowd.Randomize.Muscles == "Random" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Random", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Muscles = "Random";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Muscles == "Low" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Low", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Muscles = "Low";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Muscles == "Medium" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Muscles = "Medium";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Muscles == "High" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Muscles = "High";
				}
			}
			// Hair
			using (new Horizontal()) {
				GUILayout.Label ( "Hair :", GUILayout.Width (45));
				if ( EditorVariables.DK_UMACrowd.Randomize.Hair == "Random" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Random", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Hair = "Random";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Hair == "None" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "None", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Hair = "None";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Hair.Contains("Simple") ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Simple", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Hair = "Simple";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Hair == "Simple+Modules" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "+Modules", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Hair = "Simple+Modules";
				}
			}
			// Pilosity
			using (new Horizontal()) {
				GUILayout.Label ( "Pilosity :", GUILayout.Width (45));
				if ( EditorVariables.DK_UMACrowd.Randomize.Pilosity == "Random" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Random", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Pilosity = "Random";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Pilosity == "None" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "None", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Pilosity = "None";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Pilosity == "Low" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Low", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Pilosity = "Low";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Pilosity == "Medium" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Pilosity = "Medium";
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Pilosity == "High" ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))){
					EditorVariables.DK_UMACrowd.Randomize.Pilosity = "High";
				}
			}
			using (new Horizontal()) {
				// lips
				if ( EditorVariables.DK_UMACrowd.Randomize.Lips == true ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Lips", GUILayout.ExpandWidth (true))){
					if ( EditorVariables.DK_UMACrowd.Randomize.Lips == true ) EditorVariables.DK_UMACrowd.Randomize.Lips = false;
					else EditorVariables.DK_UMACrowd.Randomize.Lips = true;
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Lips == true ){
					GUI.color = Color.white;
					string _value = EditorVariables.DK_UMACrowd.Randomize.LipsChance.ToString();
					_value= GUILayout.TextField(_value, 3, GUILayout.Width (30));
					_value= Regex.Replace(_value, "[^0-9]", "");
					if ( _value == "" ) _value = "1";
					EditorVariables.DK_UMACrowd.Randomize.LipsChance = Convert.ToInt32(_value);	
				}
				// makeup
				if ( EditorVariables.DK_UMACrowd.Randomize.Makeup == true ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Makeup", GUILayout.ExpandWidth (true))){
					if ( EditorVariables.DK_UMACrowd.Randomize.Makeup == true ) EditorVariables.DK_UMACrowd.Randomize.Makeup = false;
					else EditorVariables.DK_UMACrowd.Randomize.Makeup = true;
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Makeup == true ){
					GUI.color = Color.white;
					string _value = EditorVariables.DK_UMACrowd.Randomize.MakeupChance.ToString();
					_value= GUILayout.TextField(_value, 3, GUILayout.Width (30));
					_value= Regex.Replace(_value, "[^0-9]", "");
					if ( _value == "" ) _value = "1";
					EditorVariables.DK_UMACrowd.Randomize.MakeupChance = Convert.ToInt32(_value);	
				}
				// tatoo
				if ( EditorVariables.DK_UMACrowd.Randomize.Tatoo == true ) GUI.color = Green;
				else GUI.color = Color.white;
				if ( GUILayout.Button ( "Tatoo", GUILayout.ExpandWidth (true))){
					if ( EditorVariables.DK_UMACrowd.Randomize.Tatoo == true ) EditorVariables.DK_UMACrowd.Randomize.Tatoo = false;
					else EditorVariables.DK_UMACrowd.Randomize.Tatoo = true;
				}
				if ( EditorVariables.DK_UMACrowd.Randomize.Tatoo == true ){
					GUI.color = Color.white;
					string _value = EditorVariables.DK_UMACrowd.Randomize.TatooChance.ToString();
					_value= GUILayout.TextField(_value, 3, GUILayout.Width (30));
					_value= Regex.Replace(_value, "[^0-9]", "");
					if ( _value == "" ) _value = "1";
					EditorVariables.DK_UMACrowd.Randomize.TatooChance = Convert.ToInt32(_value);	
				}
			}
		}
		#endregion Step5 Shape

		#region Step6 Elements
		if ( DK_UMA_Editor.Step6 ) {
			// Navigate
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if (GUILayout.Button ( "<", GUILayout.ExpandWidth (false))) 
				{		DK_UMA_Editor.Step5 = true ;	DK_UMA_Editor.Step6 = false ;	}
				GUI.color = Red;
				if (GUILayout.Button ( "Reset All", GUILayout.ExpandWidth (true))) 
				{		DK_UMA_Editor.Step0 = true ;	}
				GUI.color = Color.yellow;
			}
			GUILayout.Space(5);
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if ( GUILayout.Button ( "Randomize and go Next", GUILayout.ExpandWidth (true))) 
				{
					EditorVariables.DK_UMACrowd.Randomize.RanElements = true;
					DK_UMA_Editor.Step7 = true ;	
					// TO COMPLET
				}
				GUI.color = Green;
				if ( GUILayout.Button ( "Apply and go Next", GUILayout.ExpandWidth (true))) 
				{
					DK_UMA_Editor.Step7 = true ;	DK_UMA_Editor.Step6 = false ;	
				}
			}
			GUILayout.Label ( "Here you can setup the wear creation.", GUILayout.ExpandWidth (true));
			GUILayout.Space(5);

			using (new ScrollView(ref scroll)) 
			{
				using (new Horizontal()) {
					if ( EditorVariables.DK_UMACrowd.Wears.RanWearChoice == true ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Randomize Wear choice", GUILayout.ExpandWidth (true))) 
					{
						if ( EditorVariables.DK_UMACrowd.Wears.RanWearChoice == true ) EditorVariables.DK_UMACrowd.Wears.RanWearChoice = false;
						else EditorVariables.DK_UMACrowd.Wears.RanWearChoice = true;
					}
				}

				using (new Horizontal()) {
					GUILayout.Label ( "Wear activation :", GUILayout.ExpandWidth (false));
					if ( EditorVariables.DK_UMACrowd.Wears.RanWearYesMax == 0 ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "No", GUILayout.ExpandWidth (false))) 
					{
						EditorVariables.DK_UMACrowd.Wears.RanWearYesMax = 0;
					}
					if ( EditorVariables.DK_UMACrowd.Wears.RanWearYesMax >= 1 ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Yes", GUILayout.ExpandWidth (false))) 
					{
						EditorVariables.DK_UMACrowd.Wears.RanWearYesMax = 1;
					}
					if ( EditorVariables.DK_UMACrowd.Wears.RanWearAct == true ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Randomize", GUILayout.ExpandWidth (false))) 
					{
						if ( EditorVariables.DK_UMACrowd.Wears.RanWearAct == true ) EditorVariables.DK_UMACrowd.Wears.RanWearAct = false;
						else EditorVariables.DK_UMACrowd.Wears.RanWearAct = true;
					}
				}
				using (new Horizontal()) {
					if ( EditorVariables.DK_UMACrowd.Wears.RanWearAct == true ){
						GUILayout.Label ( "Max chance to activate :", GUILayout.ExpandWidth (false));
						string _value = EditorVariables.DK_UMACrowd.Wears.RanWearYesMax.ToString();
						_value= GUILayout.TextField(_value, 3, GUILayout.Width (30));
						_value= Regex.Replace(_value, "[^0-9]", "");
						if ( _value == "" ) _value = "1";
						EditorVariables.DK_UMACrowd.Wears.RanWearYesMax = Convert.ToInt32(_value);	
					}
				}
				using (new Horizontal()) {
					GUILayout.Label ( "Wear types :", GUILayout.ExpandWidth (false));
					if ( EditorVariables.DK_UMACrowd.Wears.RanUnderwearChoice == true ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Underwear", GUILayout.ExpandWidth (true))) 
					{
						if ( EditorVariables.DK_UMACrowd.Wears.RanUnderwearChoice == true ) EditorVariables.DK_UMACrowd.Wears.RanUnderwearChoice = false;
						else EditorVariables.DK_UMACrowd.Wears.RanUnderwearChoice = true;
					}
					if ( EditorVariables.DK_UMACrowd.Wears.WearOverlays == true ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Overlays", GUILayout.ExpandWidth (true))) 
					{
						if ( EditorVariables.DK_UMACrowd.Wears.WearOverlays == true ) EditorVariables.DK_UMACrowd.Wears.WearOverlays = false;
						else EditorVariables.DK_UMACrowd.Wears.WearOverlays = true;
					}
					if ( EditorVariables.DK_UMACrowd.Wears.WearMeshes == true ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Meshes", GUILayout.ExpandWidth (true))) 
					{
						if ( EditorVariables.DK_UMACrowd.Wears.WearMeshes == true ) EditorVariables.DK_UMACrowd.Wears.WearMeshes = false;
						else EditorVariables.DK_UMACrowd.Wears.WearMeshes = true;
					}
				}

				if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count == 6 ) using (new Horizontal()) {
						GUI.color = Color.white;
						GUILayout.Label ( "Wear Weights :", GUILayout.Width (100));
						GUI.color = Color.yellow;
						if ( GUILayout.Button ( "Add all", GUILayout.ExpandWidth (true))) 
						{
							EditorVariables.DK_UMACrowd.Wears.WearWeightList.Clear();
							EditorVariables.DK_UMACrowd.CreateWeights();
						}
						if ( GUILayout.Button ( "Remove All", GUILayout.ExpandWidth (true))) 
						{
							EditorVariables.DK_UMACrowd.RemoveWeights();
						}
					}
				if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count == 6 ) using (new Horizontal()) {
						GUI.color = Color.white;
						GUILayout.Label ( "Add for All :", GUILayout.Width (100));
						GUI.color = Color.yellow;
						if ( GUILayout.Button ( "Light", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Light";
							for (int i = 0; i <  EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count; i ++) {
								if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[i].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[i].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							}

						}
						if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Medium";
							for (int i = 0; i <  EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count; i ++) {
								if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[i].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[i].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							}
						}

						if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "High";
							for (int i = 0; i <  EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count; i ++) {
								if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[i].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[i].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							}
						}

						if ( GUILayout.Button ( "Heavy", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Heavy";
							for (int i = 0; i <  EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count; i ++) {
								if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[i].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[i].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							}
						}
					}
				if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count == 6 ) using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label ( "Head Weight :", GUILayout.Width (100));
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Contains("Light") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Light", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Light";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Remove(DK_UMA_Editor.tmpWearWeight);

						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Contains("Medium") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Medium";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Contains("High") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "High";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Contains("Heavy") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Heavy", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Heavy";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[0].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
					}
				if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count == 6 ) using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label ( "Torso Weight :", GUILayout.Width (100));
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Contains("Light") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Light", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Light";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Remove(DK_UMA_Editor.tmpWearWeight);

						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Contains("Medium") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Medium";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Contains("High") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "High";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Contains("Heavy") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Heavy", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Heavy";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[1].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
					}
				if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count == 6 ) using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label ( "Hands Weight :", GUILayout.Width (100));
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Contains("Light") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Light", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Light";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Remove(DK_UMA_Editor.tmpWearWeight);

						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Contains("Medium") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Medium";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Contains("High") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "High";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Contains("Heavy") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Heavy", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Heavy";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[2].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
					}
				if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count == 6 ) using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label ( "Legs Weight :", GUILayout.Width (100));
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Contains("Light") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Light", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Light";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Contains("Medium") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Medium";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Contains("High") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "High";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Contains("Heavy") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Heavy", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Heavy";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[3].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
					}
				if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count == 6 ) using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label ( "Feet Weight :", GUILayout.Width (100));
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Contains("Light") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Light", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Light";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Contains("Medium") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Medium";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Contains("High") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "High";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Contains("Heavy") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Heavy", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Heavy";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[4].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
					}
				if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList.Count == 6 ) using (new Horizontal()) {
						GUI.color = Color.yellow;
						GUILayout.Label ( "Shoulder Weight :", GUILayout.Width (100));
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Contains("Light") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Light", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Light";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Contains("Medium") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Medium";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Contains("High") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "High";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
						if ( EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Contains("Heavy") == true ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Heavy", GUILayout.ExpandWidth (true))) 
						{
							DK_UMA_Editor.tmpWearWeight = "Heavy";
							if (EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Contains(DK_UMA_Editor.tmpWearWeight) == false ) EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Add(DK_UMA_Editor.tmpWearWeight);
							else EditorVariables.DK_UMACrowd.Wears.WearWeightList[5].Weights.Remove(DK_UMA_Editor.tmpWearWeight);
						}
					}
			}
		}
		#endregion Step6 Elements

		#region Step7 Create
		if ( DK_UMA_Editor.Step7 ) {
			// Navigate
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if (GUILayout.Button ( "<", GUILayout.ExpandWidth (false))) 
				{		DK_UMA_Editor.Step6 = true ;	DK_UMA_Editor.Step7 = false ;	}
				GUI.color = Red;
				if (GUILayout.Button ( "Reset All", GUILayout.ExpandWidth (true))) 
				{		DK_UMA_Editor.Step0 = true ;	}
				GUI.color = Color.yellow;
			}
			GUILayout.Space(5);
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				if (DK_UMA_Editor.MultipleUMASetup && GUILayout.Button ( "Generate Crowd", GUILayout.ExpandWidth (true))) 
				{
					GameObject DKUMAGeneratorObj = GameObject.Find("DKUMAGenerator");

					DK_UMA_Editor.Step0 = true ;
					DK_UMA_Editor.Step1 = false ;
					DK_UMA_Editor.MultipleUMASetup = false ;
					EditorVariables.SingleORMulti = true ;
					//	DK_UMACrowd.GeneratorMode = "Preset";
					DK_UMACrowd.OverlayLibraryObj = EditorVariables.OverlayLibraryObj;
					DK_UMACrowd.DKSlotLibraryObj = EditorVariables.DKSlotLibraryObj;
					DK_UMACrowd.RaceLibraryObj = EditorVariables.RaceLibraryObj;
					EditorVariables.DK_UMACrowd.umaTimerEnd = 0;

					EditorVariables.DK_UMACrowd.generateLotsUMA = true;
					EditorVariables.DK_UMACrowd.LaunchGenerateUMA();
				}
				GUI.color = Green;
				if ( !DK_UMA_Editor.MultipleUMASetup && GUILayout.Button ( "Generate Model", GUILayout.ExpandWidth (true))) 
				{
					GameObject DKUMAGeneratorObj = GameObject.Find("DKUMAGenerator");

					EditorVariables.DK_UMACrowd.RaceAndGender.RaceDone = false;
					DK_UMA_Editor.Step0 = true ;
					DK_UMACrowd.OverlayLibraryObj = EditorVariables.OverlayLibraryObj;
					DK_UMACrowd.DKSlotLibraryObj = EditorVariables.DKSlotLibraryObj;
					DK_UMACrowd.RaceLibraryObj = EditorVariables.RaceLibraryObj;
					EditorVariables.DK_UMACrowd.LaunchGenerateUMA();

					DK_UMA_Editor.CreatingUMA = true;
					if ( DK_UMA_Editor.CreatingUMA == true ) 
					{
						EditorVariables.UMAModel = GameObject.Find("New UMA Model");
						if (EditorVariables.UMAModel != null ) 
						{
							EditorVariables.UMAModel.name = ("New UMA Model (Rename it)");
							EditorVariables.UMAModel.transform.parent =EditorVariables.MFSelectedList.transform;
							DK_UMA_Editor.CreatingUMA = false;
							Selection.activeGameObject = EditorVariables.UMAModel;
						//	EditorVariables.UMAModel.name = EditorVariables.AvatarName;
						}
					}
				}
			}
			using (new Horizontal()) {	
				// Flesh Variation
				GUI.color = Color.white;
				GUILayout.Label("Color Variation :", GUILayout.Width (100));
				GUILayout.Label("Flesh", GUILayout.ExpandWidth (false));
				EditorVariables.DK_UMACrowd.Colors.AdjRanMaxi = GUILayout.HorizontalSlider(EditorVariables.DK_UMACrowd.Colors.AdjRanMaxi ,0,0.5f);
				GUILayout.Label(EditorVariables.DK_UMACrowd.Colors.AdjRanMaxi.ToString(), GUILayout.Width (40));
			}
			using (new Horizontal()) {
				// Hair Variation
				GUILayout.Space(110);
				GUI.color = Color.white;
				GUILayout.Label("Hair", GUILayout.ExpandWidth (false));
				EditorVariables.DK_UMACrowd.Colors.HairAdjRanMaxi = GUILayout.HorizontalSlider(EditorVariables.DK_UMACrowd.Colors.HairAdjRanMaxi ,0,0.5f);
				GUILayout.Label(EditorVariables.DK_UMACrowd.Colors.HairAdjRanMaxi.ToString(), GUILayout.Width (40));
			}
			using (new Horizontal()) {
				// wear Variation
				GUILayout.Space(110);
				GUI.color = Color.white;
				GUILayout.Label("Wear", GUILayout.ExpandWidth (false));
				EditorVariables.DK_UMACrowd.Colors.WearAdjRanMaxi = GUILayout.HorizontalSlider(EditorVariables.DK_UMACrowd.Colors.WearAdjRanMaxi ,0,0.5f);
				GUILayout.Label(EditorVariables.DK_UMACrowd.Colors.WearAdjRanMaxi.ToString(), GUILayout.Width (40));
			}
			GUILayout.Space(5);
			GUI.color = Color.white ;
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ( "Race Library :", GUILayout.Width (110));
				GUI.color = Color.white;
				GUILayout.TextField ( EditorVariables.RaceLibraryObj.name, GUILayout.ExpandWidth (true));
				if ( GUILayout.Button ( "Change", GUILayout.Width (60))){
					DK_UMA_Editor.OpenLibrariesWindow();
					ChangeLibrary.CurrentLibN = EditorVariables.RaceLibraryObj.name;
					ChangeLibrary.CurrentLibrary = EditorVariables.RaceLibraryObj;
					ChangeLibrary.Action = "";
				}
			}
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ( "Slot Library :", GUILayout.Width (110));
				GUI.color = Color.white;
				GUILayout.TextField ( EditorVariables.DKSlotLibraryObj.name, GUILayout.ExpandWidth (true));
				if ( GUILayout.Button ( "Change", GUILayout.Width (60))){
					DK_UMA_Editor.OpenLibrariesWindow();
					ChangeLibrary.CurrentLibN = EditorVariables.DKSlotLibraryObj.name;
					ChangeLibrary.CurrentLibrary = EditorVariables.DKSlotLibraryObj;
					ChangeLibrary.Action = "";

				}
			}
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ( "Overlay Library :", GUILayout.Width (110));
				GUI.color = Color.white;
				GUILayout.TextField ( EditorVariables.OverlayLibraryObj.name, GUILayout.ExpandWidth (true));
				if ( GUILayout.Button ( "Change", GUILayout.Width (60))){
					DK_UMA_Editor.OpenLibrariesWindow();
					ChangeLibrary.CurrentLibN = EditorVariables.OverlayLibraryObj.name;
					ChangeLibrary.CurrentLibrary = EditorVariables.OverlayLibraryObj;
					ChangeLibrary.Action = "";

				}
			}	
			GUILayout.Space(5);
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ( "UMA Crowd :", GUILayout.Width (110));
				GUI.color = Color.white;
				//	GUILayout.TextField ( EditorVariables.UMACrowdObj.name, GUILayout.ExpandWidth (true));
			}
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ( "Generator :", GUILayout.Width (110));
				GUI.color = Color.white;
				//	GUILayout.TextField ( EditorVariables.DKUMAGeneratorObj.name, GUILayout.ExpandWidth (true));
			}	
		}
		#endregion Step7 Create
		#endregion Create buttons list

	}
}
