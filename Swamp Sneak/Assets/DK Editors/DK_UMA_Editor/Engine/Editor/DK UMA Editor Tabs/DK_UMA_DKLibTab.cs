using UnityEngine;
using System.Collections;
using UnityEditor;

public class DK_UMA_DKLibTab : EditorWindow {
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

		// title
		using (new Horizontal()) {
			GUI.color = Color.white ;
			GUILayout.Label("DK UMA Engine", "toolbarbutton", GUILayout.ExpandWidth (true));
			GUI.color = Red;
			if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				DK_UMA_Editor.ShowDKLibraries = false;
			}
		}
		if ( !DK_UMA_Editor.ShowDKLibSE && !DK_UMA_Editor.ShowDKLibSA )
		{
			if ( DK_UMA_Editor.Helper )
			{
				GUI.color = Color.white;
				GUILayout.TextField("All the DK UMA Libraries need to be correctly populated and configured for the Editor to work properly." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			}

			if ( !DK_UMA_Editor.ShowGenPreset && EditorVariables.SlotsAnatomyLibraryObj != null && EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length != 0 ) 
			{
				GUI.color = Color.yellow;
				if ( DK_UMA_Editor.Helper ) GUILayout.TextField("The Anatomy Slots are used to generate the Model, during the Generation, the engine will use them depending on the Active 'Generator Preset'. Every Anatomy Slot MUST be unique." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				if (  GUILayout.Button ( "Modify Anatomy Slots Library", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OpenAnatomy_Editor();
					DK_UMA_Editor.AnatomyPart = "";
				}
			}
			GUI.color = Color.white;
			if (!DK_UMA_Editor.ShowGenPreset ) 
				EditorGUILayout.HelpBox("Create and modify the Color Presets used by the DK Overlays.", UnityEditor.MessageType.None);
			if ( !DK_UMA_Editor.ShowGenPreset &&  GUILayout.Button ( "Color Presets", GUILayout.ExpandWidth (true))) {
				DK_UMA_Editor.OpenColorPresetWin();
				ColorPreset_Editor.Statut = "";
			}
			GUI.color = Color.yellow;

			if (!DK_UMA_Editor.ShowGenPreset &&  DK_UMA_Editor.Helper )
			{
				GUI.color = Color.white;
				GUILayout.TextField("To create a Model, the DK UMA uses a Generator Preset, gathering the required Slots. You can create and use your own custom Presets." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUI.color = Color.yellow;
				GUILayout.TextField("All the presets can be modified to meet your requirement." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			}
		/*	GUI.color = Color.white;
			if (!DK_UMA_Editor.ShowGenPreset &&  GUILayout.Button ( "Modify Generator Presets", GUILayout.ExpandWidth (true))) {
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
				DK_UMA_Editor.ShowGenPreset = true;
				DK_UMA_Editor.EditGenPreset = true;
			}	
			GUI.color = Color.yellow;
			if (!DK_UMA_Editor.ShowGenPreset &&  DK_UMA_Editor.Helper ) GUILayout.TextField("You can create your own presets, adding the Slots to a specific Library." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			GUI.color = Color.white;
			if ( !DK_UMA_Editor.ShowGenPreset && GUILayout.Button ( "New Generator Preset", GUILayout.ExpandWidth (true))) {
				DK_UMA_Editor.NewPresetName = "New Preset";
				DK_UMA_Editor.ShowGenPreset = true;
				DK_UMA_Editor.NewGenPreset = true;
				for(int i = 0; i < EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
					EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.Selected = false;
				}
			}*/
		}


		if ( DK_UMA_Editor.ShowDKLibSE ){
			DK_UMA_Editor.DK_UMA = GameObject.Find("DK_UMA");
			DK_UMA_Editor.SlotExpressions = GameObject.Find("Slot Expressions");
			if ( DK_UMA_Editor.DK_UMA == null ) {
				var goDK_UMA = new GameObject();	goDK_UMA.name = "DK_UMA";
				var goSlotExpressions = new GameObject();	goSlotExpressions.name = "Slot Expressions";
				goSlotExpressions.transform.parent = goDK_UMA.transform;
			}
			else if ( DK_UMA_Editor.SlotExpressions == null ) {
				DK_UMA_Editor.SlotExpressions = new GameObject();	DK_UMA_Editor.SlotExpressions.name = "Slot Expressions";
				DK_UMA_Editor.SlotExpressions.transform.parent = DK_UMA_Editor.DK_UMA.transform;
			}
			DK_UMA_Editor.SlotExpressions = GameObject.Find("Slot Expressions");

			if ( DK_UMA_Editor.SelectedExpression != null ) 
			{
				DK_UMA_Editor.dk_SlotExpressionsElement = DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
			}

			GUILayout.Space(5);
			using (new Horizontal()) {
				GUI.color = Color.white ;
				GUILayout.Label("Modify Slot Expressions", "toolbarbutton", GUILayout.ExpandWidth (true));
				GUI.color = Red;
				if (  GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
					DK_UMA_Editor.ShowDKLib = false;
					DK_UMA_Editor.ShowDKLibSE = false;
					DK_UMA_Editor.ShowDKLibSA = false;

				}
			}
			GUI.color = Color.white ;
			if ( DK_UMA_Editor.Helper ) GUILayout.TextField("The Expressions are used during the Auto Detect process to populate your libraries. You can add words to the Expressions, with an Anatomy link." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUI.color = Color.yellow ;
			if ( DK_UMA_Editor.Helper ) GUILayout.TextField("Write the Expression for the research engine to find during the Auto Detect process." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			using (new Horizontal()) {
				GUI.color = Color.white ;
				GUILayout.Label("Expression :", GUILayout.ExpandWidth (false));
				if ( DK_UMA_Editor.NewExpressionName != "" ) GUI.color = Green;
				else GUI.color = Red;
				DK_UMA_Editor.NewExpressionName = GUILayout.TextField(DK_UMA_Editor.NewExpressionName, 50, GUILayout.ExpandWidth (true));
				if (  GUILayout.Button ( "Change", GUILayout.ExpandWidth (false))) {
					Debug.Log ( "Expression " +DK_UMA_Editor.SelectedExpression.name+ " changed to " +DK_UMA_Editor.NewExpressionName+ ".");
					DK_UMA_Editor.SelectedExpression.name = DK_UMA_Editor.NewExpressionName;
					DK_UMA_Editor.dk_SlotExpressionsElement.dk_SlotExpressionsElement.dk_SlotExpressionsName = DK_UMA_Editor.NewExpressionName;
					EditorUtility.SetDirty(DK_UMA_Editor.dk_SlotExpressionsElement.gameObject);
					AssetDatabase.SaveAssets();
				}
			}
			GUI.color = Color.yellow ;
			if ( DK_UMA_Editor.Helper ) GUILayout.TextField("The Anatomy Part is really important, it is the place where the detected Slot Element will be generated. In most of the cases, a full Model counts only one Anatomy part of each type (Eyes, head...)." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			if ( !DK_UMA_Editor.ShowSelectAnatomy )using (new Horizontal()) {
					GUI.color = Color.white ;
					GUILayout.Label("Anatomy Part :", GUILayout.ExpandWidth (false));
					if ( DK_UMA_Editor.AnatomyPart != "" ) GUI.color = Green;
					else GUI.color = Red;
					if ( DK_UMA_Editor.SelectedExpression == null) DK_UMA_Editor.AnatomyPart = "";
					else if ( DK_UMA_Editor.dk_SlotExpressionsElement.dk_SlotExpressionsElement.AnatomyPart == null ) DK_UMA_Editor.AnatomyPart = "Select an Anatomy Part";
					else DK_UMA_Editor.AnatomyPart = DK_UMA_Editor.dk_SlotExpressionsElement.dk_SlotExpressionsElement.AnatomyPart.name;
					GUILayout.Label (DK_UMA_Editor.AnatomyPart, GUILayout.ExpandWidth (true));
					GUI.color = Color.white ;
					if (  GUILayout.Button ( "Select", GUILayout.ExpandWidth (false))) {
						DK_UMA_Editor.ShowSelectAnatomy = true;
					}
				}


			// Overlay Type
			GUI.color = Color.yellow;
			if ( DK_UMA_Editor.Helper ) GUILayout.TextField("The Overlay Type is used by the Generator during the Model's creation. " +
				"All the 'Naked body parts' must be of the 'Flesh' Type, the Head slot must be of the 'Face' type." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label ( "Body", GUILayout.ExpandWidth (false));
				if ( DK_UMA_Editor.OverlayType == "Flesh" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Flesh", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Flesh";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "Face" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Face", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Face";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}

				if ( DK_UMA_Editor.OverlayType == "Eyes" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Eyes", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Eyes";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "Hair" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Hair", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Hair";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
			}

			using (new Horizontal()) {	
				if ( DK_UMA_Editor.OverlayType == "Eyebrow" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Eyebrow", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Eyebrow";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "Lips" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Lips", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Lips";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "Makeup" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Makeup", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Makeup";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "Tatoo" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Tatoo", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Tatoo";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "Beard" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Beard", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Beard";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
			}

			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label ( "Wears :", GUILayout.ExpandWidth (false));
				if ( DK_UMA_Editor.OverlayType == "TorsoWear" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Torso", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "TorsoWear";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( GUILayout.Button ( "Shoulder", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "ShoulderWear";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( GUILayout.Button ( "Belt", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "BeltWear";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "LegsWear" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Leg", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "LegsWear";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "FeetWear" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Feet", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "FeetWear";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "HandsWear" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Hand", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "HandsWear";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if ( DK_UMA_Editor.OverlayType == "HeadWear" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Head", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "HeadWear";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
			}
			using (new Horizontal()) {	
				if ( DK_UMA_Editor.OverlayType == "Underwear" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "Underwear", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "Underwear";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = DK_UMA_Editor.OverlayType; 
				}
				if (DK_UMA_Editor.OverlayType == "" ) GUI.color = Green;
				else GUI.color = Color.gray;
				if ( GUILayout.Button ( "None", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OverlayType = "";
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType = ""; 
				}
			}
			// weights
			if ( DK_UMA_Editor.OverlayType.Contains("Wear") == true && DK_UMA_Editor.OverlayType != "Underwear" ) using (new Horizontal()) {
					GUI.color = Color.white;
					GUILayout.Label ( "Weight", GUILayout.ExpandWidth (false));
					if ( EditorVariables.SelectedElementWearWeight == "Light" ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Light", GUILayout.ExpandWidth (true))) {
						EditorVariables.SelectedElementWearWeight = "Light";
						DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
						Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight = EditorVariables.SelectedElementWearWeight; 
					}
					if ( EditorVariables.SelectedElementWearWeight == "Medium" ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))) {
						EditorVariables.SelectedElementWearWeight = "Medium";
						DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
						Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight = EditorVariables.SelectedElementWearWeight; 

					}
					if ( EditorVariables.SelectedElementWearWeight == "High" ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))) {
						EditorVariables.SelectedElementWearWeight = "High";
						DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
						Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight = EditorVariables.SelectedElementWearWeight; 

					}
					if ( EditorVariables.SelectedElementWearWeight == "Heavy" ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "Heavy", GUILayout.ExpandWidth (true))) {
						EditorVariables.SelectedElementWearWeight = "Heavy";
						DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
						Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight = EditorVariables.SelectedElementWearWeight; 

					}
					if ( EditorVariables.SelectedElementWearWeight == "" ) GUI.color = Green;
					else GUI.color = Color.gray;
					if ( GUILayout.Button ( "No", GUILayout.ExpandWidth (true))) {
						EditorVariables.SelectedElementWearWeight = "";
						DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
						Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight = EditorVariables.SelectedElementWearWeight; 

					}
				}
			GUI.color = Color.yellow ;
			if ( !DK_UMA_Editor.ShowSelectAnatomy && DK_UMA_Editor.Helper ) GUILayout.TextField("Here follows a list of the installed Expressions. The 'P' letter is about Prefab, gray seams that the Expression has no Prefab, cyan seams that it is ok. Click on a gray 'P' to create a prefab. The Auto Detect Process will only use the 'Selected' Expression. " , 268, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			// List menu
			if ( !DK_UMA_Editor.ShowSelectAnatomy )using (new Horizontal()) {
					GUI.color = Color.white ;
					if (  GUILayout.Button ( "Add New", GUILayout.ExpandWidth (true))) {
						GameObject NewExpressionObj =  (GameObject) Instantiate(Resources.Load("New_Expression"), Vector3.zero, Quaternion.identity);
						NewExpressionObj.name = "New Expression";
						Selection.activeGameObject = NewExpressionObj;
						DK_UMA_Editor.SelectedExpression = NewExpressionObj;
						if ( DK_UMA_Editor.SlotExpressions != null ) NewExpressionObj.transform.parent = DK_UMA_Editor.SlotExpressions.transform;
						DK_UMA_Editor.NewExpressionName = DK_UMA_Editor.SelectedExpression.name;
						Debug.Log ("New Expression created and selected.");
					}
					if (  GUILayout.Button ( "Copy Selected", GUILayout.ExpandWidth (true))) {
						if ( DK_UMA_Editor.SelectedExpression != null )
						{
							GameObject NewExpressionObj =  (GameObject) Instantiate(DK_UMA_Editor.SelectedExpression, Vector3.zero, Quaternion.identity);
							NewExpressionObj.name = DK_UMA_Editor.SelectedExpression.name + " (Copy)";
							NewExpressionObj.transform.parent = DK_UMA_Editor.SlotExpressions.transform;
							Debug.Log (DK_UMA_Editor.SelectedExpression+ " has been copied to " + NewExpressionObj.name+ ".");
						}
						else Debug.Log ("You have to select an Expression from the list to be able to copy it.");

					}
					if (  GUILayout.Button ( "Select All", GUILayout.ExpandWidth (true))) {
						foreach ( Transform Expression in DK_UMA_Editor.SlotExpressions.transform )
						{
							DK_SlotExpressionsElement Expression_SlotExpressionsElement =  Expression.GetComponent<DK_SlotExpressionsElement>();
							Expression_SlotExpressionsElement.dk_SlotExpressionsElement.Selected = true; 
						}
					}
				}
			// Lists
			GUILayout.Space(5);
			// Choose Anatomy Part List

			if ( DK_UMA_Editor.ShowSelectAnatomy ) 
			{
				using (new Horizontal()) {
					GUI.color = Color.white ;
					GUILayout.Label("Choose Anatomy Part", "toolbarbutton", GUILayout.ExpandWidth (true));
					GUI.color = Red;
					if (  GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
						DK_UMA_Editor.ShowSelectAnatomy = false;
					}
				}
				GUILayout.Space(5);
				GUI.color = Color.white ;
				using (new Horizontal()) {
					GUILayout.Label("Selected Part :",  GUILayout.ExpandWidth (false));
					GUI.color = Color.yellow ;
					if ( DK_UMA_Editor.SelectedAnaPart != null ) GUILayout.Label(DK_UMA_Editor.SelectedAnaPart.name, GUILayout.ExpandWidth (true));
				}
				GUILayout.Space(5);
				GUI.color = Color.white ;
				if ( GUILayout.Button ( "Assign to Expression", GUILayout.ExpandWidth (true))) {
					DK_SlotExpressionsElement Expression_SlotExpressionsElement =  DK_UMA_Editor.SelectedExpression.GetComponent<DK_SlotExpressionsElement>();
					Debug.Log ( "Anatomy Part " +DK_UMA_Editor.SelectedAnaPart.name+ " assigned to " +Expression_SlotExpressionsElement.name+ ".");
					GameObject _Prefab = PrefabUtility.GetPrefabParent(DK_UMA_Editor.SelectedAnaPart.gameObject) as GameObject;
					if ( _Prefab ) Expression_SlotExpressionsElement.dk_SlotExpressionsElement.AnatomyPart = _Prefab;
					else Expression_SlotExpressionsElement.dk_SlotExpressionsElement.AnatomyPart = DK_UMA_Editor.SelectedAnaPart.gameObject;

					DK_UMA_Editor.ShowSelectAnatomy = false;
					EditorUtility.SetDirty(Expression_SlotExpressionsElement.gameObject);
					AssetDatabase.SaveAssets();
				}
				GUILayout.Space(5);
				#region Search
				using (new Horizontal()) {
					GUI.color = Color.white;
					GUILayout.Label("Search for :", GUILayout.ExpandWidth (false));
					DK_UMA_Editor.SearchString = GUILayout.TextField(DK_UMA_Editor.SearchString, 100, GUILayout.ExpandWidth (true));

				}
				#endregion Search

				GUILayout.Space(5);
				using (new Horizontal()) {
					GUI.color = Color.white ;
					GUILayout.Label("Anatomy Part", "toolbarbutton", GUILayout.Width (200));
					GUILayout.Label("Race", "toolbarbutton", GUILayout.Width (60));
					GUILayout.Label("Gender", "toolbarbutton", GUILayout.Width (60));
					GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
				}
				using (new ScrollView(ref DK_UMA_Editor.scroll2)) 
				{
					for(int i = 0; i < EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
						if ( DK_UMA_Editor.SearchString == "" || EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].name.ToLower().Contains(DK_UMA_Editor.SearchString.ToLower()) ) using (new Horizontal(GUILayout.Width (80))) {
								// Element
								GUI.color = Color.white ;
								if (GUILayout.Button ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName , Slim, GUILayout.Width (200))) {
									DK_UMA_Editor.SelectedAnaPart = EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i];
								}
								// Race
								if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
									EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
								}
								DK_Race DK_Race;
								DK_Race = EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
								if ( DK_Race.Race.Count == 0 ) GUI.color = Red;
								if ( DK_Race.Race.Count == 0 && GUILayout.Button ( "No Race" , Slim, GUILayout.Width (60))) {

								}
								GUI.color = Green;
								if ( DK_Race.Race.Count == 1 && GUILayout.Button ( DK_Race.Race[0] , Slim, GUILayout.Width (60))) {

								}
								if ( DK_Race.Race.Count > 1 && GUILayout.Button ( "Multi" , Slim, GUILayout.Width (60))) {

								}
								// Gender
								if ( DK_Race.Gender == "" ) GUI.color = Red;
								if ( DK_Race.Gender == "" ) GUILayout.Label ( "N" , "Button") ;
								GUI.color = Green;
								if ( DK_Race.Gender != "" && DK_Race.Gender == "Female" ) GUILayout.Label ( "Female" , Slim, GUILayout.Width (50) );
								if ( DK_Race.Gender != "" && DK_Race.Gender == "Male" ) GUILayout.Label ( "Male" , Slim, GUILayout.Width (50) );
								if ( DK_Race.Gender != "" && DK_Race.Gender == "Both" ) GUILayout.Label ( "Both" , Slim, GUILayout.Width (50) );

								// OverlayType
								if ( EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
									EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
								}
								DK_Race = EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
								if ( DK_Race.OverlayType == "" ) GUI.color = Red;
								if ( DK_Race.OverlayType == "" && GUILayout.Button ( "No OverlayType" , Slim, GUILayout.Width (50))) {

								}
								GUI.color = Green;
								if ( DK_Race.OverlayType != "" && GUILayout.Button ( DK_Race.OverlayType , Slim, GUILayout.Width (50))) {

								}
							}
					}
				}
			}
			else

				#region Search

				if ( !DK_UMA_Editor.ShowSelectAnatomy ){
					using (new Horizontal()) {
						GUI.color = Color.white;
						GUILayout.Label("Search for :", GUILayout.ExpandWidth (false));
						DK_UMA_Editor.SearchString = GUILayout.TextField(DK_UMA_Editor.SearchString, 100, GUILayout.ExpandWidth (true));

					}
					#endregion Search
					// Expressions List
					using (new Horizontal()) {
						GUI.color = Color.white ;
						GUILayout.Label("Expression", "toolbarbutton", GUILayout.Width (145));
						GUILayout.Label("Anatomy Part", "toolbarbutton", GUILayout.Width (120));
						GUILayout.Label("Overlay Type", "toolbarbutton", GUILayout.Width (90));
						GUILayout.Label("Weight", "toolbarbutton", GUILayout.Width (90));
						GUILayout.Label("Selected", "toolbarbutton", GUILayout.Width (70));
						GUILayout.Label("Delete", "toolbarbutton", GUILayout.ExpandWidth (true));
					}
					using (new ScrollView(ref scroll)) 
					{
						foreach ( Transform Expression in DK_UMA_Editor.SlotExpressions.transform )
						{
							DK_SlotExpressionsElement Expression_SlotExpressionsElement =  Expression.GetComponent<DK_SlotExpressionsElement>();
							if ( DK_UMA_Editor.SearchString == "" || Expression.name.ToLower().Contains(DK_UMA_Editor.SearchString.ToLower()) )using (new Horizontal()) {
									// Prefab Verification
									string myPath = PrefabUtility.GetPrefabParent( Expression.gameObject ).ToString() ;
									if ( myPath == "null" ) GUI.color = Color.gray;
									else GUI.color = Color.cyan;
									if ( GUILayout.Button ( "P", "toolbarbutton", GUILayout.ExpandWidth (false))) {
										// Create Prefab
										if ( myPath == "null" ) 
										{
											PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/DK_SlotExpressions/" + Expression.name + ".prefab", Expression.gameObject );
											GameObject clone = PrefabUtility.InstantiateAttachedAsset( AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Editor/Prefabs/DK_SlotExpressions/"+ Expression.name + ".prefab", typeof(GameObject)) ) as GameObject;										
											clone.name = Expression.name;
											clone.transform.parent = Expression.parent;
											DestroyImmediate ( Expression.gameObject ) ;
										}
									}
									if ( Expression && DK_UMA_Editor.SelectedExpression && Expression == DK_UMA_Editor.SelectedExpression.transform ) GUI.color = Color.yellow ;
									else GUI.color = Color.white ;
									if ( Expression && GUILayout.Button (  Expression.name , Slim, GUILayout.Width (120))) {
										if ( Expression_SlotExpressionsElement.dk_SlotExpressionsElement.AnatomyPart != null ) DK_UMA_Editor.AnatomyPart = Expression_SlotExpressionsElement.dk_SlotExpressionsElement.AnatomyPart.name;
										else DK_UMA_Editor.AnatomyPart = "Select an Anatomy part";
										DK_UMA_Editor.SelectedExpression = Expression.gameObject;
										Selection.activeGameObject = DK_UMA_Editor.SelectedExpression;
										DK_UMA_Editor.NewExpressionName = Expression.name;
										DK_UMA_Editor.OverlayType = Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType;
										EditorVariables.SelectedElementWearWeight = Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight;
									}
									// Anatomy part
									if ( Expression_SlotExpressionsElement.dk_SlotExpressionsElement.AnatomyPart != null )
										GUILayout.Label(Expression_SlotExpressionsElement.dk_SlotExpressionsElement.AnatomyPart.name, Slim, GUILayout.Width (120));
									else GUILayout.Label("Select an Anatomy part for the Expression to be linked.", Slim, GUILayout.Width (120));
									// Overlay Type
									if ( Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType != "" )
										GUILayout.Label(Expression_SlotExpressionsElement.dk_SlotExpressionsElement.OverlayType, Slim, GUILayout.Width (90));
									else 
									{
										GUI.color = Color.yellow ;
										GUILayout.Label("Not an Overlay.", Slim, GUILayout.Width (90));
									}
									// WearWeight
									if ( Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight == "" ) GUI.color = Red;
									if ( Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight == "" && GUILayout.Button ( "No Weight" , Slim, GUILayout.Width (50))) {

									}
									GUI.color = Green;
									if ( Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight != "" && GUILayout.Button ( Expression_SlotExpressionsElement.dk_SlotExpressionsElement.WearWeight , Slim, GUILayout.Width (50))) {

									}

									// Select
									if ( Expression && Expression_SlotExpressionsElement.dk_SlotExpressionsElement.Selected == true ) GUI.color = Green;
									else GUI.color = Color.gray ;
									if (GUILayout.Button ( "Selected" , GUILayout.Width (65))) {

										if ( Expression && Expression_SlotExpressionsElement.dk_SlotExpressionsElement.Selected == true )Expression_SlotExpressionsElement.dk_SlotExpressionsElement.Selected = false;
										else Expression_SlotExpressionsElement.dk_SlotExpressionsElement.Selected = true; 
									}
									GUI.color = Red;
									if ( Expression && GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
										DestroyImmediate ( Expression.gameObject ) ;
									}
								}
						}
					}
				}
		}
	}
}
