using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using UMA.Examples;

public class ImportFromRandomSet : EditorWindow {

	// variable
	UMACrowdRandomSet _RandomSet;
	DKUMA_Variables _DKUMA_Variables;

	public List<UMA.SlotData> SlotsList = new List<UMA.SlotData>();
	public List<DKSlotData> DKSlotsList = new List<DKSlotData>();




	[MenuItem("UMA/DK Editor/Helpers/Import from RandomSet")]
	[MenuItem("Window/DK Editors/DK UMA/Helpers/Import from RandomSet")]
	
	public static void Init()
	{
		// Get existing open window or if none, make a new one:
		ImportFromRandomSet Editorwindow = EditorWindow.GetWindow<ImportFromRandomSet> (false, "Import UMA Set");
		Editorwindow.autoRepaintOnSceneChange = true;
		Editorwindow.Show ();
	}

	void OnEnable () {
		GameObject DK_UMA = GameObject.Find("DK_UMA");
		
		if ( DK_UMA == null ) {
			DK_UMA = (GameObject)PrefabUtility.InstantiatePrefab (Resources.Load ("DK_UMA"));
			Debug.Log ("Instantiating DK_UMA object 3");
			DK_UMA.name = "DK_UMA";
			DK_UMA = GameObject.Find("DK_UMA");
		}
		if ( _DKUMA_Variables == null )
			_DKUMA_Variables = DK_UMA.GetComponent<DKUMA_Variables>();
		_DKUMA_Variables.CleanLibraries ();
		_DKUMA_Variables.SearchAll ();

		// reset
		_RandomSet = null;
		SlotsList.Clear();
		DKSlotsList.Clear();
	}

	void OnGUI () {
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

		#region Title
		GUILayout.Label ( "Import from RandomSet", "toolbarbutton", GUILayout.ExpandWidth (true));
		#endregion Title

		#region Instruction
		GUI.color = Color.white;
		GUILayout.TextField("You can Import all the Slots from a RandomSet and assign the Overlays to them as DK UMA Linked Overlays." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

		if ( _RandomSet == null ) {
			GUI.color = Color.yellow;
			GUILayout.TextField("Select a RandomSet from your project to be able to import it." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		}
		#endregion Instruction

		if ( _RandomSet != null ) {
			#region Main Menu
			using (new Horizontal()) {
				GUI.color = Color.white;
				if (GUILayout.Button ( "Option 1", "toolbarbutton", GUILayout.ExpandWidth (true))) {

				}
				if (GUILayout.Button ( "Option 2", "toolbarbutton", GUILayout.ExpandWidth (true))) {
					
				}
			}
			#endregion Main Menu
			GUI.color = Color.white;
			GUILayout.TextField("Fist you need to list the UMA slots present in the RandomSet." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUILayout.TextField("Then launch the Import process." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			if (GUILayout.Button ( "Import", GUILayout.ExpandWidth (true))) {
				Import ();
			}
		}
	}

	void Import () {
		foreach ( UMACrowdRandomSet.CrowdSlotElement SlotElement in _RandomSet.data.slotElements ) {
			// get slot name
			foreach ( UMACrowdRandomSet.CrowdSlotData possibleSlot in SlotElement.possibleSlots ) {
				// compare name in the _DKUMA_Variables.UMASlotsList
				foreach ( UMA.SlotDataAsset Slot in _DKUMA_Variables.UMASlotsList ) {
					if ( Slot.slotName == possibleSlot.slotID ) {
						AddToDKSlots ( Slot, possibleSlot );
					}
				}			
			}
		}
	}

	#region Add Element
	void AddToDKSlots (UMA.SlotDataAsset Slot, UMACrowdRandomSet.CrowdSlotData possibleSlot ){
		
		#region Slot
		// verify the type
		if ( _DKUMA_Variables.LinkedUMASlotsList.Contains(Slot) == false ){
			// create the new DK element
			DKSlotData newSlot = new DKSlotData() ;
			//copy the values from the UMA element
			newSlot.name = Slot.name ;
			newSlot.overlayScale = Slot.overlayScale ;
			newSlot.slotName = Slot.slotName ;

			// Add the correct slotDNA TO VERIFY IF IT'S NECESSARY
			
			// add the DK element to the DK...List
			_DKUMA_Variables.DKSlotsList.Add (newSlot);		

			// Create the prefab
			System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/");
			// Gender select and create the asset
			// Male
			if ( _RandomSet.data.raceID.Contains("Male") == true && _RandomSet.data.raceID.Contains("Female") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Male/");
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Male/"+newSlot.name+"/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Male/"+newSlot.name+"/"+newSlot.name+".asset");
				newSlot._UMA = Slot;
				// add the name to the DK...
				_DKUMA_Variables.LinkedUMASlotsList.Add (newSlot._UMA);
				if ( newSlot._UMA != null ){
					DKUMA_Variables.LinkedUMASlotData newData = new DKUMA_Variables.LinkedUMASlotData ();
					newData.Name = newSlot.name;
					newData.DKSlot = newSlot;
					newData.UMASlot = newSlot._UMA;
					newData.index = _DKUMA_Variables.LinkedUMASlotsList.Count-1;
					_DKUMA_Variables.LinkedUMASlotsList.Add(newSlot._UMA);
				}
				newSlot.Gender = "Male";
				AssetDatabase.CreateAsset(newSlot, _path);
				AssetDatabase.Refresh ();

			}
			// Female
			if ( _RandomSet.data.raceID.Contains("Female") == true && _RandomSet.data.raceID.Contains("Male") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Female/");
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Female/"+newSlot.name+"/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Female/"+newSlot.name+"/"+newSlot.name+".asset");
				newSlot._UMA = Slot;
				newSlot.Gender = "Female";
				AssetDatabase.CreateAsset(newSlot, _path);
				AssetDatabase.Refresh ();
			}
			// Shared
			if ( _RandomSet.data.raceID.Contains("Female") == false && _RandomSet.data.raceID.Contains("Male") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Shared/");
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Shared/"+newSlot.name+"/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Slots/Shared/"+newSlot.name+"/"+newSlot.name+".asset");
				newSlot._UMA = Slot;
				newSlot.Gender = "Both";
				AssetDatabase.CreateAsset(newSlot, _path);
				AssetDatabase.Refresh ();
			}

			// assign linked overlays
			foreach ( UMACrowdRandomSet.CrowdOverlayElement OverlayElement in possibleSlot.overlayElements ) {
				foreach ( UMACrowdRandomSet.CrowdOverlayData possibleOverlay in OverlayElement.possibleOverlays ) {
					// compare name in the _DKUMA_Variables.UMAOverlaysList
					foreach ( UMA.OverlayDataAsset Overlay in _DKUMA_Variables.UMAOverlaysList ) {
						if ( Overlay.overlayName == possibleOverlay.overlayID ) {
							AddToDKOverlays ( Overlay, newSlot );

						}
					}
				}
			}
		}
		#endregion Slot
	}

	void AddToDKOverlays (UMA.OverlayDataAsset Overlay, DKSlotData DKSlot){

		#region Overlay
		// verify the type
		if ( _DKUMA_Variables.DKOverlaysNamesList.Contains(Overlay.name) == false ){
			// test TODELETE
			
			// create the new DK element
			DKOverlayData newOverlay = ScriptableObject.CreateInstance<DKOverlayData>();
			//copy the values from the UMA element
			newOverlay.name = Overlay.name ;
			if ( Overlay.textureList.Length > 0 ){
				List<Texture2D> tmp_textureList = new List<Texture2D>();
				if ( newOverlay.textureList == null ) newOverlay.textureList = new List<Texture2D>().ToArray();

				tmp_textureList = newOverlay.textureList.ToList();

				foreach ( Texture texture in Overlay.textureList ){
					if ( texture != null ){
						Texture2D newTexture = texture as Texture2D;
						if ( newTexture != null ){
							try {

								tmp_textureList.Add (newTexture);


								newOverlay.textureList.ToList().Add (newTexture);

							}
							catch (ArgumentNullException e){
								Debug.LogError (newOverlay+"/"+Overlay+", "+texture.name+"("+newTexture.name+") : " +e);
							}
						}
					}
				}
			}
			else Debug.LogError ( Overlay.name+" has no texture !!");
			newOverlay.overlayName = Overlay.overlayName ;
			newOverlay.rect = Overlay.rect ;
			newOverlay.tags = Overlay.tags ;
			
			// Add the correct slotDNA TO VERIFY IF IT'S NECESSARY
			
			// add the DK element to the DK...List
			_DKUMA_Variables.DKOverlaysList.Add (newOverlay);
			
			// add the name to the DK...NamesList
			_DKUMA_Variables.DKOverlaysNamesList.Add (newOverlay.name);
			
			// Create the prefab
			System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/");
			// Gender select and create the asset
			// Male
			if ( _RandomSet.data.raceID.Contains("Male") == true && _RandomSet.data.raceID.Contains("Female") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Male/");
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Male/"+newOverlay.name+"/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Male/"+newOverlay.name+"/"+newOverlay.name+".asset");
				newOverlay._UMA = Overlay;
				newOverlay.Gender = "Male";
				AssetDatabase.CreateAsset(newOverlay, _path);
				AssetDatabase.Refresh ();
			}
			// Female
			if ( _RandomSet.data.raceID.Contains("Female") == true  && _RandomSet.data.raceID.Contains("Male") == false ){ 
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Female/");
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Female/"+newOverlay.name+"/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Female/"+newOverlay.name+"/"+newOverlay.name+".asset");
				newOverlay._UMA = Overlay;
				newOverlay.Gender = "Female";
				AssetDatabase.CreateAsset(newOverlay, _path);
				AssetDatabase.Refresh ();
			}
			// Shared
			if ( _RandomSet.data.raceID.Contains("Female") == false && _RandomSet.data.raceID.Contains("Male") == false ){
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Shared/");
				System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Shared/"+newOverlay.name+"/");
				string _path = ("Assets/DK Editors/DK_UMA_Content/Elements/Overlays/Shared/"+newOverlay.name+"/"+newOverlay.name+".asset");
				newOverlay._UMA = Overlay;
				newOverlay.Gender = "Both";
				AssetDatabase.CreateAsset(newOverlay, _path);
				AssetDatabase.Refresh ();

			}

			// Add the new overlay to the DKslot linked overlays list
			if ( DKSlot.LinkedOverlayList.Contains(newOverlay) == false ){
				DKSlot.LinkedOverlayList.Add (newOverlay);
			}
		}
		#endregion Overlay
	}
	#endregion Add Element

	void OnSelectionChange () {
		if ( Selection.activeObject && Selection.activeObject.GetType().ToString() == "UMACrowdRandomSet" ){
			_RandomSet = Selection.activeObject as UMACrowdRandomSet;
		}
		else _RandomSet = null;

		Repaint();
	}
}
