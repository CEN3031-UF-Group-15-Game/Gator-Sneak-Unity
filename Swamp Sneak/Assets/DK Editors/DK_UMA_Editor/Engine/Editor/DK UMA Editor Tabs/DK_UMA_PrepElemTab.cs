using UnityEngine;
using System.Collections;
using UnityEditor;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0472 // Null is true.
#pragma warning disable 0649 // Never Assigned.

public class DK_UMA_PrepElemTab : EditorWindow {
	public static Color Green = new Color (0.8f, 1f, 0.8f, 1);
	public static Color Red = new Color (0.9f, 0.5f, 0.5f);

	public static Vector2 scroll;

	public static bool DisplayTextures = false; 
	public static bool DisplayConfiguration = true; 
	public static bool DisplayOverlayTypes = true; 
	public static bool DisplayUMA = false; 


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

		#region Prepare

		if (EditorVariables.RaceLibraryObj == null || EditorVariables.DKSlotLibraryObj == null || EditorVariables.OverlayLibraryObj == null){
			DetectAndAddDK.AddAll ();
		}
		else if (EditorVariables._RaceLibrary == null){
			Debug.Log ("DetectAndAddDK.DetectAll ();");
			DetectAndAddDK.DetectAll ();
		}
		if (Selection.activeObject 
			&& (Selection.activeObject 
				&& (Selection.activeObject.GetType ().ToString () == "DKRaceData"
					|| Selection.activeObject.GetType ().ToString () == "DKSlotData"
					|| Selection.activeObject.GetType ().ToString () == "DKOverlayData")))
			using (new ScrollView(ref scroll)) {
				// Race Assign
				if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DKRaceData") {
					for (int i = 0; i < EditorVariables._RaceLibrary.raceElementList.Length; i ++) {
						if (EditorVariables._RaceLibrary.raceElementList [i] == Selection.activeObject) {
							DKRaceData SelectedData = EditorVariables._RaceLibrary.raceElementList [i];
							EditorVariables.SelectedElementObj = Selection.activeObject as GameObject;
						}
					}
				}
				// Slot assign
				if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DKSlotData") {
					for (int i = 0; i < EditorVariables._DKSlotLibrary.slotElementList.Length; i ++) {
						if (EditorVariables._DKSlotLibrary.slotElementList [i] == Selection.activeObject) {
							DKSlotData SelectedData = EditorVariables._DKSlotLibrary.slotElementList [i];
							EditorVariables.SelectedElementObj = Selection.activeObject as GameObject;
							EditorVariables.SelectedElementOverlayType = SelectedData.OverlayType;
						}
					}
				}
				// Overlay Assign
				if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DKOverlayData") {
					for (int i = 0; i < EditorVariables._OverlayLibrary.overlayElementList.Length; i ++) {
						if (EditorVariables._OverlayLibrary.overlayElementList [i] == Selection.activeObject) {
							DKOverlayData SelectedData = EditorVariables._OverlayLibrary.overlayElementList [i];
							EditorVariables.SelectedElementObj = Selection.activeObject as GameObject;
						}
					}
				}

				GUILayout.Space (5);

				#region Textures	
				if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DKOverlayData") {
					GUI.color = Color.white;
					using (new Horizontal()) {
						if (GUILayout.Button ("Textures", "toolbarbutton", GUILayout.ExpandWidth (true))) {
							DisplayTextures = !DisplayTextures;
						}
						if ( DisplayTextures ) GUI.color = Green;
						else GUI.color = Color.gray;
						if (GUILayout.Button ("show Textures", "toolbarbutton", GUILayout.Width (150))) {
							DisplayTextures = !DisplayTextures;
						}
					}
					if ( DisplayTextures ){
						if (DK_UMA_Editor.Helper) GUILayout.TextField ("The textures have to be 'Read/Write Enabled'.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

						if (( Selection.activeObject as DKOverlayData ).textureList != null ){
							for (int i1 = 0; i1 < (Selection.activeObject as DKOverlayData ).textureList.Length; i1 ++) {
								var obj = (Selection.activeObject as DKOverlayData).textureList [i1];
								string file = AssetDatabase.GetAssetPath (obj);
								var importer = TextureImporter.GetAtPath (file) as TextureImporter;
								if ( obj != null ){										
									using (new Horizontal()) {
										// launch readable
										GUI.color = Color.white;
										GUILayout.TextField (obj.name, GUILayout.Width (300));
									//	GUILayout.TextField (file, GUILayout.Width (150));
										if (!importer.isReadable) {
											GUI.color = Color.yellow;
											if (GUILayout.Button ("Make readable", GUILayout.Width (110))) {
												importer.isReadable = true;
												AssetDatabase.ImportAsset (file);
											}
										} 
										else if (GUILayout.Button ("Go to", GUILayout.Width (110))) {
											Selection.activeObject = obj;
										}
									}
								}
								else {
									GUI.color = Red;
									GUILayout.Label ("The UMA element is not installed ! Install it to your project.", GUILayout.Width (350));
								}
							}
						}
					}
				}
				#endregion Textures

				//This draws a Line to seperate the Controls
				GUI.color = Color.white;
				GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));
				using (new Horizontal()) {
					if (GUILayout.Button ("Configure the '"+Selection.activeObject.GetType ().ToString ()+"' Element", "toolbarbutton", GUILayout.ExpandWidth (true))) {
						DisplayConfiguration = !DisplayConfiguration;
					}
					if ( DisplayConfiguration ) GUI.color = Green;
					else GUI.color = Color.gray;
					if (GUILayout.Button ("show Configuration", "toolbarbutton", GUILayout.Width (150))) {
						DisplayConfiguration = !DisplayConfiguration;
					}
				}					
				if ( DisplayConfiguration ){
					if (Selection.activeObject.GetType ().ToString () == "DKSlotData"
						|| Selection.activeObject.GetType ().ToString () == "DKOverlayData"
						|| Selection.activeObject.GetType ().ToString () == "DKRaceData")

						using (new Horizontal()) {
						//	if (Selection.activeObject.GetType ().ToString () == "DKSlotData" ) 
								// Slot Preview
							/*	using (new Vertical()) {
									Texture2D Preview = null;
									Sprite _Sprite = null;
									if (Preview == null ){
										string path = AssetDatabase.GetAssetPath ((Selection.activeObject as DKSlotData));
										path = path.Replace ((Selection.activeObject as DKSlotData).name+".asset", "");
										Preview = AssetDatabase.LoadAssetAtPath(path+"Preview-"+(Selection.activeObject as DKSlotData).name+".asset", typeof(Texture2D) ) as Texture2D;
										// load sprite
										if ( Preview == null ){
											_Sprite = AssetDatabase.LoadAssetAtPath(path+"Sprite-"+(Selection.activeObject as DKSlotData).name+".png", typeof(Sprite) ) as Sprite;
										}
									}
									if ( Preview != null ){
										GUI.color = Color.white ;
										if ( GUILayout.Button( Preview ,GUILayout.Width (50), GUILayout.Height (50))){								
										}
									}
									else if ( _Sprite != null ){
										GUI.color = Color.white ;
										if ( GUILayout.Button( _Sprite.texture ,GUILayout.Width (50), GUILayout.Height (50))){								
										}
									}
									else {
										GUI.color = Color.black ;
										if ( GUILayout.Button( Preview ,GUILayout.Width (50), GUILayout.Height (50))){								
										}
									}
								}*/

							#region Element Name & Linked UMA

							using (new Vertical()) {
								using (new Horizontal()) {

									GUI.color = Color.white;
									GUILayout.Label ("Name", GUILayout.ExpandWidth (false));
									if (EditorVariables.UMAObj == null) {
										DK_UMA_Editor.UMAObjName = EditorVariables.UMAObjDefault;
										EditorVariables.UMAObj = GameObject.Find (DK_UMA_Editor.UMAObjName);
									}
								/*	EditorVariables.SelectedElementName = GUILayout.TextField ( EditorVariables.SelectedElementName, 50, GUILayout.Width (150));
									GUILayout.Label ("UMA", GUILayout.ExpandWidth (false));						
									EditorVariables.SelectedElementName = GUILayout.TextField ( EditorVariables.SelectedElementName, 50, GUILayout.Width (150));
	*/
									EditorVariables.SelectedElementName = GUILayout.TextField ( EditorVariables.SelectedElementName, 50, GUILayout.Width (150));
									GUILayout.Label ("UMA link", GUILayout.ExpandWidth (false));	
									if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DKOverlayData") {
									(Selection.activeObject as DKOverlayData)._UMA = 
											(UMA.OverlayDataAsset)EditorGUILayout.ObjectField("",
												(Selection.activeObject as DKOverlayData)._UMA,typeof(UMA.OverlayDataAsset),false, GUILayout.Width (200));
									}
									else if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DKSlotData") {
										(Selection.activeObject as DKSlotData)._UMA = 
											(UMA.SlotDataAsset)EditorGUILayout.ObjectField("",
												(Selection.activeObject as DKSlotData)._UMA,typeof(UMA.SlotDataAsset),false, GUILayout.Width (200));
									}
								}


								if (Selection.activeObject.GetType ().ToString () == "DKSlotData") {
									DKSlotData DKslot = Selection.activeObject as DKSlotData;
									if ( DKslot._UMA != null && DKslot._UMA.material != null && DKslot._UMA.material.clothProperties != null ){
										GUI.color = Color.white;
										if (GUILayout.Button ("Physics Slot of the Element", "toolbarbutton", GUILayout.ExpandWidth (true))) {}
										EditorGUILayout.HelpBox("This linked UMA element seams to use Cloth Physics. You can assign the corresponding Physics UMA slot or let it use the standard one if left blank.", UnityEditor.MessageType.None);

										using (new Horizontal()) {
											UMA.SlotDataAsset tmpSlot = DKslot.PhysicsUMASlot;
											GUILayout.Label ("Cloth Physics Slot :", GUILayout.Width (110));
											DKslot.PhysicsUMASlot = (UMA.SlotDataAsset)EditorGUILayout.ObjectField("",
												DKslot.PhysicsUMASlot,typeof(UMA.SlotDataAsset),false, GUILayout.Width (200));
											if ( tmpSlot != DKslot.PhysicsUMASlot ) SaveAsset ();
										}

									}
								}

								using (new Horizontal()) {
									if (Selection.activeObject.GetType ().ToString () == "DKRaceData") {
										GUILayout.Label ("Race :", GUILayout.Width (90));
										DK_UMA_Editor._Name = GUILayout.TextField (DK_UMA_Editor._Name, 50, GUILayout.Width (150));
									}
								}
								#endregion Element Name & Linked UMA

								#region Linked to Slot
								if (Selection.activeObject && !EditorVariables.AutoDetLib && Selection.activeObject.GetType ().ToString () == "DKOverlayData") {
									//This draws a Line to seperate the Controls
									GUI.color = Color.white;
									GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

									if (DK_UMA_Editor.Helper) {
										GUILayout.Space (5);
										GUI.color = Color.white;
										EditorGUILayout.HelpBox("To ease the creation process, you can link an Overlay to a Slot.", UnityEditor.MessageType.None);
										//	GUILayout.Space (10);
									}
									DKOverlayData _SelectedOv = (Selection.activeObject as DKOverlayData);

									using (new Horizontal()) {
										GUI.color = Color.white;
										GUILayout.Label ("Linked to Slot(s) :", GUILayout.ExpandWidth (false));
										if (_SelectedOv.LinkedToSlot.Count == 0 )
											GUILayout.Label ("Not linked", GUILayout.ExpandWidth (true));
										else if (_SelectedOv.LinkedToSlot.Count == 1 && _SelectedOv.LinkedToSlot [0] != null )
											GUILayout.Label (_SelectedOv.LinkedToSlot [0].name, GUILayout.ExpandWidth (true));
										else if (_SelectedOv.LinkedToSlot.Count == 1 && _SelectedOv.LinkedToSlot [0] == null )
											GUILayout.Label ("IS MISSING ! Please fixe it manually using the Inspector.", GUILayout.ExpandWidth (true));
										else if (_SelectedOv.LinkedToSlot.Count > 0) {
											GUI.color = Green;
											GUILayout.Label ("Multiple", GUILayout.ExpandWidth (true));
										}
									}
								}
								#endregion Linked to Slot

								#region Gender
								//This draws a Line to seperate the Controls
								GUI.color = Color.white;
								GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

								if ( Selection.activeObject && !EditorVariables.AutoDetLib && Selection.activeObject.GetType ().ToString () == "DKOverlayData" ){
									DKOverlayData _SelectedOv = (Selection.activeObject as DKOverlayData);
									if ( _SelectedOv.LinkedToSlot.Count == 0 ){
										GUI.color = Color.yellow;
										if (!EditorVariables.AutoDetLib && EditorVariables.SelectedElementObj == Selection.activeGameObject && DK_UMA_Editor.Helper && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot)
											GUILayout.TextField ("You can specify a Gender or let it be usable by both.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
										if (!EditorVariables.AutoDetLib && EditorVariables.SelectedElementObj == Selection.activeGameObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot){
											using (new Horizontal()) {
												GUILayout.Label ("Gender", GUILayout.Width (60));

												if (EditorVariables.SelectedElementGender == "Both")
													GUI.color = Green;
												else
													GUI.color = Color.gray;
												if (GUILayout.Button ("Both", GUILayout.Width (130))) {
													EditorVariables.SelectedElementGender = "Both";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) {
														SelectedSlotElement.Gender = EditorVariables.SelectedElementGender;
													}
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) {
														SelectedOverlayElement.Gender = EditorVariables.SelectedElementGender;
													}
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}
												if (EditorVariables.SelectedElementGender == "Female")
													GUI.color = Green;
												else
													GUI.color = Color.gray;
												if (GUILayout.Button ("Female", GUILayout.Width (130))) {
													EditorVariables.SelectedElementGender = "Female";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) {
														SelectedSlotElement.Gender = EditorVariables.SelectedElementGender;
													}
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) {
														SelectedOverlayElement.Gender = EditorVariables.SelectedElementGender;
													}
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}
												if (EditorVariables.SelectedElementGender == "Male")
													GUI.color = Green;
												else
													GUI.color = Color.gray;
												if (GUILayout.Button ("Male", GUILayout.Width (130))) {
													EditorVariables.SelectedElementGender = "Male";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) {
														SelectedSlotElement.Gender = EditorVariables.SelectedElementGender;
													}
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) {
														SelectedOverlayElement.Gender = EditorVariables.SelectedElementGender;
													}
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}							
											}
										}
									}
									else {
										EditorGUILayout.HelpBox("The selected overlay is linked to a slot, it is not necessary to setup its Gender.", UnityEditor.MessageType.None);
									}
								}
								// for slot
								else {
									GUI.color = Color.yellow;
									if (!EditorVariables.AutoDetLib && EditorVariables.SelectedElementObj == Selection.activeGameObject && DK_UMA_Editor.Helper && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot)
										GUILayout.TextField ("You can specify a Gender or let it be usable by both.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
									if (!EditorVariables.AutoDetLib && EditorVariables.SelectedElementObj == Selection.activeGameObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot){
										using (new Horizontal()) {
											GUILayout.Label ("Gender", GUILayout.Width (60));

											if (EditorVariables.SelectedElementGender == "Both")
												GUI.color = Green;
											else
												GUI.color = Color.gray;
											if (GUILayout.Button ("Both", GUILayout.Width (130))) {
												EditorVariables.SelectedElementGender = "Both";
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
												if (SelectedSlotElement != null) {
													SelectedSlotElement.Gender = EditorVariables.SelectedElementGender;
												}
												DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
												if (SelectedOverlayElement != null) {
													SelectedOverlayElement.Gender = EditorVariables.SelectedElementGender;
												}
												EditorUtility.SetDirty (Selection.activeObject);
												AssetDatabase.SaveAssets ();
											}
											if (EditorVariables.SelectedElementGender == "Female")
												GUI.color = Green;
											else
												GUI.color = Color.gray;
											if (GUILayout.Button ("Female", GUILayout.Width (130))) {
												EditorVariables.SelectedElementGender = "Female";
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
												if (SelectedSlotElement != null) {
													SelectedSlotElement.Gender = EditorVariables.SelectedElementGender;
												}
												DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
												if (SelectedOverlayElement != null) {
													SelectedOverlayElement.Gender = EditorVariables.SelectedElementGender;
												}
												EditorUtility.SetDirty (Selection.activeObject);
												AssetDatabase.SaveAssets ();
											}
											if (EditorVariables.SelectedElementGender == "Male")
												GUI.color = Green;
											else
												GUI.color = Color.gray;
											if (GUILayout.Button ("Male", GUILayout.Width (130))) {
												EditorVariables.SelectedElementGender = "Male";
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
												if (SelectedSlotElement != null) {
													SelectedSlotElement.Gender = EditorVariables.SelectedElementGender;
												}
												DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
												if (SelectedOverlayElement != null) {
													SelectedOverlayElement.Gender = EditorVariables.SelectedElementGender;
												}
												EditorUtility.SetDirty (Selection.activeObject);
												AssetDatabase.SaveAssets ();
											}							
										}
									}
								}
							
								#endregion Gender

								#region Element Race
								//This draws a Line to seperate the Controls
								GUI.color = Color.white;
								GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

								if ( Selection.activeObject && !EditorVariables.AutoDetLib && Selection.activeObject.GetType ().ToString () == "DKOverlayData" ){
									DKOverlayData _SelectedOv = (Selection.activeObject as DKOverlayData);
									if ( _SelectedOv.LinkedToSlot.Count == 0 ){
										GUI.color = Color.yellow;									
										if (Selection.activeObject 
											&& !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot 
											&& (Selection.activeObject.GetType ().ToString () == "DKSlotData" 
												|| Selection.activeObject.GetType ().ToString () == "DKOverlayData" )
											&& !EditorVariables.AutoDetLib)
										{
											using (new Horizontal()) {
												GUILayout.TextField ("Don't forget to set the Races able to use the Element", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
												GUI.color = Color.white;
												if (Selection.activeObject.GetType ().ToString () != "DKRaceData"){
													//		GUILayout.Label ("Element's Races :", GUILayout.Width (100));
													if (EditorVariables.UMAObj == null) {
														DK_UMA_Editor.UMAObjName = EditorVariables.UMAObjDefault;
														EditorVariables.UMAObj = GameObject.Find (DK_UMA_Editor.UMAObjName);
													}
													if (EditorVariables.SelectedElementObj == Selection.activeGameObject) {
														if (Selection.activeObject.GetType ().ToString () != "DKRaceData" && GUILayout.Button ("Open Races List", GUILayout.Width (150))) {
															DK_UMA_Editor.OpenRaceSelectEditor ();
														}
													}
												}
											}
										}
									}
									else {
										EditorGUILayout.HelpBox("The selected overlay is linked to a slot, it is not necessary to setup its Races.", UnityEditor.MessageType.None);
									}
								}
								// for slot
								else {
									GUI.color = Color.yellow;									
									if (Selection.activeObject 
										&& !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot 
										&& (Selection.activeObject.GetType ().ToString () == "DKSlotData" 
											|| Selection.activeObject.GetType ().ToString () == "DKOverlayData" )
										&& !EditorVariables.AutoDetLib)
									{
										using (new Horizontal()) {
											GUILayout.TextField ("Don't forget to set the Races able to use the Element", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
											GUI.color = Color.white;
											if (Selection.activeObject.GetType ().ToString () != "DKRaceData"){
												//		GUILayout.Label ("Element's Races :", GUILayout.Width (100));
												if (EditorVariables.UMAObj == null) {
													DK_UMA_Editor.UMAObjName = EditorVariables.UMAObjDefault;
													EditorVariables.UMAObj = GameObject.Find (DK_UMA_Editor.UMAObjName);
												}
												if (EditorVariables.SelectedElementObj == Selection.activeGameObject) {
													if (Selection.activeObject.GetType ().ToString () != "DKRaceData" && GUILayout.Button ("Open Races List", GUILayout.Width (150))) {
														DK_UMA_Editor.OpenRaceSelectEditor ();
													}
												}
											}
										}
									}
								}
								#endregion
							}
						}

					#region choose Place button
					//This draws a Line to seperate the Controls
					GUI.color = Color.white;
					GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));
					if (Selection.activeObject && !EditorVariables.AutoDetLib 
						&& Selection.activeObject.GetType ().ToString () != "DKRaceData" ) {
						// For Overlay
						if ( Selection.activeObject && !EditorVariables.AutoDetLib && Selection.activeObject.GetType ().ToString () == "DKOverlayData" ){
							DKOverlayData _SelectedOv = (Selection.activeObject as DKOverlayData);
							if ( _SelectedOv.LinkedToSlot.Count == 0 ){

								GUI.color = Color.yellow;
								if (EditorVariables.SelectedElementObj == Selection.activeGameObject && DK_UMA_Editor.Helper && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot)
									EditorGUILayout.HelpBox("You can change the Element's place used during the model's generation. The place is very important, " +
										"an Element without a place will not be generated.", UnityEditor.MessageType.None);

								GUI.color = Green;
								if (EditorVariables.SelectedElementObj == Selection.activeGameObject){
									using (new Horizontal()) {
										EditorVariables.SelectedElemSlot = Selection.activeObject as DKSlotData;
										if ( EditorVariables.SelectedElemSlot ) EditorVariables.SelectedElemPlace = EditorVariables.SelectedElemSlot.Place;
										if ( EditorVariables.SelectedElemOvlay ) EditorVariables.SelectedElemPlace = EditorVariables.SelectedElemOvlay.Place;
										GUI.color = Color.white;
										GUILayout.Label ("Place of the Element :", bold, GUILayout.ExpandWidth (false));
										if ( EditorVariables.SelectedElemPlace)
											GUI.color = Green;
										else {
											GUI.color = Red;
											if ( EditorVariables.SelectedElemSlot ) {
												EditorVariables.SelectedElemPlace = (Selection.activeObject as DKSlotData).Place;
												SaveAsset ();
											}
											if ( EditorVariables.SelectedElemOvlay ) {
												EditorVariables.SelectedElemPlace = (Selection.activeObject as DKOverlayData).Place;
												SaveAsset ();
											}
										}
										if ( Selection.activeObject 
											&& Selection.activeObject != null
											&& ( (Selection.activeObject as DKSlotData) != null 
												&& (Selection.activeObject as DKSlotData).Place != null 
												|| (Selection.activeObject as DKOverlayData) != null
												&& (Selection.activeObject as DKOverlayData).Place != null) ){
											string place = "";
											if ( (Selection.activeObject as DKSlotData) != null 
												&& (Selection.activeObject as DKSlotData).Place != null ) place = (Selection.activeObject as DKSlotData).Place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName;
											else if ( (Selection.activeObject as DKOverlayData) != null
												&& (Selection.activeObject as DKOverlayData).Place != null ) place = (Selection.activeObject as DKOverlayData).Place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName;
											GUILayout.Label ( place, bold, GUILayout.ExpandWidth (true));
										}
										else GUILayout.Label ("Choose ---->", GUILayout.ExpandWidth (true));
										GUI.color = Color.white;
										if (EditorVariables.SelectedElementObj == Selection.activeGameObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
											&& GUILayout.Button ("Choose Place", GUILayout.Width (150))) {
											DK_UMA_Editor.OpenPlaceWin ();
										}
									}
								}
							}
							else {
								EditorGUILayout.HelpBox("The selected overlay is linked to a slot, it is not necessary to setup its place.", UnityEditor.MessageType.None);
							}
						}
						// for Slot
						else if ( Selection.activeObject && !EditorVariables.AutoDetLib && Selection.activeObject.GetType ().ToString () == "DKSlotData" ){
							if (EditorVariables.SelectedElementObj == Selection.activeGameObject){
								using (new Horizontal()) {
									EditorVariables.SelectedElemSlot = Selection.activeObject as DKSlotData;
									if ( EditorVariables.SelectedElemSlot ) EditorVariables.SelectedElemPlace = EditorVariables.SelectedElemSlot.Place;
									if ( EditorVariables.SelectedElemOvlay ) EditorVariables.SelectedElemPlace = EditorVariables.SelectedElemOvlay.Place;
									GUI.color = Color.white;
									GUILayout.Label ("Place of the Element :", bold, GUILayout.ExpandWidth (false));
									if ( EditorVariables.SelectedElemPlace)
										GUI.color = Green;
									else
										GUI.color = Red;
									if ( Selection.activeObject 
										&& Selection.activeObject != null
										&& ( (Selection.activeObject as DKSlotData) != null 
											&& (Selection.activeObject as DKSlotData).Place != null 
											|| (Selection.activeObject as DKOverlayData) != null
											&& (Selection.activeObject as DKOverlayData).Place != null) ){
										string place = "";
										if ( (Selection.activeObject as DKSlotData) != null 
											&& (Selection.activeObject as DKSlotData).Place != null ) place = (Selection.activeObject as DKSlotData).Place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName;
										else if ( (Selection.activeObject as DKOverlayData) != null
											&& (Selection.activeObject as DKOverlayData).Place != null ) place = (Selection.activeObject as DKOverlayData).Place.dk_SlotsAnatomyElement.dk_SlotsAnatomyName;
										GUILayout.Label ( place, bold, GUILayout.ExpandWidth (true));
									}
									else GUILayout.Label ("Choose ---->", GUILayout.ExpandWidth (true));
									GUI.color = Color.white;
									if (EditorVariables.SelectedElementObj == Selection.activeGameObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
										&& GUILayout.Button ("Choose Place", GUILayout.Width (150))) {
										DK_UMA_Editor.OpenPlaceWin ();
									}
								}
							}
						}
					}
					#endregion Place

					#region choose ColorPresets button
					if (Selection.activeObject && !EditorVariables.AutoDetLib && Selection.activeObject.GetType ().ToString () == "DKOverlayData") {
						//This draws a Line to seperate the Controls
						GUI.color = Color.white;
						GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

						EditorVariables.SelectedElemOvlay = Selection.activeObject as DKOverlayData;
						if ( EditorVariables.SelectedElemOvlay.OverlayType == "Flesh"
							|| EditorVariables.SelectedElemOvlay.OverlayType == "Face"
							|| EditorVariables.SelectedElemOvlay.OverlayType == "Hair"
							|| EditorVariables.SelectedElemOvlay.OverlayType == "Eyes"
							|| EditorVariables.SelectedElemOvlay.OverlayType == "Eyebrow"
							|| EditorVariables.SelectedElemOvlay.OverlayType == "Lip"
							|| EditorVariables.SelectedElemOvlay.OverlayType == "Beard" ){
							EditorGUILayout.HelpBox("The Color Presets of the selected 'Overlay Type' ("+EditorVariables.SelectedElemOvlay.OverlayType+") are handled by the races.", UnityEditor.MessageType.None);
						}
						else {
							if (DK_UMA_Editor.Helper) {
								GUILayout.Space (5);
								GUI.color = Color.white;
								EditorGUILayout.HelpBox("You can assign some Color Presets to your overlay.", UnityEditor.MessageType.None);
								//	GUILayout.Space (10);
							}
							using (new Horizontal()) {					
								GUI.color = Color.white;
								GUILayout.Label ("Color(s) :", GUILayout.ExpandWidth (false));

								if ( EditorVariables.SelectedElemOvlay.ColorPresets.Count == 1)
									GUILayout.Label ( EditorVariables.SelectedElemOvlay.ColorPresets [0].name, GUILayout.ExpandWidth (true));
								else if ( EditorVariables.SelectedElemOvlay.ColorPresets.Count > 0) {
									GUI.color = Green;
									GUILayout.Label ("Multiple ("+ EditorVariables.SelectedElemOvlay.ColorPresets.Count.ToString()+")", GUILayout.ExpandWidth (true));
									//	if ( EditorVariables.SelectedElemOvlay.ColorPresets)
									//		GUI.color = Green;
									//	else
									//		GUI.color = Color.white;
									//	if (!DK_UMA_Editor.chooseOverlay && GUILayout.Button ("List", GUILayout.ExpandWidth (false))) {
									//		if (DK_UMA_Editor.LinkedOverlayList)
									//			DK_UMA_Editor.LinkedOverlayList = false;
									//		else
									//			DK_UMA_Editor.LinkedOverlayList = true;
									//	}
								} else {
									GUI.color = Color.yellow;
									GUILayout.Label ("Choose ---->", GUILayout.ExpandWidth (true));
								}
								GUI.color = Color.white;
								if (EditorVariables.SelectedElementObj == Selection.activeGameObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
									&& GUILayout.Button ("Choose Color Presets", GUILayout.Width (150))) {
									DK_UMA_Editor.OpenColorPresetWin ();
									ColorPreset_Editor._OverlayData =  EditorVariables.SelectedElemOvlay;
									ColorPreset_Editor.Statut = "ToOverlay";
								}
							}
						}
					}
					#endregion choose ColorPresets button

					#region Stacked Overlays
					if (Selection.activeObject && !EditorVariables.AutoDetLib && Selection.activeObject.GetType ().ToString () == "DKOverlayData") {
						//This draws a Line to seperate the Controls
						GUI.color = Color.white;
						GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

						GUI.color = Color.white;
						if ( DK_UMA_Editor.Helper )
							EditorGUILayout.HelpBox("Stacked overlays are additional textures applied on a wear Slot, " +
								"they can use various color presets different than the first overlay of the wear (the current selected overlay).", UnityEditor.MessageType.None);

						using (new Horizontal()) {
							DKOverlayData oldOpt1 = (Selection.activeObject as DKOverlayData).Opt01;
							GUILayout.Label ("Stacked Overlay 01 :", GUILayout.Width (180));
							(Selection.activeObject as DKOverlayData).Opt01 = 
								(DKOverlayData)EditorGUILayout.ObjectField("",(Selection.activeObject as DKOverlayData).Opt01,typeof(DKOverlayData),false, GUILayout.Width (270));
							if ( oldOpt1 != (Selection.activeObject as DKOverlayData).Opt01 ) SaveAsset ();
						}
						using (new Horizontal()) {
							DKOverlayData oldOpt2 = (Selection.activeObject as DKOverlayData).Opt02;
							GUILayout.Label ("Stacked Overlay 02 :", GUILayout.Width (180));
							(Selection.activeObject as DKOverlayData).Opt02 = 
								(DKOverlayData)EditorGUILayout.ObjectField("",(Selection.activeObject as DKOverlayData).Opt02,typeof(DKOverlayData),false, GUILayout.Width (270));
							if ( oldOpt2 != (Selection.activeObject as DKOverlayData).Opt02 ) SaveAsset ();
						}
						/*	using (new Horizontal()) {
										GUILayout.Label ("Dirt Overlay(s) :", GUILayout.ExpandWidth (false));
										if (_SelectedOv.DirtOverlays.Count == 0 )
											GUILayout.Label ("None", GUILayout.ExpandWidth (true));
										else if (_SelectedOv.DirtOverlays.Count == 1)
											GUILayout.Label (_SelectedOv.DirtOverlays [0].name, GUILayout.ExpandWidth (true));
										else if (_SelectedOv.DirtOverlays.Count > 0) {
											GUI.color = Green;
											GUILayout.Label ("Multiple", GUILayout.ExpandWidth (true));
										}
										if (EditorVariables.SelectedElementObj == Selection.activeGameObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
											&& GUILayout.Button ("Edit", GUILayout.ExpandWidth (false))) {
											OpenChooseDirtOverlay ();
										}
									}*/



					}
					#endregion Stacked Overlays
				
					if (Selection.activeObject && !EditorVariables.AutoDetLib && Selection.activeObject.GetType ().ToString () == "DKSlotData") {
						
						#region choose Linked Overlay
						//This draws a Line to seperate the Controls
						GUI.color = Color.white;
						GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

						if ( ( Selection.activeObject as DKSlotData ) != null
							&& ( ( Selection.activeObject as DKSlotData ).OverlayType == "Flesh"
								|| ( Selection.activeObject as DKSlotData ).OverlayType == "Face" ) )
							EditorGUILayout.HelpBox("This Element is of Overlay Type 'Flesh' or 'Face'. " +
								"You can let the engine use the Flesh and Face overlays for this DK Slot. " +
								"Or you can specify a Linked Overlay.", UnityEditor.MessageType.None);
						

						if (DK_UMA_Editor.Helper) {
							GUILayout.Space (5);
							GUI.color = Color.white;
							EditorGUILayout.HelpBox("To ease the creation process, you can link a slot to an Overlay.", UnityEditor.MessageType.None);
							//	GUILayout.Space (10);
						}

						if (EditorVariables.SelectedElementObj == Selection.activeGameObject)
							using (new Horizontal()) {
								GUI.color = Color.white;
								GUILayout.Label ("Linked Overlay(s) :", GUILayout.ExpandWidth (false));
								EditorVariables.overlayList = ( Selection.activeObject as DKSlotData ).LinkedOverlayList;
								if ( EditorVariables.overlayList.Count == 1
									&& EditorVariables.overlayList [0] != null ) {
									EditorGUILayout.ObjectField("",EditorVariables.overlayList [0],typeof(DKOverlayData),false, GUILayout.Width (180));
									GUILayout.Label ( "", GUILayout.ExpandWidth (true));
								}
								else if ( EditorVariables.overlayList.Count > 0) {
									GUI.color = Green;
									GUILayout.Label ("Multiple", GUILayout.ExpandWidth (true));
									if (DK_UMA_Editor.LinkedOverlayList) GUI.color = Green;
									else GUI.color = Color.white;

								} else {
									GUI.color = Color.yellow;
									GUILayout.Label ("Choose ---->", GUILayout.ExpandWidth (true));

								}
								GUI.color = Color.white;
								if (EditorVariables.SelectedElementObj == Selection.activeGameObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
									&& GUILayout.Button ("Choose Linked Overlay", GUILayout.Width (150))) {
									DK_UMA_Editor.OpenChooseOverlayWin ();
								}
							}
						#endregion choose Linked Overlay

						//This draws a Line to seperate the Controls
						GUI.color = Color.white;
						GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

						#region Color Presets of the Linked Overlay
						if ( ( Selection.activeObject as DKSlotData ) != null
							&& ( Selection.activeObject as DKSlotData ).LinkedOverlayList.Count == 1
							&& ( Selection.activeObject as DKSlotData ).LinkedOverlayList[0] != null ){

							using (new Horizontal()) {
								GUI.color = Color.white;
								GUILayout.Label ("Color Presets of Linked Overlay :", GUILayout.ExpandWidth (false));
								string ColorsAssigned = "None";
								if ( ( Selection.activeObject as DKSlotData ).LinkedOverlayList[0].ColorPresets.Count == 1 )
									ColorsAssigned = ( Selection.activeObject as DKSlotData ).LinkedOverlayList[0].ColorPresets[0].ColorPresetName;
								else if ( ( Selection.activeObject as DKSlotData ).LinkedOverlayList[0].ColorPresets.Count > 1 )
									ColorsAssigned = "Multiple ("+( Selection.activeObject as DKSlotData ).LinkedOverlayList[0].ColorPresets.Count+")";
								GUILayout.Label (ColorsAssigned, GUILayout.ExpandWidth (true));
								if ( GUILayout.Button ("Choose Color Presets", GUILayout.Width (150))) {
									DK_UMA_Editor.OpenColorPresetWin ();
									ColorPreset_Editor._OverlayData =  ( Selection.activeObject as DKSlotData ).LinkedOverlayList[0];
									ColorPreset_Editor.Statut = "ToOverlay";
								}
							}
							//This draws a Line to seperate the Controls
							GUI.color = Color.white;
							GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));
						}
						#endregion Color Presets of the Linked Overlay

						#region LOD
						// LOD
						if ( ( Selection.activeObject as DKSlotData ) != null )
							using (new Horizontal()) {

								DKSlotData SelectedSlotElement = ( Selection.activeObject as DKSlotData );

								GUI.color = Color.white;
								GUILayout.Label ("L.O.D :", GUILayout.Width (50));
								// No LOD or is LOD Master 
								if ( SelectedSlotElement._LOD.IsLOD0 == true ) GUI.color = Green;
								else GUI.color = Color.grey;
								if ( SelectedSlotElement._LOD.MasterLOD == null ){
									// LOD 0 button
									if (GUILayout.Button ("Is LOD 0", GUILayout.ExpandWidth (false))) {
										if ( SelectedSlotElement._LOD.IsLOD0 == true ) SelectedSlotElement._LOD.IsLOD0 = false;
										else SelectedSlotElement._LOD.IsLOD0 = true;
										EditorUtility.SetDirty (SelectedSlotElement);
										AssetDatabase.SaveAssets ();
									}
								}
								// Is LOD child
								else {
									GUI.color = Color.gray;
									GUILayout.Label ("Is LOD child of ", GUILayout.Width (50));
									if (GUILayout.Button (SelectedSlotElement._LOD.MasterLOD.name, GUILayout.ExpandWidth (false))) {
										Selection.activeObject = SelectedSlotElement._LOD.MasterLOD;
									}
								}
								// For LOD 0
								if ( SelectedSlotElement._LOD.IsLOD0 == true ){
									// LOD 1
									string LOD1Text = "";
									if ( SelectedSlotElement._LOD.LOD1 == null ) LOD1Text = "Choose LOD 1";
									else LOD1Text = SelectedSlotElement._LOD.LOD1.slotName;
									// choose LOD 1 button
									if ( SelectedSlotElement._LOD.LOD1 != null ) GUI.color = Green;
									else GUI.color = Color.white;
									if (GUILayout.Button (LOD1Text, GUILayout.Width (172))) {
										// open choose slot window
										ChooseSlot_Win.Action = "LOD1";
										DK_UMA_Editor.OpenChooseSlotWin ();
									}
									// LOD 2
									string LOD2Text = "";
									if ( SelectedSlotElement._LOD.LOD2 == null ) LOD2Text = "Choose LOD 2";
									else LOD2Text = SelectedSlotElement._LOD.LOD2.slotName;
									// choose LOD 2 button
									if ( SelectedSlotElement._LOD.LOD2 != null ) GUI.color = Green;
									else GUI.color = Color.white;
									if (GUILayout.Button (LOD2Text, GUILayout.Width (172))) {
										// open choose slot window
										ChooseSlot_Win.Action = "LOD2";
										DK_UMA_Editor.OpenChooseSlotWin ();
									}
								}						 
							}
						#endregion LOD
					}
				}

				#region Race Only
				EditorVariables.SelectedElementObj = Selection.activeObject as GameObject;
				if (Selection.activeObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
					&& Selection.activeObject.GetType ().ToString () == "DKRaceData"
					&& !EditorVariables.AutoDetLib) {
					GUILayout.Space (10);
					GUI.color = Color.white;
					if (DK_UMA_Editor.Helper)
						GUILayout.TextField ("You can set any DNA aspect for your avatars, it is controlled by the DKRaceData.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					if (DK_UMA_Editor.Helper)
						GUILayout.TextField ("It is composed by 2 parts, the Race limitations and the Bones, the bones are including the DNA behaviour for you to choose how it will manipulate the aspect of your avatar.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					if (DK_UMA_Editor.Helper)
						GUILayout.TextField ("Tip : The race limitations can be set directly when you are modifying an avatar.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					if (DK_UMA_Editor.Helper)
						GUILayout.TextField ("Also Color Presets can be created and be applied to the races.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

					if (GUILayout.Button ("Open Race Editor", GUILayout.ExpandWidth (true))) {
						DK_UMA_Editor.OpenRaceEditor ();
					}				
				}
				#endregion Race Only

				#region Overlay Type
				//This draws a Line to seperate the Controls
				GUI.color = Color.white;
				GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

				if ( Selection.activeObject.GetType ().ToString () != "DKRaceData" ) {
					using (new Horizontal()) {
						if (GUILayout.Button ("Overlay Type", "toolbarbutton", GUILayout.ExpandWidth (true))) {
							DisplayOverlayTypes = !DisplayOverlayTypes;
						}
						if ( DisplayOverlayTypes ) GUI.color = Green;
						else GUI.color = Color.gray;
						if (GUILayout.Button ("show OverlayType", "toolbarbutton", GUILayout.Width (150))) {
							DisplayOverlayTypes = !DisplayOverlayTypes;
						}
					}
				}
				if ( DisplayOverlayTypes ){
					GUI.color = Color.white;
					EditorVariables.SelectedElementObj = Selection.activeObject as GameObject;

					if ( Selection.activeObject && !EditorVariables.AutoDetLib ){
						DKOverlayData _SelectedOv = (Selection.activeObject as DKOverlayData);
						if ( (Selection.activeObject.GetType ().ToString () == "DKOverlayData" 
							&&  _SelectedOv.LinkedToSlot.Count ==  0) || Selection.activeObject.GetType ().ToString () == "DKSlotData" ){

							if (!DK_UMA_Editor.LinkedOverlayList ){
									EditorGUILayout.HelpBox("The Overlay Type is a key setting used for sorting the elements to auto create the RPG lists. " +
										"Use the 'Add to Libraries' button of the 'Elements Manager' to create the lists.", UnityEditor.MessageType.None);

								// helper
								if ( Selection.activeObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
									&& (Selection.activeObject.GetType ().ToString () == "DKOverlayData"
										|| Selection.activeObject.GetType ().ToString () == "DKSlotData") 
									&& !EditorVariables.AutoDetLib && DK_UMA_Editor.Helper ) { 

									EditorGUILayout.HelpBox("All the 'Naked body parts' must be of the 'Flesh' Type, the Head must be of the 'Face' type.", UnityEditor.MessageType.None);

									if (Selection.activeObject.GetType ().ToString () == "DKOverlayData")
										EditorGUILayout.HelpBox("About Hair : You can use hair modules, it has to be of the 'Hair+Element' Overlay type and its place is 'Hair'. " +
											"It can be usefull to assign it to the corresponding Slot.", UnityEditor.MessageType.None);
									if (Selection.activeObject.GetType ().ToString () == "DKOverlayData")
										EditorGUILayout.HelpBox("About Beard : If your Overlay is a Beard, select the Beard Overlay type and the place Head. " +
											"The pilosity is settable during the creation.", UnityEditor.MessageType.None);
									if (Selection.activeObject.GetType ().ToString () == "DKOverlayData")
										EditorGUILayout.HelpBox("About Eyebrow : If your Overlay is an Eyebrow, select the Hair Overlay type and the place Head.", UnityEditor.MessageType.None);
								}

								if (!DK_UMA_Editor.LinkedOverlayList && Selection.activeObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
									&& Selection.activeObject.GetType ().ToString () == "DKSlotData" 
									&& !EditorVariables.AutoDetLib) {
									if (DK_UMA_Editor.Helper) {
										EditorGUILayout.HelpBox("Please remember : The head is composed by a single 'Face' slot (of the Anatomy part 'Head') and multiple 'Face+Element' slots," +
											" such as the Mouth or the Eyelid (of Anatomy part eyelid, not Eye).", UnityEditor.MessageType.None);
										EditorGUILayout.HelpBox("About Hair : You can use hair modules, it has to be of the 'Hair+Element' Overlay type and its place is 'Hair_Module'. " +
											"It can be usefull to assign it to the corresponding Overlay.", UnityEditor.MessageType.None);
										EditorGUILayout.HelpBox("About Beard : If your slot is a Beard, you will have to create a new Anatomy part.", UnityEditor.MessageType.None);
									
									}
								}

									#region Body

									if ( Selection.activeObject.GetType ().ToString () == "DKOverlayData" ){
										if (Selection.activeObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
											&& (Selection.activeObject.GetType ().ToString () == "DKOverlayData"
												|| Selection.activeObject.GetType ().ToString () == "DKSlotData")
											&& !EditorVariables.AutoDetLib) {

											using (new Horizontal()) {
												GUI.color = Color.white;

												GUILayout.Label ("Body :", GUILayout.ExpandWidth (false));
												if (EditorVariables.SelectedElementOverlayType == "Flesh") GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Flesh", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "Flesh";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}

												if (EditorVariables.SelectedElementOverlayType == "Face") GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Face", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "Face";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}

												if (EditorVariables.SelectedElementOverlayType == "Hair") GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Hair", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "Hair";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}

												if ((Selection.activeObject.GetType ().ToString () == "DKSlotData" && (Selection.activeObject as DKSlotData).Elem == true) 
													|| (Selection.activeObject.GetType ().ToString () == "DKOverlayData" && (Selection.activeObject as DKOverlayData).Elem == true))
													GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("+ Elem", GUILayout.ExpandWidth (true))) {
													if (Selection.activeObject.GetType ().ToString () == "DKSlotData" && (Selection.activeObject as DKSlotData).Elem == false)
														(Selection.activeObject as DKSlotData).Elem = true;
													else if (Selection.activeObject.GetType ().ToString () == "DKSlotData")
														(Selection.activeObject as DKSlotData).Elem = false;
													else if (Selection.activeObject.GetType ().ToString () == "DKOverlayData" && (Selection.activeObject as DKOverlayData).Elem == false)
														(Selection.activeObject as DKOverlayData).Elem = true;
													else if (Selection.activeObject.GetType ().ToString () == "DKOverlayData")
														(Selection.activeObject as DKOverlayData).Elem = false;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}
												if (EditorVariables.SelectedElementOverlayType == "Eyes") GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Eyes", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "Eyes";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}
											}
										}
										if (Selection.activeObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
											&& (Selection.activeObject.GetType ().ToString () == "DKOverlayData"
												|| Selection.activeObject.GetType ().ToString () == "DKSlotData") 
											&& !EditorVariables.AutoDetLib) {
											using (new Horizontal()) {
										if (EditorVariables.SelectedElementOverlayType == "Eyebrow") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Eyebrow", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "Eyebrow";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}

										if (EditorVariables.SelectedElementOverlayType == "Lips") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Lip", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "Lips";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}

										if (EditorVariables.SelectedElementOverlayType == "Makeup") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Makeup", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "Makeup";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}

										if (EditorVariables.SelectedElementOverlayType == "Tatoo") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Tatoo", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "Tatoo";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}

										if (EditorVariables.SelectedElementOverlayType == "Beard") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Beard", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "Beard";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}
									}
								
										}
									}
									else if ( Selection.activeObject.GetType ().ToString () == "DKSlotData" )
										EditorGUILayout.HelpBox("This Element is a DK Slot, its Overlay Type is auto assigned by the 'Place'.", UnityEditor.MessageType.None);
									
							//	}
									#endregion Body

									#region Wear
									if ( Selection.activeObject.GetType ().ToString () == "DKOverlayData" ){
										GUI.color = Color.white;
										if (Selection.activeObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
											&& (Selection.activeObject.GetType ().ToString () == "DKOverlayData" 
												|| Selection.activeObject.GetType ().ToString () == "DKSlotData")
											&& !EditorVariables.AutoDetLib) {

											//This draws a Line to seperate the Controls
											GUI.color = Color.white;
											GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

											if (DK_UMA_Editor.Helper && Selection.activeObject.GetType ().ToString () == "DKSlotData") 
												EditorGUILayout.HelpBox("About Wear : If your slot is a Wear, select the corresponding Overlay type and Anatomy part (ex.: Torso and TorsoWear for a T-shirt)." +
													"Then assign the correct Overlay to it ( ex.: FemaleTshirt01 Overlay for the FemaleTshirt01 slot).", UnityEditor.MessageType.None);
																		
											if (DK_UMA_Editor.Helper && Selection.activeObject.GetType ().ToString () == "DKOverlayData") { 
												EditorGUILayout.HelpBox("About Wear : If your Overlay is associated to a slot, verify to set the overlay exaclty the same than the slot, with the same weight (ex.: Torso and TorsoWear for a T-shirt, weight Light)." +
													"The name of the associated slot is displayed below.", UnityEditor.MessageType.None);
												EditorGUILayout.HelpBox("Overlay only Wear : If your Overlay has been created to be applied without an associated slot, it is possible that the leg overlay has to be applied on the torso Anatomy part, depending on your overlay creation." +
													"Try to tweek it if you encounter a bad overlay placement.", UnityEditor.MessageType.None);							
											}

											using (new Horizontal()) {
												GUI.color = Color.white;
												GUILayout.Label ("Wears :", GUILayout.ExpandWidth (false));
												if (EditorVariables.SelectedElementOverlayType == "TorsoWear") GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Torso", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "TorsoWear";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}
												if (EditorVariables.SelectedElementOverlayType == "ShoulderWear") GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Shoulder", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "ShoulderWear";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null)  SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}

												if (EditorVariables.SelectedElementOverlayType == "BeltWear") GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Belt", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "BeltWear";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}

												if (EditorVariables.SelectedElementOverlayType == "CloakWear") GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Cloak", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "CloakWear";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}

												if (EditorVariables.SelectedElementOverlayType == "LegsWear")GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Leg", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "LegsWear";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}
												if (EditorVariables.SelectedElementOverlayType == "FeetWear") GUI.color = Green;
												else GUI.color = Color.gray;
													if (GUILayout.Button ("Feet", GUILayout.ExpandWidth (true))) {
													EditorVariables.SelectedElementOverlayType = "FeetWear";
													DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
													if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
													if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
													EditorUtility.SetDirty (Selection.activeObject);
													AssetDatabase.SaveAssets ();
												}
											}
										}
										if (Selection.activeObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
											&& (Selection.activeObject.GetType ().ToString () == "DKOverlayData"
												|| Selection.activeObject.GetType ().ToString () == "DKSlotData")
											&& !EditorVariables.AutoDetLib) 
											using (new Horizontal()) {
										if (EditorVariables.SelectedElementOverlayType == "HandsWear") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Hand", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "HandsWear";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}

										if (EditorVariables.SelectedElementOverlayType == "HeadWear") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Head", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "HeadWear";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}

										if (EditorVariables.SelectedElementOverlayType == "ArmbandWear") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Armband", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "ArmbandWear";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}

										if (EditorVariables.SelectedElementOverlayType == "WristWear") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Wrist", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "WristWear";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}

										if (EditorVariables.SelectedElementOverlayType == "Underwear") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("Underwear", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "Underwear";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}
										if (EditorVariables.SelectedElementOverlayType == "") GUI.color = Green;
										else GUI.color = Color.gray;
											if (GUILayout.Button ("None", GUILayout.ExpandWidth (true))) {
											EditorVariables.SelectedElementOverlayType = "";
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if (SelectedSlotElement != null) SelectedSlotElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
											if (SelectedOverlayElement != null) SelectedOverlayElement.OverlayType = EditorVariables.SelectedElementOverlayType;
											EditorUtility.SetDirty (Selection.activeObject);
											AssetDatabase.SaveAssets ();
										}
									}			
									}
										#endregion Wear

									#region Weight
									GUI.color = Color.white;
									if (Selection.activeObject
										&& ((Selection.activeObject.GetType ().ToString () == "DKSlotData"
											&& (Selection.activeObject as DKSlotData).OverlayType != null && (Selection.activeObject as DKSlotData).OverlayType.ToLower ().Contains ("wear") == true)
											|| (Selection.activeObject.GetType ().ToString () == "DKOverlayData"
												&& (Selection.activeObject as DKOverlayData).OverlayType != null && (Selection.activeObject as DKOverlayData).OverlayType.ToLower ().Contains ("wear") == true))
										&& !EditorVariables.AutoDetLib) {
										//This draws a Line to seperate the Controls
										GUI.color = Color.white;
										GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

										using (new Horizontal()) {
											GUILayout.Label ("Weight", GUILayout.ExpandWidth (false));
											if (EditorVariables.SelectedElementWearWeight == "Light") GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Light", GUILayout.ExpandWidth (true))) {
												EditorVariables.SelectedElementWearWeight = "Light";
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
												if (SelectedSlotElement != null) SelectedSlotElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
												if (SelectedOverlayElement != null) SelectedOverlayElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												EditorUtility.SetDirty (Selection.activeObject);
												AssetDatabase.SaveAssets ();
											}
											if (EditorVariables.SelectedElementWearWeight == "Medium") GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Medium", GUILayout.ExpandWidth (true))) {
												EditorVariables.SelectedElementWearWeight = "Medium";
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
												if (SelectedSlotElement != null) SelectedSlotElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
												if (SelectedOverlayElement != null) SelectedOverlayElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												EditorUtility.SetDirty (Selection.activeObject);
												AssetDatabase.SaveAssets ();
											}

											if (EditorVariables.SelectedElementWearWeight == "High") GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("High", GUILayout.ExpandWidth (true))) {
												EditorVariables.SelectedElementWearWeight = "High";
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
												if (SelectedSlotElement != null) SelectedSlotElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
												if (SelectedOverlayElement != null) SelectedOverlayElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												EditorUtility.SetDirty (Selection.activeObject);
												AssetDatabase.SaveAssets ();
											}
											if (EditorVariables.SelectedElementWearWeight == "Heavy") GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Heavy", GUILayout.ExpandWidth (true))) {
												EditorVariables.SelectedElementWearWeight = "Heavy";
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
												if (SelectedSlotElement != null) SelectedSlotElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
												if (SelectedOverlayElement != null) SelectedOverlayElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												EditorUtility.SetDirty (Selection.activeObject);
												AssetDatabase.SaveAssets ();
											}
											if (EditorVariables.SelectedElementWearWeight == "") GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("No", GUILayout.ExpandWidth (true))) {
												EditorVariables.SelectedElementWearWeight = "";
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
												if (SelectedSlotElement != null) SelectedSlotElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												DKOverlayData SelectedOverlayElement = Selection.activeObject as DKOverlayData;
												if (SelectedOverlayElement != null) SelectedOverlayElement.WearWeight = EditorVariables.SelectedElementWearWeight;
												EditorUtility.SetDirty (Selection.activeObject);
												AssetDatabase.SaveAssets ();
											}
										}
									}
									#endregion Weight

									#region Hide
								if (Selection.activeObject.GetType ().ToString () != "DKRaceData" 
									&& Selection.activeObject.GetType ().ToString () != "DKOverlayData") 
								{ 
										//This draws a Line to seperate the Controls
										GUI.color = Color.white;
										GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

									if (DK_UMA_Editor.Helper)
										EditorGUILayout.HelpBox("In some cases you will need to delete a flesh part of your avatar to replace it by a wear slot " +
											"(ex.: to replace the feet when generating some boots).", UnityEditor.MessageType.None);
									using (new Horizontal()) {
										if (EditorVariables.Replace == true) GUI.color = Color.cyan;
										else GUI.color = Color.gray;											
										DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
										if ( SelectedSlotElement.OverlayType.Contains("Wear") && GUILayout.Button ("Replace the flesh anatomy part by this one", GUILayout.ExpandWidth (true))) {
											if (EditorVariables.Replace == true) EditorVariables.Replace = false;
											else EditorVariables.Replace = true;
											if (SelectedSlotElement != null) SelectedSlotElement.Replace = EditorVariables.Replace;
											EditorUtility.SetDirty (SelectedSlotElement);
											AssetDatabase.SaveAssets ();
										}								
									}		

										if (EditorVariables.SelectedElementOverlayType == "MaskWear"){
											using (new Horizontal()) {
												GUILayout.Label ("Hide", GUILayout.ExpandWidth (false));
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;

												if ( SelectedSlotElement._HideData.HideMouth ) GUI.color = Green;
												else GUI.color = Color.gray;
												if (GUILayout.Button ("Mouth", GUILayout.ExpandWidth (true))) {
													if (SelectedSlotElement._HideData.HideMouth == true) SelectedSlotElement._HideData.HideMouth = false;
													else SelectedSlotElement._HideData.HideMouth = true;
													EditorUtility.SetDirty (SelectedSlotElement);
													AssetDatabase.SaveAssets ();
												}
												if ( SelectedSlotElement._HideData.HideBeard ) GUI.color = Green;
												else GUI.color = Color.gray;
												if (GUILayout.Button ("Beard", GUILayout.ExpandWidth (true))) {
													if (SelectedSlotElement._HideData.HideBeard == true) SelectedSlotElement._HideData.HideBeard = false;
													else SelectedSlotElement._HideData.HideBeard = true;
													EditorUtility.SetDirty (SelectedSlotElement);
													AssetDatabase.SaveAssets ();
												}
											}
										}

										if (EditorVariables.SelectedElementOverlayType == "HeadWear"){
											using (new Horizontal()) {
												GUILayout.Label ("Hide", GUILayout.ExpandWidth (false));
												DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
												if ( SelectedSlotElement._HideData.HideHair ) GUI.color = Green;
												else GUI.color = Color.gray;
												if (GUILayout.Button ("Hair", GUILayout.ExpandWidth (true))) {
													if (SelectedSlotElement._HideData.HideHair == true) SelectedSlotElement._HideData.HideHair = false;
													else SelectedSlotElement._HideData.HideHair = true;
													EditorUtility.SetDirty (SelectedSlotElement);
													AssetDatabase.SaveAssets ();
												}
												if ( SelectedSlotElement._HideData.HideHairModule ) GUI.color = Green;
												else GUI.color = Color.gray;
												if (GUILayout.Button ("Hair Module", GUILayout.ExpandWidth (true))) {
													if (SelectedSlotElement._HideData.HideHairModule == true) SelectedSlotElement._HideData.HideHairModule = false;
													else SelectedSlotElement._HideData.HideHairModule = true;
													EditorUtility.SetDirty (SelectedSlotElement);
													AssetDatabase.SaveAssets ();
												}
												if ( SelectedSlotElement._HideData.HideEars ) GUI.color = Green;
												else GUI.color = Color.gray;
												if (GUILayout.Button ("Ears", GUILayout.ExpandWidth (true))) {
													if (SelectedSlotElement._HideData.HideEars == true) SelectedSlotElement._HideData.HideEars = false;
													else SelectedSlotElement._HideData.HideEars = true;
													EditorUtility.SetDirty (SelectedSlotElement);
													AssetDatabase.SaveAssets ();
												}
												if ( SelectedSlotElement._HideData.HideMouth ) GUI.color = Green;
												else GUI.color = Color.gray;
												if (GUILayout.Button ("Mouth", GUILayout.ExpandWidth (true))) {
													if (SelectedSlotElement._HideData.HideMouth == true) SelectedSlotElement._HideData.HideMouth = false;
													else SelectedSlotElement._HideData.HideMouth = true;
													EditorUtility.SetDirty (SelectedSlotElement);
													AssetDatabase.SaveAssets ();
												}
												if ( SelectedSlotElement._HideData.HideBeard ) GUI.color = Green;
												else GUI.color = Color.gray;
												if (GUILayout.Button ("Beard", GUILayout.ExpandWidth (true))) {
													if (SelectedSlotElement._HideData.HideBeard == true) SelectedSlotElement._HideData.HideBeard = false;
													else SelectedSlotElement._HideData.HideBeard = true;
													EditorUtility.SetDirty (SelectedSlotElement);
													AssetDatabase.SaveAssets ();
												}
												if ( SelectedSlotElement._HideData.HideCollar ) GUI.color = Green;
												else GUI.color = Color.gray;
												if (GUILayout.Button ("Collar", GUILayout.ExpandWidth (true))) {
													if (SelectedSlotElement._HideData.HideCollar == true) SelectedSlotElement._HideData.HideCollar = false;
													else SelectedSlotElement._HideData.HideCollar = true;
													EditorUtility.SetDirty (SelectedSlotElement);
													AssetDatabase.SaveAssets ();
												}
											}
										}

									if (EditorVariables.SelectedElementOverlayType == "TorsoWear"){
										DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
										using (new Horizontal()) {
											GUILayout.Label ("Hide", GUILayout.ExpandWidth (false));
											if ( SelectedSlotElement._HideData.HideShoulders ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Shoulders", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideShoulders == true) SelectedSlotElement._HideData.HideShoulders = false;
												else SelectedSlotElement._HideData.HideShoulders = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideLegs ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Legs", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideLegs == true) SelectedSlotElement._HideData.HideLegs = false;
												else SelectedSlotElement._HideData.HideLegs = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideBelt ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Belt", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideBelt == true) SelectedSlotElement._HideData.HideBelt = false;
												else SelectedSlotElement._HideData.HideBelt = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideArmBand ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("ArmBand", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideArmBand == true) SelectedSlotElement._HideData.HideArmBand = false;
												else SelectedSlotElement._HideData.HideArmBand = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
										}
										using (new Horizontal()) {
											if ( SelectedSlotElement._HideData.HideWrist ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Wrist", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideWrist == true) SelectedSlotElement._HideData.HideWrist = false;
												else SelectedSlotElement._HideData.HideWrist = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideCollar ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Collar", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideCollar == true) SelectedSlotElement._HideData.HideCollar = false;
												else SelectedSlotElement._HideData.HideCollar = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideCloak ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Cloak", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideCloak == true) SelectedSlotElement._HideData.HideCloak = false;
												else SelectedSlotElement._HideData.HideCloak = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideBackpack ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Backpack", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideBackpack == true) SelectedSlotElement._HideData.HideBackpack = false;
												else SelectedSlotElement._HideData.HideBackpack = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
										}
									}
									if (EditorVariables.SelectedElementOverlayType == "LegsWear")
										using (new Horizontal()) {
											GUILayout.Label ("Hide", GUILayout.ExpandWidth (false));
											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if ( SelectedSlotElement._HideData.HideBelt ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Belt", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideBelt == true) SelectedSlotElement._HideData.HideBelt = false;
												else SelectedSlotElement._HideData.HideBelt = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideLegBand ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Leg band", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideLegBand == true) SelectedSlotElement._HideData.HideLegBand = false;
												else SelectedSlotElement._HideData.HideLegBand = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideUnderwear ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Underwear", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideUnderwear == true) SelectedSlotElement._HideData.HideUnderwear = false;
												else SelectedSlotElement._HideData.HideUnderwear = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
										}
									if (EditorVariables.SelectedElementOverlayType == "HandsWear")
										using (new Horizontal()) {
											GUILayout.Label ("Hide", GUILayout.ExpandWidth (false));

											DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
											if ( SelectedSlotElement._HideData.HideRingLeft ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Ring Left", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideRingLeft == true) SelectedSlotElement._HideData.HideRingLeft = false;
												else SelectedSlotElement._HideData.HideRingLeft = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideRingRight ) GUI.color = Green;
											else GUI.color = Color.gray;
											if ( SelectedSlotElement._HideData.HideRingRight ) GUI.color = Green;
											if (GUILayout.Button ("Ring Right", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideRingRight == true) SelectedSlotElement._HideData.HideRingRight = false;
												else SelectedSlotElement._HideData.HideRingRight = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
											if ( SelectedSlotElement._HideData.HideWrist ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Wrist", GUILayout.ExpandWidth (true))) {
												if (SelectedSlotElement._HideData.HideWrist == true) SelectedSlotElement._HideData.HideWrist = false;
												else SelectedSlotElement._HideData.HideWrist = true;
												EditorUtility.SetDirty (SelectedSlotElement);
												AssetDatabase.SaveAssets ();
											}
										}
								}

									#endregion Hide

									#endregion OverlayType

								#region Legacy Menu
								if ( Selection.activeObject && !EditorVariables.AutoDetLib 
									&& Selection.activeObject.GetType ().ToString () == "DKSlotData") {
									if (DK_UMA_Editor.Helper) {
										GUILayout.Space (5);
										GUI.color = Color.white;
										EditorGUILayout.HelpBox("The Legacy slot(s) is generated automatically with this slot." +
											"It is usefull in case that the current slot is a EditorVariables.Replace slot, such as a Tshirt with Legacy arms and torso.", UnityEditor.MessageType.None);
									}
										GUI.color = Color.white;
										EditorGUILayout.HelpBox("The Legacy slot(s) is generated automatically by a DK slot. " +
											"It is usefull in case that the current slot Replaces a body slot, such as a Tshirt with Legacy arms replacing the original full torso of the avatar.", UnityEditor.MessageType.None);
										
									DKSlotData SelectedSlotElement = Selection.activeObject as DKSlotData;
									using (new Horizontal()) {
										if ( SelectedSlotElement._LegacyData.IsLegacy == false ){
											if ( SelectedSlotElement._LegacyData.HasLegacy == true ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Uses Legacy slot(s)", GUILayout.ExpandWidth (true))) {
												if ( SelectedSlotElement._LegacyData.HasLegacy == true ) SelectedSlotElement._LegacyData.HasLegacy = false;
												else SelectedSlotElement._LegacyData.HasLegacy = true;
											}
										}
										if ( SelectedSlotElement._LegacyData.HasLegacy == false ){
											if ( SelectedSlotElement._LegacyData.IsLegacy == true ) GUI.color = Green;
											else GUI.color = Color.gray;
											if (GUILayout.Button ("Is a Legacy slot", GUILayout.ExpandWidth (true))) {
												if ( SelectedSlotElement._LegacyData.IsLegacy == true ) SelectedSlotElement._LegacyData.IsLegacy = false;
												else SelectedSlotElement._LegacyData.IsLegacy = true;
											}
										}
									}

									if ( SelectedSlotElement._LegacyData.HasLegacy == true )
										using (new Horizontal()) {
											GUI.color = Color.white;
											GUILayout.Label ("Legacy", GUILayout.ExpandWidth (false));
											if (SelectedSlotElement._LegacyData.LegacyList.Count == 1)
												GUILayout.Label (SelectedSlotElement._LegacyData.LegacyList [0].name, GUILayout.ExpandWidth (true));
											else if (SelectedSlotElement._LegacyData.LegacyList.Count > 0) {
												GUI.color = Green;
												GUILayout.Label ("Multiple", GUILayout.ExpandWidth (true));
												if (SelectedSlotElement._LegacyData.LegacyList.Count < 2)
													GUI.color = Green;
												else
													GUI.color = Color.white;
												//	if (!DK_UMA_Editor.chooseOverlay && GUILayout.Button ("List", GUILayout.ExpandWidth (false))) {
												//	if (SelectedSlotElement._LegacyData.LegacyList)
												//		SelectedSlotElement._LegacyData.LegacyList = false;
												//	else
												//		SelectedSlotElement._LegacyData.LegacyList = true;
												//	}
											} else {
												GUI.color = Color.cyan;
												GUILayout.Label ("Choose ---->", GUILayout.ExpandWidth (true));
											}
											GUI.color = Color.cyan;
											if (EditorVariables.SelectedElementObj == Selection.activeGameObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
												&& GUILayout.Button ("Choose", GUILayout.ExpandWidth (false))) {
												DK_UMA_Editor.OpenChooseSlotWin ();
											}
										}
									if ( SelectedSlotElement._LegacyData.IsLegacy == true ) {
										using (new Horizontal()) {
											GUI.color = Color.white;
											GUILayout.Label ("Elder(s)", GUILayout.ExpandWidth (false));
											if (SelectedSlotElement._LegacyData.ElderList.Count == 1)
												GUILayout.Label (SelectedSlotElement._LegacyData.ElderList [0].name, GUILayout.ExpandWidth (true));
											else if (SelectedSlotElement._LegacyData.ElderList.Count > 0) {
												GUI.color = Green;
												GUILayout.Label ("Multiple", GUILayout.ExpandWidth (true));
												if (SelectedSlotElement._LegacyData.ElderList.Count < 2) GUI.color = Green;
												else GUI.color = Color.white;
											} 
											else {
												GUI.color = Color.cyan;
												GUILayout.Label ("Choose ---->", GUILayout.ExpandWidth (true));
											}
											GUI.color = Color.cyan;
											if (EditorVariables.SelectedElementObj == Selection.activeGameObject && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot
												&& GUILayout.Button ("Choose Elder", GUILayout.Width (150))) {
												DK_UMA_Editor.OpenChooseSlotWin ();
											}
										}
										if ( SelectedSlotElement._LegacyData.Replace == true ) GUI.color = Green;
										else GUI.color = Color.gray;
										if (GUILayout.Button ("Replace the Anatomy Part (Place)", GUILayout.ExpandWidth (true))) {
											if ( SelectedSlotElement._LegacyData.Replace == true ) SelectedSlotElement._LegacyData.Replace = false;
											else SelectedSlotElement._LegacyData.Replace = true;
										}
									}
								}
								#endregion Legacy Menu					
							}
						}
						else if ( Selection.activeObject.GetType ().ToString () == "DKOverlayData" ) {
							EditorGUILayout.HelpBox("The selected overlay is linked to a slot, it is not necessary to setup its Overlay Type.", UnityEditor.MessageType.None);

					
						}
					}
				
				}
			}
		
			#region Apply changes
			if ( !EditorVariables.AutoDetLib && !DK_UMA_Editor.choosePlace && !DK_UMA_Editor.chooseOverlay && !DK_UMA_Editor.chooseSlot )
			{
			//This draws a Line to seperate the Controls
			GUI.color = Color.white;
			GUILayout.Box(GUIContent.none, GUILayout.Width(Screen.width-25), GUILayout.Height(3));

			GUI.color = Color.yellow;
			if ( EditorVariables.SelectedElementObj == Selection.activeGameObject && DK_UMA_Editor.Helper ) 
			GUILayout.TextField("Setup the Element and click on the Apply button." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			if ( EditorVariables.SelectedElementObj != Selection.activeGameObject ) GUILayout.TextField("You need to select an Element using the 'Elements Manager'." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		
			using (new Horizontal()) {
				if ( ( (Selection.activeObject as DKOverlayData) != null || (Selection.activeObject as DKSlotData) != null )
						&& GUILayout.Button("Apply changes to the selected Element and add it to the races", GUILayout.ExpandWidth (true))){
					SaveAsset ();

					DK_RPG_UMA_Generator _DK_RPG_UMA_Generator = FindObjectOfType<DK_RPG_UMA_Generator>();
					if ( _DK_RPG_UMA_Generator != null )
						_DK_RPG_UMA_Generator.PopulateLists();
				}
			}
			#endregion Apply changes
		}
		#endregion Prepare
	}

	public static void SaveAsset (){
		EditorUtility.SetDirty(Selection.activeObject);
		AssetDatabase.SaveAssets();
		Debug.Log ("DK UMA Prepare element : Asset saved");
	}

	void OnSelectionChange() {	
		Repaint();
	}
}
