using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;


public class DK_UMA_ProjectPacker_Win : EditorWindow {

	Vector2 scroll;
	Color Green = new Color (0.8f, 1f, 0.8f, 1);
//	Color Red = new Color (0.9f, 0.5f, 0.5f);

	public bool ContentChoice;
	public bool ContentListView;

	public static bool DKSlots = true;
	public static bool DKOverlays = true;
	public static bool DKRaces = true;
	public static bool ColorPresets = true;
	public static bool Places = true;
	public static bool AvatarsData = true;
	public static bool EquipmentSets = true;
	public static bool ItemsData = true;
	public static bool DKUMASettings = true;

	// lists
/*	public static List<DKSlotData> SlotsList = new List<DKSlotData>();
	public static List<DKOverlayData> OverlaysList = new List<DKOverlayData>();
	public static List<DKRaceData> RacesList = new List<DKRaceData>();
	public static List<ColorPresetData> ColorPresetsList = new List<ColorPresetData>();
	public static List<DK_SlotsAnatomyData> PlacesList = new List<DK_SlotsAnatomyData>();
	public static List<DK_UMA_AvatarData> AvatarsDataList = new List<DK_UMA_AvatarData>();

*/	
	[MenuItem("UMA/DK Editor/Project Packer")]
	[MenuItem("Window/DK Editors/DK UMA/Project Packer")]
	public static void Init()
	{
		// Get existing open window or if none, make a new one:
		DK_UMA_ProjectPacker_Win window = EditorWindow.GetWindow<DK_UMA_ProjectPacker_Win> (false, "DK UMA Packer");
		window.autoRepaintOnSceneChange = true;
		window.Show ();
	}

	void OnGUI () {
		this.minSize = new Vector2(350, 600);
		this.maxSize = new Vector2(355, 610);

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


		using (new Horizontal()) {
			GUI.color = Color.white ;
			GUILayout.Label ( "DK UMA Project Packer", "toolbarbutton", GUILayout.ExpandWidth (true));
		}
		GUI.color = Color.yellow ;
		EditorGUILayout.HelpBox("It is highly recommanded to save your Content using this tool BEFORE installing any Unity or DK UMA new version.", UnityEditor.MessageType.Warning);

		GUI.color = Color.white ;
		EditorGUILayout.HelpBox("This tool grabs all the DK UMA Content and Packs it as a unityPackage.", UnityEditor.MessageType.None);

		using (new Horizontal()) {
			GUI.color = Color.white;
			if (GUILayout.Button ( "Content to pack", "toolbarbutton", GUILayout.ExpandWidth (true))) {
	//			ContentChoice = !ContentChoice;
			}
	/*		if ( ContentChoice ) GUI.color = Green;
			else GUI.color = Color.white;
			if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				ContentChoice = !ContentChoice;
			}*/
		}
		GUI.color = Color.white ;
		EditorGUILayout.HelpBox("Select the Content types you want to pack.", UnityEditor.MessageType.None);
		// toggles
		using (new Horizontal()) {
			DKSlots = EditorGUILayout.Toggle ( "DK Slots",DKSlots );
			DKOverlays = EditorGUILayout.Toggle ( "DK Overlays",DKOverlays );
		}
		using (new Horizontal()) {
			DKRaces = EditorGUILayout.Toggle ( "DK Races",DKRaces );
			ColorPresets = EditorGUILayout.Toggle ( "ColorPresets",ColorPresets );
		}
		using (new Horizontal()) {
			AvatarsData = EditorGUILayout.Toggle ( "Avatars Data",AvatarsData );
			EquipmentSets = EditorGUILayout.Toggle ( "Equipment Sets",EquipmentSets );
		}
		using (new Horizontal()) {
			ItemsData = EditorGUILayout.Toggle ( "DK Items",ItemsData );
			GUI.color = Color.yellow ;
			DKUMASettings = EditorGUILayout.Toggle ( "DK UMA Settings",DKUMASettings );
		}
		GUI.color = Green ;
		if ( GUILayout.Button ( "Create the List of the selected Content", GUILayout.ExpandWidth (true))) {
			DK_UMAPacker.PackName = "";
			DK_UMAPacker.guidsToPack.Clear();
			DK_UMAPacker.SlotsList.Clear();
			DK_UMAPacker.OverlaysList.Clear();
			DK_UMAPacker.RacesList.Clear();
			DK_UMAPacker.ColorPresetsList.Clear();
			DK_UMAPacker.AvatarsDataList.Clear();
			DK_UMAPacker.PlacesList.Clear();
			DK_UMAPacker.SetsDataList.Clear();
			DK_UMAPacker.ItemsDataList.Clear();
			DK_UMAPacker.DKUMASettingsList.Clear();
			if ( DKSlots ) DK_UMAPacker.FindSlots();
			if ( DKOverlays ) DK_UMAPacker.FindOverlays();
			if ( DKRaces ) DK_UMAPacker.FindRaces();
			if ( ColorPresets ) DK_UMAPacker.FindColorPresets();
			if ( AvatarsData ) DK_UMAPacker.FindAvatars();
			if ( Places ) DK_UMAPacker.FindPlaces();
			if ( EquipmentSets ) DK_UMAPacker.FindSets();
			if ( ItemsData ) DK_UMAPacker.FindItems();
			if ( DKUMASettings) DK_UMAPacker.FindSettings();
		}
		GUILayout.Space(5);
		using (new Horizontal()) {
			GUI.color = Color.white;
			if (GUILayout.Button ( "Selected Content View", "toolbarbutton", GUILayout.ExpandWidth (true))) {
				ContentListView = !ContentListView;
			}
			if ( ContentListView ) GUI.color = Green;
			else GUI.color = Color.white;
			if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				ContentListView = !ContentListView;
			}
		}	
		if ( ContentListView ){
			GUI.color = Color.white;
			EditorGUILayout.HelpBox("Here follows the list of all the Content to pack.", UnityEditor.MessageType.None);
			using (new ScrollView(ref scroll)) {
				// slots
				GUI.color = Color.yellow;
				GUILayout.Label ( "Slots ("+DK_UMAPacker.SlotsList.Count+")", "HelpBox", GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				foreach ( DKSlotData element in DK_UMAPacker.SlotsList ){
					using (new Horizontal()) {
						GUILayout.Label ( "DK Slot", "HelpBox", GUILayout.Width (75));
						GUILayout.Label ( element.name, Slim, GUILayout.ExpandWidth (true));
					}
				}
				// overlays
				GUI.color = Color.yellow;
				GUILayout.Label ( "Overlays ("+DK_UMAPacker.OverlaysList.Count+")", "HelpBox", GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				foreach ( DKOverlayData element in DK_UMAPacker.OverlaysList ){
					using (new Horizontal()) {
						GUILayout.Label ( "DK Overlays", "HelpBox", GUILayout.Width (75));
						GUILayout.Label ( element.name, Slim, GUILayout.ExpandWidth (true));
					}
				}
				// Races
				GUI.color = Color.yellow;
				GUILayout.Label ( "Races ("+DK_UMAPacker.RacesList.Count+")", "HelpBox", GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				foreach ( DKRaceData element in DK_UMAPacker.RacesList ){
					using (new Horizontal()) {
						GUILayout.Label ( "DK Race", "HelpBox", GUILayout.Width (75));
						GUILayout.Label ( element.name, Slim, GUILayout.ExpandWidth (true));
					}
				}
				// Place
				GUI.color = Color.yellow;
				GUILayout.Label ( "Places ("+DK_UMAPacker.PlacesList.Count+")", "HelpBox", GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				foreach ( GameObject element in DK_UMAPacker.PlacesList ){
					using (new Horizontal()) {
						GUILayout.Label ( "Place", "HelpBox", GUILayout.Width (75));
					GUILayout.Label ( element.gameObject.name, Slim, GUILayout.ExpandWidth (true));
					}
				}
				// color presets
				GUI.color = Color.yellow;
				GUILayout.Label ( "Color Presets ("+DK_UMAPacker.ColorPresetsList.Count+")", "HelpBox", GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				foreach ( ColorPresetData element in DK_UMAPacker.ColorPresetsList ){
					using (new Horizontal()) {
						GUILayout.Label ( "Color Preset", "HelpBox", GUILayout.Width (75));
						GUILayout.Label ( element.name, Slim, GUILayout.ExpandWidth (true));
					}
				}
				// Avatars
				GUI.color = Color.yellow;
				GUILayout.Label ( "Avatars ("+DK_UMAPacker.AvatarsDataList.Count+")", "HelpBox", GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				foreach ( DK_UMA_AvatarData element in DK_UMAPacker.AvatarsDataList ){
					using (new Horizontal()) {
						GUILayout.Label ( "Avatar Data", "HelpBox", GUILayout.Width (75));
						GUILayout.Label ( element.name, Slim, GUILayout.ExpandWidth (true));
					}
				}
				// Sets
				GUI.color = Color.yellow;
				GUILayout.Label ( "Equipment Sets ("+DK_UMAPacker.SetsDataList.Count+")", "HelpBox", GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				foreach ( DKEquipmentSetData element in DK_UMAPacker.SetsDataList ){
					using (new Horizontal()) {
						GUILayout.Label ( "Equipment", "HelpBox", GUILayout.Width (75));
						GUILayout.Label ( element.name, Slim, GUILayout.ExpandWidth (true));
					}
				}
				// Items
				GUI.color = Color.yellow;
				GUILayout.Label ( "Items ("+DK_UMAPacker.ItemsDataList.Count+")", "HelpBox", GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				foreach ( DK_UMA_Item element in DK_UMAPacker.ItemsDataList ){
					using (new Horizontal()) {
						GUILayout.Label ( "DK Item", "HelpBox", GUILayout.Width (75));
						GUILayout.Label ( element.name, Slim, GUILayout.ExpandWidth (true));
					}
				}
				// Items
				GUI.color = Color.yellow;
				GUILayout.Label ( "DK UMA Settings ("+DK_UMAPacker.DKUMASettingsList.Count+")", "HelpBox", GUILayout.ExpandWidth (true));
				GUI.color = Color.white;
				foreach ( DK_UMA_GameSettings element in DK_UMAPacker.DKUMASettingsList ){
					using (new Horizontal()) {
						GUILayout.Label ( "DK Settings", "HelpBox", GUILayout.Width (75));
						GUILayout.Label ( element.name, Slim, GUILayout.ExpandWidth (true));
					}
				}
			}
		}
		using (new Horizontal()) {
			GUI.color = Color.white;
			if (GUILayout.Button ( "packaging Process", "toolbarbutton", GUILayout.ExpandWidth (true))) {
				//			ContentChoice = !ContentChoice;
			}
			/*		if ( ContentChoice ) GUI.color = Green;
			else GUI.color = Color.white;
			if (GUILayout.Button ( "Show", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				ContentChoice = !ContentChoice;
			}*/
		}
		int count = DK_UMAPacker.SlotsList.Count
			+DK_UMAPacker.OverlaysList.Count
			+DK_UMAPacker.RacesList.Count
			+DK_UMAPacker.PlacesList.Count
			+DK_UMAPacker.ColorPresetsList.Count
			+DK_UMAPacker.AvatarsDataList.Count
			+DK_UMAPacker.SetsDataList.Count
			+DK_UMAPacker.ItemsDataList.Count
			+DK_UMAPacker.DKUMASettingsList.Count;

		EditorGUILayout.HelpBox("Selected Content count : "+count, UnityEditor.MessageType.None);
	//	EditorGUILayout.HelpBox("paths Count : "+DK_UMAPacker.guidsToPack.Count, UnityEditor.MessageType.None);

	//	GUILayout.Space(5);
	//	if ( PackName != "" ) GUI.color = Green;
	//	else GUI.color = Red;
	//	if ( PackName == "" ) 
		EditorGUILayout.HelpBox("Write a name for the Package to be able to save it.", UnityEditor.MessageType.None);

		DK_UMAPacker.PackName = EditorGUILayout.TextField("Package Name: ", DK_UMAPacker.PackName);

		if ( DK_UMAPacker.PackName != "" && count > 0 ){
			GUI.color = Green ;
			if ( GUILayout.Button ( "Pack the selected Content", GUILayout.ExpandWidth (true))) {
				DK_UMAPacker.PackAssets ();
			}
		}
		GUILayout.Space(10);
		GUI.color = Color.white ;
		EditorGUILayout.HelpBox("This is the first release of this featured tool for DK UMA, " +
			"please contact us about any problem you encounter using it.", UnityEditor.MessageType.Info);

		if ( GUILayout.Button ( "Facebook Page", GUILayout.ExpandWidth (true))) {
			Application.OpenURL ("https://www.facebook.com/DKeditorsUnity3D/");
		}
		GUILayout.Space(5);
	}
}
