using UnityEngine;
using System.Collections;
using UnityEditor;

public class DK_UMA_WelcomeTab : EditorWindow {
	public static Color Green = new Color (0.8f, 1f, 0.8f, 1);
	public static Color Red = new Color (0.9f, 0.5f, 0.5f);



	public static Vector2 scroll;

//	public static GameObject DK_UMA;

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

		GUI.color = Color.white;
		using (new HorizontalCentered()) {
			GUILayout.TextField ("Welcome to the DK UMA Editor", 256, bold, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		}


		using (new Horizontal()) {
			GUI.color = Green;
			if ( GUILayout.Button("For idea, request or assistance, please contact us", GUILayout.ExpandWidth (true))) 
			{
				Application.OpenURL ( "http://alteredreality.wix.com/dk-uma#!contact/c24vq" );
			}
		}

		GUI.color = Green;
		using (new HorizontalCentered())
			GUILayout.TextField("Enjoy it, rate it ! And get more content." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		#region detect Slots Libraries

		#region Anatomy Library detection
		if ( DK_UMA_Editor.DK_UMA != null && DK_UMA_Editor._UMA != null ){
			EditorVariables.SlotsAnatomyLibraryObj = GameObject.Find ("DK_SlotsAnatomyLibrary");
			GUI.color = Red;
			if (EditorVariables.SlotsAnatomyLibraryObj == null)
				EditorGUILayout.HelpBox("You need to create an Anatomy Library. It contains all the Generic body parts used by the DK Editor to create your Avatars.", UnityEditor.MessageType.None);
			if (EditorVariables.SlotsAnatomyLibraryObj == null) {
				GUI.color = Green;
				if (GUILayout.Button ("Create the Anatomy Library", GUILayout.ExpandWidth (true))) {
					EditorVariables.SlotsAnatomyLibraryObj = (GameObject)PrefabUtility.InstantiatePrefab (Resources.Load ("DK_SlotsAnatomyLibrary"));
					EditorVariables.SlotsAnatomyLibraryObj.name = "DK_SlotsAnatomyLibrary";
					EditorVariables._SlotsAnatomyLibrary = EditorVariables.SlotsAnatomyLibraryObj.GetComponent<DK_SlotsAnatomyLibrary> ();
					DK_UMA_Editor.DK_UMA = GameObject.Find ("DK_UMA");
					if (DK_UMA_Editor.DK_UMA == null) {
						var goDK_UMA = new GameObject ();
						goDK_UMA.name = "DK_UMA";
						DK_UMA_Editor.DK_UMA = GameObject.Find ("DK_UMA");
					}
					EditorVariables.SlotsAnatomyLibraryObj.transform.parent = DK_UMA_Editor.DK_UMA.transform;
					DetectAndAddDK.AddAll ();
				}
			} else if (EditorVariables._SlotsAnatomyLibrary == null)
				EditorVariables._SlotsAnatomyLibrary = EditorVariables.SlotsAnatomyLibraryObj.GetComponent<DK_SlotsAnatomyLibrary> ();
		}
		#endregion Anatomy Library detection
		#endregion detect Slots Libraries

		GUI.color = Color.white;

		using (new Horizontal()) {
			if (  GUILayout.Button("How to use it", GUILayout.ExpandWidth (true))){
				DK_UMA_Editor.OpenForumTuto ();
			}
			if (  GUILayout.Button("Web Documentation", GUILayout.ExpandWidth (true))){
				DK_UMA_Editor.OpenDocumentationLink ();
			}
			if ( GUILayout.Button ( "Version information", GUILayout.ExpandWidth (true))) {
				DK_UMA_Editor.OpenVersionWin ();
			}
		}
	/*	GUI.color = Color.yellow;
		EditorGUILayout.HelpBox("DK UMA is constantly Updated and sometime requires some hotfix.", UnityEditor.MessageType.None);
		using (new Horizontal()) {
			GUI.color = Color.yellow;
			if ( GUILayout.Button("Hotfixes on the DK Forum", GUILayout.ExpandWidth (true))) 
			{
				Application.OpenURL ( "http://unity3d-dk-tools.boards.net/board/47/hotfixes" );
			}
		}*/
			
		if ( DK_UMA_Editor.DK_UMA != null && DK_UMA_Editor._UMA != null ){
			GUI.color = Color.white;
			using (new ScrollView(ref scroll)) {
				if (EditorVariables.DK_UMACrowd ) {
					GUI.color = Color.white;
					EditorGUILayout.HelpBox("Helper : You can activate the helper comments by clicking on the '?' at the end of the ontop menu.", UnityEditor.MessageType.None);

					#region Help
					if ( DK_UMA_Editor.Helper ) {
						GUI.color = Color.yellow;
						EditorGUILayout.HelpBox("Welcome to the DK UMA Editor. With this tool you can create and edit your DK UMA Avatars in Editor mode or Runtime mode.", UnityEditor.MessageType.Info);

						EditorGUILayout.HelpBox("To work properly, DK UMA and UMA have to be installed to the current scene." +
							" The DK UMA prefab is automatically installed to the current scene when opening the DK UMA Editor window." +
							" Use the Elements Manager to install the UMA prefab to the scene.", UnityEditor.MessageType.Info);

						EditorGUILayout.HelpBox("Use the Elements Manager to import any UMA element for DK UMA to be able to use it. DK UMA can not edit the basic UMA Avatars.", UnityEditor.MessageType.Info);
					}
					#endregion Help

					GUI.color = Color.white;
					using (new Horizontal()) {
						EditorGUILayout.HelpBox("Open directly the example scene to try the tool.", UnityEditor.MessageType.None);
						if (  GUILayout.Button("Open the Example scene", GUILayout.Width (170))){
							UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
							UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/DK Editors/DK_UMA_Editor/Scenes/Example Scene.unity");
						}
					}

					// navigate

					EditorGUILayout.HelpBox("Navigation : You can navigate from any DK UMA tab to another tab by clicking on the tabs of the DK Editor window.", UnityEditor.MessageType.None);
					GUILayout.Space (10);
					GUI.color = Color.white;
					GUILayout.Label ("commonly used windows", "toolbarbutton", GUILayout.ExpandWidth (true));
					EditorGUILayout.HelpBox("The 'Elements Manager' is the interface to detect, list and manage all the UMA and DK UMA elements, " +
						"offering a usefull overview of your UMA and DK UMA project.", UnityEditor.MessageType.None);
					if ( GUILayout.Button ( "Open the Elements Manager", GUILayout.ExpandWidth (true))) {
						DK_UMA_Editor.OpenAutoDetectWin();
					}

					GUI.color = Color.white;
					EditorGUILayout.HelpBox("In the Prepare panel you can setup the DK UMA contents. It is dynamic, " +
						"just select a UMA or DK element and the corresponding options will be displayed.", UnityEditor.MessageType.None);
					if ( EditorVariables.DK_UMACrowd && GUILayout.Button ("Go to the Prepare menu", GUILayout.ExpandWidth (true))) {
						DK_UMA_Editor.ShowPrepare = true;
						DK_UMA_Editor.Step0 = true;
						DK_UMA_Editor.ResetSteps ();	
						DKUMACleanLibraries.CleanLibraries ();
					}

					GUI.color = Color.white;
					EditorGUILayout.HelpBox("Use the Create Panel to create an Avatar, just follow the steps to create it as you want it," +
						" randomly or manually.", UnityEditor.MessageType.None);

					GUI.color = Color.white;
					if ( EditorVariables.DK_UMACrowd 						
						&& GUILayout.Button ("Go to the Create menu", GUILayout.ExpandWidth (true))) {
						DK_UMA_Editor.showCreate = true;
						DK_UMA_Editor.Step0 = true;
						DK_UMA_Editor.ResetSteps ();	
						DKUMACleanLibraries.CleanLibraries ();
					}
					EditorGUILayout.HelpBox("Advanced Tools are available for DK UMA to create avatars and manage the equipment in game or using the Editors " +
						"and DK UMA Integrations to the best RPG Frameworks ans 3rd Person Controller actually on the Asset Store.", UnityEditor.MessageType.None);

					if (  GUILayout.Button("Tools and Integrations overview", GUILayout.ExpandWidth (true))){
						DK_UMA_Editor.showAbout = true;
						DK_UMA_Editor.Step0 = true;
						DK_UMA_Editor.ResetSteps ();		
					}
				}
			}
		}
	}
}
