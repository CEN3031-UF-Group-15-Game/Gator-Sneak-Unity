using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class Expression_Editor : EditorWindow {
	Vector2 scroll;
	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);
	string SearchString = "";
	bool Helper = false;
	
	GameObject DK_UMA;
	GameObject SlotExpressions;
	DK_ExpressionData SelectedExpression;
	DK_UMACrowd _DK_UMACrowd;
	ExpressionLibrary Library;
	string NewExpressionName = "";
	bool ShowSelectAnatomy = false;
	GameObject SelectedAnaPart;

	string Place;
	string OverlayType = "";

	void OnGUI () {
		this.minSize = new Vector2(435, 500);
		
		#region fonts variables
		var bold = new GUIStyle ("label");
		var boldFold = new GUIStyle ("foldout");
		bold.fontStyle = FontStyle.Bold;
		bold.fontSize = 14;
		boldFold.fontStyle = FontStyle.Bold;
		//	var someMatched = false;
		
		var Slim = new GUIStyle ("label");
		Slim.fontStyle = FontStyle.Normal;
		Slim.fontSize = 10;	
		
		var style = new GUIStyle ("label");
		style.wordWrap = true;
		
		#endregion fonts variables
		DK_UMA = GameObject.Find("DK_UMA");
		_DK_UMACrowd = GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>();
		SlotExpressions = GameObject.Find("Expressions");
		if ( DK_UMA == null ) {
			var goDK_UMA = new GameObject();	
			goDK_UMA.name = "DK_UMA";
		}
		if ( SlotExpressions == null ) {
			SlotExpressions = (GameObject) Instantiate(AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Editor/Prefabs/Resources/DKExpressions.prefab", typeof(GameObject)) );
			SlotExpressions.name = "Expressions";
			SlotExpressions.transform.parent = DK_UMA.transform;
			PrefabUtility.ReconnectToLastPrefab(SlotExpressions);
			Library = SlotExpressions.GetComponent<ExpressionLibrary>();
		}
		using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ("Expressions window", "toolbarbutton", GUILayout.ExpandWidth (true));
			if ( Helper ) GUI.color = Green;
			else GUI.color = Color.yellow;
			if ( GUILayout.Button ( "?", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				if ( Helper ) Helper = false;
				else Helper = true;
			}
		}
			GUI.color = Color.white;
			if (Helper)
					GUILayout.TextField ("The Expressions are used during the Auto Detect process to populate your libraries. You can add words to the Expressions, with an Anatomy link.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUI.color = Color.yellow;
			if (Helper)
					GUILayout.TextField ("Write the Expression for the research engine to find during the Auto Detect process.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

		if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DK_ExpressionData") 
					SelectedExpression = Selection.activeObject as DK_ExpressionData;
			
		if ( SelectedExpression ) using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ("Expression :", GUILayout.ExpandWidth (false));
			if (NewExpressionName != "") GUI.color = Green;
			else GUI.color = Red;
			NewExpressionName = GUILayout.TextField (NewExpressionName, 50, GUILayout.ExpandWidth (true));
			if ( GUILayout.Button ("Change", GUILayout.ExpandWidth (false))) {
				Debug.Log ("Expression " + SelectedExpression.name + " changed to " + NewExpressionName + ".");
				SelectedExpression.Name = NewExpressionName;
				string _path = ("Assets/DK Editors/DK_UMA_Editor/Prefabs/Expressions/"+SelectedExpression.name+".asset");
				AssetDatabase.RenameAsset( _path, NewExpressionName );
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			
			}
		}
					
		GUI.color = Color.yellow ;
		if ( Helper && SelectedExpression ) GUILayout.TextField("The Anatomy Part is really important, it is the place where the detected Slot Element will be generated. In most of the cases, a full Model counts only one Anatomy part of each type (Eyes, head...)." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		if ( !ShowSelectAnatomy && SelectedExpression )using (new Horizontal()) {
			GUI.color = Color.white ;
			GUILayout.Label("Anatomy Part :", GUILayout.ExpandWidth (false));
			if ( Place != "" ) GUI.color = Green;
			else GUI.color = Red;
			if ( SelectedExpression == null) Place = "Select an Anatomy Part";
			else if ( SelectedExpression.Place == null ) Place = "Select an Anatomy Part";
			else Place = SelectedExpression.Place.name;
			GUILayout.Label (Place, GUILayout.ExpandWidth (true));
			GUI.color = Color.white ;
			if (  GUILayout.Button ( "Select", GUILayout.ExpandWidth (false))) {
				ShowSelectAnatomy = true;
			}
		}

		// Overlay Type
		GUI.color = Color.yellow;
		if ( Helper ) GUILayout.TextField("The Overlay Type is used by the Generator during the Model's creation. " +
		                                  "All the 'Naked body parts' must be of the 'Flesh' Type, the Head slot must be of the 'Face' type." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		if ( SelectedExpression ) using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Body :", GUILayout.ExpandWidth (false));
			if ( SelectedExpression.OverlayType == "Flesh" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Flesh", GUILayout.ExpandWidth (true))) {
				OverlayType = "Flesh";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "Face" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Face", GUILayout.ExpandWidth (true))) {
				OverlayType = "Face";
				SelectedExpression.OverlayType = OverlayType;
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			
			if ( SelectedExpression.OverlayType == "Eyes" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Eyes", GUILayout.ExpandWidth (true))) {
				OverlayType = "Eyes";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "Hair" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Hair", GUILayout.ExpandWidth (true))) {
				OverlayType = "Hair";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.Elem == true ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "+Elem", GUILayout.ExpandWidth (true))) {
				if ( SelectedExpression.Elem == true ) SelectedExpression.Elem = false;
				else SelectedExpression.Elem = true;
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
		}

		if ( SelectedExpression ) using (new Horizontal()) {	
			if ( SelectedExpression.OverlayType == "Eyebrow" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Eyebrow", GUILayout.ExpandWidth (true))) {
				OverlayType = "Eyebrow";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "Lips" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Lips", GUILayout.ExpandWidth (true))) {
				OverlayType = "Lips";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "Makeup" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Makeup", GUILayout.ExpandWidth (true))) {
				OverlayType = "Makeup";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "Tatoo" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Tatoo", GUILayout.ExpandWidth (true))) {
				OverlayType = "Tatoo";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "Beard" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Beard", GUILayout.ExpandWidth (true))) {
				OverlayType = "Beard";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets (); 
			}
		}

		if ( SelectedExpression ) using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Wears :", GUILayout.ExpandWidth (false));
			if ( SelectedExpression.OverlayType == "TorsoWear" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Torso", GUILayout.ExpandWidth (true))) {
				OverlayType = "TorsoWear";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "ShoulderWear" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Shoulder", GUILayout.ExpandWidth (true))) {
				OverlayType = "ShoulderWear";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "LegsWear" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Leg", GUILayout.ExpandWidth (true))) {
				OverlayType = "LegsWear";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "FeetWear" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Feet", GUILayout.ExpandWidth (true))) {
				OverlayType = "FeetWear";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "HandsWear" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Hand", GUILayout.ExpandWidth (true))) {
				OverlayType = "HandsWear";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "HeadWear" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Head", GUILayout.ExpandWidth (true))) {
				OverlayType = "HeadWear";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
		}

		if ( SelectedExpression ) using (new Horizontal()) {	
			if ( SelectedExpression.OverlayType == "Underwear" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Underwear", GUILayout.ExpandWidth (true))) {
				OverlayType = "Underwear";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.OverlayType == "" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "None", GUILayout.ExpandWidth (true))) {
				OverlayType = "";
				SelectedExpression.OverlayType = OverlayType; 
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
		}

		if ( SelectedExpression && SelectedExpression.OverlayType.Contains("Wear") == true )
		using (new Horizontal()) {
			if ( SelectedExpression && SelectedExpression.Replace == true ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( SelectedExpression && GUILayout.Button ("Replace", GUILayout.ExpandWidth (true))) {
				if ( SelectedExpression.Replace == false ) SelectedExpression.Replace = true;
				else SelectedExpression.Replace = false;
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
		}
		if ( SelectedExpression && SelectedExpression.OverlayType == "HeadWear")
		using (new Horizontal()) {
			if (  SelectedExpression && SelectedExpression.HideHair ) GUI.color = Green;
			else GUI.color = Color.gray;
			if (GUILayout.Button ("Hide Hair", GUILayout.ExpandWidth (true))) {
				if ( SelectedExpression && SelectedExpression.HideHair == true)  SelectedExpression.HideHair = false;
				else  SelectedExpression.HideHair = true;
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
				
			}
			if (  SelectedExpression && SelectedExpression.HideHairModule ) GUI.color = Green;
			else GUI.color = Color.gray;
			if (GUILayout.Button ("Hide Hair Module", GUILayout.ExpandWidth (true))) {
				if ( SelectedExpression && SelectedExpression.HideHairModule == true)  SelectedExpression.HideHairModule = false;
				else  SelectedExpression.HideHairModule = true;
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
				
			}
			if (  SelectedExpression && SelectedExpression.HideMouth ) GUI.color = Green;
			else GUI.color = Color.gray;
			if (GUILayout.Button ("Hide Mouth", GUILayout.ExpandWidth (true))) {
				if ( SelectedExpression && SelectedExpression.HideMouth == true)  SelectedExpression.HideMouth = false;
				else  SelectedExpression.HideMouth = true;
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
				
			}
			if (  SelectedExpression && SelectedExpression.HideEars ) GUI.color = Green;
			else GUI.color = Color.gray;
			if (GUILayout.Button ("Hide Ears", GUILayout.ExpandWidth (true))) {
				if ( SelectedExpression && SelectedExpression.HideEars == true)  SelectedExpression.HideEars = false;
				else  SelectedExpression.HideEars = true;
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
		}	
		
		
		if ( SelectedExpression && OverlayType != null && OverlayType.Contains("Wear") == true && OverlayType != "Underwear" ) using (new Horizontal()) {
			GUI.color = Color.white;
			GUILayout.Label ( "Weight :", GUILayout.ExpandWidth (false));
			if ( SelectedExpression.WearWeight == "Light" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Light", GUILayout.ExpandWidth (true))) {
				SelectedExpression.WearWeight = "Light";
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
			}
			if ( SelectedExpression.WearWeight == "Medium" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Medium", GUILayout.ExpandWidth (true))) {
				SelectedExpression.WearWeight = "Medium";
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
				
			}
			if ( SelectedExpression.WearWeight == "High" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "High", GUILayout.ExpandWidth (true))) {
				SelectedExpression.WearWeight = "High";
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
				
			}
			if ( SelectedExpression.WearWeight == "Heavy" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "Heavy", GUILayout.ExpandWidth (true))) {
				SelectedExpression.WearWeight = "Heavy";
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
				
			}
			if ( SelectedExpression.WearWeight == "" ) GUI.color = Green;
			else GUI.color = Color.gray;
			if ( GUILayout.Button ( "None", GUILayout.ExpandWidth (true))) {
				SelectedExpression.WearWeight = "";
				EditorUtility.SetDirty (SelectedExpression);
				AssetDatabase.SaveAssets ();
				
			}
		}

		// List menu
		if ( !ShowSelectAnatomy )using (new Horizontal()) {
			GUI.color = Color.white ;
			if (  GUILayout.Button ( "Add New", GUILayout.ExpandWidth (true))) {
				// Create
				DK_ExpressionData newExpression = new DK_ExpressionData();
				newExpression.name = "New Expression";
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Editor/Prefabs/Expressions/");
				string _path = ("Assets/DK Editors/DK_UMA_Editor/Prefabs/Expressions/"+newExpression.name+".asset");
				AssetDatabase.CreateAsset(newExpression, _path);
				AssetDatabase.Refresh ();
				Selection.activeObject = newExpression;
				NewExpressionName = SelectedExpression.name;

				// store in library
				SlotExpressions = GameObject.Find("Expressions") ;
				Library = SlotExpressions.GetComponent<ExpressionLibrary>();
				if ( SlotExpressions != null && Library  ) {
					Library.AddExpression (newExpression);
					Debug.Log ("New Expression created and selected.");
				}

			}
			if (  GUILayout.Button ( "Copy Selected", GUILayout.ExpandWidth (true))) {
				if ( SelectedExpression != null )
				{
					// Copy
					System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Editor/Prefabs/Expressions/");
					string _path1 = ("Assets/DK Editors/DK_UMA_Editor/Prefabs/Expressions/"+SelectedExpression.name+".asset");
					string _path2 = ("Assets/DK Editors/DK_UMA_Editor/Prefabs/Expressions/"+SelectedExpression.name+" (Copy).asset");
					AssetDatabase.CopyAsset(_path1, _path2);
					AssetDatabase.Refresh ();
					DK_ExpressionData newExpression = AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Editor/Prefabs/Expressions/"+SelectedExpression.name+" (Copy).asset", _DK_UMACrowd.Default.ResearchDefault.DK_ExpressionData.GetType()) as DK_ExpressionData;
					Selection.activeObject = newExpression;
					NewExpressionName = SelectedExpression.name;
					
					
					// store in library
					if ( SlotExpressions != null && Library  ) {
						Library.AddExpression (newExpression);
						Debug.Log ("New Expression created and selected.");
					}
					Debug.Log (SelectedExpression.name+ " has been copied to " + Library.name+ ".");
				}
				else Debug.Log ("You have to select an Expression from the list to be able to copy it.");
				
			}
			if (  GUILayout.Button ( "Select All", GUILayout.ExpandWidth (true))) {
				for (int i = 0; i < Library.ExpressionList.Length ; i++)
				{
					Library.ExpressionList[i].Selected = true; 
				}
			}
		}


		// Lists
		GUILayout.Space(5);
		// Choose Anatomy Part List
		if ( ShowSelectAnatomy ) 
		{
			using (new Horizontal()) {
				GUI.color = Color.white ;
				GUILayout.Label("Choose Anatomy Part", "toolbarbutton", GUILayout.ExpandWidth (true));
				GUI.color = Red;
				if (  GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
					ShowSelectAnatomy = false;
				}
			}
			GUILayout.Space(5);
			GUI.color = Color.white ;
			using (new Horizontal()) {
				GUILayout.Label("Selected Part :",  GUILayout.ExpandWidth (false));
				GUI.color = Color.yellow ;
				if ( SelectedAnaPart != null ) GUILayout.Label(SelectedAnaPart.name, GUILayout.ExpandWidth (true));
			}
			GUILayout.Space(5);
			GUI.color = Color.white ;
			if ( GUILayout.Button ( "Assign to Expression", GUILayout.ExpandWidth (true))) {
				Debug.Log ( "Anatomy Part " +SelectedAnaPart.name+ " assigned to " +SelectedExpression.name+ ".");
				GameObject _Prefab = PrefabUtility.GetPrefabParent( SelectedAnaPart.gameObject) as GameObject;
				if ( _Prefab ) SelectedExpression.Place = _Prefab;
				else SelectedExpression.Place = SelectedAnaPart.gameObject;
				ShowSelectAnatomy = false;

				EditorUtility.SetDirty(SelectedExpression);
				AssetDatabase.SaveAssets();
			}
			GUILayout.Space(5);
			#region Search
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label("Search for :", GUILayout.ExpandWidth (false));
				SearchString = GUILayout.TextField(SearchString, 100, GUILayout.ExpandWidth (true));
				
			}
			#endregion Search

			DK_SlotsAnatomyLibrary _SlotsAnatomyLibrary = GameObject.Find("DK_SlotsAnatomyLibrary").GetComponent<DK_SlotsAnatomyLibrary>();
			GUILayout.Space(5);
			using (new Horizontal()) {
				GUI.color = Color.cyan ;
				GUILayout.Label("Anatomy Part", "toolbarbutton", GUILayout.Width (200));
				GUILayout.Label("Race", "toolbarbutton", GUILayout.Width (50));
				GUILayout.Label("Gender", "toolbarbutton", GUILayout.Width (50));
				GUILayout.Label("Type", "toolbarbutton", GUILayout.Width (80));
				GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
			}
			using (new ScrollView(ref scroll)) 
			{
				for(int i = 0; i < _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){

					if ( SearchString == "" || _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].name.ToLower().Contains(SearchString.ToLower()) ) 
					using (new Horizontal(GUILayout.Width (80))) {
						// Element
						if (SelectedAnaPart == _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject) GUI.color = Color.yellow ;
						else GUI.color = Color.white ;
						if (GUILayout.Button ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName , "toolbarbutton", GUILayout.Width (200))) {
							SelectedAnaPart = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject;
						}
						// Race
						if ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
							_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
						}
						DK_Race DK_Race;
						DK_Race = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
						if ( DK_Race.Race.Count == 0 ) GUI.color = Red;
						if ( DK_Race.Race.Count == 0 && GUILayout.Button ( "No Race" , "toolbarbutton", GUILayout.Width (50))) {
							
						}
						GUI.color = Green;
						if ( DK_Race.Race.Count == 1 && GUILayout.Button ( DK_Race.Race[0] , "toolbarbutton", GUILayout.Width (50))) {
							
						}
						if ( DK_Race.Race.Count > 1 && GUILayout.Button ( "Multi" , "toolbarbutton", GUILayout.Width (50))) {
							
						}
						// Gender
						if ( DK_Race.Gender == "" ) GUI.color = Red;
						if ( DK_Race.Gender == "" ) GUILayout.Label ( "N" , "Button") ;
						GUI.color = Green;
						if ( DK_Race.Gender != "" && DK_Race.Gender == "Female" ) GUILayout.Label ( "Female" , "toolbarbutton", GUILayout.Width (50) );
						if ( DK_Race.Gender != "" && DK_Race.Gender == "Male" ) GUILayout.Label ( "Male" , "toolbarbutton", GUILayout.Width (50) );
						if ( DK_Race.Gender != "" && DK_Race.Gender == "Both" ) GUILayout.Label ( "Both" , "toolbarbutton", GUILayout.Width (50) );
						
						// OverlayType
						if ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
							_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
						}
						DK_Race = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
						if ( DK_Race.OverlayType == "" ) GUI.color = Color.gray;
						if ( DK_Race.OverlayType == "" && GUILayout.Button ( "/" , "toolbarbutton", GUILayout.Width (80))) {
							
						}
						GUI.color = Green;
						if ( DK_Race.OverlayType != "" && GUILayout.Button ( DK_Race.OverlayType , "toolbarbutton", GUILayout.Width (80))) {
							
						}
					}
				}
			}
		}
		else
		if ( !ShowSelectAnatomy ){ 

			#region Search
		
			using (new Horizontal()) {
				GUI.color = Color.white;
				GUILayout.Label("Search for :", GUILayout.ExpandWidth (false));
				SearchString = GUILayout.TextField(SearchString, 100, GUILayout.ExpandWidth (true));
				
			}
			#endregion Search
			// Expressions List
			using (new Horizontal()) {
				GUI.color = Color.cyan ;
				GUILayout.Label("X", "toolbarbutton", GUILayout.Width (17));
				GUILayout.Label("Expression", "toolbarbutton", GUILayout.Width (145));
				GUILayout.Label("Place", "toolbarbutton", GUILayout.Width (70));
				GUILayout.Label("Type", "toolbarbutton", GUILayout.Width (70));
				GUILayout.Label("Weight", "toolbarbutton", GUILayout.Width (50));
				GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
			}

				Library = SlotExpressions.GetComponent<ExpressionLibrary>();
				using (new ScrollView(ref scroll)) 
				{
					for(int i = 0; i < Library.ExpressionList.Length; i ++){
					{
						DK_ExpressionData Expression = Library.ExpressionList[i];
						if ( Expression && Expression.name.Contains("DefaultExpression") == false)
						using (new Horizontal()) {
							GUI.color = Red;
							if ( Expression && GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
								DestroyImmediate ( Expression ) ;
							}
							if ( SearchString == "" || Library.ExpressionList[i].name.ToLower().Contains(SearchString.ToLower()) ){
								if ( Expression && SelectedExpression && Expression == SelectedExpression ) GUI.color = Color.yellow ;
								else GUI.color = Color.white ;
								if ( Expression && GUILayout.Button (  Expression.name , "toolbarbutton", GUILayout.Width (145))) {
									if ( Expression.Place != null ) Place = Expression.Place.name;
									else Place = "Select an Anatomy part";
									SelectedExpression = Expression;
									Selection.activeObject = SelectedExpression;
									NewExpressionName = Expression.name;
									OverlayType = Expression.OverlayType;
								}
								// Anatomy part
								if ( Expression.Place != null )
									GUILayout.Label(Expression.Place.name, "toolbarbutton", GUILayout.Width (70));
								else {
									GUI.color = Red ;
									GUILayout.Label("Select an Anatomy part for the Expression to be linked.", "toolbarbutton", GUILayout.Width (120));
								}
								// Overlay Type
								if ( Expression.OverlayType != "" )
									GUILayout.Label(Expression.OverlayType, "toolbarbutton", GUILayout.Width (70));
								else 
								{
									GUI.color = Color.gray ;
									GUILayout.Label("No Type.", "toolbarbutton", GUILayout.Width (70));
								}
								// WearWeight
								if ( Expression.WearWeight == "" ) GUI.color = Color.gray ;
								if ( Expression.WearWeight == "" && GUILayout.Button ( "No Weight" , "toolbarbutton", GUILayout.Width (50))) {
									
								}
								GUI.color = Green;
								if ( Expression.WearWeight != "" && GUILayout.Button ( Expression.WearWeight , "toolbarbutton", GUILayout.Width (50))) {
									
								}
								
								// Select
								if ( Expression && Expression.Selected == true ) GUI.color = Green;
								else GUI.color = Color.gray ;
								if (GUILayout.Button ( "Selected", "toolbarbutton" , GUILayout.Width (65))) {
									
									if ( Expression && Expression.Selected == true )Expression.Selected = false;
									else Expression.Selected = true; 
								}

							}
						}
					}
				}
			}
		}
	}

	void OnSelectionChange (){
		if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DK_ExpressionData") 
			SelectedExpression = Selection.activeObject as DK_ExpressionData;
		Repaint ();
	}
}
