using UnityEngine;
using System.Collections;
using UnityEditor;


public class DKUMAVersionWin : EditorWindow {

//	Color Green = new Color (0.8f, 1f, 0.8f, 1);
//	Color Red = new Color (0.9f, 0.5f, 0.5f);

	Vector2 scroll;


	void OnGUI () {
		this.minSize = new Vector2(370, 500);

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

		GUILayout.Label ( "DK U.M.A.2 Editor information", "toolbarbutton", GUILayout.ExpandWidth (true));

		using (new ScrollView(ref scroll)) {

			GUI.color = Color.white;
			GUILayout.TextField("It is a very early new version, so please report any trouble about it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUILayout.Space (10);
			GUILayout.TextField("v2.6.3 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("- Auto preparation of the scene when a DK UMA avatar is instantiated. " +
				"Drag and drop a prefab or Instantiate it by code and all the necessary objects are added " +
				"to the scene in Editor mode or at runtime.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- Import automatically the UMA content from the UMA Wardrobe Recipes.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- Import and setup automatically the cloth Physics for the imported elements.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- Update of the DK UMA Editor for more correctors to detect anything concerning the UMA and DK UMA content and to store them in the database.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- Automatically add the necessary elements to the libraries of the scene at runtime.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- Automatically correct the libraries of the scene at runtime.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- Update of the DK Race Editor, including some options to fasten the management of the Color Presets.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- Update of the Color Presets Editor, for sorting depending of the Type of the presets.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- Modification of the Ingame Creator separating its UI components from the DK_UMA object of the scene.", UnityEditor.MessageType.None);


			GUILayout.Space (10);
			GUILayout.TextField("v2.6.2 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("- Cloth Physics support added.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- A new tool, the 'Import UMA Content Editor' to help you import the UMA content.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- 'Ingame Creator' update with more information in the editor window and an updated EquipmentSets process and same for the save avatar to Database.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- New Corrector Tab to apply the properties of the Places if necessary and to find the missing Linked Elements.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- New system to fix the libraries.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- New system to grab automarically the missing elements if available in the project.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- A new process to add automatically the required DK elements to the libraries when missing.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("- More information in the tool.", UnityEditor.MessageType.None);

			GUILayout.Space (10);
			GUILayout.TextField("v2.4.4 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("Ingame Creator : Update adding a 'basic / naked' equipment to avoid the avatar to be wearing the vine leaf.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Ingame Creator : Update adding toggles to display or not some options, " +
				"avoiding the little windows to be containing useless settings for those devs unable to offer all the features available in the DK UMA engine.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Natural Behaviour : Update about the detection of the avatars to be able to use it.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("A lot of fixes about the avatars generation, tatoos and body painting, avatar save / load and more.", UnityEditor.MessageType.None);

			GUILayout.Space (10);
			GUILayout.TextField("v2.4.3 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("New Inspector for the DK UMA Avatar, enabling almost all the customization of an avatar in the Editor mode, including the Load and Save functions and the Equipment Set.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("DK UMA Packer : This tool gathers all the DK UMA Content (slots, overlays, races, Color Presets, Places and Avatars Data), then it save them as a unityPackage.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Ingame Creator : Update fixing some problems about tatoos and body painting.", UnityEditor.MessageType.None);

			GUILayout.Space (10);
			GUILayout.TextField("v2.4.2 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("Editor Preview avatars : After more than one year, the Preview avatars are back.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("RPG Editor : Some fixes about the sub layer of the clothes and the storage of the modified avatar definition.", UnityEditor.MessageType.None);

			GUILayout.Space (10);
			GUILayout.TextField("v2.4.1 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("Ooti Motion Controller : A new integration to enable Ooti Motion Controller to use the DK UMA avatars.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Natural Behaviour v2.4 : A totally rewritten version of the Natural Behaviour tool, to give some life to your avatars.", UnityEditor.MessageType.None);

			GUILayout.Space (10);
			GUILayout.TextField("v2.4.0 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("Invector Combat v2 : The new integration for the Invector Combat v2 with the combat system and the inventory management.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Race Editor / Creator : A new tool to prepare the UMA and the DK UMA races for your avatars.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Items Manager : A new tool to prepare and manage your DK UMA slots and overlays as items for an Inventory System.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("New Editor UI and Inspectors displaying detailled information about most of the DK UMA system.", UnityEditor.MessageType.None);
			GUILayout.Space (10);
			GUILayout.TextField("v2.3.9 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("Updated ORK integration : Refresh the avatar DNA after changing zone." 
				+" Save the player avatar to its file after equipping, unequipping gear. Building the game is now possible.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Updated Invector TPC integration : The DK avatars and NPCs V2 are now working properly. Use the prefabs.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Equipment Sets : Assign directly an Equipment Set to an avatar (DK_RPG_UMA) for him to equip it at loading.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Updated Elements Manager for the races management.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("New Inspector for the DK UMA avatars.", UnityEditor.MessageType.None);
			GUILayout.Space (10);
			GUILayout.TextField("v2.3.8 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("Detailed Tool tips added to the inspector for the most used DK UMA components.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("A new setting is available in the DK_RPG_UMA component, the Anatomy Rule. " +
				"It enables the engine to skip generating the head, the hand and reduce the arms DNA to -0.5. " +
				"It can be usefull for the usage of the DK UMA avatar as a first person avatar.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("The engine is ready to work with the new tools and also for a Photon network support.", UnityEditor.MessageType.None);
			GUILayout.Space (10);
			GUILayout.TextField("v2.3.5 :" , 300, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("The slots and overlays libraries of any scene are now fully dynamic depending of the 'DK UMA Game Settings' of your project. " 
				+"That means the libraries of the scenes can be left blank " 
				+"avoiding for you to have to modify all the scenes of your game about that.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("The Color Presets Editor window have been rewritten to be dynamic.", UnityEditor.MessageType.None);
			GUILayout.Space (10);
			GUILayout.TextField("v2.3.4 :" , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("The DK Slots preview images are back. If a preview image of an element is present in the same folder, " +
				"it will be displayed in the elements manager.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("The function for the generation of multiple avatars is now back into the DK UMA Editor.", UnityEditor.MessageType.None);
			GUILayout.Space (10);
			GUILayout.TextField("v2.3.3 :" , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("The Stacked Overlays function is now ready.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("The equipment elements are now able to stack 3 different overlays for the aspect and 2 more overlays " +
				"for the dirt will be added, this feature is ready to works but is not saved to the avatar. It will be ready for the 2.3.3 update.", UnityEditor.MessageType.None);
			GUILayout.Space (10);
			GUILayout.TextField("v2.3.0 :" , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("A new 'Cutout' UMA material is available for some of the UMA2 elements assets previously not displaying the overlay.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("'Equipment sets' have been migrated into the DKUMA_Variables.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Equipment sets functions are now ready.", UnityEditor.MessageType.None);
			EditorGUILayout.HelpBox("Code modifications for less alerts.", UnityEditor.MessageType.None);

		}
	}
}
