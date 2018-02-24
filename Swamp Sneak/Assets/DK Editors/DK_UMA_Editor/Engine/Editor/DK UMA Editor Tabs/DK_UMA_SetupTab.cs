using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;


[System.Serializable]
public class DK_UMA_SetupTab : EditorWindow {
	public static Color Green = new Color (0.8f, 1f, 0.8f, 1);
	public static Color Red = new Color (0.9f, 0.5f, 0.5f);

	public static Vector2 scroll;

	public static bool ShowNewItem = false;
	public static string newItemName = "Write a name";

	public static bool ShowMiscDatas;
	public static bool ShowColorsDatas;
	public static bool ShowLODDatas;
	public static DK_UMA_GameSettings _DK_UMA_GameSettings;

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

		if ( DK_UMA_Editor.Helper ) {
			GUI.color = Color.yellow;
			GUILayout.TextField("Take care modifying the Engine parameters..." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		}
	
		#region Generation Type

		GUILayout.Space(5);
		using (new ScrollView(ref scroll)) 
		{	
			GUI.color = Color.white;

			GUILayout.Label("DK UMA Game Settings", "toolbarbutton", GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("A DK UMA Game Settings Database is required for the engine to work properly. " +
				"This Database has to be assigned in every scene using DK UMA.", UnityEditor.MessageType.Info);
		

			if ( !ShowNewItem ) {
				if ( DK_UMA_Editor._DKUMA_Variables == null ){
					GUI.color = Red;
					EditorGUILayout.HelpBox("The component 'DKUMA_Variables' is not present in your scene. Add it to the DK_UMA gameobject.", UnityEditor.MessageType.Warning);
				}
				else if ( DK_UMA_Editor._DKUMA_Variables._DK_UMA_GameSettings == null  ){
					GUI.color = Red;
					EditorGUILayout.HelpBox("No DK UMA Game Settings have been assigned to the scene. Select one using the filed bellow.", UnityEditor.MessageType.Warning);
				}

				GUI.color = Color.white;
				DK_UMA_Editor._DKUMA_Variables._DK_UMA_GameSettings = 
					(DK_UMA_GameSettings)EditorGUILayout.ObjectField("DK UMA Game Settings :",DK_UMA_Editor._DKUMA_Variables._DK_UMA_GameSettings,typeof(DK_UMA_GameSettings),false);
				
				EditorGUILayout.HelpBox("You can create a new DK UMA Game Settings to keep your own settings to be replaced by the default one at every update of the DK UMA engine.", UnityEditor.MessageType.None);
				if(GUILayout.Button("Create a new DK UMA Game Settings")){
					ShowNewItem = true;
				}
				if ( DK_UMA_Editor._DKUMA_Variables != null
					&& DK_UMA_Editor._DKUMA_Variables._DK_UMA_GameSettings != null ){
					using (new Horizontal()) {
						bool tmpDebug = DK_UMA_Editor._DKUMA_Variables._DK_UMA_GameSettings.Debugger;
						DK_UMA_Editor._DKUMA_Variables._DK_UMA_GameSettings.Debugger = EditorGUILayout.Toggle ( "Use DK Debugger :",DK_UMA_Editor._DKUMA_Variables._DK_UMA_GameSettings.Debugger );
						if ( DK_UMA_Editor._DKUMA_Variables._DK_UMA_GameSettings.Debugger != tmpDebug ) SaveAsset ();
					}
					using (new Horizontal()) {	
						if(GUILayout.Button("Save the Game Settings")){
							SaveAsset ();
						}
						if(GUILayout.Button("Save the Current scene")){
							SaveScene ();
						}
					}
				}
			}
			if ( DK_UMA_Editor._DKUMA_Variables != null ){
				if ( ShowNewItem ){				
					GUI.color = Color.white;
					GUILayout.Label("Create a new DK UMA Game Settings", "toolbarbutton", GUILayout.ExpandWidth (true));
					EditorGUILayout.HelpBox("Write a name for the New Database then click on the Create button.", UnityEditor.MessageType.None);

					using (new Horizontal()) {	
						GUI.color = Color.white;
						GUILayout.Label ( "Name :", GUILayout.Width (110));
						if ( newItemName == "Write a name" ) GUI.color = Red;
						else GUI.color = Green;
						newItemName = GUILayout.TextField(newItemName, 50, GUILayout.ExpandWidth (true));
					}	
					using (new Horizontal()) {
						if ( newItemName != "Write a name" ) {
							GUI.color = Green;
							if (GUILayout.Button ( "Create the New Item", GUILayout.ExpandWidth (true))) {
								DK_UMA_SetupTab.CreateItem (newItemName);
								ShowNewItem = false;
							}
						}
					}
					GUI.color = Color.white;
					if(GUILayout.Button("Cancel")){
						ShowNewItem = false;
					}
				}

				_DK_UMA_GameSettings = DK_UMA_Editor._DKUMA_Variables._DK_UMA_GameSettings;

				if ( _DK_UMA_GameSettings != null ){
					GUILayout.Label("UMA Version", "toolbarbutton", GUILayout.ExpandWidth (true));
					EditorGUILayout.HelpBox("Select the UMA engine version you want to use.", UnityEditor.MessageType.Info);
					DK_UMA_GameSettings.UMAVersionEnum oldVers = _DK_UMA_GameSettings.UMAVersion;
					_DK_UMA_GameSettings.UMAVersion = (DK_UMA_GameSettings.UMAVersionEnum ) EditorGUILayout.EnumPopup ("UMA Version", _DK_UMA_GameSettings.UMAVersion );
					if ( oldVers != _DK_UMA_GameSettings.UMAVersion ) SaveAsset ();
				}

				if ( _DK_UMA_GameSettings != null  ){
					if ( ShowNewItem == false ){
						EditorGUILayout.HelpBox("The next values are the DNA limits for the randomized DNA values of any generated avatar.", UnityEditor.MessageType.None);
						using (new Horizontal()) {
							_DK_UMA_GameSettings.MinDnaValueLimit = EditorGUILayout.FloatField("Min Dna Value :", _DK_UMA_GameSettings.MinDnaValueLimit);
							if ( _DK_UMA_GameSettings.MinDnaValueLimit < -0.5f ) _DK_UMA_GameSettings.MinDnaValueLimit = -0.5f;
							_DK_UMA_GameSettings.MaxDnaValueLimit = EditorGUILayout.FloatField("Max Dna Value :", _DK_UMA_GameSettings.MaxDnaValueLimit);
							if ( _DK_UMA_GameSettings.MaxDnaValueLimit > 2 ) _DK_UMA_GameSettings.MaxDnaValueLimit = 2;
						}
						using (new Horizontal()) {
							_DK_UMA_GameSettings.SaveToFile = EditorGUILayout.Toggle ( "Use DK Save System ",_DK_UMA_GameSettings.SaveToFile );
							_DK_UMA_GameSettings.EquipmentSets.UseSets = EditorGUILayout.Toggle ( "Use Equipment Sets ",_DK_UMA_GameSettings.EquipmentSets.UseSets );

						}
						#endregion Generation Type

						#region Engine
						GUILayout.Space(5);
						using (new Horizontal()) {
							GUI.color = Color.white;
							if (GUILayout.Button ( "Color Variation", "toolbarbutton", GUILayout.ExpandWidth (true))) {
								ShowColorsDatas = !ShowColorsDatas;
							}
							if ( ShowColorsDatas ) GUI.color = Green;
							else GUI.color = Color.white;
							if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
								ShowColorsDatas = !ShowColorsDatas;
							}
						}
						if ( ShowColorsDatas ){
							GUI.color = Color.white;
							EditorGUILayout.HelpBox("Color Variation is used during the random generation of an avatar.", UnityEditor.MessageType.None);

							using (new Horizontal()) {
								// Flesh Variation
								GUILayout.Label("Flesh", GUILayout.Width (50));
								_DK_UMA_GameSettings.Colors.FleshVariation = GUILayout.HorizontalSlider(_DK_UMA_GameSettings.Colors.FleshVariation ,0,0.5f);
								_DK_UMA_GameSettings.Colors.FleshVariation = EditorGUILayout.FloatField("", _DK_UMA_GameSettings.Colors.FleshVariation, GUILayout.Width (40));
								if ( _DK_UMA_GameSettings.Colors.FleshVariation < 0 ) _DK_UMA_GameSettings.Colors.FleshVariation = 0;
								if ( _DK_UMA_GameSettings.Colors.FleshVariation > 0.5f ) _DK_UMA_GameSettings.Colors.FleshVariation = 0.5f;
								EditorVariables.DK_UMACrowd.Colors.AdjRanMaxi = _DK_UMA_GameSettings.Colors.FleshVariation;
							}
							using (new Horizontal()) {
								// Hair Variation
								GUI.color = Color.white;
								GUILayout.Label("Hair", GUILayout.Width (50));
								_DK_UMA_GameSettings.Colors.HairVariation = GUILayout.HorizontalSlider(_DK_UMA_GameSettings.Colors.HairVariation ,0,0.5f);
								_DK_UMA_GameSettings.Colors.HairVariation = EditorGUILayout.FloatField("", _DK_UMA_GameSettings.Colors.HairVariation, GUILayout.Width (40));
								if ( _DK_UMA_GameSettings.Colors.HairVariation < 0 ) _DK_UMA_GameSettings.Colors.HairVariation = 0;
								if ( _DK_UMA_GameSettings.Colors.HairVariation > 0.5f ) _DK_UMA_GameSettings.Colors.HairVariation = 0.5f;
								EditorVariables.DK_UMACrowd.Colors.HairAdjRanMaxi = _DK_UMA_GameSettings.Colors.HairVariation;
							}
							using (new Horizontal()) {	
								// wear Variation
								GUI.color = Color.white;
								GUILayout.Label("Wear", GUILayout.Width (50));
								_DK_UMA_GameSettings.Colors.WearVariation = GUILayout.HorizontalSlider(_DK_UMA_GameSettings.Colors.WearVariation ,0,0.5f);
								_DK_UMA_GameSettings.Colors.WearVariation = EditorGUILayout.FloatField("", _DK_UMA_GameSettings.Colors.WearVariation, GUILayout.Width (40));
								if ( _DK_UMA_GameSettings.Colors.WearVariation < 0 ) _DK_UMA_GameSettings.Colors.WearVariation = 0;
								if ( _DK_UMA_GameSettings.Colors.WearVariation > 0.5f ) _DK_UMA_GameSettings.Colors.WearVariation = 0.5f;
								EditorVariables.DK_UMACrowd.Colors.WearAdjRanMaxi = _DK_UMA_GameSettings.Colors.WearVariation;
								
							}
						}
						GUILayout.Space(5);
						using (new Horizontal()) {
							GUI.color = Color.white;
							if (GUILayout.Button ( "LOD Settings", "toolbarbutton", GUILayout.ExpandWidth (true))) {
								ShowLODDatas = !ShowLODDatas;
							}
							if ( ShowLODDatas ) GUI.color = Green;
							else GUI.color = Color.white;
							if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
								ShowLODDatas = !ShowLODDatas;
							}
						}
						if ( ShowLODDatas ){
							GUI.color = Color.white;
							EditorGUILayout.HelpBox("The LOD feature is still in test an may generate some little visual glitches when an avatar changes of LOD. It is due to the UMA engine.", UnityEditor.MessageType.Warning);

							using (new Horizontal()) {
								_DK_UMA_GameSettings._LOD.UseLOD = EditorGUILayout.Toggle ( "Use LOD",_DK_UMA_GameSettings._LOD.UseLOD );
								_DK_UMA_GameSettings._LOD.UseMeshLOD = EditorGUILayout.Toggle ( "Use Mesh LOD (WIP)",_DK_UMA_GameSettings._LOD.UseMeshLOD );
							}
						//	using (new Horizontal()) {
						//		_DK_UMA_GameSettings._LOD.Frequency = EditorGUILayout.FloatField("LOD Frequency", _DK_UMA_GameSettings._LOD.Frequency, GUILayout.ExpandWidth (true));
						//	}
							using (new Horizontal()) {
								_DK_UMA_GameSettings._LOD.LOD0Resolution = EditorGUILayout.IntField("LOD 0 Resolution", _DK_UMA_GameSettings._LOD.LOD0Resolution, GUILayout.ExpandWidth (true));
								_DK_UMA_GameSettings._LOD.Frequency = EditorGUILayout.FloatField("LOD Frequency", _DK_UMA_GameSettings._LOD.Frequency, GUILayout.ExpandWidth (true));
							}
							using (new Horizontal()) {
								_DK_UMA_GameSettings._LOD.LOD1Resolution = EditorGUILayout.IntField("LOD 1 Resolution", _DK_UMA_GameSettings._LOD.LOD1Resolution,GUILayout.ExpandWidth (true));
								_DK_UMA_GameSettings._LOD.LOD1Distance = EditorGUILayout.IntField("LOD 1 Distance", _DK_UMA_GameSettings._LOD.LOD1Distance, GUILayout.ExpandWidth (true));
							}
							using (new Horizontal()) {
								_DK_UMA_GameSettings._LOD.LOD2Resolution = EditorGUILayout.IntField("LOD 2 Resolution", _DK_UMA_GameSettings._LOD.LOD2Resolution, GUILayout.ExpandWidth (true));
								_DK_UMA_GameSettings._LOD.LOD2Distance = EditorGUILayout.IntField("LOD 2 Distance", _DK_UMA_GameSettings._LOD.LOD2Distance, GUILayout.ExpandWidth (true));
							}
							using (new Horizontal()) {
								_DK_UMA_GameSettings._LOD.LOD3Resolution = EditorGUILayout.IntField("LOD 3 Resolution", _DK_UMA_GameSettings._LOD.LOD3Resolution, GUILayout.ExpandWidth (true));
								_DK_UMA_GameSettings._LOD.LOD3Distance = EditorGUILayout.IntField("LOD 3 Distance", _DK_UMA_GameSettings._LOD.LOD3Distance, GUILayout.ExpandWidth (true));
							}
							using (new Horizontal()) {
								_DK_UMA_GameSettings._LOD.LOD4Resolution = EditorGUILayout.IntField("LOD 4 Resolution", _DK_UMA_GameSettings._LOD.LOD4Resolution, GUILayout.ExpandWidth (true));
								_DK_UMA_GameSettings._LOD.LOD4Distance = EditorGUILayout.IntField("LOD 4 Distance", _DK_UMA_GameSettings._LOD.LOD4Distance, GUILayout.ExpandWidth (true));
							}

						}
						#endregion Engine

						GUILayout.Space(5);
						using (new Horizontal()) {
							GUI.color = Color.white;
							if (GUILayout.Button ( "Miscelaneous Default Datas", "toolbarbutton", GUILayout.ExpandWidth (true))) {
								ShowMiscDatas = !ShowMiscDatas;
							}
							if ( ShowMiscDatas ) GUI.color = Green;
							else GUI.color = Color.white;
							if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
								ShowMiscDatas = !ShowMiscDatas;
							}
						}
						if ( ShowMiscDatas ){
							GUI.color = Color.white;
							EditorGUILayout.HelpBox("During the Fix process of the 'Elements Manager', the Default UMA Material will be assigned to a UMA Slot or Overlay if its UMA Material is missing.", UnityEditor.MessageType.None);
							_DK_UMA_GameSettings.MiscDefaultDatas.UMAMaterial = 
								(UMA.UMAMaterial)EditorGUILayout.ObjectField("Default UMA Material :",_DK_UMA_GameSettings.MiscDefaultDatas.UMAMaterial,typeof(UMA.UMAMaterial),false);

							EditorGUILayout.HelpBox("The next Datas are used by the Race Editor/Creator. They are the default values containers of a new created DK UMA or UMA race.", UnityEditor.MessageType.None);
							_DK_UMA_GameSettings.DefaultRaceData.FemaleRace = 
								(DKRaceData)EditorGUILayout.ObjectField("Female DK UMA Race :",_DK_UMA_GameSettings.DefaultRaceData.FemaleRace,typeof(DKRaceData),false);
							_DK_UMA_GameSettings.DefaultRaceData.MaleRace = 
								(DKRaceData)EditorGUILayout.ObjectField("Male DK UMA Race :",_DK_UMA_GameSettings.DefaultRaceData.MaleRace,typeof(DKRaceData),false);
							_DK_UMA_GameSettings.DefaultRaceData.UMAFemaleRace = 
								(UMA.RaceData)EditorGUILayout.ObjectField("Female UMA Race :",_DK_UMA_GameSettings.DefaultRaceData.UMAFemaleRace,typeof(UMA.RaceData),false);
							_DK_UMA_GameSettings.DefaultRaceData.UMAMaleRace = 
								(UMA.RaceData)EditorGUILayout.ObjectField("Male UMA Race :",_DK_UMA_GameSettings.DefaultRaceData.UMAMaleRace,typeof(UMA.RaceData),false);
						}
					}
				}
			}
		}
	}

	public static void CreateItem ( string name ) {
		DK_UMA_GameSettings newItem = new DK_UMA_GameSettings ();
		newItem = ScriptableObject.CreateInstance("DK_UMA_GameSettings") as DK_UMA_GameSettings;
		Selection.activeObject = newItem;
		newItem.name = name;
		AssetDatabase.CreateAsset(newItem as UnityEngine.Object, "Assets/DK Editors/DK_UMA_Content/Databases/" + newItem.name + ".asset");
		AssetDatabase.Refresh();
	}

	public static void SaveAsset (){
		EditorUtility.SetDirty (_DK_UMA_GameSettings);
		AssetDatabase.SaveAssets ();
	}

	public static void SaveScene (){
		EditorUtility.SetDirty (DK_UMA_Editor._DKUMA_Variables);
	
		EditorSceneManager.MarkSceneDirty (DK_UMA_Editor._DKUMA_Variables.gameObject.scene);
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
	
	}
}
