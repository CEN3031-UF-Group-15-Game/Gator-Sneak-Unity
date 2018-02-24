using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class StartupDKUMAEditor {
	static StartupDKUMAEditor()
	{ 
		/*
		// Verify if the asset is present in the project
		string SymbolsStandalone = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
		string SymbolsAndroid = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);

		#region DK UMA RPG Editor
		if ( AssetDatabase.FindAssets ("DK_RPG_UMA_Avatar_Win").Length == 0 ) {
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
		}
		#endregion DK UMA RPG Editor

		// last remove
		if ( SymbolsStandalone == ";" ) SymbolsStandalone = SymbolsStandalone.Replace ( ";", "" );
		if ( SymbolsAndroid == ";" ) SymbolsAndroid = SymbolsAndroid.Replace ( ";", "" );

		// save
		PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Standalone, SymbolsStandalone );
		PlayerSettings.SetScriptingDefineSymbolsForGroup ( BuildTargetGroup.Android, SymbolsAndroid );

	//	Debug.Log ("BuildTargetGroup.Standalone "+PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone));
	*/
	}
}