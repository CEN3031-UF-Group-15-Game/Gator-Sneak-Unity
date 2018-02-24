using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;



public class DK_UMA_Installer_Win : EditorWindow {

	Vector2 scroll;
//	Color Green = new Color (0.8f, 1f, 0.8f, 1);
//	Color Red = new Color (0.9f, 0.5f, 0.5f);



	[MenuItem("UMA/DK Editor/Installer/Installer")]
	[MenuItem("Window/DK Editors/DK UMA/Installer/Installer")]
	public static void Init()
	{
		// Get existing open window or if none, make a new one:
		DK_UMA_Installer_Win window = EditorWindow.GetWindow<DK_UMA_Installer_Win> (false, "DKUMA Install");
		window.autoRepaintOnSceneChange = true;
		window.Show ();
	}

	void OnGUI () {
		this.minSize = new Vector2(330, 620);
		this.maxSize = new Vector2(340, 630);

		#region fonts variables
		var bold = new GUIStyle ("label");
		var boldFold = new GUIStyle ("foldout");
		bold.fontStyle = FontStyle.Bold;
	//	bold.fontSize = 14;
		boldFold.fontStyle = FontStyle.Bold;
		var Slim = new GUIStyle ("label");
		Slim.fontStyle = FontStyle.Normal;
		Slim.fontSize = 10;	
		var style = new GUIStyle ("label");
		style.wordWrap = true;
		#endregion fonts variables

		using (new Horizontal()) {
			GUI.color = Color.white ;
			GUILayout.Label ( "DK UMA Installer", "toolbarbutton", GUILayout.ExpandWidth (true));
		}

		using (new ScrollView(ref scroll)) {
			#region UMA Installed

			#if UNITY_5_5_OR_NEWER
			GUI.color = Color.white ;
			EditorGUILayout.HelpBox("Unity 5.5 or newer detected. ", UnityEditor.MessageType.None);					
			#else
			EditorGUILayout.HelpBox("Unity version is lower than 5.5.", UnityEditor.MessageType.None);
			#endif


			GUILayout.Label ( "UMA Version", "toolbarbutton", GUILayout.ExpandWidth (true));
			// verify UMA installation
			if ( AssetDatabase.IsValidFolder ("Assets/UMA") == true ){
				#region UMA
				// UMA 2.6
				if ( AssetDatabase.IsValidFolder ("Assets/UMA/Core/Extensions/DynamicCharacterSystem") == true ){
					EditorGUILayout.HelpBox("UMA 2.6 is installed.", UnityEditor.MessageType.None);
				}
				else {
					EditorGUILayout.HelpBox("UMA 2.0.5 is installed.", UnityEditor.MessageType.None);
				}
				#endregion UMA

				#region DK UMA
				GUILayout.Label ( "DK UMA Version", "toolbarbutton", GUILayout.ExpandWidth (true));
				if ( AssetDatabase.IsValidFolder ("Assets/DK Editors/DK_UMA_Editor/Engine") == true ){
					EditorGUILayout.HelpBox("DK UMA is installed.", UnityEditor.MessageType.None);
					if ( AssetDatabase.IsValidFolder ("Assets/DK Editors/DK_UMA_Content") == true ){
						// pack content
						EditorGUILayout.HelpBox("Remember to save your DK UMA content before deleting DK UMA " +
							"and updating the DK UMA Default Content or your work will be lost ! Use the Project Packer.", UnityEditor.MessageType.Warning);
					
						if ( GUILayout.Button ( "Open the DK UMA Project Packer", GUILayout.ExpandWidth (true))) {
							#if DK_UMA_Editor
							GetWindow(typeof(DK_UMA_ProjectPacker_Win), false, "DK UMA Packer");
							#endif
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
					// UMA 2.0.5
					using (new Horizontal()) {
						if ( AssetDatabase.IsValidFolder ("Assets/UMA/Core/Extensions/DynamicCharacterSystem") == false ){
							if ( GUILayout.Button ( "Reinstall DK UMA for UMA 2.0.5", GUILayout.ExpandWidth (true))) {
								InstallDKUMAForUMA205 ();							
							
								InitDefineDKUMA ();
							}
						}
						// UMA 2.5
						else {
							if ( GUILayout.Button ( "Reinstall DK UMA for UMA 2.6", GUILayout.ExpandWidth (true))) {
								InstallDKUMAForUMA25 ();								
							
								InitDefineDKUMA ();
							}
						}
						GUI.color = Color.yellow ;
						if ( GUILayout.Button ( "Delete", GUILayout.Width (50))) {
							FileUtil.DeleteFileOrDirectory (Application.dataPath+"/DK Editors/DK_UMA_Editor/Engine/Scripts/Camera Scripts/CamMouseOrbit2.js");
							FileUtil.DeleteFileOrDirectory (Application.dataPath+"/DK Editors/DK_UMA_Editor");		
							FileUtil.DeleteFileOrDirectory (Application.dataPath+"/DK Editors/DK_UMA_Content");		

							// Define
							RemoveDefineDKUMA ();
							RemoveDefineDKUMAPluIns ();
							AssetDatabase.Refresh ();
						}
					}
					#region PlugIns Install
					GUI.color = Color.white ;
					GUILayout.Label ( "DK UMA PlugIns", "toolbarbutton", GUILayout.ExpandWidth (true));
					if ( AssetDatabase.FindAssets ("DK UMA PlugIns").Length != 0 ){
						if ( AssetDatabase.IsValidFolder ("Assets/DK Editors/DK_UMA_Editor/PlugIns") == false ) {
							GUI.color = Color.yellow ;
							EditorGUILayout.HelpBox("PlugIns have been found but are not installed into your project.", UnityEditor.MessageType.Warning);
							GUI.color = Color.white ;
							if ( GUILayout.Button ( "Install the DK UMA PlugIns", GUILayout.ExpandWidth (true))) {
								if ( AssetDatabase.IsValidFolder ("Assets/UMA/Core/Extensions/DynamicCharacterSystem") == true ){
									// 2.6
									AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
										"Packages/PlugIns/DK UMA PlugIns 2.6.unitypackage", false);
								}
								else {
									// 2.0.5
									AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
									"Packages/PlugIns/DK UMA PlugIns.unitypackage", false);
								}
								// Define
								InitDefineDKUMARPG ();
							}
						}
						else {
							EditorGUILayout.HelpBox("PlugIns are installed into your project.", UnityEditor.MessageType.None);
							EditorGUILayout.HelpBox("If you just updated DK UMA, you can refresh or update the PlugIns of your project" +
								" by clicking on the next button.", UnityEditor.MessageType.Info);
							using (new Horizontal()) {
								if ( GUILayout.Button ( "Reinstall the DK UMA PlugIns", GUILayout.ExpandWidth (true))) {
									if ( AssetDatabase.IsValidFolder ("Assets/UMA/Core/Extensions/DynamicCharacterSystem") == true ){
										// 2.6
										AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
											"Packages/PlugIns/DK UMA PlugIns 2.6.unitypackage", false);
									}
									else {
										// 2.0.5
										AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
											"Packages/PlugIns/DK UMA PlugIns.unitypackage", false);
									}
								
									// Define
									InitDefineDKUMARPG ();
								}
								GUI.color = Color.yellow ;
								if ( GUILayout.Button ( "Delete", GUILayout.Width (50))) {
									FileUtil.DeleteFileOrDirectory (Application.dataPath+"/DK Editors/DK_UMA_Editor/PlugIns");		

									// Define
									RemoveDefineDKUMAPluIns ();
									AssetDatabase.Refresh ();
								}
							}
							#region Integrations Install
							GUI.color = Color.white ;
							GUILayout.Label ( "DK UMA Integrations", "toolbarbutton", GUILayout.ExpandWidth (true));
							if ( AssetDatabase.FindAssets ("DK UMA Integrations").Length != 0
								&& AssetDatabase.IsValidFolder ("Assets/DK Editors/DK_UMA_Editor/PlugIns/Integrations") == false
								&& AssetDatabase.IsValidFolder ("Assets/DK Editors/DK_UMA_Editor/PlugIns/Controllers") == false ) {
								GUI.color = Color.yellow ;
								EditorGUILayout.HelpBox("Integrations have been found but are not installed into your project.", UnityEditor.MessageType.Warning);
								GUI.color = Color.white ;
								if ( GUILayout.Button ( "Install the DK UMA Integrations", GUILayout.ExpandWidth (true))) {
									AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
										"Packages/Integrations/DK UMA Integrations.unitypackage", false);
									
								}
							}
							else {
								EditorGUILayout.HelpBox("Integrations are installed into your project.", UnityEditor.MessageType.None);
								EditorGUILayout.HelpBox("If you just updated DK UMA, you can refresh or update the Integrations of your project" +
									" by clicking on the next button.", UnityEditor.MessageType.Info);
								using (new Horizontal()) {
									if ( GUILayout.Button ( "Reinstall the Integrations", GUILayout.ExpandWidth (true))) {
										AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
											"Packages/Integrations/DK UMA Integrations.unitypackage", false);
										
									}
									GUI.color = Color.yellow ;
									if ( GUILayout.Button ( "Delete", GUILayout.Width (50))) {
										FileUtil.DeleteFileOrDirectory (Application.dataPath+"/DK Editors/DK_UMA_Editor/PlugIns/Integrations");		
										FileUtil.DeleteFileOrDirectory (Application.dataPath+"/DK Editors/DK_UMA_Editor/PlugIns/Controllers");		

										AssetDatabase.Refresh ();
									}
								}
							}
							#endregion Integrations Install
						}
					}
					else {
						EditorGUILayout.HelpBox("No PlugIns have been detected. " +
							"You can get the PlugIns by purchasing the DK UMA Premium package at the assetstore or at the DK Store.", UnityEditor.MessageType.Info);
						using (new Horizontal()) {
						/*	if ( GUILayout.Button ( "DK Store (-10%)", GUILayout.ExpandWidth (true))) {
								Application.OpenURL ("https://www.sellfy.com/DKTeam");
							}*/

							if ( GUILayout.Button ( "Unity Store", GUILayout.ExpandWidth (true))) {
								Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/45131");
							}
						}
					}
					#endregion PlugIns Install

					#region How to start
					#if DK_UMA_Editor
					GUI.color = Color.white ;
					GUILayout.Label ( "First steps", "toolbarbutton", GUILayout.ExpandWidth (true));
					EditorGUILayout.HelpBox("Open the 'DK UMA Editor' to install the engine into any scene, to modify the settings or to create some avatars. " +
						"Use it also to prepare the selected element (use the 'Elements Manager' to select the Elements).", UnityEditor.MessageType.None);
					if ( GUILayout.Button ( "Open the 'DK UMA Editor'", GUILayout.ExpandWidth (true))) {
						GetWindow(typeof(DK_UMA_Editor), false, "DK UMA Editor");
					}
					EditorGUILayout.HelpBox("Open the 'Elements Manager' to install the UMA Engine to the scene, to select and have an overview of any DK UMA or UMA element or to prepare the various libraries.", UnityEditor.MessageType.None);
					
					if ( GUILayout.Button ( "Open the 'Elements Manager'", GUILayout.ExpandWidth (true))) {
						GetWindow(typeof(AutoDetect_Editor), false, "Elements Manager");
					}


					#endif

					#region Links
					GUILayout.Label ( "Information, tutorials and store", "toolbarbutton", GUILayout.ExpandWidth (true));
					using (new Horizontal()) {
						if ( GUILayout.Button ( "DK UMA Forum web page", GUILayout.ExpandWidth (true))) {
							Application.OpenURL ("http://unity3d-dk-tools.boards.net/thread/85/documentation-table-content");
						}
					/*	if ( GUILayout.Button ( "DK Store (-20%)", GUILayout.ExpandWidth (true))) {
							Application.OpenURL ("https://www.sellfy.com/DKTeam");
						}*/
					}
					#endregion Links

					#endregion How to start
				}
				else {
					GUI.color = Color.yellow ;
					EditorGUILayout.HelpBox("No DK UMA version detected.", UnityEditor.MessageType.Warning);
					GUI.color = Color.white ;
					// UMA 2.0.5
					if ( AssetDatabase.IsValidFolder ("Assets/UMA/Core/Extensions/DynamicCharacterSystem") == false ){
						if ( GUILayout.Button ( "Install DK UMA for UMA 2.0.5", GUILayout.ExpandWidth (true))) {
							InitDefineDKUMA ();
							AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
								"Packages/Install DK UMA for UMA 2.0.5.unitypackage", false);
						}
					}
					// UMA 2.6
					else {
						if ( GUILayout.Button ( "Install DK UMA for UMA 2.6", GUILayout.ExpandWidth (true))) {
							InitDefineDKUMA ();
							AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
								"Packages/Install DK UMA for UMA 2.6.unitypackage", false);	
						}
					}
				}

				#endregion DK UMA
			}
			#endregion UMA Installed

			#region UMA Not Installed
			else {
				GUI.color = Color.yellow ;
				EditorGUILayout.HelpBox("No UMA version detected. DK UMA Requires the installation of UMA 2.0.5 or UMA 2.6.", UnityEditor.MessageType.Warning);
				GUI.color = Color.white ;
				#if UNITY_5_5_OR_NEWER
				// UMA 2.0.5
				EditorGUILayout.HelpBox("DK UMA provides a modified version of the UMA 2.0.5 engine to enable you to use it with Unity 5.5. " +
				"It is useful for the users working on a project with UMA 2.0.5 and wanting to update Unity to the version 5.5.", UnityEditor.MessageType.Info);
				if ( GUILayout.Button ( "Install UMA 2.0.5 for Unity 5.5", GUILayout.ExpandWidth (true))) {
				AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
				"Packages/Install UMA2.0.5 for U5.5.unitypackage", false);
			
				}
				// UMA 2.6
				if ( GUILayout.Button ( "UMA 2.6 Download page at the Assetstore", GUILayout.ExpandWidth (true))) {
				Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/35611");
				}
				#else
				EditorGUILayout.HelpBox("Select the UMA version you want to use for your project.", UnityEditor.MessageType.None);
				if ( GUILayout.Button ( "Install UMA 2.0.5", GUILayout.ExpandWidth (true))) {
					AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
						"Packages/Install UMA2.0.5.unitypackage", false);					
				}
				if ( GUILayout.Button ( "UMA 2.6 Download page at the Assetstore", GUILayout.ExpandWidth (true))) {
					Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/35611");
				}
				#endif
			}
			#endregion UMA Not Installed
		}
	//	RemoveDefineDKUMA ();
	}

	[MenuItem("UMA/DK Editor/Installer/Debug Defines (In last ressort)/Define DK UMA")]
	public static void InitDefineDKUMA ()
	{ 		
		// Verify if the asset is present in the project
		string SymbolsStandalone = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
		string SymbolsAndroid = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
		string SymbolsIOS = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);


		#region DK UMA RPG Editor
		if ( SymbolsStandalone.Contains ( "DK_UMA_Editor" ) == false  ){
			// Standalone
			SymbolsStandalone = SymbolsStandalone+";DK_UMA_Editor";				
			// Android
			if ( SymbolsAndroid.Contains ( "DK_UMA_Editor" ) == false )
				SymbolsAndroid = SymbolsAndroid+";DK_UMA_Editor";	
			// IOS
			if ( SymbolsIOS.Contains ( "DK_UMA_Editor" ) == false )
				SymbolsIOS = SymbolsIOS+";DK_UMA_Editor";	
			
			// save
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Standalone, SymbolsStandalone );
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Android, SymbolsAndroid );
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.iOS, SymbolsIOS );


		//	Debug.Log ("BuildTargetGroup.Standalone "+PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone));
		}
		#endregion DK UMA RPG Editor

	}

	[MenuItem("UMA/DK Editor/Installer/Debug Defines (In last ressort)/Define DK UMA RPG")]
	public static void InitDefineDKUMARPG ()
	{ 		
		// Verify if the asset is present in the project
		string SymbolsStandalone = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
		string SymbolsAndroid = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
		string SymbolsIOS = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);

		#region DK UMA RPG Editor
		if ( SymbolsStandalone.Contains ( "DK_UMA_RPG_Editor" ) == false
		//	&& AssetDatabase.IsValidFolder ("Assets/DK Editors/DK_UMA_Editor/PlugIns") == true 
		){
			// Standalone
			SymbolsStandalone = SymbolsStandalone+";DK_UMA_RPG_Editor";				
			// Android
			if ( SymbolsAndroid.Contains ( "DK_UMA_RPG_Editor" ) == false )
				SymbolsAndroid = SymbolsAndroid+";DK_UMA_RPG_Editor";	
			// IOS
			if ( SymbolsIOS.Contains ( "DK_UMA_RPG_Editor" ) == false )
				SymbolsIOS = SymbolsIOS+";DK_UMA_RPG_Editor";
			
			// save
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Standalone, SymbolsStandalone );
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Android, SymbolsAndroid );
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.iOS, SymbolsIOS );


			Debug.Log ("BuildTargetGroup.Standalone "+PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone));
		}
		#endregion DK UMA RPG Editor

	}

	[MenuItem("UMA/DK Editor/Installer/Debug Defines (In last ressort)/Undefine DK UMA")]
	public static void RemoveDefineDKUMA ()
	{ 		
		// Verify if the asset is present in the project
		string SymbolsStandalone = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
		string SymbolsAndroid = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
		string SymbolsIOS = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);


		if ( SymbolsStandalone.Contains ( "DK_UMA_Editor" ) 
		//	&& AssetDatabase.IsValidFolder ("Assets/DK Editors/DK_UMA_Editor/Engine") == false
		) {
			//	Debug.Log ("DK UMA RPG Editor not found");

			// Standalone
			if ( SymbolsStandalone.Contains ( ";DK_UMA_Editor" ) )
				SymbolsStandalone = SymbolsStandalone.Replace ( ";DK_UMA_Editor", "" );	
			else if ( SymbolsStandalone.Contains ( "DK_UMA_Editor" ) )
				SymbolsStandalone = SymbolsStandalone.Replace ( "DK_UMA_Editor", "" );
			// Android
			if ( SymbolsAndroid.Contains ( ";DK_UMA_Editor" ) )
				SymbolsAndroid = SymbolsAndroid.Replace ( ";DK_UMA_Editor", "" );	
			else if ( SymbolsAndroid.Contains ( "DK_UMA_Editor" ) )
				SymbolsAndroid = SymbolsAndroid.Replace ( "DK_UMA_Editor", "" );
			// IOS
			if ( SymbolsIOS.Contains ( ";DK_UMA_Editor" ) )
				SymbolsIOS = SymbolsIOS.Replace ( ";DK_UMA_Editor", "" );	
			else if ( SymbolsIOS.Contains ( "DK_UMA_Editor" ) )
				SymbolsIOS = SymbolsIOS.Replace ( "DK_UMA_Editor", "" );
			
			// last remove
			if ( SymbolsStandalone == ";" ) SymbolsStandalone = SymbolsStandalone.Replace ( ";", "" );
			if ( SymbolsAndroid == ";" ) SymbolsAndroid = SymbolsAndroid.Replace ( ";", "" );
			if ( SymbolsIOS == ";" ) SymbolsIOS = SymbolsIOS.Replace ( ";", "" );

			// save
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Standalone, SymbolsStandalone );
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Android, SymbolsAndroid );
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.iOS, SymbolsIOS );

		}
	
	//	Debug.Log ("BuildTargetGroup.Standalone "+PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone));
	}

	[MenuItem("UMA/DK Editor/Installer/Debug Defines (In last ressort)/Undefine DK UMA RPG")]
	public static void RemoveDefineDKUMAPluIns ()
	{ 		
		// Verify if the asset is present in the project
		string SymbolsStandalone = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
		string SymbolsAndroid = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
		string SymbolsIOS = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);


		if ( (SymbolsStandalone.Contains ( "DK_UMA_RPG_Editor" ) )) {
			//	Debug.Log ("DK UMA RPG Editor not found");

			// Standalone
			if ( SymbolsStandalone.Contains ( ";DK_UMA_RPG_Editor" ) )
				SymbolsStandalone = SymbolsStandalone.Replace ( ";DK_UMA_RPG_Editor", "" );	
			else if ( SymbolsStandalone.Contains ( "DK_UMA_RPG_Editor" ) )
				SymbolsStandalone = SymbolsStandalone.Replace ( "DK_UMA_RPG_Editor", "" );
			// Android
			if ( SymbolsAndroid.Contains ( ";DK_UMA_RPG_Editor" ) )
				SymbolsAndroid = SymbolsAndroid.Replace ( ";DK_UMA_RPG_Editor", "" );	
			else if ( SymbolsAndroid.Contains ( "DK_UMA_RPG_Editor" ) )
				SymbolsAndroid = SymbolsAndroid.Replace ( "DK_UMA_RPG_Editor", "" );
			// IOS
			if ( SymbolsIOS.Contains ( ";DK_UMA_RPG_Editor" ) )
				SymbolsIOS = SymbolsIOS.Replace ( ";DK_UMA_RPG_Editor", "" );	
			else if ( SymbolsIOS.Contains ( "DK_UMA_RPG_Editor" ) )
				SymbolsIOS = SymbolsIOS.Replace ( "DK_UMA_RPG_Editor", "" );
			
			// last remove
			if ( SymbolsStandalone == ";" ) SymbolsStandalone = SymbolsStandalone.Replace ( ";", "" );
			if ( SymbolsAndroid == ";" ) SymbolsAndroid = SymbolsAndroid.Replace ( ";", "" );
			if ( SymbolsIOS == ";" ) SymbolsIOS = SymbolsIOS.Replace ( ";", "" );

			// save
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Standalone, SymbolsStandalone );
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Android, SymbolsAndroid );
			PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.iOS, SymbolsIOS );
		}
	//	Debug.Log ("BuildTargetGroup.Standalone "+PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone));
	}

	void InstallDKUMAForUMA205 (){
		AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
			"Packages/Install DK UMA for UMA 2.0.5.unitypackage", false);
	}
	void InstallDKUMAForUMA205Content (){
		AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
			"Packages/Contents/DK UMA for UMA 2.0.5 Default Content.unitypackage", true);
	}

	void InstallDKUMAForUMA25 (){
		AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
			"Packages/Install DK UMA for UMA 2.6.unitypackage", false);
	}
	void InstallDKUMAForUMA25Content (){
		AssetDatabase.ImportPackage (Application.dataPath+"/DK Editors/DK UMA Installers/" +
			"Packages/Contents/DK UMA for UMA 2.6 Default Content.unitypackage", true);
	}
}
