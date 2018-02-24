using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class DetectAndAddDK : MonoBehaviour {
	
	public static UnityEngine.Object Element;
	public static List<DKSlotData> DKSlotsList = new List<DKSlotData>();
	public static  List<DKOverlayData> DKOverlaysList = new List<DKOverlayData>();
	public static DKUMA_Variables _DKUMA_Variables;
	public static GameObject _DK_UMA;

	public static void AddAll(){
		// install UMA
		EditorVariables.DK_UMAObj = GameObject.Find("DK_UMA");
		if ( _DK_UMA == null && EditorVariables.DK_UMAObj != null ) _DK_UMA = EditorVariables.DK_UMAObj;
		if ( EditorVariables.DK_UMAObj == null ) {
			EditorVariables.DK_UMAObj = new GameObject();
			EditorVariables.DK_UMAObj.name = "DK_UMA";
			Debug.Log ( "debug");
		}
	

		GameObject UMA = GameObject.Find("UMA_DCS");
		if ( UMA == null ) UMA = GameObject.Find("UMA");
		EditorVariables.UMAObj = UMA;	
		if ( EditorVariables.UMAObj == null ) {
			EditorVariables.UMAObj = new GameObject();
			EditorVariables.UMAObj.name = "UMA";
			Debug.Log ( "debug");
			
			if ( EditorVariables.DK_UMACrowd == null ) {
				EditorVariables.UMACrowdObj = GameObject.Find("DKUMACrowd");	
				if ( EditorVariables.UMACrowdObj == null ) {
					EditorVariables.UMACrowdObj = (GameObject) PrefabUtility.InstantiatePrefab(Resources.Load("DKUMACrowd"));
					EditorVariables.UMACrowdObj.name = "DKUMACrowd";
					EditorVariables.UMACrowdObj.transform.parent = EditorVariables.DK_UMAObj.transform;
					PrefabUtility.ReconnectToLastPrefab(EditorVariables.UMACrowdObj);
				}
				EditorVariables.DK_UMACrowd =  EditorVariables.UMACrowdObj.GetComponent<DK_UMACrowd>();
				if ( EditorVariables.DK_UMACrowd == null ){
					EditorVariables.UMACrowdObj.gameObject.AddComponent<DK_UMACrowd>();
				}
				EditorVariables.DK_UMACrowd =  EditorVariables.UMACrowdObj.GetComponent<DK_UMACrowd>();
				EditorVariables.UMACrowdObj.transform.parent = EditorVariables.DK_UMAObj.transform;
				EditorVariables.DK_UMACrowd.UMAGenerated = true;
				if ( EditorVariables.DK_UMACrowd.Wears != null ) EditorVariables.DK_UMACrowd.Wears.RanWearYesMax = 25;
				EditorVariables.DK_UMACrowd.UseLinkedOv = true;
				
				if ( EditorVariables.DKSlotLibraryObj == null ) EditorVariables.DKSlotLibraryObj = GameObject.Find("DKSlotLibrary");	
				if ( EditorVariables.OverlayLibraryObj == null ) EditorVariables.OverlayLibraryObj = GameObject.Find("DKOverlayLibrary");	
				if ( EditorVariables.RaceLibraryObj == null ) EditorVariables.RaceLibraryObj = GameObject.Find("DKRaceLibrary");	
				
				if ( EditorVariables.RaceLibraryObj == null ) {
					EditorVariables.RaceLibraryObj = (GameObject) PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Race Libraries/DKRaceLibrary.prefab", typeof(GameObject) ) );
					EditorVariables.RaceLibraryObj.name = "DKRaceLibrary";
					EditorVariables.RaceLibraryObj.transform.parent = EditorVariables.DK_UMAObj.transform;
					PrefabUtility.ReconnectToLastPrefab(EditorVariables.RaceLibraryObj);
				}
				if ( EditorVariables.DKSlotLibraryObj == null ) {
					EditorVariables.DKSlotLibraryObj = (GameObject) PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Slot Libraries/DKSlotLibrary.prefab", typeof(GameObject)) );
					EditorVariables.DKSlotLibraryObj.name = "DKSlotLibrary";
					EditorVariables._DKSlotLibrary = EditorVariables.DKSlotLibraryObj.gameObject.GetComponent<DKSlotLibrary>();
					_DKUMA_Variables.ActiveSlotLibrary = EditorVariables._DKSlotLibrary;
					EditorVariables.DKSlotLibraryObj.transform.parent = EditorVariables.DK_UMAObj.transform;
					PrefabUtility.ReconnectToLastPrefab(EditorVariables.DKSlotLibraryObj);
				}
				if ( EditorVariables.OverlayLibraryObj == null ) {
					
					EditorVariables.OverlayLibraryObj = (GameObject) PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Overlay Libraries/DKOverlayLibrary.prefab", typeof(GameObject)) );
					EditorVariables.OverlayLibraryObj.name = "DKOverlayLibrary";
					EditorVariables._OverlayLibrary = EditorVariables.OverlayLibraryObj.gameObject.GetComponent<DKOverlayLibrary>();
					_DKUMA_Variables.ActiveOverlayLibrary = EditorVariables._OverlayLibrary;
					EditorVariables.OverlayLibraryObj.transform.parent = EditorVariables.DK_UMAObj.transform;
					PrefabUtility.ReconnectToLastPrefab(EditorVariables.OverlayLibraryObj);
				}
				
				// Add the variables from the original script
				try {try {
						EditorVariables.DK_UMACrowd.slotLibrary = EditorVariables.DKSlotLibraryObj.GetComponent<DKSlotLibrary>() as DKSlotLibrary;
						EditorVariables.DK_UMACrowd.overlayLibrary = EditorVariables.OverlayLibraryObj.GetComponent<DKOverlayLibrary>() as DKOverlayLibrary;
						EditorVariables.DK_UMACrowd.raceLibrary = EditorVariables.RaceLibraryObj.GetComponent<DKRaceLibrary>() as DKRaceLibrary;
					}catch(NullReferenceException) {}}catch(MissingReferenceException) {}
			}
			
			DK_UMACrowd _DK_UMACrowd = EditorVariables.UMACrowdObj.GetComponent<DK_UMACrowd>();
			if ( EditorVariables.DKSlotLibraryObj == null ) EditorVariables.DKSlotLibraryObj = GameObject.Find("DKSlotLibrary");	
			if ( EditorVariables.OverlayLibraryObj == null ) EditorVariables.OverlayLibraryObj = GameObject.Find("DKOverlayLibrary");	
			if ( EditorVariables.RaceLibraryObj == null ) EditorVariables.RaceLibraryObj = GameObject.Find("DKRaceLibrary");	
			if ( _DK_UMACrowd.raceLibrary == null ){
				_DK_UMACrowd.raceLibrary = EditorVariables.RaceLibraryObj.GetComponent<DKRaceLibrary>();
				if ( _DK_UMACrowd.raceLibrary == null ) _DK_UMACrowd.raceLibrary = EditorVariables.RaceLibraryObj.AddComponent<DKRaceLibrary>() as DKRaceLibrary;
			}
			if ( _DK_UMACrowd.overlayLibrary == null ){
				_DK_UMACrowd.overlayLibrary = EditorVariables.OverlayLibraryObj.GetComponent<DKOverlayLibrary>();
				if ( _DK_UMACrowd.overlayLibrary == null ) _DK_UMACrowd.overlayLibrary = EditorVariables.OverlayLibraryObj.AddComponent<DKOverlayLibrary>() as DKOverlayLibrary;
				
			}
			if ( _DK_UMACrowd.slotLibrary == null ){
				_DK_UMACrowd.slotLibrary = EditorVariables.SlotsAnatomyLibraryObj.GetComponent<DKSlotLibrary>();
				if ( _DK_UMACrowd.slotLibrary == null ) _DK_UMACrowd.slotLibrary = EditorVariables.DKSlotLibraryObj.AddComponent<DKSlotLibrary>() as DKSlotLibrary;
			}
		}
		DetectAll();

	}
	
	public static void DetectAll(){
		EditorVariables.DK_UMAObj = GameObject.Find("DK_UMA");
		if ( EditorVariables.DK_UMAObj == null ) AddAll();
		EditorVariables.UMACrowdObj = GameObject.Find("DKUMACrowd");	
		if ( EditorVariables.UMACrowdObj == null ) {
			EditorVariables.UMACrowdObj = (GameObject) PrefabUtility.InstantiatePrefab(Resources.Load("DKUMACrowd"));
			EditorVariables.UMACrowdObj.name = "DKUMACrowd";
			EditorVariables.UMACrowdObj.transform.parent = EditorVariables.DK_UMAObj.transform;
			PrefabUtility.ReconnectToLastPrefab(EditorVariables.UMACrowdObj);
			Debug.Log ( "debug");
		}
		EditorVariables.UMACrowdObj = GameObject.Find("DKUMACrowd");
		EditorVariables.DK_UMACrowd =  EditorVariables.UMACrowdObj.GetComponent<DK_UMACrowd>();
		
		if ( EditorVariables.RaceLibraryObj == null && EditorVariables.DK_UMACrowd.raceLibrary ) 
			EditorVariables.RaceLibraryObj = GameObject.Find(EditorVariables.DK_UMACrowd.raceLibrary.gameObject.name);
		else if ( EditorVariables.RaceLibraryObj == null ) EditorVariables.RaceLibraryObj = GameObject.Find("DKRaceLibrary");
		if ( EditorVariables.RaceLibraryObj != null ) {
			EditorVariables._RaceLibrary =  EditorVariables.RaceLibraryObj.GetComponent<DKRaceLibrary>();
			EditorVariables.DK_UMACrowd.raceLibrary = EditorVariables._RaceLibrary;
		//	Debug.Log ( "debug");
		}
		
		
		if ( EditorVariables.DKSlotLibraryObj == null && EditorVariables.DK_UMACrowd.slotLibrary ) 
			EditorVariables.DKSlotLibraryObj = GameObject.Find(EditorVariables.DK_UMACrowd.slotLibrary.gameObject.name);
		else if ( EditorVariables.DKSlotLibraryObj == null ) EditorVariables.DKSlotLibraryObj = GameObject.Find("DKSlotLibrary");
		if ( EditorVariables.DKSlotLibraryObj != null ) EditorVariables._DKSlotLibrary =  EditorVariables.DKSlotLibraryObj.GetComponent<DKSlotLibrary>();
		
		if ( EditorVariables.OverlayLibraryObj == null && EditorVariables.DK_UMACrowd.overlayLibrary ) 
			EditorVariables.OverlayLibraryObj = GameObject.Find(EditorVariables.DK_UMACrowd.overlayLibrary.gameObject.name);
		else if ( EditorVariables.OverlayLibraryObj == null ) EditorVariables.OverlayLibraryObj = GameObject.Find("DKOverlayLibrary");
		if ( EditorVariables.OverlayLibraryObj != null ) EditorVariables._OverlayLibrary =  EditorVariables.OverlayLibraryObj.GetComponent<DKOverlayLibrary>();
		
		if (EditorVariables.DK_DKUMACustomization == null ) { 
			EditorVariables.DKUMACustomizationObj = GameObject.Find("DKUMACustomization");	
			if ( EditorVariables.DKUMACustomizationObj == null ) {
				EditorVariables.DKUMACustomizationObj = (GameObject) PrefabUtility.InstantiatePrefab(Resources.Load("DKUMACustomization"));
				EditorVariables.DKUMACustomizationObj.name = "DKUMACustomization";
				EditorVariables.DKUMACustomizationObj.transform.parent = EditorVariables.DK_UMAObj.transform;
				EditorVariables.DK_DKUMACustomization =  EditorVariables.DKUMACustomizationObj.GetComponent<DKUMACustomization>();
			//	Debug.Log ( "debug");
			}
			if ( EditorVariables.DK_DKUMACustomization == null ){
				if ( EditorVariables.DKUMACustomizationObj.gameObject.GetComponent<DKUMACustomization>() == false )
					EditorVariables.DKUMACustomizationObj.gameObject.AddComponent<DKUMACustomization>();
			//	Debug.Log ( "debug");
			}
			EditorVariables.DK_DKUMACustomization =  EditorVariables.DKUMACustomizationObj.GetComponent<DKUMACustomization>();
		//	EditorVariables.DKUMACustomizationObj.transform.parent = EditorVariables.DK_UMAObj.transform;
			PrefabUtility.ReconnectToLastPrefab(EditorVariables.DKUMACustomizationObj);
		}
		DetectDKUMAOnly();
		SearchElements ();
	}

//	public static void DetectWeb(){
//		if ( _DK_UMA.GetComponent<DKUMAWebVariables>() == null ) _DK_UMA.AddComponent<DKUMAWebVariables>();
//	}

	public static void SearchElements () {
		_DK_UMA = GameObject.Find("DK_UMA");
		
		if ( _DK_UMA == null ) {
			_DK_UMA = (GameObject)PrefabUtility.InstantiatePrefab (Resources.Load ("DK_UMA"));
			Debug.Log ("Instantiating DK_UMA object 2");
			_DK_UMA.name = "DK_UMA";
			_DK_UMA = GameObject.Find("DK_UMA");
			Debug.Log ( "debug");
		}
		if ( _DKUMA_Variables == null )
			_DKUMA_Variables = _DK_UMA.GetComponent<DKUMA_Variables>();
		_DKUMA_Variables.CleanLibraries ();
		_DKUMA_Variables.SearchAll ();
		PrepareRPGLists ();
	}

	public static void PrepareRPGLists () {
		_DK_UMA = GameObject.Find("DK_UMA");

//		DK_RPG_UMA_Generator _DK_RPG_UMA_Generator = _DK_UMA.GetComponent<DK_RPG_UMA_Generator>();
	//	_DK_RPG_UMA_Generator.PopulateAllLists ();
	}

	public static void DetectDKUMAOnly(){			
		EditorVariables.SlotsAnatomyLibraryObj = GameObject.Find("DKEditorVariables._SlotsAnatomyLibrary");
		if ( EditorVariables.SlotsAnatomyLibraryObj ) EditorVariables._SlotsAnatomyLibrary =  EditorVariables.SlotsAnatomyLibraryObj.GetComponent<DK_SlotsAnatomyLibrary>();
					
		EditorVariables.DK_UMAObj = GameObject.Find("DK_UMA");
		if ( EditorVariables.DK_UMAObj == null ) {
			EditorVariables.DK_UMAObj = new GameObject();
			EditorVariables.DK_UMAObj.name = "DK_UMA";
			Debug.Log ( "debug");
		}
		
		EditorVariables.DNALibrariesObj = GameObject.Find("DNALibraries");
		if ( EditorVariables.DNALibrariesObj == null ) {
			EditorVariables.DNALibrariesObj = (GameObject) PrefabUtility.InstantiatePrefab(Resources.Load("DNALibraries"));
			EditorVariables.DNALibrariesObj.name = "DNALibraries";
			EditorVariables.DNALibrariesObj.transform.parent = EditorVariables.DK_UMAObj.transform;
		}
		
		EditorVariables.ZeroPoint = GameObject.Find("ZeroPoint");
		if ( EditorVariables.ZeroPoint == null ){
			EditorVariables.ZeroPoint = (GameObject) PrefabUtility.InstantiatePrefab(Resources.Load("ZeroPointDefault"));
			EditorVariables.ZeroPoint.name = "ZeroPoint";
		}
		
		EditorVariables.GenPresetsObj = GameObject.Find("Generator Presets");
		if ( EditorVariables.GenPresetsObj == null ) {
			EditorVariables.GenPresetsObj = (GameObject) PrefabUtility.InstantiatePrefab(Resources.Load("Generator Presets"));
			EditorVariables.GenPresetsObj.transform.parent = EditorVariables.DK_UMAObj.transform;
			EditorVariables.GenPresetsObj.name = "Generator Presets" ;
		}
		
		EditorVariables.ColorPresetsObj = GameObject.Find("Color Presets");
		if ( EditorVariables.ColorPresetsObj == null ) {
			EditorVariables.ColorPresetsObj = PrefabUtility.InstantiatePrefab(Resources.Load("Color Presets")) as GameObject;
			EditorVariables.ColorPresetsObj.transform.parent = EditorVariables.DK_UMAObj.transform;
			EditorVariables.ColorPresetsObj.name = "Color Presets" ;
		}
		if ( EditorVariables.RaceLibraryObj == null ) {
			EditorVariables.RaceLibraryObj = (GameObject) PrefabUtility.InstantiateAttachedAsset(AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Race Libraries/DKRaceLibrary.prefab", typeof(GameObject) ) );
			EditorVariables.RaceLibraryObj.name = "DKRaceLibrary";
			EditorVariables.RaceLibraryObj.transform.parent = EditorVariables.DK_UMAObj.transform;
			//	PrefabUtility.ReconnectToLastPrefab(EditorVariables.RaceLibraryObj);
		}
		if ( EditorVariables.DKSlotLibraryObj == null ) {
			EditorVariables.DKSlotLibraryObj = (GameObject) PrefabUtility.InstantiateAttachedAsset(AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Slot Libraries/DKSlotLibrary.prefab", typeof(GameObject)) );
			EditorVariables.DKSlotLibraryObj.name = "DKSlotLibrary";
			EditorVariables.DKSlotLibraryObj.transform.parent = EditorVariables.DK_UMAObj.transform;
			//	PrefabUtility.ReconnectToLastPrefab(EditorVariables.DKSlotLibraryObj);
		}
		if ( EditorVariables.OverlayLibraryObj == null ) {
			EditorVariables.OverlayLibraryObj = (GameObject) PrefabUtility.InstantiateAttachedAsset(AssetDatabase.LoadAssetAtPath("Assets/DK Editors/DK_UMA_Editor/Prefabs/Libraries/Overlay Libraries/DKOverlayLibrary.prefab", typeof(GameObject)) );
			EditorVariables.OverlayLibraryObj.name = "DKOverlayLibrary";
			EditorVariables.OverlayLibraryObj.transform.parent = EditorVariables.DK_UMAObj.transform;
			//	PrefabUtility.ReconnectToLastPrefab(EditorVariables.OverlayLibraryObj);
	/*		// Find DK_UMA_RPG_Data
			EditorVariables._DK_UMA_RPG_Data = GameObject.Find ("DK_UMA_RPG_Data");
			if (EditorVariables._DK_UMA_RPG_Data == null){
				EditorVariables._DK_UMA_RPG_Data = new GameObject();
				EditorVariables._DK_UMA_RPG_Data.name = "DK_UMA_RPG_Data";
				EditorVariables._DK_UMA_RPG_Data.transform.parent = EditorVariables.DK_UMAObj.transform;
			}
			
			
			// Find the _DK_RPG_UMA_Generator
			EditorVariables._DK_RPG_UMA_Generator = EditorVariables._DK_UMA_RPG_Data.GetComponent<DK_RPG_UMA_Generator>();
			if ( EditorVariables._DK_RPG_UMA_Generator == null ) 
				EditorVariables._DK_RPG_UMA_Generator = EditorVariables._DK_UMA_RPG_Data.AddComponent<DK_RPG_UMA_Generator>()as DK_RPG_UMA_Generator;
			*/
		}
	}
		
	public static void DetectionSelection (){
		Element = Selection.activeObject;
		_DKUMA_Variables = GameObject.Find ("DK_UMA").GetComponent<DKUMA_Variables>();

		if (Element.GetType ().ToString () == "DKSlotData") {
			EditorVariables.TmpName = Element.name;
			DKSlotData _Data = Element as DKSlotData;

			#region Gender
			if (EditorVariables.DetGender){
				if ( Element.name.Contains("Female")){
					_Data.Gender = "Female" ;
					EditorVariables.TmpName = EditorVariables.TmpName.Replace("Female", "");
				}
				else if ( Element.name.Contains("female")){
					_Data.Gender = "Female" ;
					EditorVariables.TmpName = EditorVariables.TmpName.Replace("female", "");
				}
				else if ( Element.name.Contains("Male")){
					_Data.Gender = "Male" ;
					EditorVariables.TmpName = EditorVariables.TmpName.Replace("Male", "");
				}
				else if ( Element.name.Contains("male")){
					_Data.Gender = "Male" ;
					EditorVariables.TmpName = EditorVariables.TmpName.Replace("male", "");
				}
			}
			#endregion Gender
			
			#region races
			if (EditorVariables.DetRace){
				// Add races
				for (int i = 0; i < EditorVariables.RaceToApplyList.Count ; i++)
				{
					if ( _Data.Race.Contains(EditorVariables.RaceToApplyList[i]) == false )
						_Data.Race.Add(EditorVariables.RaceToApplyList[i]);
				}
			}
			#endregion races
			
			#region Link Detection
			if ( EditorVariables.DetLink ) for(int i2 = 0; i2 < DKOverlaysList.Count; i2 ++){
				//		Debug.Log ( i0+" / "+(DKSlotsList.Count)+" "+DKSlotsList[i0]+" & "+i2 +" / "+DKOverlaysList.Count+" "+DKOverlaysList[i2]);
				if ( DKOverlaysList[i2].name==_Data.name ){
					if (DKOverlaysList[i2].LinkedToSlot.Contains(_Data) == false ) {
						DKOverlaysList[i2].LinkedToSlot.Add(_Data);
					}
					if (_Data.overlayList.Contains(DKOverlaysList[i2]) == false ) {
						_Data.overlayList.Add( DKOverlaysList[i2]);
					}
					EditorUtility.SetDirty(_Data);
					EditorUtility.SetDirty(DKOverlaysList[i2]);
				}
			}
			#endregion Link Detection
			
			#region Expressions
			ExpressionLibrary Library = GameObject.Find ("Expressions").GetComponent<ExpressionLibrary>();
			for (int i = 0; i < Library.ExpressionList.Length ; i++)
			{
				if (EditorVariables.TmpName.Contains( Library.ExpressionList[i].name )){
					if ( EditorVariables.DetOvType )_Data.Elem = Library.ExpressionList[i].Elem;
					if ( EditorVariables.DetOvType ) _Data.OverlayType = Library.ExpressionList[i].OverlayType;
					if ( EditorVariables.DetPlace )_Data.Place = Library.ExpressionList[i].Place.GetComponent<DK_SlotsAnatomyElement>();
					if ( EditorVariables.DetOvType )_Data.Replace = Library.ExpressionList[i].Replace;
					if ( EditorVariables.DetWWeight )_Data.WearWeight = Library.ExpressionList[i].WearWeight;
					if ( EditorVariables.DetOvType ){
						_Data.Replace = Library.ExpressionList[i].Replace;
						_Data._HideData.HideMouth = Library.ExpressionList[i].HideMouth;
						_Data._HideData.HideEars = Library.ExpressionList[i].HideEars;
						_Data._HideData.HideBeard = Library.ExpressionList[i].HideBeard;
						_Data._HideData.HideHair = Library.ExpressionList[i].HideHair;
						_Data._HideData.HideHairModule = Library.ExpressionList[i].HideHairModule;
					}
				}
			}
			#endregion Expressions
			
			// finishing
			EditorUtility.SetDirty(Element);
			AssetDatabase.SaveAssets();
		}
	}
		
	public static void DetectionAll (){	
		// slots
		if ( EditorVariables.DetSlots ) {
			DKSlotsList = _DKUMA_Variables.DKSlotsList;
			for (int i0 = 0; i0 < DKSlotsList.Count ; i0++)
			{
				if ( (EditorVariables.SearchResultsOnly 
				      && DKSlotsList[i0].name.ToLower().Contains(DK_UMA_Editor.SearchString.ToLower()))
				    || !EditorVariables.SearchResultsOnly  ){
					
					Element = DKSlotsList[i0] as DKSlotData;
					
					if (Element.GetType ().ToString () == "DKSlotData") {
						EditorVariables.TmpName = Element.name;
						DKSlotData _Data = Element as DKSlotData;
						
						#region Gender
						if ( EditorVariables.DetGender ){
							if ( Element.name.Contains("Female")){
								_Data.Gender = "Female" ;
								EditorVariables.TmpName = EditorVariables.TmpName.Replace("Female", "");
							}
							else if ( Element.name.Contains("female")){
								_Data.Gender = "Female" ;
								EditorVariables.TmpName = EditorVariables.TmpName.Replace("female", "");
							}
							else if ( Element.name.Contains("Male")){
								_Data.Gender = "Male" ;
								EditorVariables.TmpName = EditorVariables.TmpName.Replace("Male", "");
							}
							else if ( Element.name.Contains("male")){
								_Data.Gender = "Male" ;
								EditorVariables.TmpName = EditorVariables.TmpName.Replace("male", "");
							}
						}
						#endregion Gender
						
						#region races
						// Add races
						if ( EditorVariables.DetRace ) for (int i = 0; i < EditorVariables.RaceToApplyList.Count ; i++)
						{
							if ( _Data.Race.Contains(EditorVariables.RaceToApplyList[i]) == false )
								_Data.Race.Add(EditorVariables.RaceToApplyList[i]);
						}
						#endregion races
						
						#region Link Detection
						if ( EditorVariables.DetLink ) for(int i2 = 0; i2 < DKOverlaysList.Count; i2 ++){
							if ( DKOverlaysList[i2].name==DKSlotsList[i0].name ){
								if (DKOverlaysList[i2].LinkedToSlot.Contains(DKSlotsList[i0]) == false ) {
									DKOverlaysList[i2].LinkedToSlot.Add(DKSlotsList[i0]);
									
								}
								if (DKSlotsList[i0].overlayList.Contains(DKOverlaysList[i2]) == false ) {
									DKSlotsList[i0].overlayList.Add( DKOverlaysList[i2]);
									
								}
								EditorUtility.SetDirty(DKSlotsList[i0]);
								EditorUtility.SetDirty(DKOverlaysList[i2]);
							}
						}
						#endregion Link Detection
						
						#region Expressions
						ExpressionLibrary Library = GameObject.Find ("Expressions").GetComponent<ExpressionLibrary>();
						for (int i = 0; i < Library.ExpressionList.Length ; i++)
						{
							if (EditorVariables.TmpName.Contains( Library.ExpressionList[i].name )){
								if ( EditorVariables.DetOvType ) _Data.Elem = Library.ExpressionList[i].Elem;
								if ( EditorVariables.DetOvType ) _Data.OverlayType = Library.ExpressionList[i].OverlayType;
								if ( EditorVariables.DetPlace ) _Data.Place = Library.ExpressionList[i].Place.GetComponent<DK_SlotsAnatomyElement>();
								if ( EditorVariables.DetWWeight )_Data.WearWeight = Library.ExpressionList[i].WearWeight;
								if ( EditorVariables.DetOvType ){
									_Data.Replace = Library.ExpressionList[i].Replace;
									_Data._HideData.HideMouth = Library.ExpressionList[i].HideMouth;
									_Data._HideData.HideEars = Library.ExpressionList[i].HideEars;
									_Data._HideData.HideBeard = Library.ExpressionList[i].HideBeard;
									_Data._HideData.HideHair = Library.ExpressionList[i].HideHair;
									_Data._HideData.HideHairModule = Library.ExpressionList[i].HideHairModule;
								}
							}
							#endregion Expressions
						}
						// finishing
						EditorUtility.SetDirty(Element);
					}
				}
			}
			AssetDatabase.SaveAssets();
		}
		
		if ( EditorVariables.DetOverlay ) {
			DKOverlaysList = _DKUMA_Variables.DKOverlaysList;
			for (int i0 = 0; i0 < DKOverlaysList.Count ; i0++)
			{
				if ( (EditorVariables.SearchResultsOnly 
				      && DKOverlaysList[i0].name.ToLower().Contains(DK_UMA_Editor.SearchString.ToLower()))
				    || !EditorVariables.SearchResultsOnly  ){
					Element = DKOverlaysList[i0] as DKOverlayData;
					
					if (Element.GetType ().ToString () == "DKOverlayData") {
						EditorVariables.TmpName = Element.name;
						DKOverlayData _Data = Element as DKOverlayData;
						
						//	Debug.Log ( "detect gender" );
						
						#region Gender
						if ( EditorVariables.DetGender ){
							if ( Element.name.Contains("Female")){
								_Data.Gender = "Female" ;
								EditorVariables.TmpName = EditorVariables.TmpName.Replace("Female", "");
							}
							else if ( Element.name.Contains("female")){
								_Data.Gender = "Female" ;
								EditorVariables.TmpName = EditorVariables.TmpName.Replace("female", "");
							}
							else if ( Element.name.Contains("Male")){
								_Data.Gender = "Male" ;
								EditorVariables.TmpName = EditorVariables.TmpName.Replace("Male", "");
							}
							else if ( Element.name.Contains("male")){
								_Data.Gender = "Male" ;
								EditorVariables.TmpName = EditorVariables.TmpName.Replace("male", "");
							}
							else _Data.Gender = "Both" ;
						}
						#endregion Gender
						
						#region races
						// Add races
						if ( EditorVariables.DetRace ) for (int i = 0; i < EditorVariables.RaceToApplyList.Count ; i++)
						{
							if ( _Data.Race.Contains(EditorVariables.RaceToApplyList[i]) == false )
								_Data.Race.Add(EditorVariables.RaceToApplyList[i]);
						}
						#endregion races
						
						#region Expressions
						ExpressionLibrary Library = GameObject.Find ("Expressions").GetComponent<ExpressionLibrary>();
						for (int i = 0; i < Library.ExpressionList.Length ; i++)
						{
							if (EditorVariables.TmpName.Contains( Library.ExpressionList[i].name )){
								if ( EditorVariables.DetOvType ) _Data.Elem = Library.ExpressionList[i].Elem;
								if ( EditorVariables.DetOvType ) _Data.OverlayType = Library.ExpressionList[i].OverlayType;
								if ( EditorVariables.DetPlace ) _Data.Place = Library.ExpressionList[i].Place.GetComponent<DK_SlotsAnatomyElement>();
								if ( EditorVariables.DetWWeight )_Data.WearWeight = Library.ExpressionList[i].WearWeight;
							}
						}
						#endregion Expressions
						
						// finishing
						EditorUtility.SetDirty(Element);
					}
				}
			}
			AssetDatabase.SaveAssets();
		}
	}	
	#region Search Elements

#endregion Search Elements



}
