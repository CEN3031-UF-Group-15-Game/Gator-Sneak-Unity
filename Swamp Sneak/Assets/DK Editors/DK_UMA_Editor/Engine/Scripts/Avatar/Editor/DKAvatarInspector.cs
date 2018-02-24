using UnityEngine;
using System.Collections;
using UnityEditor;
using UMA;

[CustomEditor(typeof(DK_RPG_UMA))]
public class DKAvatarInspector : Editor {
	bool ViewNewInspector = true;
	UMADynamicAvatar UMAAvatar;
	DK_RPG_UMA Avatar;
	TransposeDK2UMA Transpose;
	bool ViewHelper = false;

	bool ShowMain;
	bool ShowShape;
	bool ShowEquipment;
	bool ShowEquipmentSet;
	bool ShowSave;
	bool ShowUMA;

	bool SaveToFile;
	bool SaveToData;
	bool SaveSet;
	DKEquipmentSetData SelectedSet;

	string NewSaveName = "";
	string NewSetName = "";

	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);

	void OnEnable (){
		Avatar = (DK_RPG_UMA)target;
		if ( Avatar != null ){
			UMAAvatar = Avatar.gameObject.GetComponentInChildren<UMADynamicAvatar>();
			Transpose = Avatar.gameObject.GetComponent<TransposeDK2UMA>();
		}
	}

	public override void OnInspectorGUI()
	{
		GUIStyle bold = new GUIStyle ("label");
		GUIStyle boldFold = new GUIStyle ("foldout");
		bold.fontStyle = FontStyle.Bold;
		bold.fontSize = 14;
		boldFold.fontStyle = FontStyle.Bold;

		using (new HorizontalCentered()) {
			GUILayout.Label ("DK UMA Avatar", bold, GUILayout.ExpandWidth (true));
		}
	//	EditorGUILayout.HelpBox("You can use the new Inspector for a detailled information about the options available for a DK UMA avatar.", UnityEditor.MessageType.None);

		ViewNewInspector = EditorGUILayout.Toggle ( "Detailled Inspector",ViewNewInspector, GUILayout.ExpandWidth (false));
		ViewHelper = EditorGUILayout.Toggle ( "View Helper Info",ViewHelper, GUILayout.ExpandWidth (false));
		if ( ViewHelper ) EditorGUILayout.HelpBox("This component is the DK UMA RPG definition of an avatar. All the elements used by the avatar are stored here. It contains also the options for the avatar. Never delete this component.", UnityEditor.MessageType.None);
		if ( GUILayout.Button ( "Disconnect from Prefab", GUILayout.ExpandWidth (true))) {
			PrefabUtility.DisconnectPrefabInstance (Avatar.gameObject);
		}

		if ( ViewNewInspector ){
			GUILayout.Space (5);
			using (new Horizontal()) {
				if (GUILayout.Button ( "Main Settings", "toolbarbutton", GUILayout.ExpandWidth (true))) {
					ShowMain = !ShowMain;
				}
				if ( !ShowMain )  GUI.color = Color.white;
				else GUI.color = Green;
				if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
					ShowMain = !ShowMain;
				}
			}
			if ( ShowMain ){
				GUI.color = Color.white;
				Avatar.Name = (string)EditorGUILayout.TextField("Name in Game",Avatar.Name,  GUILayout.ExpandWidth (true));
				if ( ViewHelper ) EditorGUILayout.HelpBox("IsPlayer is used for the LOD to generate the Player avatar first. It is also used by the various DK Integrations (Premium only).", UnityEditor.MessageType.None);
				Avatar.IsPlayer = EditorGUILayout.Toggle ( "Is the Player ?",Avatar.IsPlayer);
				if ( ViewHelper ) EditorGUILayout.HelpBox("The Gender and the Race of the avatar. It can not be modified or the avatar will be corrupted.", UnityEditor.MessageType.None);
				EditorGUILayout.LabelField ("Gender :",Avatar.Gender,  GUILayout.ExpandWidth (true));
				EditorGUILayout.LabelField ( "Race :",Avatar.Race,  GUILayout.ExpandWidth (true));
				EditorGUILayout.ObjectField("DK RaceData",Avatar.RaceData,typeof(DKRaceData),false);

				if ( ViewHelper ) EditorGUILayout.HelpBox("To use a DK UMA avatar with a first person controller. This option is used to generate a full avatar or to hide the head," +
					" the hands and to reduce the arms to the minimum possible.", UnityEditor.MessageType.None);
				Avatar.AnatomyRule.AnatomyToCreate = (DK_RPG_UMA.AnatomyChoice) EditorGUILayout.EnumPopup("Anatomy Rule :", Avatar.AnatomyRule.AnatomyToCreate);

				if ( ViewHelper ) EditorGUILayout.HelpBox("In some particular cases it could be necessary to hide the avatar at start after it to be generated.", UnityEditor.MessageType.None);
				Transpose.HideAtStart = EditorGUILayout.Toggle ( "Hide at Start ?",Transpose.HideAtStart);

				if ( ViewHelper ) EditorGUILayout.HelpBox("The Animation Controller assigned to the Avatar when it is generated. Can be modified.", UnityEditor.MessageType.None);
				UMAAvatar.animationController = 
					(RuntimeAnimatorController)EditorGUILayout.ObjectField("Animation Controller",UMAAvatar.animationController,typeof(RuntimeAnimatorController),false);
			}

			// Shape
			GUILayout.Space (5);
			GUI.color = Color.white;
			using (new Horizontal()) {
				if (GUILayout.Button ( "Shape Wizard", "toolbarbutton", GUILayout.ExpandWidth (true))) {
					ShowShape = !ShowShape;
				}
				if ( !ShowShape )  GUI.color = Color.white;
				else GUI.color = Green;
				if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
					ShowShape = !ShowShape;
				}
			}
			if ( ShowShape ){
				GUI.color = Color.white;
				EditorGUILayout.HelpBox("DK UMA RPG Editor or Premium Only.", UnityEditor.MessageType.None);

				#if DK_UMA_RPG_Editor

				if (GUILayout.Button ( "Open the Shape Wizard", GUILayout.ExpandWidth (true))) {
					EditorWindow.GetWindow(typeof(DK_RPG_UMA_DNA), false, "Avatar Shape");
				}
				#else
				// open the buy premium or buy RPG Editor
				if (GUILayout.Button ( "Premium Pack page", GUILayout.ExpandWidth (true))) {
					Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/45131");
				}
				if (GUILayout.Button ( "RPG Editor page", GUILayout.ExpandWidth (true))) {
					Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/37697");
				}
				#endif
			}

			// equipment
			GUILayout.Space (5);
			GUI.color = Color.white;
			using (new Horizontal()) {
				if (GUILayout.Button ( "Elements Wizard", "toolbarbutton", GUILayout.ExpandWidth (true))) {
					ShowEquipment = !ShowEquipment;
				}
				if ( !ShowEquipment )  GUI.color = Color.white;
				else GUI.color = Green;
				if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
					ShowEquipment = !ShowEquipment;
				}
			}
			if ( ShowEquipment ){
				GUI.color = Color.white;
				EditorGUILayout.HelpBox("DK UMA RPG Editor or Premium Only.", UnityEditor.MessageType.None);

				#if DK_UMA_RPG_Editor

				if (GUILayout.Button ( "Open the Elements Wizard", GUILayout.ExpandWidth (true))) {
					EditorWindow.GetWindow(typeof(DK_RPG_UMA_Avatar_Win), false, "Avatar Elements");
				}
				#else
				// open the buy premium or buy RPG Editor
				if (GUILayout.Button ( "Premium Pack page", GUILayout.ExpandWidth (true))) {
				Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/45131");
				}
				if (GUILayout.Button ( "RPG Editor page", GUILayout.ExpandWidth (true))) {
				Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/37697");
				}
				#endif
			}

			// equipment set
			GUILayout.Space (5);
			GUI.color = Color.white;
			using (new Horizontal()) {
				if (GUILayout.Button ( "Equipment Set", "toolbarbutton", GUILayout.ExpandWidth (true))) {
					ShowEquipmentSet = !ShowEquipmentSet;
				}
				if ( !ShowEquipmentSet )  GUI.color = Color.white;
				else GUI.color = Green;
				if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
					ShowEquipmentSet = !ShowEquipmentSet;
				}
			}
			if ( ShowEquipmentSet ){
				GUI.color = Color.white;
				if ( ViewHelper ) EditorGUILayout.HelpBox("Premium and Ingame Creator Only. An avatar can equip an Equipment Set. It is usefull to quickly equip an NPC.", UnityEditor.MessageType.None);
				Avatar.EquipmentSet.UseDKEquipmentSet = EditorGUILayout.Toggle ( "Use Equipment Set ?",Avatar.EquipmentSet.UseDKEquipmentSet);
				if ( Avatar.EquipmentSet.UseDKEquipmentSet ){
					if ( ViewHelper ) EditorGUILayout.HelpBox("Select an Equipment Set to equip, it is loaded automatically.", UnityEditor.MessageType.None);
					DKEquipmentSetData oldSet = Avatar.EquipmentSet.DKEquipmentSet;
					Avatar.EquipmentSet.DKEquipmentSet = 
						(DKEquipmentSetData)EditorGUILayout.ObjectField("Equipment Set",Avatar.EquipmentSet.DKEquipmentSet,typeof(DKEquipmentSetData),false);
					if ( oldSet != Avatar.EquipmentSet.DKEquipmentSet ) { 
						LoadEquipmentSet ();
						Avatar.Rebuild ();
					}
				}
				if ( Avatar.EquipmentSet.SetLoaded ){
					EditorWindow.GetWindow(typeof(DK_UMA_InfoDialog_Win), false, "DialogBox");
					DK_UMA_InfoDialog_Win.EquipmentSetLoaded = true;


				}
				// Strip off the avatar
				using (new Horizontal()) {
					if (GUILayout.Button ( "Strip off the avatar", GUILayout.ExpandWidth (true))) {
						Avatar.EquipmentSet = null;
						Avatar._Equipment = new DK_RPG_UMA.EquipmentData ();
						Avatar.Rebuild ();
					}
					if (GUILayout.Button ( "No underwear", GUILayout.ExpandWidth (true))) {
						Avatar._Avatar._Body._Underwear.Slot = null;
						Avatar._Avatar._Body._Underwear.Overlay = null;
						Avatar.Rebuild ();
					}
				}
				if ( !SaveSet && GUILayout.Button ( "Save Equipment Set", GUILayout.ExpandWidth (true))) {
					SaveSet = true;
				}
				if ( SaveSet ){
					EditorGUILayout.HelpBox("write the name of the Set in the 'Set Name' field or select an existing set to update.", UnityEditor.MessageType.None);
					if ( NewSaveName == "" && SelectedSet == null ) GUI.color = Red; 
					else GUI.color = Green; 
					NewSetName = (string)EditorGUILayout.TextField("Set Name",NewSetName,  GUILayout.ExpandWidth (true));
					GUILayout.Label ("OR", GUILayout.ExpandWidth (true));
					SelectedSet = 
						(DKEquipmentSetData)EditorGUILayout.ObjectField("Equipment Set",SelectedSet,typeof(DKEquipmentSetData),false);

					if ( NewSaveName != "" || SelectedSet != null ){
						if (GUILayout.Button ( "Save the Equipment Set", GUILayout.ExpandWidth (true))) {
							SaveEquipmentSet ();
						}
					}
					GUI.color = Color.white;
					if (GUILayout.Button ( "Cancel save Equipment Set", GUILayout.ExpandWidth (true))) {
						SaveSet = false;
						SelectedSet = null;
						NewSetName = "";
					}
				}
			}

			// load / save
			GUILayout.Space (5);
			using (new Horizontal()) {
				if (GUILayout.Button ( "Load / Save Avatar", "toolbarbutton", GUILayout.ExpandWidth (true))) {
					ShowSave = !ShowSave;
				}
				if ( !ShowSave )  GUI.color = Color.white;
				else GUI.color = Green;
				if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
					ShowSave = !ShowSave;
				}
			}
			if ( ShowSave ){
				GUI.color = Color.white;
				if ( ViewHelper ) EditorGUILayout.HelpBox("Premium and Ingame Creator Only. An avatar can be generated by itself or loaded from a saved file or loaded from a Data object.", UnityEditor.MessageType.None);
			//	Avatar.LoadFrom = (DK_RPG_UMA.LoadFromChoice) EditorGUILayout.EnumPopup("Load From :", Avatar.LoadFrom);

				Avatar.LoadFromFile = EditorGUILayout.Toggle ( "Load From File",Avatar.LoadFromFile);

			//	if ( Avatar.LoadFrom == DK_RPG_UMA.LoadFromChoice.Data ) {
				if ( Avatar.LoadFromFile == false ) {
				//	Avatar.LoadFromFile = false;
					if ( ViewHelper ) EditorGUILayout.HelpBox("Premium and Ingame Creator Only. Using the Ingame Creator you can save an avatar to a Data Object. " +
						"Assign an Avatar Data for it to be loaded at start. A Data Object can not be modified in Game Play.", UnityEditor.MessageType.None);
					DK_UMA_AvatarData oldAvData = Avatar.AvatarFromDB;

					Avatar.AvatarFromDB = 
						(DK_UMA_AvatarData)EditorGUILayout.ObjectField("Avatar Data",Avatar.AvatarFromDB,typeof(DK_UMA_AvatarData),false);
					// refresh the avatar
					if ( oldAvData != Avatar.AvatarFromDB ){
						if (Avatar.AvatarFromDB != null ){
							// get definition
							Avatar.SavedRPGStreamed = Avatar.AvatarFromDB.StreamedAvatar;
							// load the avatar
							Avatar.LoadRPGStreamed ();

							Avatar.AvatarLoaded = true;

						}
					}
				}
			//	if ( Avatar.LoadFrom == DK_RPG_UMA.LoadFromChoice.File ) {
				if ( Avatar.LoadFromFile == true ) {
				//	Avatar.LoadFromFile = true;
					if ( ViewHelper ) EditorGUILayout.HelpBox("Premium and Ingame Creator Only. Using the Ingame Creator you can save an avatar to a file. Is the avatar RPG Definition loaded from a saved file ? " +
						"If so, write the name of the file in the 'File Name' field.", UnityEditor.MessageType.None);
					Avatar.FileName = (string)EditorGUILayout.TextField("File Name",Avatar.FileName,  GUILayout.ExpandWidth (true));
				}
				// dialog box
				if ( Avatar.AvatarLoaded ){
					EditorWindow.GetWindow(typeof(DK_UMA_InfoDialog_Win), false, "DialogBox");
					DK_UMA_InfoDialog_Win.AvatarLoaded = true;
				}

				// save the avatar
				if ( SaveToData == false && SaveToFile == false ){
					using (new Horizontal()) {
						if (GUILayout.Button ( "Save as file", GUILayout.ExpandWidth (true))) {
							SaveToFile = true;
						}
						if (GUILayout.Button ( "Save as Data", GUILayout.ExpandWidth (true))) {
							SaveToData = true;
						}
					}
				}
				else {
					EditorGUILayout.HelpBox("write the name of the save in the 'Save Name' field.", UnityEditor.MessageType.None);
					if ( NewSaveName == "" ) GUI.color = Red; 
					else GUI.color = Green; 
					if ( SaveToFile ) NewSaveName = (string)EditorGUILayout.TextField("File Name",NewSaveName,  GUILayout.ExpandWidth (true));
					else if ( SaveToData ) NewSaveName = (string)EditorGUILayout.TextField("Data Name",NewSaveName,  GUILayout.ExpandWidth (true));
					if ( NewSaveName != "" ){
						if (GUILayout.Button ( "Save the avatar", GUILayout.ExpandWidth (true))) {
							DK_UMACrowd _DK_UMACrowd = GameObject.Find ("DKUMACrowd").GetComponent<DK_UMACrowd>();
							if ( _DK_UMACrowd != null ){								
								DK_UMA_Save.SaveCompleteAvatar ( Avatar, NewSaveName, _DK_UMACrowd, SaveToFile, SaveToData );
								if ( SaveToData ) Debug.Log ( "Avatar '"+NewSaveName+"' have been saved as an AvatarData." );
								else if ( SaveToFile ) Debug.Log ( "Avatar '"+NewSaveName+"' have been saved as a file." );

								SaveToData = false;
								SaveToFile = false;
							}
							else {
								Debug.LogError ("Can not save the avatar, the current scene requires to be ready for DK UMA to be able to save the avatar");
								Debug.LogError ("Open the DK UMA Editor, it will prepare the scene for DK UMA automatically.");
								SaveToData = false;
								SaveToFile = false;
							}
						}
					}
					GUI.color = Color.white;
					if (GUILayout.Button ( "Cancel save", GUILayout.ExpandWidth (true))) {
						SaveToData = false;
						SaveToFile = false;
					}
				}
			/*	if ( Avatar.LoadFrom == DK_RPG_UMA.LoadFromChoice.None ) {
					Avatar.LoadFromFile = false;
					Avatar.AvatarFromDB = null;
				}
				if ( Avatar.LoadFromFile ) Avatar.LoadFrom = DK_RPG_UMA.LoadFromChoice.File;
				else if ( Avatar.AvatarFromDB != null || Avatar.LoadFrom == DK_RPG_UMA.LoadFromChoice.Data ) Avatar.LoadFrom = DK_RPG_UMA.LoadFromChoice.Data;
				else {
					Avatar.LoadFrom = DK_RPG_UMA.LoadFromChoice.None;
					Avatar.LoadFromFile = false;
					Avatar.AvatarFromDB = null;
				}*/
			}
		}
		else {
			if ( ViewHelper ) EditorGUILayout.HelpBox("Most of the properties have an HelpBox, position the mouse over it to have some information about it.", UnityEditor.MessageType.None);
			DrawDefaultInspector();
		}

		GUILayout.Space (5);
		GUI.color = Color.white;
		using (new Horizontal()) {
			if (GUILayout.Button ( "UMA Avatar", "toolbarbutton", GUILayout.ExpandWidth (true))) {
				ShowUMA = !ShowUMA;
			}
			if ( !ShowUMA )  GUI.color = Color.white;
			else GUI.color = Green;
			if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				ShowUMA = !ShowUMA;
			}
		}
		if ( ShowUMA ){
			GUI.color = Color.white;
			if ( ViewHelper ) EditorGUILayout.HelpBox("You can access to the UMA Avatar. It can be usefull to modify the 'Character Created', 'Character Destroyed' and 'Character Updated' properties. To do so, click on the button and unfold the UMADynamicAvatar component.", UnityEditor.MessageType.None);
			if (GUILayout.Button ( "Access to the UMA Avatar", GUILayout.ExpandWidth (true))) {
				Selection.activeObject = UMAAvatar.gameObject;
			}
		}
	}

	void LoadEquipmentSet (){
		if ( Avatar.EquipmentSet.UseDKEquipmentSet && Avatar.EquipmentSet.DKEquipmentSet != null ) {
			DKLoadEquipmentSet.LoadingSet ( Avatar.EquipmentSet.DKEquipmentSet, Avatar );
			Avatar.EquipmentSet.SetLoaded = true;

		}
	}

	void SaveEquipmentSet (){
		DKSaveEquipmentSet.SavingSet ( NewSetName, SelectedSet, Avatar );
		SaveSet = false;
		Debug.Log ( "Equipment Set have been saved." );
		SelectedSet = null;
		NewSetName = "";
	}
}
