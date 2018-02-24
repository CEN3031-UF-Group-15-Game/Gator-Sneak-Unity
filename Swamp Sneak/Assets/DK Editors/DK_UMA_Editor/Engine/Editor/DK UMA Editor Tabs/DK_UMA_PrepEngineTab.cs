using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using UMA;

public class DK_UMA_PrepEngineTab : EditorWindow {
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

		GUI.color = Color.white;
		EditorGUILayout.HelpBox("You can setup the DK UMA engine. Here is the Color Presets editor.", UnityEditor.MessageType.None);

		using (new Horizontal()) {
			GUI.color = Color.white;
			if (GUILayout.Button ("DK Engine", GUILayout.ExpandWidth (true))) {
				DK_UMA_Editor.ShowDKLibraries = true;
			}											
		}
		EditorGUILayout.HelpBox("select a DK Element using the 'Elements Manager'. then use this Tab to set it up.", UnityEditor.MessageType.None);


		GUI.color = Color.white;
		GUILayout.Label ("Content Wizard", "toolbarbutton", GUILayout.ExpandWidth (true));

		if (Selection.activeObject && Selection.activeObject.GetType ().ToString ().Contains ("UMA.")) {
			GUI.color = Color.yellow;
			GUILayout.TextField ("Your selection is a UMA Element : "+Selection.activeObject.name+"", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			GUILayout.TextField ("Your selection is of type : "+Selection.activeObject.GetType ().ToString()+"", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUI.color = Color.white;
			EditorGUILayout.HelpBox("A UMA element have to be imported as a DK Blue Print for DK UMA to be able to use it with all the DK UMA advanced features.", UnityEditor.MessageType.None);

			EditorGUILayout.HelpBox("Open the Elements Manager and verify if the UMA element have no DK Blue Print. The name of the element is automatically " +
				"paste in the Search field of the Elements Manager.", UnityEditor.MessageType.None);

			if (GUILayout.Button ("Open the Elements Manager and search for the DK Blue Print", GUILayout.ExpandWidth (true))) {
				GetWindow(typeof(AutoDetect_Editor), false, "Manager");
				AutoDetect_Editor window = EditorWindow.GetWindow<AutoDetect_Editor> (false, "Elements Manager");
				window.SearchString = Selection.activeObject.name;
				if ( Selection.activeObject && Selection.activeObject.GetType () == typeof(UMA.OverlayDataAsset) ) {
					window.ShowDKoverlay = true;
					window.ShowDKslot = false;
				}
				if ( Selection.activeObject && Selection.activeObject.GetType () == typeof(UMA.SlotDataAsset) ) {
					window.ShowDKslot = true;
					window.ShowDKoverlay = false;
				}
				window.Showslot = false;
				window.Showoverlay = false;
				window.ShowRaces = false;
				window.ShowDKRaces = false;
			}
			EditorGUILayout.HelpBox("If no DK Blue Print is found in the Elements Manager, you can create the new DK Blue Print by clicking on the following button.", UnityEditor.MessageType.None);

			if (GUILayout.Button ("Create the DK Blue Print of this UMA element", GUILayout.ExpandWidth (true))) {
				if ( Selection.activeObject && Selection.activeObject.GetType () == typeof(UMA.SlotDataAsset) )
					DK_UMA_Editor.AddToDK (typeof(UMA.SlotDataAsset), Selection.activeObject);
				else if ( Selection.activeObject && Selection.activeObject.GetType () == typeof(UMA.OverlayDataAsset) )
					DK_UMA_Editor.AddToDK (typeof(UMA.OverlayDataAsset), Selection.activeObject);
			}

			EditorGUILayout.HelpBox("Then refresh the Elements Manager by clicking on its '1- Detect Elements' button.", UnityEditor.MessageType.None);



		}

		// Plug-In
		if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "PlugInData") {
			GUILayout.TextField ("Your selection is a Plug-In File.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		}
		// Export project
		if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "ExportData") {
			string _Path = AssetDatabase.GetAssetPath (Selection.activeObject);
			if (_Path.Contains ("Assets/DK Editors/DK_UMA_Editor/Exporter/Exporting/") == true) { 
				GUI.color = Color.yellow;
				GUILayout.TextField ("Your selection is an Export project, select the Plug-In Tab of the Editor Window, " +
					"then click on the button to open the DK UMA Exporter. ", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.TextField ("If you can't find the Importer in your Plug-Ins List, you will have to download and install. " +
					"then click on the button to open the DK UMA Importer. ", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				using (new Horizontal()) {
					if (GUILayout.Button ("Open the Plug-Ins Tab", GUILayout.ExpandWidth (true))) {
						DK_UMA_Editor.ShowPrepare = false;
						DK_UMA_Editor.showPlugIn = true;
					}
				}
			}
			if (_Path.Contains ("Assets/DK Editors/DK_UMA_Editor/Exporter/Incoming/") == true) {
				GUI.color = Color.yellow;
				GUILayout.TextField ("Your selection is an Incoming project, select the Plug-In Tab of the Editor Window, " +
					"then click on the button to open the DK UMA Importer. ", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.TextField ("If you can't find the Importer in your Plug-Ins List, you will have to download and install. " +
					"then click on the button to open the DK UMA Importer. ", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

				GUI.color = Color.white;
				using (new Horizontal()) {
					if (GUILayout.Button ("Open the Plug-Ins Tab", GUILayout.ExpandWidth (true))) {
						DK_UMA_Editor.ShowPrepare = false;
						DK_UMA_Editor.showPlugIn = true;
					}
				}
			}
		}
		// create a RPG element
		if ((Selection.activeGameObject 
			&& Selection.activeGameObject.GetComponent<Transform> () == true
			&& Selection.activeGameObject.GetComponent<DK_Model> () == null
			&& Selection.activeGameObject.GetComponent<DKUMAData> () == null
			&& Selection.activeGameObject.GetComponentInChildren<DKUMAData> () == null
			&& Selection.activeGameObject.GetComponentInParent<DKUMAData> () == null
			&& Selection.activeGameObject.GetComponent<UMA.UMAData> () == null
			&& Selection.activeGameObject.GetComponentInParent<UMA.UMAData> () == null
			&& Selection.activeGameObject.GetComponentInChildren<UMA.UMAData> () == null
			&& Selection.activeGameObject.GetComponentInChildren<SkinnedMeshRenderer> () != null)) 
		{

			if ( Selection.activeGameObject.GetComponent<DK_RPG_Element> () == null 
				&& Selection.activeGameObject.GetComponentInChildren<DK_RPG_Element> () == null
				&& Selection.activeGameObject.GetComponentInParent<DK_RPG_Element> () == null ){
				GUILayout.TextField ("You can create a RPG element from your current selection. add the necessary scripts by clicking on the button.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				if ( GUILayout.Button ("Add the RPG Element Script to the selection", GUILayout.ExpandWidth (true))) {
					DK_RPG_Element.CreateOnGameObject(Selection.activeGameObject);
				}
			}
			else {
				// Instructions
				GUILayout.TextField ("A DK RPG UMA Element is a simple definition of an ensemble of values. Use the Inspector to modify the values.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.TextField ("The first on is the Slot you want to add to the avatar" +
					", the second  is the Overlay for the slot and the thrid one is the ColorPreset.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.TextField ("Select the Slot for corresponding gender, or select for both of then for the Element to be available for both.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.TextField ("The selected Slot must be ready to be used by the desired gender.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUILayout.TextField ("If the Slot is null but the Overlay is not, that means for the generator that the overlay IS the element and it will be applied on the body of the avatar.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

				// RPG Collider
				if ( Selection.activeGameObject.GetComponent<DK_RPG_Element_Collider> () == null
					&& Selection.activeGameObject.GetComponentInChildren<DK_RPG_Element_Collider> () == null
					&& Selection.activeGameObject.GetComponentInParent<DK_RPG_Element_Collider> () == null ){
					GUILayout.TextField ("You can add a RPG UMA Collider to your element. add the necessary scripts by clicking on the button.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					GUILayout.TextField ("This Collider will add the element to your avatar.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					if ( GUILayout.Button ("Add the RPG Collider Script to the selection", GUILayout.ExpandWidth (true))) {
						DK_RPG_Element_Collider.CreateOnGameObject(Selection.activeGameObject);
					}
				}
			}
		}							

		else{
			if ((Selection.activeGameObject 
				&& (Selection.activeGameObject.GetComponentInParent<UMA.UMAData> () != null
					|| Selection.activeGameObject.GetComponentInParent<UMADynamicAvatar> () != null
					|| Selection.activeGameObject.GetComponent<UMADynamicAvatar> () != null )
				&& Selection.activeGameObject.GetComponentInParent<DKUMAData> () == null ))
			{
				GUI.color = Color.white;
				GUILayout.TextField ("Your selection is a UMA Avatar. You can import it into DK UMA to be able to handle it with all the Editor Options. ", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUI.color = Green;
				if (GUILayout.Button ("Convert to DK UMA", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OpenDKConvWin ();
				}

			}
		}

		// Selected model
		if (Selection.activeGameObject) {

			if ((Selection.activeGameObject.transform.parent
				&& Selection.activeGameObject.transform.parent.GetComponent<DKUMAData> () == true)
				|| Selection.activeGameObject.GetComponent<DKUMAData> () == true
				|| Selection.activeGameObject.GetComponent<DK_Model> () == true) {
				DK_UMA_Editor.showModify = true;
			} else DK_UMA_Editor.showModify = false;
		}

		// selected Slot
		if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DKSlotData") {
			GUI.color = Color.white;
		/*	using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ("Slot Library :", GUILayout.Width (75));
				GUI.color = Color.white;
				GUILayout.TextField (EditorVariables.DKSlotLibraryObj.name, GUILayout.ExpandWidth (true));
				if (GUILayout.Button ("Change", GUILayout.Width (60))) {
					DK_UMA_Editor.OpenLibrariesWindow ();
					ChangeLibrary.CurrentLibN = EditorVariables.DKSlotLibraryObj.name;
					ChangeLibrary.CurrentLibrary = EditorVariables.DKSlotLibraryObj;
					ChangeLibrary.Action = "";
				}
			}*/
			GUILayout.Space (5);
			GUI.color = Green;
			if ( EditorVariables.DKSlotLibraryObj == null ) EditorVariables.DKSlotLibraryObj = FindObjectOfType<DKSlotLibrary>().gameObject;
			DKSlotLibrary DKSlotLibrary = EditorVariables.DKSlotLibraryObj.GetComponent<DKSlotLibrary> ();
			if (DKSlotLibrary.slotElementList.Contains ((Selection.activeObject as DKSlotData)) == false) {
				if (GUILayout.Button ("Add to selected Library", GUILayout.ExpandWidth (true))) {
					List<DKSlotData> DKSlotLibraryL;
					DKSlotLibraryL = DKSlotLibrary.slotElementList.ToList ();
					DKSlotLibraryL.Add ((Selection.activeObject as DKSlotData));
					DKSlotLibrary.slotElementList = DKSlotLibraryL.ToArray ();
					EditorUtility.SetDirty (DKSlotLibrary);
					AssetDatabase.SaveAssets ();
					DK_UMA_Editor.OnSelectionChange ();
				}
			}
		}

		// selected Overlay
		else if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DKOverlayData") {
		/*	GUI.color = Color.white;
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ("Overlay Library :", GUILayout.Width (75));
				GUI.color = Color.white;
				GUILayout.TextField (EditorVariables.OverlayLibraryObj.name, GUILayout.ExpandWidth (true));
				if (GUILayout.Button ("Change", GUILayout.Width (60))) {
					DK_UMA_Editor.OpenLibrariesWindow ();
					ChangeLibrary.CurrentLibN = EditorVariables.OverlayLibraryObj.name;
					ChangeLibrary.CurrentLibrary = EditorVariables.OverlayLibraryObj;
					ChangeLibrary.Action = "";
				}
			}*/
			GUILayout.Space (5);
			GUI.color = Green;
			DKOverlayLibrary OverlayLibrary = FindObjectOfType<DKOverlayLibrary> ();
			if (OverlayLibrary.overlayElementList.Contains ((Selection.activeObject as DKOverlayData)) == false) {
				if (GUILayout.Button ("Add to selected Library", GUILayout.ExpandWidth (true))) {
					List<DKOverlayData> OverlayLibraryL;
					OverlayLibraryL = OverlayLibrary.overlayElementList.ToList ();
					OverlayLibraryL.Add ((Selection.activeObject as DKOverlayData));
					OverlayLibrary.overlayElementList = OverlayLibraryL.ToArray ();
					EditorUtility.SetDirty (OverlayLibrary);
					AssetDatabase.SaveAssets ();
					DK_UMA_Editor.OnSelectionChange ();
				}
			}
		} 
		// selected Race
		else if (Selection.activeObject && Selection.activeObject.GetType ().ToString () == "DKRaceData") {
			DKRaceLibrary RaceLibrary = EditorVariables.RaceLibraryObj.GetComponent<DKRaceLibrary> ();
			GUI.color = Color.white;
			using (new Horizontal()) {
				GUI.color = Color.yellow;
				GUILayout.Label ("Race Library :", GUILayout.Width (75));
				GUI.color = Color.white;
				GUILayout.TextField (EditorVariables.RaceLibraryObj.name, GUILayout.ExpandWidth (true));
				if (GUILayout.Button ("Change", GUILayout.Width (60))) {
					DK_UMA_Editor.OpenLibrariesWindow ();
					ChangeLibrary.CurrentLibN = EditorVariables.RaceLibraryObj.name;
					ChangeLibrary.CurrentLibrary = EditorVariables.RaceLibraryObj;
					ChangeLibrary.Action = "";
				}
			}
			GUILayout.Space (5);
			GUI.color = Green;
			if (RaceLibrary.raceElementList.Contains ((Selection.activeObject as DKRaceData)) == false) {
				if (GUILayout.Button ("Add to selected Library", GUILayout.ExpandWidth (true))) {
					List<DKRaceData> RaceLibraryL;
					RaceLibraryL = RaceLibrary.raceElementList.ToList ();
					RaceLibraryL.Add ((Selection.activeObject as DKRaceData));
					RaceLibrary.raceElementList = RaceLibraryL.ToArray ();
					EditorUtility.SetDirty (RaceLibrary);
					AssetDatabase.SaveAssets ();
					DK_UMA_Editor.OnSelectionChange ();
				}
			}
		} 
		else {
			if (!Selection.activeObject) {
				GUI.color = Color.white;
				GUILayout.TextField ("You can use the 'Elements Manager' to select the UMA or DK elements from your assets. It will detect all of them in a click.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
				GUI.color = Green;
				if ( EditorVariables.DK_UMACrowd 
					&& EditorVariables.SlotsAnatomyLibraryObj && EditorVariables._SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length != 0
					&& GUILayout.Button ("Elements Manager", GUILayout.ExpandWidth (true))) {
					DK_UMA_Editor.OpenAutoDetectWin ();
				}
			}
		}
	}
}
